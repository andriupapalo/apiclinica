using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultaMedica.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string NumeroCredencial { get; set; }
        public string NombreCompleto { get; set; }
        public string Especialidad { get; set; }
        public string TelefonoContacto { get; set; }
        public string Email { get; set; }
        public string HospitalDeTrabajo { get; set; }
        public List<DoctorPaciente> DoctorPacientes { get; set; }
    }
}
