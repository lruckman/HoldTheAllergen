using System;
using System.Linq;
using HoldTheAllergen.Data.Models;
using System.Data.Entity;

namespace HoldTheAllergen.Data.DataAccess.Impl
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context)
            : base(context)
        {
        }

        public User GetUser(Guid id)
        {
            var user =
                Context.Set<User>()
                    .Include(x => x.Allergies)
                    .Include(x => x.StarredMenuItems)
                    .FirstOrDefault(u => u.Id == id);

            if (user != null)
            {
                user.LastActivity = DateTime.UtcNow;
                Context.SaveChanges();
            }
            else
            {
                user = Create(new User { CreateDate = DateTime.Now, LastActivity = DateTime.UtcNow, Id = id });
            }

            return user;
        }
    }
}