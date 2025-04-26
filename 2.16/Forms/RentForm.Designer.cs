namespace FilmRentalApp.Forms
{
    partial class RentForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cbClients;
        private System.Windows.Forms.ComboBox cbFilms;
        private System.Windows.Forms.DateTimePicker dtStart;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.Button btnRent;
        private System.Windows.Forms.DataGridView dgvRentals;
        private System.Windows.Forms.Label lblClient;
        private System.Windows.Forms.Label lblFilm;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblEndDate;

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
            this.cbClients = new System.Windows.Forms.ComboBox();
            this.cbFilms = new System.Windows.Forms.ComboBox();
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.btnRent = new System.Windows.Forms.Button();
            this.dgvRentals = new System.Windows.Forms.DataGridView();
            this.lblClient = new System.Windows.Forms.Label();
            this.lblFilm = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRentals)).BeginInit();
            this.SuspendLayout();

            // 
            // lblClient
            // 
            this.lblClient.AutoSize = true;
            this.lblClient.Location = new System.Drawing.Point(20, 20);
            this.lblClient.Name = "lblClient";
            this.lblClient.Size = new System.Drawing.Size(49, 15);
            this.lblClient.Text = "Клиент:";

            // 
            // cbClients
            // 
            this.cbClients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbClients.FormattingEnabled = true;
            this.cbClients.Location = new System.Drawing.Point(100, 17);
            this.cbClients.Name = "cbClients";
            this.cbClients.Size = new System.Drawing.Size(250, 23);

            // 
            // lblFilm
            // 
            this.lblFilm.AutoSize = true;
            this.lblFilm.Location = new System.Drawing.Point(20, 55);
            this.lblFilm.Name = "lblFilm";
            this.lblFilm.Size = new System.Drawing.Size(47, 15);
            this.lblFilm.Text = "Фильм:";

            // 
            // cbFilms
            // 
            this.cbFilms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilms.FormattingEnabled = true;
            this.cbFilms.Location = new System.Drawing.Point(100, 52);
            this.cbFilms.Name = "cbFilms";
            this.cbFilms.Size = new System.Drawing.Size(250, 23);

            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(20, 90);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(77, 15);
            this.lblStartDate.Text = "Дата начала:";

            // 
            // dtStart
            // 
            this.dtStart.Location = new System.Drawing.Point(100, 87);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(250, 23);

            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(20, 125);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(102, 15);
            this.lblEndDate.Text = "Дата окончания:";

            // 
            // dtEnd
            // 
            this.dtEnd.Location = new System.Drawing.Point(130, 122);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(220, 23);

            // 
            // btnRent
            // 
            this.btnRent.Location = new System.Drawing.Point(100, 160);
            this.btnRent.Name = "btnRent";
            this.btnRent.Size = new System.Drawing.Size(150, 30);
            this.btnRent.Text = "Оформить аренду";
            this.btnRent.UseVisualStyleBackColor = true;
            this.btnRent.Click += new System.EventHandler(this.btnRent_Click);

            // 
            // dgvRentals
            // 
            this.dgvRentals.AllowUserToAddRows = false;
            this.dgvRentals.AllowUserToDeleteRows = false;
            this.dgvRentals.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRentals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRentals.Location = new System.Drawing.Point(20, 210);
            this.dgvRentals.Name = "dgvRentals";
            this.dgvRentals.ReadOnly = true;
            this.dgvRentals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRentals.Size = new System.Drawing.Size(600, 250);

            // 
            // RentForm
            // 
            this.ClientSize = new System.Drawing.Size(650, 480);
            this.Controls.Add(this.lblClient);
            this.Controls.Add(this.cbClients);
            this.Controls.Add(this.lblFilm);
            this.Controls.Add(this.cbFilms);
            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.dtStart);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.dtEnd);
            this.Controls.Add(this.btnRent);
            this.Controls.Add(this.dgvRentals);
            this.Name = "RentForm";
            this.Text = "Оформление аренды";
            this.Load += new System.EventHandler(this.RentForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRentals)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
