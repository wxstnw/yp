using System;
using System.Data.SQLite;
using System.Windows.Forms;
using FilmRentalApp.Database;

namespace FilmRentalApp.Forms
{
    public partial class FilmEditForm : Form
    {
        private int? filmId;

        public FilmEditForm(int? filmId = null)
        {
            InitializeComponent();
            this.filmId = filmId;
        }

        private void FilmEditForm_Load(object sender, EventArgs e)
        {
            if (filmId.HasValue)
            {
                LoadFilmData();
            }
        }

        private void LoadFilmData()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Films WHERE Id = @Id";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", filmId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtTitle.Text = reader["Title"].ToString();
                            txtGenre.Text = reader["Genre"].ToString();
                            txtYear.Text = reader["Year"].ToString();
                            txtDuration.Text = reader["Duration"].ToString();
                            txtDescription.Text = reader["Description"].ToString();
                            cmbStatus.Text = reader["Status"].ToString();
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query;

                if (filmId.HasValue)
                {
                    query = @"UPDATE Films SET Title = @Title, Genre = @Genre, Year = @Year, 
                              Duration = @Duration, Description = @Description, Status = @Status 
                              WHERE Id = @Id";
                }
                else
                {
                    query = @"INSERT INTO Films (Title, Genre, Year, Duration, Description, Status) 
                              VALUES (@Title, @Genre, @Year, @Duration, @Description, @Status)";
                }

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                    cmd.Parameters.AddWithValue("@Genre", txtGenre.Text);
                    cmd.Parameters.AddWithValue("@Year", txtYear.Text);
                    cmd.Parameters.AddWithValue("@Duration", txtDuration.Text);
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@Status", cmbStatus.Text);

                    if (filmId.HasValue)
                        cmd.Parameters.AddWithValue("@Id", filmId);

                    cmd.ExecuteNonQuery();
                }
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
