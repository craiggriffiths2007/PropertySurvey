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
	public partial class LadderSign2 : ContentPage
	{
		public LadderSign2 ()
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

                fnamea = string.Format("{0:00000000}", App.CurrentApp.LadderRecord.RecID) + "LadSig2.jpg";

                App.CurrentApp.LadderRecord.s_spare4 = fnamea;
                App.CurrentApp.LadderRecord.bSigned2 = true;

                fname = "Signatures/" + fnamea;

                App.files.SaveStream(fname, bitmap);
            }

            await Navigation.PopAsync(false);
        }
    }
}