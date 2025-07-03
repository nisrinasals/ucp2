namespace ucp2
{
    partial class FormKeuangan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormKeuangan));
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnTambah = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvKeuangan = new System.Windows.Forms.DataGridView();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.txtKeterangan = new System.Windows.Forms.TextBox();
            this.txtNim = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.PictureBox();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.cB_JenisTransaksi = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKeuangan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(155, 319);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 31;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(52, 319);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(75, 23);
            this.btnTambah.TabIndex = 28;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Geoform", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(137, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(742, 38);
            this.label6.TabIndex = 27;
            this.label6.Text = "DATA KEUANGAN ATLET FAKULTAS TEKNIK UMY";
            // 
            // dgvKeuangan
            // 
            this.dgvKeuangan.BackgroundColor = System.Drawing.Color.Tan;
            this.dgvKeuangan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKeuangan.Location = new System.Drawing.Point(305, 90);
            this.dgvKeuangan.Name = "dgvKeuangan";
            this.dgvKeuangan.RowHeadersWidth = 51;
            this.dgvKeuangan.RowTemplate.Height = 24;
            this.dgvKeuangan.Size = new System.Drawing.Size(585, 375);
            this.dgvKeuangan.TabIndex = 26;
            this.dgvKeuangan.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKeuangan_CellContentClick);
            // 
            // txtJumlah
            // 
            this.txtJumlah.Location = new System.Drawing.Point(38, 267);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(248, 22);
            this.txtJumlah.TabIndex = 23;
            // 
            // txtKeterangan
            // 
            this.txtKeterangan.Location = new System.Drawing.Point(38, 166);
            this.txtKeterangan.Name = "txtKeterangan";
            this.txtKeterangan.Size = new System.Drawing.Size(248, 22);
            this.txtKeterangan.TabIndex = 21;
            // 
            // txtNim
            // 
            this.txtNim.Location = new System.Drawing.Point(38, 116);
            this.txtNim.Name = "txtNim";
            this.txtNim.Size = new System.Drawing.Size(248, 22);
            this.txtNim.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 248);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 16);
            this.label4.TabIndex = 19;
            this.label4.Text = "Jumlah";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 199);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 16);
            this.label3.TabIndex = 18;
            this.label3.Text = "Jenis Transaksi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 17;
            this.label2.Text = "Keterangan";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 16);
            this.label1.TabIndex = 16;
            this.label1.Text = "Nim";
            // 
            // btnBack
            // 
            this.btnBack.Image = ((System.Drawing.Image)(resources.GetObject("btnBack.Image")));
            this.btnBack.Location = new System.Drawing.Point(0, -2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(48, 40);
            this.btnBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnBack.TabIndex = 33;
            this.btnBack.TabStop = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(155, 366);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(75, 23);
            this.btnReport.TabIndex = 34;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(52, 367);
            this.btnImport.Margin = new System.Windows.Forms.Padding(2);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 22);
            this.btnImport.TabIndex = 35;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // cB_JenisTransaksi
            // 
            this.cB_JenisTransaksi.FormattingEnabled = true;
            this.cB_JenisTransaksi.Items.AddRange(new object[] {
            "Pemasukan",
            "Pengeluaran"});
            this.cB_JenisTransaksi.Location = new System.Drawing.Point(38, 221);
            this.cB_JenisTransaksi.Name = "cB_JenisTransaksi";
            this.cB_JenisTransaksi.Size = new System.Drawing.Size(248, 24);
            this.cB_JenisTransaksi.TabIndex = 36;
            // 
            // FormKeuangan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Bisque;
            this.ClientSize = new System.Drawing.Size(902, 477);
            this.Controls.Add(this.cB_JenisTransaksi);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dgvKeuangan);
            this.Controls.Add(this.txtJumlah);
            this.Controls.Add(this.txtKeterangan);
            this.Controls.Add(this.txtNim);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormKeuangan";
            this.Text = "Data Keuangan";
            this.Load += new System.EventHandler(this.FormKeuangan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKeuangan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvKeuangan;
        private System.Windows.Forms.TextBox txtJumlah;
        private System.Windows.Forms.TextBox txtKeterangan;
        private System.Windows.Forms.TextBox txtNim;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox btnBack;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.ComboBox cB_JenisTransaksi;
    }
}