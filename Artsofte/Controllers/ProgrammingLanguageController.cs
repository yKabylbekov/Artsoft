using Artsofte.Data;
using Artsofte.Models;
using Microsoft.AspNetCore.Mvc;

namespace Artsofte.Controllers
{
    public class ProgrammingLanguageController : Controller
    {
        private readonly ArtsoftDbContext _context;

        public ProgrammingLanguageController( ArtsoftDbContext context )
        {
            _context = context;
        }

        [Route( "language/add" )]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route( "language/add" )]
        public IActionResult Create( [Bind( "Name" )] ProgrammingLanguage language )
        {
            if( ModelState.IsValid ) {
                _context.ProgrammingLanguages.Add( language );
                _context.SaveChanges();
                return RedirectToAction("Index","Employee");
            }
            return View( language );
        }
    }
}
