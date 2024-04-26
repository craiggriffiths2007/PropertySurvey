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
	public partial class VanDeliveryVan : ContentPage
	{
		public VanDeliveryVan ()
		{
			InitializeComponent ();

            BindingContext = App.net.DeliveryVanVehicleCheckList as DeliveryVanVehicleCheckList;

            SetCompleteCheck();
            SetFloorOptionsVisible();
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

            if (App.CurrentApp.DeliveryVanVehicleCheckList.is_complete == 2)
            {
                if (App.CurrentApp.DeliveryVanVehicleCheckList.not_complete_reason == "")
                    result = result + "Reason not completed\n";
            }
            else
            {
                if (App.CurrentApp.DeliveryVanVehicleCheckList.name == "")
                    result = result + "Name\n";
                if (App.CurrentApp.DeliveryVanVehicleCheckList.destination == "")
                    result = result + "Destination\n";
                if (App.CurrentApp.DeliveryVanVehicleCheckList.vehicle_registration == "")
                    result = result + "Registration\n";
                if (App.CurrentApp.DeliveryVanVehicleCheckList.mileage == "")
                    result = result + "Mileage\n";

                if (App.CurrentApp.DeliveryVanVehicleCheckList.spare_i_2 == 2 && App.CurrentApp.DeliveryVanVehicleCheckList.spare_s_2 == "")
                    result = result + "Loading/Storage area\n";

                if (App.CurrentApp.DeliveryVanVehicleCheckList.ats_card == 2 && App.CurrentApp.DeliveryVanVehicleCheckList.ats_card_s.Length == 0)
                    result = result + "ATS Card\n";
                if (App.CurrentApp.DeliveryVanVehicleCheckList.bodywork_check == 2 && App.CurrentApp.DeliveryVanVehicleCheckList.bodywork_check_s.Length == 0)
                    result = result + "Bodywork Check\n";
                if (App.CurrentApp.DeliveryVanVehicleCheckList.breakdown_card == 2 && App.CurrentApp.DeliveryVanVehicleCheckList.breakdown_card_s.Length == 0)
                    result = result + "Breakdown Card\n";
                if (App.CurrentApp.DeliveryVanVehicleCheckList.clean_external == 2 && App.CurrentApp.DeliveryVanVehicleCheckList.clean_external_s.Length == 0)
                    result = result + "Clean External\n";
                if (App.CurrentApp.DeliveryVanVehicleCheckList.clean_internal == 2 && App.CurrentApp.DeliveryVanVehicleCheckList.clean_internal_s.Length == 0)
                    result = result + "Clean Internal\n";
                if (App.CurrentApp.DeliveryVanVehicleCheckList.fan_belt == 2 && App.CurrentApp.DeliveryVanVehicleCheckList.fan_belt_s.Length == 0)
                    result = result + "Fan Belt\n";
                if (App.CurrentApp.DeliveryVanVehicleCheckList.fire_extinguisher == 2 && App.CurrentApp.DeliveryVanVehicleCheckList.fire_extinguisher_s.Length == 0)
                    result = result + "Fire Extinguisher\n";
                if (App.CurrentApp.DeliveryVanVehicleCheckList.first_aid_box == 2 && App.CurrentApp.DeliveryVanVehicleCheckList.first_aid_box_s.Length == 0)
                    result = result + "First Aid Box\n";
                if (App.CurrentApp.DeliveryVanVehicleCheckList.fuel_card == 2 && App.CurrentApp.DeliveryVanVehicleCheckList.fuel_card_s.Length == 0)
                    result = result + "Fuel Card\n";
                if (horn.IsComplete() == false)
                    result = result + "Horn\n";
                if (jack.IsComplete() == false)
                    result = result + "Jack\n";
                if (jump_leads.IsComplete() == false)
                    result = result + "Jump Leads\n";
                if (keys_for_branches.IsComplete() == false)
                    result = result + "Keys For Branches\n";
                if (lights_inducators.IsComplete() == false)
                    result = result + "Lights inducators\n";
                if (oil_water_checked.IsComplete() == false)
                    result = result + "Oil + Water Check\n";
                if (racks_poles.IsComplete() == false)
                    result = result + "Rack Poles\n";
                if (ratchet_straps.IsComplete() == false)
                    result = result + "Ratchet Straps\n";
                if (receipt_book.IsComplete() == false)
                    result = result + "Receipt Book\n";
                if (bump_hats.IsComplete() == false)
                    result = result + "Bump Hats\n";
                if (service_due_sticker.IsComplete() == false)
                    result = result + "Service Due Sticker\n";
                if (spanners_for_rack_removal.IsComplete() == false)
                    result = result + "Spanners For Rack Removal\n";
                if (spare_oil.IsComplete() == false)
                    result = result + "Spare Oil\n";
                if (coolant_anti_freeze_mix.IsComplete() == false)
                    result = result + "Coolant + Anti Freeze Mix\n";
                if (spare_wheel.IsComplete() == false)
                    result = result + "Spare Wheel\n";
                if (tow_ropes.IsComplete() == false)
                    result = result + "Tow Ropes\n";
                if (tyre_pressure.IsComplete() == false)
                    result = result + "Type Pressure\n";
                if (van_height_sticker.IsComplete() == false)
                    result = result + "Van Height Sticker\n";
                if (van_locks.IsComplete() == false)
                    result = result + "Van Locks\n";
                if (wheel_nut_check_sticker.IsComplete() == false)
                    result = result + "Weel Nut Check Sticker\n";
                if (wheelbrace.IsComplete() == false)
                    result = result + "Wheelbrace\n";
                if (windscreen_washer.IsComplete() == false)
                    result = result + "Windscreen Washer\n";
                if (pda_phone_accident_pack.IsComplete() == false)
                    result = result + "PDA Phone + Accident Pack\n";
                //if (branch_keys.IsComplete() == false)
                //    result = result + "Branch Keys\n";


                if (passenger_front_pressure.IsComplete() == false)
                    result = result + "Pressure passenger front\n";
                if (passenger_rear_pressure.IsComplete() == false)
                    result = result + "Pressure passenger rear\n";
                if (driver_front_pressure.IsComplete() == false)
                    result = result + "Pressure driver front\n";
                if (driver_rear_pressure.IsComplete() == false)
                    result = result + "Pressure driver rear\n";
                if (spare_tyre_pressure.IsComplete() == false)
                    result = result + "Spare tyre pressure\n";


                if (App.CurrentApp.DeliveryVanVehicleCheckList.bDriverSigned == false)
                    result = result + "Driver signature\n";
                if (App.CurrentApp.DeliveryVanVehicleCheckList.bCheckedBySigned == false)
                    result = result + "Checked by signature\n";

                if (App.CurrentApp.DeliveryVanVehicleCheckList.photos_left == 0)
                    result = result + "Passenger side photograph\n";

                if (App.CurrentApp.DeliveryVanVehicleCheckList.photos_right == 0)
                    result = result + "Driver side photograph\n";

                if (App.CurrentApp.DeliveryVanVehicleCheckList.photos_front == 0)
                    result = result + "Front side photograph\n";

                if (App.CurrentApp.DeliveryVanVehicleCheckList.photos_rear == 0)
                    result = result + "Rear side photograph\n";

                if (App.CurrentApp.DeliveryVanVehicleCheckList.bDiagramsComplete == false)
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
                        App.CurrentApp.DeliveryVanVehicleCheckList.bComplete = false;
                        App.data.SaveVanChecksDeliveryVan();
                        await this.Navigation.PopAsync(false);
                    }
                    else
                    {
                        App.CurrentApp.DeliveryVanVehicleCheckList.bComplete = false;
                    }
                });
            }
            else
            {
                App.CurrentApp.DeliveryVanVehicleCheckList.bComplete = true;
                App.data.SaveVanChecksDeliveryVan();
                this.Navigation.PopAsync(false);
            }
        }

        private void OnCompleteChanged(object sender, EventArgs e)
        {
            SetCompleteCheck();
        }

        private void SetCompleteCheck()
        {
            if (App.net.DeliveryVanVehicleCheckList.is_complete == 1)
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
            App.data.SaveVanChecksDeliveryVan();
            Navigation.PushAsync(new VanImages(), false);
        }

        private void OnFloorSelectionChanged(object sender, EventArgs e)
        {
            SetFloorOptionsVisible();
        }

        private void SetFloorOptionsVisible()
        {
            if (App.net.DeliveryVanVehicleCheckList.spare_i_2 == 1 || App.net.DeliveryVanVehicleCheckList.spare_i_2 == 0)
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