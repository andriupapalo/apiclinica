using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ConsultaMedica.Models;
using FluentValidation;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConsultaMedica.Controllers
{
    [Route("api/[Controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly DbDataContext _dbDataContext;
        private readonly IValidator<Paciente> _PacienteValidacion;
        public PacientesController(IValidator<Paciente> PacienteValidacion, DbDataContext dbDataContext)
        {
            _dbDataContext = dbDataContext;
            _PacienteValidacion = PacienteValidacion;
        }

        public IActionResult Index()
        {
            return Ok(_dbDataContext.Get());
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbDataContext.Get());
        }

        [HttpGet("{id}", Name = "GetPacientes")]
        public IActionResult GetById(int id)
        {
            var paciente = _dbDataContext.GetById(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return Ok(paciente);
        }

        [HttpGet("Detalle/{id}", Name = "GetPacientesDetalle")]
        public IActionResult GetByIdDetallePaciente(int id)
        {
            var paciente = _dbDataContext.GetByIdDetallePaciente(id);
            if (paciente == null)
            {
                return NotFound();
            }
            //return Ok(paciente);
            return new JsonResult(paciente);
        }



        public List<String> response = new List<String>();
        [HttpPost]
        public IActionResult Create([FromBody] Paciente paciente)
        {
            var resultado = _PacienteValidacion.Validate(paciente);
            if (resultado.IsValid)
            {
                var resul = _dbDataContext.Create(paciente);
                return CreatedAtRoute(
                routeName: "GetPacientes",
                routeValues: new { id = resul.Id },
                value: resul);
            }
            else {
                foreach (var error in resultado.Errors)
                {
                    response.Add(error.ToString());
                }
            }
            return new JsonResult(response);
        }


        [HttpPut("{id}")]
        public IActionResult Update([FromBody] Paciente paciente,int id)
        {
            var respuesta = _dbDataContext.Update(paciente,id);
            if (respuesta == null)
            {
                return NotFound();
            }
            return CreatedAtRoute(
            routeName: "GetPacientes",
            routeValues: new { id = respuesta.Id },
            value: respuesta);

        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            var resultado=_dbDataContext.DeleteById(id);
            if (resultado == false)
            {
                return NotFound();
            }

            return new JsonResult(resultado);
        }
    }
}