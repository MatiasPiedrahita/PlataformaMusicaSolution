using System;
using System.Collections.Generic;
using PlataformaMusicaLib;
using PlataformaMusicaLib.Models;
using PlataformaMusicaLib.Enums;

namespace PlataformaMusicaApp
{
    class Program
    {
        private static PlataformaMusica plataforma;

        static void Main(string[] args)
        {
            Console.WriteLine("🎵 ===== BIENVENIDO A LA PLATAFORMA MUSICAL ===== 🎵\n");
            
            // Inicializar plataforma
            plataforma = new PlataformaMusica("MusicStream");
            
            // Cargar datos de prueba
            CargarDatosDePrueba();
            
            // Mostrar menú principal
            MostrarMenu();
        }

        static void CargarDatosDePrueba()
        {
            Console.WriteLine("📦 Cargando datos de prueba...\n");

            // Crear artistas
            var shakira = new Artista("Shakira", "shakira@music.com", "Cantante colombiana internacional");
            var maluma = new Artista("Maluma", "maluma@music.com", "Artista urbano colombiano");
            var coldplay = new Artista("Coldplay", "coldplay@music.com", "Banda británica de rock alternativo");

            plataforma.Catalogo.Artistas.Add(shakira);
            plataforma.Catalogo.Artistas.Add(maluma);
            plataforma.Catalogo.Artistas.Add(coldplay);

            // Crear canciones
            var cancion1 = new Cancion("Waka Waka", shakira, GeneroMusical.POP, 215);
            var cancion2 = new Cancion("Hips Don't Lie", shakira, GeneroMusical.POP, 218);
            var cancion3 = new Cancion("Felices los 4", maluma, GeneroMusical.REGGAETON, 189);
            var cancion4 = new Cancion("Corazón", maluma, GeneroMusical.REGGAETON, 195);
            var cancion5 = new Cancion("Yellow", coldplay, GeneroMusical.ROCK, 269);
            var cancion6 = new Cancion("Fix You", coldplay, GeneroMusical.ROCK, 292);

            // Simular reproducciones
            cancion1.Reproducciones = 1500000;
            cancion2.Reproducciones = 2300000;
            cancion3.Reproducciones = 890000;
            cancion4.Reproducciones = 567000;
            cancion5.Reproducciones = 1800000;
            cancion6.Reproducciones = 1200000;

            // Agregar canciones al catálogo
            plataforma.Catalogo.AgregarCancion(cancion1);
            plataforma.Catalogo.AgregarCancion(cancion2);
            plataforma.Catalogo.AgregarCancion(cancion3);
            plataforma.Catalogo.AgregarCancion(cancion4);
            plataforma.Catalogo.AgregarCancion(cancion5);
            plataforma.Catalogo.AgregarCancion(cancion6);

            // Agregar canciones a los artistas
            shakira.AgregarCancion(cancion1);
            shakira.AgregarCancion(cancion2);
            maluma.AgregarCancion(cancion3);
            maluma.AgregarCancion(cancion4);
            coldplay.AgregarCancion(cancion5);
            coldplay.AgregarCancion(cancion6);

            // Crear álbumes
            var albumShakira = new Album("Grandes Éxitos", shakira, TipoAlbum.ALBUM);
            albumShakira.AgregarCancion(cancion1);
            albumShakira.AgregarCancion(cancion2);
            plataforma.Catalogo.AgregarAlbum(albumShakira);

            Console.WriteLine("✅ Datos de prueba cargados exitosamente\n");
        }

