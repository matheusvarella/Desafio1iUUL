    using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdmDentalOffice
{
    public class AppointmentValidation
    {
        private DateTime dateTimeNow = DateTime.Now;
        private DateTime consultatioDateDateTime;
        public AppointmentValidation() 
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR", false);
        }
        public string cpfValidation(string cpf)
        {
           
            PatientValidation patientValidation = new PatientValidation();

            string error =  patientValidation.cpfValidation(cpf);

            ListPatient listPatient = new ListPatient();
            if (error == null)
            {
                if (!listPatient.existPatient(long.Parse(cpf)))
                {
                    error = "CPF não cadastrado como paciente";
                }
            }
            return error;
        }
        public string consultationDateValidation(string consultationDate)
        {
            try
            {
                int day = int.Parse(consultationDate.Substring(0, 2));
                int month = int.Parse(consultationDate.Substring(3, 2));
                int year = int.Parse(consultationDate.Substring(6, 4));
                consultatioDateDateTime = new DateTime(day, month, year);
            }
            catch
            {
                return "Formato de data inválido";
            }

            if (dateTimeNow > consultatioDateDateTime)
            {
                return "Data da consulta é anterior a data atual";
            }

            return null;
        }
        public string starTimeValidation(string starTime)
        {
            int startTimeInt = int.Parse(starTime);
            if(startTimeInt%15 != 0)
            {
                return "Formato de hora inicial inválido";
            }
            return null;
        }
        public string endTimeValidation(string endTime)
        {
            int endTimeInt = int.Parse(endTime);
            if (endTimeInt % 15 != 0)
            {
                return "Formato de hora final inválido";
            }
            return null;
        }

    }
}
