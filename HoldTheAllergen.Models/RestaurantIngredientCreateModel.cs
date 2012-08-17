using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HoldTheAllergen.Models
{
    public class RestaurantIngredientCreateModel : IValidatableObject
    {
        [HiddenInput(DisplayValue = false)]
        public int RestaurantId { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(4000)]
        public string Description { get; set; }

        [Display(Name = "Allergens")]
        public int[] AllergenIds { get; set; }

        public MultiSelectList AllergenList { get; set; }

        public bool CreateMenuItem { get; set; }

        [StringLength(50)]
        public string GroupName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (CreateMenuItem && string.IsNullOrWhiteSpace(GroupName))
            {
                yield return
                    new ValidationResult("Group Name is required for menu items.", new[] { "GroupName" })
                    ;
            }
        }
    }
}