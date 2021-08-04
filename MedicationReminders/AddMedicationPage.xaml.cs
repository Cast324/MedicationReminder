using System;
using System.Collections.Generic;
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
            List<String> items = new List<string>();
            items.Add("Test");
            items.Add("Weekly");
            frequencyPicker.ItemsSource = items;
        }

        void saveButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Medication newMed = new Medication()
            {
                Name = medicationEntry.Text
            };

            var date = datePicker.Date.ToString("MM/dd");
            var time = timePicker.Time.ToString();
            timePicker.Time = TimeSpan.Parse(time);

            //using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            //{
            //    conn.CreateTable<Medication>();
            //    int rowsAffected = conn.Insert(newMed);

            //    if (rowsAffected > 0)
            //    {
            //        medicationEntry.Text = string.Empty;
            //        DisplayAlert("Success", "Med saved", "Ok");
            //        Navigation.PopAsync();
            //    }
            //    else
            //        DisplayAlert("Failure", "Med was not saved, please try again", "Ok");
            //}
        }
    }
}
