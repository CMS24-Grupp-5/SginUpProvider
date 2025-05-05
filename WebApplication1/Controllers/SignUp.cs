using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController(ISignUpService signUpService) : ControllerBase
    {

        private readonly ISignUpService signUpService = signUpService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] SignUpFormData formData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await signUpService.SignUpAsync(formData);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
