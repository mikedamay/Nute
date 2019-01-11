using System.Collections.Generic;

namespace Nute.Entities
{
    public class MealIngredient
    {
        public long MealId { get; private set; }
        public long IngredientId { get; private set; }

        public IEnumerable<Meal> Meals { get; }
        public IEnumerable<Ingredient> Ingredients{ get; }
    }
}