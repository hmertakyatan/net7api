using MyAPI.Application.Repositories;
using MyAPI.Domain.Entities;
using MyAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Persistence.Repositories
{
    public class CustomerReadRepository : ReadRepository<Customer>, ICustomerReadRepository
    {
        public CustomerReadRepository(MyAPIDbContext context) : base(context)
        {
        }
    }
}
