using System;
using MyAPI.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Application.Repositories
{
    public interface IUserReadRepository : IReadRepository<User>
    {
    }
}
