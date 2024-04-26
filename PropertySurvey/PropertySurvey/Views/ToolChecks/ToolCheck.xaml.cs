using Plugin.Media;
using Plugin.Media.Abstractions;
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
    public partial class ToolCheck : ContentPage
    {
        public ToolCheck()
        {
            BindingContext = App.CurrentApp.ToolsRecord as ToolsTable;
            InitializeComponent();
            branch.text = App.net.App_Settings.set_branchcode;

            if (App.net.ToolsRecord.RecID == 0)
            {
                App.net.ToolsRecord.registration = App.net.App_Settings.last_reg;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            App.net.App_Settings.last_reg = App.net.ToolsRecord.registration;

            App.data.SaveToolsRecord();

            return false;
        }

        private bool CheckComplete()
        {
            if (App.CurrentApp.ToolsRecord.files_a == 0 ||
            App.CurrentApp.ToolsRecord.pliers_a == 0 ||
            App.CurrentApp.ToolsRecord.chisels_a == 0 ||
            App.CurrentApp.ToolsRecord.pincers_a == 0 ||
            App.CurrentApp.ToolsRecord.scraper_a == 0 ||
            App.CurrentApp.ToolsRecord.hacksaw_a == 0 ||
            App.CurrentApp.ToolsRecord.crowbar_a == 0 ||
            App.CurrentApp.ToolsRecord.handsaw_a == 0 ||
            App.CurrentApp.ToolsRecord.molegrips_a == 0 ||
            App.CurrentApp.ToolsRecord.sidecutters_a == 0 ||
            App.CurrentApp.ToolsRecord.hammer_a == 0 ||
            App.CurrentApp.ToolsRecord.spiritlevel_a == 0 ||
            App.CurrentApp.ToolsRecord.screwdrivers_a == 0 ||
            App.CurrentApp.ToolsRecord.bolsterchisel_a == 0 ||
            App.CurrentApp.ToolsRecord.setsquare_a == 0 ||
            App.CurrentApp.ToolsRecord.stanleyknife_a == 0 ||
            App.CurrentApp.ToolsRecord.clubhammer_a == 0 ||
            App.CurrentApp.ToolsRecord.tapemeasure_a == 0 ||
            App.CurrentApp.ToolsRecord.slidingbevel_a == 0 ||
            App.CurrentApp.ToolsRecord.glazingshovel_a == 0 ||
            App.CurrentApp.ToolsRecord.pointingtrowel_a == 0 ||
            App.CurrentApp.ToolsRecord.setofallenkeys_a == 0 ||
            App.CurrentApp.ToolsRecord.adjustablespanner_a == 0 ||
            App.CurrentApp.ToolsRecord.socketsetjoin_a == 0 ||
            App.CurrentApp.ToolsRecord.copingsawjoin_a == 0 ||
            App.CurrentApp.ToolsRecord.augerbitsjoin_a == 0 ||
            App.CurrentApp.ToolsRecord.nailpunchjoin_a == 0 ||
            App.CurrentApp.ToolsRecord.puttyknifejoin_a == 0 ||
            App.CurrentApp.ToolsRecord.socketsetjoin_a == 0 ||
            App.CurrentApp.ToolsRecord.copingsawjoin_a == 0 ||
            App.CurrentApp.ToolsRecord.rivetgunjoin_a == 0 ||
            App.CurrentApp.ToolsRecord.files_f == 0 ||
            App.CurrentApp.ToolsRecord.pliers_f == 0 ||
            App.CurrentApp.ToolsRecord.chisels_f == 0 ||
            App.CurrentApp.ToolsRecord.pincers_f == 0 ||
            App.CurrentApp.ToolsRecord.scraper_f == 0 ||
            App.CurrentApp.ToolsRecord.hacksaw_f == 0 ||
            App.CurrentApp.ToolsRecord.crowbar_f == 0 ||
            App.CurrentApp.ToolsRecord.handsaw_f == 0 ||
            App.CurrentApp.ToolsRecord.molegrips_f == 0 ||
            App.CurrentApp.ToolsRecord.sidecutters_f == 0 ||
            App.CurrentApp.ToolsRecord.hammer_f == 0 ||
            App.CurrentApp.ToolsRecord.spiritlevel_f == 0 ||
            App.CurrentApp.ToolsRecord.screwdrivers_f == 0 ||
            App.CurrentApp.ToolsRecord.bolsterchisel_f == 0 ||
            App.CurrentApp.ToolsRecord.setsquare_f == 0 ||
            App.CurrentApp.ToolsRecord.stanleyknife_f == 0 ||
            App.CurrentApp.ToolsRecord.clubhammer_f == 0 ||
            App.CurrentApp.ToolsRecord.tapemeasure_f == 0 ||
            App.CurrentApp.ToolsRecord.slidingbevel_f == 0 ||
            App.CurrentApp.ToolsRecord.glazingshovel_f == 0 ||
            App.CurrentApp.ToolsRecord.pointingtrowel_f == 0 ||
            App.CurrentApp.ToolsRecord.setofallenkeys_f == 0 ||
            App.CurrentApp.ToolsRecord.adjustablespanner_f == 0 ||
            App.CurrentApp.ToolsRecord.socketsetjoin_f == 0 ||
            App.CurrentApp.ToolsRecord.copingsawjoin_f == 0 ||
            App.CurrentApp.ToolsRecord.augerbitsjoin_f == 0 ||
            App.CurrentApp.ToolsRecord.nailpunchjoin_f == 0 ||
            App.CurrentApp.ToolsRecord.puttyknifejoin_f == 0 ||
            App.CurrentApp.ToolsRecord.socketsetjoin_f == 0 ||
            App.CurrentApp.ToolsRecord.copingsawjoin_f == 0 ||
            App.CurrentApp.ToolsRecord.rivetgunjoin_f == 0 )
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void OnSign(object sender, EventArgs e)
        {
            if (App.net.ToolsRecord.signature_printed.Length == 0)
            {
                DisplayAlert("Incomplte", "Please add fitter name", "OK");
            }
            else
            {
                if (CheckComplete() == true)
                {
                    Navigation.PushAsync(new ToolCheckSign(), false);
                }
                else
                {
                    DisplayAlert("Incomplte", "Please complete all checks before signing", "OK");
                }
            }
        }

        private void OnSign2(object sender, EventArgs e)
        {
            if (App.net.ToolsRecord.signature_printed2.Length == 0)
            {
                DisplayAlert("Incomplte", "Please add checked by name", "OK");
            }
            else
            {
                if (CheckComplete() == true)
                {
                    Navigation.PushAsync(new ToolCheckSign2(), false);
                }
                else
                {
                    DisplayAlert("Incomplte", "Please complete all checks before signing", "OK");
                }
            }
        }

        private void Send_Clicked(object sender, EventArgs e)
        {
            if (App.net.ToolsRecord.bSigned == true &&
                App.net.ToolsRecord.bSigned2 == true &&
                App.net.ToolsRecord.photo_filename.Length > 0)
            {
                Navigation.PushAsync(new ToolCheckSend(), false);
            }
        }

        private void OnPhotograph(object sender, EventArgs e)
        {
            TakePicture();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetButtons();
        }
        private void SetButtons()
        {
            if (App.net.ToolsRecord.bSigned == true)
                fitter_sign_button.ImageSource = "green_tick.png";
            else
                fitter_sign_button.ImageSource = "question.png";

            if (App.net.ToolsRecord.bSigned2 == true)
                checkby_sign_button.ImageSource = "green_tick.png";
            else
                checkby_sign_button.ImageSource = "question.png";

            if(App.net.ToolsRecord.photo_filename.Length > 0)
                photograph_button.ImageSource = "green_tick.png";
            else
                photograph_button.ImageSource = "question.png";
        }

        public async void TakePicture()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                //await DisplayAlert(Constants.BASE_TITLE, Constants.ERR002, "OK");
                return;
            }
            int num = App.net.random.Next(100000);
            //string dataHora = Constants.GetDateTimeString;
            //string nomeArquivo = string.Format("Foto_{0}.jpg", dataHora);
            App.net.ToolsRecord.photo_filename = "T" + string.Format("{0:0000000}", App.CurrentApp.ToolsRecord.RecID) + App.net.ToolsRecord.date_done.Substring(0, 2) + "-" + App.net.ToolsRecord.date_done.Substring(3, 2) + "-" + App.net.ToolsRecord.date_done.Substring(8, 2) + "-" + num.ToString() + "-" + App.net.App_Settings.set_ownercode + ".png";
            App.data.SaveToolsRecord();

            // Bate uma nova foto e salva a mesma no device
            MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Photos/",
                Name = App.net.ToolsRecord.photo_filename,// = App.net.ToolsRecord.date_done.Substring(0, 2) + "-" + App.net.ToolsRecord.date_done.Substring(3, 2) + "-" + App.net.ToolsRecord.date_done.Substring(8, 2) + "-" + num.ToString() + "-" + App.net.App_Settings.set_ownercode + ".png",
                AllowCropping = true,
                DefaultCamera = CameraDevice.Front
            });

            if (file == null)
                return;

            App.files.SaveStream("Photos/"+ App.net.ToolsRecord.photo_filename,file.GetStream());

            file.Dispose();

            
            /*
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
            */
        }
    }
}

