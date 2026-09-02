using System.ComponentModel.DataAnnotations;

namespace StudentCourseRegistrationSystem.Models.ViewModels
{
    public class Register
    {

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm password is Required")]
        [DataType(DataType.Password)]
        [Compare("Password" , ErrorMessage ="Password do not match")]
        public String ConfirmPassword { get; set; } = string.Empty;

    }
}
