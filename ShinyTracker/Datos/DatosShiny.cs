using System;
using System.Collections.Generic;
using System.Text;
using ShinyTracker.Modelo;
using ShinyTracker.Conexion;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;

namespace ShinyTracker.Datos
{
    public class DatosShiny
    {
        public async Task InsertarShiny(ModelShiny Parametros)
        {
            await CConexion.firebase
                .Child("Shiny")
                .PostAsync(new ModelShiny()
                {
                    UidUsuario = Parametros.UidUsuario,
                    Pokemon = Parametros.Pokemon,
                    NoPokedex = Parametros.NoPokedex,
                    Juego = Parametros.Juego,
                    FechaCaptura = Parametros.FechaCaptura,
                    Imagen = Parametros.Imagen
                });
        }

        public async Task<List<ModelShiny>> ConsulShinies(string IdUsuario)
        {
            return (await CConexion.firebase
                .Child("Shiny")
                .OnceAsync<ModelShiny>())
                .Where(a => a.Object.UidUsuario == IdUsuario)
                .Select(item => new ModelShiny
                {
                    IdShiny = item.Key,
                    UidUsuario = item.Object.UidUsuario,
                    Pokemon = item.Object.Pokemon,
                    NoPokedex = item.Object.NoPokedex,
                    Juego = item.Object.Juego,
                    FechaCaptura = item.Object.FechaCaptura,
                    Imagen = item.Object.Imagen
                }).ToList();
        }

        public async Task<List<ModelShiny>> ConsulShiniesId(string Id)
        {
            return (await CConexion.firebase
                .Child("Shiny")
                .OnceAsync<ModelShiny>())
                .Where(a => a.Key == Id)
                .Select(item => new ModelShiny
                {
                    IdShiny = item.Key,
                    UidUsuario = item.Object.UidUsuario,
                    Pokemon = item.Object.Pokemon,
                    NoPokedex = item.Object.NoPokedex,
                    Juego = item.Object.Juego,
                    FechaCaptura = item.Object.FechaCaptura,
                    Imagen = item.Object.Imagen
                }).ToList();
        }

        public async Task ActualizarShiny(ModelShiny Parametros, string Id)
        {
            await CConexion.firebase
                .Child("Shiny")
                .Child(Id)
                .PutAsync(new ModelShiny()
                {
                    UidUsuario = Parametros.UidUsuario,
                    Pokemon = Parametros.Pokemon,
                    NoPokedex = Parametros.NoPokedex,
                    Juego = Parametros.Juego,
                    FechaCaptura = Parametros.FechaCaptura,
                    Imagen = Parametros.Imagen
                });
        }
    }
}