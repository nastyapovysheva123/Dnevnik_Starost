using Dapper;

using Project1.Migrations;
using Project1.Models;

using System.Data.SQLite;
using System.Linq;

namespace Project1.Managers
{
    public class GroupsManager : IGroupsManager
    {
        public const string TABLE_NAME = "Groups";
        public const string NAME_COLUMN = nameof(Group.Name);
        public const string ID_COLUMN = nameof(Group.Id);

        public Group GetGroup(string id)
        {
            Group group = null;
            using (SQLiteConnection connection = new SQLiteConnection(SQLiteMigration.ConnectionString))
            {
                connection.Open();
                var sqlQuery = $"SELECT * FROM {TABLE_NAME} WHERE {ID_COLUMN} = '{(id)}'";
                group = connection.Query<Group>(sqlQuery).FirstOrDefault();
                connection.Close();
            }
            return group;
        }

        public Group Create(Group group)
        {
            using (SQLiteConnection connection = new SQLiteConnection(SQLiteMigration.ConnectionString))
            {
                connection.Open();
                var sqlQuery = $"INSERT INTO {TABLE_NAME} ({ID_COLUMN}, {NAME_COLUMN}) VALUES('{(group.Id)}', '{(group.Name)}')";
                connection.Query<string>(sqlQuery).FirstOrDefault();
                connection.Close();
            }
            return group;
        }

        public void Update(Group group)
        {
            using (SQLiteConnection connection = new SQLiteConnection(SQLiteMigration.ConnectionString))
            {
                connection.Open();
                var sqlQuery = $"UPDATE { TABLE_NAME} SET {NAME_COLUMN} = '{(group.Name)}' WHERE {ID_COLUMN} = '{(group.Id)}'";
                connection.Query<string>(sqlQuery);
                connection.Close();
            }
        }
    }
}