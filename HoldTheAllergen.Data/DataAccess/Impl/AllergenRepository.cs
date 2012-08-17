using System.Collections.Generic;
using System.Linq;
using HoldTheAllergen.Data.Models;

namespace HoldTheAllergen.Data.DataAccess.Impl
{
    public class AllergenRepository : Repository<Allergen>, IAllergenRepository
    {
        private readonly HoldTheAllergenEntities _context;

        public AllergenRepository(HoldTheAllergenEntities context)
            : base(context)
        {
            _context = context;
        }

        public IEnumerable<Allergen> GetAllergens()
        {
            return _context.Allergens.OrderBy(allergen => allergen.Name).ToArray();
        }
    }
}