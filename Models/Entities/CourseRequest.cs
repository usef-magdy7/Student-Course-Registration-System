namespace StudentCourseSystem.Models.Entities
{
    public class CourseRequest
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public RequestStatus Status { get; set; } = RequestStatus.Pending;
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

        public Course Course { get; set; } = null!;
        public Student Student { get; set; } = null!;
    }
}