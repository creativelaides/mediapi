// MedApi.Domain/Doctor.cs
using MedApi.Domain.Primitives;
using System;
using System.Collections.Generic;

namespace MedApi.Domain
{
    public class Doctor : AggregateRoot
    {
        // Constructor protegido para EF Core
        protected Doctor()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Specialization = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;
            MedicalCenter = string.Empty;
            _appointments = new List<Appointment>();
        }

        // Constructor principal
        public Doctor(
            string firstName,
            string lastName,
            string specialization,
            string phoneNumber,
            string email,
            string medicalCenter) : this()
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Specialization = specialization ?? throw new ArgumentNullException(nameof(specialization));
            PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            MedicalCenter = medicalCenter ?? throw new ArgumentNullException(nameof(medicalCenter));
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Specialization { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string MedicalCenter { get; private set; }

        private readonly List<Appointment> _appointments = new();
        public IReadOnlyCollection<Appointment> Appointments => _appointments.AsReadOnly();

        public void AddAppointment(Appointment appointment)
        {
            if (appointment == null)
                throw new ArgumentNullException(nameof(appointment));

            _appointments.Add(appointment);
        }
    }
}