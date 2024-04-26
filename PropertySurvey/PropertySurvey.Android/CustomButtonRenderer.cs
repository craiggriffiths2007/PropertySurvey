using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MartControls;
using SurvAppX.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;
using Orientation = Android.Widget.Orientation;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Button), typeof(CustomButtonCompatRenderer))]

namespace SurvAppX.Droid
{
    public class CustomButtonCompatRenderer : Xamarin.Forms.Platform.Android.AppCompat.ButtonRenderer
    {
        public CustomButtonCompatRenderer(Context context) : base(context)
        {
            AutoPackage = false;

            Dispose(false);
        }


        public override void ChildDrawableStateChanged(Android.Views.View child)
        {
            base.ChildDrawableStateChanged(child);
            Control.Text = Element.Text;
        }

        private class ButtonClickListener : Java.Lang.Object, IOnClickListener, IJavaObject, IDisposable
        {
            public static readonly Lazy<CustomButtonCompatRenderer.ButtonClickListener> Instance =
                new Lazy<CustomButtonCompatRenderer.ButtonClickListener>(() => new CustomButtonCompatRenderer.ButtonClickListener());

            public void OnClick(Xamarin.Forms.View v)
            {
                MyButtonRenderer buttonRenderer = v.Tag as MyButtonRenderer;

                if (buttonRenderer != null)
                {
                    // have to cast to MyButton to access the new SendClicked method
                    // which replaces that of the base class Button
                    MyButton myButton = (MyButton)buttonRenderer.Element;

                    myButton.SendClicked();
                    buttonRenderer.ForceUpdateText();
                }
            }

            public void OnClick(Android.Views.View v)
            {
                throw new NotImplementedException();
            }
        }
    }
}