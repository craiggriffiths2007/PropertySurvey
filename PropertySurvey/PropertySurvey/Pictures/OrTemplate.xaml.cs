using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OrTemplate : ContentPage
	{
		public OrTemplate ()
		{
			InitializeComponent ();
            App.net.drawing_edit_mode = false;
            App.net.load_template_image = false;
        }
        private async void OnNew(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new DrawingPage(), this);
            await Navigation.PopAsync(false);
        }
        private async void OnTemp(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new TemplateType(), this);
            await Navigation.PopAsync(false);
        }
    }
}
