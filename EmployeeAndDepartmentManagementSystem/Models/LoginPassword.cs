using System.ComponentModel.DataAnnotations;

namespace EmployeeAndDepartmentManagementSystem.Models
{
    public class LoginPassword
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, ErrorMessage = "Username cannot be longer than 50 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Password cannot be longer than 255 characters")]
        public string Password { get; set; }

    }
}
