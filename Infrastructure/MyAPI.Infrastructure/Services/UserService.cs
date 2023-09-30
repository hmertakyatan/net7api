using MyAPI.Application.Abstractions;
using MyAPI.Application.Repositories;
using MyAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUserReadRepository _userReadRepository;

        public UserService(IUserReadRepository userReadRepository,
            IUserWriteRepository userWriteRepository)
        {
            _userReadRepository = userReadRepository;
            _userWriteRepository = userWriteRepository;
        }
        public Task<bool> AddAsync(User model)
        {
            return _userWriteRepository.AddAsync(model);
        }

        public Task<bool> AddRangeAsync(List<User> datas)
        {
            return _userWriteRepository.AddRangeAsync(datas);
        }

        public IQueryable<User> GetAll()
        {
            return _userReadRepository.GetAll();
        }

        public Task<User> GetByIdAsync(string id)
        {
            return _userReadRepository.GetByIdAsync(id);
        }

        public Task<User> GetSingleAsync(Expression<Func<User, bool>> predicate)
        {
            return _userReadRepository.GetSingleAsync(predicate);
        }

        public IQueryable<User> GetWhere(Expression<Func<User, bool>> predicate)
        {
            return _userReadRepository.GetWhere(predicate);
        }

        public bool Remove(User model)
        {
            return _userWriteRepository.Remove(model);
        }

        public Task<bool> RemoveAsync(string id)
        {
            return _userWriteRepository.RemoveAsync (id);
        }

        public bool RemoveRange(List<User> datas)
        {
            return (_userWriteRepository.RemoveRange(datas));
        }

        public void RoleAssignment(string id, string role)
        {
            _userWriteRepository.RoleAssignment(id, role);
        }

        public Task<int> SaveAsync(User model)
        {
            return _userWriteRepository.SaveAsync (model);
        }

        public bool Update(User model)
        {
            return _userWriteRepository.Update (model);
        }
    }
}
