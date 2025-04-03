using System.IdentityModel.Tokens.Jwt;
using MedApi.API.DTOs;
using MedApi.Application.Appointments.Commands;
using MedApi.Application.Appointments.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AppointmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AppointmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("available")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAvailableAppointments([FromQuery] string specialty)
    {
        var query = new GetAvailableAppointmentsQuery(specialty);
        var appointments = await _mediator.Send(query);

        var response = appointments.Select(a => new AppointmentResponse(
            a.Id,
            a.AppointmentDate,
            a.Specialty!,
            a.Status.ToString(),
            $"{a.Doctor?.FirstName} {a.Doctor?.LastName}",
            a.Doctor?.Specialization ?? string.Empty));

        return Ok(response);
    }

    [HttpPost("reserve")]
    public async Task<IActionResult> ReserveAppointment([FromBody] ReserveAppointmentRequest request)
    {
        var patientIdentifier = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (string.IsNullOrEmpty(patientIdentifier))
            return Unauthorized();

        var command = new ReserveAppointmentCommand(request.AppointmentId, patientIdentifier);
        var result = await _mediator.Send(command);

        if (!result)
            return BadRequest("Appointment not available");

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentRequest request)
    {
        var command = new CreateAppointmentCommand(
            request.AppointmentDate,
            request.Specialty,
            request.DoctorId,
            Guid.NewGuid()); // In a real app, you'd get the patient ID from the token

        var appointmentId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAvailableAppointments), new { id = appointmentId }, null);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelAppointment(Guid id)
    {
        var patientIdentifier = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (string.IsNullOrEmpty(patientIdentifier))
            return Unauthorized();

        var command = new CancelAppointmentCommand(id, patientIdentifier);
        var result = await _mediator.Send(command);

        if (!result)
            return BadRequest("Appointment not found or not yours to cancel");

        return NoContent();
    }
}