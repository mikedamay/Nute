using System.Collections.Generic;

namespace Nute.Entities
{
    public class Meal
    {
        public Meal()
        {
            
        }
        public long Id { get; private set; }
        public MealTime MealTime { get; private set; }
        public List<MealIngredient> Ingredients { get; }
          = new List<MealIngredient>();
        public Meal(MealTime mealTime, long id = 0)
        {
            Id = id;
            MealTime = mealTime;
        }        
    }
}