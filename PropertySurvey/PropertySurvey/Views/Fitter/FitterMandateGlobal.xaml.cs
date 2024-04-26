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
	public partial class FitterMandateGlobal : ContentPage
	{
		public FitterMandateGlobal ()
		{
			InitializeComponent ();
            var fs = new FormattedString();
            fs.Spans.Add(new Span { Text = "I/we " });
            fs.Spans.Add(new Span { Text = App.CurrentApp.HeaderRecord.uc_name, ForegroundColor = Color.Black, });
            fs.Spans.Add(new Span { Text = " of " });
            fs.Spans.Add(new Span { Text = App.CurrentApp.HeaderRecord.add_long, ForegroundColor = Color.Black, });
            fs.Spans.Add(new Span { Text = " confirm that all work has been completed to my/our satisfaction and authorise any remaining stage payments/finance to be collected." });

            signature_label.FormattedText = fs;

            //signaturePad.CaptionText = "I/we " + App.CurrentApp.HeaderRecord.uc_name + " of " + App.CurrentApp.HeaderRecord.add_long + " confirm that all work has been completed to my/our satisfaction and authorise any remaining stage payments/finance to be collected.";
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
                if (false)//(App.CurrentApp.HeaderRecord.typeB == "Securing")
                    fname = string.Format("Signatures/{0:00000000}_sandates.jpg", App.CurrentApp.HeaderRecord.udi_cont);
                else
                    fname = string.Format("Signatures/{0:00000000}_fandates.jpg", App.CurrentApp.HeaderRecord.udi_cont);

                App.files.SaveStream(fname, bitmap);
            }
            App.CurrentApp.HeaderRecord.fmanimage = true;
            await Navigation.PopAsync(false);
        }
    }
}