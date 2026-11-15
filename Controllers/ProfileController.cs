using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthTemp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProfileController : Controller
    {
        [HttpGet("me")] 
        public IActionResult GetMyProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var email = User.FindFirstValue(ClaimTypes.Email);
            var FullName = User.FindFirstValue("FullName");
            var roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value);

            return Ok(new { Id = userId, Email = email, FullName =  FullName, Roles = roles });
        }

        [HttpGet("admin-dashboard")]
        [Authorize(Roles = "admin")]
        public IActionResult GetAdminStates()
        {
            return Ok(new { SecretData = "Secret data" });
        }
    }
}
