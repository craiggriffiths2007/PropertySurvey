using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LockTypeChoose : ContentPage
	{
		public LockTypeChoose ()
		{
			InitializeComponent ();
		}

        private async void general_button_clicked(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new LockItem(t_locking_item_mode.general_lock), this);
            await Navigation.PopAsync(false);
        }

        private async void multi_button_clicked(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new LockItem(t_locking_item_mode.multipoint_lock), this);
            await Navigation.PopAsync(false);
        }
    }
}