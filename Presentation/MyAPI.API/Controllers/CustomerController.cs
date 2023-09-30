using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using MyAPI.Application.Abstractions;
using MyAPI.Domain.Entities;
using AutoMapper;
using MyAPI.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.OutputCaching;

namespace MyAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [OutputCache]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomersController> _logger;
       

        public CustomersController(ICustomerService customerService, ILogger<CustomersController> logger, IMapper mapper)
        {
            _customerService = customerService;
            _logger = logger;
            _mapper = mapper;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            try
            {
                var customers = _customerService.GetAll();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(string id)
        {
            try
            {
                var customer = await _customerService.GetByIdAsync(id);
                if (customer == null)
                    return NotFound("Belirtilen ID'ye sahip müşteri bulunamadı.");

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerDto postCustomer)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Geçersiz müşteri verisi.");

                var customer = _mapper.Map<Customer>(postCustomer);
                customer.Id = Guid.NewGuid();
                customer.CreatedTime = DateTime.Now;
                customer.UpdatedDate = DateTime.Now;

                var result = await _customerService.AddAsync(customer);
                if (result)
                {
                    await _customerService.SaveAsync(customer);
                    return Ok("Müşteri başarıyla oluşturuldu.");
                }
                else
                {
                    return BadRequest("Müşteri oluşturulurken bir hata oluştu.");
                }
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(string id, [FromBody] CustomerDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Geçersiz müşteri verisi.");

                var existingCustomer = await _customerService.GetByIdAsync(id);
                if (existingCustomer == null)
                    return NotFound("Belirtilen ID'ye sahip müşteri bulunamadı.");

                // Güncelleme işlemleri burada gerçekleştirilir
                existingCustomer.Name = model.Name;
                existingCustomer.Mail = model.Mail;
                existingCustomer.Phone = model.Phone;
                existingCustomer.UpdatedDate = DateTime.Now;
               

                await _customerService.SaveAsync(existingCustomer);

                return Ok("Müşteri başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            try
            {
                var existingCustomer = await _customerService.GetByIdAsync(id);
                if (existingCustomer == null)
                    return NotFound("Belirtilen ID'ye sahip müşteri bulunamadı.");

                var result = await _customerService.RemoveAsync(id);
                if (result)
                {
                    await _customerService.SaveAsync(existingCustomer);
                    return Ok("Müşteri başarıyla silindi.");
                }

                return BadRequest("Müşteri silinirken bir hata oluştu.");
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        private IActionResult HandleError(Exception ex)
        {
            _logger.LogError(ex, $"Hata: {ex.Message}");
            return StatusCode(500, "Bir sunucu hatası oluştu.");
        }
    }
}