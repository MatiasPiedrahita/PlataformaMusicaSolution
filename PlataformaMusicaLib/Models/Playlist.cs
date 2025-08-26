using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaMusicaLib.Models
{
    public class Playlist
    {
        public Guid Id { get; private set; }
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; private set; }
        public bool Publica { get; set; }
        public List<ItemPlaylist> Items { get; private set; }
        public Usuario Creador { get; private set; }

        public Playlist(string nombre, Usuario creador)
        {
            Id = Guid.NewGuid();
            Nombre = nombre;
            Creador = creador;
            FechaCreacion = DateTime.Now;
            Publica = false;
            Items = new List<ItemPlaylist>();
        }

        public void AgregarCancion(Cancion cancion, int orden)
        {
            var item = new ItemPlaylist(cancion, orden);
            Items.Add(item);
            Console.WriteLine($"✓ '{cancion.Titulo}' agregada a la playlist '{Nombre}'");
        }

        public void EliminarCancion(ItemPlaylist item)
        {
            if (Items.Remove(item))
            {
                Console.WriteLine($"✓ Canción eliminada de la playlist '{Nombre}'");
            }
        }

        public void CompartirEnRedes()
        {
            if (Publica)
            {
                Console.WriteLine($"Playlist '{Nombre}' compartida en redes sociales");
                Console.WriteLine($"   Por: {Creador.Nombre}");
                Console.WriteLine($"   {Items.Count} canciones");
            }
            else
            {
                Console.WriteLine("No puedes compartir una playlist privada");
            }
        }

        public int ObtenerDuracionTotal()
        {
            return Items.Sum(item => item.Cancion.Duracion);
        }

        public string GetDuracionFormateada()
        {
            int totalSegundos = ObtenerDuracionTotal();
            int horas = totalSegundos / 3600;
            int minutos = (totalSegundos % 3600) / 60;
            int segundos = totalSegundos % 60;

            if (horas > 0)
                return $"{horas}:{minutos:D2}:{segundos:D2}";
            else
                return $"{minutos}:{segundos:D2}";
        }

        public List<Cancion> ObtenerCancionesOrdenadas()
        {
            return Items.OrderBy(item => item.Orden)
                       .Select(item => item.Cancion)
                       .ToList();
        }

        public void ReordenarItems()
        {
            Items = Items.OrderBy(item => item.Orden).ToList();
            Console.WriteLine($"✓ Items de la playlist '{Nombre}' reordenados");
        }

        public void MostrarInfo()
        {
            Console.WriteLine($"\n=== PLAYLIST: {Nombre} ===");
            Console.WriteLine($"Creada: {FechaCreacion:dd/MM/yyyy}");
            Console.WriteLine($"Creador: {Creador.Nombre}");
            Console.WriteLine($"Estado: {(Publica ? "Pública" : "Privada")}");
            Console.WriteLine($"Canciones: {Items.Count}");
            Console.WriteLine($"Duración total: {GetDuracionFormateada()}");
            
            if (Items.Count > 0)
            {
                Console.WriteLine("\nCANCIONES:");
                var cancionesOrdenadas = ObtenerCancionesOrdenadas();
                for (int i = 0; i < cancionesOrdenadas.Count; i++)
                {
                    var cancion = cancionesOrdenadas[i];
                    Console.WriteLine($"   {i + 1}. {cancion.Titulo} - {cancion.Artista.Nombre} [{cancion.GetDuracionFormateada()}]");
                }
            }
        }
    }
}