using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace ConsultaMedica.Models
{

    public class DbDataContext : DbContext
    {
        public bool erro = false;
        public string msg = "";
        public Paciente paci;
        public Paciente Doct;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = DIONISIO; Initial Catalog = REGISTROCLINICO; User ID = sa;Password=79782606Papalolo;Integrated Security=false");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DoctorPaciente>().HasKey(x => new { x.DoctorId, x.PacienteId });
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<DoctorPaciente> DoctorPaciente { get; set; }


        public List<Paciente> Get()
        {
            return Paciente.ToList();
        }

        public Paciente GetById(int id)
        {
            return Paciente.FirstOrDefault(x => x.Id == id);

        }
        public Paciente GetByIdDetallePaciente(int id)
        {
                return Paciente.Where(x => x.Id == id).Include(x => x.DoctorPacientes).ThenInclude(y => y.Doctor).FirstOrDefault();
         }

        public Paciente Create(Paciente Paci)
        {
            erro = false;
            try
            {
                Paciente.Add(Paci);
                SaveChanges();
                erro = true;
            }
            catch (Exception err)
            {
                msg = err.Message;
            }
            return Paci;
        }

        public Paciente Update(Paciente Paci,int id)
        {
            var paci = Paciente.FirstOrDefault(x => x.Id == id);
            try
            {
                if (paci!=null){
                    paci.NumeroIdentificacion = Paci.NumeroIdentificacion;
                    paci.NombreCompleto = Paci.NombreCompleto;
                    paci.NumeroSeguroSocial = Paci.NumeroSeguroSocial;
                    paci.CodigoPostal = Paci.CodigoPostal;
                    paci.Email=Paci.Email;
                    paci.Edad = Paci.Edad;
                    paci.Sexo = Paci.Sexo;
                    Entry(paci).State = EntityState.Modified;
                    SaveChanges();
                }
            }
            catch (Exception err)
            {
                var msg = err.Message;
            }
            return paci;
        }
        public bool DeleteById(int id)

        {
            var paci = Paciente.FirstOrDefault(x => x.Id == id);
            try
            {
                if (paci != null)
                {
                    Entry(paci).State = EntityState.Deleted;
                    SaveChanges();
                    erro = true;
                }
            }
            catch (Exception err)
            {
                msg = err.Message;
            }
            return erro;
        }

        /*Validacion para doctores*/
        public List<Doctor> GetDoc()
        {
            return Doctor.ToList();
        }

        public Doctor GetByIdDoc(int id)
        {
            return Doctor.FirstOrDefault(x => x.Id == id);

        }

        public Doctor GetByIdDetalleDoctores(int id)
        {
            return Doctor.Where(x => x.Id == id).Include(x => x.DoctorPacientes).ThenInclude(y => y.Paciente).FirstOrDefault();
        }

        public Doctor CreateDoc(Doctor Doct)
        {
            erro = false;
            try
            {
                Doctor.Add(Doct);
                SaveChanges();
                erro = true;
            }
            catch (Exception err)
            {
                msg = err.Message;
            }
            return Doct;
        }
    }
}
