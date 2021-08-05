using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedicationReminders
{
    public partial class App : Application
    {

        public static string DatabaseLocation = string.Empty;
        public App()
        {
            InitializeComponent();
            DependencyService.Get<INotificationManager>().Initialize();

            MainPage = new NavigationPage(new HomePage());
        }

        public App(string databaseLocation)
        {
            InitializeComponent();
            DependencyService.Get<INotificationManager>().Initialize();

            MainPage = new NavigationPage(new HomePage());

            DatabaseLocation = databaseLocation;
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
