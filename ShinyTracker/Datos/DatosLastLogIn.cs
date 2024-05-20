using System;
using System.Collections.Generic;
using System.Text;
using ShinyTracker.Modelo;
using ShinyTracker.Conexion;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;

namespace LastLogInTracker.Datos
{
    public class DatosLastLogIn
    {
        public async Task InsertLastLogIn(ModelLastLogIn Parametros)
        {
            await CConexion.firebase
                .Child("LastLogIn")
                .PostAsync(new ModelLastLogIn()
                {
                    IdUsuario = Parametros.IdUsuario,
                    Usuario = Parametros.Usuario
                });
        }

        public async Task<List<ModelLastLogIn>> ConsulLastLogIn()
        {
            return (await CConexion.firebase
                .Child("LastLogIn")
                .OnceAsync<ModelLastLogIn>())
                .Where(a => a.Key != "Modelo")
                .Select(item => new ModelLastLogIn
                {
                    IdLastLogIn = item.Key,
                    IdUsuario = item.Object.IdUsuario,
                    Usuario = item.Object.Usuario
                }).ToList();
        }

        public async Task DeleteLastLogIn(string Id)
        {
            await CConexion.firebase
                .Child("LastLogIn")
                .Child(Id)
                .DeleteAsync();
        }
    }
}
