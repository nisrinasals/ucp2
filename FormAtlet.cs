using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Runtime.Caching;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using System.Linq;

namespace ucp2
{
 public partial class FormAtlet : Form
    {
        koneksi kon = new koneksi();
        private string connectionString;

        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
        };
        private const string CacheKey = "AtletData";

        public FormAtlet()
        {
            InitializeComponent();
            connectionString = kon.connectionString();
        }

        private void FormAtlet_Load(object sender, EventArgs e)
        {
            // Mengatur form menjadi fullscreen
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.dgvAtlet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
                        var query = "SELECT nim AS [NIM], nama, prodi, angkatan, cabor FROM dbo.Atlet";
                        var da = new SqlDataAdapter(query, conn);
                        da.Fill(dt);
                    }
                    _cache.Add(CacheKey, dt, _policy);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error memuat data prestasi atlet: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dt = new DataTable();
                }
            }
            dgvAtlet.AutoGenerateColumns = true;
            dgvAtlet.DataSource = dt;
        }

        private void EnsureIndexes()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    var indexScript = @"
                    IF OBJECT_ID('dbo.Atlet', 'U') IS NOT NULL
                        BEGIN
                            IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='idx_Atlet_Nama')
                                CREATE NONCLUSTERED INDEX idx_Atlet_Nama ON dbo.Atlet(nama);
                            IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='idx_Atlet_Cabor')
                                CREATE NONCLUSTERED INDEX idx_Atlet_Cabor ON dbo.Atlet(cabor);
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
            txtNim.Clear();
            txtNama.Clear();
            cB_Prodi.SelectedIndex = -1;
            cB_Prodi.Text = "";
            txtAngkatan.Clear();
            txtCabor.Clear();

            txtNim.Focus();
        }


        private void btnTambah_Click(object sender, EventArgs e)
        {
            string prodi = cB_Prodi.Text.Trim();


            if (string.IsNullOrWhiteSpace(txtNim.Text) ||
                string.IsNullOrWhiteSpace(txtNama.Text) ||
                string.IsNullOrWhiteSpace(prodi) ||
                string.IsNullOrWhiteSpace(txtAngkatan.Text) ||
                string.IsNullOrWhiteSpace(txtCabor.Text))
            {
                MessageBox.Show("Harap isi semua data!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtNim.Text.Trim().Length != 11 || !long.TryParse(txtNim.Text.Trim(), out _))
            {
                MessageBox.Show("NIM harus terdiri dari 11 angka.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtAngkatan.Text.Trim(), out int angkatan) || angkatan < 2010 || angkatan > DateTime.Now.Year)
            {
                MessageBox.Show($"Angkatan harus diisi antara tahun 2010 dan {DateTime.Now.Year}.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!txtNama.Text.Trim().All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            {
                MessageBox.Show("Nama hanya boleh berisi huruf dan spasi.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.Equals(prodi, "Teknik Sipil", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(prodi, "Teknik Mesin", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(prodi, "Teknik Elektro", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(prodi, "Teknologi Informasi", StringComparison.OrdinalIgnoreCase))

            {
                MessageBox.Show("Prodi hanya boleh 'Teknik Sipil', 'Teknik Mesin', 'Teknik Elektro', atau 'Teknologi Informasi'.", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlConnection conn = null;
            SqlTransaction transaction = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                transaction = conn.BeginTransaction();

                using (var checkCmd = new SqlCommand("SELECT COUNT(*) FROM Atlet WHERE nim = @nim", conn, transaction))
                {
                    checkCmd.Parameters.AddWithValue("@nim", txtNim.Text.Trim());

                    int existingCount = (int)checkCmd.ExecuteScalar();

                    if (existingCount > 0)
                    {
                        MessageBox.Show("Data dengan NIM tersebut sudah ada!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        transaction.Rollback();
                        return;
                    }
                }

                using (var cmd = new SqlCommand("AddAtlet", conn, transaction))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@nim", txtNim.Text.Trim());
                    cmd.Parameters.AddWithValue("@nama", txtNama.Text.Trim());
                    cmd.Parameters.AddWithValue("@prodi", prodi);
                    cmd.Parameters.AddWithValue("@angkatan", txtAngkatan.Text.Trim());
                    cmd.Parameters.AddWithValue("@cabor", txtCabor.Text.Trim());

                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();


                MessageBox.Show("Data berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                try
                {
                    transaction?.Rollback();
                }
                catch (Exception rollbackEx)
                {
                    MessageBox.Show("Kesalahan kritis! Gagal melakukan rollback: " + rollbackEx.Message, "Kesalahan Kritis", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                MessageBox.Show("Terjadi kesalahan saat menambahkan data: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn?.Close();
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvAtlet.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih baris yang ingin dihapus terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Yakin ingin meghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            try
            {
                var Nim = dgvAtlet.SelectedRows[0].Cells["nim"].Value.ToString();
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("DeleteAtlet", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nim", Nim);
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
            string prodi = cB_Prodi.Text.Trim();

            if (dgvAtlet.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data yang akan diubah!", "Peringatan");
                return;
            }

            if (txtNim.Text.Trim().Length != 11 || !long.TryParse(txtNim.Text.Trim(), out _))
            {
                MessageBox.Show("NIM harus terdiri dari 11 angka.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtAngkatan.Text.Trim(), out int angkatan) || angkatan < 2010 || angkatan > DateTime.Now.Year)
            {
                MessageBox.Show($"Angkatan harus diisi antara tahun 2010 dan {DateTime.Now.Year}.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!txtNama.Text.Trim().All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            {
                MessageBox.Show("Nama hanya boleh berisi huruf dan spasi.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.Equals(prodi, "Teknik Sipil", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(prodi, "Teknik Mesin", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(prodi, "Teknik Elektro", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(prodi, "Teknologi Informasi", StringComparison.OrdinalIgnoreCase))

            {
                MessageBox.Show("Prodi hanya boleh 'Teknik Sipil', 'Teknik Mesin', 'Teknik Elektro', atau 'Teknologi Informasi'.", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Apakah Anda yakin ingin memperbarui data Atlet ini?", "Konfirmasi Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("UpdateAtlet", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nim", txtNim.Text);
                        cmd.Parameters.AddWithValue("@nama", txtNama.Text);
                        cmd.Parameters.AddWithValue("@prodi", prodi);
                        cmd.Parameters.AddWithValue("@angkatan", txtAngkatan.Text);
                        cmd.Parameters.AddWithValue("@cabor", txtCabor.Text);
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

        private void dgvAtlet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            {
                if (e.RowIndex < 0) return;
                var row = dgvAtlet.Rows[e.RowIndex];
                txtNim.Text = row.Cells[0].Value?.ToString() ?? string.Empty;
                txtNama.Text = row.Cells[1].Value?.ToString() ?? string.Empty;
                cB_Prodi.Text = row.Cells[2].Value?.ToString() ?? string.Empty;
                txtAngkatan.Text = row.Cells[3].Value?.ToString() ?? string.Empty;
                txtCabor.Text = row.Cells[4].Value?.ToString() ?? string.Empty;
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
    }
}
