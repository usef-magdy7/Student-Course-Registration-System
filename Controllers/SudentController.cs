using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCourseRegistrationSystem.Data;
using StudentCourseRegistrationSystem.Models;
using StudentCourseRegistrationSystem.Models.Entities;
using StudentCourseRegistrationSystem.Models.ViewModels;
using System.Security.Claims;
using System.Security.Claims;
namespace StudentCourseRegistrationSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
            var students = await _context.Students.ToListAsync();
            return View(students);
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(e => e.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }

            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            if (id != student.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(student);

            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(e => e.Id == id);
            if (student == null)
                return NotFound();

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        /////////////////////////////////////////////////////////////////////////////
        
        public async Task<IActionResult> AvailableCourses()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var courses = await _context.Courses.ToListAsync();

            var studentEnrollments = await _context.StudentCourses
                .Where(sc => sc.StudentId == currentUserId)
                .Select(sc => sc.CourseId)
                .ToListAsync();

            var studentRequests = await _context.CourseRequests
                .Where(cr => cr.StudentId == currentUserId)
                .Select(cr => cr.CourseId)
                .ToListAsync();

          
            var model = courses.Select(c => new StudentCourseEnrollmentViewModel
            {
                CourseId = c.Id,
                Title = c.Title,
                Code = c.CourseCode,
                Credits = c.CreditHours,
                IsEnrolled = studentEnrollments.Contains(c.Id),
                IsPending = studentRequests.Contains(c.Id)
            }).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RequestEnrollment(int courseId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(currentUserId))
            {
                return RedirectToAction("Login", "Account");
            }

            var request = new CourseRequest
            {
                StudentId = currentUserId,
                CourseId = courseId,
                Status = RequestStatus.Pending
            };

            _context.CourseRequests.Add(request);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(AvailableCourses));
        }
    }
}