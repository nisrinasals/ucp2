using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Runtime.Caching;

namespace ucp2
{
    public partial class FormEvent : Form
    {
        private readonly string connectionString = "Server=SAS\\SQLEXPRESS;Database=keuangan2;Integrated Security=True";

        private int _selectedRelasiId = -1;
        private int _selectedPrestasiDefinitionId = -1;
        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
        };
        private const string CacheKey = "EventData";
        public FormEvent()
        {
            InitializeComponent();
        }

        private void FormEvent_Load(object sender, EventArgs e)
        {
            EnsureIndexes();
            LoadData();
        }

        private void LoadData()
        {
            DataTable dt;
            if (_cache.Contains(CacheKey))
            {
                dt = _cache.Get(CacheKey) as DataTable;
            }
            else
            {
                dt = new DataTable();
                try
                {
                    using (var conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        var query = @"
                        SELECT
                            E.id_event AS [ID Event],
                            E.nama_event AS [Nama Event],
                            E.jenis_event AS [Jenis Event],
                            E.keterangan AS [Keterangan],
                            E.tanggal AS [Tanggal],
                            PA.nim AS [NIM],
                            PA.peran_partisipasi AS [Peran]
                        FROM 
                            Event E
                        INNER JOIN 
                            Partisipasi_Atlet PA ON E.id_event = PA.id_event";

                        var da = new SqlDataAdapter(query, conn);
                        da.Fill(dt);
                    }
                    _cache.Add(CacheKey, dt, _policy);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error memuat data event atlet: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dt = new DataTable();
                }

            }
            dgvEvent.AutoGenerateColumns = true;
            dgvEvent.DataSource = dt;

            if (dgvEvent.Columns.Contains("ID Event"))
                dgvEvent.Columns["ID Event"].Visible = false;

        }

        private void EnsureIndexes()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    var indexScript = @"
                    IF OBJECT_ID('dbo.Event', 'U') IS NOT NULL
                        BEGIN
                            IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='idx_Event_Nama')
                                CREATE NONCLUSTERED INDEX idx_Event_Nama ON dbo.Event(nama_event);
                            IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='idx_Event_JenisEvent')
                                CREATE NONCLUSTERED INDEX idx_Event_JenisEvent ON dbo.Event(jenis_event);
                            IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='idx_Event_Tanggal')
                                CREATE NONCLUSTERED INDEX idx_Event_Tanggal ON dbo.Event(tanggal);
                        END";

                    using (var cmd = new SqlCommand(indexScript, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saat memastikan indeks: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearForm()
        {
            txtNIM.Clear();
            txtEvent.Clear();
            txtJenisEvent.Clear();
            txtKeterangan.Clear();
            txtPeran.Clear();

            txtNIM.Focus();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            string nimAtlet = txtNIM.Text.Trim();
            string namaEvent = txtEvent.Text.Trim();
            string jenisEvent = txtJenisEvent.Text.Trim();
            string keterangan = txtKeterangan.Text.Trim();
            string peran = txtPeran.Text.Trim();

            if (string.IsNullOrWhiteSpace(txtNIM.Text) ||
                string.IsNullOrWhiteSpace(txtEvent.Text) ||
                string.IsNullOrWhiteSpace(txtJenisEvent.Text) ||
                string.IsNullOrWhiteSpace(txtKeterangan.Text) ||
                string.IsNullOrWhiteSpace(txtPeran.Text))
            {
                MessageBox.Show("Harap isi semua data!", "Peringatan");
                return;
            }

            DateTime selectedDate = dtpTanggal.Value;

            if (selectedDate > DateTime.Today)
            {
                MessageBox.Show("Tanggal event tidak boleh di masa depan.", "Validasi Tanggal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selectedDate < DateTime.Today.AddYears(-100))
            {
                MessageBox.Show("Tanggal event terlalu jauh di masa lalu.", "Validasi Tanggal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;

                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    SqlCommand cmd = new SqlCommand("AddEventWithPartisipasi", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Transaction = transaction;


                    cmd.Parameters.AddWithValue("@nama_event", namaEvent);
                    cmd.Parameters.AddWithValue("@jenis_event", jenisEvent);
                    cmd.Parameters.AddWithValue("@tanggal", selectedDate.Date);
                    cmd.Parameters.AddWithValue("@keterangan", keterangan);
                    cmd.Parameters.AddWithValue("@peran_partisipasi", peran);
                    cmd.Parameters.AddWithValue("@nim", nimAtlet);
                    cmd.ExecuteNonQuery();
                    transaction.Commit();

                    MessageBox.Show("Event atlet berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _cache.Remove(CacheKey);
                    LoadData();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _cache.Remove(CacheKey);
            LoadData();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening FormMenu: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvEvent.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih baris event yang ingin dihapus terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idEventToDelete;
            if (!int.TryParse(dgvEvent.SelectedRows[0].Cells["ID Event"].Value?.ToString(), out idEventToDelete)) 
            {
                MessageBox.Show("ID Event tidak valid atau tidak ditemukan di baris yang dipilih.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Apakah Anda yakin ingin menghapus Event dengan ID: {idEventToDelete} dan semua partisipasi atlet yang terkait?",
                "Konfirmasi Hapus Data",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                SqlConnection conn = null; 
                try
                {
                    conn = new SqlConnection(connectionString);
                    conn.Open();


                    using (SqlCommand cmd = new SqlCommand("DeleteEvent", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_event", idEventToDelete);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Event dan partisipasi terkait berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData(); 
                            ClearForm(); 
                        }
                        else
                        {
                            MessageBox.Show("Gagal menghapus Event.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat menghapus data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
