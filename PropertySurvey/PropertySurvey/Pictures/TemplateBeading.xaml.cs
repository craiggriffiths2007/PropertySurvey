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
	public partial class TemplateBeading : ContentPage
	{
		public TemplateBeading()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            int index = 0;

            //vaList<strir images = App.net.window_fname_list;

            foreach (string fname in App.net.bead_fname_list)
            {
                var image = new Image();

                image.Source = ImageSource.FromResource(fname);

                TapGestureRecognizer tap = new TapGestureRecognizer();
                tap.Tapped += OnImageClicked;

                image.GestureRecognizers.Add(tap);

                image.TabIndex = index++;

                wrapLayout.Children.Add(image);
            }
        }

        private async void OnImageClicked(object sender, EventArgs e)
        {
            Image lblClicked = (Image)sender;

            App.net.load_template_image = true;
            App.net.template_to_load = lblClicked.TabIndex;
            App.net.template_type_to_load = "Bead";
            //Navigation.PushAsync(new DrawingPage(), false);
            Navigation.InsertPageBefore(new DrawingPage(), this);
            await Navigation.PopAsync(false);
            //Image lblClicked = (Image)sender;
            //int index = lblClicked.TabIndex;
            //DisplayAlert("Alert", "You have been alerted by " + index.ToString(), "OK");
            //.exit(0);
        }

    }
}