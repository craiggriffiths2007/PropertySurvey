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
	public partial class MotorSign : ContentPage
	{
		public MotorSign ()
		{
			InitializeComponent ();
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


                App.files.SaveStream(fname, bitmap);
            }
            App.net.HeaderRecord.i_signed = 1;
            await Navigation.PopAsync(false);
        }
    }
}