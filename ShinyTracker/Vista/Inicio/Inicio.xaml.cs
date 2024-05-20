using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShinyTracker.Vista.Inicio
{
    public partial class Inicio : MasterDetailPage
    {
        public Inicio()
        {
            InitializeComponent();
            this.Master = new InicioMaster();
            this.Detail = new NavigationPage(new InicioDetalle());
            App.MasterDet = this;
        }
    }
}