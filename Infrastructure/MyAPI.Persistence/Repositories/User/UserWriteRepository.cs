using MyAPI.Application.Repositories;
using MyAPI.Domain.Entities;
using MyAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Persistence.Repositories
{
    public class UserWriteRepository : WriteRepository<User>, IUserWriteRepository
    {
        public UserWriteRepository(MyAPIDbContext context) : base(context)
        {
            
        }

        public async Task RoleAssignment(string id, string role)
        {
            var user =  Table.FirstOrDefault(data => data.Id == Guid.Parse(id));

            if (user != null)
            {
                user.Role = role;
                await _context.SaveChangesAsync();
            }
        }
    }

}
