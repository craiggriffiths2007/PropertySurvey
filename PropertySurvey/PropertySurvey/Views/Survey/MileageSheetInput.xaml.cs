using Plugin.Media;
using Plugin.Media.Abstractions;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MileageSheetInput : ContentPage
	{
		public MileageSheetInput()
		{
			InitializeComponent ();
            
            if (App.net.App_Settings.set_usertype == "SUR")
            {
                postcode_stack_panel.IsVisible = true;
                time_stack_panel.IsVisible = false;
            }
            if (App.net.App_Settings.set_usertype == "FIT" || App.net.App_Settings.set_usertype == "SAT")
            {
                postcode_stack_panel.IsVisible = false;
                time_stack_panel.IsVisible = true;
            }
            
            if (App.net.MileageRecord.RecID == 0)
            {
                App.net.MileageRecord.registration = App.net.App_Settings.last_reg;
                App.net.MileageRecord.start_postcode = App.net.App_Settings.last_pcode_from;
                App.net.MileageRecord.finish_postcode = App.net.App_Settings.last_pcode_to;
            }
            //CrossMedia.Current.Initialize();

            SetOtherPlaces();
            SetMileageButtons();
            // Needs to be here if changed in this method
            BindingContext = App.net.MileageRecord as Milage_sheet;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            SetMileageButtons();
        }

        private void StartUpCamera2()
        {
            //Xamarin.Forms.DependencyService.Register<ICameraHelper>();
            //DependencyService.Get<ICameraHelper>().StartCamera();
        }

        private void SetMileageButtons()
        {
            if (App.net.MileageRecord.new_sspare1.Length > 0)
                startmileButton.ImageSource = "green_tick.png";
            else
                startmileButton.ImageSource = "question.png";

            if (App.net.MileageRecord.new_sspare2.Length > 0)
                endmileButton.ImageSource = "green_tick.png";
            else
                endmileButton.ImageSource = "question.png";


            if (App.net.MileageRecord.bSigned == true)
                sign_tick.Source = "green_tick.png";
            else
                sign_tick.Source = "";
        }

        private async void SMButton_Clicked(object sender, EventArgs e)
        {
            string fname = "";
            int num = App.net.random.Next(100000);
            

            if (true)
            {

                fname = App.net.MileageRecord.sheet_date.Substring(0, 2) + "-" + App.net.MileageRecord.sheet_date.Substring(3, 2) + "-" + App.net.MileageRecord.sheet_date.Substring(8, 2) + "-" + num.ToString() + "-" + App.net.App_Settings.set_ownercode + ".jpg";

                App.net.MileageRecord.new_sspare1 = "Photos/" + fname;

                App.data.SaveMileage();

                App.net.photos_taken = 0;
                App.net.photos_required = 1;
                App.net.photos_enable_increment = false;
                App.net.photo_fname = fname;

                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    //await DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Photos",
                    Name = App.net.MileageRecord.sheet_date.Substring(0, 2) + "-" + App.net.MileageRecord.sheet_date.Substring(3, 2) + "-" + App.net.MileageRecord.sheet_date.Substring(8, 2) + "-" + num.ToString() + "-" + App.net.App_Settings.set_ownercode + ".jpg",
                    PhotoSize = PhotoSize.Small
                });

                

                var bitmap = SKBitmap.Decode(file.GetStream());
                var canvas = new SKCanvas(bitmap);
                var font = SKTypeface.FromFamilyName("Arial");
                var brush1 = new SKPaint
                {
                    Typeface = font,
                    TextSize = Convert.ToInt64(Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                    IsAntialias = true,
                    Color = new SKColor(255, 255, 255, 255),
                    
                };

                var brush2 = new SKPaint
                {
                    Typeface = font,
                    TextSize = Convert.ToInt64(Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                    IsAntialias = true,
                    Color = new SKColor(0, 0, 0, 255)
                };

                canvas.DrawText(DateTime.Now.ToString("dd/MM/yyyy HH:mm"), 20, 20, brush1);
                canvas.DrawText(DateTime.Now.ToString("dd/MM/yyyy HH:mm"), 22, 22, brush2);
                var imageSK = SKImage.FromBitmap(bitmap);

                using (var image = SKImage.FromBitmap(bitmap))
                using (var data = image.Encode(SKEncodedImageFormat.Png, 80))
                {
                    byte[] dd = data.ToArray();
                    App.files.SaveBinary("Photos/" + App.net.photo_fname, data.ToArray());
                }

                imageSK.Dispose();
                bitmap.Dispose();
                file.Dispose();
            }
            else
            {
                //string fname = "";
                //int num = App.net.random.Next(100000);

                fname = App.net.MileageRecord.sheet_date.Substring(0, 2) + "-" + App.net.MileageRecord.sheet_date.Substring(3, 2) + "-" + App.net.MileageRecord.sheet_date.Substring(8, 2) + "-" + num.ToString() + "-" + App.net.App_Settings.set_ownercode + ".jpg";

                App.net.MileageRecord.new_sspare1 = "Photos/" + fname;

                App.net.photos_taken = 0;
                App.net.photos_required = 1;
                App.net.photos_enable_increment = false;
                App.net.photo_fname = fname;
                Device.BeginInvokeOnMainThread(StartUpCamera2);

                //await CrossMedia.Current.Initialize();
                //TakePicture();
                //Device.BeginInvokeOnMainThread(TakePicture);
                //CrossMedia.Current.TakePhotoAsync();

                //Task<MediaFile> TakePhotoAsync(StoreCameraMediaOptions options);
            }
        }

        private async void EMButton_Clicked(object sender, EventArgs e)
        {
            string fname = "";
            int num = App.net.random.Next(100000);
            App.data.SaveMileage();
            if (true)
            {
                fname = App.net.MileageRecord.sheet_date.Substring(0, 2) + "-" + App.net.MileageRecord.sheet_date.Substring(3, 2) + "-" + App.net.MileageRecord.sheet_date.Substring(8, 2) + "-" + num.ToString() + "-" + App.net.App_Settings.set_ownercode + ".jpg";

                App.net.MileageRecord.new_sspare2 = "Photos/" + fname;

                App.net.photos_taken = 0;
                App.net.photos_required = 1;
                App.net.photos_enable_increment = false;
                App.net.photo_fname = fname;

                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    //await DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Photos",
                    Name = App.net.MileageRecord.sheet_date.Substring(0, 2) + "-" + App.net.MileageRecord.sheet_date.Substring(3, 2) + "-" + App.net.MileageRecord.sheet_date.Substring(8, 2) + "-" + num.ToString() + "-" + App.net.App_Settings.set_ownercode + ".jpg",
                    PhotoSize = PhotoSize.Small
                });

                var bitmap = SKBitmap.Decode(file.GetStream());
                var canvas = new SKCanvas(bitmap);
                var font = SKTypeface.FromFamilyName("Arial");
                var brush1 = new SKPaint
                {
                    Typeface = font,
                    TextSize = Convert.ToInt64(Device.GetNamedSize(NamedSize.Large, typeof(Label))),
                    IsAntialias = true,
                    Color = new SKColor(255, 255, 255, 255),

                };

                var brush2 = new SKPaint
                {
                    Typeface = font,
                    TextSize = Convert.ToInt64(Device.GetNamedSize(NamedSize.Large, typeof(Label))),
                    IsAntialias = true,
                    Color = new SKColor(0, 0, 0, 255)
                };

                canvas.DrawText(DateTime.Now.ToString("dd/MM/yyyy HH:mm"), 20, 20, brush1);
                canvas.DrawText(DateTime.Now.ToString("dd/MM/yyyy HH:mm"), 22, 22, brush2);
                var imageSK = SKImage.FromBitmap(bitmap);

                using (var image = SKImage.FromBitmap(bitmap))
                using (var data = image.Encode(SKEncodedImageFormat.Png, 80))
                {
                    byte[] dd = data.ToArray();
                    App.files.SaveBinary("Photos/" + App.net.photo_fname, data.ToArray());
                }

                imageSK.Dispose();
                bitmap.Dispose();
                file.Dispose();
            }
            else
            {
                //string fname = "";
                //int num = App.net.random.Next(100000);

                fname = App.net.MileageRecord.sheet_date.Substring(0, 2) + "-" + App.net.MileageRecord.sheet_date.Substring(3, 2) + "-" + App.net.MileageRecord.sheet_date.Substring(8, 2) + "-" + num.ToString() + "-" + App.net.App_Settings.set_ownercode + ".jpg";

                App.net.MileageRecord.new_sspare2 = "Photos/" + fname;

                App.net.photos_taken = 0;
                App.net.photos_required = 1;
                App.net.photos_enable_increment = false;
                App.net.photo_fname = fname;
                Device.BeginInvokeOnMainThread(StartUpCamera2);
            }
        }


        public async void TakePicture()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                //await DisplayAlert(Constants.BASE_TITLE, Constants.ERR002, "OK");
                return;
            }

            //string dataHora = Constants.GetDateTimeString;
            //string nomeArquivo = string.Format("Foto_{0}.jpg", dataHora);

            // Bate uma nova foto e salva a mesma no device
            MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Photos/",
                Name = App.net.photo_fname,
                AllowCropping = true,
                DefaultCamera = CameraDevice.Front
            });

            if (file == null)
                return;

            var resizeFactor = 0.5f;
            var bitmap = SKBitmap.Decode(file.GetStream());
            var canvas = new SKCanvas(bitmap);
            var font = SKTypeface.FromFamilyName("Arial");
            var brush = new SKPaint
            {
                Typeface = font,
                TextSize = Convert.ToInt64(Device.GetNamedSize(NamedSize.Default, typeof(Label))),
                IsAntialias = true,
                Color = new SKColor(255, 255, 255, 255)
            };

            canvas.DrawText(DateTime.Now.ToString("dd/MM/yyyy HH:mm"), 0, bitmap.Height * resizeFactor / 2.0f, brush);
            var imageSK = SKImage.FromBitmap(bitmap);
            //image.Source = (SKImageImageSource)imageSK;
            //file.sa
            // create an image and then get the PNG (or any other) encoded data
            using (var image = SKImage.FromBitmap(bitmap))
            using (var data = image.Encode(SKEncodedImageFormat.Png, 80))
            {
                byte[] dd = data.ToArray();
                App.files.SaveBinary("Photos/" + App.net.photo_fname, data.ToArray());
            }

            imageSK.Dispose();
            bitmap.Dispose();
            file.Dispose();
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            App.net.App_Settings.last_reg = App.net.MileageRecord.registration;
            App.net.App_Settings.last_pcode_from = App.net.MileageRecord.start_postcode;
            App.net.App_Settings.last_pcode_to = App.net.MileageRecord.finish_postcode;

            App.data.SaveSettings();
            App.data.SaveMileage();

            base.OnBackButtonPressed();
            return false;
        }

        private void SetOtherPlaces()
        {
            int val = App.net.MileageRecord.no_of_other_places;

            if (val > 0)
                op1.IsVisible = true;
            else
                op1.IsVisible = false;
            if (val > 1)
                op2.IsVisible = true;
            else
                op2.IsVisible = false;
            if (val > 2)
                op3.IsVisible = true;
            else
                op3.IsVisible = false;
            if (val > 3)
                op4.IsVisible = true;
            else
                op4.IsVisible = false;
            if (val > 4)
                op5.IsVisible = true;
            else
                op5.IsVisible = false;
            if (val > 5)
                op6.IsVisible = true;
            else
                op6.IsVisible = false;
            if (val > 6)
                op7.IsVisible = true;
            else
                op7.IsVisible = false;
            if (val > 7)
                op8.IsVisible = true;
            else
                op8.IsVisible = false;
            if (val > 8)
                op9.IsVisible = true;
            else
                op9.IsVisible = false;
            if (val > 9)
                op10.IsVisible = true;
            else
                op10.IsVisible = false;
        }

        private void OnOtherPlacesChanged(object sender, EventArgs e)
        {
            SetOtherPlaces();
        }

        private void SignButton_Clicked(object sender, EventArgs e)
        {
            App.net.App_Settings.last_reg = App.net.MileageRecord.registration;
            App.net.App_Settings.last_pcode_from = App.net.MileageRecord.start_postcode;
            App.net.App_Settings.last_pcode_to = App.net.MileageRecord.finish_postcode;

            App.data.SaveSettings();
            App.data.SaveMileage();

            Navigation.PushAsync(new MileageSign(), false);
        }

        private async void Send_Clicked(object sender, EventArgs e)
        {
            if (((App.net.App_Settings.set_usertype == "SUR") && (App.net.MileageRecord.start_postcode.Length == 0 || App.net.MileageRecord.finish_postcode.Length == 0)) || App.net.MileageRecord.start_mileage.Length == 0 || App.net.MileageRecord.end_mileage.Length == 0)
            {
                //MessageBox.Show("Please complete all the required fields before sending this record.");
            }
            else
            {
                if (App.net.MileageRecord.bSigned == true)
                {
                    App.net.App_Settings.last_reg = App.net.MileageRecord.registration;
                    App.net.App_Settings.last_pcode_from = App.net.MileageRecord.start_postcode;
                    App.net.App_Settings.last_pcode_to = App.net.MileageRecord.finish_postcode;

                    App.data.SaveSettings();

                    App.data.SaveMileage();

                    Navigation.InsertPageBefore(new MileageSend(), this);
                    await Navigation.PopAsync(false);
                }
            }
        }

        private void toll_charges_changed(object sender, EventArgs e)
        {
            if(App.net.MileageRecord.toll_charges==1)
            {
                toll_charges_area.IsVisible= true;
            }
            else
            {
                toll_charges_area.IsVisible = false;
            }
        }
    }
}