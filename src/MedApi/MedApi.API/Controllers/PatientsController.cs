using MedApi.API.DTOs;
using MedApi.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MedApi.API.Controllers;

[ApiController]
[Route("api/patients")]
[Authorize]
public class PatientsController : ControllerBase
{
    private readonly IPatientRepository _patientRepository;

    public PatientsController(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetPatientInfo()
    {
        var patientId = User.FindFirstValue("patientId");
        if (!Guid.TryParse(patientId, out var id))
            return Unauthorized();

        var patient = await _patientRepository.GetPatientWithAppointmentsAsync(id);
        if (patient == null)
            return NotFound();

        var response = new PatientDetailsResponse(
            patient.Id,
            $"{patient.FirstName} {patient.LastName}",
            patient.Identifier!,
            patient.Email!,
            patient.DateOfBirth,
            patient.PhoneNumber!,
            patient.Appointments.Select(a => new AppointmentResponse(
                a.Id,
                a.AppointmentDate,
                a.Specialty!,
                a.Status.ToString(),
                $"{a.Doctor?.FirstName} {a.Doctor?.LastName}",
                a.Doctor?.Specialization ?? string.Empty))
            .ToList());

        return Ok(response);
    }
}

public record PatientDetailsResponse(
    Guid Id,
    string FullName,
    string Identifier,
    string Email,
    DateOnly DateOfBirth,
    string PhoneNumber,
    List<AppointmentResponse> Appointments);