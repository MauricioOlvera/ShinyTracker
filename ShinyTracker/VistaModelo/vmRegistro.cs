using Firebase.Auth;
using Newtonsoft.Json;
using ShinyTracker.Datos;
using ShinyTracker.Modelo;
using ShinyTracker.Vista;
using ShinyTracker.Vista.Inicio;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ShinyTracker.VistaModelo
{
    public class vmRegistro : BaseViewModel
    {
        #region VARIABLES

        string Llave = "AIzaSyAftvOCN2rxwJEcZzF8HrxlcyHw4f1pfSY";

        public string txtUsuario;
        public string txtCorreo;
        public string txtContraseña;

        public bool isRunning;
        public bool isVisible;
        public bool isEnabled;

        #endregion

        #region CONSTRUCTOR

        public vmRegistro(INavigation navigation)
        {
            Navigation = navigation;
        }

        #endregion

        #region OBJETOS

        public string Usuario
        {
            get { return txtUsuario; }
            set { SetValue(ref txtUsuario, value); }
        }

        public string Correo
        {
            get { return txtCorreo; }
            set { SetValue(ref txtCorreo, value); }
        }

        public string Contraseña
        {
            get { return txtContraseña; }
            set { SetValue(ref txtContraseña, value); }
        }

        public bool IsVisibleTxt
        {
            get { return this.isVisible; }
            set { SetValue(ref this.isVisible, value); }
        }

        public bool IsEnabledTxt
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        public bool IsRunningTxt
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        #endregion

        #region PROCESOS
        public async Task Registrar()
        {
            var funcion = new DatosUsuario();
            var Parametros = new ModelUsuario();

            Parametros.Usuario = Usuario;
            Parametros.Correo = Correo;
            Parametros.CantidadPokemon = "0";
            Parametros.ImagenUsuario = "https://RNYPh5C/defalt-Avatar.jpg";

            await funcion.InsertUsuario(Parametros);
        }

        public void ProcesoSimple()
        {

        }

        public async Task ProcesoRegistro()
        {
            if (string.IsNullOrEmpty(this.txtCorreo))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debe de ingresar un correo.",
                    "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.txtContraseña))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debe de ingresar una contraseña.",
                    "Aceptar");
                return;
            }

            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Llave));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(Correo.ToString(), Contraseña.ToString());
                string gettoken = auth.FirebaseToken;

                await Registrar();

                //await Application.Current.MainPage.Navigation.PushAsync(new LogIn());
                await App.Current.MainPage.DisplayAlert("Alerta", "Registro exitoso, inicia sesion para continuar.", "Aceptar");

                await ProcesoIrLogIn();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alerta", ex.Message, "Aceptar");
            }
        }

        public async Task ProcesoIrLogIn()
        {
            //await Application.Current.MainPage.Navigation.PushAsync(new LogIn());
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        #endregion

        #region COMANDOS

        public ICommand ProcesoSimpcommand => new Command(ProcesoSimple);

        public ICommand RegistrarCommand => new Command(async () => await ProcesoRegistro());

        public ICommand IrLogInCommand => new Command(async () => await ProcesoIrLogIn());

        #endregion
    }
}