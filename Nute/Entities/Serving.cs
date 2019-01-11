using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Logging;

namespace Nute.Entities
{
    public class Serving
    {
        public Serving()
        {
            
        }

        public Serving(Ingredient ingredient, Quantity quantity, long id = 0L)
        {
            Id = id;
            Ingredient = ingredient;
            Quantity = quantity;
        }
        public long Id { get; private set; }
        [Required]
        public Ingredient Ingredient { get; private set; }
        
        #region Quantity
        [NotMapped]
        public Quantity Quantity
        {
            get => new Quantity(count: _quantityCount, unit:  _quantityUnit);
            set
            {
                _quantityCount = value.Count;
                _quantityUnit = value.Unit;
            }
        }
        private decimal _quantityCount;
#pragma warning disable 169
        private long _quantityUnitId;
#pragma warning restore 169
        private Unit _quantityUnit;
        #endregion
    }
}