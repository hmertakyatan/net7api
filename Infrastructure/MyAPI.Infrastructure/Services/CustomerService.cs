using MyAPI.Application.Abstractions;
using MyAPI.Application.Repositories;
using MyAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyAPI.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerWriteRepository _customerWriteRepository;
        private readonly ICustomerReadRepository _customerReadRepository;

        public CustomerService(ICustomerReadRepository customerReadRepository,
            ICustomerWriteRepository customerWriteRepository)
        {
            _customerReadRepository = customerReadRepository;
            _customerWriteRepository = customerWriteRepository;
        }

        public Task<bool> AddAsync(Customer model)
        {
            return _customerWriteRepository.AddAsync(model);
        }

        public Task<bool> AddRangeAsync(List<Customer> datas)
        {
            return _customerWriteRepository.AddRangeAsync(datas);
        }

        public IQueryable<Customer> GetAll()
        {
            return _customerReadRepository.GetAll();
        }

        public Task<Customer> GetByIdAsync(string id)
        {
            return _customerReadRepository.GetByIdAsync(id);
        }

        public Task<Customer> GetSingleAsync(Expression<Func<Customer, bool>> method)
        {
            return _customerReadRepository.GetSingleAsync(method);
        }

        public IQueryable<Customer> GetWhere(Expression<Func<Customer, bool>> method)
        {
            return _customerReadRepository.GetWhere(method);
        }

        public bool Remove(Customer model)
        {
            return _customerWriteRepository.Remove(model);
        }

        public Task<bool> RemoveAsync(string id)
        {
            return _customerWriteRepository.RemoveAsync(id);
        }

        public bool RemoveRange(List<Customer> datas)
        {
            return _customerWriteRepository.RemoveRange(datas);
        }

        public Task<int> SaveAsync(Customer model)
        {
            return _customerWriteRepository.SaveAsync(model);
        }

        public bool Update(Customer model)
        {
            return _customerWriteRepository.Update(model);
        }
    }
}



