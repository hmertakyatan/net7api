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
    public class OrderService : IOrderService
    {
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IOrderReadRepository _orderReadRepository;

        public OrderService(IOrderReadRepository orderReadRepository,
            IOrderWriteRepository orderWriteRepository)
        {
            _orderReadRepository = orderReadRepository;
            _orderWriteRepository = orderWriteRepository;
        }

        public Task<bool> AddAsync(Order model)
        {
            return _orderWriteRepository.AddAsync(model);
        }

        public Task<bool> AddRangeAsync(List<Order> datas)
        {
            return _orderWriteRepository.AddRangeAsync(datas);
        }

        public IQueryable<Order> GetAll()
        {
            return _orderReadRepository.GetAll();
        }

        public Task<Order> GetByIdAsync(string id)
        {
            return _orderReadRepository.GetByIdAsync(id);
        }

        public Task<Order> GetSingleAsync(Expression<Func<Order, bool>> method)
        {
            return _orderReadRepository.GetSingleAsync(method);
        }

        public IQueryable<Order> GetWhere(Expression<Func<Order, bool>> method)
        {
            return _orderReadRepository.GetWhere(method);
        }
        public bool Remove(Order model)
        {
            return _orderWriteRepository.Remove(model);
        }

        public Task<bool> RemoveAsync(string id)
        {
            return _orderWriteRepository.RemoveAsync(id);
        }

        public bool RemoveRange(List<Order> datas)
        {
            return _orderWriteRepository.RemoveRange(datas);
        }

        public Task<int> SaveAsync(Order model)
        {
            return _orderWriteRepository.SaveAsync(model);
        }

        public bool Update(Order model)
        {
            return _orderWriteRepository.Update(model);
        }
    }
}