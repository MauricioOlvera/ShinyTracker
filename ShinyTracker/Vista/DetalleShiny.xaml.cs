using ShinyTracker.Modelo;
using ShinyTracker.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShinyTracker.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleShiny : ContentPage
    {
        public DetalleShiny(ModelShiny Parametros)
        {
            InitializeComponent();
            BindingContext = new vmDetalleShiny(Navigation, Parametros);
        }
    }
}