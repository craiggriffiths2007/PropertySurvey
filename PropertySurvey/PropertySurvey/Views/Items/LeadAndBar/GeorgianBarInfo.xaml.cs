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
    public static class georgian_bar_logic
    {
        public static List<string> bar_types = new List<string>() { "", "18mm brown", "18mm white", "25mm brown", "25mm white", "45mm brown", "45mm white", "8mm gold" };
        public static List<string> anti_rattle = new List<string>() { "", "None", "Square peg", "Round peg" };
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GeorgianBarInfo : ContentPage
	{
		public GeorgianBarInfo ()
        {
            InitializeComponent();

            gb_anti_rattle.set_button_list(georgian_bar_logic.anti_rattle);
            georgian_bar.SetPickerItems(georgian_bar_logic.bar_types);

            if (App.net.CurrentItem == "timber")
                BindingContext = App.net.TimberRecord as TimberTable;
            else if (App.net.CurrentItem == "alum")
                BindingContext = App.net.AlumRecord as AlumTable;
            else if (App.net.CurrentItem == "upvc")
                BindingContext = App.net.UPVCRecord as UPVCTable;
            else if (App.net.CurrentItem == "comp")
                BindingContext = App.net.CompRecord as CompositeTable;
            else if (App.net.CurrentItem == "glass")
                BindingContext = App.net.GlassRecord as GlassTable;
        }

        private async void OnNext(object sender, EventArgs e)
        {
            App.data.SaveItem();

            if (georgian_bar.Text == "" ||
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
                        App.net.TimberRecord.lead_bBarComplete = true;
                    }
                    if (App.net.CurrentItem == "alum")
                    {
                        App.net.AlumRecord.lead_CWidthf = (float)width;
                        App.net.AlumRecord.lead_CHeightf = (float)height;
                        App.net.AlumRecord.lead_bBarComplete = true;
                    }
                    if (App.net.CurrentItem == "upvc")
                    {
                        App.net.UPVCRecord.lead_CWidthf = (float)width;
                        App.net.UPVCRecord.lead_CHeightf = (float)height;
                        App.net.UPVCRecord.lead_bBarComplete = true;
                    }
                    if (App.net.CurrentItem == "comp")
                    {
                        App.net.CompRecord.lead_CWidthf = (float)width;
                        App.net.CompRecord.lead_CHeightf = (float)height;
                        App.net.CompRecord.lead_bBarComplete = true;
                    }

                    await Navigation.PopAsync(false);
                }
            }
        }
    }
}