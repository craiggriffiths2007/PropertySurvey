using System;
using System.Collections.Generic;

using SkiaSharp;

using TouchTracking;
using Xamarin.Essentials;

namespace MartControls
{
    class TouchManipulationBitmap
    {
        public SKBitmap bitmap;
        //public SKMatrix scale = SKMatrix.MakeScale(10.0f, 10.0f);
        Dictionary<long, TouchManipulationInfo> touchDictionary =
            new Dictionary<long, TouchManipulationInfo>();

        public TouchManipulationBitmap(SKBitmap bitmap, bool rotate, float ScreenWidth, float ScreenHeight)
        {
            this.bitmap = bitmap;

            // Get Metrics
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            // Orientation (Landscape, Portrait, Square, Unknown)
            var orientation = mainDisplayInfo.Orientation;

            
            if (rotate==true)
            {
                Matrix = SKMatrix.CreateIdentity();
                SKMatrix scale = SKMatrix.CreateScale(0.3f,0.3f);
                SKMatrix matrix = Matrix;

                SKMatrix.PostConcat(ref matrix, scale);
                //SKMatrix.PostConcat(ref matrix, scale);
                //Matrix = matrix;

                SKMatrix.Rotate(ref matrix, 1.5708f, bitmap.Width / 2, bitmap.Height / 2);
                //matrix.TransX = -2.0f;
                //SKMatrix.
                //float scale = matrix.ScaleX;
                //float mat = matrix.TransX;
                float scale_val = 0.0f;
                if (orientation == DisplayOrientation.Portrait)
                {
                    scale_val = ScreenWidth / bitmap.Height;
                }
                else
                {
                    //scale_val = 0.5f;
                    scale_val = ScreenHeight / bitmap.Width;
                }

                SKMatrix.PostConcat(ref matrix,
                SKMatrix.MakeScale(scale_val, scale_val, 0, 0));

                if (orientation == DisplayOrientation.Portrait)
                {
                    matrix.TransX -= ((scale_val * (bitmap.Width - bitmap.Height)) + ((ScreenWidth - (bitmap.Width * scale_val)) / 2.0f));//(((bitmap.Width/ bitmap.Height)* ScreenWidth)/((bitmap.Width / bitmap.Height)*2.0f));

                    matrix.TransY += scale_val * ((bitmap.Width - bitmap.Height) / 2.0f);
                }
                else
                {
                    matrix.TransY += 2.0f * ((45.0f / 360.0f) * ScreenHeight);
                }
                //SKMatrix.(ref matrix, 1.5708f, bitmap.Width / 2, bitmap.Height / 2);
                //SKMatrix.PreConcat(ref matrix, scale);



                Matrix = matrix;
            }
            else
            {
                //Matrix = SKMatrix.MakeScale(0.003f, 0.003f);
                Matrix = SKMatrix.MakeIdentity();
                SKMatrix matrix = Matrix;

                float scale_val = ScreenWidth / bitmap.Width;
                SKMatrix.PostConcat(ref matrix,
                    SKMatrix.MakeScale(scale_val, scale_val, 0, 0));

                if (orientation == DisplayOrientation.Portrait)
                {
                    matrix.TransY += (ScreenHeight / 2.0f) - (scale_val * (bitmap.Height) / 2.0f);
                }

                Matrix = matrix;
            }

            TouchManager = new TouchManipulationManager
            {
                Mode = TouchManipulationMode.ScaleRotate
            };
        }

        public TouchManipulationManager TouchManager { set; get; }

        public SKMatrix Matrix { set; get; }

        public void Paint(SKCanvas canvas)
        {
            canvas.Save();
            SKMatrix matrix = Matrix;
            canvas.Concat(ref matrix);
            canvas.DrawBitmap(bitmap, 0, 0);
            canvas.Restore();
        }

        public bool HitTest(SKPoint location)
        {
            // Invert the matrix
            SKMatrix inverseMatrix;

            if (Matrix.TryInvert(out inverseMatrix))
            {
                // Transform the point using the inverted matrix
                SKPoint transformedPoint = inverseMatrix.MapPoint(location);

                // Check if it's in the untransformed bitmap rectangle
                SKRect rect = new SKRect(0, 0, bitmap.Width, bitmap.Height);
                return rect.Contains(transformedPoint);
            }
            return false;
        }

        public void ProcessTouchEvent(long id, TouchActionType type, SKPoint location)
        {
            switch (type)
            {
                case TouchActionType.Pressed:
                    touchDictionary.Add(id, new TouchManipulationInfo
                    {
                        PreviousPoint = location,
                        NewPoint = location
                    });
                    break;

                case TouchActionType.Moved:
                    TouchManipulationInfo info = touchDictionary[id];
                    info.NewPoint = location;
                    Manipulate();
                    info.PreviousPoint = info.NewPoint;
                    break;

                case TouchActionType.Released:
                    touchDictionary[id].NewPoint = location;
                    Manipulate();
                    touchDictionary.Remove(id);
                    break;

                case TouchActionType.Cancelled:
                    touchDictionary.Remove(id);
                    break;
            }
        }

        void Manipulate()
        {
            TouchManipulationInfo[] infos = new TouchManipulationInfo[touchDictionary.Count];
            touchDictionary.Values.CopyTo(infos, 0);
            SKMatrix touchMatrix = SKMatrix.MakeIdentity();

            if (infos.Length == 1)
            {
                SKPoint prevPoint = infos[0].PreviousPoint;
                SKPoint newPoint = infos[0].NewPoint;
                SKPoint pivotPoint = Matrix.MapPoint(bitmap.Width / 2, bitmap.Height / 2);

                touchMatrix = TouchManager.OneFingerManipulate(prevPoint, newPoint, pivotPoint);
            }
            else if (infos.Length >= 2)
            {
                int pivotIndex = infos[0].NewPoint == infos[0].PreviousPoint ? 0 : 1;
                SKPoint pivotPoint = infos[pivotIndex].NewPoint;
                SKPoint newPoint = infos[1 - pivotIndex].NewPoint;
                SKPoint prevPoint = infos[1 - pivotIndex].PreviousPoint;

                touchMatrix = TouchManager.TwoFingerManipulate(prevPoint, newPoint, pivotPoint);
            }

            SKMatrix matrix = Matrix;

            SKMatrix.PostConcat(ref matrix, touchMatrix);
            //SKMatrix.PostConcat(ref matrix, scale);
            Matrix = matrix;
        }
    }
}
