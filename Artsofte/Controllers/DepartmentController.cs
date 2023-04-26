using Artsofte.Data;
using Artsofte.Models;
using Microsoft.AspNetCore.Mvc;

namespace Artsofte.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ArtsoftDbContext _context;

        public DepartmentController( ArtsoftDbContext context )
        {
            _context = context;
        }

        [Route("department/add")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route( "department/add" )]
        public IActionResult Create( [Bind( "Name,Floor" )] Department department )
        {
            if( ModelState.IsValid ) {
                _context.Departments.Add( department );
                _context.SaveChanges();
                return RedirectToAction( "Index", "Employee" );
            }
            return View( department );
        }
    }
}
