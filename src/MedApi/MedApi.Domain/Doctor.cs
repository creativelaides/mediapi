// MedApi.Domain/Doctor.cs
using MedApi.Domain.Primitives;
using System;
using System.Collections.Generic;

namespace MedApi.Domain
{
    public class Doctor : AggregateRoot
    {
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public string? Specialization { get; private set; }
        public string? PhoneNumber { get; private set; }
        public string? Email { get; private set; }
        public string? MedicalCenter { get; private set; }

        private readonly List<Appointment> _appointments = [];
        public IReadOnlyCollection<Appointment> Appointments => _appointments.AsReadOnly();
        
        // Constructor protegido para EF Core
        protected Doctor() { }

        // Constructor principal
        public Doctor
        (
            string firstName, 
            string lastName, 
            string specialization, 
            string phoneNumber, 
            string email, 
            string medicalCenter
        )
        {
            FirstName = firstName ?? throw new ArgumentException("First Name is required");
            LastName = lastName ?? throw new ArgumentException("Last Name is required");
            Specialization = specialization ?? throw new ArgumentException("Specialization is required");
            PhoneNumber = phoneNumber ?? throw new ArgumentException("Phone Number is required");
            Email = email ?? throw new ArgumentException("Email is required");
            MedicalCenter = medicalCenter ?? throw new ArgumentException("Medical Center is required");
        }

        public void AddAppointment(Appointment appointment)
        {
            ArgumentNullException.ThrowIfNull(appointment);

            _appointments.Add(appointment);
        }
    }
}