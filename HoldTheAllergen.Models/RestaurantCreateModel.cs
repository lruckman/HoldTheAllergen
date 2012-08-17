using System.ComponentModel.DataAnnotations;
using System.Web;

namespace HoldTheAllergen.Models
{
    public class RestaurantCreateModel
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public HttpPostedFileBase Logo { get; set; }
    }
}