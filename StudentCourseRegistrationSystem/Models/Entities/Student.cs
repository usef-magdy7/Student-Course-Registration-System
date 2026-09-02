using System.ComponentModel.DataAnnotations;

namespace StudentCourseRegistrationSystem.Models.Entities
{
    public class Student
    {
            [Key]
            public int Id { get; set; }

            [Required(ErrorMessage = "Student name is required")]
            [StringLength(100, MinimumLength = 3)]
            public string FullName { get; set; } = string.Empty;

            [Required(ErrorMessage = "Email is required")]
            [EmailAddress(ErrorMessage = "Invalid email format")]
            public string Email { get; set; } = string.Empty;

            [Phone]
            [Display(Name = "Phone Number")]
            public string Phone { get; set; } = string.Empty;

            [DataType(DataType.Date)]
            [Display(Name = "Date of Birth")]
            public DateTime DateOfBirth { get; set; }

            [Required]
            public int Level { get; set; }

            [Required]
            public string Department { get; set; } = string.Empty;

           
            public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
        
    }
}

