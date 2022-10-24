using System.Xml.Linq;
using EcoGasBackend.Models;
using EcoGasBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcoGasBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService) =>
            _userService = userService;

        [HttpGet]
        public async Task<List<User>> Get() =>
            await _userService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> Get(string id)
        {
            var user = await _userService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            await _userService.CreateAsync(user);

            return CreatedAtAction(nameof(Get), new { userName = user.UserName }, user);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult<User>> Update(string id, User updatedUser)
        {
            var user = await _userService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            user.Name = updatedUser.Name;
            user.UserName = updatedUser.UserName;

            await _userService.UpdateAsync(id, user);

            return user;
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            await _userService.RemoveAsync(id);

            return NoContent();
        }
    }
}
