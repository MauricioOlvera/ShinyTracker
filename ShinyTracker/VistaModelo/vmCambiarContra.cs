using Firebase.Auth;
using ShinyTracker.Vista;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ShinyTracker.VistaModelo
{
    public class vmCambiarContra : BaseViewModel
    {
        #region VARIABLES

        string Llave = "AIzaSyAftvOCN2rxwJEcZzF8HrxlcyHw4f1pfSY";

        string txtCorreo;

        #endregion

        #region CONSTRUCTOR

        public vmCambiarContra(INavigation navigation)
        {
            Navigation = navigation;
        }

        #endregion

        #region OBJETOS

        public string Correo
        {
            get { return txtCorreo; }
            set { SetValue(ref txtCorreo, value); }
        }

        #endregion

        #region PROCESOS
        public async Task ProcesoEnviarCorreo()
        {
            if (string.IsNullOrEmpty(this.txtCorreo))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debe ingresar un correo.",
                    "Aceptar");
                return;
            }

            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Llave));
                await authProvider.SendPasswordResetEmailAsync(Correo.ToString());

                await App.Current.MainPage.DisplayAlert("Alerta", $"Correo enviado con exito!", "Aceptar");

                await Regresar();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", $"Error {ex}", "OK");
            }

        }

        public async Task Regresar()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public void ProcesoSimple()
        {

        }

        #endregion

        #region COMANDOS
        public ICommand EnviarCorreoCommand => new Command(async () => await ProcesoEnviarCorreo());

        public ICommand RegresarCommand => new Command(async () => await Regresar());

        public ICommand ProcesoSimpcommand => new Command(ProcesoSimple);

        #endregion
    }
}