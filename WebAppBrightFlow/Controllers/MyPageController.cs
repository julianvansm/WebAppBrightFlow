using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
using System.Web;

namespace MyPage.Controllers
{
    public class MyPageController : Controller
    {

        private List<Person> people = new List<Person>
        {
                new Person { Name = "Jessica", Age = 22 , Gender = "Vrouw", Desription = "Jessica werkt als secretaresse en is een heel vriendelijk en zeer hard werkende werknemer.", YearOfEmployment = DateTime.Today },
                new Person { Name = "Bob", Age = 53, Gender = "Man", Desription = "Bob zorgt voor een goede en gezellige sfeer als product manager, daarnaast kan hij de lekkerste appeltaarten bakken.", YearOfEmployment = DateTime.Today },
                new Person { Name = "Charlie", Age = 27, Gender = "Man",  Desription = "Charlie is de van de kantine en houdt enorm van broodjes.", YearOfEmployment = DateTime.Today }
        };


        public IActionResult Index()
        {
    

            ViewBag.People = people;
            return View();
        }



        public IActionResult Detail(string name)
        {
            var person = people.FirstOrDefault(p => p.Name == name);

 
            if (person == null)
            {
                return NotFound();
            }


            ViewBag.Name = person.Name;
            ViewBag.Age = person.Age;
            ViewBag.Gender = person.Gender;
            ViewBag.Description = person.Desription;
            ViewBag.YearOfEmployment = person.YearOfEmployment;


            return View();
        }
    


        [HttpPost]
        public IActionResult AddPerson (Person newPerson)
        {
        
                people.Add(newPerson);
                return ();
        }
    }
}