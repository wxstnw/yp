namespace FilmRentalApp.Forms
{
    partial class FilmEditForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.TextBox txtGenre;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblGenre;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblStatus;

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
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.txtGenre = new System.Windows.Forms.TextBox();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblGenre = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.lblDuration = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(120, 15);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(250, 23);
            // 
            // txtGenre
            // 
            this.txtGenre.Location = new System.Drawing.Point(120, 50);
            this.txtGenre.Name = "txtGenre";
            this.txtGenre.Size = new System.Drawing.Size(250, 23);
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(120, 85);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(250, 23);
            // 
            // txtDuration
            // 
            this.txtDuration.Location = new System.Drawing.Point(120, 120);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(250, 23);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(120, 155);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(250, 60);
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Items.AddRange(new object[] { "Доступен", "Недоступен" });
            this.cmbStatus.Location = new System.Drawing.Point(120, 225);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(250, 23);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(295, 265);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 27);
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Labels
            // 
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Size = new System.Drawing.Size(100, 23);
            this.lblTitle.Text = "Название:";
            this.lblGenre.Location = new System.Drawing.Point(20, 50);
            this.lblGenre.Size = new System.Drawing.Size(100, 23);
            this.lblGenre.Text = "Жанр:";
            this.lblYear.Location = new System.Drawing.Point(20, 85);
            this.lblYear.Size = new System.Drawing.Size(100, 23);
            this.lblYear.Text = "Год:";
            this.lblDuration.Location = new System.Drawing.Point(20, 120);
            this.lblDuration.Size = new System.Drawing.Size(100, 23);
            this.lblDuration.Text = "Длительность:";
            this.lblDescription.Location = new System.Drawing.Point(20, 155);
            this.lblDescription.Size = new System.Drawing.Size(100, 23);
            this.lblDescription.Text = "Описание:";
            this.lblStatus.Location = new System.Drawing.Point(20, 225);
            this.lblStatus.Size = new System.Drawing.Size(100, 23);
            this.lblStatus.Text = "Статус:";
            // 
            // FilmEditForm
            // 
            this.ClientSize = new System.Drawing.Size(400, 310);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.txtGenre);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.txtDuration);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblGenre);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblStatus);
            this.Name = "FilmEditForm";
            this.Text = "Редактирование фильма";
            this.Load += new System.EventHandler(this.FilmEditForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
