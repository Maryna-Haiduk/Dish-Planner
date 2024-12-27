using System.ComponentModel.DataAnnotations;

namespace DishPlannerApp.DTOs
{
    public class UserLoginDto
    {
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
