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
	public partial class FitterWarning : ContentPage
	{
		public FitterWarning ()
		{
			InitializeComponent ();
		}

        private void OnClick(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new FitterPhotosWarning(), this);
            Navigation.PopAsync(false);
        }
    }
}