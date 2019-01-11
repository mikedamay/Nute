using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Nute.Entities
{
    public class ScheduledMeal
    {
        public long Id { get; private set; }
        [Column(TypeName = "date")]
        public DateTime EatenOn { get; private set; }
        public bool Eaten { get; private set; }
        [Required]
        public Meal Meal { get; private set; }

        public ScheduledMeal()
        {
            
        }
        public ScheduledMeal(Meal meal, DateTime eatenOn, bool eaten, long id = 0)
        {
            Debug.Assert(eatenOn.TimeOfDay == TimeSpan.Zero);
                // date onlyh;
            Id = id;
            Meal = meal;
            EatenOn = eatenOn;
            Eaten = eaten;
        }
    }
}