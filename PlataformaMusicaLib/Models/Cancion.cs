using System;
using PlataformaMusicaLib.Enums;

namespace PlataformaMusicaLib.Models
{
    public class Cancion
    {
        public Guid Id { get; private set; }
        public string Titulo { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public GeneroMusical Genero { get; set; }
        public bool Explicita { get; set; }
        public long Reproducciones { get; set; }
        public int Duracion { get; set; } // en segundos
        public Artista Artista { get; set; }

        public Cancion(string titulo, Artista artista, GeneroMusical genero, int duracion = 180)
        {
            Id = Guid.NewGuid();
            Titulo = titulo;
            Artista = artista;
            Genero = genero;
            Duracion = duracion;
            FechaLanzamiento = DateTime.Now;
            Explicita = false;
            Reproducciones = 0;
        }

        public void Reproducir()
        {
            Reproducciones++;
            Console.WriteLine($"🎵 Reproduciendo: {Titulo} - {Artista.Nombre}");
            Console.WriteLine($"   Género: {Genero} | Duración: {Duracion/60}:{Duracion%60:D2}");
        }

        public string GetDuracionFormateada()
        {
            return $"{Duracion/60}:{Duracion%60:D2}";
        }
    }
}