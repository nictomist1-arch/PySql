using System;
using College.Data;
using College.Models;


namespace College_DB
{
    class Program
    {
        static readonly string conn_str = "Data Source=COMP11A1\\SQLEXPRESS;" +"Initial Catalog=CollegeDB;" +"Integrated Security=True;" +"TrustServerCertificate=True;";
        static bool is_running = true;

        static StudentRepository student_repo = new StudentRepository(conn_str);
        static GroupRepository group_repo = new GroupRepository(conn_str);

        static void Main()
        {
            Console.WriteLine("Добро пожаловать!");
            while (is_running)
            {
                Console.WriteLine("1 — Показать всех студентов");
                Console.WriteLine("2 — Показать студента по ID");
                Console.WriteLine("3 — Показать студентов группы");
                Console.WriteLine("4 — Добавить студента");
                Console.WriteLine("5 — Показать все группы");
                Console.WriteLine("6 — Показать группу по ID");
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
                        ShowAllStudents();
                        break;
                    case "2":
                        ShowAllStudentById();
                        break;
                    case "3":
                        Console.Write("Введите ID группы: ");
                        if (int.TryParse(Console.ReadLine(), out int groupId) && groupId > 0)
                        {
                            ShowStudentsFromGroup(groupId);
                        }
                        else
                        {
                            Console.WriteLine("Введите положительное целое число.");
                        }
                        break;
                    case "4":
                        AddStudent();
                        break;
                    case "5":
                        ShowAllGroups();
                        break;
                    case "6":
                        ShowGroupById();
                        break;
                    case "9":
                        is_running = false;
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

        static void ShowStudentsFromGroup(int id)
        {
            var students = student_repo.GetStudentsFromGroup(id);
            if (students.Count == 0)
            {
                Console.WriteLine("Студенты с указанным ID группы не найдены.");
            }
            foreach (var student in students)
            {
                Console.WriteLine(student.ToString());
            }
        }

        static void ShowAllStudentById()
        {
            Console.Write("Введите ID студента: ");
            int id = int.Parse(Console.ReadLine());
            var student = student_repo.GetStudentById(id);
            Console.WriteLine(student);

        }

        static void ShowAllStudents()
        {
            var students = student_repo.GetAllStudent();
            foreach (var student in students)
            {
                Console.WriteLine(student.ToString());
            } 
        }
        static void ShowAllGroups()
        {
            var groups = group_repo.GetAllGroups();
            foreach (var group in groups)
            {
                Console.WriteLine(group.ToString());
            }
        }

        static void ShowGroupById()
        {
            Console.Write("Введите ID группы: ");
            int id = int.Parse(Console.ReadLine());
            var group = group_repo.GetGroupById(id);
            Console.WriteLine(group);
        }

        static void AddStudent()
        {
            var student = new Student();
            Console.Write("Введите имя: ");
            student.FirstName = Console.ReadLine();
            Console.Write("Введите фамилию: ");
            student.LastName = Console.ReadLine();
            Console.Write("Введите возраст: ");
            student.Age = int.Parse(Console.ReadLine());
            Console.Write("Введите ID группы: ");
            student.GroupId = int.Parse(Console.ReadLine());
            student_repo.AddStudent(student);
            Console.WriteLine("Студент добавлен.");
        }
    }
}
