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
                Console.WriteLine($"📱 Playlist '{Nombre}' compartida en redes sociales");
                Console.WriteLine($"   👤 Por: {Creador.Nombre}");
                Console.WriteLine($"   🎵 {Items.Count} canciones");
            }
            else
            {
                Console.WriteLine("❌ No puedes compartir una playlist privada");
            }
        }
    }
}