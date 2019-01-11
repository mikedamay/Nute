using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Nute.Entities
{
    public enum MealTimeValueLongevity { System = 1, Temporary = 2 }
    public class MealTime
    {
        public MealTime()
        {
            
        }
        public long Id { get; private set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; private set; }
        public MealTimeValueLongevity Longevity { get; private set; } 
        public MealTime(string name, MealTimeValueLongevity longevity, long id = 0)
        {
            Id = id;
            Name = name;
            Longevity = longevity;
        }
    }
}