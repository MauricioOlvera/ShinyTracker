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
    public partial class EditarPokemon : ContentPage
    {
        public EditarPokemon(ModelShiny Parametro)
        {
            InitializeComponent();
            BindingContext = new vmEditarPokemon(Navigation, Parametro);
        }
    }
}