using JohanHanssonSUT24_LABB3.Models;

namespace JohanHanssonSUT24_LABB3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool menuBool = true;
            //Main menu
            while (menuBool)
            {
                Console.WriteLine("--SCHOOL ADMIN MENU--");
                Console.WriteLine("[1] Show all students");
                Console.WriteLine("[2] Show students according to class");
                Console.WriteLine("[3] Add staff member");
                Console.WriteLine("[4] Show all staff members");
                Console.WriteLine("[5] Exit program");
                Console.WriteLine("Choose an option");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1": //Methods to show students
                        Methods.ShowStudents();
                        break;
                    case "2"://Methods to show students in a chosen class
                        Methods.StudentsInClass();
                        break;
                    case "3": //Methods to add staff members
                        Methods.AddMember();
                        break;
                    case "4": //Methods to show all staff members
                        Methods.ShowStaff();
                        break;
                    case "5":
                        menuBool = false;
                        break;
                    default: //Error-msg
                        Console.WriteLine("Choose between 1-5");
                        break;


                }

            }
        }
    }
}
