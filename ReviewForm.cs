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
    public partial class ReviewForm : Form
    {
        private string connectionString = "Server=SAS\\SQLEXPRESS;Database=keuangan2;Integrated Security=True";

        public ReviewForm(DataTable data)
        {
            InitializeComponent();
            dgvPreview.DataSource = data;
        }
        private void PreviewForm_Load(object sender, EventArgs e)
        {
            // Optional: Sesuaikan DataGridView jika perlu
            dgvPreview.AutoResizeColumns(); // Menyesuaikan ukuran kolom
        }

        private void button1_Click(object sender, EventArgs e)
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
                // Mengambil data dari DataGridView Preview
                DataTable dt = (DataTable)dgvPreview.DataSource;

                // Melakukan validasi dan import data baris per baris
                foreach (DataRow row in dt.Rows)
                {
                    // Validasi setiap baris sebelum diimpor
                    if (!ValidateRow(row))
                    {
                        // Jika validasi gagal, lanjutkan ke baris berikutnya
                        continue; // Lewati baris ini jika tidak valid
                    }

                    // Query SQL untuk memasukkan data ke tabel Mahasiswa
                    string query = "INSERT INTO Mahasiswa (NIM, Nama, Email, Telepon, Alamat) VALUES (@NIM, @Nama, @Email, @Telepon, @Alamat)";

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            // Menambahkan parameter untuk setiap kolom
                            cmd.Parameters.AddWithValue("@NIM", row["NIM"]);
                            cmd.Parameters.AddWithValue("@Nama", row["Nama"]);
                            cmd.Parameters.AddWithValue("@Email", row["Email"]);
                            cmd.Parameters.AddWithValue("@Telepon", row["Telepon"]);
                            cmd.Parameters.AddWithValue("@Alamat", row["Alamat"]);

                            // Menjalankan perintah SQL
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                // Menampilkan pesan sukses setelah data berhasil diimpor
                MessageBox.Show("Data berhasil dimasukkan ke database.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Menutup form preview setelah data diimpor
                this.Close();
            }
            catch (Exception ex)
            {
                // Menampilkan pesan kesalahan jika terjadi error saat mengimpor data
                MessageBox.Show("Terjadi kesalahan saat mengimpor data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Tambahkan validasi tambahan sesuai kebutuhan (misalnya pola tertentu)



        private void BtnOke_Click(object sender, EventArgs e)
        {

        }
    }
}
