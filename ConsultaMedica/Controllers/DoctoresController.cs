using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsultaMedica.Models;
using FluentValidation;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace ConsultaMedica.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class DoctoresController : ControllerBase
    {
        private readonly DbDataContext _dbDataContext;
        private readonly IValidator<Doctor> _DoctorValidacion;
        public DoctoresController(IValidator<Doctor> DoctorValidacion, DbDataContext dbDataContext)
        {
            _dbDataContext = dbDataContext;
            _DoctorValidacion = DoctorValidacion;
        }

        public IActionResult Index()
        {
            return Ok(_dbDataContext.GetDoc());
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbDataContext.GetDoc());
        }

        [HttpGet("{id}", Name = "GetDoctores")]
        public IActionResult GetByIdDoc(int id)
        {
            var doctor = _dbDataContext.GetByIdDoc(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return Ok(doctor);
        }

        [HttpGet("Detalle/{id}", Name = "GetDoctoresDetalle")]
        public IActionResult GetByIdDetalleDoctores(int id)
        {
            var doctor = _dbDataContext.GetByIdDetalleDoctores(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return Ok(doctor);
        }


        public List<String> response = new List<String>();
        [HttpPost]
        public IActionResult Create([FromBody] Doctor doctor)
        {
            var resultado = _DoctorValidacion.Validate(doctor);
            if (resultado.IsValid)
            {
                var resul = _dbDataContext.CreateDoc(doctor);
                return CreatedAtRoute(
                routeName: "GetDoctores",
                routeValues: new { id = resul.Id },
                value: resul);

            }
            else
            {
                foreach (var error in resultado.Errors)
                {
                    response.Add(error.ToString());
                }
            }
            //return Ok("Ok");
            return new JsonResult(response);
        }
    }
}