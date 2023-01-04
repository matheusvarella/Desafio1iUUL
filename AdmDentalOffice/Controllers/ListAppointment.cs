using System;
using System.Collections.Generic;
using System.Linq;
using AdmDentalOffice.Models;

namespace AdmDentalOffice.Controllers
{
    public static class ListAppointment
    {
        private static List<Appointment> appointments = new List<Appointment>();

        public static string AddAppointment(Appointment appointment)
        {
            if (FutureAppointment(appointment.Cpf))
            {
                return "Paciente já possui consulta agendada";
            }

            if (HaveAppointmentInThisTime(appointment))
            {
                return "Já existe consulta agendada nesse horário";
            }

            appointments.Add(appointment);
            return null;
        }

        public static string RemoveAppointment(Appointment appointment)
        {
            try 
            {
                appointments.Remove(appointment);
                return null;
            }
            catch (Exception ex)
            {
                return "Agendamento não encontrado";
            }
        }

        public static void RemoveAllAppointments(long cpf)
        {
            foreach (Appointment appointment in appointments)
            {
                if (appointment.Cpf == cpf)
                {
                    appointments.Remove(appointment);
                }
            }
        }

        public static Appointment FindAppointmentForCpf(long cpf)
        {
            foreach (Appointment appointment in appointments)
            {
                if (appointment.Cpf == cpf)
                {
                    return appointment;
                }
            }
            throw new Exception("CPF não cadastrado na lista de consultas");
        }

        public static bool FutureAppointment(long cpf)
        {
            DateTime dateNow = DateTime.Now;
            foreach (Appointment appointment in appointments)
            {
                if (appointment.Cpf == cpf)
                {
                    int day = int.Parse(appointment.ConsultationDate.Substring(0, 2));
                    int month = int.Parse(appointment.ConsultationDate.Substring(3, 2));
                    int year = int.Parse(appointment.ConsultationDate.Substring(6, 4));
                    DateTime dateAppointment = new DateTime(year, month, day);
                    dateAppointment.AddSeconds((double)(int.Parse(appointment.StartTime.Substring(0, 2)) * 60 * 60) + int.Parse(appointment.StartTime.Substring(2, 2)) * 60);

                    if (dateAppointment > dateNow)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool HaveAppointmentInThisTime(Appointment appointment)
        {
            foreach (Appointment appointmentInList in appointments)
            {
                if (appointmentInList.ConsultationDate == appointment.ConsultationDate)
                {
                    int startTimeIntAppointmentInList = int.Parse(appointmentInList.StartTime);
                    int endTimeIntAppointmentInList = int.Parse(appointmentInList.EndTime);

                    int startTimeIntAppointment = int.Parse(appointment.StartTime);
                    int endTimeIntAppointment = int.Parse(appointment.EndTime);

                    if (startTimeIntAppointment >= startTimeIntAppointmentInList && startTimeIntAppointment < endTimeIntAppointmentInList)
                    {
                        return true;
                    }
                    if (endTimeIntAppointment >= startTimeIntAppointmentInList && endTimeIntAppointment <= endTimeIntAppointmentInList)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static Dictionary<Appointment, Patient> ListAllAppointments()
        {
            appointments = appointments.OrderBy(x => x.ConsultationDate).ToList();

            var response = new Dictionary<Appointment, Patient>();

            foreach (var appointment in appointments)
            {
                var patient = ListPatient.GetPatient(appointment.Cpf);

                response.Add(appointment, patient);
            }

            return response;
        }

        public static Dictionary<Appointment, Patient> ListAppointmentsByPeriod(string initialDate, string finalDate)
        {
            appointments = appointments.OrderBy(x => x.ConsultationDate).ToList();

            var response = new Dictionary<Appointment, Patient>();

            foreach (var appointment in appointments)
            {
                if (DateTime.Parse(appointment.ConsultationDate) >= DateTime.Parse(initialDate) &&
                    DateTime.Parse(appointment.ConsultationDate) <= DateTime.Parse(finalDate))
                {
                    var patient = ListPatient.GetPatient(appointment.Cpf);

                    response.Add(appointment, patient);
                }
            }

            return response;
        }

        public static Appointment GetAppointment(long cpf)
        {
            var appointment = appointments.FirstOrDefault(x => x.Cpf == cpf && DateTime.Parse(x.ConsultationDate) > DateTime.Now);

            return appointment;
        }

        public static Appointment GetAppointment(long cpf, string consultDate, string initialHour)
        {
            var appointment = appointments.FirstOrDefault(
                x => x.Cpf == cpf && 
                x.ConsultationDate == consultDate &&
                x.StartTime == initialHour);

            return appointment;
        }
    }
}