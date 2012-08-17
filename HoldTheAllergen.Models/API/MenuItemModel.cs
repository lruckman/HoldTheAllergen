namespace HoldTheAllergen.Models.API
{
    public class MenuItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AllergenNames { get; set; }
        public bool Starred { get; set; }
        public string StarAction { get; set; }
        public string Action { get; set; }
    }
}