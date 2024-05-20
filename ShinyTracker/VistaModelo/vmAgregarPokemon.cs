using Plugin.Media;
using Plugin.Media.Abstractions;
using ShinyTracker.Conexion;
using ShinyTracker.Modelo;
using ShinyTracker.Datos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using LastLogInTracker.Datos;

namespace ShinyTracker.VistaModelo
{
    public class vmAgregarPokemon : BaseViewModel
    {
        #region VARIABLES

        string txtPokemon;
        string txtNumero ="#";
        string txtJuego;
        string txtFecha;
        ImageSource _Imagen;
        List<ModelLastLogIn> lstLastLogIn;
        string txtImagenUrl;

        #endregion

        #region CONSTRUCTOR

        public vmAgregarPokemon(INavigation navigation)
        {
            Navigation = navigation;
        }

        #endregion

        #region OBJETOS

        public string Pokemon
        {
            get { return txtPokemon; }
            set { SetValue(ref txtPokemon, value); }
        }

        public string Numero
        {
            get { return txtNumero; }
            set { SetValue(ref txtNumero, value); }
        }

        public string Juego
        {
            get { return txtJuego; }
            set { SetValue(ref txtJuego, value); }
        }

        public string Fecha
        {
            get { return ConvertirFecha(txtFecha); }
            set { SetValue(ref txtFecha, value); }
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
        public string ImagenUrl
        {
            get { return txtImagenUrl; }
            set { SetValue(ref txtImagenUrl, value); }
        }

        public List<ModelLastLogIn> ListaLastLogIn
        {
            get { return lstLastLogIn; }
            set { SetValue(ref lstLastLogIn, value); }
        }

        #endregion

        #region PROCESOS
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

             ImagenUrl = await SubirImagen(file.GetStream(), Path.GetFileName(file.Path));

             var imgSource = ImageSource.FromStream(file.GetStream);

             return imgSource;
        }

        public async Task PonerFoto()
        {
            Imagen = await SeleccionarFoto();
        }

        public async Task<string> SubirImagen(Stream Img, string fileName)
        {
            var imageUrl = await CConexion.firebaseStorage
                .Child("Imagenes")
                .Child(fileName)
                .PutAsync(Img);

            return imageUrl;
        }

        public string ConvertirFecha(string fecha)
        {
            string fechaCon = "";

            if (fecha == null || fecha == "")
            {
                return fechaCon;
            }

            fechaCon = fecha.Substring(0, 10);

            return fechaCon;
        }

        public async Task Insertar()
        {
            var funcion = new DatosShiny();
            var Parametros = new ModelShiny();

            lstLastLogIn = await ConsultarLastLogIn();

            Parametros.UidUsuario = lstLastLogIn[0].IdUsuario;
            Parametros.Pokemon = Pokemon;
            Parametros.NoPokedex = Numero;
            Parametros.Juego = Juego;
            Parametros.FechaCaptura = Fecha;
            Parametros.Imagen = ImagenUrl;

            await funcion.InsertarShiny(Parametros);

            await App.Current.MainPage.DisplayAlert("Alerta", "¡Pokemon agregado con exito!", "Aceptar");

            await Volver();
        }

        public async Task<List<ModelLastLogIn>> ConsultarLastLogIn()
        {
            var funcion = new DatosLastLogIn();
            var lst = await funcion.ConsulLastLogIn();

            //if(lst == null)
            //{
            //    await App.Current.MainPage.DisplayAlert("Alerta", "no trajo na", "Aceptar");
            //}
            //else
            //{
            //    await App.Current.MainPage.DisplayAlert("Alerta", $"si trajo we ira {lst[0].Usuario}", "Aceptar");
            //}

            return lst;
        }

        public async Task Volver()
        {
            await Navigation.PopAsync();
        }

        #endregion

        #region COMANDOS
        public ICommand PonerFotoCommand => new Command(async () => await PonerFoto());
        public ICommand InsertarCommand => new Command(async () => await Insertar());
        public ICommand VolverCommand => new Command(async () => await Volver());

        #endregion
    }
}