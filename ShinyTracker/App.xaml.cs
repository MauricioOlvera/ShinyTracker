using ShinyTracker.Vista;
using ShinyTracker.Vista.Inicio;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShinyTracker
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterDet { get; set; }  

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LogIn());
            //MainPage = new NavigationPage(new Inicio());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
