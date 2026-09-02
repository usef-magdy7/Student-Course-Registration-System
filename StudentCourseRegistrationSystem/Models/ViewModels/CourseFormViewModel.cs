using System.ComponentModel.DataAnnotations;

namespace StudentCourseRegistrationSystem.Models.ViewModels
{
    public class CourseFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Course Code is required")]
        [Display(Name = "Course Code")]
        public string CourseCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required(ErrorMessage = "Credit Hours is required")]
        [Range(1, 6, ErrorMessage = "Credit hours must be between 1 and 6")]
        [Display(Name = "Credit Hours")]
        public int CreditHours { get; set; } = 3;

        [Required(ErrorMessage = "Department is required")]
        public string Department { get; set; } = string.Empty;

        [Display(Name = "Max Students")]
        public int MaxStudents { get; set; } = 30;

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;
    }
}
