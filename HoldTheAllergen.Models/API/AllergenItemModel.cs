namespace HoldTheAllergen.Models.API
{
    public class AllergenItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
        public string SelectAction { get; set; }
    }
}