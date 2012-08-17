using System.Web.Mvc;

namespace HoldTheAllergen.Models
{
    public class RestaurantIngredientDeleteModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
    }
}