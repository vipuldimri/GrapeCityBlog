using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrapeCityBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationManager _authManager;

        public UsersController(UserManager<User> userManager, IAuthenticationManager authManager)
        {

            _userManager = userManager;
            _authManager = authManager;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginDTO([FromBody] LoginDTO loginDTO)
        {
            if (!await _authManager.ValidateUser(loginDTO))
            {
                return Unauthorized(_authManager.GetMessage());
            }
            var user = _authManager.getUser();
            return Ok(new {  Token = await _authManager.CreateToken() });
        }
    }
}
