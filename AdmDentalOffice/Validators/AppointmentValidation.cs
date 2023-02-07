using System;
using System.Globalization;
using System.Threading;
using AdmDentalOffice.Controllers;

namespace AdmDentalOffice.Validators
{
    public class AppointmentValidation
    {
        private DateTime dateTimeNow = DateTime.Now;
        private DateTime consultatioDateDateTime;
        private int startTimeInt;
        private int endTimeInt;
        public AppointmentValidation()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR", false);
        }
        public void CpfValidation(string cpf)
        {
            if (!cpf.CPFIsValid())
                throw new Exception("CPF inválido, digite o CPF corretamente");

        }
        public void ConsultationDateValidation(string consultationDate)
        {
            try
            {
                int day = int.Parse(AppointmentDate.Substring(0, 2));
                int month = int.Parse(AppointmentDate.Substring(3, 2));
                int year = int.Parse(AppointmentDate.Substring(6, 4));
                consultatioDateDateTime = new DateTime(year, month, day);
            }
            catch
            {
                throw new Exception("Formato de data inválido");
            }

            if (dateTimeNow > consultatioDateDateTime)
            {
                throw new Exception("Data da consulta não pode ser anterior a data atual");
            }

        }

        public void StarTimeValidation(string starTime)
        {
            startTimeInt = int.Parse(starTime);
            if (startTimeInt % 15 != 0)
            {
                throw new Exception("Formato de hora inicial inválido");
            }

            if (startTimeInt < 800 || startTimeInt > 1900)
            {
                throw new Exception("Horário de atendimento é somente das 08:00 até as 19:00");
            }

            double seconds = TransformToSeconds(starTime);

            consultatioDateDateTime.AddSeconds(seconds);

            if (dateTimeNow > consultatioDateDateTime)
            {
                throw new Exception("Data da consulta não pode ser anterior a data atual");
            }

        }
        public void EndTimeValidation(string endTime)
        {
            endTimeInt = int.Parse(endTime);
            if (endTimeInt % 15 != 0)
            {
                throw new Exception("Formato de hora final inválido");
            }

            if (endTimeInt < 800 || endTimeInt > 1900)
            {
                throw new Exception("Horário de atendimento é somente das 08:00 até as 19:00");
            }

            int timeOfAppointment = endTimeInt - startTimeInt;

            if (timeOfAppointment < 15 || timeOfAppointment % 15 != 0)
            {
                throw new Exception("Horário da consulta inválido. É necessário ao menos 15 minutos para a consulta e ela só deve ser agendada a cada 15 minutos");
            }
            
        }

        private double TransformToSeconds(string value)
        {
            return (double)(int.Parse(value.Substring(0, 2)) * 60 * 60) + int.Parse(value.Substring(2, 2)) * 60;
        }

    }
}
