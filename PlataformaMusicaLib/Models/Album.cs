using System;
using System.Collections.Generic;
using PlataformaMusicaLib.Enums;

namespace PlataformaMusicaLib.Models
{
    public class Album
    {
        public Guid Id { get; private set; }
        public string Titulo { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public Artista Artista { get; set; }
        public List<Cancion> Canciones { get; private set; }
        public TipoAlbum Tipo { get; set; }

        public Album(string titulo, Artista artista, TipoAlbum tipo)
        {
            Id = Guid.NewGuid();
            Titulo = titulo;
            Artista = artista;
            Tipo = tipo;
            FechaLanzamiento = DateTime.Now;
            Canciones = new List<Cancion>();
        }

        public void AgregarCancion(Cancion cancion)
        {
            Canciones.Add(cancion);
            Console.WriteLine($"✓ Canción '{cancion.Titulo}' agregada al álbum '{Titulo}'");
        }

        public int ObtenerDuracionTotal()
        {
            return Canciones.Sum(cancion => cancion.Duracion);
        }
    }
}