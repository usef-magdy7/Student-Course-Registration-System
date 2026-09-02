using Microsoft.AspNetCore.Mvc;
using StudentCourseRegistrationSystem.Data;
using StudentCourseRegistrationSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;

using StudentCourseRegistrationSystem.Models.ViewModels;

namespace StudentCourseRegistrationSystem.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;

        public CourseController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Course
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.ToListAsync();
            return View(courses);
        }

        // GET: Course/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var course = await _context.Courses
                .Include(c => c.StudentCourses)
                    .ThenInclude(sc => sc.Student)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null) return NotFound();

          
            var studentCoursesList = course.StudentCourses.ToList();

            var viewModel = new CourseDetailsViewModel
            {
                CourseId = course.Id,
                Title = course.Title,
                EnrolledStudentIds = studentCoursesList
                    .Select(sc => int.TryParse(sc.StudentId, out int parsedId) ? parsedId : 0)
                    .ToList(),
                EnrolledStudents = studentCoursesList
                    .Where(sc => sc.Student != null)
                    .Select(sc => new Student
                    {
                        Email = sc.Student.Email,
                        FullName = sc.Student.UserName
                    })
                    .ToList()
            };

            return View(viewModel);
        }

        // GET: Course/Students/5
        public async Task<IActionResult> Students(int id)
        {
            var course = await _context.Courses
                .Include(c => c.StudentCourses)
                    .ThenInclude(sc => sc.Student)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null) return NotFound();

            var studentCoursesList = course.StudentCourses.ToList();

            var viewModel = new CourseDetailsViewModel
            {
                CourseId = course.Id,
                Title = course.Title,
                EnrolledStudentIds = studentCoursesList
                    .Select(sc => int.TryParse(sc.StudentId, out int parsedId) ? parsedId : 0)
                    .ToList(),
                EnrolledStudents = studentCoursesList
                    .Where(sc => sc.Student != null)
                    .Select(sc => new Student
                    {
                        Email = sc.Student.Email,
                        FullName = sc.Student.UserName
                    })
                    .ToList()
            };

            return View(viewModel);
        }
        // GET: Course/Create
        public IActionResult Create()
        {
            return View(new CourseFormViewModel());
        }

        // POST: Course/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var course = new Course
                {
                    CourseCode = model.CourseCode,
                    Title = model.Title,
                    Department = model.Department,
                    CreditHours = model.CreditHours,
                    MaxStudents = model.MaxStudents,
                    IsActive = model.IsActive
                };

                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Course/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();

            var viewModel = new CourseFormViewModel
            {
                Id = course.Id,
                CourseCode = course.CourseCode,
                Title = course.Title,
                Department = course.Department,
                CreditHours = course.CreditHours,
                MaxStudents = course.MaxStudents,
                IsActive = course.IsActive
            };

            return View(viewModel);
        }

        // POST: Course/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseFormViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var course = await _context.Courses.FindAsync(id);
                if (course == null) return NotFound();

                course.CourseCode = model.CourseCode;
                course.Title = model.Title;
                course.Department = model.Department;
                course.CreditHours = model.CreditHours;
                course.MaxStudents = model.MaxStudents;
                course.IsActive = model.IsActive;

                _context.Update(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Course/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();
            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
