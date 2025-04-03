namespace MedApi.API.DTOs;

public record AppointmentResponse(
    Guid Id,
    DateTime AppointmentDate,
    string Specialty,
    string Status,
    string DoctorName,
    string DoctorSpecialization);

public record CreateAppointmentRequest(
    DateTime AppointmentDate,
    string Specialty,
    Guid DoctorId);

public record ReserveAppointmentRequest(Guid AppointmentId);