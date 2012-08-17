using System.Collections.Generic;

namespace HoldTheAllergen.Models
{
    public class MenuItemModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> Ingredients { get; set; }

        public IEnumerable<string> Allergens { get; set; }
    }
}