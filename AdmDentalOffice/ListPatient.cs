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

        public void removePatient(Patient patient)
        {
            patients.Remove(patient);
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
                patientAndAppointment.Add(patient, appointment);
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
