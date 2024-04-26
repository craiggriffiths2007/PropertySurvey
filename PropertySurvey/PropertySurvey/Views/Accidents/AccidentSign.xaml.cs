using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SignaturePad.Forms;

namespace PropertySurvey
{
    public enum accident_signature_type { vehicle_accident_signature, work_accident_signature, work_supervisor_signature };

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccidentSign : ContentPage
    {
        accident_signature_type which_signature;

        public AccidentSign(accident_signature_type _which_signature)
        {
            which_signature = _which_signature;
            InitializeComponent();
            date_name_label.Text = "Signed on : " + String.Format("{0:dd/MM/yyyy}", DateTime.Today);
        }

        private void SignatureChanged(object sender, EventArgs e)
        {
            save_button.IsEnabled = true;
        }

        private async void save_clicked(object sender, EventArgs e)
        {
            string fname = "";

            using (var bitmap = await signaturePad.GetImageStreamAsync(SignatureImageFormat.Jpeg, Color.Black, Color.White, 1f))
            {
                switch (which_signature)
                {
                    case accident_signature_type.vehicle_accident_signature:
                        fname = String.Format("Signatures/9{0:0000000}_sig", App.CurrentApp.AccidentRecord.RecID) + ".jpg";
                        App.CurrentApp.AccidentRecord.y_signed = true;
                        break;
                    case accident_signature_type.work_accident_signature:
                        fname = string.Format("Signatures/8{0:0000000}", App.CurrentApp.FAccidentsRecord.RecID) + "FacciSig1.jpg";
                        App.CurrentApp.FAccidentsRecord.person_signed = 1;
                        App.net.FAccidentsRecord.spare1 = fname;
                        break;
                    case accident_signature_type.work_supervisor_signature:
                        fname = string.Format("Signatures/8{0:0000000}", App.CurrentApp.FAccidentsRecord.RecID) + "FacciSig2.jpg";
                        App.CurrentApp.FAccidentsRecord.supervisor_signed = 1;
                        App.net.FAccidentsRecord.spare2 = fname;
                        break;
                }

                App.files.SaveStream(fname, bitmap);
            }

            await Navigation.PopAsync(false);
        }
    }
}