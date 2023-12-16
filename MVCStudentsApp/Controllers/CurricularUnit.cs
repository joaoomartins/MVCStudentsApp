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

namespace StudentsAppMVC.Controllers
{
    public class CurricularUnitController : Controller
    {
        private readonly SchoolDatabaseContext mvcDbContext = new();

        //Curricular Units

        [HttpGet]
        public async Task<IActionResult> IndexUc()
        {
            var curricularUnit = await mvcDbContext.CurricularUnits.ToListAsync();
            return View(curricularUnit);
        }

        // GET
        [HttpGet]
        public IActionResult CreateUc()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUc(CurricularUnit curricularUnit)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    mvcDbContext.CurricularUnits.Add(curricularUnit);
                    await mvcDbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(IndexUc));
                }
                return View(curricularUnit);
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUc(int id)
        {
            var curricularUnit = await mvcDbContext.CurricularUnits.FindAsync(id);
            return View(curricularUnit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUc(int id, CurricularUnit curricularUnit)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingUc = await mvcDbContext.CurricularUnits.FindAsync(id);
                    existingUc.NameUnits = curricularUnit.NameUnits;

                    await mvcDbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(IndexUc));
                }
                return View(curricularUnit);
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> DetailsUc(int id)
        {
            var curricularUnit = await mvcDbContext.CurricularUnits.FindAsync(id);

            if (curricularUnit == null)
            {
                return NotFound();
            }

            return View(curricularUnit);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUc(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CurricularUnit curricularUnit = await mvcDbContext.CurricularUnits.FindAsync(id);

            if (curricularUnit == null)
            {
                return NotFound();
            }

            return View(curricularUnit);
        }

        [HttpPost, ActionName("DeleteUc")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedUc(int id)
        {
            var curricularUnit = await mvcDbContext.CurricularUnits.FindAsync(id);

            if (curricularUnit == null)
            {
                return NotFound();
            }

            mvcDbContext.CurricularUnits.Remove(curricularUnit);
            await mvcDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(IndexUc));
        }

    }
}