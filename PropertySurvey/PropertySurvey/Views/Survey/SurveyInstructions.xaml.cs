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
	public partial class SurveyInstructions : ContentPage
	{
		public SurveyInstructions ()
		{
			InitializeComponent ();

            if (App.net.HeaderRecord.udi_jobtext.Length == 0)
                App.net.HeaderRecord.udi_jobtext = "?";
            if (App.net.HeaderRecord.policy_number.Length == 0)
                App.net.HeaderRecord.policy_number = "?";
            if (App.net.HeaderRecord.udi_inst.Length == 0)
                App.net.HeaderRecord.udi_inst = "?";

            BindingContext = App.net.HeaderRecord as Header;
        }
	}
}