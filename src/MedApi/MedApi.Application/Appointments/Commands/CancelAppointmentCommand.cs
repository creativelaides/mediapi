namespace MedApi.Application.Appointments.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;

    public record CancelAppointmentCommand : IRequest<bool>
    {
        public Guid AppointmentId { get; }

        public CancelAppointmentCommand(Guid appointmentId)
        {
            AppointmentId = appointmentId;
        }
    }

    public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand, bool>
    {
        public Task<bool> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}