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
	public partial class FitterMandate : ContentPage
	{
		public FitterMandate ()
		{
            InitializeComponent();
			var fs = new FormattedString();
            fs.Spans.Add(new Span { Text = "I/We "  });
            fs.Spans.Add(new Span { Text = App.CurrentApp.HeaderRecord.uc_name, ForegroundColor = Color.Black, });
            fs.Spans.Add(new Span { Text = " of " });
            fs.Spans.Add(new Span { Text = App.CurrentApp.HeaderRecord.add_long + "\n" + "\n", ForegroundColor = Color.Black, });

            if (App.CurrentApp.HeaderRecord.si_mpay.Contains("CRC"))
            {
                fs.Spans.Add(new Span { Text = "I/We have checked all work commissioned and am/are completely satisfied with the installation work completed by the installation team. All items worked on are in complete working order and the property has been left in the same condition as found." + "\n" + "\n" });
                fs.Spans.Add(new Span { Text = "This decleration signifies the start of the 2 year workmanship guarantee."});
            }
            else
            {
                fs.Spans.Add(new Span { Text = " confirm that all work has been completed to my/our satisfaction and authorise any remaining stage payments/finance to be collected." });

            }

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
                if (false) //(App.CurrentApp.HeaderRecord.typeB == "Securing")
                    fname = string.Format("Signatures\\{0:00000000}_sandates.jpg", App.CurrentApp.HeaderRecord.udi_cont);
                else
                    fname = string.Format("Signatures/{0:00000000}_fandates.jpg", App.CurrentApp.HeaderRecord.udi_cont);

                App.files.SaveStream(fname, bitmap);
            }
            App.CurrentApp.HeaderRecord.fmanimage = true;
            await Navigation.PopAsync(false);
        }
    }
}