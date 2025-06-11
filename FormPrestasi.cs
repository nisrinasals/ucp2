using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Runtime.Caching;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;


namespace ucp2
{
    public partial class FormPrestasi : Form
    {
        private readonly string connectionString = "Server=LAPTOP-I0H7METT\\CHESTAYURCEL;Database=keuangan2;Integrated Security=True";

        private int _selectedRelasiId = -1;
        private int _selectedPrestasiDefinitionId = -1;
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
                try
                {
                    using (var conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        var query = @"
                        SELECT
                            PA.id_prestasi AS [ID Relasi],
                            A.nim AS [NIM Atlet],
                            A.nama AS [Nama Atlet],
                            P.id_prestasi AS [ID Prestasi],
                            P.nama_prestasi AS [Nama Prestasi],
                            P.tingkat_prestasi AS [Tingkat],
                            P.tahun_prestasi AS [Tahun]
                        FROM 
                            Prestasi_Atlet PA
                        INNER JOIN 
                            Atlet A ON PA.nim = A.nim
                        INNER JOIN 
                            Prestasi P ON PA.id_prestasi = P.id_prestasi
                        ORDER BY 
                            A.nama, P.tahun_prestasi DESC, P.nama_prestasi ASC;";
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
            dgvPrestasi.AutoGenerateColumns = true;
            dgvPrestasi.DataSource = dt;

            if (dgvPrestasi.Columns.Contains("ID Relasi"))
                dgvPrestasi.Columns["ID Relasi"].Visible = false;
            if (dgvPrestasi.Columns.Contains("ID Prestasi"))
                dgvPrestasi.Columns["ID Prestasi"].Visible = false;
        }
    //    aku nyoba
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
            txtNIM.Clear();
            txtPrestasi.Clear();
            txtTingkat.Clear();
            txtTahun.Clear();
            _selectedRelasiId = -1; 
            _selectedPrestasiDefinitionId = -1;

            //Fokus kembali ke NIM user siap memasukkan data baru
            txtNIM.Focus();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNIM.Text) ||
                string.IsNullOrWhiteSpace(txtPrestasi.Text) ||
                string.IsNullOrWhiteSpace(txtTingkat.Text) ||
                string.IsNullOrWhiteSpace(txtTahun.Text))
            {
                MessageBox.Show("Harap isi semua data!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    using (var cmd = new SqlCommand("AddPrestasiForAtlet", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@nim", txtNIM.Text.Trim());
                        cmd.Parameters.AddWithValue("@nama_prestasi", txtPrestasi.Text.Trim());
                        cmd.Parameters.AddWithValue("@tingkat_prestasi", txtTingkat.Text.Trim());
                        cmd.Parameters.AddWithValue("@tahun_prestasi", txtTahun.Text.Trim());

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Data prestasi berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadData();
                ClearForm();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Gagal menambahkan data: " + ex.Message, "Kesalahan Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan umum: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (_selectedRelasiId == -1)
            {
                MessageBox.Show("Silakan pilih baris relasi yang ingin dihapus dari tabel.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("DeletePrestasi", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_prestasi", _selectedRelasiId);
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
            if (_selectedPrestasiDefinitionId == -1)
            {
                MessageBox.Show("Silakan pilih baris yang ingin diupdate.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Yakin ingin mengupdate data ini?", "Konfirmasi", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("UpdatePrestasi", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_prestasi", _selectedPrestasiDefinitionId);
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
                _selectedRelasiId = Convert.ToInt32(row.Cells["ID Relasi"].Value);
                _selectedPrestasiDefinitionId = Convert.ToInt32(row.Cells["ID Prestasi"].Value);
                txtNIM.Text = row.Cells["NIM Atlet"].Value?.ToString() ?? string.Empty;
                txtPrestasi.Text = row.Cells["Nama Prestasi"].Value?.ToString() ?? string.Empty;
                txtTingkat.Text = row.Cells["Tingkat"].Value?.ToString() ?? string.Empty;
                txtTahun.Text = row.Cells["Tahun"].Value?.ToString() ?? string.Empty;
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

//                    ReviewForm previewForm = new ReviewForm(dt);
//                    previewForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading the Excel file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnImport_Click_1(object sender, EventArgs e)
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
