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
    public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
    {
        public ProductWriteRepository(MyAPIDbContext context) : base(context)
        {
        }
    }
}
