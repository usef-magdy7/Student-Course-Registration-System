using StudentCourseSystem.Models.Entities;

namespace StudentCourseSystem.Models.ViewModels
{
    public class CourseDetailsViewModel
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<int> EnrolledStudentIds { get; set; } = new();
        

        public List<Student> EnrolledStudents { get; set; } = new();
    }
}