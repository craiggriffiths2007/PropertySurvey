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
	public partial class VanVan : ContentPage
	{
		public VanVan ()
		{
			InitializeComponent ();

            BindingContext = App.net.WeeklyVanCheckSheet as WeeklyVanCheckSheet;

            SetCompleteCheck();

            SetHelmets();
            SetValues();
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

            if (App.CurrentApp.WeeklyVanCheckSheet.is_complete == 2)
            {
                if (App.CurrentApp.WeeklyVanCheckSheet.not_complete_reason == "")
                    result = result + "Reason not completed\n";
            }
            else
            {

                if (App.CurrentApp.WeeklyVanCheckSheet.branch.Length == 0)
                    result = result + "Branch\n";
                if (App.CurrentApp.WeeklyVanCheckSheet.vehicle_reg.Length == 0)
                    result = result + "Registration\n";
                if (App.CurrentApp.WeeklyVanCheckSheet.mileage.Length == 0)
                    result = result + "Mileage\n";
                /*
                if (circuit_breaker.IsComplete() == false)
                    result = result + "Circuit Breaker\n";
                if (power_breaker.IsComplete() == false)
                    result = result + "power Brake\n";
                if (hammer_drill.IsComplete() == false)
                    result = result + "Hammer Drill\n";
                if (ordinary_drill.IsComplete() == false)
                    result = result + "Ordinary Drill\n";
                if (cordless_drill.IsComplete() == false)
                    result = result + "Cordless Drill\n";
                if (spare_battery_and_charger.IsComplete() == false)
                    result = result + "Spare battery and charger\n";
                if (circular_saw.IsComplete() == false)
                    result = result + "Curcular Saw\n";
                if (jig_saw.IsComplete() == false)
                    result = result + "Jigsaw\n";
                if (planer_check_blade.IsComplete() == false)
                    result = result + "Planner check blade\n";
                if (heat_gun.IsComplete() == false)
                    result = result + "Heat gun\n";
                if (sander.IsComplete() == false)
                    result = result + "Sander\n";
                if (hoover.IsComplete() == false)
                    result = result + "Hoover\n";
                if (halogen_lamp.IsComplete() == false)
                    result = result + "Halogen Lamp\n";
                 * */
                if (loading_storage.IsComplete() == false)
                    result = result + "Loading/Storage area\n";
                if (circuit_breaker.IsComplete() == false)
                    result = result + "Circuit breaker\n";
                if (power_breaker.IsComplete() == false)
                    result = result + "Power breaker\n";
                if (hammer_drill.IsComplete() == false)
                    result = result + "Hammer Drill\n";
                if (ordinary_drill.IsComplete() == false)
                    result = result + "Ordinary drill\n";
                if (cordless_drill.IsComplete() == false)
                    result = result + "Chordless drill\n";
                if (spare_battery_and_charger.IsComplete() == false)
                    result = result + "Spare battery and charger\n";
                if (circular_saw.IsComplete() == false)
                    result = result + "Curcular Saw\n";
                if (jig_saw.IsComplete() == false)
                    result = result + "Jigsaw\n";
                if (planer_check_blade.IsComplete() == false)
                    result = result + "Planer check blade\n";
                if (heat_gun.IsComplete() == false)
                    result = result + "Heat gun\n";
                if (sander.IsComplete() == false)
                    result = result + "Sander\n";
                if (hoover.IsComplete() == false)
                    result = result + "Hoover\n";
                if (halogen_lamp.IsComplete() == false)
                    result = result + "Halogen lamp\n";
                if (extension_lead.IsComplete() == false)
                    result = result + "Extension lead\n";
                if (router.IsComplete() == false)
                    result = result + "Router\n";
                if (letterboxjig.IsComplete() == false)
                    result = result + "Letterbox Jig\n";
                if (industrial_ladders.IsComplete() == false)
                    result = result + "industrial ladders\n";
                if (ladder_clamps.IsComplete() == false)
                    result = result + "Ladder clamps\n";
                if (step_ladders.IsComplete() == false)
                    result = result + "Step ladders\n";
                if (ladder_stopper.IsComplete() == false)
                    result = result + "Ladder stopper\n";
                if (philips_bit.IsComplete() == false)
                    result = result + "Philips bit\n";
                if (screw_box.IsComplete() == false)
                    result = result + "Screw box\n";
                if (tresles_x2.IsComplete() == false)
                    result = result + "Tresles\n";
                if (torch_working.IsComplete() == false)
                    result = result + "Torch working\n";
                if (ratchett_straps_x4.IsComplete() == false)
                    result = result + "Ratchet straps\n";
                if (spare_wheel.IsComplete() == false)
                    result = result + "Spare wheel\n";
                if (blue_external_dust_sheet.IsComplete() == false)
                    result = result + "Blue external dust sheet\n";
                if (internal_dust_sheets_x3.IsComplete() == false)
                    result = result + "Internal dust sheets\n";
                if (brush_and_shovel.IsComplete() == false)
                    result = result + "Brush and shovel\n";
                if (cleaner_bottle.IsComplete() == false)
                    result = result + "Cleaner bottle\n";
                if (ecloth.IsComplete() == false)
                    result = result + "e cloth\n";
                if (mastic_guns.IsComplete() == false)
                    result = result + "Mastic guns\n";
                if (glass_suckers.IsComplete() == false)
                    result = result + "Glass suckers\n";

                /*
                if (gloves.IsComplete() == false)
                    result = result + "Gloves\n";
                if (wrist_guards.IsComplete() == false)
                    result = result + "Wrist guards\n";
                if (goggles.IsComplete() == false)
                    result = result + "Goggles\n";
                if (ear_defenders.IsComplete() == false)
                    result = result + "Ear Defenders\n";
                if (dust_masks.IsComplete() == false)
                    result = result + "Dust masks\n";
                    */
                if (customer_care_cards.IsComplete() == false)
                    result = result + "Customer care cards\n";
                if (completion_forms.IsComplete() == false)
                    result = result + "Excess payment forms\n";
                if (freepost_envelopes.IsComplete() == false)
                    result = result + "Envelopes\n";
                if (mandate_forms.IsComplete() == false)
                    result = result + "Mandate forms\n";
                if (quality_manuals.IsComplete() == false)
                    result = result + "COSHH Sheets\n";
                if (stapler.IsComplete() == false)
                    result = result + "Stapler\n";
                if (worksheets.IsComplete() == false)
                    result = result + "Stock Usage Sheets\n";
                if (plasters.IsComplete() == false)
                    result = result + "Plasters\n";
                if (dressing.IsComplete() == false)
                    result = result + "Dressing\n";
                if (eyewashers.IsComplete() == false)
                    result = result + "Eyewashers\n";
                if (steri_wipes.IsComplete() == false)
                    result = result + "Steri whipes\n";
                if (bag.IsComplete() == false)
                    result = result + "Bag\n";
                if (flexi_meter.IsComplete() == false)
                    result = result + "Flexi meter\n";
                if (merlin_low_e_detector.IsComplete() == false)
                    result = result + "Merlin low e detector\n";
                if (cabin_condition.IsComplete() == false)
                    result = result + "Cabin condition\n";
                if (national_tyres_card.IsComplete() == false)
                    result = result + "National tyres card\n";
                if (breakdown_card.IsComplete() == false)
                    result = result + "Breakdown card\n";
                if (fuel_card.IsComplete() == false)
                    result = result + "Fuel card\n";
                if (shell_fuel_card.IsComplete() == false)
                    result = result + "Shell fuel card\n";
                if (shell_points_card.IsComplete() == false)
                    result = result + "shell points card\n";
                if (fire_extinguisher.IsComplete() == false)
                    result = result + "Fire extinguisher\n";
                if (jack.IsComplete() == false)
                    result = result + "Jack\n";
                if (wheelbrace.IsComplete() == false)
                    result = result + "wheelbrace\n";
                if (jump_leads.IsComplete() == false)
                    result = result + "Jump leads\n";
                if (fan_belt.IsComplete() == false)
                    result = result + "Fan belt\n";
                if (tow_ropes.IsComplete() == false)
                    result = result + "Tow ropes\n";
                if (spare_oil.IsComplete() == false)
                    result = result + "Spare oil\n";
                if (coolant_anti_freeze.IsComplete() == false)
                    result = result + "Coolant anti freeze\n";
                if (van_height_sticker.IsComplete() == false)
                    result = result + "Van height sticker\n";
                if (wheel_nut_check_sticker.IsComplete() == false)
                    result = result + "Wheel nut check sticker\n";
                if (no_smoking_sticker.IsComplete() == false)
                    result = result + "No smoking sticker\n";
                if (racks_and_poles.IsComplete() == false)
                    result = result + "Racks and poles\n";
                if (tyre_conditions.IsComplete() == false)
                    result = result + "Tyre conditions\n";
                if (van_locks.IsComplete() == false)
                    result = result + "Van locks\n";
                if (oil_and_water_checked.IsComplete() == false)
                    result = result + "Oil and water checked\n";
                if (hows_my_driving_sticker.IsComplete() == false)
                    result = result + "Hows my driving sticker\n";

                if (accident_pack_on_pda.IsComplete() == false)
                    result = result + "Accident pack on PDA\n";
                if (hi_vis_vests.IsComplete() == false)
                    result = result + "Hi vis vests\n";
                if (grinder.IsComplete() == false)
                    result = result + "Grinder\n";
                if (extension_lead.IsComplete() == false)
                    result = result + "Extension lead\n";
                //if (out_of_ten.Text.Length == 0)
                //    result = result + "Marks out of 10\n";


                if (App.CurrentApp.WeeklyVanCheckSheet.bDriverSigned == false)
                    result = result + "Driver signature\n";
                if (App.CurrentApp.WeeklyVanCheckSheet.bCheckedBySigned == false)
                    result = result + "Checked by signature\n";
                if (App.CurrentApp.WeeklyVanCheckSheet.photos_left == 0)
                    result = result + "Passenger side photograph\n";
                if (App.CurrentApp.WeeklyVanCheckSheet.photos_right == 0)
                    result = result + "Driver side photograph\n";
                if (App.CurrentApp.WeeklyVanCheckSheet.photos_front == 0)
                    result = result + "Front side photograph\n";
                if (App.CurrentApp.WeeklyVanCheckSheet.photos_rear == 0)
                    result = result + "Rear side photograph\n";
                if (App.CurrentApp.WeeklyVanCheckSheet.bDiagramsComplete == false)
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
                        App.CurrentApp.WeeklyVanCheckSheet.bComplete = false;
                        App.data.SaveVanChecksVan();
                        await this.Navigation.PopAsync(false);
                    }
                    else
                    {
                        App.CurrentApp.WeeklyVanCheckSheet.bComplete = false;
                    }
                });
            }
            else
            {
                App.CurrentApp.WeeklyVanCheckSheet.bComplete = true;
                App.data.SaveVanChecksVan();
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
            App.data.SaveVanChecksVan();
            Navigation.PushAsync(new VanImages(), false);
        }

        private void OnSafetyChanged(object sender, EventArgs e)
        {
            SetHelmets();
        }

        private void SetHelmets()
        {
            ManufactureDateOnHelmet.IsVisible = (App.CurrentApp.WeeklyVanCheckSheet.safety_helmets > 0);
            ManufactureDateOnHelmet2.IsVisible = (App.CurrentApp.WeeklyVanCheckSheet.safety_helmets > 1);
            ManufactureDateOnHelmet3.IsVisible = (App.CurrentApp.WeeklyVanCheckSheet.safety_helmets > 2);
            ManufactureDateOnHelmet4.IsVisible = (App.CurrentApp.WeeklyVanCheckSheet.safety_helmets > 3);
            ManufactureDateOnHelmet5.IsVisible = (App.CurrentApp.WeeklyVanCheckSheet.safety_helmets > 4);
            ManufactureDateOnHelmet6.IsVisible = (App.CurrentApp.WeeklyVanCheckSheet.safety_helmets > 5);
            ManufactureDateOnHelmet7.IsVisible = (App.CurrentApp.WeeklyVanCheckSheet.safety_helmets > 6);
            ManufactureDateOnHelmet8.IsVisible = (App.CurrentApp.WeeklyVanCheckSheet.safety_helmets > 7);
            ManufactureDateOnHelmet9.IsVisible = (App.CurrentApp.WeeklyVanCheckSheet.safety_helmets > 8);
            ManufactureDateOnHelmet10.IsVisible = (App.CurrentApp.WeeklyVanCheckSheet.safety_helmets > 9);
        }

        private void SetValues()
        {
            if (App.CurrentApp.WeeklyVanCheckSheet.gloves == 0)
                gloves_s.IsVisible = true;
            else
                gloves_s.IsVisible = false;

            if (App.CurrentApp.WeeklyVanCheckSheet.wrist_guards == 0)
                wrist_guards_s.IsVisible = true;
            else
                wrist_guards_s.IsVisible = false;

            if (App.CurrentApp.WeeklyVanCheckSheet.goggles == 0)
                goggles_s.IsVisible = true;
            else
                goggles_s.IsVisible = false;

            if (App.CurrentApp.WeeklyVanCheckSheet.ear_defenders == 0)
                ear_defenders_s.IsVisible = true;
            else
                ear_defenders_s.IsVisible = false;

            if (App.CurrentApp.WeeklyVanCheckSheet.dust_masks == 0)
                dust_masks_s.IsVisible = true;
            else
                dust_masks_s.IsVisible = false;
        }

        private void OnGlovesChanged(object sender, EventArgs e)
        {
            SetValues();
        }

        private void OnWristChanged(object sender, EventArgs e)
        {
            SetValues();
        }

        private void OnGogglesChanged(object sender, EventArgs e)
        {
            SetValues();
        }

        private void OnEarChanged(object sender, EventArgs e)
        {
            SetValues();
        }

        private void OnDustChanged(object sender, EventArgs e)
        {
            SetValues();
        }
    }
}