        static void MostrarMenu()
        {
            int opcion;
            do
            {
                Console.WriteLine("\n🎵 ===== MENÚ PRINCIPAL =====");
                Console.WriteLine("1. 👤 Registrar Usuario");
                Console.WriteLine("2. 🎧 Buscar y Reproducir Canción");
                Console.WriteLine("3. 📊 Ver Estadísticas");
                Console.WriteLine("4. 🎯 Ver Recomendaciones");
                Console.WriteLine("5. 📻 Generar Radio por Artista");
                Console.WriteLine("6. 🔍 Ver Catálogo Completo");
                Console.WriteLine("7. 🎵 Flujo Completo: Usuario y Playlist");
                Console.WriteLine("8. 👨‍🎤 Flujo Completo: Artista y Seguidores");
                Console.WriteLine("0. ❌ Salir");
                Console.Write("\nSelecciona una opción: ");

                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine();
                    switch (opcion)
                    {
                        case 1: RegistrarUsuario(); break;
                        case 2: BuscarYReproducirCancion(); break;
                        case 3: plataforma.MostrarEstadisticas(); break;
                        case 4: MostrarRecomendaciones(); break;
                        case 5: GenerarRadio(); break;
                        case 6: MostrarCatalogo(); break;
                        case 7: FlujoCompletoUsuarioPlaylist(); break;
                        case 8: FlujoCompletoArtistaSeguidores(); break;
                        case 0: Console.WriteLine("👋 ¡Gracias por usar la plataforma!"); break;
                        default: Console.WriteLine("❌ Opción inválida"); break;
                    }
                }
                else
                {
                    Console.WriteLine("❌ Por favor ingresa un número válido");
                    opcion = -1;
                }

                if (opcion != 0)
                {
                    Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcion != 0);
        }

        // FLUJO 1: Registrar Usuario y Crear Playlist
        static void FlujoCompletoUsuarioPlaylist()
        {
            Console.WriteLine("🎯 === FLUJO COMPLETO 1: REGISTRAR USUARIO Y CREAR PLAYLIST ===\n");

            // Paso 1: Registrar usuario
            Console.Write("👤 Ingresa el nombre del usuario: ");
            string nombre = Console.ReadLine();
            Console.Write("📧 Ingresa el email: ");
            string email = Console.ReadLine();

            var usuario = new Usuario(nombre, email);
            
            if (plataforma.RegistrarUsuario(usuario))
            {
                Console.WriteLine("\n⭐ ¿Quieres ser usuario Premium? (s/n): ");
                if (Console.ReadLine()?.ToLower() == "s")
                {
                    usuario.Premium = true;
                    Console.WriteLine("✅ Cuenta actualizada a Premium");
                }

                // Paso 2: Seguir artistas
                Console.WriteLine("\n👨‍🎤 Artistas disponibles para seguir:");
                for (int i = 0; i < plataforma.Catalogo.Artistas.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {plataforma.Catalogo.Artistas[i].Nombre}");
                }

                Console.Write("Selecciona un artista para seguir (número): ");
                if (int.TryParse(Console.ReadLine(), out int artistaIndex) && 
                    artistaIndex > 0 && artistaIndex <= plataforma.Catalogo.Artistas.Count)
                {
                    usuario.SeguirArtista(plataforma.Catalogo.Artistas[artistaIndex - 1]);
                }

                // Paso 3: Crear playlist
                Console.Write("\n🎵 Nombre de tu nueva playlist: ");
                string nombrePlaylist = Console.ReadLine();
                var playlist = usuario.CrearPlaylist(nombrePlaylist);

                // Paso 4: Agregar canciones a la playlist
                Console.WriteLine("\n🎵 Canciones disponibles:");
                for (int i = 0; i < Math.Min(5, plataforma.Catalogo.Canciones.Count); i++)
                {
                    var cancion = plataforma.Catalogo.Canciones[i];
                    Console.WriteLine($"{i + 1}. {cancion.Titulo} - {cancion.Artista.Nombre}");
                }

                Console.Write("Selecciona una canción para agregar (número): ");
                if (int.TryParse(Console.ReadLine(), out int cancionIndex) && 
                    cancionIndex > 0 && cancionIndex <= Math.Min(5, plataforma.Catalogo.Canciones.Count))
                {
                    var cancionSeleccionada = plataforma.Catalogo.Canciones[cancionIndex - 1];
                    playlist.AgregarCancion(cancionSeleccionada, 1);
                    
                    // Reproducir la canción
                    cancionSeleccionada.Reproducir();
                }

                // Paso 5: Mostrar información final
                Console.WriteLine("\n📋 === RESUMEN DEL USUARIO ===");
                Console.WriteLine(usuario.GetInfo());

                Console.WriteLine("\n✅ ¡Flujo completado exitosamente!");
            }
        }

