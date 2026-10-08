using System.Collections.Generic;
using System.Linq;
using College.Models;
using Dapper;
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

        public List<Student> GetAllStudents2()
        {
            return GetAllStudent();
        }

        public Student GetStudentById2(int id)
        {
            return GetStudentById(id);
        }

        public List<Student> GetAllStudent()
        {
            using (var connection = new SqlConnection(_conn_str))
            {
                string sql = "SELECT StudentId, FirstName, LastName, Age, GroupId FROM Students";
                return connection.Query<Student>(sql).ToList();
            }
        }

        public List<Student> GetStudentsFromGroup(int id)
        {
            using (var connection = new SqlConnection(_conn_str))
            {
                string sql = "SELECT StudentId, FirstName, LastName, Age, GroupId FROM Students WHERE GroupId = @id";
                return connection.Query<Student>(sql, new { id }).ToList();
            }
        }

        public void CreateStudent(Student student)
        {
            using (var connection = new SqlConnection(_conn_str))
            {
                string sql = "INSERT INTO Students (FirstName, LastName, Age, GroupId) " +
                    "OUTPUT INSERTED.StudentId VALUES (@FirstName, @LastName, @Age, @GroupId)";
                student.StudentId = connection.QuerySingle<int>(sql, student);
            }
        }

        public Student GetStudentById(int id)
        {
            using (var connection = new SqlConnection(_conn_str))
            {
                string sql = "SELECT StudentId, FirstName, LastName, Age, GroupId FROM Students WHERE StudentId = @id";
                return connection.QueryFirstOrDefault<Student>(sql, new { id });
            }
        }
    }
}
