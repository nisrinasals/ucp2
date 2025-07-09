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
using System.Windows.Forms.DataVisualization.Charting;

namespace ucp2
{
    public partial class FormDashboard: Form
    {
        koneksi kon = new koneksi();
        private readonly string connectionString;

        public FormDashboard()
        {
            InitializeComponent();
            connectionString = kon.connectionString();
        }

        private void FormDashboard_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            cmbJenis.Items.AddRange(new string[] { "Semua", "Pemasukan", "Pengeluaran" });
            cmbJenis.SelectedIndex = 0;

            LoadChartData("Semua");

            cmbJenis.SelectedIndexChanged += cmbJenis_SelectedIndexChanged;
        }

        private void cmbJenis_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFilter = cmbJenis.SelectedItem.ToString();
            LoadChartData(selectedFilter);
        }

        private void LoadChartData(string filter)
        {
            chartKeuangan.Series.Clear();

            string query = @"
                SELECT
                    a.nama,
                    SUM(CASE WHEN dk.jenis_transaksi = 'Pemasukan' THEN dk.jumlah ELSE 0 END) AS Pemasukan,
                    SUM(CASE WHEN dk.jenis_transaksi = 'Pengeluaran' THEN dk.jumlah ELSE 0 END) AS Pengeluaran
                FROM
                    Atlet a
                LEFT JOIN
                    DataKeuangan dk ON a.nim = dk.nim
                GROUP BY
                    a.nama
                ORDER BY
	                a.nama ASC";

            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data chart: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (filter == "Semua" || filter == "Pemasukan")
            {
                Series seriesPemasukan = new Series("Pemasukan")
                {
                    ChartType = SeriesChartType.Column,
                    Color = System.Drawing.Color.ForestGreen,
                    IsValueShownAsLabel = true
                };
                foreach (DataRow row in dt.Rows)
                {
                    seriesPemasukan.Points.AddXY(row["nama"].ToString(), row["Pemasukan"]);
                }
                chartKeuangan.Series.Add(seriesPemasukan);
            }

            if (filter == "Semua" || filter == "Pengeluaran")
            {
                Series seriesPengeluaran = new Series("Pengeluaran")
                {
                    ChartType = SeriesChartType.Column,
                    Color = System.Drawing.Color.Firebrick,
                    IsValueShownAsLabel = true
                };
                foreach (DataRow row in dt.Rows)
                {
                    seriesPengeluaran.Points.AddXY(row["nama"].ToString(), row["Pengeluaran"]);
                }
                chartKeuangan.Series.Add(seriesPengeluaran);
            }

            chartKeuangan.ChartAreas[0].AxisX.Title = "Nama Atlet";
            chartKeuangan.ChartAreas[0].AxisY.Title = "Jumlah (Rp)";
            chartKeuangan.ChartAreas[0].AxisX.Interval = 1;
            chartKeuangan.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
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
