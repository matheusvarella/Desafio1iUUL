using System;
using System.Collections.Generic;
using System.Linq;

namespace AdmDentalOffice
{
    public static class ListPatient
    {
        private static List<Patient> patients = new List<Patient>();

        public static string addPatient(Patient patient)
        {
            if (existPatient(patient.Cpf))
            {
                return "CPF de paciente ja cadastrado";
            }
            patients.Add(patient);

            return null;
        }

        public static string removePatient(long cpf)
        {
            if (existPatient(cpf))
            {
                foreach (Patient patient in patients)
                {
                    if (patient.Cpf == cpf)
                    {
                        if (!ListAppointment.futureAppointment(patient.Cpf))
                        {
                            ListAppointment.removeAllAppointments(patient.Cpf);
                            patients.Remove(patient);
                            break;
                        }
                        else
                        {
                            return "Esse paciente possui consultas futuras por isso não pode ser excluido";
                        }
                    }
                }

                return null;
            }
            else
            {
                 return "CPF de paciente não encontrado";
            }
        }
        
        public static List<Patient> getAllPatients()
        {
            return patients;
        }

        private static Dictionary<Patient, Appointment> listPatientAndAppointment()
        {
            Dictionary<Patient, Appointment> patientAndAppointment = new Dictionary<Patient, Appointment>();

            patients.ForEach(patient =>
            {
                Appointment appointment = ListAppointment.findAppointmentForCpf(patient.Cpf);

                DateTime dateNow = DateTime.Now;
                int day = int.Parse(appointment.ConsultationDate.Substring(0, 2));
                int month = int.Parse(appointment.ConsultationDate.Substring(3, 2));
                int year = int.Parse(appointment.ConsultationDate.Substring(6, 4));
                DateTime dateAppointment = new DateTime(year, month,day);
                dateAppointment.AddSeconds((double)(int.Parse(appointment.StartTime.Substring(0, 2)) * 60 * 60) + (int.Parse(appointment.StartTime.Substring(2, 2)) * 60));

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
        public static Dictionary<Patient, Appointment> listPatientsByName()
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

        public static Dictionary<Patient, Appointment> listPatientsByCpf()
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

        public static bool existPatient(long cpf)
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
