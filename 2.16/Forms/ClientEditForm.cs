using System;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Xml.Linq;
using FilmRentalApp.Database;

namespace FilmRentalApp.Forms
{
    public partial class ClientEditForm : Form
    {
        private int clientId = -1;

        public ClientEditForm(int id = -1)
        {
            InitializeComponent();
            clientId = id;

            if (clientId != -1)
            {
                LoadClient();
            }
        }

        private void LoadClient()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT * FROM Clients WHERE Id = @id", conn);
                cmd.Parameters.AddWithValue("@id", clientId);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtName.Text = reader["FullName"].ToString();
                    txtPhone.Text = reader["Phone"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                SQLiteCommand cmd;
                if (clientId == -1)
                {
                    cmd = new SQLiteCommand("INSERT INTO Clients (FullName, Phone, Email) VALUES (@n, @p, @e)", conn);
                }
                else
                {
                    cmd = new SQLiteCommand("UPDATE Clients SET FullName = @n, Phone = @p, Email = @e WHERE Id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", clientId);
                }

                cmd.Parameters.AddWithValue("@n", txtName.Text);
                cmd.Parameters.AddWithValue("@p", txtPhone.Text);
                cmd.Parameters.AddWithValue("@e", txtEmail.Text);

                cmd.ExecuteNonQuery();
                this.Close();
            }
        }
    }
}
