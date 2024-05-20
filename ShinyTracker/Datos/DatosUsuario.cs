using System;
using System.Collections.Generic;
using System.Text;
using ShinyTracker.Modelo;
using ShinyTracker.Conexion;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ShinyTracker.Datos
{
    public class DatosUsuario
    {
        public async Task InsertUsuario(ModelUsuario Parametros)
        {
            await CConexion.firebase
                .Child("Usuario")
                .PostAsync(new ModelUsuario()
                {
                    Usuario = Parametros.Usuario,
                    Correo = Parametros.Correo,
                    ImagenUsuario = Parametros.ImagenUsuario,
                    CantidadPokemon = Parametros.CantidadPokemon
                }
                );
        }

        public async Task<List<ModelUsuario>> ConsulUsuario(string Uid)
        {
            return (await CConexion.firebase
                .Child("Usuario")
                .OnceAsync<ModelUsuario>())
                .Where(a => a.Key == Uid)
                .Select(item => new ModelUsuario
                {
                    Uid = item.Key,
                    Usuario = item.Object.Usuario,
                    Correo = item.Object.Correo,
                    ImagenUsuario = item.Object.ImagenUsuario,
                    CantidadPokemon = item.Object.CantidadPokemon
                }
                ).ToList();
        }

        public async Task<List<ModelUsuario>> ConsulUsuarioCorreo(string Correo)
        {
            return (await CConexion.firebase
                .Child("Usuario")
                .OnceAsync<ModelUsuario>())
                .Where(a => a.Object.Correo == Correo)
                .Select(item => new ModelUsuario
                {
                    Uid = item.Key,
                    Usuario = item.Object.Usuario,
                    Correo = item.Object.Correo,
                    ImagenUsuario = item.Object.ImagenUsuario,
                    CantidadPokemon = item.Object.CantidadPokemon
                }
                ).ToList();
        }

        public async Task ActualizarUsuario(ModelUsuario Parametros, string Id)
        {
            await CConexion.firebase
                .Child("Usuario")
                .Child(Id)
                .PutAsync(new ModelUsuario()
                {
                    Uid = Parametros.Uid,
                    Usuario = Parametros.Usuario,
                    Correo = Parametros.Correo,
                    ImagenUsuario = Parametros.ImagenUsuario,
                    CantidadPokemon = Parametros.CantidadPokemon
                });
        }
    }
}
