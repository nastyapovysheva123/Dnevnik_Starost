using Dapper;
using Project1.Migrations;
using Project1.Models;
using System;
using System.Data.SQLite;
using System.Linq;

namespace Project1.Managers
{
    public class CaptainsManager : ICaptainsManager
    {
        public const string TABLE_NAME = "Captains";
        public const string STUDENT_COLUMN = nameof(Captain.StudentId);
        public const string PASS_COLUMN = nameof(Captain.PassHash);
        public const string NOTE_COLUMN = nameof(Captain.Note);
        public const string EMAIL_COLUMN = nameof(Captain.Email);
        public const string GROUP_ID_COLUMN = nameof(Captain.GroupId);

        string connectionString;

        public static string Current_Id { get; set; }

        public static string Current_Group_Id { get; set; }


        public CaptainsManager()
        {
            connectionString = SQLiteMigration.ConnectionString;
        }

        public Captain GetCaptain(string id)
        {
            Captain captain = null;
            using (SQLiteConnection connection = new SQLiteConnection(SQLiteMigration.ConnectionString))
            {
                connection.Open();
                captain = connection.Query<Captain>($"SELECT * FROM {TABLE_NAME} WHERE {STUDENT_COLUMN} = '{id}'").FirstOrDefault();
                connection.Close();
            }
            return captain;
        }

        public Captain Create(Captain captain)
        {
            using (SQLiteConnection connection = new SQLiteConnection(SQLiteMigration.ConnectionString))
            {
                connection.Open();
                var sqlQuery = $"INSERT INTO {TABLE_NAME} ({PASS_COLUMN}, {STUDENT_COLUMN}, {NOTE_COLUMN}, {EMAIL_COLUMN}, {GROUP_ID_COLUMN}) " +
                    $"VALUES('{(captain.PassHash)}', '{(captain.StudentId)}', '{(captain.Note)}', '{(captain.Email)}', '{(captain.GroupId)}')";
                connection.Query<string>(sqlQuery).FirstOrDefault();
                connection.Close();
            }
            Current_Id = captain?.StudentId;
            Current_Group_Id = captain?.GroupId;
            return captain;
        }

        public Captain GetCurrent(string email, string password)
        {
            Captain captain = null;
            using (SQLiteConnection connection = new SQLiteConnection(SQLiteMigration.ConnectionString))
            {
                connection.Open();
                captain = connection.Query<Captain>($"SELECT * FROM {TABLE_NAME} WHERE {EMAIL_COLUMN} = '{(email)}' and {PASS_COLUMN} = '{(password)}'").FirstOrDefault();
                connection.Close();
            }

            Current_Id = captain?.StudentId;
            Current_Group_Id = captain?.GroupId;
            return captain;
        }

        public void Update(Captain captain)
        {
            using (SQLiteConnection connection = new SQLiteConnection(SQLiteMigration.ConnectionString))
            {
                connection.Open();
                connection.Query<Captain>($"UPDATE {TABLE_NAME} SET {EMAIL_COLUMN} = '{(captain.Email)}', " +
                    $"{PASS_COLUMN} = '{(captain.PassHash)}', " +
                    $"{GROUP_ID_COLUMN} = '{(captain.GroupId)}', " +
                    $"{NOTE_COLUMN} = '{(captain.Note)}' " +
                    $"WHERE {STUDENT_COLUMN} = '{(captain.StudentId)}'").FirstOrDefault();
                connection.Close();
            }
        }

        public Captain GetCurrent()
        {
            return GetCaptain(Current_Id);
        }

        public void Unload()
        {
            Current_Id = null;
            Current_Group_Id = null;
        }
    }
}