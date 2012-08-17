using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HoldTheAllergen.Models
{
    public class MenuItemCreateModel
    {
        [HiddenInput(DisplayValue = false)]
        [Required]
        public int RestaurantId { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string GroupName { get; set; }

        [Display(Name = "Ingredients")]
        [Required]
        public int[] IngredientIds { get; set; }

        public MultiSelectList IngredientList { get; set; }


    }
}