using System.ComponentModel.DataAnnotations;

namespace DishPlannerApp.DTOs
{
    public class RecipeDto
    {
        public int RecipeId { get; set; }
        [Required]
        [StringLength(200)]
        public string RecipeTitle { get; set; }
        public string Description { get; set; }
        public List<string> Ingredients { get; set; }
        public string Instructions { get; set; }
    }
}