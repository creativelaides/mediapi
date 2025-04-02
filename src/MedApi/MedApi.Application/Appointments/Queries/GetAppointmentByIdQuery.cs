using MedApi.Application.Interfaces;
using MedApi.Domain;
using MediatR;

namespace MedApi.Application.Appointments.Queries;

public record GetAppointmentByIdQuery : IRequest<Appointment?>
{
    public Guid AppointmentId { get; }

    public GetAppointmentByIdQuery(Guid appointmentId)
    {
        AppointmentId = appointmentId;
    }
}

public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, Appointment?>
{
    private readonly IAppointmentRepository _appointmentRepository;

    public GetAppointmentByIdQueryHandler(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<Appointment?> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
    {
        return await _appointmentRepository.GetByIdAsync(request.AppointmentId);
    }
}
