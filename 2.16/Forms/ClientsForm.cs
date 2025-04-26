using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using FilmRentalApp.Database;
using FilmRentalApp.Forms;

namespace FilmRentalApp
{
    public partial class ClientsForm : Form
    {
        public ClientsForm()
        {
            InitializeComponent();
        }

        private void ClientsForm_Load(object sender, EventArgs e)
        {
            LoadClients();
        }

        private void LoadClients()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Clients";
                using (var adapter = new SQLiteDataAdapter(query, conn))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dgvClients.DataSource = table;
                }
            }

            dgvClients.Columns["Id"].Visible = false;
            dgvClients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new ClientEditForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadClients();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvClients.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvClients.CurrentRow.Cells["Id"].Value);
                using (var form = new ClientEditForm(id))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadClients();
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите клиента для редактирования.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvClients.CurrentRow != null)
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить клиента?", "Удаление", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dgvClients.CurrentRow.Cells["Id"].Value);
                    using (var conn = DatabaseHelper.GetConnection())
                    {
                        conn.Open();
                        string query = "DELETE FROM Clients WHERE Id = @Id";
                        using (var cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", id);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    LoadClients();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите клиента для удаления.");
            }
        }
    }
}
