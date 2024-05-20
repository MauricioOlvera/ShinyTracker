using ShinyTracker.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShinyTracker.Vista.Inicio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InicioMaster : ContentPage
    {
        public InicioMaster()
        {
            InitializeComponent();
            BindingContext = new vmInicio(Navigation);
        }
    }
}