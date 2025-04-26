using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using FilmRentalApp.Database;

namespace FilmRentalApp.Forms
{
    public partial class ReturnForm : Form
    {
        public ReturnForm()
        {
            InitializeComponent();
        }

        private void ReturnForm_Load(object sender, EventArgs e)
        {
            LoadActiveRentals();
        }

        private void LoadActiveRentals()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT 
                        Rentals.Id,
                        Clients.FullName AS Клиент,
                        Films.Title AS Фильм,
                        Rentals.StartDate AS 'Дата начала',
                        Rentals.EndDate AS 'Дата окончания'
                    FROM Rentals
                    JOIN Clients ON Rentals.ClientId = Clients.Id
                    JOIN Films ON Rentals.FilmId = Films.Id
                    WHERE Rentals.Status = 'Активна'";

                var adapter = new SQLiteDataAdapter(query, conn);
                var dt = new DataTable();
                adapter.Fill(dt);
                dgvRentals.DataSource = dt;
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (dgvRentals.SelectedRows.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите запись для возврата.");
                return;
            }

            int rentalId = Convert.ToInt32(dgvRentals.SelectedRows[0].Cells["Id"].Value);

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Обновить статус аренды
                        var cmd = new SQLiteCommand("UPDATE Rentals SET Status = 'Возвращена' WHERE Id = @id", conn);
                        cmd.Parameters.AddWithValue("@id", rentalId);
                        cmd.ExecuteNonQuery();

                        // Освободить фильм
                        var cmd2 = new SQLiteCommand(@"
                            UPDATE Films 
                            SET Status = 'Доступен' 
                            WHERE Id = (SELECT FilmId FROM Rentals WHERE Id = @id)", conn);
                        cmd2.Parameters.AddWithValue("@id", rentalId);
                        cmd2.ExecuteNonQuery();

                        transaction.Commit();

                        MessageBox.Show("Фильм успешно возвращён.");
                        LoadActiveRentals();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Ошибка при возврате: " + ex.Message);
                    }
                }
            }
        }
    }
}
