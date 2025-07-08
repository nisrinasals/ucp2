using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ucp2
{
    public partial class ReviewForm: Form
    {
        private string connectionString = "Server=SAS\\SQLEXPRESS;Database=keuangan2;Integrated Security=True";

        public ReviewForm(DataTable data)

        {
            InitializeComponent();
            dgvPreview.DataSource = data;
        }

        private bool ValidateRow(DataRow row)
        {
            string nim = row["NIM"].ToString();

            if (nim.Length != 11)
            {
                MessageBox.Show(
                    "NIM harus terdiri dari 11 karakter.",
                    "Kesalahan Validasi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return false;
            }
            return true;
        }

        private void ImportDataToDatabase()
        {
            try
            {
                DataTable dt = (DataTable)dgvPreview.DataSource;

                foreach (DataRow row in dt.Rows)
                {
                    if (!ValidateRow(row))
                    {
                        continue;
                    }
                    string query = "INSERT INTO Atlet (nim, nama, prodi, angkatan, cabor) VALUES (@nim, @nama, @prodi, @angkatan, @cabor)";

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@nim", row["nim"]);
                            cmd.Parameters.AddWithValue("@nama", row["nama"]);
                            cmd.Parameters.AddWithValue("@prodi", row["prodi"]);
                            cmd.Parameters.AddWithValue("@angkatan", row["angkatan"]);
                            cmd.Parameters.AddWithValue("@cabor", row["cabor"]);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                MessageBox.Show("Data berhasil dimasukkan ke database.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat mengimpor data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPreview_Click(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            {
                var result = MessageBox.Show(
                    "Apakah Anda ingin mengimpor data ini ke database?",
                    "Konfirmasi",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    ImportDataToDatabase();
                }
            }

        }

        private void ReviewForm_Load(object sender, EventArgs e)
        {
            // Mengatur form menjadi fullscreen
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;

            dgvPreview.AutoResizeColumns();
        }
    }
}
