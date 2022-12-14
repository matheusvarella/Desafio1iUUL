using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmDentalOffice
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
            BirthDate = BirthDate;
        }

    }
}
