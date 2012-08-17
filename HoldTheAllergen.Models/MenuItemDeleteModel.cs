using System.Web.Mvc;

namespace HoldTheAllergen.Models
{
    public class MenuItemDeleteModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
    }
}