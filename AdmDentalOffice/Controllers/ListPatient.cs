using AdmDentalOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdmDentalOffice.Controllers
{
    public static class ListPatient
    {
        private static List<Patient> patients = new List<Patient>();

        public static void AddPatient(Patient patient)
        {
            if (ExistPatient(patient.Cpf))
            {
                throw new Exception("CPF de paciente ja cadastrado");
            }
            patients.Add(patient);

        }

        public static void RemovePatient(long cpf)
        {
            if (ExistPatient(cpf))
            {
                foreach (Patient patient in patients)
                {
                    if (patient.Cpf == cpf)
                    {
                        if (!ListAppointment.FutureAppointment(patient.Cpf))
                        {
                            ListAppointment.RemoveAllAppointments(patient.Cpf);
                            patients.Remove(patient);
                            break;
                        }
                        else
                        {
                            throw new Exception("Esse paciente possui consultas futuras por isso não pode ser excluido");
                        }
                    }
                }

                
            }
            else
            {
                throw new Exception("CPF de paciente não encontrado");
            }
        }

        public static List<Patient> GetAllPatients()
        {
            return patients;
        }

        private static Dictionary<Patient, Appointment> ListPatientAndAppointment()
        {
            Dictionary<Patient, Appointment> patientAndAppointment = new Dictionary<Patient, Appointment>();

            patients.ForEach(patient =>
            {
                Appointment appointment = ListAppointment.FindAppointmentForCpf(patient.Cpf);

                DateTime dateNow = DateTime.Now;
                int day = int.Parse(appointment.ConsultationDate.Substring(0, 2));
                int month = int.Parse(appointment.ConsultationDate.Substring(3, 2));
                int year = int.Parse(appointment.ConsultationDate.Substring(6, 4));
                DateTime dateAppointment = new DateTime(year, month, day);
                dateAppointment.AddSeconds((double)(int.Parse(appointment.StartTime.Substring(0, 2)) * 60 * 60) + int.Parse(appointment.StartTime.Substring(2, 2)) * 60);

                if (dateAppointment > dateNow)
                {
                    patientAndAppointment.Add(patient, appointment);
                }
                else
                {
                    patientAndAppointment.Add(patient, null);
                }
            });
            return patientAndAppointment;
        }
        public static Dictionary<Patient, Appointment> ListPatientsByName()
        {
            var listPatients = patients.OrderBy(x => x.Name).ToList();

            var result = new Dictionary<Patient, Appointment>();

            foreach (var patient in listPatients)
            {
                var appointment = ListAppointment.GetAppointment(patient.Cpf);

                result.Add(patient, appointment);
            }

            return result;
        }

        public static Dictionary<Patient, Appointment> ListPatientsByCpf()
        {
            var listPatients = patients.OrderBy(x => x.Cpf).ToList();

            var result = new Dictionary<Patient, Appointment>();

            foreach (var patient in listPatients)
            {
                var appointment = ListAppointment.GetAppointment(patient.Cpf);

                result.Add(patient, appointment);
            }

            return result;
        }

        public static bool ExistPatient(long cpf)
        {
            foreach (Patient patient in patients)
            {
                if (patient.Cpf.Equals(cpf))
                {
                    return true;
                }
            }
            return false;
        }

        public static Patient GetPatient(long cpf)
        {
            var patient = patients.FirstOrDefault(x => x.Cpf == cpf);

            return patient;
        }
    }
}
