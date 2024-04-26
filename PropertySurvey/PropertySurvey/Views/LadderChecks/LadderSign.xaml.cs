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
	public partial class LadderSign : ContentPage
	{
		public LadderSign ()
		{
			InitializeComponent ();
		}

        private void SignatureChanged(object sender, EventArgs e)
        {
            save_button.IsEnabled = true;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            string fname = "";
            string fnamea = "";

            using (var bitmap = await signaturePad.GetImageStreamAsync(SignatureImageFormat.Jpeg, Color.Black, Color.White, 1f))
            {
                int num = App.net.random.Next(100000);

                fnamea = string.Format("{0:00000000}", App.CurrentApp.LadderRecord.RecID) + "LadSig1.jpg";

                App.CurrentApp.LadderRecord.signature_filename = fnamea;
                App.CurrentApp.LadderRecord.bSigned = true;

                fname = "Signatures/" + fnamea;

                App.files.SaveStream(fname, bitmap);
            }

            await Navigation.PopAsync(false);
        }
    }
}