        // FLUJO 2: Crear Artista y Gestionar Seguidores
        static void FlujoCompletoArtistaSeguidores()
        {
            Console.WriteLine("🎯 === FLUJO COMPLETO 2: ARTISTA Y GESTIÓN DE SEGUIDORES ===\n");

            // Paso 1: Crear nuevo artista
            Console.Write("👨‍🎤 Nombre del artista: ");
            string nombre = Console.ReadLine();
            Console.Write("📧 Email del artista: ");
            string email = Console.ReadLine();
            Console.Write("📝 Biografía: ");
            string biografia = Console.ReadLine();

            var artista = new Artista(nombre, email, biografia);
            plataforma.Catalogo.Artistas.Add(artista);
            Console.WriteLine("✅ Artista registrado exitosamente");

            // Paso 2: Crear canciones para el artista
            Console.WriteLine("\n🎵 Vamos a crear canciones para el artista:");
            
            for (int i = 1; i <= 2; i++)
            {
                Console.Write($"Título de la canción {i}: ");
                string titulo = Console.ReadLine();
                
                Console.WriteLine("Géneros disponibles:");
                var generos = Enum.GetValues<GeneroMusical>();
                for (int j = 0; j < generos.Length; j++)
                {
                    Console.WriteLine($"{j + 1}. {generos[j]}");
                }
                
                Console.Write("Selecciona el género (número): ");
                if (int.TryParse(Console.ReadLine(), out int generoIndex) && 
                    generoIndex > 0 && generoIndex <= generos.Length)
                {
                    var genero = generos[generoIndex - 1];
                    var cancion = new Cancion(titulo, artista, genero);
                    
                    // Simular algunas reproducciones
                    cancion.Reproducciones = new Random().Next(1000, 50000);
                    
                    plataforma.Catalogo.AgregarCancion(cancion);
                    artista.AgregarCancion(cancion);
                }
            }

            // Paso 3: Crear álbum
            Console.Write("\n🎼 Nombre del álbum: ");
            string nombreAlbum = Console.ReadLine();
            
            Console.WriteLine("Tipos de álbum:");
            var tipos = Enum.GetValues<TipoAlbum>();
            for (int i = 0; i < tipos.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {tipos[i]}");
            }
            
            Console.Write("Selecciona el tipo (número): ");
            if (int.TryParse(Console.ReadLine(), out int tipoIndex) && 
                tipoIndex > 0 && tipoIndex <= tipos.Length)
            {
                var tipo = tipos[tipoIndex - 1];
                var album = new Album(nombreAlbum, artista, tipo);
                
                // Agregar las canciones del artista al álbum
                foreach (var cancion in artista.Canciones)
                {
                    album.AgregarCancion(cancion);
                }
                
                artista.PublicarAlbum(album);
                plataforma.Catalogo.AgregarAlbum(album);
            }

            // Paso 4: Simular seguidores
            if (plataforma.UsuariosRegistrados.Count > 0)
            {
                Console.WriteLine("\n👥 Usuarios disponibles para seguir al artista:");
                for (int i = 0; i < plataforma.UsuariosRegistrados.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {plataforma.UsuariosRegistrados[i].Nombre}");
                }

                Console.Write("Selecciona un usuario para que siga al artista (número): ");
                if (int.TryParse(Console.ReadLine(), out int usuarioIndex) && 
                    usuarioIndex > 0 && usuarioIndex <= plataforma.UsuariosRegistrados.Count)
                {
                    var usuario = plataforma.UsuariosRegistrados[usuarioIndex - 1];
                    usuario.SeguirArtista(artista);
                }
            }
            else
            {
                Console.WriteLine("\n📝 No hay usuarios registrados. El artista será seguido automáticamente por usuarios de prueba.");
                // Crear un usuario de prueba
                var usuarioPrueba = new Usuario("Fan #1", "fan1@test.com");
                plataforma.RegistrarUsuario(usuarioPrueba);
                usuarioPrueba.SeguirArtista(artista);
            }

            // Paso 5: Mostrar estadísticas del artista
            Console.WriteLine("\n📊 === INFORMACIÓN DEL ARTISTA ===");
            Console.WriteLine(artista.GetInfo());

            // Paso 6: Generar radio del artista
            plataforma.GenerarRadio(artista);

            Console.WriteLine("\n✅ ¡Flujo completado exitosamente!");
        }

