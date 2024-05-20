using LastLogInTracker.Datos;
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
    public class vmInicio : BaseViewModel
    {
        #region VARIABLES

        string _Texto;
        List<ModelLastLogIn> lstLastLogIn;

        #endregion

        #region CONSTRUCTOR

        public vmInicio(INavigation navigation)
        {
            Navigation = navigation;
        }

        #endregion

        #region OBJETOS

        public string Texto
        {
            get { return _Texto; }
            set { SetValue(ref _Texto, value); }
        }

        #endregion

        #region PROCESOS
        public async Task IrPerfil()
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PopAsync();
            await App.MasterDet.Detail.Navigation.PushAsync(new Perfil());
        }

        public async Task IrMisPokemon()
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PopAsync();
            await App.MasterDet.Detail.Navigation.PushAsync(new MisPokemon());
        }

        public async Task IrAgregar()
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PopAsync();
            await App.MasterDet.Detail.Navigation.PushAsync(new AgregarPokemon());
        }

        public async Task CerrarSesion()
        {
            App.MasterDet.IsPresented = false;
            var funcion = new DatosLastLogIn();
            lstLastLogIn = await ConsultarLastLogIn();
            await funcion.DeleteLastLogIn(lstLastLogIn[0].IdLastLogIn);
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public async Task<List<ModelLastLogIn>> ConsultarLastLogIn()
        {
            var funcion = new DatosLastLogIn();
            var lst = await funcion.ConsulLastLogIn();

            return lst;
        }

        #endregion

        #region COMANDOS
        public ICommand IrPerfilCommand => new Command(async () => await IrPerfil());

        public ICommand IrMisPokemonCommand => new Command(async () => await IrMisPokemon());

        public ICommand IrAgregarCommand => new Command(async () => await IrAgregar());

        public ICommand CerrarSesionCommand => new Command(async () => await CerrarSesion());

        #endregion
    }
}