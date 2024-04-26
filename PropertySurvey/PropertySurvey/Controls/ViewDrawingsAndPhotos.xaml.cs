using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MartControls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewDrawingsAndPhotos : StackLayout
    {
		public ViewDrawingsAndPhotos ()
		{
			InitializeComponent ();
		}

        public int num_drawings
        {
            set
            {
                drawings_label.Text = "Drawings : " + value.ToString();
                view_draings_button.IsVisible = value > 0;
            }
        }

        public int num_photos
        {
            set
            {
                photo_label.Text = "Photos : " + value.ToString();
                view_photos_button.IsVisible = value > 0;
            }
        }

        private void view_drawings_clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PropertySurvey.ViewDrawing(PropertySurvey.drawing_file_types.dft_generic_drawing), false);
        }

        private void view_photos_clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PropertySurvey.Camera(true), false);
        }
    }
}