using Microsoft.AspNetCore.Mvc;
using WebApplication1.Extentions;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

/// <summary>
/// API-kontroller för användarregistrering.
/// </summary>
[ApiKey]
[Route("api/[controller]")]
[ApiController]
public class SignUpController(ISignUpService signUpService) : ControllerBase
{
    private readonly ISignUpService signUpService = signUpService;

    /// <summary>
    /// Registrerar en ny användare.
    /// </summary>
    /// <param name="formData">Formulärdata innehållande e-postadress och lösenord.</param>
    /// <returns>
    /// Ett <see cref="SignUpResult"/>-objekt som visar resultatet av registreringen.
    /// Returnerar HTTP 200 om registreringen lyckas, annars 400 med felmeddelande.
    /// </returns>
    /// <response code="200">Användaren registrerades framgångsrikt.</response>
    /// <response code="400">Fel i formuläret eller registreringen misslyckades.</response>
    [HttpPost("register")]
    [ProducesResponseType(typeof(SignUpResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SignUpResult), StatusCodes.Status400BadRequest)]
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
