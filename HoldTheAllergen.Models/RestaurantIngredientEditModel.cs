using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HoldTheAllergen.Models
{
    public class RestaurantIngredientEditModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(4000)]
        public string Description { get; set; }

        [Display(Name = "Allergens")]
        public int[] AllergenIds { get; set; }

        public MultiSelectList AllergenList { get; set; }
    }
}