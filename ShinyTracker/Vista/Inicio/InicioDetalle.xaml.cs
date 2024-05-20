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
    public partial class InicioDetalle : ContentPage
    {
        public InicioDetalle()
        {
            InitializeComponent();
            //webView.Source = "https://www.wikidex.net/wiki/Pok%C3%A9mon_variocolor"; 
        }
    }
}