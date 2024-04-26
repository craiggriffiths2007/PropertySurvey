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
    public partial class VanCheckedSignature : ContentPage
    {
        public VanCheckedSignature()
        {
            InitializeComponent();

            signaturePad.CaptionText = "It is the responsibility of the user of the vehicle to take responsible care, undertake basic maintenance of all the above items, report any loss or damage immediately for replacement purposes. My signature acknowledges this.";

        }

        private void SignatureChanged(object sender, EventArgs e)
        {
            save_button.IsEnabled = true;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            string fname = "";
            string check_type = "";
            int item_no = 0;

            using (var bitmap = await signaturePad.GetImageStreamAsync(SignatureImageFormat.Png, Color.Black, Color.White, 1f))
            {
                switch (App.CurrentApp.CurrentItem)
                {
                    case "deliveryvan": App.CurrentApp.DeliveryVanVehicleCheckList.bCheckedBySigned = true; item_no = App.CurrentApp.DeliveryVanVehicleCheckList.item_no; check_type = "a"; break;
                    case "delivery": App.CurrentApp.DeliveryVehicleCheckList.bCheckedBySigned = true; item_no = App.CurrentApp.DeliveryVehicleCheckList.item_no; check_type = "d"; break;
                    case "van": App.CurrentApp.WeeklyVanCheckSheet.bCheckedBySigned = true; item_no = App.CurrentApp.WeeklyVanCheckSheet.item_no; check_type = "v"; break;
                    case "car": App.CurrentApp.CarPanelSheet.bCheckedBySigned = true; item_no = App.CurrentApp.CarPanelSheet.item_no; check_type = "c"; break;
                }

                switch (App.net.drawing_type)
                {
                    case "driver":
                        fname = string.Format("Signatures/VC/" + App.net.VanChecksHeader.unique_id + "_{0:00000000}_" + check_type + "_dsi.jpg", item_no); break;
                    case "checker":
                        fname = string.Format("Signatures/VC/" + App.net.VanChecksHeader.unique_id + "_{0:00000000}_" + check_type + "_csi.jpg", item_no); break;
                }

                App.files.SaveStream(fname, bitmap);
            }

            await Navigation.PopAsync(false);
        }


        protected override bool OnBackButtonPressed()
        {

            //Navigation.PopToRootAsync();


            base.OnBackButtonPressed();
            return false;
        }

    }
}