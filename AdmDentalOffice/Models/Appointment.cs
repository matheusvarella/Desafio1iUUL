namespace AdmDentalOffice.Models
{
    public class Appointment
    {
        public int Id { get; private set; }
        public int PatientId { get; private set; }
        public long Cpf { get; private set; }
        public string AppointmentDate { get; private set; }
        public string StartTime { get; private set; }
        public string EndTime { get; private set; }
        public Patient Patient { get; private set; }

        private Appointment() { }

        public Appointment(long cpf, string appointmentDate, string starTime, string endTime)
        {
            Cpf = cpf;
            AppointmentDate = appointmentDate;
            StartTime = starTime;
            EndTime = endTime;
        }
    }
}
