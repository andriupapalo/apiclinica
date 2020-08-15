using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ConsultaMedica.Models.Validaciones
{
    public class DoctorValidacion:AbstractValidator<Doctor>
    {
        public DoctorValidacion()
        {
            RuleFor(x => x.NumeroCredencial).NotEmpty().MinimumLength(5)
                .WithMessage("Por favor Especifique Numero de credencial completo");

            RuleFor(x => x.NombreCompleto).NotEmpty().Length(5, 100)
                .WithMessage("Por favor Especifique nombre completo");

            RuleFor(x => x.Especialidad).NotEmpty().MinimumLength(5)
                .WithMessage("Por favor Especifique La especialidad");

            RuleFor(x => x.TelefonoContacto).NotEmpty().MinimumLength(5)
                .WithMessage("Por favor Especifique Telefono de contacto");

            RuleFor(x=>x.Email).EmailAddress()
                .WithMessage("Por favor Especifique el email");
            RuleFor(x=>x.HospitalDeTrabajo).NotEmpty().MinimumLength(5)
                .WithMessage("Por favor Especifique Hospital donde trabaja");
        }
    }
}
