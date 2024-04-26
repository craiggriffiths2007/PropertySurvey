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
	public partial class VanBrowse : ContentPage
	{
        public class ListData
        {
            public int uid { get; set; }
            public int item_number { get; set; }
            public string item_type { get; set; }
            public string commnts { get; set; }
            public string back_colour { get; set; }
            public string other_info { get; set; }
            public int isComplete { get; set; }
            public string inc_mes { get; set; }

            public ListData(int uID, int _item_number, string _item_type, string _commnts, bool _isComplete, string _other_info)
            {
                this.uid = uID;

                this.item_number = _item_number;
                this.item_type = _item_type;
                this.commnts = _commnts;
                if (_isComplete == true)
                {
                    this.isComplete = 1;
                }
                else
                {
                    this.isComplete = 0;
                }
                this.other_info = _other_info;

                if (this.isComplete == 0)
                {
                    this.back_colour = "#b38e91";
                    this.item_type = this.item_type + "  (incomplete)";
                }
                else
                {
                    this.back_colour = "#7ccb7e";
                }
            }
        }

        ListData selected_data = null;
        bool bSelected = false;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.data.SaveVanChecks();
            DrawList();
            selected_data = null;
            bSelected = false;
        }

        public VanBrowse ()
		{
			InitializeComponent ();
		}

        private void DrawList()
        {
            List<ListData> dataSource = new List<ListData>();
            /*
            App.CurrentApp.total_incomplete_delivery = 0;
            App.CurrentApp.total_delivery = 0;
            App.CurrentApp.total_incomplete_vans = 0;
            App.CurrentApp.total_vans = 0;
            App.CurrentApp.total_incomplete_cars = 0;
            App.CurrentApp.total_cars = 0;
            */

            if (App.CurrentApp.CurrentItem == "deliveryvan")
            {
                List<DeliveryVanVehicleCheckList> delivery_van_vehicle = App.data.GetDeliveryVanVehicleCheckListByID(App.net.VanChecksHeader.unique_id);
                foreach (var item in delivery_van_vehicle)
                {
                    dataSource.Add(new ListData(item.RecID, item.item_no, "Reg: " + item.vehicle_registration, "Driver name: " + item.driver_printed, item.bComplete, "Mileage: " + item.mileage));
                }
                if (delivery_van_vehicle.Count() > 0)
                    Title = "Delivery Van - " + delivery_van_vehicle.Count().ToString();
                else
                    Title = "Delivery Van";
            }

            if (App.CurrentApp.CurrentItem == "delivery")
            {
                List<DeliveryVehicleCheckList> delivery_vehicle = App.data.GetDeliveryVehicleCheckListByID(App.net.VanChecksHeader.unique_id);
                foreach (var item in delivery_vehicle)
                {
                    dataSource.Add(new ListData(item.RecID, item.item_no, "Reg: " + item.vehicle_registration, "Driver name: " + item.driver_printed, item.bComplete, "Mileage: " + item.mileage));
                }
                if (delivery_vehicle.Count() > 0)
                    Title = "Delivery HGV - " + delivery_vehicle.Count().ToString();
                else
                    Title = "Delivery HGV";
            }

            if (App.CurrentApp.CurrentItem == "van")
            {
                List<WeeklyVanCheckSheet> weekly_van = App.data.GetWeeklyVanChecksByID(App.net.VanChecksHeader.unique_id);
                foreach (var item in weekly_van)
                {
                    dataSource.Add(new ListData(item.RecID, item.item_no, "Reg: " + item.vehicle_reg, "Driver name: " + item.driver_printed, item.bComplete, "Mileage: " + item.mileage));
                }
                if (weekly_van.Count() > 0)
                    Title = "Fitter Van - " + weekly_van.Count().ToString();
                else
                    Title = "Fitter Van";
            }

            if (App.CurrentApp.CurrentItem == "car")
            {
                List<CarPanelSheet> car_panel = App.data.GetCarPanelSheetByID(App.net.VanChecksHeader.unique_id);
                foreach (var item in car_panel)
                {
                    dataSource.Add(new ListData(item.RecID, item.item_no, "Reg: " + item.vehicle_reg, "Driver name: " + item.driver_printed, item.bComplete, "Mileage: " + item.mileage));
                }
                if (car_panel.Count() > 0)
                    Title = "Car - " + car_panel.Count().ToString();
                else
                    Title = "Car";
            }

            listView.ItemsSource = dataSource;
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            DoSelect();
        }

        private void OnSelect(object sender, EventArgs e)
        {
            DoSelect();
        }

        private void DoSelect()
        {
            if (selected_data == (listView as ListView).SelectedItem as ListData && bSelected == false)
            {
                bSelected = true;
                if (App.CurrentApp.CurrentItem == "deliveryvan")
                {
                    if (App.data.LoadDeliveryVan(selected_data.uid) == true)
                        Navigation.PushAsync(new VanDeliveryVan(), false);
                }

                if (App.CurrentApp.CurrentItem == "delivery")
                {
                    if (App.data.LoadDelivery(selected_data.uid) == true)
                        Navigation.PushAsync(new VanDelivery(), false);
                }

                if (App.CurrentApp.CurrentItem == "van")
                {
                    if(App.data.LoadVan(selected_data.uid)==true)
                        Navigation.PushAsync(new VanVan(), false);
                }

                if (App.CurrentApp.CurrentItem == "car")
                {
                    if (App.data.LoadCar(selected_data.uid) == true)
                        Navigation.PushAsync(new VanCar(), false);
                }
            }
            else
            {
                selected_data = (listView as ListView).SelectedItem as ListData;
            }
        }
    }
}