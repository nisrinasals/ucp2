using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Runtime.Caching;


namespace ucp2
{
    public partial class FormPrestasi : Form
    {
        private readonly string connectionString = "Server=SAS\\SQLEXPRESS;Database=keuangan2;Integrated Security=True";

        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
        };
        private const string CacheKey = "PrestasiData";
        public FormPrestasi()
        {
            InitializeComponent();
        }

        private void FormPrestasi_Load(object sender, EventArgs e)
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
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var query = "SELECT nama_prestasi AS [Nama Prestasi], tingkat_prestasi, tahun_prestasi, id_prestasi FROM dbo.Prestasi";
                    var da = new SqlDataAdapter(query, conn);
                    da.Fill(dt);
                }
                _cache.Add(CacheKey, dt, _policy);
            }
            dgvPrestasi.AutoGenerateColumns = true;
            dgvPrestasi.DataSource = dt;
        }

        private void EnsureIndexes()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    var indexScript = @"
                    IF OBJECT_ID('dbo.Prestasi', 'U') IS NOT NULL
                        BEGIN
                            IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='idx_Prestasi_Nama')
                                CREATE NONCLUSTERED INDEX idx_Prestasi_Nama ON dbo.Prestasi(nama_prestasi);
                            IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='idx_Prestasi_Tahun')
                                CREATE NONCLUSTERED INDEX idx_Prestasi_Tahun ON dbo.Prestasi(tahun_prestasi);
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
            txtPrestasi.Clear();
            txtTingkat.Clear();
            txtTahun.Clear();

            //Fokus kembali ke NIM user siap memasukkan data baru
            txtPrestasi.Focus();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPrestasi.Text))
            {
                MessageBox.Show("Harap isi semua data!", "Peringatan");
                return;
            }
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("AddPrestasi", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nama_prestasi", txtPrestasi.Text.Trim());
                        cmd.Parameters.AddWithValue("@tingkat_prestasi", txtTingkat.Text.Trim());
                        cmd.Parameters.AddWithValue("@tahun_prestasi", txtTahun.Text.Trim());
                        cmd.ExecuteNonQuery();
                    }
                }
                _cache.Remove(CacheKey);
                MessageBox.Show("Data berhasil ditambahkan!", "Sukses");
                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Kesalahan");
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvPrestasi.SelectedRows.Count == 0) return;
            if (MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            try
            {
                var idPrestasiToDelete = Convert.ToInt32(dgvPrestasi.SelectedRows[0].Cells["id_prestasi"].Value);
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("DeletePrestasi", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_prestasi", idPrestasiToDelete);
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvPrestasi.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data yang akan diubah!", "Peringatan");
                return;
            }
            try
            {
                int idPrestasiToUpdate = Convert.ToInt32(dgvPrestasi.SelectedRows[0].Cells["id_prestasi"].Value);
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("UpdatePrestasi", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_prestasi", idPrestasiToUpdate);
                        cmd.Parameters.AddWithValue("@nama_prestasi", txtPrestasi.Text);
                        cmd.Parameters.AddWithValue("@tingkat_prestasi", txtTingkat.Text);
                        cmd.Parameters.AddWithValue("@tahun_prestasi", txtTahun.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
                _cache.Remove(CacheKey);
                MessageBox.Show("Data berhasil diperbarui", "Sukses");
                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Kesalahan");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _cache.Remove(CacheKey);
            LoadData();
        }

        private void dgvPrestasi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvPrestasi.Rows.Count) return;
            try
            {
                var row = dgvPrestasi.Rows[e.RowIndex];
                txtPrestasi.Text = row.Cells["Nama Prestasi"].Value?.ToString() ?? string.Empty;
                txtTingkat.Text = row.Cells["tingkat_prestasi"].Value?.ToString() ?? string.Empty;
                txtTahun.Text = row.Cells["tahun_prestasi"].Value?.ToString() ?? string.Empty;
            }
            catch (Exception ex)
            {
                // Tangani error jika ada masalah saat mengambil nilai sel
                MessageBox.Show("Error saat memilih data: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
    }
}
