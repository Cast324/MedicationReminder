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
                var sortedList = posts.OrderBy(o => o.DateTime).ToList();

                medicationListView.ItemsSource = sortedList;

            }
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