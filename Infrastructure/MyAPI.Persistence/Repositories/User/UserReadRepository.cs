using MyAPI.Domain.Entities;
using MyAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Persistence.Repositories
{
    public class UserReadRepository : ReadRepository<User>
    {
        public UserReadRepository(MyAPIDbContext context) : base(context)
        {
        }
    }
}
