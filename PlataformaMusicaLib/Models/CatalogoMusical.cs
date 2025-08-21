using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaMusicaLib.Enums;

namespace PlataformaMusicaLib.Models
{
    public class CatalogoMusical
    {
        public List<Cancion> Canciones { get; private set; }
        public List<Album> Albumes { get; private set; }
        public List<Artista> Artistas { get; private set; }

        public CatalogoMusical()
        {
            Canciones = new List<Cancion>();
            Albumes = new List<Album>();
            Artistas = new List<Artista>();
        }

        public void AgregarCancion(Cancion cancion)
        {
            Canciones.Add(cancion);
            if (!Artistas.Contains(cancion.Artista))
            {
                Artistas.Add(cancion.Artista);
            }
        }

        public void AgregarAlbum(Album album)
        {
            Albumes.Add(album);
            foreach (var cancion in album.Canciones)
            {
                AgregarCancion(cancion);
            }
        }

        public List<Cancion> BuscarPorGenero(GeneroMusical genero)
        {
            return Canciones.Where(c => c.Genero == genero).ToList();
        }

        public List<Cancion> BuscarPorTitulo(string query)
        {
            return Canciones.Where(c => c.Titulo.ToLower().Contains(query.ToLower())).ToList();
        }

        public List<Cancion> BuscarPorArtista(string nombreArtista)
        {
            return Canciones.Where(c => c.Artista.Nombre.ToLower().Contains(nombreArtista.ToLower())).ToList();
        }

        public List<Cancion> ObtenerTopGlobal(int cantidad = 10)
        {
            return Canciones.OrderByDescending(c => c.Reproducciones).Take(cantidad).ToList();
        }
    }
}