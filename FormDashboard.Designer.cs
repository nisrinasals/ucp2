namespace ucp2
{
    partial class FormDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.chartKeuangan = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cmbJenis = new System.Windows.Forms.ComboBox();
            this.labelJudul = new System.Windows.Forms.Label();
            this.labelFilter = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chartKeuangan)).BeginInit();
            this.SuspendLayout();
            // 
            // chartKeuangan
            // 
            this.chartKeuangan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chartKeuangan.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartKeuangan.Legends.Add(legend1);
            this.chartKeuangan.Location = new System.Drawing.Point(40, 150);
            this.chartKeuangan.Name = "chartKeuangan";
            this.chartKeuangan.Size = new System.Drawing.Size(880, 460);
            this.chartKeuangan.TabIndex = 0;
            this.chartKeuangan.Text = "chartKeuangan";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            title1.Name = "Title1";
            title1.Text = "Grafik Keuangan per Atlet";
            this.chartKeuangan.Titles.Add(title1);
            // 
            // cmbJenis
            // 
            this.cmbJenis.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmbJenis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJenis.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbJenis.FormattingEnabled = true;
            this.cmbJenis.Location = new System.Drawing.Point(420, 100);
            this.cmbJenis.Name = "cmbJenis";
            this.cmbJenis.Size = new System.Drawing.Size(250, 28);
            this.cmbJenis.TabIndex = 1;
            // 
            // labelJudul
            // 
            this.labelJudul.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelJudul.AutoSize = true;
            this.labelJudul.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelJudul.Location = new System.Drawing.Point(300, 30);
            this.labelJudul.Name = "labelJudul";
            this.labelJudul.Size = new System.Drawing.Size(374, 36);
            this.labelJudul.TabIndex = 2;
            this.labelJudul.Text = "Dashboard Keuangan Atlet";
            // 
            // labelFilter
            // 
            this.labelFilter.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelFilter.AutoSize = true;
            this.labelFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFilter.Location = new System.Drawing.Point(240, 103);
            this.labelFilter.Name = "labelFilter";
            this.labelFilter.Size = new System.Drawing.Size(155, 20);
            this.labelFilter.TabIndex = 3;
            this.labelFilter.Text = "Filter Transaksi:";
            // 
            // FormDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Bisque;
            this.ClientSize = new System.Drawing.Size(960, 640);
            this.Controls.Add(this.labelFilter);
            this.Controls.Add(this.labelJudul);
            this.Controls.Add(this.cmbJenis);
            this.Controls.Add(this.chartKeuangan);
            this.Name = "FormDashboard";
            this.Text = "Dashboard Keuangan";
            this.Load += new System.EventHandler(this.FormDashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartKeuangan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartKeuangan;
        private System.Windows.Forms.ComboBox cmbJenis;
        private System.Windows.Forms.Label labelJudul;
        private System.Windows.Forms.Label labelFilter;
    }
}