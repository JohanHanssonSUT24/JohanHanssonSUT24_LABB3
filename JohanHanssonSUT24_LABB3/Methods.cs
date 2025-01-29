using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JohanHanssonSUT24_LABB3.Models;
using Microsoft.EntityFrameworkCore;

namespace JohanHanssonSUT24_LABB3
{
    public class Methods
    {
        //Method to create a "temporary" context
        private static SchoolDbContext CreateContext()
        {
            return new SchoolDbContext();
        }
        //Menu
        public static void ShowStudents()
        {
            Console.Clear();
            Console.WriteLine("--SHOW ALL STUDENTS--");
            Console.WriteLine("Sort by:");
            Console.WriteLine("[1]First name Ascending");
            Console.WriteLine("[2]First name Descending");
            Console.WriteLine("[3]Last name Ascending");
            Console.WriteLine("[4]Last name Descending");
            string userInput = Console.ReadLine();
            //Call method for context
            using var context = CreateContext();
            //Create IQueryable to be able to send questions to DB
            IQueryable<Student> students = context.Students;

            //Menu for sorting students by name
            switch (userInput)
            {
                case "1":
                    students = students.OrderBy(students => students.FirstName);
                    break;
                case "2":
                    students = students.OrderByDescending(students => students.FirstName);
                    break;
                case "3":
                    students = students.OrderBy(students => students.LastName);
                    break;
                case "4":
                    students = students.OrderByDescending(students => students.LastName);
                    break;
                default:
                    Console.WriteLine("Choose between 1-4");
                    break;
            }
            //Print student name
            var student = students.ToList();
            foreach (var stud in student)
            {
                Console.WriteLine($"{stud.FirstName} {stud.LastName}");
            }
            Console.WriteLine();
        }
        //Method to show students in a chosen class
        public static void StudentsInClass()
        {
            using var context = CreateContext();
            var classes = context.Classes.ToList();
            Console.WriteLine("--CLASSES--");
            Console.WriteLine("Choose a class: ");
            for (int i = 0; i < classes.Count; i++)
            {
                Console.WriteLine($"{i + 1}, {classes[i].ClassName}");
            }
            int userChoice;
            if (int.TryParse(Console.ReadLine(), out userChoice) && userChoice > 0 && userChoice <= classes.Count)
            {
                var classChoice = classes[userChoice - 1];
                var classStudents = context.Students.Where(students => students.ClassId == classChoice.Id).ToList();
                Console.WriteLine($"Students in chosen class: {classChoice.ClassName}");
                foreach (var stud in classStudents)
                {
                    Console.WriteLine($"{stud.FirstName} {stud.LastName}");
                }
            }
            else
            {
                Console.WriteLine("Choose a class in the list.");
            }
        }
        //Method to add staff member
        public static void AddMember()
        { 
            Console.WriteLine("--ADD NEW STAFF MEMBER--");
            Console.WriteLine("Type in full name");
            string userInput = Console.ReadLine();
            Console.WriteLine("Enter occupation");
            string staffOccupation = Console.ReadLine();          
            using var context = CreateContext();
            //Search for first matching occupation-object
            var occupation = context.Occupations
                .FirstOrDefault(occupation => occupation.OccupationName == staffOccupation);
            //Adding new member to Staff
            if (occupation == null)
            {
                Console.WriteLine("No matching occupation.");
            }
            else
            {
                var newMember = new Staff
                {
                    StaffName = userInput,
                    OccupationId = occupation.OccupationId
                };
                context.Staff.Add(newMember);
                context.SaveChanges();
                Console.WriteLine($"{userInput} - {occupation.OccupationName}, was added to the staff.");
            }  
        }
        public static void ShowStaff()
        {
            //Menu to show and sort staff members, same function as ShowStudents-method
            Console.Clear();
            Console.WriteLine("--STAFF MEMBERS--");
            Console.WriteLine("Sort by:");
            Console.WriteLine("[1]Name Ascending");
            Console.WriteLine("[2]Name Descending");
            Console.WriteLine("[3]Occupation Ascending");
            Console.WriteLine("[4]Occupation Descending");
            string userInput = Console.ReadLine();
            using var context = CreateContext();
            IQueryable<Staff> staff = context.Staff.Include(s => s.Occupation);

            switch (userInput)
            {
                case "1":
                    staff = staff.OrderBy(staff => staff.StaffName);
                    break;
                case "2":
                    staff = staff.OrderByDescending(staff => staff.StaffName);
                    break;
                case "3":
                    staff = staff.OrderBy(staff => staff.Occupation.OccupationName);
                    break;
                case "4":
                    staff = staff.OrderByDescending(staff => staff.Occupation.OccupationName);
                    break;
                default:
                    Console.WriteLine("Choose between 1-4");
                    break;
            }
            var staffMembers = staff.ToList();
            foreach (var staf in staffMembers)
            {
                if(staf.Occupation != null)
                {
                    Console.WriteLine($"{staf.StaffName} - {staf.Occupation.OccupationName}");
                }
                else
                {
                    Console.WriteLine($"{staf.StaffName} - No occupation");
                }                
            }
            Console.WriteLine();
        }
    }
}
