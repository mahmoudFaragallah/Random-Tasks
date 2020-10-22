using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkboxes_In_AspCore.Data;
using Checkboxes_In_AspCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Checkboxes_In_AspCore.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            var item = _context.Courses.ToList();
            return View(item);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
            return RedirectToAction("Index", "Course");
        }
    }
}
