using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MartControls;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TemplateWindows : ContentPage
	{
        public TemplateWindows()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            int index = 0;

            //wrapLayout.

            //vaList<strir images = App.net.window_fname_list;

            foreach (string fname in App.net.window_fname_list)
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
            App.net.template_type_to_load = "Windows";

            Navigation.InsertPageBefore(new DrawingPage(), this);
            await Navigation.PopAsync(false);
            //Navigation.PushAsync(new DrawingPage(), false);
            //Image lblClicked = (Image)sender;
            //int index = lblClicked.TabIndex;
            //DisplayAlert("Alert", "You have been alerted by " + index.ToString(), "OK");
            //.exit(0);
        }
	}
}