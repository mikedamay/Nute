using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Nute.Entities
{
    public class Nutrient
    {
        public Nutrient()
        {
            
        }
        public Nutrient( string name, int id = 0)
        {
            Id = id;
            Name = name;
        }
        public long Id { get; private set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; private set; }
    }
}