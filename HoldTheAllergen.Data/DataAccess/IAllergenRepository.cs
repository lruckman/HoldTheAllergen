using System.Collections.Generic;
using HoldTheAllergen.Data.Models;

namespace HoldTheAllergen.Data.DataAccess
{
    public interface IAllergenRepository : IRepository<Allergen>
    {
        IEnumerable<Allergen> GetAllergens();

    }
}