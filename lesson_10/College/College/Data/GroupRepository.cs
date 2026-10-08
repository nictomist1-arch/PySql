using System.Collections.Generic;
using System.Linq;
using College.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace College.Data
{
    public class GroupRepository
    {
        private readonly string _conn_str;
        public GroupRepository(string connectionString)
        {
            _conn_str = connectionString;
        }

        public List<Group> GetAllGroups()
        {
            using (var connection = new SqlConnection(_conn_str))
            {
                string sql = "SELECT GroupId, GroupName FROM Groups";
                return connection.Query<Group>(sql).ToList();
            }
        }

        public Group GetGroupById(int id)
        {
            using (var connection = new SqlConnection(_conn_str))
            {
                string sql = "SELECT GroupId, GroupName FROM Groups WHERE GroupId = @id";
                return connection.QueryFirstOrDefault<Group>(sql, new { id });
            }
        }
    }
}
