using Model;
using System.Collections.Generic;

namespace Repository
{
    public interface IPersonRepository
    {
        public List<Person> GetAllPeople();

        public Person GetPersonById(int Id);

        public List<Person> SearchByNameOrLastname(string text);

        public void AddPerson(Person Person);

        public void UpdatePerson(Person person);

        public void DeletePerson(Person person);
    }
}
