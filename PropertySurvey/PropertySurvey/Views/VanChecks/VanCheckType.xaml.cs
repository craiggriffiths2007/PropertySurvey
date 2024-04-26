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
	public partial class VanCheckType : ContentPage
	{
		public VanCheckType ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetValues();
        }

        private void SetValues()
        {
            App.CurrentApp.total_delivery_van = App.CurrentApp.VanChecksHeader.total_delivery_van;
            App.CurrentApp.total_delivery = App.CurrentApp.VanChecksHeader.total_delivery;
            App.CurrentApp.total_vans = App.CurrentApp.VanChecksHeader.total_vans;
            App.CurrentApp.total_cars = App.CurrentApp.VanChecksHeader.total_cars;

            App.CurrentApp.total_incomplete_delivery = App.CurrentApp.VanChecksHeader.total_incomplete_delivery;
            App.CurrentApp.total_incomplete_delivery_van = App.CurrentApp.VanChecksHeader.total_incomplete_delivery_van;
            App.CurrentApp.total_incomplete_vans = App.CurrentApp.VanChecksHeader.total_incomplete_vans;
            App.CurrentApp.total_incomplete_cars = App.CurrentApp.VanChecksHeader.total_incomplete_cars;

            int total = App.CurrentApp.total_delivery + App.CurrentApp.total_vans + App.CurrentApp.total_cars + App.CurrentApp.total_delivery_van;

            if (App.CurrentApp.total_delivery_van > 0)
                i1.Text = "Delivery Van - " + (App.CurrentApp.total_delivery_van - App.CurrentApp.total_incomplete_delivery_van).ToString() + "/" + App.CurrentApp.total_delivery_van.ToString();
            else
                i1.Text = "Delivery Van";

            if (App.CurrentApp.total_delivery > 0)
                i2.Text = "Delivery HGV - " + (App.CurrentApp.total_delivery - App.CurrentApp.total_incomplete_delivery).ToString() + "/" + App.CurrentApp.total_delivery.ToString();
            else
                i2.Text = "Delivery HGV";

            if (App.CurrentApp.total_vans > 0)
                i3.Text = "Fitter Van - " + (App.CurrentApp.total_vans - App.CurrentApp.total_incomplete_vans).ToString() + "/" + App.CurrentApp.total_vans.ToString();
            else
                i3.Text = "Fitter Van";

            if (App.CurrentApp.total_cars > 0)
                i4.Text = "Car - " + (App.CurrentApp.total_cars - App.CurrentApp.total_incomplete_cars).ToString() + "/" + App.CurrentApp.total_cars.ToString();
            else
                i4.Text = "Car";

            Title = "Vehicles - " + total.ToString();

            if (App.CurrentApp.VanChecksHeader.spare_s_1 != "")
            {
                l1.Text = "Van Checks for Branch : " + App.CurrentApp.VanChecksHeader.spare_s_1;
                l2.Text = "Week commencing : " + App.CurrentApp.VanChecksHeader.check_date;
            }
            else
            {
                l1.Text = " ";
                l2.Text = " ";
            }
        }

        private void OnDelVan(object sender, EventArgs e)
        {
            App.CurrentApp.CurrentItem = "deliveryvan";
            Navigation.PushAsync(new VanBrowse(), false);
        }

        private void OnDelHGV(object sender, EventArgs e)
        {
            App.CurrentApp.CurrentItem = "delivery";
            Navigation.PushAsync(new VanBrowse(), false);
        }

        private void OnFitVan(object sender, EventArgs e)
        {
            App.CurrentApp.CurrentItem = "van";
            Navigation.PushAsync(new VanBrowse(), false);
        }

        private void OnCar(object sender, EventArgs e)
        {
            App.CurrentApp.CurrentItem = "car";
            Navigation.PushAsync(new VanBrowse(), false);
        }
    }
}