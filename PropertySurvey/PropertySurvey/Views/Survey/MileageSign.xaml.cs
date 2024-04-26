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
    public partial class MileageSign : ContentPage
    {
        public MileageSign()
        {
            InitializeComponent();

            signaturePad.CaptionText = "I confirm that the mileage claimed is a true and accurate account of the actual mileage used solely for business for Martindales Ltd and understand that any claim found to be deliberately exaggerated  will result in disciplinary action being taken by Martindales Ltd.";
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

                fname = "Signatures/" + App.net.MileageRecord.sheet_date.Substring(0,2) + "-" + App.net.MileageRecord.sheet_date.Substring(3, 2) + "-" + App.net.MileageRecord.sheet_date.Substring(8, 2) + "-" + num.ToString() + "-" + App.net.App_Settings.set_ownercode + ".jpg";

                App.net.MileageRecord.signature_filename = fname;
                App.net.MileageRecord.bSigned = true;

                App.files.SaveStream(fname, bitmap);
            }

            await Navigation.PopAsync(false);
        }
    }
}