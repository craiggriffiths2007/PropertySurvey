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
	public partial class VanCar : ContentPage
	{
		public VanCar ()
		{
			InitializeComponent ();

            BindingContext = App.net.CarPanelSheet as CarPanelSheet;

            SetCompleteCheck();
        }

        protected override bool OnBackButtonPressed()
        {
            CheckInAndSave();

            return true;
        }

        private void CheckInAndSave()
        {
            string result = "";

            result = "Please complete :\n\n";

            if (App.CurrentApp.CarPanelSheet.is_complete == 2)
            {
                if (App.CurrentApp.CarPanelSheet.not_complete_reason == "")
                    result = result + "Reason not completed\n";
            }
            else
            {

                if (vehicle_reg.text.Length == 0)
                    result = result + "Registration\n";
                if (fuel_card.IsComplete() == false)
                    result = result + "UK Fuel Card\n";
                if (shell_fuel_card.IsComplete() == false)
                    result = result + "Shell Fuel Card\n";

                if (shell_points_card.IsComplete() == false)
                    result = result + "Shell points card\n";
                if (interior_clean.IsComplete() == false)
                    result = result + "Interior clean\n";
                if (oil_level.IsComplete() == false)
                    result = result + "Oil level\n";
                if (water_level.IsComplete() == false)
                    result = result + "Water level\n";
                if (windscreen_wash.IsComplete() == false)
                    result = result + "Windscreen wash\n";
                if (spare_wheel.IsComplete() == false)
                    result = result + "Spare wheel\n";
                if (jack.IsComplete() == false)
                    result = result + "Jack\n";
                if (wheel_brace.IsComplete() == false)
                    result = result + "Wheel brace\n";
                if (tools.IsComplete() == false)
                    result = result + "Tools\n";

                if (pressure_passenger_front.IsComplete() == false)
                    result = result + "Pressure passenger front\n";
                if (pressure_passenger_rear.IsComplete() == false)
                    result = result + "Pressure passenger rear\n";
                if (pressure_driver_front.IsComplete() == false)
                    result = result + "Pressure driver front\n";
                if (pressure_driver_rear.IsComplete() == false)
                    result = result + "Pressure driver rear\n";
                if (spare_tyre_pressure.IsComplete() == false)
                    result = result + "Spare tyre pressure\n";

                if (App.CurrentApp.CarPanelSheet.bDriverSigned == false)
                    result = result + "Driver signature\n";
                if (App.CurrentApp.CarPanelSheet.bCheckedBySigned == false)
                    result = result + "Checked by signature\n";

                if (App.CurrentApp.CarPanelSheet.photos_left == 0)
                    result = result + "Passenger side photograph\n";

                if (App.CurrentApp.CarPanelSheet.photos_right == 0)
                    result = result + "Driver side photograph\n";

                if (App.CurrentApp.CarPanelSheet.photos_front == 0)
                    result = result + "Front side photograph\n";

                if (App.CurrentApp.CarPanelSheet.photos_rear == 0)
                    result = result + "Rear side photograph\n";

                if (App.CurrentApp.CarPanelSheet.bDiagramsComplete == false)
                    result = result + "Damage diagrams\n";
            }

            if (result.Length > 20)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var response = await Application.Current.MainPage.DisplayAlert("Missing information",
                        "Please complete :\n\n" + result + "\n\nClose Anyway?\n", "   Yes   ", "   No   ");
                    if (response)
                    {
                        App.CurrentApp.CarPanelSheet.bComplete = false;
                        App.data.SaveVanChecksCar();
                        await this.Navigation.PopAsync(false);
                    }
                    else
                    {
                        App.CurrentApp.CarPanelSheet.bComplete = false;
                    }
                });
            }
            else
            {
                App.CurrentApp.CarPanelSheet.bComplete = true;
                App.data.SaveVanChecksCar();
                this.Navigation.PopAsync(false);
            }
        }

        private void OnCompleteChanged(object sender, EventArgs e)
        {
            SetCompleteCheck();
        }

        private void SetCompleteCheck()
        {
            if (App.net.WeeklyVanCheckSheet.is_complete == 1)
            {
                complete_area.IsVisible = true;
                incomplete_area.IsVisible = false;
            }
            else
            {
                complete_area.IsVisible = false;
                incomplete_area.IsVisible = true;
            }
        }

        private void OnImagesClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VanImages(), false);
        }
    }
}