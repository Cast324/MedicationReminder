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
        public MedicationPage()
        {
            InitializeComponent();
            //List<string> list = new List<string>();
            //list.Add("Testing");
            //list.Add("Maybe");
            //list.First();

            //medicationListView.ItemsSource = list;
            medicationListView.SelectionMode = ListViewSelectionMode.None;
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
        }

        private void addToolBarButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}