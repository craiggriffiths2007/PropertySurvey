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
	public partial class VanDelivery : ContentPage
	{
		public VanDelivery ()
		{
			InitializeComponent ();

            BindingContext = App.net.DeliveryVehicleCheckList as DeliveryVehicleCheckList;

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

            if (App.CurrentApp.DeliveryVehicleCheckList.is_complete == 2)
            {
                if (App.CurrentApp.DeliveryVehicleCheckList.not_complete_reason == "")
                    result = result + "Reason not completed\n";
            }
            else
            {

                if (App.CurrentApp.DeliveryVehicleCheckList.name == "")
                    result = result + "Name\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.destination == "")
                    result = result + "Destination\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.vehicle_registration == "")
                    result = result + "Registration\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.mileage == "")
                    result = result + "Mileage\n";

                if (App.CurrentApp.DeliveryVehicleCheckList.spare_i_2 == 2 && App.CurrentApp.DeliveryVehicleCheckList.spare_s_2 == "")
                    result = result + "Loading/Storage area\n";

                //if (App.CurrentApp.DeliveryVehicleCheckList.national_tyres_card == 0)
                //    result = result + "National tyres card\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.fmg_support_sticker == 0)
                    result = result + "FMG Support Sticker\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.clean_external == 0)
                    result = result + "Clean external\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.clean_internal == 0)
                    result = result + "Clean internal\n";
                //if (App.CurrentApp.DeliveryVehicleCheckList.fan_belt == 0)
                //    result = result + "Fan belt\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.fire_extinguisher == 0)
                    result = result + "Fire extinguisher\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.first_aid_box == 0)
                    result = result + "First aid box\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.horn == 0)
                    result = result + "Horn\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.oil_and_water_checked == 0)
                    result = result + "Oil water\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.accident_pack == 0)
                    result = result + "Accident pack\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.portable_lighting == 0)
                    result = result + "Portable lighting\n";

                if (App.CurrentApp.DeliveryVehicleCheckList.ad_blue_level_check == 0)
                    result = result + "AD Blue Level Check\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.racks_and_poles == 0)
                    result = result + "Racks and Poles\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.ratchet_straps == 0)
                    result = result + "Ratchet Straps\n";

                if (App.CurrentApp.DeliveryVehicleCheckList.service_due_sticker == 0)
                    result = result + "Service due stick\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.spare_oil == 0)
                    result = result + "Spare oil\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.coolant_anti_freez == 0)
                    result = result + "Coolant anti freeze\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.tyre_pressure == 0)
                    result = result + "Tyre pressure\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.van_height_sticker == 0)
                    result = result + "Van height sticker\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.van_locks == 0)
                    result = result + "Van locks\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.wheel_nut_check_sticker == 0)
                    result = result + "Wheel nut check sticker\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.windscreen_washer == 0)
                    result = result + "Windscreen washer\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.fuel_oil_leaks == 0)
                    result = result + "Fuel oil leaks\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.battery_security_condition == 0)
                    result = result + "Battery security condition\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.tyres_and_wheel_fixing == 0)
                    result = result + "Tyres and wheel fixings\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.spray_suppression == 0)
                    result = result + "Spray compression\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.steering == 0)
                    result = result + "Steering\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.security_of_load == 0)
                    result = result + "Security of load\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.mirrors == 0)
                    result = result + "Mirrors\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.lights == 0)
                    result = result + "Lights\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.reflectors == 0)
                    result = result + "Reflectors\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.inducators == 0)
                    result = result + "Inducators\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.wipers == 0)
                    result = result + "Wipers\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.washers == 0)
                    result = result + "Washers\n";
                //if (App.CurrentApp.DeliveryVehicleCheckList.horn_comp == 0)
                //    result = result + "Horn comp\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.excessive_exhaust_smoke == 0)
                    result = result + "Excessive exhaust smoke\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.brakes == 0)
                    result = result + "Brakes\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.security_of_body == 0)
                    result = result + "Security of body\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.markers == 0)
                    result = result + "Markers\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.glass_windscreen == 0)
                    result = result + "Glass windscreen\n";

                if (App.CurrentApp.DeliveryVehicleCheckList.receipt_book == 0)
                    result = result + "Receipt Book\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.keys_for_branches_sat == 0)
                    result = result + "Keys for Branches and Sat\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.pda_phone_accident_pack == 0)
                    result = result + "PDA Phone and Accident Pack\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.trade_invoices == 0)
                    result = result + "Trade Invoices\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.blue_bags == 0)
                    result = result + "Blue Bags\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.delivery_lists == 0)
                    result = result + "Delivery Lists\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.collection_lists == 0)
                    result = result + "Collection Lists\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.trade_delivery_notes == 0)
                    result = result + "Trade Delivery Notes\n";


                if (App.CurrentApp.DeliveryVehicleCheckList.report_defects == "")
                    result = result + "Report defects\n";

                if (App.CurrentApp.DeliveryVehicleCheckList.passenger_front_pressure == 2 && App.CurrentApp.DeliveryVehicleCheckList.passenger_front_pressure_s!="")
                    result = result + "Pressure passenger front\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.passenger_rear_pressure == 2 && App.CurrentApp.DeliveryVehicleCheckList.passenger_rear_pressure_s != "")
                    result = result + "Pressure passenger rear\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.driver_front_pressure == 2 && App.CurrentApp.DeliveryVehicleCheckList.driver_front_pressure_s != "")
                    result = result + "Pressure driver front\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.passenger_front_pressure == 2 && App.CurrentApp.DeliveryVehicleCheckList.passenger_front_pressure_s != "")
                    result = result + "Pressure driver rear\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.passenger_rear_pressure == 2 && App.CurrentApp.DeliveryVehicleCheckList.passenger_rear_pressure_s != "")
                    result = result + "Spare tyre pressure\n";

                if (App.CurrentApp.DeliveryVehicleCheckList.bDriverSigned == false)
                    result = result + "Driver signature\n";
                if (App.CurrentApp.DeliveryVehicleCheckList.bCheckedBySigned == false)
                    result = result + "Checked by signature\n";

                if (App.CurrentApp.DeliveryVehicleCheckList.photos_left == 0)
                    result = result + "Passenger side photograph\n";

                if (App.CurrentApp.DeliveryVehicleCheckList.photos_right == 0)
                    result = result + "Driver side photograph\n";

                if (App.CurrentApp.DeliveryVehicleCheckList.photos_front == 0)
                    result = result + "Front side photograph\n";

                if (App.CurrentApp.DeliveryVehicleCheckList.photos_rear == 0)
                    result = result + "Rear side photograph\n";


                if (App.CurrentApp.DeliveryVehicleCheckList.bDiagramsComplete == false)
                    result = result + "Damage diagrams\n";
            }
            /*
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
            */
            if (result.Length > 20)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var response = await Application.Current.MainPage.DisplayAlert("Missing information",
                        "Please complete :\n\n" + result + "\n\nClose Anyway?\n", "   Yes   ", "   No   ");
                    if (response)
                    {
                        App.CurrentApp.DeliveryVehicleCheckList.bComplete = false;
                        App.data.SaveVanChecksDelivery();
                        await this.Navigation.PopAsync(false);
                    }
                    else
                    {
                        App.CurrentApp.DeliveryVehicleCheckList.bComplete = false;
                    }
                });
            }
            else
            {
                App.CurrentApp.DeliveryVehicleCheckList.bComplete = true;
                App.data.SaveVanChecksDelivery();
                this.Navigation.PopAsync(false);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetOptionsVisible();
            SetFloorOptionsVisible();
        }

        private void SetOptionsVisible()
        {
            if (App.net.DeliveryVehicleCheckList.is_complete == 1)
            {
                incomplete_area.IsVisible = false;
                complete_area.IsVisible = true;
            }
            else
            {
                incomplete_area.IsVisible = true;
                complete_area.IsVisible = false;
            }
        }

        private void OnCompleteChanged(object sender, EventArgs e)
        {
            SetCompleteCheck();
        }

        private void SetCompleteCheck()
        {
            if (App.net.DeliveryVehicleCheckList.is_complete == 1)
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
            App.data.SaveVanChecksDelivery();
            Navigation.PushAsync(new VanImages(), false);
        }

        private void OnFloorSelectionChanged(object sender, EventArgs e)
        {
            SetFloorOptionsVisible();
        }

        private void SetFloorOptionsVisible()
        {
            if (App.net.DeliveryVehicleCheckList.spare_i_2 == 1 || App.net.DeliveryVehicleCheckList.spare_i_2 == 0)
            {
                floor_area.IsVisible = false;
            }
            else
            {
                floor_area.IsVisible = true;
            }
        }
    }
}