using NUnit.Framework;
using System;
using Model;
using Repository;
using NSubstitute;
using System.Collections.Generic;

namespace RepositoryTests
{
    public class PersonRepositoryTests
    {
        [SetUp]
        public void Setup()
        {
        }
    }
    public class AddPeopleTests : PersonRepositoryTests
    {
        [Test]
        public void AddPeopleFailedTest()
        {
            Person personFixture = new Person("Igor", "Assuncao", new DateTime(1996, 7, 11));
            List<Person> personListFixture = new List<Person>();

            var PersonRepositoryMock = Substitute.For<IPersonRepository>();

            PersonRepositoryMock.AddPerson(personFixture, personListFixture).Returns(false);

            var addedPersonResult = PersonRepositoryMock.AddPerson(personFixture, personListFixture);
            Assert.False(addedPersonResult);
        }

        [Test]
        public void AddPeopleSuccessfullyTest()
        {
            Person personFixture = new Person("Igor", "Assuncao", new DateTime(1996, 7, 11));
            List<Person> personListFixture = new List<Person>();

            var PersonRepositoryMock = Substitute.For<IPersonRepository>();

            PersonRepositoryMock.AddPerson(personFixture, personListFixture).Returns(true);

            var addedPersonResult = PersonRepositoryMock.AddPerson(personFixture, personListFixture);
            Assert.True(addedPersonResult);
        }
    }

    public class GetPersonByNameTests : PersonRepositoryTests
    {
        [Test]
        public void GetPersonByNameFailedDueToNotFound()
        {
            List<Person> personListFixture = new List<Person>();

            var PersonRepositoryMock = Substitute.For<IPersonRepository>();

            var getPersonByNameResult = PersonRepositoryMock.GetPersonByName("test", personListFixture);

            Assert.IsNull(getPersonByNameResult);
        }

        [Test]
        public void GetPersonByNameSuccessfully()
        {
            Person personFixture = new Person("Igor", "Assuncao", new DateTime(1996, 7, 11));
            List<Person> personListFixture = new List<Person>();

            var PersonRepositoryMock = Substitute.For<IPersonRepository>();

            PersonRepositoryMock.GetPersonByName(personFixture.Name, personListFixture).Returns(personFixture);

            var getPersonByNameResult = PersonRepositoryMock.GetPersonByName(personFixture.Name, personListFixture);

            Assert.AreEqual(getPersonByNameResult, personFixture);
        }
    }

    public class GetPersonRemainingTimeForBirthdayTests : PersonRepositoryTests
    {
        [Test]
        public void GetPersonRemainingTimeForBirthday()
        {
            Person personFixture = new Person("Igor", "Assuncao", new DateTime(1996, 7, 11));

            var PersonRepositoryMock = Substitute.For<IPersonRepository>();

            PersonRepositoryMock.GetPersonRemainingTimeForBirthday(personFixture).Returns(PersonRepository2.GetPersonRemainingTimeForBirthday(personFixture));

            var result = PersonRepositoryMock.GetPersonRemainingTimeForBirthday(personFixture);

            Assert.IsNotNull(result.Days);
        }
    }
}