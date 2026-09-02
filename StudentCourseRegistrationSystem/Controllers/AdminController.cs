using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCourseRegistrationSystem.Data;
using StudentCourseRegistrationSystem.Models.Entities;

namespace StudentCourseRegistrationSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;


        public AdminController(AppDbContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> PendingRequests()
        {
            var requests = await _context.CourseRequests.Include(r => r.Student)
                .Include(r => r.Course)
                .Where(r => r.Status == RequestStatus.Pending)
                .ToListAsync();
            return View(requests);
        }
        //------------------------------------------------------------------------------//
        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var request = await _context.CourseRequests.FindAsync(id);
            if (request == null) return NotFound();

            request.Status = RequestStatus.Approved;

            var studentCourse = new StudentCourse
            {
                StudentId = request.StudentId,
                CourseId = request.CourseId
            };

            _context.StudentCourses.Add(studentCourse);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(PendingRequests));
        }
        //---------------------------------------------------------------------------------//

        public async Task<IActionResult> Reject(int id)
        {
            var request = await _context.CourseRequests.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            request.Status = RequestStatus.Rejected;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(PendingRequests));
        }
        /////////////////////////////////////////////////////////////////////////////////////////
       
    }
}
