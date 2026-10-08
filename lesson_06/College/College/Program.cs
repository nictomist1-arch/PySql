using System;
using Microsoft.Data.SqlClient;


namespace College_DB
{
    class Program
    {
        static string conn_str = Connect();
        static bool is_running = true;
        static void Main()
        {
            Console.WriteLine("Добро пожаловать!");
            while (is_running)
            {
                Console.WriteLine("1 — Показать всех студентов");
                Console.WriteLine("2 — Показать все группы");
                Console.WriteLine("3 — Показать студентов группы");
                Console.WriteLine("9 — Выход");
                Console.Write("Выберите команду: ");
                string choice = Console.ReadLine();
                if (choice == null)
                {
                    is_running = false;
                    break;
                }

                choice = choice.Trim();
                switch (choice)
                {
                    case "1":
                        ShowAllStudents(conn_str);
                        break;
                    case "9":
                        is_running = false;
                        break;
                    case "2":
                        ShowAllGroups(conn_str);
                        break;
                    case "3":
                        Console.Write("Введите ID группы: ");
                        if (int.TryParse(Console.ReadLine(), out int groupId) && groupId > 0)
                        {
                            ShowStudentsFromGroup(conn_str, groupId);
                        }
                        else
                        {
                            Console.WriteLine("Введите положительное целое число.");
                        }
                        break;
                    default:
                        Console.WriteLine("Такой команды нет");
                        break;
                        
                }

                if (is_running && !Console.IsInputRedirected)
                {
                    Console.WriteLine("Нажмите любую клавишу, чтобы вернуться в меню...");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }
            Console.WriteLine("Пока!");
        }

        static string Connect()
        {
            string connectionString =
                "Data Source=COMP11A1\\SQLEXPRESS;" +
                "Initial Catalog=College;" +
                "Integrated Security=True;" +
                "TrustServerCertificate=True;";
            return connectionString;
        }
        static void AddGroup(string conn_str, string name)
        {
            SqlConnection connection = new SqlConnection(conn_str);
            connection.Open();
            string sql = "INSERT INTO dbo.Groups(GroupName) VALUES(@name)";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@name", name);
            command.ExecuteNonQuery();
            connection.Close();
        }
        static void AddStudents(string conn_str, string first_name, string last_name, string age, string group_id)
        {
            SqlConnection connection = new SqlConnection(conn_str);
            connection.Open();
            string sql = "INSERT INTO dbo.Students(FirstName, LastName, Age, GroupId)"+
                         "VALUES(@firstName, @lastName, @age, @groupId)";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@FirstName", first_name);
            command.Parameters.AddWithValue("@lastName", last_name);
            command.Parameters.AddWithValue("@age", age);
            command.Parameters.AddWithValue("@groupId", group_id);
            command.ExecuteNonQuery();
            connection.Close();
        }

        static void ShowAllGroups(string conn_str)
        {
            using (SqlConnection connection = new SqlConnection(conn_str))
            {
                connection.Open();
                string sql = "SELECT GroupId, GroupName FROM dbo.Groups ORDER BY GroupId";
                using (SqlCommand command = new SqlCommand(sql, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string group_name = reader.GetString(1);

                        Console.WriteLine($" {reader["GroupId"]} | {reader["GroupName"]}");
                    }
                }
            }
        }

        static void ShowStudentsFromGroup(string conn_str, int id)
        {
            using (SqlConnection connection = new SqlConnection(conn_str))
            {
                connection.Open();
                string sql = "SELECT s.StudentId, s.LastName, s.FirstName " +
                    "FROM dbo.Students AS s " +
                    "WHERE s.GroupId = @Id ORDER BY s.StudentId";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("Студенты с указанным ID группы не найдены.");
                        }

                        while (reader.Read())
                        {
                            Console.WriteLine($" {reader["StudentId"]} | {reader["LastName"]} | {reader["FirstName"]} |");
                        }
                    }
                }
            }
        }

        static void ShowAllStudents(string conn_str)
        {
            SqlConnection connection = new SqlConnection(conn_str);
            connection.Open();
            string sql = "SELECT * FROM dbo.Students";
            SqlCommand command = new SqlCommand(sql, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string first_name = reader.GetString(1);
                string last_name = reader.GetString(2);
                int age = reader.GetInt32(3);
                int group_id = reader.GetInt32(4);

                Console.WriteLine($" {id} | {first_name} | {last_name} | {age} | {group_id} |");
            }
            connection.Close();
        }
    }
}
