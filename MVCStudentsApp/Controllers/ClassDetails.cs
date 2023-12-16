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

namespace MVCStudentsApp.Controllers
{
    public class ClassDetails : Controller
    {
        private readonly SchoolDatabaseContext mvcDbContext = new();

        // GET: ClassDetails
        [HttpGet]
        public async Task<IActionResult> IndexCd()
        {
            var classDetails = await mvcDbContext.ClassDetails.ToListAsync();
            return View(classDetails);
        }

        // GET
        [HttpGet]
        public IActionResult CreateCd()
        {
            var teachers = mvcDbContext.People
            .Where(p => p.FkRoleNavigation.LabelRole == "Teacher")
            .Select(p => new { Id = p.Id, FullName = $"{p.FirstName} {p.LastName}" })
            .ToList();

            ViewBag.TeacherId = new SelectList(teachers, "Id", "FullName");

            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCd(ClassDetail classDetails)
        {
            Console.WriteLine(classDetails);
            try
            {
                mvcDbContext.ClassDetails.Add(classDetails);
                await mvcDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(IndexCd));

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditCd(int id)
        {
            var classDetails = await mvcDbContext.ClassDetails.FindAsync(id);
            return View(classDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCd(int id, ClassDetail classDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingCd = await mvcDbContext.ClassDetails.FindAsync(id);
                    existingCd.NameClass = classDetails.NameClass;
                    existingCd.Teacher = classDetails.Teacher;
                    existingCd.TeacherId = classDetails.TeacherId;

                    await mvcDbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(IndexCd));
                }
                return View(classDetails);
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> DetailsCd(int id)
        {
            var classDetails = await mvcDbContext.ClassDetails.FindAsync(id);

            if (classDetails == null)
            {
                return NotFound();
            }

            return View(classDetails);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCd(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ClassDetail classDetails = await mvcDbContext.ClassDetails.FindAsync(id);

            if (classDetails == null)
            {
                return NotFound();
            }

            return View(classDetails);
        }

        [HttpPost, ActionName("DeleteCd")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedCd(int id)
        {
            var classDetails = await mvcDbContext.ClassDetails.FindAsync(id);

            if (classDetails == null)
            {
                return NotFound();
            }

            mvcDbContext.ClassDetails.Remove(classDetails);
            await mvcDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(IndexCd));
        }
    }
}
