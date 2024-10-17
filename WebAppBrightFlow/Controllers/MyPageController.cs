using Microsoft.AspNetCore.Mvc;
using WebAppBrightFlow.Data;
using Microsoft.EntityFrameworkCore;
using WebAppBrightFlow.Models;
using System.Web;
using System.Linq;

namespace MyPage.Controllers
{
    public class MyPageController : Controller
    {
        private readonly ApplicationDbContext _context;


        public MyPageController(ApplicationDbContext context)
        {
            _context = context;
        }


     


        public IActionResult Index()
        {
            var people = _context.People.ToList();
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
            _context.People.Add(newPerson);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}