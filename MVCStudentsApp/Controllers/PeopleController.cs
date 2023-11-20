using StudentsAppMVC.Data;
using MVCStudentsApp.Models;
using MVCStudentsApp.PeopleModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace StudentsAppMVC.Controllers
{
    public class PeopleController : Controller
    {
        private readonly SchoolDatabaseContext mvcDbContext = new();

        [HttpGet]
        public async Task<IActionResult> Index(string search, int page = 1)
        {
            int pageSize = 10;
            IQueryable<Person> query = mvcDbContext.People.Include(p => p.FkRoleNavigation);

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.FirstName.Contains(search) || p.LastName.Contains(search));
            }

            List<Person> people = await query.OrderBy(p => p.Id)
                                            .Skip((page - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToListAsync();

            ViewBag.Search = search;
            ViewBag.Page = page;
            return View(people);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(IFormCollection addPersonRequest)
        {
            Person person = new()
            {
                FirstName = addPersonRequest["FirstName"],
                LastName = addPersonRequest["LastName"],
                DateOfBirth = DateTime.Parse(addPersonRequest["DateOfBirth"]),
                FkRole = int.Parse(addPersonRequest["FkRole"])
            };

            await mvcDbContext.People.AddAsync(person);
            await mvcDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Person person = await mvcDbContext.People.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            ViewBag.Roles = await mvcDbContext.Roles.ToListAsync();

            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, IFormCollection editPersonRequest)
        {
            Person person = await mvcDbContext.People.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            person.FirstName = editPersonRequest["FirstName"];
            person.LastName = editPersonRequest["LastName"];
            person.DateOfBirth = DateTime.Parse(editPersonRequest["DateOfBirth"]);
            person.FkRole = int.Parse(editPersonRequest["FkRole"]);

            mvcDbContext.Update(person);
            await mvcDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Person person = await mvcDbContext.People.Include(p => p.FkRoleNavigation).FirstOrDefaultAsync(p => p.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Person person = await mvcDbContext.People.Include(p => p.FkRoleNavigation).FirstOrDefaultAsync(p => p.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Person person = await mvcDbContext.People.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            mvcDbContext.People.Remove(person);
            await mvcDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }


    }
}