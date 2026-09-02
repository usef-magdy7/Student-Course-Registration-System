namespace StudentCourseRegistrationSystem.Models.ViewModels
{
    public class StudentCourseEnrollmentViewModel
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int Credits { get; set; }
        public bool IsEnrolled { get; set; }
        public bool IsPending { get; set; }
    }
}