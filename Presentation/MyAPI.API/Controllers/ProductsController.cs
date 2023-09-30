using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MyAPI.Application.Abstractions;
using MyAPI.Domain.Dto;
using MyAPI.Domain.Entities;
using MyAPI.Infrastructure.Services;
using System;
using System.Threading.Tasks;

namespace MyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [OutputCache]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _mapper = mapper;
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(PostProductDto postProduct)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Geçersiz ürün verisi.");

                var product = _mapper.Map<Product>(postProduct);
                product.Id = Guid.NewGuid();
                product.CreatedTime = DateTime.Now;
                product.UpdatedDate = DateTime.Now;
                var result = await _productService.AddAsync(product);
                if (result)
                {
                    await _productService.SaveAsync(product);
                    return Ok("Ürün başarıyla eklendi.");
                }
                else
                    return BadRequest("Ürün eklenirken bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hata: {ex.Message}");
            }
        }
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                var products = _mapper.Map<List<GetProductDto>>(_productService.GetAll());
                if (products.Count > 0)
                    return Ok(products);
                else
                    return NotFound("Hiçbir ürün bulunamadı.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hata: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                if (product != null)
                    return Ok(product);
                else
                    return NotFound("Belirtilen ID'ye sahip ürün bulunamadı.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hata: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] PostProductDto PutProduct)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Geçersiz müşteri verisi.");

                var existingProduct = _mapper.Map<Product>(PutProduct);
                if (existingProduct == null)
                    return NotFound("Belirtilen ID'ye sahip müşteri bulunamadı.");

                // Güncelleme işlemleri burada gerçekleştirilir
                existingProduct.Name = PutProduct.Name;
                existingProduct.UpdatedDate = DateTime.Now;
                var result = _productService.Update(existingProduct);
                if (result)
                {
                    await _productService.SaveAsync(existingProduct);
                    return Ok("Ürün başarıyla güncellendi.");
                }
                    
                else
                    return NotFound("Belirtilen ID'ye sahip ürün bulunamadı.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hata: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            try
            {
                var existingProduct = await _productService.GetByIdAsync(id);
                if (existingProduct==null)
                {
                    return NotFound("Belirtilen ID'ye sahip ürün bulunamadı.");
                }
                var result = await _productService.RemoveAsync(id);
                if (result)
                {
                    await _productService.SaveAsync(existingProduct);
                    return Ok("Ürün başarıyla silindi");
                }
                return BadRequest("Ürün silinirken bir hata oluştu.");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hata: {ex.Message}");
            }
        }
    }
}
