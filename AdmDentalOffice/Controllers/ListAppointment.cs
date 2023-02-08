using System;
using System.Collections.Generic;
using System.Linq;
using AdmDentalOffice.Data;
using AdmDentalOffice.Models;

namespace AdmDentalOffice.Controllers
{
    public static class ListAppointment
    {
        //private static List<Appointment> appointments = new List<Appointment>();
        private static AdmDentalOfficeDataContext context = new AdmDentalOfficeDataContext();

        public static void InsertAppointment(Appointment appointment)
        {
            if (FutureAppointment(appointment.Cpf))
            {
                throw new Exception("Paciente já possui consulta agendada");
            }

            if (HaveAppointmentInThisTime(appointment))
            {
                throw new Exception("Já existe consulta agendada nesse horário");
            }

            var patient = context.Patients.FirstOrDefault(x => x.Cpf == appointment.Cpf);

            appointment.PatientId = patient.Id;


            //appointments.Add(appointment);
            context.Add(appointment);
            context.SaveChanges();
        }

        public static void RemoveAppointment(Appointment appointment)
        {
            try 
            {
                //appointments.Remove(appointment);
                context.Remove(appointment);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Agendamento não encontrado. "+ex.Message);
            }
        }

        public static void RemoveAllAppointments(long cpf)
        {
            var appointments = context.Appointments.Where(x => x.Cpf == cpf).ToList();

            foreach (Appointment appointment in appointments)
            {
                //if (appointment.Cpf == cpf)
                //{
                //    //appointments.Remove(appointment);
                //}
                context.Remove(appointment);
                context.SaveChanges();
            }
        }

        public static Appointment FindAppointmentForCpf(long cpf)
        {
            //foreach (Appointment appointment in appointments)
            //{
            //    if (appointment.Cpf == cpf)
            //    {
            //        return appointment;
            //    }
            //}
            try
            {
                var appointment = context.Appointments.FirstOrDefault(x => x.Cpf == cpf);

                return appointment;
            } 
            catch (Exception)
            {
                throw new Exception("CPF não cadastrado na lista de consultas");
            }
        }

        public static bool FutureAppointment(long cpf)
        {
            DateTime dateNow = DateTime.Now;
            var appointments = context.Appointments.ToList();

            foreach (Appointment appointment in appointments)
            {
                if (appointment.Cpf == cpf)
                {
                    int day = int.Parse(appointment.AppointmentDate.Substring(0, 2));
                    int month = int.Parse(appointment.AppointmentDate.Substring(3, 2));
                    int year = int.Parse(appointment.AppointmentDate.Substring(6, 4));
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
            var appointments = context.Appointments.ToList();
            foreach (Appointment appointmentInList in appointments)
            {
                if (appointmentInList.AppointmentDate == appointment.AppointmentDate)
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
            var listAppointments = context.Appointments.OrderBy(x => x.AppointmentDate).ToList();

            var response = new Dictionary<Appointment, Patient>();

            foreach (var appointment in listAppointments)
            {
                var patient = ListPatient.GetPatient(appointment.Cpf);

                response.Add(appointment, patient);
            }

            return response;
        }

        public static Dictionary<Appointment, Patient> ListAppointmentsByPeriod(string initialDate, string finalDate)
        {
            //appointments = appointments.OrderBy(x => x.AppointmentDate).ToList();
            var listAppointments = context.Appointments.OrderBy(x => x.AppointmentDate).ToList();

            var response = new Dictionary<Appointment, Patient>();

            foreach (var appointment in listAppointments)
            {
                if (DateTime.Parse(appointment.AppointmentDate) >= DateTime.Parse(initialDate) &&
                    DateTime.Parse(appointment.AppointmentDate) <= DateTime.Parse(finalDate))
                {
                    var patient = ListPatient.GetPatient(appointment.Cpf);

                    response.Add(appointment, patient);
                }
            }

            return response;
        }

        public static Appointment GetAppointment(long cpf)
        {
            //var appointment = context.Appointments.FirstOrDefault(x => x.Cpf == cpf && DateTime.Parse(x.AppointmentDate) > DateTime.Now);
            var appointment = context
                .Appointments
                .FirstOrDefault(x => x.Cpf == cpf);

            return appointment;
        }

        public static Appointment GetAppointment(long cpf, string consultDate, string initialHour)
        {
            var appointment = context.Appointments.FirstOrDefault(
                x => x.Cpf == cpf && 
                x.AppointmentDate.Contains(consultDate) &&
                x.StartTime == initialHour);

            return appointment;
        }
    }
}