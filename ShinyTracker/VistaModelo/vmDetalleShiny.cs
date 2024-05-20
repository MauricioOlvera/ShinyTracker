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
    public class vmDetalleShiny : BaseViewModel
    {
        #region VARIABLES

        string _Texto;
        public ModelShiny ParametrosRecibe { get; set; }

        #endregion

        #region CONSTRUCTOR

        public vmDetalleShiny(INavigation navigation, ModelShiny ParametrosTrae)
        {
            Navigation = navigation;
            ParametrosRecibe = ParametrosTrae;
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
        public async Task Volver()
        {
            await Navigation.PopAsync();
        }

        public void ProcesoSimple()
        {

        }

        #endregion

        #region COMANDOS
        public ICommand VolverCommand => new Command(async () => await Volver());

        public ICommand ProcesoSimpcommand => new Command(ProcesoSimple);

        #endregion
    }
}