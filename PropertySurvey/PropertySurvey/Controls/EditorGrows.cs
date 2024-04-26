using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MartControls
{
    public class EditorGrows : Editor
    {
        public EditorGrows()
        {
            this.TextChanged += (sender, e) =>
            {
                this.InvalidateMeasure();

                Focused += CrashStopperHack_Focused;
            };

            void CrashStopperHack_Focused(object sender, FocusEventArgs e)
            {
                //this.Unfocus();
            }
            
        }
    }

    public class ImageGrows : Image
    {
        public ImageGrows()
        {
            //this.PropertyChanged += (sender, e) =>
            //{
            //    this.InvalidateMeasure();
            //};

        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(Source))
                Device.BeginInvokeOnMainThread(async () =>
                {
                    this.VerticalOptions = LayoutOptions.Fill;
                    this.HorizontalOptions = LayoutOptions.Fill;
                    this.InvalidateMeasure();
                    //await this.ScaleTo(1.2);
                    //await this.ScaleTo(1);
                });
        }
    }
}