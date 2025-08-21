using System;

namespace PlataformaMusicaLib.Models
{
    public class ItemPlaylist
    {
        public Guid Id { get; private set; }
        public int Orden { get; set; }
        public DateTime FechaAgregado { get; private set; }
        public Cancion Cancion { get; set; }

        public ItemPlaylist(Cancion cancion, int orden)
        {
            Id = Guid.NewGuid();
            Cancion = cancion;
            Orden = orden;
            FechaAgregado = DateTime.Now;
        }

        public void CambiarOrden(int nuevoOrden)
        {
            Orden = nuevoOrden;
            Console.WriteLine($"✓ Orden cambiado a {nuevoOrden} para '{Cancion.Titulo}'");
        }
    }
}