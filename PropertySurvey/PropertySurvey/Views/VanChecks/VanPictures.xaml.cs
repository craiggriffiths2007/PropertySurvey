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
	public partial class VanPictures : ContentPage
	{
		public VanPictures ()
		{
			InitializeComponent ();

            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += OnImage1Clicked;
            van1.GestureRecognizers.Add(tap);

            tap = new TapGestureRecognizer();
            tap.Tapped += OnImage2Clicked;
            van2.GestureRecognizers.Add(tap);

            tap = new TapGestureRecognizer();
            tap.Tapped += OnImage3Clicked;
            van3.GestureRecognizers.Add(tap);

            tap = new TapGestureRecognizer();
            tap.Tapped += OnImage4Clicked;
            van4.GestureRecognizers.Add(tap);

            App.net.photos_required = 1;
            App.net.camera_landscape_mode = true;
            
        }
        private void SaveCheck()
        {

        }

        private void OnImage1Clicked(object sender, EventArgs e)
        {
            App.net.current_van_picture = 1;
            MakeVanFilename();
            Device.BeginInvokeOnMainThread(StartUpCamera2);
        }
        private void OnImage2Clicked(object sender, EventArgs e)
        {
            App.net.current_van_picture = 2;
            MakeVanFilename();
            Device.BeginInvokeOnMainThread(StartUpCamera2);
        }
        private void OnImage3Clicked(object sender, EventArgs e)
        {
            App.net.current_van_picture = 3;
            MakeVanFilename();
            Device.BeginInvokeOnMainThread(StartUpCamera2);
        }
        private void OnImage4Clicked(object sender, EventArgs e)
        {
            App.net.current_van_picture = 4;
            MakeVanFilename();
            Device.BeginInvokeOnMainThread(StartUpCamera2);
        }
        private void StartUpCamera2()
        {
            switch (App.CurrentApp.current_van_picture)
            {
                case 1:
                    switch (App.CurrentApp.CurrentItem)
                    {
                        case "deliveryvan": App.CurrentApp.DeliveryVanVehicleCheckList.photos_left = 1; break;
                        case "delivery": App.CurrentApp.DeliveryVehicleCheckList.photos_left = 1; break;
                        case "van": App.CurrentApp.WeeklyVanCheckSheet.photos_left = 1; break;
                        case "car": App.CurrentApp.CarPanelSheet.photos_left = 1; break;
                    }
                    break;
                case 2:
                    switch (App.CurrentApp.CurrentItem)
                    {
                        case "deliveryvan": App.CurrentApp.DeliveryVanVehicleCheckList.photos_right = 1; break;
                        case "delivery": App.CurrentApp.DeliveryVehicleCheckList.photos_right = 1; break;
                        case "van": App.CurrentApp.WeeklyVanCheckSheet.photos_right = 1; break;
                        case "car": App.CurrentApp.CarPanelSheet.photos_right = 1; break;
                    }
                    break;
                case 3:
                    switch (App.CurrentApp.CurrentItem)
                    {
                        case "deliveryvan": App.CurrentApp.DeliveryVanVehicleCheckList.photos_front = 1; break;
                        case "delivery": App.CurrentApp.DeliveryVehicleCheckList.photos_front = 1; break;
                        case "van": App.CurrentApp.WeeklyVanCheckSheet.photos_front = 1; break;
                        case "car": App.CurrentApp.CarPanelSheet.photos_front = 1; break;
                    }
                    break;
                case 4:
                    switch (App.CurrentApp.CurrentItem)
                    {
                        case "deliveryvan": App.CurrentApp.DeliveryVanVehicleCheckList.photos_rear = 1; break;
                        case "delivery": App.CurrentApp.DeliveryVehicleCheckList.photos_rear = 1; break;
                        case "van": App.CurrentApp.WeeklyVanCheckSheet.photos_rear = 1; break;
                        case "car": App.CurrentApp.CarPanelSheet.photos_rear = 1; break;
                    }
                    break;
            }

            //Xamarin.Forms.DependencyService.Register<ICameraHelper>();
            //DependencyService.Get<ICameraHelper>().StartCameraLandscape();
            //DependencyService.Get<ICameraHelper>().StartCameraLandscapeFixed();
        }
        private void MakeVanFilename()
        {
            int item_no = 0;
            string check_type = "";
            switch (App.CurrentApp.CurrentItem)
            {
                case "deliveryvan": item_no = App.CurrentApp.DeliveryVanVehicleCheckList.item_no; check_type = "a"; break;
                case "delivery": item_no = App.CurrentApp.DeliveryVehicleCheckList.item_no; check_type = "d"; break;
                case "van": item_no = App.CurrentApp.WeeklyVanCheckSheet.item_no; check_type = "v"; break;
                case "car": item_no = App.CurrentApp.CarPanelSheet.item_no; check_type = "c"; break;
            }
            switch (App.CurrentApp.current_van_picture)
            {
                case 1:
                    App.net.photo_fname = string.Format("VC/" + App.CurrentApp.VanChecksHeader.unique_id + "_{0:00000000}_" + check_type + "_pas.jpg", item_no); break;
                case 2:
                    App.net.photo_fname = string.Format("VC/" + App.CurrentApp.VanChecksHeader.unique_id + "_{0:00000000}_" + check_type + "_drv.jpg", item_no); break;
                case 3:
                    App.net.photo_fname = string.Format("VC/" + App.CurrentApp.VanChecksHeader.unique_id + "_{0:00000000}_" + check_type + "_fro.jpg", item_no); break;
                case 4:
                    App.net.photo_fname = string.Format("VC/" + App.CurrentApp.VanChecksHeader.unique_id + "_{0:00000000}_" + check_type + "_rea.jpg", item_no); break;
                //case 5:
                //    App.net.photo_fname = string.Format("Photos/VC/" + App.CurrentApp.VanChecksHeader.unique_id + "_{0:00000000}_" + check_type + "_cab.jpg", item_no); break;
                //case 6:
                //    App.net.photo_fname = string.Format("Photos/VC/" + App.CurrentApp.VanChecksHeader.unique_id + "_{0:00000000}_" + check_type + "_ins.jpg", item_no); break;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Send(this, "preventPortraitPic");
            App.CurrentApp.camera_vehicle = 0;
            SetImages();
            switch (App.CurrentApp.CurrentItem)
            {
                case "deliveryvan": App.data.SaveVanChecksDeliveryVan(); break;
                case "delivery": App.data.SaveVanChecksDelivery(); break;
                case "van": App.data.SaveVanChecksVan(); break;
                case "car": App.data.SaveVanChecksCar(); break;
            }
        }

        //during page close setting back to portrait
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            
        }

        private void SetImages()
        {
            switch (App.CurrentApp.CurrentItem)
            {
                case "deliveryvan":
                    App.CurrentApp.current_van_picture = 1; MakeVanFilename();
                    if (App.files.FileExists("Photos/" + App.net.photo_fname))
                        van1.Source = ImageSource.FromFile(App.files.CreatePathToFile("Photos/" + App.net.photo_fname));
                    else
                        van1.Source = ImageSource.FromResource("PropertySurvey.Images.hgv_pas.png");
                    App.CurrentApp.current_van_picture = 2; MakeVanFilename();
                    if (App.files.FileExists("Photos/" + App.net.photo_fname))
                        van2.Source = ImageSource.FromFile(App.files.CreatePathToFile("Photos/" + App.net.photo_fname));
                    else
                        van2.Source = ImageSource.FromResource("PropertySurvey.Images.hgv_drv.png");
                    App.CurrentApp.current_van_picture = 3; MakeVanFilename();
                    if (App.files.FileExists("Photos/" + App.net.photo_fname))
                        van3.Source = ImageSource.FromFile(App.files.CreatePathToFile("Photos/" + App.net.photo_fname));
                    else
                        van3.Source = ImageSource.FromResource("PropertySurvey.Images.hgv_fnt.png");
                    App.CurrentApp.current_van_picture = 4; MakeVanFilename();
                    if (App.files.FileExists("Photos/" + App.net.photo_fname))
                        van4.Source = ImageSource.FromFile(App.files.CreatePathToFile("Photos/" + App.net.photo_fname));
                    else
                        van4.Source = ImageSource.FromResource("PropertySurvey.Images.hgv_bak.png");
                    break;

                case "delivery":
                    App.CurrentApp.current_van_picture = 1; MakeVanFilename();
                    if (App.files.FileExists("Photos/" + App.net.photo_fname))
                        van1.Source = ImageSource.FromFile(App.files.CreatePathToFile("Photos/" + App.net.photo_fname));
                    else
                        van1.Source = ImageSource.FromResource("PropertySurvey.Images.hgv_pas.png");
                    App.CurrentApp.current_van_picture = 2; MakeVanFilename();
                    if (App.files.FileExists("Photos/" + App.net.photo_fname))
                        van2.Source = ImageSource.FromFile(App.files.CreatePathToFile("Photos/" + App.net.photo_fname));
                    else
                        van2.Source = ImageSource.FromResource("PropertySurvey.Images.hgv_drv.png");
                    App.CurrentApp.current_van_picture = 3; MakeVanFilename();
                    if (App.files.FileExists("Photos/" + App.net.photo_fname))
                        van3.Source = ImageSource.FromFile(App.files.CreatePathToFile("Photos/" + App.net.photo_fname));
                    else
                        van3.Source = ImageSource.FromResource("PropertySurvey.Images.hgv_fnt.png");
                    App.CurrentApp.current_van_picture = 4; MakeVanFilename();
                    if (App.files.FileExists("Photos/" + App.net.photo_fname))
                        van4.Source = ImageSource.FromFile(App.files.CreatePathToFile("Photos/" + App.net.photo_fname));
                    else
                        van4.Source = ImageSource.FromResource("PropertySurvey.Images.hgv_bak.png");
                    break;

                case "van":
                    App.CurrentApp.current_van_picture = 1; MakeVanFilename();
                    if(App.files.FileExists("Photos/" + App.net.photo_fname))
                        van1.Source = ImageSource.FromFile(App.files.CreatePathToFile("Photos/" + App.net.photo_fname));
                    else
                        van1.Source = ImageSource.FromResource("PropertySurvey.Images.van1.png");
                    App.CurrentApp.current_van_picture = 2; MakeVanFilename();
                    if (App.files.FileExists("Photos/" + App.net.photo_fname))
                        van2.Source = ImageSource.FromFile(App.files.CreatePathToFile("Photos/" + App.net.photo_fname));
                    else
                        van2.Source = ImageSource.FromResource("PropertySurvey.Images.van2.png");
                    App.CurrentApp.current_van_picture = 3; MakeVanFilename();
                    if (App.files.FileExists("Photos/" + App.net.photo_fname))
                        van3.Source = ImageSource.FromFile(App.files.CreatePathToFile("Photos/" + App.net.photo_fname));
                    else
                        van3.Source = ImageSource.FromResource("PropertySurvey.Images.van3.png");
                    App.CurrentApp.current_van_picture = 4; MakeVanFilename();
                    if (App.files.FileExists("Photos/" + App.net.photo_fname))
                        van4.Source = ImageSource.FromFile(App.files.CreatePathToFile("Photos/" + App.net.photo_fname));
                    else
                        van4.Source = ImageSource.FromResource("PropertySurvey.Images.van4.png");
                    break;

                case "car":
                    App.CurrentApp.current_van_picture = 1; MakeVanFilename();
                    if (App.files.FileExists("Photos/" + App.net.photo_fname))
                        van1.Source = ImageSource.FromFile(App.files.CreatePathToFile("Photos/" + App.net.photo_fname));
                    else
                        van1.Source = ImageSource.FromResource("PropertySurvey.Images.car_pas.png");
                    App.CurrentApp.current_van_picture = 2; MakeVanFilename();
                    if (App.files.FileExists("Photos/" + App.net.photo_fname))
                        van2.Source = ImageSource.FromFile(App.files.CreatePathToFile("Photos/" + App.net.photo_fname));
                    else
                        van2.Source = ImageSource.FromResource("PropertySurvey.Images.car_drv.png");
                    App.CurrentApp.current_van_picture = 3; MakeVanFilename();
                    if (App.files.FileExists("Photos/" + App.net.photo_fname))
                        van3.Source = ImageSource.FromFile(App.files.CreatePathToFile("Photos/" + App.net.photo_fname));
                    else
                        van3.Source = ImageSource.FromResource("PropertySurvey.Images.car_fnt.png");
                    App.CurrentApp.current_van_picture = 4; MakeVanFilename();
                    if (App.files.FileExists("Photos/" + App.net.photo_fname))
                        van4.Source = ImageSource.FromFile(App.files.CreatePathToFile("Photos/" + App.net.photo_fname));
                    else
                        van4.Source = ImageSource.FromResource("PropertySurvey.Images.car_bak.png");
                    break;

            }
        }



        protected override bool OnBackButtonPressed()
        {
            //MessagingCenter.Send(this, "allowLandScapePortrait");
            MessagingCenter.Send(this, "allowLandScapePortraitPic");
            return false;
        }
    }
}