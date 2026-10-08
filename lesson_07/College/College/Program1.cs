using System;
using College.Data;


namespace College_DB
{
    class Program
    {
        static readonly string conn_str = "Data Source=COMP11A1\\SQLEXPRESS;" +"Initial Catalog=College;" +"Integrated Security=True;" +"TrustServerCertificate=True;";
        static bool is_running = true;

        static StudentRepository student_repo = new StudentRepository(conn_str);

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
                        ShowAllStudents();
                        break;
                    case "9":
                        is_running = false;
                        break;
                    case "2":
                        //ShowAllGroups(conn_str);
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

        static void ShowAllStudents()
        {
            var students = student_repo.GetAllStudent();
            foreach (var student in students)
            {
                Console.WriteLine(student.ToString());
            } 
        }

    }
}
