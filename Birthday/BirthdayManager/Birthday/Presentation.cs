using System;
using Model;
using Service;

namespace Presentation
{
    class Presentation
    {
        static void Main(string[] args)
        {
            char[] validOptions = {'0', '1', '2' };
            char option;

            do
            {
                Console.WriteLine("Choose your option: ");
                Console.WriteLine("0 - Exit");
                Console.WriteLine("1 - Add Person");
                Console.WriteLine("2 - Search Person By Name");

                Console.Write("Option: ");
                option = Console.ReadLine().ToCharArray()[0];

                if (!Array.Exists(validOptions, validOption => validOption == option)) Console.WriteLine("Invalid option!");

                switch (option)
                {
                    case '1':
                        AddPerson();
                        break;
                    case '2':
                        SearchPersonByName();
                        break;
                }
            } while (option != '0');

            Console.WriteLine("Bye!");
        }

        static void AddPerson()
        {
            string name = ReadStringFromInput("Insert your name: ");
            string lastname = ReadStringFromInput("Insert your lastname: ");
            DateTime birthday = ReadDateTimeFromInput("Insert your birthday: ");

            var personStatus = PersonService.Add(name, lastname, birthday);

            if (personStatus.Registered)
                Console.WriteLine($"{personStatus.Message}");
            Console.WriteLine();
        }

        static void SearchPersonByName()
        {
            string name = ReadStringFromInput("Insert name: ");

            var person = PersonService.GetByName(name);

            Console.WriteLine();
            Console.WriteLine(person.Message);
            
            if (person.Found)
            {
                Console.WriteLine(person.Person.GetFullName());
                Console.WriteLine($"Days for birthday: {person.RemainingTimeForBirthday.Days}");
            }

            Console.WriteLine();
        }

        static string ReadStringFromInput(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        static DateTime ReadDateTimeFromInput(string message)
        {
            DateTime date = new DateTime();
            bool validDate = false;
            string input;

            try
            {
                do
                {
                    Console.WriteLine("Use the format MM-DD-YYYY");
                    Console.Write(message);
                    input = Console.ReadLine();

                    date = DateTime.Parse(input);

                    validDate = true;
                } while (!validDate);
            }
            catch (Exception)
            {
                if (!validDate)
                {
                    Console.WriteLine("Something went wrong, please try again...");
                }
            }

            return date;
        }
    }
}
