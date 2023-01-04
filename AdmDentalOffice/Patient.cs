using System;
using System.Globalization;
using System.Threading;

namespace AdmDentalOffice
{
    public class Patient
    {
        public string Name { get; set; }

        public long Cpf { get; set; }

        public DateTime BirthDate { get; set; }

        public Patient(string name, long cpf, DateTime birthDate)
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR", false);
            
            Name = name;
            Cpf = cpf;
            BirthDate = birthDate;
        }

    }
}
