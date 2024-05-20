using Plugin.Media.Abstractions;
using Plugin.Media;
using ShinyTracker.Modelo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ShinyTracker.Conexion;
using System.IO;
using ShinyTracker.Datos;

namespace ShinyTracker.VistaModelo
{
    public class vmEditarPokemon : BaseViewModel
    {
        #region VARIABLES

        string txtPokemon;
        string txtNumero = "#";
        string txtJuego;
        string txtFecha;
        ImageSource _Imagen;
        string txtImagenUrl;

        public ModelShiny ParametrosRecibe { get; set; }

        #endregion

        #region CONSTRUCTOR

        public vmEditarPokemon(INavigation navigation, ModelShiny parametrosTrae)
        {
            Navigation = navigation;
            ParametrosRecibe = parametrosTrae;
            PasarDatos();
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
            var funcion = new DatosShiny();
            var Parametros = new ModelShiny();

            Parametros.UidUsuario = ParametrosRecibe.UidUsuario;
            Parametros.Pokemon = Pokemon;
            Parametros.NoPokedex = Numero;
            Parametros.Juego = Juego;
            Parametros.FechaCaptura = Fecha;
            Parametros.Imagen = ImagenUrl;
            Parametros.IdShiny = ParametrosRecibe.IdShiny;

            await funcion.ActualizarShiny(Parametros, ParametrosRecibe.IdShiny);

            await App.Current.MainPage.DisplayAlert("Alerta", "¡Pokemon actualizado con exito!", "Aceptar");

            await Volver();
        }

        public async Task PonerFoto()
        {
            Imagen = await SeleccionarFoto();
        }

        public async Task Volver()
        {
            await Navigation.PopAsync();
        }

        public void PasarDatos()
        {
            Pokemon = ParametrosRecibe.Pokemon;
            Numero = ParametrosRecibe.NoPokedex;
            Juego = ParametrosRecibe.Juego;
            Fecha = ParametrosRecibe.FechaCaptura;
            ImagenUrl = ParametrosRecibe.Imagen;
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

        #endregion

        #region COMANDOS
        public ICommand ActualizarCommand => new Command(async () => await Actualizar());

        public ICommand PonerFotoCommand => new Command(async () => await PonerFoto());

        public ICommand VolverCommand => new Command(async () => await Volver());

        #endregion
    }
}