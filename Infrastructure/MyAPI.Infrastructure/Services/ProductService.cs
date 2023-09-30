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
    public class ProductService : IProductService   
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        public ProductService(IProductReadRepository productReadRepository,
            IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public Task<bool> AddAsync(Product model)
        {
            return _productWriteRepository.AddAsync(model);
        }

        public Task<bool> AddRangeAsync(List<Product> datas)
        {
            return _productWriteRepository.AddRangeAsync(datas);
        }

        public IQueryable<Product> GetAll()
        {
           return _productReadRepository.GetAll();
        }

        public Task<Product> GetByIdAsync(string id)
        {
            return _productReadRepository.GetByIdAsync(id);
        }

        public Task<Product> GetSingleAsync(Expression<Func<Product, bool>> method)
        {
            return _productReadRepository.GetSingleAsync(method);
        }

        public IQueryable<Product> GetWhere(Expression<Func<Product, bool>> method)
        {
            return _productReadRepository.GetWhere(method);
        }

        public bool Remove(Product model)
        {
            return _productWriteRepository.Remove(model);
        }

        public Task<bool> RemoveAsync(string id)
        {
            return _productWriteRepository.RemoveAsync(id);
        }

        public bool RemoveRange(List<Product> datas)
        {
            return _productWriteRepository.RemoveRange(datas);
        }

        public Task<int> SaveAsync(Product model)
        {
            return _productWriteRepository.SaveAsync(model);
        }

        public bool Update(Product model)
        {
            return _productWriteRepository.Update(model);
        }
    }
}
