using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Runtime.Caching;
namespace ucp2
{
    public partial class FormKeuangan : Form
    {
        private readonly string connectionString = "Server=SAS\\SQLEXPRESS;Database=keuangan2;Integrated Security=True";

        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
        };
        private const string CacheKey = "KeuanganData";
        public FormKeuangan()
        {
            InitializeComponent();
        }

        private void FormKeuangan_Load(object sender, EventArgs e)
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
                            K.id_keuangan AS [ID Keuangan],
                            K.nim AS [NIM Atlet],
                            A.nama AS [Nama Atlet],
                            K.jenis_transaksi AS [Jenis Transaksi],
                            K.keterangan AS [Keterangan],
                            K.jumlah AS [Jumlah],
                            K.tanggal AS [Tanggal],
                            K.saldo_total
                        FROM 
                            DataKeuangan K
                        INNER JOIN 
                            Atlet A ON K.nim = A.nim";

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
            txtJenis.Clear();
            txtKeterangan.Clear();
            txtJumlah.Clear();

            //Fokus kembali ke NIM user siap memasukkan data baru
            txtNim.Focus();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {

        }

        private void btnHapus_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            _cache.Remove(CacheKey);
            LoadData();
        }

        private void dgvKeuangan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
