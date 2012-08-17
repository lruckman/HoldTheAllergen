namespace HoldTheAllergen.Models
{
    public class RestaurantIngredientModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string AllergenNames { get; set; }
    }
}