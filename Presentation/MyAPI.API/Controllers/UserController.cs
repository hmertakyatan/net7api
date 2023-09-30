using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MyAPI.Application.Abstractions;
using MyAPI.Domain.Entities;
using MyAPI.Infrastructure.Services;

namespace MyAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [OutputCache]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;
        public UserController(IUserService userService, ILogger logger)
        {
            _userService = userService;

            _logger = logger;

        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                var users = _userService.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                var user = _userService.GetByIdAsync(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> PostUser(User user)
        {
            try
            {

                var result = await _userService.AddAsync(user);
                if (result)
                {
                    await _userService.SaveAsync(user);
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Error");
                }
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id)
        {
            try
            {
                var existingUser = await _userService.GetByIdAsync(id);
                if (existingUser == null)
                {
                    return BadRequest("User not found or no exist.");
                }
                var result = _userService.Update(existingUser);
                if (result)
                {
                    await _userService.SaveAsync(existingUser);
                    return Ok("User is updated succesfuly.");
                }
                else
                    return BadRequest("Error");

            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var existingUser = await _userService.GetByIdAsync(id);
                if (existingUser == null)
                {
                    return NotFound("User not found or no exist");
                }
                var result = await _userService.RemoveAsync(id);
                if (result)
                {
                    await _userService.SaveAsync(existingUser);
                    return Ok("User is deleted succesfuly.");
                }
                return BadRequest("Error.");
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("Changerole {id}")]
        public async Task<IActionResult> ChangeRole(string id, string role)
        {
            try
            {
                var existingUser = await _userService.GetByIdAsync(id);
                if (existingUser == null)
                {
                    return NotFound("User not found or not exist");
                }

                // Update user's role directly using string 'id'
                 _userService.RoleAssignment(id, role);

                return Ok("Role changed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private IActionResult HandleError(Exception ex)
        {
            _logger.LogError(ex, $"Hata: {ex.Message}");
            return StatusCode(500, "Bir sunucu hatası oluştu.");
        }
    }

}
