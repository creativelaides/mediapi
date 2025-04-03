using MedApi.API.DTOs;
using MedApi.Application.Interfaces;
using MedApi.Domain;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MedApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IPatientRepository _patientRepository;
    private readonly IConfiguration _configuration;

    public AuthController(
        IPatientRepository patientRepository,
        IConfiguration configuration)
    {
        _patientRepository = patientRepository;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var patient = await _patientRepository.AuthenticateAsync(
            request.Identifier, 
            request.DateOfBirth
        );

        if (patient == null)
            return Unauthorized("Invalid credentials");

        var token = GenerateJwtToken(patient);
        return Ok(new LoginResponse(token, $"{patient.FirstName} {patient.LastName}", patient.Identifier!));
    }

    private string GenerateJwtToken(Patient patient)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:secretKey"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, patient.Identifier!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("patientId", patient.Id.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:validIssuer"],
            audience: _configuration["JwtSettings:validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:expiryInMinutes"])),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}