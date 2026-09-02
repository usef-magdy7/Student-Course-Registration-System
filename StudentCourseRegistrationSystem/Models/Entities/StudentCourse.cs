using Microsoft.AspNetCore.Identity;

namespace StudentCourseRegistrationSystem.Models.Entities
{
    public class StudentCourse
    {
      public string StudentId { get; set; }
      public IdentityUser Student { get; set; }
      public int CourseId { get; set; }
      public Course Course { get; set; }

      public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

    }
}
