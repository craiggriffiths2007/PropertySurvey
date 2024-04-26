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
    public partial class Hnds : ContentPage
    {
        bool height_pages_visible = false;
        public Hnds()
        {
            InitializeComponent();

            List<string> lintel_button_list = new List<string>() { "...", "n/a", "Yes", "No" };
            lintel_question.set_button_list(lintel_button_list);

            BindingContext = App.net.HeaderRecord as Header;

            List<string> ground_surface_list = new List<string>() { "Concrete", "Paving flags", "Block paving", "Tarmac", "Timber decking", "Loose gravel", "Soil", "Grass" };
            ground_surface.SetPickerItems(ground_surface_list);

            List<string> type_of_equipment_list = new List<string>() { "Step Ladders", "Two Tier Ladders", "Three Tier Ladders", "Tower Scaffolding", "Tubular Scaffolding", "Conservatory Access Ladders", "Mobile Elevated Work Platform(Cherry Picker)", "Large step ladders and boards" };
            type_of_equipment.SetPickerItems(type_of_equipment_list);

            List<string> fbunfinother_list = new List<string>() { "1 meter", "2 meter", "3 meter", "4 meter", "5 meter", "6 meter", "7 meter", "8 meter", "9 meter", };
            size_of_barriers_required.SetPickerItems(fbunfinother_list);

            SetHeightPages();

            if(App.net.HeaderRecord.iRecordType>0)
            { 
                page1.IsEnabled = false;
            }

            obs_wires_text.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeSentence);
            loose_brick_text.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeSentence);
            lintel_present_text.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeSentence);
            asvizex.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeSentence);
            risks_and_dangers.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeSentence);

            Device.BeginInvokeOnMainThread(SetInputVisible);
        }

        private void OnLayout(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(SetInputVisible);
        }

        //protected override void OnCurrentPageChanged()
        //{
        //    base.OnCurrentPageChanged();
            //SetPageNumber();
        //}

        //private void SetPageNumber()
        //{
            //page_num.Text = (this.Children.IndexOf(CurrentPage)+1).ToString() + "/" + this.Children.Count().ToString();
        //}

        private void SetInputVisible()
        {
            if (App.net.HeaderRecord.obs_wires == 1)
                obs_wires_text.IsVisible = true;
            else
                obs_wires_text.IsVisible = false;

            if (App.net.HeaderRecord.loose_brick == 1)
                loose_brick_text.IsVisible = true;
            else
                loose_brick_text.IsVisible = false;

            lintel_comment.IsVisible = false;

            if (App.net.HeaderRecord.lintel_present == 1)
            {
                lintel_warning.IsVisible = false;
                lintel_comment.IsVisible = false;
                lintel_present_text.IsVisible = false;

            }
            else
            {
                
                if (App.net.HeaderRecord.lintel_present == 2)
                {
                    lintel_comment.IsVisible = true;
                    lintel_present_text.IsVisible = true;
                }
                else
                {
                    if (App.net.HeaderRecord.lintel_present != 0)
                    {
                        lintel_warning.IsVisible = true;
                        lintel_present_text.IsVisible = true;
                    }
                }
            }

            if (App.net.HeaderRecord.type_of_equipment == "Tower Scaffolding")
                tower_scaffolding_button.IsVisible = true;
            else
                tower_scaffolding_button.IsVisible = false;

            if (App.net.HeaderRecord.asbestos_visible == 1)
                asvizex.IsVisible = true;
            else
                asvizex.IsVisible = false;

            if (App.net.HeaderRecord.shop_front_work == 1)
                barriers_area.IsVisible = true;
            else
                barriers_area.IsVisible = false;

            if (App.net.HeaderRecord.work_on_public_footpath == 1)
                footpath_warning.IsVisible = true;
            else
                footpath_warning.IsVisible = false;
        }

        private void WorkAtHeight_OnSelectionChanged(object sender, EventArgs e)
        {
            SetHeightPages();
        }

        private void SetHeightPages()
        {
            if (App.net.HeaderRecord.work_at_height == 2)
            {
                if (height_pages_visible == true)
                {
                    height_area.IsVisible = false;
                    height_pages_visible = false;
                }
            }
            if (App.net.HeaderRecord.work_at_height == 1)
            {
                if (height_pages_visible == false)
                {
                    height_area.IsVisible = true;
                    height_pages_visible = true;
                }
            }
            //SetPageNumber();
        }

        protected override bool OnBackButtonPressed()
        {
            if (App.net.HeaderRecord.iRecordType == 0)
            {
                if (IsHnscomplete() == true)
                {
                    App.CurrentApp.HeaderRecord.bHazFin = true;

                }
                else
                {
                    App.CurrentApp.HeaderRecord.bHazFin = false;
                }
            }
            //App.CurrentApp.HeaderRecord.bHazFin = true;
            Navigation.PopAsync(false);

            return true;
        }

        public bool IsHnscomplete()
        {
            if (App.CurrentApp.HeaderRecord.easy_park == 0 ||
               App.CurrentApp.HeaderRecord.access_rear == 0 ||
              App.CurrentApp.HeaderRecord.obs_wires == 0 ||
               App.CurrentApp.HeaderRecord.loose_brick == 0 ||
                (App.CurrentApp.HeaderRecord.obs_wires == 1 && App.CurrentApp.HeaderRecord.obs_wires_text.Length == 0) ||
                (App.CurrentApp.HeaderRecord.loose_brick == 1 && App.CurrentApp.HeaderRecord.loose_brick_text.Length == 0) ||
                App.CurrentApp.HeaderRecord.asbestos_visible == 0 ||
                App.CurrentApp.HeaderRecord.items_above_roof == 0 ||
                (App.CurrentApp.HeaderRecord.asbestos_visible == 1 && App.CurrentApp.HeaderRecord.asvizex.Length == 0) ||
                App.CurrentApp.HeaderRecord.risks_and_dangers.Length == 0 ||
               (App.CurrentApp.HeaderRecord.work_at_height == 1 && (
               App.CurrentApp.HeaderRecord.bWorkInside == 0 ||
               App.CurrentApp.HeaderRecord.no_ladders == 0 ||
               App.CurrentApp.HeaderRecord.inst_height.Length == 0 ||
               App.CurrentApp.HeaderRecord.type_of_equipment.Length == 0 ||
               App.CurrentApp.HeaderRecord.ground_surface.Length == 0)))

            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void Asbestos_button_OnSelectionChanged(object sender, EventArgs e)
        {
            SetInputVisible();
        }
    }
}