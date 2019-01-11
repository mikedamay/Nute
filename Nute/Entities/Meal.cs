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
        public List<Serving> Servings { get; }
          = new List<Serving>();
        public Meal(MealTime mealTime, long id = 0)
        {
            Id = id;
            MealTime = mealTime;
        }        
    }
}