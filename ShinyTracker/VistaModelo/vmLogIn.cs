using Firebase.Auth;
using LastLogInTracker.Datos;
using Newtonsoft.Json;
using ShinyTracker.Datos;
using ShinyTracker.Modelo;
using ShinyTracker.Vista;
using ShinyTracker.Vista.Inicio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ShinyTracker.VistaModelo
{
    public class vmLogIn : BaseViewModel
    {
        #region VARIABLES

        public string Llave = "AIzaSyAftvOCN2rxwJEcZzF8HrxlcyHw4f1pfSY";

        public string txtCorreo;
        public string txtContraseña;
        List<ModelUsuario> lstUsuario;

        public bool isRunning;
        public bool isVisible;
        public bool isEnabled;

        #endregion

        #region CONSTRUCTOR

        public vmLogIn(INavigation navigation)
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

        public string Contraseña
        {
            get { return txtContraseña; }
            set { SetValue(ref txtContraseña, value); }
        }

        public bool IsRunningTxt
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
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

        public List<ModelUsuario> ListaUsuario
        {
            get { return lstUsuario; }
            set { SetValue(ref lstUsuario, value); }
        }

        #endregion

        #region PROCESOS
        public async Task ProcesoLogIn()
        {
            if (string.IsNullOrEmpty(this.txtCorreo))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debe ingresar un correo.",
                    "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.txtContraseña))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debe ingresar una contraseña.",
                    "Aceptar");
                return;
            }

            this.IsVisibleTxt = true;
            this.IsRunningTxt = true;

            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Llave));
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(Correo.ToString(), Contraseña.ToString());
                var content = await auth.GetFreshAuthAsync();
                var serializedcontnet = JsonConvert.SerializeObject(content);

                Preferences.Set("MyFirebaseRefreshToken", serializedcontnet);

                lstUsuario = await ConsultarUsuario(Correo);

                await Registrar();

                await App.Current.MainPage.DisplayAlert("Alerta", "¡Inicio de sesion exitoso!\nBienvenid@ " + lstUsuario[0].Usuario, "Aceptar");

                await Application.Current.MainPage.Navigation.PushAsync(new Inicio());
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alerta", ex.ToString(), "Aceptar");
            }

            this.IsVisibleTxt = false;
            this.IsRunningTxt = false;
        }

        public async Task IrCambiarContra()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CambiarContra());
        }

        public async Task IrRegistro()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Registro());
            //await Application.Current.MainPage.Navigation.PopAsync();
        }

        public async Task<List<ModelUsuario>> ConsultarUsuario(string Correo)
        {
            var funcion = new DatosUsuario();
            var lst = await funcion.ConsulUsuarioCorreo(Correo);

            return lst;
        }

        public async Task Registrar()
        {
            var funcion = new DatosLastLogIn();
            var Parametros = new ModelLastLogIn();

            Parametros.IdUsuario = lstUsuario[0].Uid;
            Parametros.Usuario = lstUsuario[0].Usuario;

            await funcion.InsertLastLogIn(Parametros);
        }

        #endregion

        #region COMANDOS
        public ICommand LogInCommand => new Command(async () => await ProcesoLogIn());

        public ICommand IrCambiarContraCommand => new Command(async () => await IrCambiarContra());

        public ICommand IrRegistroCommand => new Command(async () => await IrRegistro());

        #endregion
    }
}