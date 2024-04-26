using SignaturePad.Forms;
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
	public partial class FitterSignLintel : ContentPage
	{
		public FitterSignLintel ()
		{
			InitializeComponent ();


            signature_label.Text = "I confirm that I have been informed that a lintel is required";

        }

        private void SignatureChanged(object sender, EventArgs e)
        {
            save_button.IsEnabled = true;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            string fname = "";

            using (var bitmap = await signaturePad.GetImageStreamAsync(SignatureImageFormat.Jpeg, Color.Black, Color.White, 1f))
            {
                fname = string.Format("Signatures/{0:00000000}_flintel.jpg", App.CurrentApp.HeaderRecord.udi_cont);

                App.files.SaveStream(fname, bitmap);
            }

            App.CurrentApp.HeaderRecord.uspot_p3 = 1;

            await Navigation.PopAsync(false);
        }
    }
}