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
	public partial class TemplateType : ContentPage
	{
		public TemplateType ()
		{
			InitializeComponent ();
		}

        private async void OnWindows(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new TemplateWindows(), this);
            await Navigation.PopAsync(false);
            //Navigation.PushAsync(new TemplateWindows(), false);
        }

        private async void OnDoors(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new TemplateDoors(), this);
            await Navigation.PopAsync(false);
            //Navigation.PushAsync(new TemplateDoors(), false);
        }

        private async void OnCons(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new TemplateCons(), this);
            await Navigation.PopAsync(false);
            //Navigation.PushAsync(new TemplateCons(), false);
        }

        private async void OnSection(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new TemplateBeading(), this);
            await Navigation.PopAsync(false);
            //Navigation.PushAsync(new TemplateBeading(), false);
        }

        private async void OnHandles(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new TemplateHandles(), this);
            await Navigation.PopAsync(false);
            //Navigation.PushAsync(new TemplateHandles(), false);
        }

        private async void OnGreen(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new TemplateGreen(), this);
            await Navigation.PopAsync(false);
            //Navigation.PushAsync(new TemplateGreen(), false);
        }
    }
}