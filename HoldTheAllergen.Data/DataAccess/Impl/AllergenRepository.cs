using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using HoldTheAllergen.Data.Models;

namespace HoldTheAllergen.Data.DataAccess.Impl
{
    public class AllergenRepository : Repository<Allergen>, IAllergenRepository
    {

        public AllergenRepository(DbContext context)
            : base(context)
        {
        }

        public IEnumerable<Allergen> GetAllergens()
        {
            return Context.Set<Allergen>()
                .OrderBy(allergen => allergen.Name)
                .ToArray();
        }
    }
}