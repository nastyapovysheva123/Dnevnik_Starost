using Project1.Managers;

using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Text;

namespace Project1.Migrations
{
    public class SQLiteMigration
    {
        // Наименование базы данных.
        private static string DB_NAME = Path.Combine(Path.GetTempPath(), "Test.db3");

        public static string ConnectionString = "Data Source = " + DB_NAME;

        public void CreateDB()
        {            
            // Если нет файла базы данных.
            if (!File.Exists(DB_NAME))
            {
                // Создание файла.
                SQLiteConnection.CreateFile(DB_NAME);

                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {                  
                        command.CommandText = CreateCaptainTableString();
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }

                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = CreateStudentTableString();
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }

                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        command.CommandText = CreateGroupsTableString();
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
        }

        private string CreateCaptainTableString()
        {
            StringBuilder commandText = new StringBuilder();
            commandText.Append($"CREATE TABLE [{CaptainsManager.TABLE_NAME}]");
            commandText.Append("( ");
            commandText.Append($"[{CaptainsManager.PASS_COLUMN}] nvarchar(100) NOT NULL, ");
            commandText.Append($"[{CaptainsManager.EMAIL_COLUMN}] nvarchar(100) NOT NULL, ");
            commandText.Append($"[{CaptainsManager.NOTE_COLUMN}] nvarchar(100), ");
            commandText.Append($"[{CaptainsManager.GROUP_ID_COLUMN}] nvarchar(100) NOT NULL, ");            
            commandText.Append($"[{CaptainsManager.STUDENT_COLUMN}] nvarchar(100) PRIMARY KEY NOT NULL);");
            return commandText.ToString();
        }

        private string CreateStudentTableString()
        {
            StringBuilder commandText = new StringBuilder();
            commandText.Append($"CREATE TABLE [{StudentManager.TABLE_NAME}]");
            commandText.Append("( ");      
            commandText.Append($"[{StudentManager.ADDRESS_COLUMN}] nvarchar(100), ");
            commandText.Append($"[{StudentManager.BIRTHDAY_COLUMN}] nvarchar(100), ");
            commandText.Append($"[{StudentManager.EMAIL_COLUMN}] nvarchar(100), ");
            commandText.Append($"[{StudentManager.GROUP_ID}] nvarchar(100), ");
            commandText.Append($"[{StudentManager.NAME_COLUMN}] nvarchar(100), ");
            commandText.Append($"[{StudentManager.PATRONYMIC_COLUMN}] nvarchar(100), ");
            commandText.Append($"[{StudentManager.PHONE_COLUMN}] nvarchar(100), ");
            commandText.Append($"[{StudentManager.SURNAME_COLUMN}] nvarchar(100), ");
            commandText.Append($"[{StudentManager.STUDENT_ID_COLUMN}] nvarchar(100) PRIMARY KEY NOT NULL);");
            return commandText.ToString();
        }

        private string CreateGroupsTableString()
        {
            StringBuilder commandText = new StringBuilder();
            commandText.Append($"CREATE TABLE [{GroupsManager.TABLE_NAME}]");
            commandText.Append("( ");
            commandText.Append($"[{GroupsManager.NAME_COLUMN}] nvarchar(100), ");
            commandText.Append($"[{GroupsManager.ID_COLUMN}] nvarchar(100) PRIMARY KEY NOT NULL);");
            return commandText.ToString();
        }

    }
}