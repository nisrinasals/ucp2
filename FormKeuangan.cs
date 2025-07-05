using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Runtime.Caching;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.Formula.Functions;
using static NPOI.HSSF.Util.HSSFColor;
using System.Xml.Linq;
namespace ucp2
{
    public partial class FormKeuangan : Form
    {
        private readonly string connectionString = "Server=SAS\\SQLEXPRESS;Database=keuangan2;Integrated Security=True";

        private int _selectedKeuanganId = -1;
        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
        };
        private const string CacheKey = "KeuanganData";
        private const string GlobalSaldoCacheKey = "GlobalSaldo";

        public FormKeuangan()
        {
            InitializeComponent();
        }
        //aku nyoba
        private void FormKeuangan_Load(object sender, EventArgs e)
        {
            EnsureIndexes();
            LoadData();
            DisplayGlobalSaldo();
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
                            K.id_keuangan AS [ID Keuangan],
                            K.nim AS [NIM Atlet],
                            A.nama AS [Nama Atlet],
                            K.jenis_transaksi AS [Jenis Transaksi],
                            K.keterangan AS [Keterangan],
                            K.jumlah AS [Jumlah],
                            K.tanggal AS [Tanggal]
                        FROM 
                            DataKeuangan K
                        INNER JOIN 
                            Atlet A ON K.nim = A.nim
                        ORDER BY K.tanggal DESC, K.id_keuangan DESC;";

                        var da = new SqlDataAdapter(query, conn);
                        da.Fill(dt);
                    }
                    _cache.Add(CacheKey, dt, _policy);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error memuat data keuangan atlet: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dt = new DataTable();
                }

            }
            dgvKeuangan.AutoGenerateColumns = true;
            dgvKeuangan.DataSource = dt;

            if (dgvKeuangan.Columns.Contains("ID Keuangan"))
                dgvKeuangan.Columns["ID Keuangan"].Visible = false;

        }

        private void EnsureIndexes()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    var indexScript = @"
                    IF OBJECT_ID('dbo.DataKeuangan', 'U') IS NOT NULL
                        BEGIN
                            IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='idx_Keuangan_Tanggal')
                                CREATE NONCLUSTERED INDEX idx_Keuangan_Tanggal ON dbo.DataKeuangan(tanggal);
                            IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='idx_Keuangan_JenisTransaksi')
                                CREATE NONCLUSTERED INDEX idx_Keuangan_JenisTransaksi ON dbo.DataKeuangan(jenis_transaksi);
                            IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='idx_Keuangan_NIM')
                                CREATE NONCLUSTERED INDEX idx_Keuangan_NIM ON dbo.DataKeuangan(nim);
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
            cB_JenisTransaksi.SelectedIndex = -1;
            cB_JenisTransaksi.Text = "";
            txtKeterangan.Clear();
            txtJumlah.Clear();

            txtNim.Focus();
        }

        private void DisplayGlobalSaldo()
        {
            decimal globalSaldo = 0;
            if (_cache.Contains(GlobalSaldoCacheKey))
            {
                globalSaldo = (decimal)_cache.Get(GlobalSaldoCacheKey);
            }
            else
            {
                try
                {
                    using (var conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        var query = "SELECT saldo_total FROM GlobalKeuangan WHERE id_global = 1;";
                        using (var cmd = new SqlCommand(query, conn))
                        {
                            var result = cmd.ExecuteScalar();
                            if (result != DBNull.Value && result != null)
                            {
                                globalSaldo = Convert.ToDecimal(result);
                                _cache.Add(GlobalSaldoCacheKey, globalSaldo, _policy);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error memuat saldo total global: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            lblGlobalSaldo.Text = $"Total Saldo: {globalSaldo:C}"; // :C untuk pemformatan mata uang
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            string nimInput = txtNim.Text.Trim();
            string keterangan = txtKeterangan.Text.Trim();
            string jenisTransaksi = cB_JenisTransaksi.Text.Trim();
            decimal jumlah;

            if (string.IsNullOrWhiteSpace(keterangan) || 
                string.IsNullOrWhiteSpace(jenisTransaksi) || 
                string.IsNullOrWhiteSpace(nimInput) ||
                string.IsNullOrWhiteSpace(txtJumlah.Text))
            {
                MessageBox.Show("NIM, Keterangan, Jenis Transaksi, dan Jumlah harus diisi.", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.Equals(jenisTransaksi, "Pemasukan", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(jenisTransaksi, "Pengeluaran", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Jenis Transaksi hanya boleh 'Pemasukan' atau 'Pengeluaran'.", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtJumlah.Text, out jumlah) || jumlah <= 0)
            {
                MessageBox.Show("Jumlah harus berupa angka positif dan valid.", "Validasi Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;

                try
                { 
                    conn.Open();
                    transaction = conn.BeginTransaction(); 

                    SqlCommand cmd = new SqlCommand("AddTransaksiKeuangan", conn); 
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Transaction = transaction; 


                    cmd.Parameters.AddWithValue("@jenis_transaksi", jenisTransaksi);
                    cmd.Parameters.AddWithValue("@keterangan", keterangan);
                    cmd.Parameters.AddWithValue("@jumlah", jumlah);
                    cmd.Parameters.AddWithValue("@nim", nimInput);
                    cmd.ExecuteNonQuery(); 
                    transaction.Commit();

                    MessageBox.Show("Transaksi berhasil disimpan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _cache.Remove(CacheKey);
                    _cache.Remove(GlobalSaldoCacheKey);
                    LoadData();
                    DisplayGlobalSaldo();
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
            _cache.Remove(GlobalSaldoCacheKey);
            LoadData();
            DisplayGlobalSaldo();
        }

        private void dgvKeuangan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvKeuangan.Rows.Count > 0)
            {
                try
                {
                    var row = dgvKeuangan.Rows[e.RowIndex];

                    _selectedKeuanganId = Convert.ToInt32(row.Cells["ID Keuangan"].Value);

                    txtNim.Text = row.Cells["NIM Atlet"].Value?.ToString() ?? string.Empty; 
                    txtKeterangan.Text = row.Cells["Keterangan"].Value?.ToString() ?? string.Empty;
                    cB_JenisTransaksi.Text = row.Cells["Jenis Transaksi"].Value?.ToString() ?? string.Empty;
                    txtJumlah.Text = row.Cells["Jumlah"].Value?.ToString() ?? string.Empty;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saat memilih data: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        private void btnReport_Click(object sender, EventArgs e)
        {
            report formReport = new report();
            formReport.ShowDialog();
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
