namespace StudentCourseSystem.ViewModels
{
    public class PendingRequestViewModel
    {
        public int RequestId { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string StudentEmail { get; set; } = string.Empty;
        public string StudentDepartment { get; set; } = string.Empty;
        public int StudentLevel { get; set; }
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = string.Empty;
        public string CourseCode { get; set; } = string.Empty;
        public DateTime RequestedAt { get; set; }
    }
}