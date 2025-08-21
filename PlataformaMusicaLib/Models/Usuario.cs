using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaMusicaLib.Models
{
    public class Usuario : Persona
    {
        public bool Premium { get; set; }
        public List<Playlist> Playlists { get; private set; }
        public List<Artista> ArtistasSeguidos { get; private set; }

        public Usuario(string nombre, string email) : base(nombre, email)
        {
            Premium = false;
            Playlists = new List<Playlist>();
            ArtistasSeguidos = new List<Artista>();
        }

        public Playlist CrearPlaylist(string nombre)
        {
            var playlist = new Playlist(nombre, this);
            Playlists.Add(playlist);
            Console.WriteLine($"✓ Playlist '{nombre}' creada exitosamente");
            return playlist;
        }

        public void SeguirArtista(Artista artista)
        {
            if (!ArtistasSeguidos.Contains(artista))
            {
                ArtistasSeguidos.Add(artista);
                artista.AgregarSeguidor(this);
                Console.WriteLine($"✓ Ahora sigues a {artista.Nombre}");
            }
            else
            {
                Console.WriteLine($"Ya sigues a {artista.Nombre}");
            }
        }

        public void DescargarCancion(Cancion cancion)
        {
            if (Premium)
            {
                Console.WriteLine($"🎵 Descargando: {cancion.Titulo} - {cancion.Artista.Nombre}");
                Console.WriteLine("✓ Descarga completada (Usuario Premium)");
            }
            else
            {
                Console.WriteLine("Necesitas ser usuario Premium para descargar canciones");
            }
        }

        public override string GetInfo()
        {
            return base.GetInfo() + 
                   $"\nTipo: Usuario {(Premium ? "Premium" : "Gratuito")}" +
                   $"\nPlaylists: {Playlists.Count}" +
                   $"\nArtistas seguidos: {ArtistasSeguidos.Count}";
        }
    }
}