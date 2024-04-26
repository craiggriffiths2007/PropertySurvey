using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RepairOrReplace : ContentPage
	{
        private t_current_item current_item;

        public RepairOrReplace (t_current_item _current_item)
		{
            current_item = _current_item;
			InitializeComponent ();
		}

        private async void navigate_next_screen(bool repair, bool fensa)
        {
            switch (current_item)
            {
                case t_current_item.item_aluminium:
                    App.net.AlumRecord.bRepair = repair;
                    App.net.AlumRecord.bFencer = fensa;
                    Navigation.InsertPageBefore(new AluminiumItem(), this);
                    await Navigation.PopAsync(false);
                    break;
                case t_current_item.item_bifolding:
                    App.net.BifoldRecord.bRepair = repair;
                    App.net.BifoldRecord.fensa = fensa;
                    Navigation.InsertPageBefore(new BifoldItem(), this);
                    await Navigation.PopAsync(false);
                    break;
                case t_current_item.item_composite:
                    App.net.CompRecord.bRepair = repair;
                    App.net.CompRecord.fensa = fensa;
                    Navigation.InsertPageBefore(new CompositeDoor(), this);
                    await Navigation.PopAsync(false);
                    break;
                case t_current_item.item_conservatory:
                    App.net.ConsRecord.bRepair = repair;
                    App.net.ConsRecord.fensa = fensa;
                    Navigation.InsertPageBefore(new ConservatoryItem(), this);
                    await Navigation.PopAsync(false);
                    break;
                case t_current_item.item_timber:
                    App.net.TimberRecord.bRepair = repair;
                    App.net.TimberRecord.Fensa = fensa;
                    Navigation.InsertPageBefore(new TimberItem(), this);
                    await Navigation.PopAsync(false);
                    break;
                case t_current_item.item_upvc:
                    App.net.UPVCRecord.bRepair = repair;
                    App.net.UPVCRecord.fensa = fensa;
                    Navigation.InsertPageBefore(new UPVCitem(), this);
                    await Navigation.PopAsync(false);
                    break;
            }
        }

        private void replace_old_button_clicked(object sender, EventArgs e)
        {
            navigate_next_screen(false, false);
        }

        private void replace_new_button_clicked(object sender, EventArgs e)
        {
            navigate_next_screen(false, true);
        }

        private void repair_button_clicked(object sender, EventArgs e)
        {
            navigate_next_screen(true, false);
        }
    }
}