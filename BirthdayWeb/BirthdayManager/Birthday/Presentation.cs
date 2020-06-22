using System;
using System.Globalization;
using Model;
using Service;
using Context;
using Microsoft.EntityFrameworkCore;
using CommonPersonStatus;
using System.Collections.Generic;

namespace Presentation
{
    class Presentation
    {
        static void Main(string[] args)
        {
            PersonContext personContext = new PersonContext();
            personContext.Database.EnsureDeleted();
            personContext.Database.Migrate();
            

            char[] validOptions = {'0', '1', '2', '3', '4', '5', '6', '7', '8' };
            char option;

            do
            {
                Console.WriteLine("Choose your option: ");
                Console.WriteLine("0 - Exit");
                Console.WriteLine("1 - Get All People");
                Console.WriteLine("2 - Search Person By Id");
                Console.WriteLine("3 - Get all people that birthday is today");
                Console.WriteLine("4 - Add Person");
                Console.WriteLine("5 - Update Person");
                Console.WriteLine("6 - Remove Person");
                Console.WriteLine("7 - Add Friend");
                Console.WriteLine("8 - Remove Friend");

                Console.Write("Option: ");
                option = Console.ReadLine().ToCharArray()[0];

                if (!Array.Exists(validOptions, validOption => validOption == option)) Console.WriteLine("Invalid option!");

                switch (option)
                {
                    case '1':
                        SearchAllPeople();
                        break;
                    case '2':
                        SearchPersonById();
                        break;
                    case '3':
                        GetAllPeopleThatBirthdayIsToday();
                        break;
                    case '4':
                        AddPerson();
                        break;
                    case '5':
                        UpdatePerson();
                        break;
                    case '6':
                        RemovePerson();
                        break;
                    case '7':
                        AddFriend();
                        break;
                    case '8':
                        RemoveFriend();
                        break;
                }
            } while (option != '0');

            Console.WriteLine("Bye!");
        }

        private static void SearchAllPeople()
        {
            List<Person> allPeople = PersonService.GetAllPeople();

            Console.WriteLine("All People Result:");
            Console.WriteLine("------------------");

            if (allPeople.Count <= 0)
            {
                Console.WriteLine("No people found.");
                return;
            }

            foreach(Person person in allPeople)
            {
                Console.WriteLine($"ID: {person.Id}");
                Console.WriteLine($"Name: {person.Name}");
                Console.WriteLine($"Lastname: {person.Lastname}");
                string message = person.Friends.Count <= 0 ? "This person has no friends." : "This person has friends.";
                Console.WriteLine(message);
            }
        }

        static void SearchPersonById()
        {
            int Id = Int32.Parse(ReadStringFromInput("Insert Id: "));

            var person = PersonService.GetById(Id);

            Console.WriteLine();
            Console.WriteLine(person.Message);

            if (person.Found)
            {
                Console.WriteLine($"Id: {person.Person.Id}");
                Console.WriteLine($"Full Name: {person.Person.GetFullName()}");
                Console.WriteLine($"Birthday: {person.Person.Birthday}");
                Console.WriteLine($"Days for birthday: {person.RemainingDaysForBirthday}");
                if (person.Person.Friends.Count > 0)
                {
                    Console.WriteLine("Friends: [");
                    foreach(PersonFriends friend in person.Person.Friends)
                    {
                        Console.WriteLine($"-- Friend Id: {friend.friendId}");
                        var friendFound = PersonService.GetById(friend.friendId);
                        Console.WriteLine($"-- Friend Name: {friendFound.Person.GetFullName()}");
                        Console.WriteLine($"-- Remaining days for birthday friend: {friendFound.RemainingDaysForBirthday}");
                        Console.WriteLine("-------------------------------");
                    }
                    Console.WriteLine("]");
                }
                else
                    Console.WriteLine("This person does not have any friends.");
            }

            Console.WriteLine();
        }

