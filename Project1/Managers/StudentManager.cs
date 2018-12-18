using Dapper;
using Project1.Migrations;
using Project1.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace Project1.Managers
{
    public class StudentManager : IStudentManager
    {
        public const string TABLE_NAME = "Students";
        public const string EMAIL_COLUMN = nameof(Student.Email);
        public const string GROUP_ID = nameof(Student.Group_Id);
        public const string STUDENT_ID_COLUMN = nameof(Student.Id);
        public const string NAME_COLUMN = nameof(Student.Name);
        public const string SURNAME_COLUMN = nameof(Student.Surname);
        public const string PATRONYMIC_COLUMN = nameof(Student.Patronymic);
        public const string BIRTHDAY_COLUMN = nameof(Student.Birthday);
        public const string PHONE_COLUMN = nameof(Student.Phone);
        public const string ADDRESS_COLUMN = nameof(Student.Address);

        string connectionString;

        public StudentManager()
        {
            connectionString = SQLiteMigration.ConnectionString;
        }

        public List<Student> GetStudents(string id)
        {
            List<Student> students = new List<Student>();
            using (SQLiteConnection connection = new SQLiteConnection(SQLiteMigration.ConnectionString))
            {
                connection.Open();
                students = connection.Query<Student>($"SELECT * FROM {TABLE_NAME} WHERE {GROUP_ID} = '{(id)}'").ToList();
                connection.Close();
            }
            return students;
        }

        public Student GetStudent(string id)
        {
            Student student = null;
            using (SQLiteConnection connection = new SQLiteConnection(SQLiteMigration.ConnectionString))
            {
                connection.Open();
                student = connection.Query<Student>($"SELECT * FROM {TABLE_NAME} WHERE {STUDENT_ID_COLUMN} = '{(id)}'").FirstOrDefault();
                connection.Close();
            }
            return student;
        }
        
        public Student Create(Student user)
        {
            using (SQLiteConnection connection = new SQLiteConnection(SQLiteMigration.ConnectionString))
            {
                connection.Open();
                var sqlQuery = $"INSERT INTO {TABLE_NAME} ({EMAIL_COLUMN}, " +
                    $"{ADDRESS_COLUMN}, " +
                    $"{BIRTHDAY_COLUMN}, " +
                    $"{NAME_COLUMN}, " +
                    $"{SURNAME_COLUMN}, " +
                    $"{PATRONYMIC_COLUMN}, " +
                    $"{PHONE_COLUMN}, " +
                    $"{STUDENT_ID_COLUMN}, " +
                    $"{GROUP_ID}) " +
                    $"VALUES('{(user.Email)}','{user.Address}', '{user.Birthday}', '{user.Name}', '{user.Surname}', '{user.Patronymic}', " +
                    $"'{user.Phone}', '{(user.Id)}', '{user.Group_Id}')";
                connection.Query<Guid>(sqlQuery).FirstOrDefault();
                connection.Close();
            }
            return user;
        }

        public void Update(Student student)
        {
            using (SQLiteConnection connection = new SQLiteConnection(SQLiteMigration.ConnectionString))
            {
                connection.Open();
                connection.Query<Captain>($"UPDATE {TABLE_NAME} SET {EMAIL_COLUMN} = '{(student.Email)}', " +
                    $"{GROUP_ID} = '{(student.Group_Id)}', " +
                    $"{ADDRESS_COLUMN} = '{(student.Address)}', " +
                    $"{BIRTHDAY_COLUMN} = '{(student.Birthday)}', " +
                    $"{NAME_COLUMN} = '{(student.Name)}', " +
                    $"{SURNAME_COLUMN} = '{(student.Surname)}', " +
                    $"{PATRONYMIC_COLUMN} = '{(student.Patronymic)}', " +
                    $"{PHONE_COLUMN} = '{(student.Phone)}' " +
                    $"WHERE {STUDENT_ID_COLUMN} = '{(student.Id)}'").FirstOrDefault();
                connection.Close();
            }
        }

        public void Delete(string id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(SQLiteMigration.ConnectionString))
            {
                connection.Open();
                var sqlQuery = $"DELETE FROM {TABLE_NAME} WHERE Id = '{(id)}'";
                connection.Query(sqlQuery);
                connection.Close();
            }
        }
    }
}