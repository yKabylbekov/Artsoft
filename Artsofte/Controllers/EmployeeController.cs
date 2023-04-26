using Artsofte.Data;
using Artsofte.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Artsofte.Controllers
{
    
    public class EmployeeController : Controller
    {
        public ArtsoftDbContext _context;

        public EmployeeController( ArtsoftDbContext context )
        {
            _context = context;
        }

        [Route("/")]
        public IActionResult Index()
        {
            var employees = _context.Employees.ToList();
            ViewBag.Departments = _context.Departments.ToList();
            ViewBag.ProgrammingLanguages = _context.ProgrammingLanguages.ToList();
            return View( employees );
        }

        [Route("add")]
        public IActionResult Create()
        {
            ViewBag.Departments = _context.Departments.ToList();
            ViewBag.ProgrammingLanguages = _context.ProgrammingLanguages.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("add")]
        public IActionResult Create( [Bind( "Name,Surname,Age,Gender,DepartmentId,ProgrammingLanguageId" )]Employee employee )
        {
            if( ModelState.IsValid ) {
                _context.Employees.Add( employee );
                _context.SaveChanges();
                return RedirectToAction( "Index" );
            }
            return View( employee );
        }

        [Route("edit")]
        public IActionResult Edit( int id )
        {
            var employee = GetEmployeeById( id );
            ViewBag.Departments = _context.Departments.ToList();
            ViewBag.ProgrammingLanguages = _context.ProgrammingLanguages.ToList();
            if( employee == null ) {
                return NotFound();
            }
            return View( employee );
        }

        [HttpPost]
        [Route( "edit" )]
        public IActionResult Edit( Employee employee )
        {
            if( ModelState.IsValid ) {
                _context.Employees.Update( employee );
                _context.SaveChanges();
                return RedirectToAction( "Index" );
            }
            ViewBag.Departments = _context.Departments.ToList();
            ViewBag.ProgrammingLanguages = _context.ProgrammingLanguages.ToList();
            return View( employee );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete( int id )
        {
            var employee = GetEmployeeById( id );
            if( employee != null ) {
                _context.Employees.Remove( employee );
                _context.SaveChanges();
            }
            return RedirectToAction( "Index" );
        }

        public Employee GetEmployeeById( int employeeId )
        {
            return _context.Employees.FirstOrDefault( employee => employee.Id == employeeId );
        }

        public IActionResult GetNames()
        {
            var employeeNames = _context.Employees.Select( e => e.Name ).ToList();
            return Json( employeeNames );
        }
    }
}
