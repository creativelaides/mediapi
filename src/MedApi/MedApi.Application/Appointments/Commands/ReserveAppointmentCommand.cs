using MedApi.Application.Interfaces;
using MedApi.Domain.Exceptions;
using MedApi.Domain.ObjectsValues;
using MediatR;


namespace MedApi.Application.Appointments.Commands;

public record ReserveAppointmentCommand : IRequest<bool>
{
    public Guid AppointmentId { get; }
    public string PatientIdentifier { get; }

    public ReserveAppointmentCommand(Guid appointmentId, string patientIdentifier)
    {
        AppointmentId = appointmentId;
        PatientIdentifier = patientIdentifier;
    }
}

public class ReserveAppointmentCommandHandler : IRequestHandler<ReserveAppointmentCommand, bool>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReserveAppointmentCommandHandler(
        IAppointmentRepository appointmentRepository,
        IPatientRepository patientRepository,
        IUnitOfWork unitOfWork)
    {
        _appointmentRepository = appointmentRepository;
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(ReserveAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _appointmentRepository.GetByIdWithDetailsAsync(request.AppointmentId);
        var patient = await _patientRepository.GetByIdentifierAsync(request.PatientIdentifier);

        if (appointment == null || patient == null || appointment.Status != Status.Available)
            return false;

        try
        {
            appointment.AssignPatient(patient);
            appointment.Reserve();

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (ConcurrencyException) // Capturamos nuestra propia excepción
        {
            // Loggear el error aquí
            return false;
        }

    }
}