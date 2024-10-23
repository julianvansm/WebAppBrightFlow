using Microsoft.AspNetCore.Mvc;
using WebAppBrightFlow.Data;
using WebAppBrightFlow.Models;
using System.Diagnostics;

namespace MyPage.Controllers
{
    public class MyPageController : Controller
    {
        private readonly ApplicationDbContext _context;


        public MyPageController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchQuery)
        {
            var people = _context.People.ToList();
            if (!String.IsNullOrEmpty(searchQuery))
            {
                searchQuery.ToLower();

                people = people.Where(person => person.Name.Contains(searchQuery)).ToList();

            }
            ViewBag.People = people;

            return View();
        }



        public IActionResult Detail(string name)
        {
            var person = _context.People.FirstOrDefault(p => p.Name == name);

            if (person == null)
            {
                return NotFound();
            }

            ViewBag.Name = person.Name;
            ViewBag.Age = person.Age;
            ViewBag.Gender = person.Gender;
            ViewBag.Description = person.Description;
            ViewBag.YearOfEmployment = person.YearOfEmployment;

            return View();
        }



        [HttpPost]
        public IActionResult AddPerson(Person newPerson)
        {
            Trace.WriteLine("hello");
            _context.People.Add(newPerson);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DeletePerson(int id)
        {
            var person = _context.People.FirstOrDefault(p => p.Id == id);

            if (person != null)
            {
                _context.People.Remove(person);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var person = _context.People.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        
        [HttpPost]
        public IActionResult EditPerson(Person updatedPerson)
        {
            var person = _context.People.FirstOrDefault(p => p.Id == updatedPerson.Id);
            if (person != null)
            {
                person.Name = updatedPerson.Name;
                person.Age = updatedPerson.Age;
                person.Gender = updatedPerson.Gender;
                person.Description = updatedPerson.Description;
                person.YearOfEmployment = updatedPerson.YearOfEmployment;

                _context.SaveChanges(); 
            }

            return RedirectToAction("Index");
        }
    }
}
