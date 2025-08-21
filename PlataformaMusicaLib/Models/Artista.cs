using System;
using System.Collections.Generic;
using PlataformaMusicaLib.Enums;

namespace PlataformaMusicaLib.Models
{
    public class Artista : Persona
    {
        public string Biografia { get; set; }
        public DateTime FechaInicio { get; set; }
        public List<Usuario> Seguidores { get; private set; }
        public List<Album> Albumes { get; private set; }
        public List<Cancion> Canciones { get; private set; }

        public Artista(string nombre, string email, string biografia) : base(nombre, email)
        {
            Biografia = biografia;
            FechaInicio = DateTime.Now;
            Seguidores = new List<Usuario>();
            Albumes = new List<Album>();
            Canciones = new List<Cancion>();
        }

        public void PublicarAlbum(Album album)
        {
            Albumes.Add(album);
            Console.WriteLine($"🎼 Álbum '{album.Titulo}' publicado por {Nombre}");
        }

        public void AgregarCancion(Cancion cancion)
        {
            Canciones.Add(cancion);
            Console.WriteLine($"🎵 Canción '{cancion.Titulo}' agregada al catálogo de {Nombre}");
        }

        public void AgregarSeguidor(Usuario usuario)
        {
            if (!Seguidores.Contains(usuario))
            {
                Seguidores.Add(usuario);
            }
        }

        public override string GetInfo()
        {
            return base.GetInfo() + 
                   $"\nTipo: Artista" +
                   $"\nBiografía: {Biografia}" +
                   $"\nFecha Inicio: {FechaInicio:dd/MM/yyyy}" +
                   $"\nSeguidores: {Seguidores.Count}" +
                   $"\nÁlbumes: {Albumes.Count}" +
                   $"\nCanciones: {Canciones.Count}";
        }
    }
}