using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmDentalOffice
{
    public class Appointment
    {
        public string Cpf { get; set; }

        public string ConsultationDate { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public bool Canceled { get; set; }

       // public bool AppointmentRealized { get; set; }

        public Appointment(string cpf, string consultationDate, string starTime, string endTime) 
        {
            Cpf = cpf;
            ConsultationDate = consultationDate;
            StartTime = starTime;
            EndTime = endTime;
            Canceled = false;
            //AppointmentRealized = false;
        }

        public void CancelAppointment()
        {
            Canceled = true;
        }

        /*public void RealizeAppointment()
        {
            AppointmentRealized = true;
        }*/
    }
}
