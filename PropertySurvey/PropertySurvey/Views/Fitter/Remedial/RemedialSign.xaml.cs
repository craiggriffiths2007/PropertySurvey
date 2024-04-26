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
	public partial class RemedialSign : ContentPage
	{
		public RemedialSign ()
		{
			InitializeComponent ();

            signature_label.Text = "I confirm the work has been completed to my / our satisfaction.";
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
                fname = string.Format("Signatures/{0:00000000}_remesign.jpg", App.CurrentApp.HeaderRecord.udi_cont);

                App.files.SaveStream(fname, bitmap);
            }

            App.CurrentApp.HeaderRecord.r_bsigned = true;

            await Navigation.PopAsync(false);
        }
    }
}