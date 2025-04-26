namespace FilmRentalApp.Forms
{
    partial class ClientEditForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblEmail;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(12, 15);
            this.lblName.Size = new System.Drawing.Size(80, 20);
            this.lblName.Text = "ФИО:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(100, 12);
            this.txtName.Size = new System.Drawing.Size(200, 20);
            // 
            // lblPhone
            // 
            this.lblPhone.Location = new System.Drawing.Point(12, 45);
            this.lblPhone.Size = new System.Drawing.Size(80, 20);
            this.lblPhone.Text = "Телефон:";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(100, 42);
            this.txtPhone.Size = new System.Drawing.Size(200, 20);
            // 
            // lblEmail
            // 
            this.lblEmail.Location = new System.Drawing.Point(12, 75);
            this.lblEmail.Size = new System.Drawing.Size(80, 20);
            this.lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(100, 72);
            this.txtEmail.Size = new System.Drawing.Size(200, 20);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(100, 110);
            this.btnSave.Size = new System.Drawing.Size(200, 30);
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ClientEditForm
            // 
            this.ClientSize = new System.Drawing.Size(320, 160);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.btnSave);
            this.Text = "Редактирование клиента";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
