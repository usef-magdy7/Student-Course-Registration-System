using System.ComponentModel.DataAnnotations;

namespace StudentCourseSystem.Models.Entities
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string? Phone { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public int Level { get; set; }

        [Required]
        public string Department { get; set; } = string.Empty;

        // Navigation Properties
        public ICollection<CourseRequest> CourseRequests { get; set; } = new List<CourseRequest>();
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}