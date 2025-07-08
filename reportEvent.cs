using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace ucp2
{
    public partial class reportEvent : Form
    {
        koneksi kon = new koneksi();
        public reportEvent()
        {
            InitializeComponent();
        }

        private void reportEvent_Load(object sender, EventArgs e)
        {
            // Mengatur form menjadi fullscreen
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            SetupReportViewer();
            this.rvEvent.RefreshReport();
        }

        private void SetupReportViewer()
        {
            string connectionString = kon.connectionString();
            string query =@"
            SELECT
                            E.id_event,
                            E.nama_event,
                            E.jenis_event,
                            E.keterangan,
                            E.tanggal,
                            PA.nim,
                            PA.peran_partisipasi,
                            PA.id_partisipasi
                        FROM
                            Event E
                        INNER JOIN
                            Partisipasi_Atlet PA ON E.id_event = PA.id_event";

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }

            ReportDataSource rds = new ReportDataSource("DataSet1", dt);

            rvEvent.LocalReport.DataSources.Clear();
            rvEvent.LocalReport.DataSources.Add(rds);

            // Mengubah path report menjadi relatif
            string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ReportEvent.rdlc");
            rvEvent.LocalReport.ReportPath = reportPath;
            rvEvent.RefreshReport();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
