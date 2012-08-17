using System.Collections;
using System.Linq;
using System.Web.Mvc;
using HoldTheAllergen.Data.DataAccess;

namespace HoldTheAllergen.Backend.Core.Providers
{
    public class AllergenMultiSelectListProvider
    {
        private readonly IAllergenRepository _allergenRepository;

        public AllergenMultiSelectListProvider(IAllergenRepository allergenRepository)
        {
            _allergenRepository = allergenRepository;
        }

        public MultiSelectList Provide(IEnumerable selected)
        {
            var allergens =
                _allergenRepository.All().OrderBy(allergen => allergen.Name).Select(
                    allergen => new {allergen.Id, allergen.Name});
            return new MultiSelectList(allergens.ToArray(), "Id", "Name", selected);
        }
    }
}