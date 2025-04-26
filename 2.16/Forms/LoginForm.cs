using System;
using System.Data;
using System.Windows.Forms;
using FilmRentalApp.Database;
using FilmRentalApp.Forms;

namespace FilmRentalApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, введите имя пользователя и пароль.");
                return;
            }

            string query = "SELECT Role FROM Users WHERE Username = @username AND Password = @password";

            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                using (var command = new System.Data.SQLite.SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    var role = command.ExecuteScalar()?.ToString();

                    if (string.IsNullOrEmpty(role))
                    {
                        MessageBox.Show("Неверное имя пользователя или пароль.");
                        return;
                    }

                    MessageBox.Show($"Добро пожаловать, {username}! Роль: {role}");

                    if (role == "admin")
                    {
                        new AdminForm().Show();
                    }
                    else if (role == "seller")
                    {
                        new MainForm(role).Show();
                    }

                    this.Hide();
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
