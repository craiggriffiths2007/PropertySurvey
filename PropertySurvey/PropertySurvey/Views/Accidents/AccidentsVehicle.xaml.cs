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
	public partial class AccidentsVehicle : ContentPage
	{
        public List<string> menu_items = new List<string>() {
            "RESEND",
            "DELETE",
            };

        public class ListData
        {
            public int uid { get; set; }
            public string name { get; set; }
            public string otherinfo { get; set; }
            public bool bComplete { get; set; }
            public string back_colour { get; set; }

            public ListData(int uID, string _name, string _otherinfo, bool bSent, bool _bComplete)
            {
                this.uid = uID;
                this.name = _name;
                this.otherinfo = _otherinfo;

                if (bSent)
                    this.back_colour = "#1881bf";
                else if (_bComplete)
                    this.back_colour = "#7ccb7e";
                else
                    this.back_colour = "#b38e91";
            }
        }

        ListData selected_data = null;
        bool bSelected = false;

        public AccidentsVehicle()
		{
			InitializeComponent ();

            menu_pick.ItemsSource = menu_items;
        }

        private void OnMenuChanged(object sender, EventArgs e)
        {
            if (menu_pick.SelectedIndex > -1)
                switch (menu_pick.Items[menu_pick.SelectedIndex])
                {
                    case "RESEND": ResendAccident(); break;
                    case "DELETE": DeleteAccident(); break;
                }
        }

        private async void DeleteAccident()
        {
            if (selected_data!=null)
            {
                var answer = await DisplayAlert("Delete Accident?", "", "   Yes   ", "   No   ");
                if (answer == true)
                {
                    App.net.AccidentRecord = App.data.GetVehicleAccident(selected_data.uid);
                    App.data.DeleteAccident(App.net.AccidentRecord.RecID);
                    //App.data.CreateHeaderIndex();
                    DrawList();
                }
            }
        }

        private async void ResendAccident()
        {
            if (selected_data != null)
            {
                var answer = await DisplayAlert("Resend Accident?", "", "   Yes   ", "   No   ");
                if (answer == true)
                {
                    App.net.AccidentRecord = App.data.GetVehicleAccident(selected_data.uid);
                    App.net.AccidentRecord.bSent = false;
                    App.data.SaveVehicleAccident();
                    //App.data.CreateHeaderIndex();
                    DrawList();
                }
            }
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            DrawList();
        }

        private void DoSelect()
        {
            if (selected_data == (listView as ListView).SelectedItem as ListData && bSelected == false)
            {
                bSelected = true;
                App.net.AccidentRecord = App.data.GetVehicleAccident(selected_data.uid);
                if (App.net.AccidentRecord != null)
                Navigation.PushAsync(new AccidentMenuVehicle(), false);
            }
            else
                selected_data = (listView as ListView).SelectedItem as ListData;
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            DoSelect();
        }

        private void CreateVehicleAccident()
        {
            App.CurrentApp.AccidentRecord = new Accident_sheet();

            App.CurrentApp.AccidentRecord.date_time = DateTime.Now.ToShortDateString();
            App.CurrentApp.AccidentRecord.brief = "";
            App.CurrentApp.AccidentRecord.d_officers_name = "";
            App.CurrentApp.AccidentRecord.d_officers_number = "";
            App.CurrentApp.AccidentRecord.d_station = "";
            App.CurrentApp.AccidentRecord.d_place = "";
            App.CurrentApp.AccidentRecord.d_speed = "";
            App.CurrentApp.AccidentRecord.d_weather = "";
            App.CurrentApp.AccidentRecord.d_description = "";
            App.CurrentApp.AccidentRecord.d_sign_date = DateTime.Now.ToShortDateString();

            App.CurrentApp.AccidentRecord.y_make = "";
            App.CurrentApp.AccidentRecord.y_model = "";
            App.CurrentApp.AccidentRecord.y_reg = "";
            App.CurrentApp.AccidentRecord.y_used_for = "";
            App.CurrentApp.AccidentRecord.y_driver_full_name = "";
            App.CurrentApp.AccidentRecord.y_driver_dob = DateTime.Now.ToShortDateString();

            App.CurrentApp.AccidentRecord.y_address1 = "";
            App.CurrentApp.AccidentRecord.y_address2 = "";
            App.CurrentApp.AccidentRecord.y_address3 = "";
            App.CurrentApp.AccidentRecord.y_pcode = "";
            App.CurrentApp.AccidentRecord.y_occupation = "";
            App.CurrentApp.AccidentRecord.y_any_other_accidents = "";
            App.CurrentApp.AccidentRecord.y_infirmity = "";
            App.CurrentApp.AccidentRecord.y_prosecution = "";
            App.CurrentApp.AccidentRecord.y_vehicle_damage = "";
            App.CurrentApp.AccidentRecord.y_damage_to_property = "";
            App.CurrentApp.AccidentRecord.y_injuries_sustained = "";
            App.CurrentApp.AccidentRecord.y_damage_to_property = "";
            App.CurrentApp.AccidentRecord.y_injuries_sustained = "";
            App.CurrentApp.AccidentRecord.y_years_employed = "";
            App.CurrentApp.AccidentRecord.y_months_employed = "";

            App.CurrentApp.AccidentRecord.t_name = "";
            App.CurrentApp.AccidentRecord.t_add1 = "";
            App.CurrentApp.AccidentRecord.t_add2 = "";
            App.CurrentApp.AccidentRecord.t_add3 = "";
            App.CurrentApp.AccidentRecord.t_pcode = "";
            App.CurrentApp.AccidentRecord.t_make = "";
            App.CurrentApp.AccidentRecord.t_reg = "";
            App.CurrentApp.AccidentRecord.t_model = "";
            App.CurrentApp.AccidentRecord.t_insurer = "";
            App.CurrentApp.AccidentRecord.t_policy_no = "";
            App.CurrentApp.AccidentRecord.t_telnum = "";

            App.CurrentApp.AccidentRecord.p_name = "";
            App.CurrentApp.AccidentRecord.p_add1 = "";
            App.CurrentApp.AccidentRecord.p_add2 = "";
            App.CurrentApp.AccidentRecord.p_add3 = "";
            App.CurrentApp.AccidentRecord.p_pcode = "";
            App.CurrentApp.AccidentRecord.p_wittel = "";
            App.CurrentApp.AccidentRecord.v_reg = "";
            App.CurrentApp.AccidentRecord.v_model = "";
            App.CurrentApp.AccidentRecord.acc_date = DateTime.Today.ToShortDateString();
            App.CurrentApp.AccidentRecord.acc_time = DateTime.Now.ToShortTimeString();

            App.data.SaveVehicleAccident(); // Force RecId to get assigned, otherwise witnesses and signatures get wrong value
        }

        private void OnAdd(object sender, EventArgs e)
        {
            CreateVehicleAccident();
            Navigation.PushAsync(new InstructionsVehicle(), false);
        }

        private void OnDelete(object sender, EventArgs e)
        {
            if (selected_data != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var response = await Application.Current.MainPage.DisplayAlert("Confirm", "Delete accident report?\n", "   Yes   ", "   No   ");
                    if (response)
                    {
                        App.data.DeleteVehicleAccident(selected_data.uid);
                        DrawList();
                    }
                });
            }
        }

        private void OnSelect(object sender, EventArgs e)
        {
            //DoSelect();
            Navigation.PushAsync(new SendAccidents(), false);
        }

        private void DrawList()
        {
            List<ListData> dataSource = new List<ListData>();

            var query = App.data.GetVehicleAccidents();
            foreach (var item in query)
                dataSource.Add(new ListData(item.RecID, item.y_driver_full_name, String.Format("{0:dd/MM/yyyy}", item.date_time), item.bSent, item.bComplete));

            listView.ItemsSource = dataSource;
            selected_data = null;
            bSelected = false;
        }

        private void OnMenuClicked(object sender, EventArgs e)
        {
            menu_pick.Focus();
        }
    }
}
