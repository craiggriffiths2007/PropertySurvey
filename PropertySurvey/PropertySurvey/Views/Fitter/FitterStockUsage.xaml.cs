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
	public partial class FitterStockUsage : ContentPage
	{
		public FitterStockUsage ()
		{
			InitializeComponent ();

            List<string> branch_or_hired_list = new List<string>() { "...", "Branch", "Hired", "None" };

            BindingContext = App.net.HeaderRecord as Header;

            SetHireEquipment();
            SetList();
            SetInevitable();
            hireequipment.set_button_list(branch_or_hired_list);
        }

        protected override bool OnBackButtonPressed()
        {
            CheckInAndSave();

            //App.data.SaveVanChecksVan();

            // Navigation.PopAsync(false);

            return true;
        }

        private void CheckInAndSave()
        {
            string result = "";

            result = "Please complete :\n\n";

            if (App.CurrentApp.HeaderRecord.i_spare3 == 0)
                result = result + "Hire equipment y/n\n";

            if (App.CurrentApp.HeaderRecord.i_spare3 == 1 && App.CurrentApp.HeaderRecord.s_spare3.Length == 0)
            {
                result = result + "Hire equipment used\n";
            }

            if (App.CurrentApp.HeaderRecord.fitter_work.Length == 0)
                result = result + "Work completed\n";

            if (App.CurrentApp.HeaderRecord.parts_used.Length == 0)
                result = result + "Parts used\n";

            if (App.CurrentApp.HeaderRecord.ind == 0)
                result = result + "Inevitable damage\n";

            if (App.CurrentApp.HeaderRecord.ind == 1 && App.CurrentApp.HeaderRecord.inevitable_damage.Length == 0)
                result = result + "Explain Inevitable Damage\n";

            if (result.Length > 20)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var response = await Application.Current.MainPage.DisplayAlert("Missing information",
                        "Please complete :\n\n" + result + "\n\nClose Anyway?\n", "   Yes   ", "   No   ");
                    if (response)
                    {
                        App.CurrentApp.HeaderRecord.fbstockusagecomplete = false;
                        App.data.SaveHeader();
                        await this.Navigation.PopAsync(false);
                    }
                    else
                    {
                        App.CurrentApp.HeaderRecord.fbstockusagecomplete = false;
                    }
                });
            }
            else
            {
                App.CurrentApp.HeaderRecord.fbstockusagecomplete = true;
                App.data.SaveHeader();
                this.Navigation.PopAsync(false);
            }
        }


        private void ExcessYesNoLabel_OnSelectionChanged(object sender, EventArgs e)
        {
            SetHireEquipment();
        }

        private void SetHireEquipment()
        {
            if (App.CurrentApp.HeaderRecord.i_spare3 == 1)
            {
                hireequipment.IsVisible = true;
                listPicker1.IsVisible = true;
            }
            else
            {
                hireequipment.IsVisible = false;
                listPicker1.IsVisible = false;
            }
        }

        private void HiredYesNoLabel_OnSelectionChanged(object sender, EventArgs e)
        {
            SetList();
        }

        public void SetList()
        {
            List<string> equip1 = new List<string>() { "", "Conservatory Access Ladders", "Scaffold Tower" };
            List<string> equip2 = new List<string>() { "", "Tall Steps", "Conservatory Access Ladders", "3 Tier Ladder", "Scaffold Tower" };

            if (App.CurrentApp.HeaderRecord.i_spare2 == 1)
            {
                listPicker1.SetPickerItems(equip1);
            }

            if (App.CurrentApp.HeaderRecord.i_spare2 == 2)
            {
                listPicker1.SetPickerItems(equip2);
            }
            //listPicker1.Text = "";
        }

        private void hire_into_picker_changed(object sender, EventArgs e)
        {

        }

        private void InevitableYesNoLabel_OnSelectionChanged(object sender, EventArgs e)
        {
            SetInevitable();
        }

        private void SetInevitable()
        {
            if (App.CurrentApp.HeaderRecord.ind == 1)
            {
                inevitableexplain.IsVisible = true;
                inevitablewarning.IsVisible = true;
            }
            else
            {
                inevitableexplain.IsVisible = false;
                inevitablewarning.IsVisible = false;
            }
        }
    }
}