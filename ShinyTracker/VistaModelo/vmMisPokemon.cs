using LastLogInTracker.Datos;
using ShinyTracker.Datos;
using ShinyTracker.Modelo;
using ShinyTracker.Vista;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ShinyTracker.VistaModelo
{
    public class vmMisPokemon : BaseViewModel
    {
        #region VARIABLES

        List<ModelLastLogIn> lstLastLogIn;
        List<ModelShiny> lstPokemon;

        #endregion

        #region CONSTRUCTOR

        public vmMisPokemon(INavigation navigation)
        {
            Navigation = navigation;
            //MostrarPokemon();
        }

        #endregion

        #region OBJETOS

        public List<ModelShiny> ListaPokemon
        {
            get { return lstPokemon; }
            set { SetValue(ref lstPokemon, value); }
        }

        #endregion

        #region PROCESOS
        public async Task MostrarPokemon()
        {
            lstLastLogIn = await ConsultarLastLogIn();

            var funcion = new DatosShiny();
            ListaPokemon = await funcion.ConsulShinies(lstLastLogIn[0].IdUsuario);
        }

        public async Task<List<ModelLastLogIn>> ConsultarLastLogIn()
        {
            var funcion = new DatosLastLogIn();
            var lst = await funcion.ConsulLastLogIn();

            return lst;
        }

        public async Task IrDetalle(ModelShiny Parametros)
        {
            await Navigation.PushAsync(new DetalleShiny(Parametros));
        }

        public async Task IrEditar(ModelShiny Parametros)
        {
            await Navigation.PushAsync(new EditarPokemon(Parametros));
        }

        public async Task Volver()
        {
            await Navigation.PopAsync();
        }

        #endregion

        #region COMANDOS
        public ICommand IrDetalleCommand => new Command<ModelShiny>(async (p) => await IrDetalle(p));

        public ICommand IrEditarCommand => new Command<ModelShiny>(async (p) => await IrEditar(p));

        public ICommand VolverCommand => new Command(async () => await Volver());

        //public ICommand ProcesoSimpcommand => new Command(ProcesoSimple);

        #endregion
    }
}