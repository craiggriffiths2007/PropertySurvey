using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using TouchTracking;
using MartControls;
using Xamarin.Essentials;
using Plugin.Media;
using PropertySurvey.CustomViews;

namespace PropertySurvey
{
    public partial class Camera
        : ContentPage
    {
        bool bReadOnly = false;
        List<string> fileNames;
        //int imageNumber;
        int total_photos = 0;
        int current_drawing = 0;
        TouchManipulationBitmap bitmap;
        List<long> touchIds = new List<long>();
        MatrixDisplay matrixDisplay = new MatrixDisplay();

        //SKBitmap bitmap = null;
        float ScreenWidth = 0.0f;
        float ScreenHeight = 0.0f;

        bool from_camera = false;

        bool bPictureLoaded = false;

        string image_rotation = "";

        protected override void OnAppearing()
        {
            base.OnAppearing();

            App.net.taking_photo = false;
            UpdateFileList();

            if (App.CurrentApp.camera_vehicle != 1)
            {
                if (App.net.HeaderRecord.iRecordType > 0 && App.CurrentApp.fitter_header_photo_type == 2)
                {
                    del_button.IconImageSource = "";
                    add_button.IconImageSource = "";
                    lib_button.IconImageSource = "";
                    //add_button.Text = "";
                }
            }
        }

        private void OnPictureFinished()
        {

            //            byte[] photo = CameraPreview;
            DisplayAlert("Confirm", "Picture Taken", "", "Ok");
        }

        public void UpdateFileList()
        {
            string fname = "";
            string dname = "Photos";

            if (App.CurrentApp.camera_vehicle == 5) // Fitter Accident
            {
                //dname = "Photos/VC";
                fname = string.Format(string.Format("9{0:0000000}", App.CurrentApp.AccidentRecord.RecID) + "_photo_??.jpg");
            }
            else
            {
                if (App.CurrentApp.camera_vehicle == 4) // Fitter Accident
                {
                    //dname = "Photos/VC";
                    fname = string.Format(string.Format("8{0:0000000}", App.CurrentApp.FAccidentsRecord.RecID) + "_FAcci???.jpg");
                }
                else
                {
                    if (App.CurrentApp.camera_vehicle == 3) // Ladder Checks
                    {
                        //dname = "Photos/VC";
                        fname = string.Format(string.Format("{0:00000000}", App.CurrentApp.LadderRecord.RecID) + "_LadPi???.jpg");
                    }
                    else
                    {
                        if (App.CurrentApp.camera_vehicle == 2)
                        {
                            //dname = "Photos/VC";
                            //fname = string.Format("/Photos/SS/" + App.CurrentApp.VanChecksHeader.unique_id + "_{0:00000000}_" + check_type + "_???.jpg", item_no);
                            fname = string.Format(App.net.HeaderRecord.udi_cont + "_SS___???.jpg");
                        }
                        else
                        {
                            if (App.CurrentApp.camera_vehicle == 1)
                            {
                                string check_type = "";
                                int item_no = 0;

                                switch (App.CurrentApp.CurrentItem)
                                {
                                    case "deliveryvan": item_no = App.CurrentApp.DeliveryVanVehicleCheckList.item_no; check_type = "a"; break;
                                    case "delivery": item_no = App.CurrentApp.DeliveryVehicleCheckList.item_no; check_type = "d"; break;
                                    case "van": item_no = App.CurrentApp.WeeklyVanCheckSheet.item_no; check_type = "v"; break;
                                    case "car": item_no = App.CurrentApp.CarPanelSheet.item_no; check_type = "c"; break;
                                }
                                dname = "Photos/VC";
                                fname = string.Format(App.CurrentApp.VanChecksHeader.unique_id + "_{0:00000000}_" + check_type + "_0??.jpg", item_no);

                            }
                            else
                            {
                                if (App.net.HeaderRecord.iRecordType > 0 && App.net.HeaderRecord.typeB != "Securing" && App.net.CurrentItem == "")
                                {
                                    switch (App.net.fitter_header_photo_type)
                                    {
                                        case 1:
                                            if (App.net.HeaderRecord.typeB == "Remedial")
                                            {
                                                fname = string.Format("{0:00000000}_rAZRem", App.net.HeaderRecord.udi_cont);
                                                fname = fname + "*.jpg";
                                            }
                                            else
                                            {
                                                fname = string.Format("{0:00000000}_fAZFit", App.net.HeaderRecord.udi_cont);
                                                fname = fname + "*.jpg";
                                            }

                                            break;
                                        case 2:
                                            fname = string.Format("{0:00000000}_cAH", App.net.HeaderRecord.udi_cont);
                                            fname = fname + "000??.jpg";
                                            break;
                                        case 9:
                                            fname = string.Format("{0:00000000}_fAZFit", App.net.HeaderRecord.udi_cont);
                                            fname = fname + "*.jpg";
                                            break;

                                        default:
                                            if (App.net.HeaderRecord.typeB == "Remedial")
                                            {
                                                fname = string.Format("{0:00000000}_rAZRem", App.net.HeaderRecord.udi_cont);
                                                fname = fname + "*.jpg";
                                            }
                                            else
                                            {
                                                fname = string.Format("{0:00000000}_fAZFit", App.net.HeaderRecord.udi_cont);
                                                fname = fname + "*.jpg";
                                            }
                                            break;
                                    }
                                }
                                else
                                {
                                    if (App.net.HeaderRecord.b_mrk == true)
                                    {
                                        fname = string.Format("{0:00000000}_kAZSpk*.jpg", App.net.HeaderRecord.udi_cont);
                                        //fname = fname + string.Format("{0:000}*.jpg", App.net.root_item_number);
                                    }
                                    else
                                    {
                                        if ((App.net.HeaderRecord.typeB == "Securing" || App.net.HeaderRecord.type == "Complaint") && App.net.fitter_header_photo_type == 1 && App.net.CurrentItem == "")
                                        {
                                            if (App.net.HeaderRecord.typeB == "Securing")
                                            {
                                                fname = string.Format("{0:00000000}_sAZIns", App.net.HeaderRecord.udi_cont);
                                                fname = fname + "*.jpg";
                                            }
                                            if (App.net.HeaderRecord.type == "Complaint")
                                            {
                                                fname = string.Format("{0:00000000}_kAZCmp", App.net.HeaderRecord.udi_cont);
                                                fname = fname + "*.jpg";
                                            }
                                        }
                                        else
                                        {
                                            fname = string.Format("{0:00000000}_cAZ", App.net.HeaderRecord.udi_cont);
                                            fname = fname + string.Format("{0:000}??.jpg", App.net.root_item_number);

                                            switch (App.net.header_photo_type)
                                            {
                                                case 1:
                                                    fname = string.Format("{0:00000000}_cAH", App.net.HeaderRecord.udi_cont);
                                                    fname = fname + "000??.jpg";
                                                    break;
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (App.net.fitter_header_photo_type == 9)
            {

            }

            fileNames = App.files.GetFileList(dname, fname);

            total_photos = fileNames.Count();
            App.net.total_current_photos = total_photos;

            if (total_photos == 0)
            {
                land_message.IsVisible = true;
                land_message2.IsVisible = true;
                land_message3.IsVisible = true;
            }
            else
            {
                land_message.IsVisible = false;
                land_message2.IsVisible = false;
                land_message3.IsVisible = false;
            }

            if (App.net.camera_vehicle == 0)
            {
                if (App.net.header_photo_type == 1)
                {
                    App.net.HeaderRecord.front_house_photos = total_photos;
                }
                if ((App.net.HeaderRecord.iRecordType > 0 && App.net.CurrentItem == "" && App.net.fitter_header_photo_type != 9) | App.net.HeaderRecord.b_mrk == true)
                {
                    App.net.HeaderRecord.no_of_photos = total_photos;
                }
            }
            if (App.CurrentApp.camera_vehicle == 2 && App.CurrentApp.fitter_header_photo_type != 9)
            {
                App.CurrentApp.HeaderRecord.ss_no_of_photos = total_photos;
            }
            if (App.CurrentApp.camera_vehicle == 3) // Ladder Checks
            {
                App.CurrentApp.LadderRecord.total_photos = total_photos;
            }
            if (App.CurrentApp.camera_vehicle == 4) // Ladder Checks
            {
                App.CurrentApp.FAccidentsRecord.num_of_photographs = total_photos;
            }
            if (App.CurrentApp.camera_vehicle == 5) // Fitter Accident
            {
                if (total_photos > 8)
                {
                    App.CurrentApp.AccidentRecord.c_photographs = true;
                }
            }
            int hival = 0;
            App.net.image_number = 0;
            for (int i = total_photos - 1; i > -1; i--)
            {
                if (fileNames[i].Length > 8)
                {
                    //if (fileNames[i].Substring(0, 8) != App.net.HeaderRecord.udi_cont)
                    //{
                    //    fileNames = (string[])RemoveAt(fileNames, i);
                    //}
                    //else
                    {
                        if (App.CurrentApp.camera_vehicle == 1)
                        {
                            if (fileNames[i].Length > 37)
                            {
                                try
                                {
                                    hival = Convert.ToInt32(fileNames[i].Substring(37, 3));
                                }
                                catch (Exception e)
                                { }
                            }
                        }
                        else
                        {
                            hival = Convert.ToInt32(fileNames[i].Substring(15, 2));
                        }

                        if (hival > App.net.image_number)
                        {
                            App.net.image_number = hival;
                        }
                    }
                }
                //else
                //{
                //    fileNames = (string[])RemoveAt(fileNames, i);
                //}
            }
            App.net.image_number++;

            App.net.total_photos = total_photos;
            if (from_camera == true)
            {
                current_drawing = total_photos - 1;
            }
            else
            {
                current_drawing = 0;//= total_photos - 1;
            }

            DrawPictureNumber();
            //canvasView.InvalidateSurface();
            if (bPictureLoaded == true)
            {
                Device.BeginInvokeOnMainThread(LoadPicture);
            }
            //LoadPicture();
            //Device.BeginInvokeOnMainThread(LoadPicture);
        }

        public Camera(bool read_only = false)
        {
            InitializeComponent();

            App.net.pCamera = this;

            bReadOnly = read_only;

            if (bReadOnly == true)
            {
                del_button.IconImageSource = "";
                add_button.IconImageSource = "";
                lib_button.IconImageSource = "";
            }
            UpdateFileList();

            if (false) //(total_photos == 0)
            {
                App.net.photos_taken = 0;
                App.net.photos_required = 100;
                App.net.photos_enable_increment = true;
                from_camera = true;
                App.net.CreatePhotoFilename();
                Device.BeginInvokeOnMainThread(StartUpCamera2);
            }
            /*
            var tgr = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
            tgr.TappedCallback = (sender, args) => {
                DisplayAlert(sender.Height.ToString(), sender.Width.ToString(), "Yes");
            };
            image.GestureRecognizers.Add(tgr);
            */


            //DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;
        }


        //void OnMainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
        //{
        //   LoadPicture();
        //}

        void LoadPicture()
        {
            string dname = "";

            if (App.CurrentApp.camera_vehicle == 1)
            {
                dname = "Photos/VC/";
            }
            else
            {
                dname = "Photos/";
            }

            if (total_photos > 0)
            {
                if (App.files.FileExists(dname + fileNames[current_drawing]))
                {
                    image_rotation = App.files.GetImageRotation(dname + fileNames[current_drawing]);

                    byte[] data = App.files.LoadBinary(dname + fileNames[current_drawing]);
                    Stream stream = new MemoryStream(data);
                    //bitmap.bitmap = SKBitmap.Decode(stream);
                    SKBitmap bitmap = SKBitmap.Decode(stream);

                    if (image_rotation == "1")
                        this.bitmap = new TouchManipulationBitmap(bitmap, true, ScreenWidth, ScreenHeight);
                    else
                        this.bitmap = new TouchManipulationBitmap(bitmap, false, ScreenWidth, ScreenHeight);
                    this.bitmap.TouchManager.Mode = TouchManipulationMode.IsotropicScale;
                    //touch.TouchAction += OnTouchEffectAction;
                }
                canvasView.InvalidateSurface();
                //pinch_image.Source = ImageSource.FromFile(App.files.CreatePathToFile("Photos//" + fileNames[current_drawing]));
            }
            else
            {
                bitmap = null;
                canvasView.InvalidateSurface();
                //image.Source = null;
            }

            DrawPictureNumber();
        }

        private void OnClick(object sender, EventArgs e)
        {
            if (bReadOnly == false)
            {
                if (App.net.camera_vehicle > 0 || (App.net.camera_vehicle == 0 && (App.net.HeaderRecord.iRecordType == 0 || App.CurrentApp.fitter_header_photo_type == 1)))
                {
                    App.net.photos_taken = 0;
                    App.net.photos_required = 100;
                    App.net.photos_enable_increment = true;
                    from_camera = true;
                    App.net.CreatePhotoFilename();
                    Device.BeginInvokeOnMainThread(StartUpCamera2);
                }
            }
        }

        protected override bool OnBackButtonPressed()
        {
            switch (App.net.CurrentItem)
            {
                case "panel": App.net.PanelRecord.no_of_photos = total_photos; break;
                case "upvc": App.net.UPVCRecord.no_of_photos = total_photos; break;
                case "glass": App.net.GlassRecord.no_of_photos = total_photos; break;
                case "alum": App.net.AlumRecord.no_of_photos = total_photos; break;
                case "garage": App.net.GarageRecord.no_of_photos = total_photos; break;
                case "timber": App.net.TimberRecord.no_of_photos = total_photos; break;
                case "cons": App.net.ConsRecord.no_of_photos = total_photos; break;
                case "lock": App.net.LockingRecord.no_of_photos = total_photos; break;
                case "comp": App.net.CompRecord.no_of_photos = total_photos; break;
                case "green": App.net.GreenRecord.no_of_photos = total_photos; break;
                case "bifold": App.net.BifoldRecord.no_of_photos = total_photos; break;
            }

            base.OnBackButtonPressed();
            return false;
        }

        private void StartUpCamera2()
        {
            Navigation.PushAsync(new CameraView(), false);
        }

        private void DrawPictureNumber()
        {
            if (total_photos == 0)
                picnum.Text = "0";
            else
                picnum.Text = String.Format("{0:#;minus #}", current_drawing + 1) + "/" + String.Format("{0:#;minus #}", total_photos);
        }

        private void OnLeftClick(object sender, EventArgs e)
        {
            current_drawing = current_drawing - 1;
            if (current_drawing < 0)
            {
                current_drawing = total_photos - 1;
            }
            LoadPicture();
        }

        private void OnRightClick(object sender, EventArgs e)
        {
            current_drawing = current_drawing + 1;
            if (current_drawing > total_photos - 1)
            {
                current_drawing = 0;
            }
            LoadPicture();
        }

        private async void OnDel(object sender, EventArgs e)
        {
            if (bReadOnly == false)
            {
                if (App.net.camera_vehicle > 0 || (App.net.camera_vehicle == 0 && (App.net.HeaderRecord.iRecordType == 0 || App.CurrentApp.fitter_header_photo_type == 1)))
                {
                    if (current_drawing > -1 && current_drawing < total_photos)
                    {
                        var answer = await DisplayAlert("Delete this Photograph?", "", "   Yes   ", "   No   ");
                        if (answer)
                        {
                            if (App.files.FileExists("Photos/" + fileNames[current_drawing]) == true)
                            {
                                App.files.DeleteFile("Photos/" + fileNames[current_drawing]);
                                UpdateFileList();
                                if (current_drawing > total_photos - 1)
                                {
                                    current_drawing = total_photos - 1;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void OnPainting(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {

        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            //if(image_rotation=="6")
            //    canvas.RotateDegrees(90, info.Width / 2, info.Height / 2);

            ScreenWidth = (float)info.Width;
            ScreenHeight = (float)info.Height;

            if (bPictureLoaded == false)
            {
                bPictureLoaded = true;
                Device.BeginInvokeOnMainThread(LoadPicture);
            }

            if (bitmap != null)
            {
                /*
                float scale = Math.Min((float)info.Width / bitmap.bitmap.Width,
                                       (float)info.Height / bitmap.bitmap.Height);

                if (image_rotation == "6")
                {
                    scale = scale * 1.6f;
                }
                float x = (info.Width - scale * bitmap.bitmap.Width) / 2;
                float y = (info.Height - scale * bitmap.bitmap.Height) / 2;
                SKRect destRect = new SKRect(x, y, x + scale * bitmap.bitmap.Width,
                                                   y + scale * bitmap.bitmap.Height);
                */
                bitmap.Paint(canvas);
                //canvas.DrawBitmap(bitmap, destRect);
            }
        }

        void OnTouchEffectAction(object sender, TouchActionEventArgs args)
        {
            // Convert Xamarin.Forms point to pixels
            Point pt = args.Location;
            SKPoint point =
                new SKPoint((float)(canvasView.CanvasSize.Width * pt.X / canvasView.Width),
                            (float)(canvasView.CanvasSize.Height * pt.Y / canvasView.Height));
            if (bitmap == null)
                return;
            switch (args.Type)
            {
                case TouchActionType.Pressed:
                    if (bitmap.HitTest(point))
                    {
                        touchIds.Add(args.Id);
                        bitmap.ProcessTouchEvent(args.Id, args.Type, point);
                        break;
                    }
                    break;

                case TouchActionType.Moved:
                    if (touchIds.Contains(args.Id))
                    {
                        bitmap.ProcessTouchEvent(args.Id, args.Type, point);
                        canvasView.InvalidateSurface();
                    }
                    break;
                case TouchActionType.Released:
                case TouchActionType.Cancelled:
                    if (touchIds.Contains(args.Id))
                    {
                        bitmap.ProcessTouchEvent(args.Id, args.Type, point);
                        touchIds.Remove(args.Id);
                        canvasView.InvalidateSurface();
                    }
                    break;
            }
        }

        private void OnNumClick(object sender, EventArgs e)
        {
            LoadPicture();
        }

        private async void OnLib(object sender, EventArgs e)
        {
            if (bReadOnly == false)
            {
                App.net.photos_taken = 0;
                App.net.photos_required = 100;
                App.net.photos_enable_increment = true;
                from_camera = true;
                App.net.CreatePhotoFilename();

                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    //await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                    return;
                }
                var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

                });


                if (file == null)
                    return;

                var stream = file.GetStream();

                //byte[] dd = data.ToArray();
                App.files.SaveStream("Photos/" + App.net.photo_fname, file.GetStream());

                UpdateFileList();
                /*
                    image.Source = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();
                        file.Dispose();
                        return stream;
                    });
                */
            }
        }

        private void OnOK(object sender, EventArgs e)
        {
            if (App.net.camera_vehicle > 0 || (App.net.camera_vehicle == 0 && (App.net.HeaderRecord.iRecordType == 0 || App.CurrentApp.fitter_header_photo_type == 1)))
            {
                App.net.photos_taken = 0;
                App.net.photos_required = 100;
                App.net.photos_enable_increment = true;
                from_camera = true;
                App.net.CreatePhotoFilename();
                Device.BeginInvokeOnMainThread(StartUpCamera2);
            }
        }
    }
}
