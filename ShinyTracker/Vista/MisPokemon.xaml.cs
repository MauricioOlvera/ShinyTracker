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
    public partial class MisPokemon : ContentPage
    {
        vmMisPokemon vm;
        public MisPokemon()
        {
            InitializeComponent();
            vm = new vmMisPokemon(Navigation);
            BindingContext = vm;
            this.Appearing += MisPokemon_Appearing;
        }

        private async void MisPokemon_Appearing(object sender, EventArgs e)
        {
            await vm.MostrarPokemon();
        }
    }
}