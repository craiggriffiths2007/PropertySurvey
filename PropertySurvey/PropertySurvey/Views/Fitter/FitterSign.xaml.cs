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
	public partial class FitterSign : ContentPage
	{
		public FitterSign ()
		{
			InitializeComponent ();
            var fs = new FormattedString();
            fs.Spans.Add(new Span { Text = "Arrive : " });
            fs.Spans.Add(new Span { Text = App.CurrentApp.HeaderRecord.ftime_arrived, ForegroundColor = Color.Black, });
            fs.Spans.Add(new Span { Text = "\t\nLeft :" });
            fs.Spans.Add(new Span { Text = App.CurrentApp.HeaderRecord.ftime_left, ForegroundColor = Color.Black, });
            fs.Spans.Add(new Span { Text = "\t\nI confirm that fitter/fitters have attended site during the above times" });

            signature_label.FormattedText = fs;

            signaturePad.CaptionText = "Signed on : " + String.Format("{0:dd / MM / yyyy}", DateTime.Today); 
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
                if (false)//(App.CurrentApp.HeaderRecord.typeB == "Securing")
                {
                    fname = string.Format("Signatures/{0:00000000}_sompleti.jpg", App.CurrentApp.HeaderRecord.udi_cont);
                }
                else
                {
                    fname = string.Format("Signatures/{0:00000000}_fompleti.jpg", App.CurrentApp.HeaderRecord.udi_cont);
                }

                App.files.SaveStream(fname, bitmap);

                App.CurrentApp.HeaderRecord.bcompletion_signed = true;
            }

            await Navigation.PopAsync(false);
        }
    }
}