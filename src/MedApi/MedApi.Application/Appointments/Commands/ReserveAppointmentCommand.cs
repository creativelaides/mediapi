using MedApi.Application.Interfaces;
using MedApi.Domain;
using MediatR;

namespace MedApi.Application.Appointments.Commands;

public record ReserveAppointmentCommand : IRequest<bool>
{
    public Guid AppointmentId { get; }

    public ReserveAppointmentCommand(Guid appointmentId)
    {
        AppointmentId = appointmentId;
    }
}

public class ReserveAppointmentCommandHandler : IRequestHandler<ReserveAppointmentCommand, bool>
{
    private readonly IAppointmentRepository _appointmentRepository;

    public ReserveAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<bool> Handle(ReserveAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(request.AppointmentId);

        if (appointment is null)
            return false;

        appointment.Reserve();
        await _appointmentRepository.UpdateAsync(appointment);

        return true;
    }
}
