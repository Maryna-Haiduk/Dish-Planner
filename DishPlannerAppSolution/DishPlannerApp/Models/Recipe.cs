using System.ComponentModel.DataAnnotations;

namespace DishPlannerApp.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string RecipeTitle { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }
        public string CreatedByUserId { get; set; }  // Foreign key linking to the User who created it

    }
}