        static void RegistrarUsuario()
        {
            Console.Write("👤 Nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("📧 Email: ");
            string email = Console.ReadLine();

            var usuario = new Usuario(nombre, email);
            plataforma.RegistrarUsuario(usuario);
        }

        static void BuscarYReproducirCancion()
        {
            Console.Write("🔍 Ingresa el título de la canción: ");
            string query = Console.ReadLine();
            var canciones = plataforma.BuscarCancion(query);

            if (canciones.Count > 0)
            {
                Console.WriteLine("\nSelecciona una canción para reproducir:");
                for (int i = 0; i < canciones.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {canciones[i].Titulo} - {canciones[i].Artista.Nombre}");
                }

                Console.Write("Número: ");
                if (int.TryParse(Console.ReadLine(), out int seleccion) && 
                    seleccion > 0 && seleccion <= canciones.Count)
                {
                    canciones[seleccion - 1].Reproducir();
                }
            }
        }

        static void MostrarRecomendaciones()
        {
            if (plataforma.UsuariosRegistrados.Count == 0)
            {
                Console.WriteLine("❌ No hay usuarios registrados");
                return;
            }

            Console.WriteLine("👥 Usuarios disponibles:");
            for (int i = 0; i < plataforma.UsuariosRegistrados.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {plataforma.UsuariosRegistrados[i].Nombre}");
            }

            Console.Write("Selecciona un usuario (número): ");
            if (int.TryParse(Console.ReadLine(), out int index) && 
                index > 0 && index <= plataforma.UsuariosRegistrados.Count)
            {
                var usuario = plataforma.UsuariosRegistrados[index - 1];
                var recomendaciones = plataforma.RecomendarCanciones(usuario);
                
                if (recomendaciones.Count > 0)
                {
                    Console.WriteLine("\n🎵 Recomendaciones:");
                    foreach (var cancion in recomendaciones)
                    {
                        Console.WriteLine($"   • {cancion.Titulo} - {cancion.Artista.Nombre} ({cancion.Genero})");
                    }
                }
            }
        }

        static void GenerarRadio()
        {
            if (plataforma.Catalogo.Artistas.Count == 0)
            {
                Console.WriteLine("❌ No hay artistas en el catálogo");
                return;
            }

            Console.WriteLine("👨‍🎤 Artistas disponibles:");
            for (int i = 0; i < plataforma.Catalogo.Artistas.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {plataforma.Catalogo.Artistas[i].Nombre}");
            }

            Console.Write("Selecciona un artista (número): ");
            if (int.TryParse(Console.ReadLine(), out int index) && 
                index > 0 && index <= plataforma.Catalogo.Artistas.Count)
            {
                var artista = plataforma.Catalogo.Artistas[index - 1];
                plataforma.GenerarRadio(artista);
            }
        }

        static void MostrarCatalogo()
        {
            Console.WriteLine("📚 === CATÁLOGO MUSICAL ===");
            Console.WriteLine($"\n🎵 CANCIONES ({plataforma.Catalogo.Canciones.Count}):");
            foreach (var cancion in plataforma.Catalogo.Canciones)
            {
                Console.WriteLine($"   • {cancion.Titulo} - {cancion.Artista.Nombre} ({cancion.Genero}) [{cancion.GetDuracionFormateada()}]");
            }

            Console.WriteLine($"\n👨‍🎤 ARTISTAS ({plataforma.Catalogo.Artistas.Count}):");
            foreach (var artista in plataforma.Catalogo.Artistas)
            {
                Console.WriteLine($"   • {artista.Nombre} - {artista.Canciones.Count} canciones, {artista.Seguidores.Count} seguidores");
            }

            if (plataforma.Catalogo.Albumes.Count > 0)
            {
                Console.WriteLine($"\n🎼 ÁLBUMES ({plataforma.Catalogo.Albumes.Count}):");
                foreach (var album in plataforma.Catalogo.Albumes)
                {
                    Console.WriteLine($"   • {album.Titulo} - {album.Artista.Nombre} ({album.Tipo}) - {album.Canciones.Count} canciones");
                }
            }
        }
    }
}