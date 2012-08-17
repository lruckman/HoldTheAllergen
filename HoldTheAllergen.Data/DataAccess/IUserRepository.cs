using System;
using HoldTheAllergen.Data.Models;

namespace HoldTheAllergen.Data.DataAccess
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUser(Guid userId);
    }
}