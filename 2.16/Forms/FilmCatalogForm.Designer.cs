namespace FilmRentalApp
{
    partial class FilmCatalogForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvFilms;
        private System.Windows.Forms.Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvFilms = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilms)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvFilms
            // 
            this.dgvFilms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFilms.Location = new System.Drawing.Point(12, 12);
            this.dgvFilms.Name = "dgvFilms";
            this.dgvFilms.Size = new System.Drawing.Size(560, 300);
            this.dgvFilms.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(497, 325);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FilmCatalogForm
            // 
            this.ClientSize = new System.Drawing.Size(584, 367);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvFilms);
            this.Name = "FilmCatalogForm";
            this.Text = "Каталог фильмов";
            this.Load += new System.EventHandler(this.FilmCatalogForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilms)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
