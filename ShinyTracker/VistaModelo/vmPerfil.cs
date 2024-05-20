using LastLogInTracker.Datos;
using Plugin.Media.Abstractions;
using Plugin.Media;
using ShinyTracker.Conexion;
using ShinyTracker.Datos;
using ShinyTracker.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ShinyTracker.VistaModelo
{
    public class vmPerfil : BaseViewModel
    {
        #region VARIABLES

        string fotoUrl;
        string txtUsuario;
        string txtCantPokemon;
        string txtUid;
        string txtCorreo;
        ImageSource _Imagen;
        List<ModelLastLogIn> lstLastLogIn;
        List<ModelUsuario> lstUsuario;
        List<ModelShiny> lstPokemon;

        #endregion

        #region CONSTRUCTOR

        public vmPerfil(INavigation navigation)
        {
            Navigation = navigation;
            MostrarUsuario();
        }

        #endregion

        #region OBJETOS

        public string FotoUrl
        {
            get { return fotoUrl; }
            set { SetValue(ref fotoUrl, value); }
        }

        public string Usuario
        {
            get { return txtUsuario; }
            set { SetValue(ref txtUsuario, value); }
        }

        public string CantPokemon
        {
            get { return txtCantPokemon; }
            set { SetValue(ref txtCantPokemon, value); }
        }

        public string Uid
        {
            get { return txtUid; }
            set { SetValue(ref txtUid, value); }
        }

        public string Correo
        {
            get { return txtCorreo; }
            set { SetValue(ref txtCorreo, value); }
        }

        public ImageSource Imagen
        {
            get { return _Imagen; }
            set
            {
                _Imagen = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region PROCESOS
        public async Task MostrarUsuario()
        {
            MostrarPokemon();

            lstLastLogIn = await ConsultarLastLogIn();
            var funcion = new DatosUsuario();

            lstUsuario = await funcion.ConsulUsuario(lstLastLogIn[0].IdUsuario);

            PonerDatos(lstUsuario);

        }

        public async Task MostrarPokemon()
        {
            lstLastLogIn = await ConsultarLastLogIn();

            var funcion = new DatosShiny();
            lstPokemon = await funcion.ConsulShinies(lstLastLogIn[0].IdUsuario);

            CantPokemon = lstPokemon.Count.ToString();

            await Actualizar();
        }

        public async Task<List<ModelLastLogIn>> ConsultarLastLogIn()
        {
            var funcion = new DatosLastLogIn();
            var lst = await funcion.ConsulLastLogIn();

            return lst;
        }

        public async Task<ImageSource> SeleccionarFoto()
        {
            MediaFile file;

            await CrossMedia.Current.Initialize();

            file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Medium
            });

            //if(file == null)
            //{
            //    return;
            //}

            FotoUrl = await SubirImagen(file.GetStream(), Path.GetFileName(file.Path));

            var imgSource = ImageSource.FromStream(file.GetStream);

            return imgSource;
        }

        public async Task PonerFoto()
        {
            Imagen = await SeleccionarFoto();

            await Actualizar();

            MostrarUsuario();
        }

        public async Task<string> SubirImagen(Stream Img, string fileName)
        {
            var imageUrl = await CConexion.firebaseStorage
                .Child("Imagenes")
                .Child(fileName)
                .PutAsync(Img);

            return imageUrl;
        }

        public async Task Actualizar()
        {
            var funcion = new DatosUsuario();
            var Parametros = new ModelUsuario();

            Parametros.Uid = Uid;
            Parametros.Usuario = Usuario;
            Parametros.ImagenUsuario = FotoUrl;
            Parametros.CantidadPokemon = CantPokemon;
            Parametros.Correo = Correo;

            await funcion.ActualizarUsuario(Parametros, Uid);
        }

        public void PonerDatos(List<ModelUsuario> lst)
        {
            FotoUrl = lst[0].ImagenUsuario;
            Usuario = lst[0].Usuario;
            CantPokemon = lst[0].CantidadPokemon;
            Correo = lst[0].Correo;
            Uid = lst[0].Uid;
        }

        public async Task Volver()
        {
            await Navigation.PopAsync();
        }

        #endregion

        #region COMANDOS
        public ICommand PonerFotoCommand => new Command(async () => await PonerFoto());

        public ICommand VolverCommand => new Command(async () => await Volver());

        #endregion
    }
}