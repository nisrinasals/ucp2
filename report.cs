using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace ucp2
{
    public partial class report : Form
    {
        koneksi kon = new koneksi();
        public report()
        {
            InitializeComponent();
        }

        private void report_Load(object sender, EventArgs e)
        {
            // Mengatur form menjadi fullscreen
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            SetupReportViewer();
            this.reportViewer1.RefreshReport();
        }

        private void SetupReportViewer()
        {
            string connectionString = kon.connectionString();
            string query = @"
                SELECT
                    K.id_keuangan,
                    K.nim,
                    A.nama,
                    K.jenis_transaksi,
                    K.keterangan,
                    K.jumlah,
                    K.tanggal
                FROM 
                    DataKeuangan K
                INNER JOIN 
                    Atlet A ON K.nim = A.nim";

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }

            ReportDataSource rds = new ReportDataSource("DataSet1", dt);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            // Mengubah path report menjadi relatif
            string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ReportKeuangan.rdlc");
            reportViewer1.LocalReport.ReportPath = reportPath;
            reportViewer1.RefreshReport();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