        static void GetAllPeopleThatBirthdayIsToday()
        {
            List<Person> people = PersonService.GetAllPeopleThatBirthdayIsToday();

            Console.WriteLine();
            
            if (people.Count <= 0)
            {
                Console.WriteLine("No one has birthday today.");
                Console.WriteLine();
                return;
            }

            foreach(Person person in people)
            {
                Console.WriteLine($"Person Id: {person.Id}");
                Console.WriteLine($"Person Name: {person.GetFullName()}");
                Console.WriteLine($"Person Birthday: {person.Birthday}");
                Console.WriteLine("-------------------------------");
            }
        }

        static void AddPerson()
        {
            string name = ReadStringFromInput("Insert your name: ");
            string lastname = ReadStringFromInput("Insert your lastname: ");
            DateTime birthday = ReadDateTimeFromInput("Insert your birthday: ");

            var personStatus = PersonService.Add(name, lastname, birthday);

            if (personStatus.Registered)
                Console.WriteLine($"{personStatus.Message}");
            else
                Console.WriteLine($"Person not registered - {personStatus.Message}");

            Console.WriteLine();
        }

        private static void UpdatePerson()
        {
            int Id = Int32.Parse(ReadStringFromInput("Insert Id: "));

            var person = PersonService.GetById(Id);

            Console.WriteLine();

            if (!person.Found)
            {
                Console.WriteLine(person.Message);
                return;
            }

            string name = ReadStringFromInput("Insert new name: ");
            string lastname = ReadStringFromInput("Insert new lastname: ");
            DateTime birthday = ReadDateTimeFromInput("Insert new birthday: ");

            PersonService.UpdatePerson(Id, name, lastname, birthday);
        }

        private static void RemovePerson()
        {
            int Id = Int32.Parse(ReadStringFromInput("Insert Id: "));

            var person = PersonService.GetById(Id);

            Console.WriteLine(person.Message);

            if (!person.Found)
                return;

            PersonService.DeletePerson(Id);
        }

        private static void AddFriend()
        {
            int Id = Int32.Parse(ReadStringFromInput("Your ID: "));

            PersonFound personFound = PersonService.GetById(Id);
            if (!personFound.Found)
            {
                Console.WriteLine("ID does not exists.");
                return;
            }

            int FriendId = Int32.Parse(ReadStringFromInput("Friend ID: "));

            if (FriendId == Id)
            {
                Console.WriteLine("You can not add yourself as friend.");
                return;
            }

            PersonFound friendFound = PersonService.GetById(FriendId);
            if (!friendFound.Found)
            {
                Console.WriteLine("ID does not exists.");
                return;
            }

            if (personFound.Person.Friends.Exists(person => person.personId == personFound.Person.Id && person.friendId == friendFound.Person.Id)) {
                Console.WriteLine("This person is already your friend.");
                return;
            }

            PersonService.AddPersonFriend(personFound.Person.Id, friendFound.Person.Id);
        }

        private static void RemoveFriend()
        {
            int Id = Int32.Parse(ReadStringFromInput("Your ID: "));

            PersonFound personFound = PersonService.GetById(Id);
            if (!personFound.Found)
            {
                Console.WriteLine("ID does not exists.");
                return;
            }

            int FriendId = Int32.Parse(ReadStringFromInput("Friend ID: "));

            if (FriendId == Id)
            {
                Console.WriteLine("You can not remove yourself as your friend.");
                return;
            }

            PersonFound friendFound = PersonService.GetById(FriendId);
            if (!friendFound.Found)
            {
                Console.WriteLine("ID does not exists.");
                return;
            }

            if (!personFound.Person.Friends.Exists(personFriend => personFriend.personId == personFound.Person.Id && personFriend.friendId == friendFound.Person.Id)) {
                Console.WriteLine("This person is not your friend.");
                return;
            }

            PersonService.RemovePersonFriend(personFound.Person.Id, friendFound.Person.Id);
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

            do
            {
                try
                {
                    Console.WriteLine("Use the format YYYY-MM-DD");
                    Console.Write(message);
                    input = Console.ReadLine();

                    date = DateTime.ParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                    validDate = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    if (!validDate)
                    {
                        Console.WriteLine("Something went wrong, please try again...");
                    }
                }
            } while (!validDate);

            return date;
        }
    }
}
