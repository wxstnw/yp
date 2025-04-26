using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace FilmRentalApp.Database
{
    public static class DatabaseHelper
    {
        // Путь к базе данных в папке "Database" внутри проекта
        private static readonly string DbPath = Path.Combine(Application.StartupPath, "Database", "database.db");
        private static readonly string ConnectionString = $"Data Source={DbPath};Version=3;";

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(ConnectionString);
        }

        public static DataTable ExecuteQuery(string query)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }
}
