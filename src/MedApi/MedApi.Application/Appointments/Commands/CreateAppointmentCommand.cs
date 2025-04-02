using MedApi.Domain;
using MedApi.Application.Interfaces;
using MediatR;

namespace MedApi.Application.Appointments.Commands;

public record CreateAppointmentCommand : IRequest<Guid>
{
    public DateTime AppointmentDate { get; }
    public Guid DoctorId { get; }
    public Guid PatientId { get; }

    public CreateAppointmentCommand(DateTime appointmentDate, Guid doctorId, Guid patientId)
    {
        AppointmentDate = appointmentDate;
        DoctorId = doctorId;
        PatientId = patientId;
    }
}

public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, Guid>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IPatientRepository _patientRepository;

    public CreateAppointmentCommandHandler(
        IAppointmentRepository appointmentRepository,
        IDoctorRepository doctorRepository,
        IPatientRepository patientRepository)
    {
        _appointmentRepository = appointmentRepository;
        _doctorRepository = doctorRepository;
        _patientRepository = patientRepository;
    }

    public async Task<Guid> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
        var patient = await _patientRepository.GetByIdentifierAsync(request.PatientId.ToString());

        if (doctor is null || patient is null)
            throw new ArgumentException("Doctor or Patient not found");

        var appointment = new Appointment(request.AppointmentDate, doctor, patient);

        await _appointmentRepository.AddAsync(appointment);

        return appointment.Id;
    }
}
