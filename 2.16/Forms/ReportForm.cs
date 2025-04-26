using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using FilmRentalApp.Database;

namespace FilmRentalApp.Forms
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            LoadReportData();
        }

        private void LoadReportData()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT 
                        Rentals.Id AS 'ID',
                        Clients.FullName AS 'Клиент',
                        Films.Title AS 'Фильм',
                        Rentals.StartDate AS 'Дата начала',
                        Rentals.EndDate AS 'Дата окончания',
                        Rentals.Status AS 'Статус аренды'
                    FROM Rentals
                    JOIN Clients ON Rentals.ClientId = Clients.Id
                    JOIN Films ON Rentals.FilmId = Films.Id
                    ORDER BY Rentals.StartDate DESC
                ";

                using (var adapter = new SQLiteDataAdapter(query, conn))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);
                    dgvReport.DataSource = dt;

                    dgvReport.Columns["ID"].Visible = false; // скрываем внутренний ID
                }
            }
        }
    }
}
