using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShinyTracker.VistaModelo;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShinyTracker.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Perfil : ContentPage
    {
        vmPerfil vm;
        public Perfil()
        {
            InitializeComponent();
            vm = new vmPerfil(Navigation);
            BindingContext = vm;
            this.Appearing += Perfil_Appearing;
        }

        private async void Perfil_Appearing(object sender, EventArgs e)
        {
            await vm.MostrarUsuario();
        }
    }
}