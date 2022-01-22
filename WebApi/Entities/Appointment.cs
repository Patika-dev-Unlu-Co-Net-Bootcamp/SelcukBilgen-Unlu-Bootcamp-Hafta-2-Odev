using System;

namespace WebApi.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Description { get; set; }
    }
}