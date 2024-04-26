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
	public partial class VanImages : ContentPage
	{
		public VanImages ()
		{
			InitializeComponent ();

            App.net.camera_landscape_mode = false;

            App.CurrentApp.camera_vehicle = 1;
            App.CurrentApp.current_van_picture = 0;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.CurrentApp.camera_vehicle = 1;
            App.CurrentApp.current_van_picture = 0;
        }

        private void OnOutsidePhotos(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VanPictures(), false);
        }

        private void OnInsidePhotos(object sender, EventArgs e)
        {
            App.CurrentApp.camera_vehicle = 1;
            App.CurrentApp.current_van_picture = 0;
            Navigation.PushAsync(new Camera(), false);
        }

        private void OnDamagePosition(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VanDiagrams(), false);
        }

        private void OnSignatures(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VanSignatures(), false);
        }
    }
}