using System;

namespace PlataformaMusicaLib.Models
{
    public abstract class Persona
    {
        public Guid Id { get; protected set; }
        public string Nombre { get; protected set; }
        public string Email { get; protected set; }
        public DateTime FechaRegistro { get; protected set; }

        protected Persona(string nombre, string email)
        {
            Id = Guid.NewGuid();
            Nombre = nombre;
            Email = email;
            FechaRegistro = DateTime.Now;
        }

        public virtual string GetInfo()
        {
            return $"ID: {Id}\nNombre: {Nombre}\nEmail: {Email}\nFecha Registro: {FechaRegistro:dd/MM/yyyy}";
        }
    }
}