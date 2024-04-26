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
	public partial class BifoldSignature : ContentPage
	{
		public BifoldSignature ()
		{
			InitializeComponent ();

            signaturePad.CaptionText = "I confirm that the above door and the above threshold are correct.";
            
            switch (App.net.BifoldRecord.threshold_type)
            {
                case "Part M low threshold": cill_image.Source = "partm.jpg"; break;
                case "Frame - no cill": cill_image.Source = "nocill.jpg"; break;
                case "Frame - 150mm cill": cill_image.Source = "cill.jpg"; break;
                case "XPIO Rebated": cill_image.Source = "xpiorebated.jpg"; break;
                case "XPIO Low Threshold": cill_image.Source = "xpiolowthreshold.jpg"; break;
                case "XPIO Rebate and Cill": cill_image.Source = "xp10rebateandcill.jpg"; break;
                case "XPIO Low Threshold and Cill": cill_image.Source = "xp10lowthresholdandcill.jpg"; break;
                case "XP View Rebate": cill_image.Source = "xpviewrebated.jpg"; break;
                case "XP View Rebate and Cill": cill_image.Source = "xpviewrebateandcill.jpg"; break;
                case "PVC threshold": cill_image.Source = "kat_cill_standard.png"; break;
                case "Aluminium low 30mm": cill_image.Source = "kat_cill_room.png"; break;
                case "Doc. M comp. ramp Internal":
                case "Doc. M comp. ramp External": cill_image.Source = "kat_cill_low.png"; break;
                case "Rebated on a cill": cill_image.Source = "rebate_on_sill.png"; break;
                case "Rebated (Standard)": cill_image.Source = "rebated_standard.png"; break;
                case "Low threshold (Internal)": cill_image.Source = "low_threshold.png"; break;
            }
           
            switch (App.CurrentApp.BifoldRecord.number_of_doors_text)
            {
                case "1a": type_image.Source = "bf1a.jpg"; break;
                case "1b": type_image.Source = "bf1b.jpg"; break;
                case "2a": type_image.Source = "bf2a.jpg"; break;
                case "2b": type_image.Source = "bf2b.jpg"; break;
                case "2c": type_image.Source = "bf2c.jpg"; break;
                case "3a": type_image.Source = "bf3a.jpg"; break;
                case "3a2": type_image.Source = "bf3a2.jpg"; break;
                case "3b": type_image.Source = "bf3b.jpg"; break;
                case "3b2": type_image.Source = "bf3b2.jpg"; break;
                case "4a": type_image.Source = "bf4a.jpg"; break;
                case "4b": type_image.Source = "bf4b.jpg"; break;
                case "4c": type_image.Source = "bf4c.jpg"; break;
                case "4d": type_image.Source = "bf4d.jpg"; break;
                case "5a": type_image.Source = "bf5a.jpg"; break;
                case "5a2": type_image.Source = "bf5a2.jpg"; break;
                case "5b": type_image.Source = "bf5b.jpg"; break;
                case "5b2": type_image.Source = "bf5b2.jpg"; break;
                case "5c": type_image.Source = "bf5c.jpg"; break;
                case "5d": type_image.Source = "bf5d.jpg"; break;
                case "6a": type_image.Source = "bf6a.jpg"; break;
                case "6b": type_image.Source = "bf6b.jpg"; break;
                case "6c": type_image.Source = "bf6c.jpg"; break;
                case "6d": type_image.Source = "bf6d.jpg"; break;
                case "6e": type_image.Source = "bf6e.jpg"; break;
                case "7a": type_image.Source = "bf7a.jpg"; break;
                case "7a2": type_image.Source = "bf7a2.jpg"; break;
                case "7b": type_image.Source = "bf7b.jpg"; break;
                case "7b2": type_image.Source = "bf7b2.jpg"; break;
                case "7c": type_image.Source = "bf7c.jpg"; break;
                case "7d": type_image.Source = "bf7d.jpg"; break;
                case "7e": type_image.Source = "bf7e.jpg"; break;
                case "7f": type_image.Source = "bf7f.jpg"; break;


                case "202": type_image.Source = "kat_202.png"; break;
                case "202b": type_image.Source = "kat_202b.png"; break;
                case "321": type_image.Source = "kat_321.png"; break;
                case "330": type_image.Source = "kat_330.png"; break;
                case "321b": type_image.Source = "kat_321b.png"; break;
                case "330b": type_image.Source = "kat_330b.png"; break;
                case "413": type_image.Source = "kat_413.png"; break;
                case "422": type_image.Source = "kat_422.png"; break;
                case "440": type_image.Source = "kat_440.png"; break;
                case "413b": type_image.Source = "kat_413b.png"; break;
                case "422b": type_image.Source = "kat_422b.png"; break;
                case "440b": type_image.Source = "kat_440b.png"; break;
                case "532": type_image.Source = "kat_532.png"; break;
                case "541": type_image.Source = "kat_541.png"; break;
                case "550": type_image.Source = "kat_550.png"; break;
                case "532b": type_image.Source = "kat_532b.png"; break;
                case "541b": type_image.Source = "kat_541b.png"; break;
                case "550b": type_image.Source = "kat_550bpng"; break;
                case "615": type_image.Source = "kat_615.png"; break;
                case "624": type_image.Source = "kat_642.png"; break;
                case "633": type_image.Source = "kat_633.png"; break;
                case "660": type_image.Source = "kat_660.png"; break;
                case "615b": type_image.Source = "kat_615b.png"; break;
                case "624b": type_image.Source = "kat_624b.png"; break;
                case "633b": type_image.Source = "kat_633b.png"; break;
                case "660b": type_image.Source = "kat_660b.png"; break;
                case "770": type_image.Source = "kat_770.png"; break;
                case "770b": type_image.Source = "kat_770b.png"; break;
            }
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
                int num = App.net.random.Next(100000);

                fname = string.Format("Signatures/{0:00000000}_bfs", App.CurrentApp.HeaderRecord.udi_cont);
                fname = fname + string.Format("{0:000}00.jpg", App.CurrentApp.root_item_number);

                App.CurrentApp.BifoldRecord.bifold_signed = 1;

                App.files.SaveStream(fname, bitmap);
            }

            await Navigation.PopAsync(false);
        }

    }
}