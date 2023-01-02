using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmDentalOffice
{
    public class ListAppointment
    {
        private List<Appointment> appointments;
        public ListAppointment() 
        { 
            appointments = new List<Appointment>();
        }

        public void addAppointment(Appointment appointment)
        {
            if (futureAppointment(appointment.Cpf))
            {
                throw new Exception("Paciente já possui consulta agendada");
            }

            if (haveAppointmentInThisTime(appointment))
            {
                throw new Exception("Já existe consulta agendada nesse horário");
            }

            appointments.Add(appointment);
        }

        public void removeAppointment(long cpf, string dateAppointment, string startTime)
        {
            try
            {
                DateTime dateNow = DateTime.Now;

                Appointment appointment = findAppointment(cpf, dateAppointment, startTime);

                int day = int.Parse(appointment.DateAppointment.Substring(0, 2));
                int month = int.Parse(appointment.DateAppointment.Substring(3, 2));
                int year = int.Parse(appointment.DateAppointment.Substring(6, 4));
                DateTime date = new DateTime(year, month, day);
                date.AddSeconds((double)(int.Parse(appointment.StartTime.Substring(0, 2)) * 60 * 60) + (int.Parse(appointment.StartTime.Substring(2, 2)) * 60));

                if (date > dateNow)
                {
                    appointments.Remove(appointment);
                }
                else
                {
                    throw new Exception("A consulta só pode ser cancelada se estiver marcada para depois da data atual");
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void removeAllAppointments(long cpf)
        {
            foreach (Appointment appointment in appointments)
            {
                if (appointment.Cpf == cpf)
                {
                    appointments.Remove(appointment);
                }
            }
        }

        public Appointment findAppointment(long cpf, string dateAppointment, string startTime)
        {
            foreach (Appointment appointment in appointments)
            {
                if (appointment.Cpf == cpf && appointment.DateAppointment == dateAppointment && appointment.StartTime == startTime)
                {
                    return appointment;
                }
            }
            throw new Exception("Consulta não encontrada");
        }

        public Appointment findAppointmentForCpf(long cpf)
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

        public bool futureAppointment(long cpf)
        {
            DateTime dateNow = DateTime.Now;
            foreach (Appointment appointment in appointments)
            {
                if (appointment.Cpf == cpf)
                {   
                    int day = int.Parse(appointment.DateAppointment.Substring(0, 2));
                    int month = int.Parse(appointment.DateAppointment.Substring(3, 2));
                    int year = int.Parse(appointment.DateAppointment.Substring(6, 4));
                    DateTime dateAppointment = new DateTime(year, month, day);
                    dateAppointment.AddSeconds((double)(int.Parse(appointment.StartTime.Substring(0, 2)) * 60 * 60) + (int.Parse(appointment.StartTime.Substring(2, 2)) * 60));

                    if (dateAppointment > dateNow)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool haveAppointmentInThisTime(Appointment appointment)
        {
            foreach (Appointment appointmentInList in appointments)
            {
                if (appointmentInList.DateAppointment == appointment.DateAppointment)
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
    }    

    
}
