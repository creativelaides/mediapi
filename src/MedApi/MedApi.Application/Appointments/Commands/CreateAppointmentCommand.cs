using MedApi.Domain;
using MedApi.Application.Interfaces;
using MediatR;

namespace MedApi.Application.Appointments.Commands;

public record CreateAppointmentCommand : IRequest<AppointmentResult>
{
    public DateTime AppointmentDate { get; init; }
    public string Specialty { get; init; }
    public Guid DoctorId { get; init; }
    public Guid PatientId { get; init; }

    public CreateAppointmentCommand(DateTime appointmentDate, string specialty, Guid doctorId, Guid patientId)
    {
        AppointmentDate = appointmentDate;
        Specialty = specialty;
        DoctorId = doctorId;
        PatientId = patientId;
    }
}

public record AppointmentResult(Guid Id, DateTime AppointmentDate, string Specialty, Guid DoctorId, Guid PatientId);

public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, AppointmentResult>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAppointmentCommandHandler(
        IAppointmentRepository appointmentRepository,
        IDoctorRepository doctorRepository,
        IPatientRepository patientRepository,
        IUnitOfWork unitOfWork)
    {
        _appointmentRepository = appointmentRepository;
        _doctorRepository = doctorRepository;
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<AppointmentResult> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
        var patient = await _patientRepository.GetByIdAsync(request.PatientId);

        if (doctor is null)
            throw new ArgumentException("Doctor not found");

        if (patient is null)
            throw new ArgumentException("Patient not found");

        if (string.IsNullOrWhiteSpace(request.Specialty))
            throw new ArgumentException("Specialty is required");

        if (request.AppointmentDate < DateTime.UtcNow.AddMinutes(5))
            throw new ArgumentException("Appointment date must be in the future");

        var appointment = new Appointment(
            request.AppointmentDate,
            request.Specialty,
            doctor,
            patient);

        await _appointmentRepository.AddAsync(appointment);
        await _unitOfWork.SaveChangesAsync();

        return new AppointmentResult(
            appointment.Id,
            appointment.AppointmentDate,
            appointment.Specialty!,
            appointment.DoctorId,
            appointment.PatientId);
    }
}