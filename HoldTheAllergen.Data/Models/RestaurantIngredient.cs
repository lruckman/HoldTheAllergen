using System.Linq;

namespace HoldTheAllergen.Data.Models
{
    public partial class RestaurantIngredient
    {
        public string DescriptiveName()
        {
            var allergens = string.Join(",", Allergens.Select(allergen => allergen.Name));

            return string.IsNullOrWhiteSpace(allergens) ? Name : string.Format("{0} ({1})", Name, allergens);
        }
    }
}