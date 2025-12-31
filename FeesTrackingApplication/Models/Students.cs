using System.ComponentModel.DataAnnotations;

namespace FeesTrackingApplication.Models
{
    public class Students
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "PRN Number is required")]
        public string PRN_Number { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50)]
        public string FName { get; set; }

        public string MName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }
    }
}
