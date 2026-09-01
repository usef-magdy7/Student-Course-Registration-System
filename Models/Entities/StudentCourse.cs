namespace StudentCourseSystem.Models.Entities
{
    public class StudentCourse
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        public Course Course { get; set; } = null!;
        public Student Student { get; set; } = null!;
    }
}