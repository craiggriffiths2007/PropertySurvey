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
	public partial class FitterInstructions : ContentPage
	{
		public FitterInstructions () 
		{
			InitializeComponent ();

            BindingContext = App.net.HeaderRecord as Header;

            int index_pos = App.CurrentApp.HeaderRecord.fitters_instructions.IndexOf("DELIVERY NOTES");

            if (index_pos > 0)
            {
                fitins.Text = App.CurrentApp.HeaderRecord.fitters_instructions.Substring(0, index_pos);
                del_ins.IsVisible = true;
                del_ins_label.IsVisible = true;
                del_ins.Text = App.CurrentApp.HeaderRecord.fitters_instructions.Substring(index_pos + 15);
                del_ins.Text = del_ins.Text.Replace("[NL]", "\n\n");
            }
            else
            {
                fitins.Text = App.CurrentApp.HeaderRecord.fitters_instructions;
                del_ins.IsVisible = false;
                del_ins_label.IsVisible = false;
            }
        }

        private void OnNext(object sender, EventArgs e)
        {
            switch (App.CurrentApp.HeaderRecord.typeB)
            {
                case "Remedial": Navigation.InsertPageBefore(new RemedialHeader(), this); break;

                default: Navigation.InsertPageBefore(new FitterHeader(), this); break;
            }
            
            Navigation.PopAsync(false);
        }
    }
}