using System;
using System.Data;
using System.Windows.Forms;
using FilmRentalApp.Database;

namespace FilmRentalApp
{
    public partial class FilmCatalogForm : Form
    {
        public FilmCatalogForm()
        {
            InitializeComponent();
        }

        private void FilmCatalogForm_Load(object sender, EventArgs e)
        {
            LoadFilms();
        }

        private void LoadFilms()
        {
            try
            {
                string query = "SELECT Title, Genre, Year, Description FROM Films";
                DataTable dt = DatabaseHelper.ExecuteQuery(query);
                dgvFilms.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке фильмов: " + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
