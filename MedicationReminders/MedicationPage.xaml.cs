using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            List<string> list = new List<string>();
            list.Add("Testing");
            list.Add("Maybe");
            list.First();

            medicationListView.ItemsSource = list;
            medicationListView.SelectionMode = ListViewSelectionMode.None;
        }

        private void addToolBarButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}