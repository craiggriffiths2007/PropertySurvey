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
	public partial class GeorgianLeadInfo : ContentPage
	{
		public GeorgianLeadInfo ()
		{
            List<string> georgian_lt = new List<string>() { "", "6mm oval", "6mm flat", "9mm oval", "9mm flat", "12mm oval", "12mm flat" };
            List<string> georgian_sd = new List<string>() { "", "double", "single external", "single internal unit" };
            List<string> georgian_tl = new List<string>() { "", "Shiny Lead", "Aged Lead", "Gold Lead" };

            InitializeComponent ();

            lead_thickness.SetPickerItems(georgian_lt);
            single_or_double.SetPickerItems(georgian_sd);
            type_of_lead.SetPickerItems(georgian_tl);

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
            }
        }

        private async void OnNext(object sender, EventArgs e)
        {
            App.data.SaveItem();

            if (lead_thickness.Text == "" ||
                single_or_double.Text == "" ||
                type_of_lead.Text == "" ||
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