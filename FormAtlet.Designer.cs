namespace ucp2
{
    partial class FormAtlet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAtlet));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNim = new System.Windows.Forms.TextBox();
            this.txtNama = new System.Windows.Forms.TextBox();
            this.txtAngkatan = new System.Windows.Forms.TextBox();
            this.txtCabor = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvAtlet = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.PictureBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.cB_Prodi = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAtlet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 72);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nim";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 117);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nama";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 161);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Prodi";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 204);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Angkatan";
            // 
            // txtNim
            // 
            this.txtNim.Location = new System.Drawing.Point(30, 87);
            this.txtNim.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtNim.Name = "txtNim";
            this.txtNim.Size = new System.Drawing.Size(162, 20);
            this.txtNim.TabIndex = 4;
            // 
            // txtNama
            // 
            this.txtNama.Location = new System.Drawing.Point(30, 132);
            this.txtNama.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtNama.Name = "txtNama";
            this.txtNama.Size = new System.Drawing.Size(162, 20);
            this.txtNama.TabIndex = 5;
            // 
            // txtAngkatan
            // 
            this.txtAngkatan.Location = new System.Drawing.Point(30, 219);
            this.txtAngkatan.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtAngkatan.Name = "txtAngkatan";
            this.txtAngkatan.Size = new System.Drawing.Size(162, 20);
            this.txtAngkatan.TabIndex = 7;
            // 
            // txtCabor
            // 
            this.txtCabor.Location = new System.Drawing.Point(30, 262);
            this.txtCabor.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtCabor.Name = "txtCabor";
            this.txtCabor.Size = new System.Drawing.Size(162, 20);
            this.txtCabor.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 247);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Cabang Olahraga";
            // 
            // dgvAtlet
            // 
            this.dgvAtlet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAtlet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAtlet.BackgroundColor = System.Drawing.Color.Tan;
            this.dgvAtlet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAtlet.Location = new System.Drawing.Point(206, 56);
            this.dgvAtlet.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvAtlet.Name = "dgvAtlet";
            this.dgvAtlet.RowHeadersWidth = 51;
            this.dgvAtlet.RowTemplate.Height = 24;
            this.dgvAtlet.Size = new System.Drawing.Size(697, 417);
            this.dgvAtlet.TabIndex = 10;
            this.dgvAtlet.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAtlet_CellContentClick);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(168, 19);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(730, 29);
            this.label6.TabIndex = 11;
            this.label6.Text = "DATA ATLET FAKULTAS TEKNIK UMY";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(30, 327);
            this.btnTambah.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(68, 35);
            this.btnTambah.TabIndex = 12;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(105, 327);
            this.btnHapus.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(66, 35);
            this.btnHapus.TabIndex = 13;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(30, 375);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(68, 32);
            this.btnUpdate.TabIndex = 14;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(105, 375);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(64, 32);
            this.btnRefresh.TabIndex = 15;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnBack
            // 
            this.btnBack.Image = ((System.Drawing.Image)(resources.GetObject("btnBack.Image")));
            this.btnBack.Location = new System.Drawing.Point(32, 25);
            this.btnBack.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(36, 32);
            this.btnBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnBack.TabIndex = 16;
            this.btnBack.TabStop = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(30, 429);
            this.btnImport.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(68, 32);
            this.btnImport.TabIndex = 17;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // cB_Prodi
            // 
            this.cB_Prodi.FormattingEnabled = true;
            this.cB_Prodi.Items.AddRange(new object[] {
            "Teknik Sipil",
            "Teknik Mesin",
            "Teknik Elektro",
            "Teknologi Informasi"});
            this.cB_Prodi.Location = new System.Drawing.Point(30, 176);
            this.cB_Prodi.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cB_Prodi.Name = "cB_Prodi";
            this.cB_Prodi.Size = new System.Drawing.Size(162, 21);
            this.cB_Prodi.TabIndex = 18;
            // 
            // FormAtlet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Bisque;
            this.ClientSize = new System.Drawing.Size(923, 494);
            this.Controls.Add(this.cB_Prodi);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dgvAtlet);
            this.Controls.Add(this.txtCabor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtAngkatan);
            this.Controls.Add(this.txtNama);
            this.Controls.Add(this.txtNim);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormAtlet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormAtlet";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormAtlet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAtlet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNim;
        private System.Windows.Forms.TextBox txtNama;
        private System.Windows.Forms.TextBox txtAngkatan;
        private System.Windows.Forms.TextBox txtCabor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvAtlet;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.PictureBox btnBack;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.ComboBox cB_Prodi;
    }
}