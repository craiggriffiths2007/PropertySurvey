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
    public partial class ToolCheckSign2 : ContentPage
    {
        public ToolCheckSign2()
        {
            InitializeComponent();

            signaturePad.CaptionText = "I confirm that the tool check information is correct";
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

                fname = "T" + string.Format("{0:0000000}", App.CurrentApp.ToolsRecord.RecID) + App.net.ToolsRecord.date_done.Substring(0, 2) + "-" + App.net.ToolsRecord.date_done.Substring(3, 2) + "-" + App.net.ToolsRecord.date_done.Substring(8, 2) + "-" + num.ToString() + "-" + App.net.App_Settings.set_ownercode + ".jpg";

                App.net.ToolsRecord.signature_filename2 = fname;
                App.net.ToolsRecord.bSigned2 = true;

                App.files.SaveStream("Signatures/" + fname, bitmap);
            }

            await Navigation.PopAsync(false);
        }
    }
}