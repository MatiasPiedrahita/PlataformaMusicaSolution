using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaMusicaLib.Models;
using PlataformaMusicaLib.Enums;

namespace PlataformaMusicaLib
{
    public class PlataformaMusica
    {
        public string Nombre { get; private set; }
        public DateTime FechaFundacion { get; private set; }
        public List<Usuario> UsuariosRegistrados { get; private set; }
        public CatalogoMusical Catalogo { get; private set; }

        public PlataformaMusica(string nombre)
        {
            Nombre = nombre;
            FechaFundacion = DateTime.Now;
            UsuariosRegistrados = new List<Usuario>();
            Catalogo = new CatalogoMusical();
        }

        public bool RegistrarUsuario(Usuario usuario)
        {
            if (UsuariosRegistrados.Any(u => u.Email == usuario.Email))
            {
                Console.WriteLine("Ya existe un usuario con este email");
                return false;
            }

            UsuariosRegistrados.Add(usuario);
            Console.WriteLine($"Usuario {usuario.Nombre} registrado exitosamente");
            Console.WriteLine($"   Email: {usuario.Email}");
            Console.WriteLine($"   ID: {usuario.Id}");
            return true;
        }

        public List<Cancion> BuscarCancion(string query)
        {
            Console.WriteLine($"Buscando canciones con: '{query}'");
            var resultados = Catalogo.BuscarPorTitulo(query);
            
            if (resultados.Any())
            {
                Console.WriteLine($"✓ {resultados.Count} cancion(es) encontrada(s):");
                foreach (var cancion in resultados)
                {
                    Console.WriteLine($"   {cancion.Titulo} - {cancion.Artista.Nombre} ({cancion.Genero})");
                }
            }
            else
            {
                Console.WriteLine("No se encontraron canciones");
            }
            
            return resultados;
        }

        public List<Cancion> RecomendarCanciones(Usuario usuario)
        {
            Console.WriteLine($"Generando recomendaciones para {usuario.Nombre}...");
            
            var recomendaciones = new List<Cancion>();
            
            // Recomendar por géneros de artistas seguidos
            foreach (var artista in usuario.ArtistasSeguidos)
            {
                var cancionesArtista = artista.Canciones.Take(2);
                recomendaciones.AddRange(cancionesArtista);
            }
            
            // Agregar algunas canciones populares
            var topCanciones = Catalogo.ObtenerTopGlobal(3);
            foreach (var cancion in topCanciones)
            {
                if (!recomendaciones.Contains(cancion))
                {
                    recomendaciones.Add(cancion);
                }
            }

            Console.WriteLine($"✓ {recomendaciones.Count} recomendaciones generadas");
            return recomendaciones.Take(5).ToList();
        }

        public void GenerarRadio(Artista artista)
        {
            Console.WriteLine($"Generando radio basada en {artista.Nombre}");
            
            var cancionesRadio = new List<Cancion>();
            cancionesRadio.AddRange(artista.Canciones);
            
            // Agregar canciones del mismo género
            if (artista.Canciones.Any())
            {
                var genero = artista.Canciones.First().Genero;
                var cancionesSimilares = Catalogo.BuscarPorGenero(genero)
                    .Where(c => c.Artista != artista)
                    .Take(3);
                cancionesRadio.AddRange(cancionesSimilares);
            }

            Console.WriteLine($"🎵 Radio '{artista.Nombre}' creada con {cancionesRadio.Count} canciones");
        }

        public void MostrarEstadisticas()
        {
            Console.WriteLine($"\n=== ESTADÍSTICAS DE {Nombre.ToUpper()} ===");
            Console.WriteLine($"Usuarios registrados: {UsuariosRegistrados.Count}");
            Console.WriteLine($"Total canciones: {Catalogo.Canciones.Count}");
            Console.WriteLine($"Total álbumes: {Catalogo.Albumes.Count}");
            Console.WriteLine($"Total artistas: {Catalogo.Artistas.Count}");
            
            var usuariosPremium = UsuariosRegistrados.Count(u => u.Premium);
            Console.WriteLine($"Usuarios Premium: {usuariosPremium}");
            
            if (Catalogo.Canciones.Any())
            {
                var cancionMasReproducida = Catalogo.Canciones.OrderByDescending(c => c.Reproducciones).First();
                Console.WriteLine($"Canción más popular: {cancionMasReproducida.Titulo} ({cancionMasReproducida.Reproducciones} reproducciones)");
            }
        }
    }
}