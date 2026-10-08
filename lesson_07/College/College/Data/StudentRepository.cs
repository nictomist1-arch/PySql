using System.Collections.Generic;
using College.Models;
using Microsoft.Data.SqlClient;

namespace College.Data
{
    public class StudentRepository
    {
        private readonly string _conn_str;
        public StudentRepository(string connectionString)
        {
            _conn_str = connectionString;
        }

        public List<Student> GetAllStudent()
        {
            var students = new List<Student>();
            using (var connection = new SqlConnection(_conn_str))
            {
                connection.Open();
                string sql = "SELECT StudentId, FirstName, LastName, Age FROM Students";
                using (var command = new SqlCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new Student
                        {
                            StudentId = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Age = reader.GetInt32(3),
                        });
                    }
                }
            }
            return students;
        }

        public List<Student> GetStudentsFromGroup(int id)
        {
            var students = new List<Student>();
            using (var connection = new SqlConnection(_conn_str))
            {
                connection.Open();
                string sql = "SELECT StudentId, FirstName, LastName, Age FROM Students WHERE GroupId = @id";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                StudentId = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Age = reader.GetInt32(3),
                            });
                        }
                    }
                }
            }
            return students;
        }
    }
}
