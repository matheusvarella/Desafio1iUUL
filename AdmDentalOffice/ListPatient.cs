using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmDentalOffice
{
    public class ListPatient
    {
        private List<Patient> patients;

        public ListPatient() 
        {
            patients = new List<Patient>();
        }

        public void addPatient(Patient patient)
        {
            if (existPatient(patient.Cpf))
            {
                throw new Exception("CPF de paciente ja cadastrado");
            }
            patients.Add(patient);
        }

        public void removePatient(long cpf)
        {
            if (existPatient(cpf))
            {
                foreach (Patient patient in patients)
                {
                    if (patient.Cpf == cpf)
                    {
                        if (!new ListAppointment().futureAppointment(patient.Cpf))
                        {
                            new ListAppointment().removeAllAppointments(patient.Cpf);
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
        
        public List<Patient> getAllPatients()
        {
            return patients;
        }

        private Dictionary<Patient, Appointment> listPatientAndAppointment()
        {
            Dictionary<Patient, Appointment> patientAndAppointment = new Dictionary<Patient, Appointment>();

            patients.ForEach(patient =>
            {
                Appointment appointment = new ListAppointment().findAppointmentForCpf(patient.Cpf);

                DateTime dateNow = DateTime.Now;
                int day = int.Parse(appointment.DateAppointment.Substring(0, 2));
                int month = int.Parse(appointment.DateAppointment.Substring(3, 2));
                int year = int.Parse(appointment.DateAppointment.Substring(6, 4));
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
        public Dictionary<Patient, Appointment> listPatientsByName()
        {
            Dictionary<Patient, Appointment> listPatients = (Dictionary<Patient, Appointment>)listPatientAndAppointment().OrderBy(patient => patient.Key.Name);

            return listPatients;

        }

        public Dictionary<Patient, Appointment> listPatientsByCpf()
        {
            Dictionary<Patient, Appointment> listPatients = (Dictionary<Patient, Appointment>)listPatientAndAppointment().OrderBy(patient => patient.Key.Cpf);

            return listPatients;
        }

        public bool existPatient(long cpf)
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
    }
}
