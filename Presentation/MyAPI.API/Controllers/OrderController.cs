using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyAPI.Application.Abstractions;
using MyAPI.Domain.Entities;
using MyAPI.Domain.Dto;
using AutoMapper;
using MyAPI.Controllers;
using MyAPI.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OutputCaching;

namespace MyAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [OutputCache]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        public OrdersController(IOrderService orderService, IMapper mapper, IProductService productService)
        {
            _productService = productService;
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            try
            {
                var orders = _orderService.GetAll();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hata: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(string id)
        {
            try
            {
                var order = await _orderService.GetByIdAsync(id);
                if (order == null)
                    return NotFound("Belirtilen ID'ye sahip sipariş bulunamadı.");

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hata: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            try
            {
                
                var result = await _orderService.AddAsync(order);
                if (result)
                {
                    await _orderService.SaveAsync(order);
                    return Ok("ok");
                }
                else 
                    return BadRequest("not ok");
            }
            catch (Exception ex)
            {
                return BadRequest($"Sipariş oluşturulurken bir hata oluştu: {ex.Message}");
            }
            
        }


        [HttpPut("{id}")]
        public IActionResult UpdateOrder(string id, [FromBody] Order order)
        {
            try
            {
                if (id != order.Id.ToString())
                    return BadRequest("İstenen ID ile sipariş ID'si uyuşmuyor.");

                var result = _orderService.Update(order);
                if (result)
                    return Ok("Sipariş başarıyla güncellendi.");
                else
                    return NotFound("Belirtilen ID'ye sahip sipariş bulunamadı.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hata: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            try
            {
                var result = await _orderService.RemoveAsync(id);
                if (result)
                    return Ok("Sipariş başarıyla silindi.");
                else
                    return NotFound("Belirtilen ID'ye sahip sipariş bulunamadı.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hata: {ex.Message}");
            }
        }
    }
}
