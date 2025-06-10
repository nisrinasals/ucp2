using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Runtime.Caching;

namespace ucp2
{
 //agung in here
 //ucel juga
 public partial class FormAtlet : Form
    {
        private readonly string connectionString = "Server=SAS\\SQLEXPRESS;Database=keuangan2;Integrated Security=True";

        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
        };
        private const string CacheKey = "AtletData";

        public FormAtlet()
        {
            InitializeComponent();
        }

        private void FormAtlet_Load(object sender, EventArgs e)
        {
            EnsureIndexes();
            LoadData();
        }
        //nih gung
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
        //nih sals

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
            txtProdi.Clear();
            txtAngkatan.Clear();
            txtCabor.Clear();

            txtNim.Focus();
        }


        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNim.Text) ||
                string.IsNullOrWhiteSpace(txtNama.Text) ||
                string.IsNullOrWhiteSpace(txtProdi.Text) ||
                string.IsNullOrWhiteSpace(txtAngkatan.Text) ||
                string.IsNullOrWhiteSpace(txtCabor.Text))
            {
                MessageBox.Show("Harap isi semua data!", "Peringatan");
                return;
            }
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("AddAtlet", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nim", txtNim.Text.Trim());
                        cmd.Parameters.AddWithValue("@nama", txtNama.Text.Trim());
                        cmd.Parameters.AddWithValue("@prodi", txtProdi.Text.Trim());
                        cmd.Parameters.AddWithValue("@angkatan", txtAngkatan.Text.Trim());
                        cmd.Parameters.AddWithValue("@cabor", txtCabor.Text.Trim());
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
            if (dgvAtlet.SelectedRows.Count == 0) return;
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
            if (dgvAtlet.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data yang akan diubah!", "Peringatan");
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
                        cmd.Parameters.AddWithValue("@prodi", txtProdi.Text);
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
                txtProdi.Text = row.Cells[2].Value?.ToString() ?? string.Empty;
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
    }
}
