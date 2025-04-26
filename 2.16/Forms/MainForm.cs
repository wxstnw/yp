using System;
using System.Windows.Forms;

namespace FilmRentalApp.Forms
{
    public partial class MainForm : Form
    {
        private string userRole;

        public MainForm(string role)
        {
            userRole = role;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = $"Главное меню ({userRole})";
            this.ClientSize = new System.Drawing.Size(250, 250);

            var btnCatalog = new Button { Text = "Каталог", Location = new System.Drawing.Point(20, 20) };
            var btnClients = new Button { Text = "Клиенты", Location = new System.Drawing.Point(20, 60) };
            var btnRent = new Button { Text = "Аренда", Location = new System.Drawing.Point(20, 100) };
            var btnReturn = new Button { Text = "Возврат", Location = new System.Drawing.Point(20, 140) };
            
            var btnExit = new Button { Text = "Выход", Location = new System.Drawing.Point(20, 180) };

            btnCatalog.Click += (s, e) => new FilmsForm(userRole).ShowDialog();
            btnClients.Click += (s, e) => new ClientsForm().ShowDialog();
            btnRent.Click += (s, e) => new RentForm().ShowDialog();
            btnReturn.Click += (s, e) => new ReturnForm().ShowDialog();


            btnExit.Click += (s, e) =>
            {
                this.Close(); // Закрыть MainForm
                var loginForm = new LoginForm();
                loginForm.Show();
            };


            Controls.AddRange(new Control[] {
                btnCatalog, btnClients, btnRent, btnReturn, btnExit
            });
        }
    }
}
