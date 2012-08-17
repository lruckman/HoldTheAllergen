using System.Web.Mvc;

namespace HoldTheAllergen.Models
{
    public class RestaurantDeleteModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
    }
}