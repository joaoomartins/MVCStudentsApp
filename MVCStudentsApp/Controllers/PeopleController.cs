using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using StudentsAppMVC.Data;
using MVCStudentsApp.Models;
using MVCStudentsApp.PeopleModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace StudentsAppMVC.Controllers
{
    public class PeopleController : Controller
    {
        private readonly SchoolDatabaseContext mvcDbContext = new();

        [HttpGet]
        public async Task<IActionResult> Index(string search, int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 10;

            var query = from person in mvcDbContext.People
                        join role in mvcDbContext.Roles on person.FkRole equals role.Id
                        select new PersonWithRole()
                        {
                            person = person,
                            role = role
                        };

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.person.FirstName.Contains(search) || p.person.LastName.Contains(search));
            }

            var peoplePagedList = await query.ToPagedListAsync(pageNumber, pageSize);

            ViewBag.Search = search;

            return View(peoplePagedList);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Roles = mvcDbContext.Roles.ToList();
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