using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FilmRentalApp.Database;

namespace FilmRentalApp.Forms
{
    public partial class RentForm : Form
    {
        public RentForm()
        {
            InitializeComponent();
        }

        private void RentForm_Load(object sender, EventArgs e)
        {
            LoadClients();
            LoadAvailableFilms();
            LoadRentals();
        }

        private void LoadClients()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT Id, FullName FROM Clients", conn);
                var reader = cmd.ExecuteReader();
                var dt = new DataTable();
                dt.Load(reader);

                cbClients.DataSource = dt;
                cbClients.DisplayMember = "FullName";
                cbClients.ValueMember = "Id";
            }
        }

        private void LoadAvailableFilms()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT Id, Title FROM Films WHERE Status = 'Доступен'", conn);
                var reader = cmd.ExecuteReader();
                var dt = new DataTable();
                dt.Load(reader);

                cbFilms.DataSource = dt;
                cbFilms.DisplayMember = "Title";
                cbFilms.ValueMember = "Id";
            }
        }

        private void LoadRentals()
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
                Rentals.EndDate AS 'Дата окончания',
                Rentals.Status
            FROM Rentals
            JOIN Clients ON Rentals.ClientId = Clients.Id
            JOIN Films ON Rentals.FilmId = Films.Id
            WHERE Rentals.Status = 'Активна'
            ORDER BY Rentals.StartDate DESC";

                var adapter = new SQLiteDataAdapter(query, conn);
                var dt = new DataTable();
                adapter.Fill(dt);
                dgvRentals.DataSource = dt;
            }
        }


        private void btnRent_Click(object sender, EventArgs e)
        {
            if (cbClients.SelectedValue == null || cbFilms.SelectedValue == null)
            {
                MessageBox.Show("Пожалуйста, выберите клиента и фильм.");
                return;
            }

            int clientId = Convert.ToInt32(cbClients.SelectedValue);
            int filmId = Convert.ToInt32(cbFilms.SelectedValue);
            DateTime startDate = dtStart.Value.Date;
            DateTime endDate = dtEnd.Value.Date;

            if (endDate < startDate)
            {
                MessageBox.Show("Дата окончания аренды не может быть раньше даты начала.");
                return;
            }

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Добавление аренды
                        var insertRental = new SQLiteCommand(@"
                            INSERT INTO Rentals (ClientId, FilmId, StartDate, EndDate, Status)
                            VALUES (@clientId, @filmId, @startDate, @endDate, 'Активна')", conn);

                        insertRental.Parameters.AddWithValue("@clientId", clientId);
                        insertRental.Parameters.AddWithValue("@filmId", filmId);
                        insertRental.Parameters.AddWithValue("@startDate", startDate);
                        insertRental.Parameters.AddWithValue("@endDate", endDate);
                        insertRental.ExecuteNonQuery();

                        // Обновление статуса фильма
                        var updateFilmStatus = new SQLiteCommand(@"
                            UPDATE Films SET Status = 'Недоступен' WHERE Id = @filmId", conn);

                        updateFilmStatus.Parameters.AddWithValue("@filmId", filmId);
                        updateFilmStatus.ExecuteNonQuery();

                        transaction.Commit();

                        MessageBox.Show("Аренда успешно оформлена!");

                        LoadAvailableFilms();
                        LoadRentals();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Ошибка при оформлении аренды: " + ex.Message);
                    }
                }
            }
        }
    }
}
