﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoPlayback : ContentPage
    {
        public VideoPlayback(string filename)
        {
            InitializeComponent();

            videoPlayer.Source = FormsVideoLibrary.VideoSource.FromFile(filename);
            videoPlayer.Play();
        }
    }
}