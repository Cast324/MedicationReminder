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
    public partial class HomePage : TabbedPage
    {
        public HomePage()
        {
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
            
        }

        private void addToolBarButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}