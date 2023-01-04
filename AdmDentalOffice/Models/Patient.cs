using System;

namespace AdmDentalOffice.Models
{
    public class Patient
    {
        public string Name { get; set; }

        public long Cpf { get; set; }

        public DateTime BirthDate { get; set; }

        public Patient(string name, long cpf, DateTime birthDate)
        {
            Name = name;
            Cpf = cpf;
            BirthDate = birthDate;
        }

    }
}
