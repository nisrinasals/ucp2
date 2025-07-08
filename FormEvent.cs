using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Runtime.Caching;
using System.Linq;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;

namespace ucp2
{
    public partial class FormEvent : Form
    {
        koneksi kon = new koneksi();
        private readonly string connectionString;

        private int selectedEventId = -1;
        private int selectedPartisipasiId = -1;
        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
        };
        private const string CacheKey = "EventData";
        public FormEvent()
        {
            InitializeComponent();
            connectionString = kon.connectionString();
        }

        private void FormEvent_Load(object sender, EventArgs e)
        {
            // Mengatur form menjadi fullscreen
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
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
                            PA.peran_partisipasi AS [Peran],
                            PA.id_partisipasi AS [ID Partisipasi]
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
            if (dgvEvent.Columns.Contains("ID Partisipasi"))
                dgvEvent.Columns["ID Partisipasi"].Visible = false;

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
            cB_JenisEvent.SelectedIndex = -1; // Mengosongkan pilihan ComboBox
            cB_JenisEvent.Text = "";
            txtKeterangan.Clear();
            txtPeran.Clear();

            txtNIM.Focus();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            string nimAtlet = txtNIM.Text.Trim();
            string namaEvent = txtEvent.Text.Trim();
            string jenisEvent = cB_JenisEvent.Text.Trim();
            string keterangan = txtKeterangan.Text.Trim();
            string peran = txtPeran.Text.Trim();

            if (string.IsNullOrWhiteSpace(txtNIM.Text) ||
                string.IsNullOrWhiteSpace(txtEvent.Text) ||
                string.IsNullOrWhiteSpace(jenisEvent) ||
                string.IsNullOrWhiteSpace(txtKeterangan.Text) ||
                string.IsNullOrWhiteSpace(txtPeran.Text))
            {
                MessageBox.Show("Harap isi semua data!", "Peringatan");
                return;
            }

            DateTime selectedDate = dtpTanggal.Value;

            if (selectedDate.Date > DateTime.Today)
            {
                MessageBox.Show("Tanggal event tidak boleh di masa depan.", "Validasi Tanggal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selectedDate < DateTime.Today.AddYears(-15))
            {
                MessageBox.Show("Tanggal event terlalu jauh di masa lalu.", "Validasi Tanggal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.Equals(jenisEvent, "Kompetisi", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(jenisEvent, "Pelatihan", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(jenisEvent, "Sosial", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(jenisEvent, "Lainnya", StringComparison.OrdinalIgnoreCase))

            {
                MessageBox.Show("Jenis Event hanya boleh 'Kompetisi', 'Pelatihan', 'Sosial', atau 'Lainnya'.", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            if (MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("DeleteEvent", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_event", idEventToDelete);
                        cmd.ExecuteNonQuery();
                    }
                }
                _cache.Remove(CacheKey);
                MessageBox.Show("Data berhasil dihapus!", "Sukses");
                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Kesalahan");
            }
        }

        private void dgvEvent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvEvent.Rows[e.RowIndex];

                if (row.Cells["ID Event"].Value != null && int.TryParse(row.Cells["ID Event"].Value.ToString(), out selectedEventId))
                {
                    if (row.Cells["ID Partisipasi"].Value != null && int.TryParse(row.Cells["ID Partisipasi"].Value.ToString(), out selectedPartisipasiId))
                    {
                        // selectedPartisipasiId berhasil diambil
                    }
                    else
                    {
                        selectedPartisipasiId = -1; 
                        MessageBox.Show("ID Partisipasi tidak valid atau tidak ditemukan di baris yang dipilih.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    txtEvent.Text = row.Cells["Nama Event"].Value?.ToString();
                    cB_JenisEvent.Text = row.Cells["Jenis Event"].Value?.ToString();
                    txtKeterangan.Text = row.Cells["Keterangan"].Value?.ToString(); 

                    if (row.Cells["Tanggal"].Value != null && DateTime.TryParse(row.Cells["Tanggal"].Value.ToString(), out DateTime eventDate))
                    {
                        dtpTanggal.Value = eventDate;
                    }
                    else
                    {
                        dtpTanggal.Value = DateTime.Today;
                    }
                    txtNIM.Text = row.Cells["NIM"].Value?.ToString(); 
                    txtPeran.Text = row.Cells["Peran"].Value?.ToString(); 

                }
                else
                {
                    selectedEventId = -1;
                    MessageBox.Show("Gagal mendapatkan ID Event dari baris yang dipilih.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedEventId == -1)
            {
                MessageBox.Show("Pilih event yang ingin diperbarui dari tabel terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string namaEvent = txtEvent.Text.Trim();
            string jenisEvent = cB_JenisEvent.Text.Trim();
            string keteranganEvent = txtKeterangan.Text.Trim();
            DateTime selectedEventDate = dtpTanggal.Value;
            string nimAtlet = txtNIM.Text.Trim();
            string peranPartisipasi = txtPeran.Text.Trim();

            if (string.IsNullOrWhiteSpace(namaEvent) ||
                string.IsNullOrWhiteSpace(jenisEvent) ||
                string.IsNullOrWhiteSpace(keteranganEvent))
            {
                MessageBox.Show("Nama Event, Jenis Event, dan Keterangan tidak boleh kosong.", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selectedEventDate > DateTime.Today)
            {
                MessageBox.Show("Tanggal event tidak boleh lebih dari 1 tahun di masa depan.", "Validasi Tanggal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (selectedEventDate < DateTime.Today.AddYears(-10))
            {
                MessageBox.Show("Tanggal event terlalu jauh di masa lalu.", "Validasi Tanggal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.Equals(jenisEvent, "Kompetisi", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(jenisEvent, "Pelatihan", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(jenisEvent, "Sosial", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(jenisEvent, "Lainnya", StringComparison.OrdinalIgnoreCase))

            {
                MessageBox.Show("Jenis Event hanya boleh 'Kompetisi', 'Pelatihan', 'Sosial', atau 'Lainnya'.", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Apakah Anda yakin ingin memperbarui data Event ini?", "Konfirmasi Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return; 
            }

            SqlConnection conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                transaction = conn.BeginTransaction();

                using (SqlCommand cmd = new SqlCommand("UpdateEventAndPartisipasi", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Transaction = transaction;
                    cmd.Parameters.AddWithValue("@id_event", selectedEventId); 
                    cmd.Parameters.AddWithValue("@nama_event", namaEvent);
                    cmd.Parameters.AddWithValue("@jenis_event", jenisEvent);
                    cmd.Parameters.AddWithValue("@tanggal", selectedEventDate.Date); 
                    cmd.Parameters.AddWithValue("@keterangan", keteranganEvent);

                    cmd.Parameters.AddWithValue("@id_partisipasi", selectedPartisipasiId);
                    cmd.Parameters.AddWithValue("@new_nim", nimAtlet);
                    cmd.Parameters.AddWithValue("@new_peran_partisipasi", peranPartisipasi);

                    cmd.ExecuteNonQuery();

                }
                transaction.Commit(); 

                _cache.Remove(CacheKey);

                MessageBox.Show("Data Event dan Partisipasi Atlet berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearForm();
                selectedEventId = -1;
                selectedPartisipasiId = -1;
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            reportEvent formReportEvent = new reportEvent();
            formReportEvent.ShowDialog();
        }

        private void PreviewData(string filePath)
        {
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    IWorkbook workbook = new XSSFWorkbook(fs);
                    ISheet sheet = workbook.GetSheetAt(0);
                    DataTable dt = new DataTable();

                    IRow headerRow = sheet.GetRow(0);
                    foreach (var cell in headerRow.Cells)
                        dt.Columns.Add(cell.ToString());

                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        IRow dataRow = sheet.GetRow(i);
                        DataRow newRow = dt.NewRow();
                        int cellIndex = 0;
                        foreach (var cell in dataRow.Cells)
                        {
                            newRow[cellIndex++] = cell.ToString();
                        }
                        dt.Rows.Add(newRow);
                    }

                    ReviewForm previewForm = new ReviewForm(dt);
                    previewForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading the Excel file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xlsx;*.xlsm";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                PreviewData(filePath);
            }
        }
    }
}
