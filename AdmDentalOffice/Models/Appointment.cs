namespace AdmDentalOffice.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public long Cpf { get; set; }
        public string AppointmentDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public Patient Patient { get; set; }

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
