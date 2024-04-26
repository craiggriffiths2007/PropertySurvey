using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SignaturePad.Forms;

namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RALSignature : ContentPage
    {
        public RALSignature()
        {
            InitializeComponent();

            frame_inside.Text = "INS - " + App.CurrentApp.TimberRecord.frame_color_code;
            frame_outside.Text = "OUT - " + App.CurrentApp.TimberRecord.frame_color_code_out;
            door_inside.Text = "INS - " + App.CurrentApp.TimberRecord.door_color_code;
            door_outside.Text = "OUT - " + App.CurrentApp.TimberRecord.door_color_code_out;

            signaturePad.CaptionText = App.net.HeaderRecord.uc_name + "       " + DateTime.Today.ToShortDateString();
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return false;
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
                fname = string.Format("Signatures/{0:00000000}_col", App.CurrentApp.HeaderRecord.udi_cont);
                fname = fname + string.Format("{0:000}00.jpg", App.CurrentApp.root_item_number);

                App.files.SaveStream(fname, bitmap);
            }

            await Navigation.PopAsync(false);
        }
    }
}