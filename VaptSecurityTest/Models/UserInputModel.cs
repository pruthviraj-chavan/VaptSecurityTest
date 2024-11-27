using System.ComponentModel.DataAnnotations;

namespace VaptSecurityTest.Models
{
    public class UserInputModel
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{5,}$",
            ErrorMessage = "Password must contain at least one letter and one number.")]
        public string Password { get; set; }
    }
}
