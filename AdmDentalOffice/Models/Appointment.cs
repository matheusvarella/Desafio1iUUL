using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmDentalOffice.Models
{
    public class Appointment
    {
        public long Cpf { get; set; }

        public string ConsultationDate { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public Appointment(long cpf, string consultationDate, string starTime, string endTime)
        {
            Cpf = cpf;
            ConsultationDate = consultationDate;
            StartTime = starTime;
            EndTime = endTime;
        }
    }
}
