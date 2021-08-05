using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicationReminders.Model;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MedicationReminders
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MedicationPage : ContentPage
    {
        INotificationManager notificationManager;
        int notificationNumber = 0;
        public MedicationPage()
        {
            InitializeComponent();
            medicationListView.SelectionMode = ListViewSelectionMode.None;
            notificationManager = DependencyService.Get<INotificationManager>();
            notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                ShowNotification(evtData.Title, evtData.Message);
            };

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Medication>();
                var posts = conn.Table<Medication>().ToList();

                medicationListView.ItemsSource = posts;

            }

            notificationNumber++;
            string title = $"Local Notification #{notificationNumber}";
            string message = $"You have now received {notificationNumber} notifications!";
            notificationManager.SendNotification(title, message, DateTime.Now.AddSeconds(10));
        }

        void ShowNotification(string title, string message)
        {
           // Show Notification
        }

        private void addToolBarButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}