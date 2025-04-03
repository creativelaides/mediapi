using MedApi.Application.Interfaces;
using MedApi.Domain;
using MediatR;

namespace MedApi.Application.Appointments.Queries;

public record GetAvailableAppointmentsQuery : IRequest<IEnumerable<Appointment>>
{
    public string Specialty { get; }

    public GetAvailableAppointmentsQuery(string specialty)
    {
        Specialty = specialty;
    }
}

public class GetAvailableAppointmentsQueryHandler : IRequestHandler<GetAvailableAppointmentsQuery, IEnumerable<Appointment>>
{
    private readonly IAppointmentRepository _appointmentRepository;

    public GetAvailableAppointmentsQueryHandler(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<IEnumerable<Appointment>> Handle(GetAvailableAppointmentsQuery request, CancellationToken cancellationToken)
    {
        return await _appointmentRepository.GetAvailableAppointmentsAsync(request.Specialty);
    }
}