using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultaMedica.Models
{
    public class Paciente
    {
        public int Id { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string NombreCompleto { get; set; }
        public string NumeroSeguroSocial { get; set; }
        public string CodigoPostal { get; set; }
        public string TelefonoContacto { get; set; }
        public string Email {get;set;}
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public List<DoctorPaciente> DoctorPacientes { get; set; }
    }
}
