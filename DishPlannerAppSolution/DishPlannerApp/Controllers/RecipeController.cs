using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DishPlannerApp.Models;
using DishPlannerApp.Data.RecipeRepository;
using DishPlannerApp.DTOs;

namespace DishPlannerApp.Controllers
{
    [Authorize] // Only authenticated users
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        // List all recipes
        public async Task<IActionResult> Index()
        {
            var recipes = await _recipeRepository.GetAllRecipesAsync();
            var recipeDtos = recipes.Select(r => new RecipeDto
            {
                RecipeId = r.RecipeId,
                RecipeTitle = r.RecipeTitle,
                Description = r.Description,
                Ingredients = r.Ingredients,
                Instructions = r.Instructions
            });
            return View(recipeDtos);
        }

        // View details of a recipe
        public async Task<IActionResult> GetRecipeDetails(int id)
        {
            var recipe = await _recipeRepository.GetRecipeByIdAsync(id);
            if(recipe == null) return NotFound();

            var recipeDto = new RecipeDto
            {
                RecipeId = recipe.RecipeId,
                RecipeTitle = recipe.RecipeTitle,
                Description = recipe.Description,
                Ingredients = recipe.Ingredients,
                Instructions = recipe.Instructions
            };

            return View(recipeDto);
        }

        // Show create form
        public IActionResult Create() 
        {
            return View();
        }

        // Handle creation
        [HttpPost]
        public async Task<IActionResult> Create(RecipeDto recipeDto)
        {
            if(!ModelState.IsValid) return View(recipeDto);

            var recipe = new Recipe
            {
                RecipeTitle = recipeDto.RecipeTitle,
                Description = recipeDto.Description,
                Ingredients = recipeDto.Ingredients,
                Instructions = recipeDto.Instructions,
                CreatedByUserId = User.Identity.Name
            };

            await _recipeRepository.CreateRecipeAsync(recipe);
            return RedirectToAction(nameof(Index));
        }

        // Show edit form
        public async Task<IActionResult> Edit(int Id)
        {
            var recipe = await _recipeRepository.GetRecipeByIdAsync(Id);
            if (recipe == null) return NotFound();

            var recipeDto = new RecipeDto
            {
                RecipeId = recipe.RecipeId,
                RecipeTitle = recipe.RecipeTitle,
                Description = recipe.Description,
                Ingredients = recipe.Ingredients,
                Instructions = recipe.Instructions
            };

            return View(recipeDto);
        }

        // Handle updates
        [HttpPost]
        public async Task<IActionResult> Edit(RecipeDto recipeDto)
        {
            if (!ModelState.IsValid) return View(recipeDto);

            var recipe = new Recipe
            {
                RecipeId = recipeDto.RecipeId,
                RecipeTitle = recipeDto.RecipeTitle,
                Description = recipeDto.Description,
                Ingredients = recipeDto.Ingredients,
                Instructions = recipeDto.Instructions
            };

            await _recipeRepository.UpdateRecipeAsync(recipe);
            return View(recipeDto);
        }

        // Confirm deletion
        public async Task<IActionResult> Delete(int id)
        {
            var recipe = await _recipeRepository.GetRecipeByIdAsync(id); 
            if (recipe == null) return NotFound();

            return View(recipe);
        }

        // Handle deletion
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _recipeRepository.DeleteRecipeAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
