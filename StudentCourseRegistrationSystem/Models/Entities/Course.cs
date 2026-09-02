namespace StudentCourseRegistrationSystem.Models.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseCode { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int CreditHours { get; set; }
        public string Department { get; set; } = string.Empty;
        public int MaxStudents { get; set; } = 30;
        public bool IsActive { get; set; } = true;

        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

    }
}

