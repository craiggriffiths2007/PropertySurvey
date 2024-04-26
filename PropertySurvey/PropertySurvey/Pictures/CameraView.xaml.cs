using PropertySurvey.CustomViews;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net;
using System.IO;
using System.Drawing;
//using Image = System.Drawing.Image;
using SkiaSharp;

namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CameraView : ContentPage
    {
        public CameraView()
        {
            InitializeComponent();
            CameraPreview.PictureFinished += OnPictureFinished;

            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += OnCameraClicked;
            CameraPreview.GestureRecognizers.Add(tap);
        }

        void OnCameraClicked(object sender, EventArgs e)
        {
            CameraPreview.CameraClick.Execute(null);
        }

        public static SKBitmap Rotate(SKBitmap bmp)
        {
            using (var bitmap = bmp)
            {
                var rotated = new SKBitmap(bitmap.Height, bitmap.Width);

                using (var surface = new SKCanvas(rotated))
                {
                    surface.Translate(rotated.Width, 0);
                    surface.RotateDegrees(90);
                    surface.DrawBitmap(bitmap, 0, 0);
                }

                return rotated;
            }
        }

        private void OnPictureFinished()
        {
            if (false) // Too slow
            {
                SKBitmap bmp = SKBitmap.Decode(new MemoryStream(App.cameraImage));

                bmp=Rotate(bmp);

                using (bmp)
                    if (bmp != null)
                    {
                        using (var data = bmp.Encode(SKEncodedImageFormat.Png, 80))
                        {
                            //byte[] dd = data.ToArray();
                            App.files.SaveBinary("Photos/" + App.net.photo_fname, data.ToArray());
                        }
                    }
            }
            else
            {
                App.files.SaveBinary("Photos/" + App.net.photo_fname, App.cameraImage);
            }

            App.net.photos_taken++;
            App.net.image_number++;
            Title = App.net.photos_taken.ToString();

            App.net.CreatePhotoFilename();
        }
    }
}