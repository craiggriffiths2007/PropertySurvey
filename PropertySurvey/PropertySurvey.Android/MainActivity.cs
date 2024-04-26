using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using Android.Content.Res;
using Android.Support.V4.Content;
using Android.Support.V13.App;
using Android.Content;
using Android.Arch.Lifecycle;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Speech.Tts;
using Android.Util;
using Plugin.CurrentActivity;
using System.Threading.Tasks;

namespace PropertySurvey.Droid
{

    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, Label = "Splash", Icon = "@drawable/icon", Theme = "@style/splashscreen", MainLauncher = false)]
    public class Splash : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

        }
    }

    [Activity(Label = "PropertySurvey", Icon = "@drawable/icon", Theme = "@style/splashscreen", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }
        public TaskCompletionSource<string> PickImageTaskCompletionSource { set; get; }

        public static readonly int PickImageId = 1000;


        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == PickImageId)
            {
                if ((resultCode == Result.Ok) && (data != null))
                {
                    // Set the filename as the completion of the Task
                    PickImageTaskCompletionSource.SetResult(data.DataString);
                }
                else
                {
                    PickImageTaskCompletionSource.SetResult(null);
                }
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            Instance = this;

            base.Window.RequestFeature(WindowFeatures.ActionBar);
            // Name of the MainActivity theme you had there before.
            // Or you can use global::Android.Resource.Style.ThemeHoloLight
            base.SetTheme(Resource.Style.MainTheme);

            /*
            TypedArray styledAttributes = this.Theme.ObtainStyledAttributes(
            new int[] { Android.Resource.Attribute.ActionBarSize });
            var actionbarHeight = (int)styledAttributes.GetDimension(0, 0);
            styledAttributes.Recycle();
            */
            //RequestedOrientation = ScreenOrientation.Landscape;

            MessagingCenter.Subscribe<DrawingPage>(this, "allowLandScapePortraitDrawing", sender =>
            {
                RequestedOrientation = ScreenOrientation.Unspecified;
            });
            MessagingCenter.Subscribe<DrawingPage>(this, "preventLandscapeDrawing", sender =>
            {
                RequestedOrientation = ScreenOrientation.Portrait;
            });

            MessagingCenter.Subscribe<VanPictures>(this, "allowLandScapePortraitPic", sender =>
            {
                RequestedOrientation = ScreenOrientation.Unspecified;
            });
            MessagingCenter.Subscribe<VanPictures>(this, "preventPortraitPic", sender =>
            {
                RequestedOrientation = ScreenOrientation.Landscape;
            });
            MessagingCenter.Subscribe<VanDiagrams>(this, "allowLandScapePortrait", sender =>
            {
                RequestedOrientation = ScreenOrientation.Unspecified;
            });
            MessagingCenter.Subscribe<VanDiagrams>(this, "preventPortrait", sender =>
            {
                RequestedOrientation = ScreenOrientation.Landscape;
            });

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            string cameraPermission = Android.Manifest.Permission.Camera;
            if (!(ContextCompat.CheckSelfPermission(this, cameraPermission) == (int)Permission.Granted))
            {
                ActivityCompat.RequestPermissions(this, new String[] { cameraPermission, }, 0);
            }

            string phoneStatePermission = Android.Manifest.Permission.ReadPhoneState;

            if (!(ContextCompat.CheckSelfPermission(this, phoneStatePermission) == (int)Permission.Granted))
            {
                ActivityCompat.RequestPermissions(this, new String[] { phoneStatePermission, }, 0);
            }

            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            if (true)//(App.net.bCopyDatabaseFileToDocuments == true)
            {
                if ((int)Build.VERSION.SdkInt < 23)
                {
                    //return;
                }
                else
                {
                    if (PackageManager.CheckPermission(Android.Manifest.Permission.ReadExternalStorage, PackageName) != Permission.Granted
                        && PackageManager.CheckPermission(Android.Manifest.Permission.WriteExternalStorage, PackageName) != Permission.Granted)
                    {
                        var permissions = new string[] { Android.Manifest.Permission.ReadExternalStorage, Android.Manifest.Permission.WriteExternalStorage };
                        RequestPermissions(permissions, 1);
                    }
                    /*
                    if (PackageManager.CheckPermission(Android.Manifest.Permission.RecordAudio, PackageName) != Permission.Granted
                        && PackageManager.CheckPermission(Android.Manifest.Permission.RecordAudio, PackageName) != Permission.Granted)
                    {
                        var permissions = new string[] { Android.Manifest.Permission.RecordAudio, Android.Manifest.Permission.WriteExternalStorage };
                        RequestPermissions(permissions, 1);
                    }
                    */
                }
            }


            App App = new App();
            App.net.phone_serial = Android.OS.Build.Serial;
            App.ScreenHeight = (int)(Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density);
            App.ScreenWidth = (int)(Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density);

            LoadApplication(App);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);

            //Console.WriteLine("config changed");

            if (newConfig.Orientation == Android.Content.Res.Orientation.Portrait)
            {
                //_tv.LayoutParameters = _layoutParamsPortrait;
                //_tv.Text = "Changed to portrait. Timestamp = " + _timestamp;
            }
            else if (newConfig.Orientation == Android.Content.Res.Orientation.Landscape)
            {
                //_tv.LayoutParameters = _layoutParamsLandscape;
                //_tv.Text = "Changed to landscape. Timestamp = " + _timestamp;
            }
        }

    }
}