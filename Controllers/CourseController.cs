using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCourseSystem.Data;
using StudentCourseSystem.Models.Entities;
using StudentCourseSystem.Models.ViewModels;
using StudentCourseSystem.ViewModels;

namespace StudentCourseSystem.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
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

            var viewModel = new CourseDetailsViewModel
            {
                CourseId = course.Id,
                Title = course.Title,
                EnrolledStudentIds = course.StudentCourses.Select(sc => sc.StudentId).ToList(),
                EnrolledStudents = course.StudentCourses
                    .Where(sc => sc.Student != null)
                    .Select(sc => sc.Student!)
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

            var viewModel = new CourseDetailsViewModel
            {
                CourseId = course.Id,
                Title = course.Title,
                EnrolledStudentIds = course.StudentCourses.Select(sc => sc.StudentId).ToList(),
                EnrolledStudents = course.StudentCourses
                    .Where(sc => sc.Student != null)
                    .Select(sc => sc.Student!)
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

        // GET: Course/PendingRequests
        public async Task<IActionResult> PendingRequests()
        {
            var pendingRequests = await _context.CourseRequests
                .Include(cr => cr.Student)
                .Include(cr => cr.Course)
                .Where(cr => cr.Status == RequestStatus.Pending)
                .OrderByDescending(cr => cr.RequestedAt)
                .Select(cr => new PendingRequestViewModel
                {
                    RequestId = cr.Id,
                    StudentId = cr.StudentId,
                    StudentName = cr.Student.FullName,
                    StudentEmail = cr.Student.Email,
                    StudentDepartment = cr.Student.Department,
                    StudentLevel = cr.Student.Level,
                    CourseId = cr.CourseId,
                    CourseTitle = cr.Course.Title,
                    CourseCode = cr.Course.CourseCode,
                    RequestedAt = cr.RequestedAt
                })
                .ToListAsync();

            return View(pendingRequests);
        }

        // POST: Course/ApproveRequest/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveRequest(int id)
        {
            var request = await _context.CourseRequests
                .Include(cr => cr.Course)
                .FirstOrDefaultAsync(cr => cr.Id == id);

            if (request == null) return NotFound();

      
            var currentEnrolledCount = await _context.StudentCourses
                .CountAsync(sc => sc.CourseId == request.CourseId);

            if (currentEnrolledCount >= request.Course.MaxStudents)
            {
                ModelState.AddModelError("", "Cannot approve request: Course has reached maximum capacity.");
                return RedirectToAction(nameof(PendingRequests));
            }

            var isAlreadyEnrolled = await _context.StudentCourses
                .AnyAsync(sc => sc.StudentId == request.StudentId && sc.CourseId == request.CourseId);

            request.Status = RequestStatus.Approved;

            if (!isAlreadyEnrolled)
            {
                var studentCourse = new StudentCourse
                {
                    StudentId = request.StudentId,
                    CourseId = request.CourseId
                };

                _context.StudentCourses.Add(studentCourse);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(PendingRequests));
        }

        // POST: Course/RejectRequest/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectRequest(int id)
        {
            var request = await _context.CourseRequests.FindAsync(id);
            if (request == null) return NotFound();

            request.Status = RequestStatus.Rejected;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(PendingRequests));
        }
    }
}