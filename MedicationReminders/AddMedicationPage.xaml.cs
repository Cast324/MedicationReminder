using System;
using System.Collections.Generic;
using System.Globalization;
using MedicationReminders.Model;
using SQLite;
using Xamarin.Forms;

namespace MedicationReminders
{
    public partial class AddMedicationPage : ContentPage
    {
        public AddMedicationPage()
        {
            InitializeComponent();
            List<String> items = new List<string>(new string[] { "Hourly", "Daily", "Weekly" });
            
            frequencyPicker.ItemsSource = items;
            frequencyPicker.SelectedItem = items[0];
        }

        void saveButton_Clicked(System.Object sender, System.EventArgs e)
        {
            if (medicationEntry.Text.Length <= 0)
            {
                
            }
            else
            {
                var date = datePicker.Date.ToString("MM/dd");
                var time = timePicker.Time.ToString();
                var timeFromInput = DateTime.ParseExact(time, "HH:mm:ss", null, DateTimeStyles.None);
                var timeFormated = timeFromInput.ToString("h:mm tt");
                var datetime = date + " " + timeFormated;
                DateTime notificationDate = datePicker.Date + timePicker.Time;

                Medication newMed = new Medication()
                {
                    Name = medicationEntry.Text,
                    DateTime = datetime,
                    Time = time,
                    Frequency = frequencyPicker.SelectedItem.ToString()
                };

                using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
                {
                    conn.CreateTable<Medication>();
                    int rowsAffected = conn.Insert(newMed);

                    if (rowsAffected > 0)
                    {
                        INotificationManager notificationManager = DependencyService.Get<INotificationManager>();
                        notificationManager.SendNotification("Medication Reminder:", "Time to take " + medicationEntry.Text.ToString(), notificationDate);
                        Navigation.PopAsync();
                    }
                    else
                        DisplayAlert("Failure", "Med was not saved, please try again", "Ok");
                }
            }
            
        }
    }
}
