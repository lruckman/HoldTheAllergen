using System;
using System.Linq;
using HoldTheAllergen.Data.Models;

namespace HoldTheAllergen.Data.DataAccess.Impl
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly HoldTheAllergenEntities _context;

        public UserRepository(HoldTheAllergenEntities context)
            : base(context)
        {
            _context = context;
        }

        public User GetUser(Guid userId)
        {
            return
                _context.Users.Include("Allergies").Include("StarredMenuItems").FirstOrDefault(user => user.Id == userId);
        }
    }
}