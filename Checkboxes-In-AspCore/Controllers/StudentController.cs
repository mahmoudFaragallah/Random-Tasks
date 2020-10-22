using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkboxes_In_AspCore.Data;
using Checkboxes_In_AspCore.Models;
using Checkboxes_In_AspCore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace Checkboxes_In_AspCore.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            var item = _context.Students.ToList();
            return View(item);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var item = _context.Courses.ToList();

            StudentCourseViewModel model = new StudentCourseViewModel();
            model.AvailableCourses = item.Select(vm => new CheckBoxItem()
            {
                Id = vm.Id,
                Title = vm.Title,
                IsChecked = false
            }).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(StudentCourseViewModel studentCourseViewModel, Student student, StudentCourse studentCourse)
        {
            List<StudentCourse> stc = new List<StudentCourse>();

            // Set student values
            student.FirstName = studentCourseViewModel.FirstName;
            student.LastName = studentCourseViewModel.LastName;
            student.RollNumber = studentCourseViewModel.RollNumber;
            student.Email = studentCourseViewModel.Email;

            _context.Students.Add(student);
            _context.SaveChanges();

            int studentId = student.Id;

            // set courses to StudentCourseTable
            foreach (var item in studentCourseViewModel.AvailableCourses)
            {
                if (item.IsChecked == true)
                {
                    stc.Add(new StudentCourse()
                    {
                        StudentId = studentId,
                        CourseId = item.Id
                    });
                }
            }

            foreach (var item in stc)
            {
                _context.StudentCourses.Add(item);
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Student");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            StudentCourseViewModel studentCourseView = new StudentCourseViewModel();
            var student = _context.Students.Include(s => s.StudentCourse)
                                           .ThenInclude(c => c.Course)
                                           .AsNoTracking().SingleOrDefault(m => m.Id == id);

            var allCourse = _context.Courses.Select(vm => new CheckBoxItem()
            {
                Id = vm.Id,
                Title = vm.Title,
                IsChecked = vm.StudentCourse.Any(x => x.StudentId == student.Id) ? true : false
            }).ToList();

            studentCourseView.FirstName = student.FirstName;
            studentCourseView.LastName = student.LastName;
            studentCourseView.Email = student.Email;
            studentCourseView.RollNumber = student.RollNumber;
            studentCourseView.AvailableCourses = allCourse;

            return View(studentCourseView);
        }
    
        [HttpPost]
        public IActionResult Edit(StudentCourseViewModel studentCourseViewModel, Student student, StudentCourse studentCourse)
        {
            List<StudentCourse> _studentCourse = new List<StudentCourse>();

            // Set student values
            student.FirstName = studentCourseViewModel.FirstName;
            student.LastName = studentCourseViewModel.LastName;
            student.RollNumber = studentCourseViewModel.RollNumber;
            student.Email = studentCourseViewModel.Email;

            _context.Students.Update(student);
            _context.SaveChanges();

            int studentId = student.Id;

            foreach (var item in studentCourseViewModel.AvailableCourses)
            {
                if (item.IsChecked == true)
                {
                    _studentCourse.Add(new StudentCourse
                    {
                        StudentId = studentId,
                        CourseId = item.Id
                    });
                }
            }

            var databaseTable = _context.StudentCourses.Where(s => s.StudentId == studentId).ToList();
            var resultList = databaseTable.Except(_studentCourse).ToList();

            foreach (var item in resultList)
            {
                _context.StudentCourses.Remove(item);
                _context.SaveChanges();
            }

            var getCourseId = _context.StudentCourses.Where(a => a.StudentId == studentId).ToList();

            foreach (var item in _studentCourse)
            {
                if (!getCourseId.Contains(item))
                {
                    _context.StudentCourses.Add(item);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Index", "Student");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            StudentCourseViewModel studentCourseView = new StudentCourseViewModel();
            var student = _context.Students.Include(s => s.StudentCourse)
                                           .ThenInclude(c => c.Course)
                                           .AsNoTracking().SingleOrDefault(m => m.Id == id);

            var allCourse = _context.Courses.Select(vm => new CheckBoxItem()
            {
                Id = vm.Id,
                Title = vm.Title,
                IsChecked = vm.StudentCourse.Any(x => x.StudentId == student.Id) ? true : false
            }).ToList();

            studentCourseView.FirstName = student.FirstName;
            studentCourseView.LastName = student.LastName;
            studentCourseView.Email = student.Email;
            studentCourseView.RollNumber = student.RollNumber;
            studentCourseView.AvailableCourses = allCourse;

            return View(studentCourseView);
        }
    }
}
