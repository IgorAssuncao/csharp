using CommonPersonStatus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service;
using System.Collections.Generic;
using System.Linq;

namespace Web.Controllers
{
    public class PeopleController : Controller
    {
        // GET: People
        public ActionResult Index()
        {
            List<Person> people = PersonService.GetAllPeople();

            people = people.OrderBy(person => person.CalculatePersonNextBirthday()).ToList();

            return View(people);
        }

        // GET: People/Details/5
        public ActionResult Details(int id)
        {
            PersonFound personFound = PersonService.GetById(id);

            if (!personFound.Found)
                return View(nameof(Index));

            return View(personFound.Person);
        }

        public ActionResult SearchPeopleByNameOrLastname(string text)
        {
            List<Person> people = PersonService.SearchByNameOrLastname(text);
            return View(people);
        }
        
        // GET: People/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Person person)
        {
            try
            {
                if (ModelState.IsValid == false) return View();

                PersonService.Add(person.Name, person.Lastname, person.Birthday);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: People/Edit/5
        public ActionResult Edit(int id)
        {
            Person person = PersonService.GetById(id).Person;
            return View(person);
        }

        // POST: People/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Person person)
        {
            try
            {
                PersonService.UpdatePerson(id, person.Name, person.Lastname, person.Birthday);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: People/Delete/5
        public ActionResult Delete(int id)
        {
            Person person = PersonService.GetById(id).Person;
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Person person)
        {
            try
            {
                PersonService.DeletePerson(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}