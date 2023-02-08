using System;
using System.Collections.Generic;

namespace AdmDentalOffice.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Cpf { get; set; }
        public DateTime BirthDate { get; set; }
        public IList<Appointment> Appointments { get; set; }

        private Patient() { }

        public Patient(string name, long cpf, DateTime birthDate)
        {
            Name = name;
            Cpf = cpf;
            BirthDate = birthDate;
        }

    }
}
