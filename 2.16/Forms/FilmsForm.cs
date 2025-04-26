using System;
using System.Data;
using System.Windows.Forms;
using FilmRentalApp.Database;

namespace FilmRentalApp.Forms
{
    public partial class FilmsForm : Form
    {
        private readonly string userRole;

        public FilmsForm(string role)
        {
            userRole = role;
            InitializeComponent();
        }

        private void FilmsForm_Load(object sender, EventArgs e)
        {
            LoadFilms();

            // Скрываем кнопки для админа
            if (userRole != "seller")
            {
                btnAdd.Visible = false;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
            }
        }

        private void LoadFilms()
        {
            string query = "SELECT Id, Title, Genre, Year, Status FROM Films";
            dgvFilms.DataSource = DatabaseHelper.ExecuteQuery(query);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            new FilmEditForm().ShowDialog();
            LoadFilms();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvFilms.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите фильм для редактирования.");
                return;
            }

            int filmId = Convert.ToInt32(dgvFilms.SelectedRows[0].Cells["Id"].Value);
            new FilmEditForm(filmId).ShowDialog();
            LoadFilms();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvFilms.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите фильм для удаления.");
                return;
            }

            int filmId = Convert.ToInt32(dgvFilms.SelectedRows[0].Cells["Id"].Value);
            string query = $"DELETE FROM Films WHERE Id = {filmId}";
            DatabaseHelper.ExecuteQuery(query);
            LoadFilms();
        }
    }
}
