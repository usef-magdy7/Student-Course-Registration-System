using System;
using System.ComponentModel.DataAnnotations;

namespace StudentCourseRegistrationSystem.Models.Entities
{
    public enum RequestStatus
    {
        Pending,
        Approved,
        Rejected
    }

    public class CourseRequest
    {
        [Key]
        public int Id { get; set; }

        public string StudentId { get; set; }
        public Student? Student { get; set; }

        public int CourseId { get; set; }
        public Course? Course { get; set; }

        public RequestStatus Status { get; set; } = RequestStatus.Pending;
        public DateTime RequestDate { get; set; } = DateTime.UtcNow;
    }
}