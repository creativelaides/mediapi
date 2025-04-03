using MedApi.Application.Interfaces;
using MediatR;

namespace MedApi.Application.Appointments.Commands;

public record CancelAppointmentCommand : IRequest<bool>
{
    public Guid AppointmentId { get; }
    public string PatientIdentifier { get; }

    public CancelAppointmentCommand(Guid appointmentId, string patientIdentifier)
    {
        AppointmentId = appointmentId;
        PatientIdentifier = patientIdentifier;
    }
}

public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand, bool>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork; // Añadir UnitOfWork

    public CancelAppointmentCommandHandler(
        IAppointmentRepository appointmentRepository,
        IPatientRepository patientRepository,
        IUnitOfWork unitOfWork) // Inyectar UnitOfWork
    {
        _appointmentRepository = appointmentRepository;
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(request.AppointmentId);
        if (appointment == null || appointment.Patient?.Identifier != request.PatientIdentifier)
            return false;

        appointment.Cancel();
        _appointmentRepository.Update(appointment); // Cambiar a Update síncrono
        await _unitOfWork.SaveChangesAsync(); // Guardar cambios
        return true;
    }
}