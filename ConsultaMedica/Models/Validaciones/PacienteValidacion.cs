using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultaMedica.Models.Validaciones
{
    public class PacienteValidacion:AbstractValidator<Paciente>
    {
        public PacienteValidacion()
        {
            RuleFor(x => x.NumeroIdentificacion).NotEmpty().MinimumLength(5)
                .WithMessage("Por favor especifique numero de identificacion");
            RuleFor(x => x.NombreCompleto).NotEmpty().Length(5,100)
                .WithMessage("Por favor Especifique NOmbre Completo");
            RuleFor(x => x.NumeroSeguroSocial).NotEmpty().Length(5, 30)
                .WithMessage("Debe especificar No de seguro solcial Min 5 Caracteres");
            RuleFor(x => x.TelefonoContacto).NotEmpty().MinimumLength(5)
                .WithMessage("Por favor especifique telefono de contacto");
            RuleFor(x => x.Email).EmailAddress()
                .WithMessage("Por favro especifique el correo, escrito de forma correcta");
            RuleFor(x => x.Edad).NotEmpty().GreaterThan(0)
                .WithMessage("Por favro especifique La edad");
            RuleFor(x => x.Sexo).NotEmpty().MaximumLength(1).Must(Validasexo)
                .WithMessage("Por favor especifique el sexo F o M unicamente");
        }

        private bool Validasexo(String Sexo)
        {
            return (Sexo == "F" || Sexo == "M");
        }
    }
}

