using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BackToBackInfo : ContentPage
	{
        public BackToBackInfo()
        {
            List<string> ov_width = new List<string>() { "", "12mm", "14mm", "16mm", "18mm", "20mm" };

            InitializeComponent();

            spacer_thickness.SetPickerItems(App.net.spacer_thickness2);
            spacer_width.SetPickerItems(ov_width);

            if (App.net.CurrentItem == "timber")
            {
                BindingContext = App.net.TimberRecord as TimberTable;
            }
            if (App.net.CurrentItem == "alum")
            {
                BindingContext = App.net.AlumRecord as AlumTable;
            }
            if (App.net.CurrentItem == "upvc")
            {
                BindingContext = App.net.UPVCRecord as UPVCTable;
            }
            if (App.net.CurrentItem == "comp")
            {
                BindingContext = App.net.CompRecord as CompositeTable;
            }
            if (App.net.CurrentItem == "glass")
            {
                BindingContext = App.net.GlassRecord as GlassTable;
                trim_zone.IsVisible = true;
            }
        }

        private async void OnNext(object sender, EventArgs e)
        {
            App.data.SaveItem();

            if (spacer_thickness.Text == "" ||
               spacer_width.Text == "" ||
               width_spacing.TextBinding.Length == 0 ||
               height_spacing.TextBinding.Length == 0 ||
               comments.text == "")
            {

            }
            else
            {
                double width;
                double height;
                Double.TryParse(width_spacing.TextBinding, out width);
                Double.TryParse(height_spacing.TextBinding, out height);

                if (App.net.CurrentItem == "glass")
                {
                    App.net.GlassRecord.lead_CHeightf = (float)height;
                    App.net.GlassRecord.lead_CWidthf = (float)width;

                    if (App.net.GlassRecord.lead_CHeightf == 0.0f || App.net.GlassRecord.lead_CHeightf == 0.0f)
                    {

                    }
                    else
                    {
                        Navigation.InsertPageBefore(new LeadBarPosition(), this);
                        await Navigation.PopAsync(false);
                    }
                }
                else
                {

                    if (App.net.CurrentItem == "timber")
                    {
                        App.net.TimberRecord.lead_CWidthf = (float)width;
                        App.net.TimberRecord.lead_CHeightf = (float)height;
                        App.net.TimberRecord.lead_bGeorgianComplete = true;
                    }
                    if (App.net.CurrentItem == "alum")
                    {
                        App.net.AlumRecord.lead_CWidthf = (float)width;
                        App.net.AlumRecord.lead_CHeightf = (float)height;
                        App.net.AlumRecord.lead_bGeorgianComplete = true;
                    }
                    if (App.net.CurrentItem == "upvc")
                    {
                        App.net.UPVCRecord.lead_CWidthf = (float)width;
                        App.net.UPVCRecord.lead_CHeightf = (float)height;
                        App.net.UPVCRecord.lead_bGeorgianComplete = true;
                    }
                    if (App.net.CurrentItem == "comp")
                    {
                        App.net.CompRecord.lead_CWidthf = (float)width;
                        App.net.CompRecord.lead_CHeightf = (float)height;
                        App.net.CompRecord.lead_bGeorgianComplete = true;
                    }
                    await Navigation.PopAsync(false);
                }
            }
        }
    }
}