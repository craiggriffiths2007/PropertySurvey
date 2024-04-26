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
	public partial class SpotCheckSignature : ContentPage
	{
		public SpotCheckSignature ()
		{
			InitializeComponent ();

            signaturePad.CaptionText = "I/we declare that the information given in this form is true and correct to the best of my/our knowledge and belief.";
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return false;
        }

        private void SignatureChanged(object sender, EventArgs e)
        {
            save_button.IsEnabled = true;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            bool saved;
            string fname = "";
            string check_type = "";
            int item_no = 0;

            using (var bitmap = await signaturePad.GetImageStreamAsync(SignatureImageFormat.Jpeg, Color.Black, Color.White, 1f))
            {
                App.CurrentApp.HeaderRecord.uspot_signed = true;
                fname = string.Format("Signatures/{0:00000000}_000SCSig.jpg", App.CurrentApp.HeaderRecord.udi_cont);


                App.files.SaveStream(fname, bitmap);
            }

            await Navigation.PopAsync(false);
        }
    }
}