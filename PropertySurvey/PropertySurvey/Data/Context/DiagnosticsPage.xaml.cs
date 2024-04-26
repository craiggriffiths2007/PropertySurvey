using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DiagnosticsPage : ContentPage
	{
		public DiagnosticsPage ()
		{
			InitializeComponent ();
		}

        private void header_clicked(object sender, EventArgs e)
        {
            DisplayAlert("Header", App.data.table_debug_str("Header"), "ok");
        }

        private void alum_clicked(object sender, EventArgs e)
        {
            DisplayAlert("Aluminium", App.data.table_debug_str("AlumTable"), "ok");
        }

        private void bifold_clicked(object sender, EventArgs e)
        {
            DisplayAlert("Bifold", App.data.table_debug_str("BifoldTable"), "ok");
        }

        private void comp_clicked(object sender, EventArgs e)
        {
            DisplayAlert("Composite", App.data.table_debug_str("CompositeTable"), "ok");
        }

        private void cons_clicked(object sender, EventArgs e)
        {
            DisplayAlert("Conservatory", App.data.table_debug_str("ConsTable"), "ok");
        }

        private void garage_clicked(object sender, EventArgs e)
        {
            DisplayAlert("Garage", App.data.table_debug_str("GarageTable"), "ok");
        }

        private void glass_clicked(object sender, EventArgs e)
        {
            DisplayAlert("Glass", App.data.table_debug_str("GlassTable"), "ok");
        }

        private void green_clicked(object sender, EventArgs e)
        {
            DisplayAlert("Green", App.data.table_debug_str("GreenTable"), "ok");
        }

        private void lock_clicked(object sender, EventArgs e)
        {
            DisplayAlert("Locking", App.data.table_debug_str("LockingTable"), "ok");
        }

        private void panel_clicked(object sender, EventArgs e)
        {
            DisplayAlert("Panel", App.data.table_debug_str("PanelTable"), "ok");
        }

        private void timber_clicked(object sender, EventArgs e)
        {
            DisplayAlert("Timber", App.data.table_debug_str("TimberTable"), "ok");
        }

        private void upvc_clicked(object sender, EventArgs e)
        {
            DisplayAlert("UPVC", App.data.table_debug_str("UPVCTable"), "ok");
        }
    }
}