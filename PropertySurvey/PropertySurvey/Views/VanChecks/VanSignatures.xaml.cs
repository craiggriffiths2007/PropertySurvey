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
	public partial class VanSignatures : ContentPage
	{
		public VanSignatures ()
		{
			InitializeComponent ();

            switch (App.CurrentApp.CurrentItem)
            {
                case "deliveryvan": BindingContext = App.net.DeliveryVanVehicleCheckList as DeliveryVanVehicleCheckList; break; 
                case "delivery": BindingContext = App.net.DeliveryVehicleCheckList as DeliveryVehicleCheckList; break;  
                case "van": BindingContext = App.net.WeeklyVanCheckSheet as WeeklyVanCheckSheet; break;
                case "car": BindingContext = App.net.CarPanelSheet as CarPanelSheet; break; 
            }
        }
        private void OnDriverPrintNameAsync(object sender, EventArgs e)
        {
            bool bPrinted = false;
            bool bSigned = false;

            switch (App.CurrentApp.CurrentItem)
            {
                case "deliveryvan": if (App.CurrentApp.DeliveryVanVehicleCheckList.driver_printed.Length > 0) { bPrinted = true; }; if (App.CurrentApp.DeliveryVanVehicleCheckList.bDriverSigned == true) { bSigned = true; } break;
                case "delivery": if (App.CurrentApp.DeliveryVehicleCheckList.driver_printed.Length > 0) { bPrinted = true; }; if (App.CurrentApp.DeliveryVehicleCheckList.bDriverSigned == true) { bSigned = true; } break;
                case "van": if (App.CurrentApp.WeeklyVanCheckSheet.driver_printed.Length > 0) { bPrinted = true; }; if (App.CurrentApp.WeeklyVanCheckSheet.bDriverSigned == true) { bSigned = true; } break;
                case "car": if (App.CurrentApp.CarPanelSheet.driver_printed.Length > 0) { bPrinted = true; }; if (App.CurrentApp.CarPanelSheet.bDriverSigned == true) { bSigned = true; } break;
            }

            if (bPrinted == false)
            {
                DisplayAlert("Missing information", "Please complete :\n\nDrivers Name", "   OK   ");
                //MessageBox.Show("You must enter the drivers name before signing", "Driver signature", MessageBoxButton.OK);
            }
            else
            {
                if (bSigned == true)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        var response = await DisplayAlert("Signature",
                            "You have already signed the driver signature, do you want to sign it again?", "   Yes   ", "   No   ");
                        if (response)
                        {
                            App.CurrentApp.drawing_type = "driver";
                            await Navigation.PushAsync(new VanDriverSignature(), false);
                        }
                    });
                }
                else
                {
                    App.CurrentApp.drawing_type = "driver";
                    Navigation.PushAsync(new VanDriverSignature(), false);
                }
            }

        }
        private void OnCheckedByPrintNameAsync(object sender, EventArgs e)
        {
            bool bPrinted = false;
            bool bSigned = false;

            switch (App.CurrentApp.CurrentItem)
            {
                case "deliveryvan": if (App.CurrentApp.DeliveryVanVehicleCheckList.checked_printed.Length > 0) { bPrinted = true; }; if (App.CurrentApp.DeliveryVanVehicleCheckList.bCheckedBySigned == true) { bSigned = true; } break;
                case "delivery": if (App.CurrentApp.DeliveryVehicleCheckList.checked_printed.Length > 0) { bPrinted = true; }; if (App.CurrentApp.DeliveryVehicleCheckList.bCheckedBySigned == true) { bSigned = true; } break;
                case "van": if (App.CurrentApp.WeeklyVanCheckSheet.checked_printed.Length > 0) { bPrinted = true; }; if (App.CurrentApp.WeeklyVanCheckSheet.bCheckedBySigned == true) { bSigned = true; } break;
                case "car": if (App.CurrentApp.CarPanelSheet.checked_printed.Length > 0) { bPrinted = true; }; if (App.CurrentApp.CarPanelSheet.bCheckedBySigned == true) { bSigned = true; } break;
            }

            if (bPrinted == false)
            {
                DisplayAlert("Missing information", "Please complete :\n\nDrivers Name", "   OK   ");
                //MessageBox.Show("You must enter the drivers name before signing", "Driver signature", MessageBoxButton.OK);
            }
            else
            {
                if (bSigned == true)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        var response = await DisplayAlert("Signature",
                            "You have already signed the driver signature, do you want to sign it again?", "   Yes   ", "   No   ");
                        if (response)
                        {
                            App.CurrentApp.drawing_type = "checker";
                            await Navigation.PushAsync(new VanCheckedSignature(), false);
                        }
                    });
                }
                else
                {
                    App.CurrentApp.drawing_type = "checker";
                    Navigation.PushAsync(new VanCheckedSignature(), false);
                }
            }
        }
    }
}