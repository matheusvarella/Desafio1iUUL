using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdmDentalOffice
{
    public class AppointmentFactory
    {
        public AppointmentFactory() { }

        public Appointment createAppointment(string cpf, string consultationDate, string starTime, string endTime)
        {

            AppointmentValidation appointmentValidation = new AppointmentValidation();

            string errorCpf = appointmentValidation.cpfValidation(cpf);
            string errorConsultationDate = appointmentValidation.consultationDateValidation(consultationDate);
            string errorStarTime = appointmentValidation.starTimeValidation(starTime);
            string errorEndTime = appointmentValidation.endTimeValidation(endTime);
            string error = null;
            if (errorCpf != null)
            {
                error = errorCpf;
            }
            if (errorConsultationDate != null)
            {
                if (error == null)
                {
                    error = errorConsultationDate;
                }
                else
                {
                    error += ", \n" + errorConsultationDate;
                }
            }
            if (errorStarTime != null)
            {
                if (error == null)
                {
                    error = errorStarTime;
                }
                else
                {
                    error += ", \n" + errorStarTime;
                }
            }
            if (errorEndTime != null)
            {
                if (error == null)
                {
                    error = errorEndTime;
                }
                else
                {
                    error += ", \n" + errorEndTime;
                }
            }

            if (error == null)
            {
                throw new Exception(error);
            }


            return null;
        }
    }
}
