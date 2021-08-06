using System;
using System.Collections.Generic;
using System.Globalization;
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
            medicationListView.SelectionMode = ListViewSelectionMode.Single;
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

            refreshTableView();
        }

        async void ShowNotification(string title, string message)
        {
            bool answer = await DisplayAlert(message, "Did you take your medication?", "Yes", "Later");
            string[] medication = message.Split(' ');
            string fullMedicationName = string.Empty;
            for (int i = 3; i <= medication.Length - 1; i++)
            {
                if (i == medication.Length - 1)
                    fullMedicationName = fullMedicationName + medication[i];
                else
                    fullMedicationName = fullMedicationName + medication[i] + " ";

            }
            if (answer)
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Medication>();
                    var item = from s in conn.Table<Medication>()
                               where s.Name.Equals(fullMedicationName)
                                select s;
                    if (item.FirstOrDefault() != null)
                    {
                        var newMedReminder = item.FirstOrDefault();
                        var newDate = DateTime.Now;
                        switch (item.FirstOrDefault().Frequency)
                        {
                            case "Hourly":
                                newDate = DateTime.ParseExact(newMedReminder.DateTime, "MM/dd h:mm tt", null, DateTimeStyles.None).AddHours(1);
                                newMedReminder.Time = newDate.TimeOfDay.ToString();
                                newMedReminder.DateTime = newDate.ToString("MM/dd h:mm tt");
                                break;
                            case "Daily":
                                newDate = DateTime.ParseExact(newMedReminder.DateTime, "MM/dd h:mm tt", null, DateTimeStyles.None).AddDays(1);
                                newMedReminder.DateTime = newDate.ToString("MM/dd h:mm tt");
                                break;
                            case "Weekly":
                                newDate = DateTime.ParseExact(newMedReminder.DateTime, "MM/dd h:mm tt", null, DateTimeStyles.None).AddDays(7);
                                newMedReminder.DateTime = newDate.ToString("MM/dd h:mm tt");
                                break;
                            default:
                                break;
                        }
                            
                        conn.Delete<Medication>(item.FirstOrDefault().ID);
                        int rowsAffected = conn.Insert(newMedReminder);

                        if (rowsAffected > 0)
                        {
                            INotificationManager notificationManager = DependencyService.Get<INotificationManager>();
                            notificationManager.SendNotification("Medication Reminder:", "Time to take " + newMedReminder.Name, newDate);
                        }
                    }
                        

                }

                refreshTableView();
            }
        }

        private void addToolBarButton_Clicked(object sender, EventArgs e)
        {

        }

        async private void medListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            var selectedMed = medicationListView.SelectedItem as Medication;

            if (selectedMed != null)
            {
                bool answer = await DisplayAlert("Delete Medication Reminder", "Did you want to delete this Medication Reminder?", "Yes", "No");
                if (answer)
                {
                    using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                    {
                        conn.CreateTable<Medication>();
                        conn.Delete(selectedMed);

                    }

                    refreshTableView();

                }
            }
        }

        private void refreshTableView()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Medication>();
                var posts = conn.Table<Medication>().ToList();
                var sortedList = posts.OrderBy(o => o.DateTime).ToList();

                medicationListView.ItemsSource = sortedList;

            }
        }
    }
}