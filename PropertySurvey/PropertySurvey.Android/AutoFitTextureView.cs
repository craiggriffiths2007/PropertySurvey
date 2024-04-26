using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;

namespace PropertySurvey.Droid
{
    public class AutoFitTextureView : TextureView
    {
        private int mRatioWidth = 0;
        private int mRatioHeight = 0;

        public DisplayMetrics mMetrics = new DisplayMetrics();

        public AutoFitTextureView(Context context)
            : this(context, null)
        {

        }
        public AutoFitTextureView(Context context, IAttributeSet attrs)
            : this(context, attrs, 0)
        {

        }
        public AutoFitTextureView(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {

        }

        public void SetAspectRatio(int width, int height)
        {
            if (width == 0 || height == 0)
                throw new ArgumentException("Size cannot be negative.");
            mRatioWidth = width;
            mRatioHeight = height;
            RequestLayout();
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
            return;
            int width = MeasureSpec.GetSize(widthMeasureSpec);
            int height = MeasureSpec.GetSize(heightMeasureSpec);
            if (0 == mRatioWidth || 0 == mRatioHeight)
            {
                SetMeasuredDimension(width, height);
            }
            else
            {
                if (width < (float)height * mRatioWidth / (float)mRatioHeight)
                {
                    SetMeasuredDimension(width, width * mRatioHeight / mRatioWidth);
                }
                else
                {
                    SetMeasuredDimension(height * mRatioWidth / mRatioHeight, height);
                }
            }
        }

        /*
        private void setAspectRatioTextureView(int ResolutionWidth, int ResolutionHeight)
        {
            if (ResolutionWidth > ResolutionHeight)
            {
                int newWidth = DSI_width;
                int newHeight = ((DSI_width * ResolutionWidth) / ResolutionHeight);
                updateTextureViewSize(newWidth, newHeight);

            }
            else
            {
                int newWidth = DSI_width;
                int newHeight = ((DSI_width * ResolutionHeight) / ResolutionWidth);
                updateTextureViewSize(newWidth, newHeight);
            }

        }
        */
        private void updateTextureViewSize(int viewWidth, int viewHeight)
        {
            //Log.d(TAG, "TextureView Width : " + viewWidth + " TextureView Height : " + viewHeight);
            //SetLayoutParams(new FrameLayout.LayoutParams(viewWidth, viewHeight));
        }
        //protected void onMeasure(int widthMeasureSpec, int heightMeasureSpec)
        //{
        //    super.onMeasure(widthMeasureSpec, heightMeasureSpec);
        //    int width = MeasureSpec.getSize(widthMeasureSpec);
        //    int height = MeasureSpec.getSize(heightMeasureSpec);
        //    if (0 == mRatioWidth || 0 == mRatioHeight)
        //    {
        //        setMeasuredDimension(width, height);
        //    }
        //    else
        //    {
        //        WindowManager windowManager = (WindowManager)getContext().getSystemService(Context.WINDOW_SERVICE);
        //        windowManager.getDefaultDisplay().getMetrics(mMetrics);
        //        double ratio = (double)mRatioWidth / (double)mRatioHeight;
        //        double invertedRatio = (double)mRatioHeight / (double)mRatioWidth;
        //        double portraitHeight = width * invertedRatio;
        //        double portraitWidth = width * (mMetrics.heightPixels / portraitHeight);
        //        double landscapeWidth = height * ratio;
        //        double landscapeHeight = (mMetrics.widthPixels / landscapeWidth) * height;

        //        if (width < height * mRatioWidth / mRatioHeight)
        //        {
        //            setMeasuredDimension((int)portraitWidth, mMetrics.heightPixels);
        //        }
        //        else
        //        {
        //            setMeasuredDimension(mMetrics.widthPixels, (int)landscapeHeight);
        //        }
        //    }
        //}

    }
}