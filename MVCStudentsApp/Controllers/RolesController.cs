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
    public class RolesController : Controller
    {
        private readonly SchoolDatabaseContext mvcDbContext = new();

        //ROLES

        [HttpGet]
        public async Task<IActionResult> IndexRole()
        {
            var roles = await mvcDbContext.Roles.ToListAsync();
            return View(roles);
        }

        // GET: Roles
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        //POST: Roles
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(Role role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    mvcDbContext.Roles.Add(role);
                    await mvcDbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(IndexRole));
                }
                return View(role);
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(int id)
        {
            var role = await mvcDbContext.Roles.FindAsync(id);
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(int id, Role role)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingRole = await mvcDbContext.Roles.FindAsync(id);
                    existingRole.LabelRole = role.LabelRole;
                    existingRole.DescriptionRole = role.DescriptionRole;

                    await mvcDbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(IndexRole));
                }
                return View(role);
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> DetailsRole(int id)
        {
            var role = await mvcDbContext.Roles.FindAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRole(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Role role = await mvcDbContext.Roles.FindAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        [HttpPost, ActionName("DeleteRole")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedRole(int id)
        {
            var role = await mvcDbContext.Roles.FindAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            mvcDbContext.Roles.Remove(role);
            await mvcDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(IndexRole));
        }

    }
}