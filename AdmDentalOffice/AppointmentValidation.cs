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
        private DateTime dateAppointmentDateTime;
        private int startTimeInt;
        private int endTimeInt;
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
        public string dateAppointmentValidation(string dateAppointment)
        {
            try
            {
                int day = int.Parse(dateAppointment.Substring(0, 2));
                int month = int.Parse(dateAppointment.Substring(3, 2));
                int year = int.Parse(dateAppointment.Substring(6, 4));
                dateAppointmentDateTime = new DateTime(year, month, day);
            }
            catch
            {
                return "Formato de data inválido";
            }

            if (dateTimeNow > dateAppointmentDateTime)
            {
                return "Data da consulta não pode ser anterior a data atual";
            }

            return null;
        }
        public string starTimeValidation(string starTime)
        {
            startTimeInt = int.Parse(starTime);
            if(startTimeInt%15 != 0)
            {
                return "Formato de hora inicial inválido";
            }

            if (startTimeInt < 800 || startTimeInt > 1900)
            {
                return "Horário de atendimento é somente das 08:00 até as 19:00";
            }

            double seconds = transformToSeconds(starTime);

            dateAppointmentDateTime.AddSeconds(seconds);

            if (dateTimeNow > dateAppointmentDateTime)
            {
                return "Data da consulta não pode ser anterior a data atual";
            }

            return null;
        }
        public string endTimeValidation(string endTime)
        {
            endTimeInt = int.Parse(endTime);
            if (endTimeInt % 15 != 0)
            {
                return "Formato de hora final inválido";
            }

            if (endTimeInt < 800 || endTimeInt > 1900)
            {
                return "Horário de atendimento é somente das 08:00 até as 19:00";
            }

            int timeOfAppointment = endTimeInt - startTimeInt;

            if (timeOfAppointment < 15 || timeOfAppointment%15 != 0)
            {
                return "Horário da consulta inválido. É necessário ao menos 15 minutos para a consulta e ela só deve ser agendada a cada 15 minutos";
            }
            return null;
        }

        private double transformToSeconds(string value)
        {
            return (double) (int.Parse(value.Substring(0, 2)) * 60 * 60) + (int.Parse(value.Substring(2,2)) * 60);
        }

    }
}
