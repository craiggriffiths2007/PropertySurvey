using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PropertySurvey;

namespace MartControls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemSummary_PTO : StackLayout
	{
		public ItemSummary_PTO ()
		{
			InitializeComponent ();
        }

        public string summary_binding { set { item_summary.SetBinding(EditorGrows.TextProperty, value); } }
        public string PTO_binding { set { parts_to_order.SetBinding(EditorGrows.TextProperty, value); } }

        public string validation_error_string()
        {
            string result = "";

            if (this.IsVisible)
            {
                if (item_summary.Text == null || item_summary.Text.Length == 0)
                    result = "Summary\n";

                if (parts_to_order.Text == null || parts_to_order.Text.Length == 0)
                    result = result + "Parts to order\n";
            }

            return result;
        }

        private async void OnButton(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var response = await Application.Current.MainPage.DisplayAlert("",
                    "Copy Item Summary to Job Summary?", "   Yes   ", "   No   ");
                if (response)
                {
                    App.net.HeaderRecord.summ_text += ". " + (++App.net.HeaderRecord.current_summary_num).ToString() + ". " + item_summary.Text + ". ";
                    App.data.SaveHeader();
                }
            });
        }
    }
}