using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AccidentMenuVehicle : ContentPage
	{

        public AccidentMenuVehicle ()
		{
			InitializeComponent ();
            update_tick_marks();

            
        }


        protected override void OnAppearing()
        {
            update_tick_marks();
            App.data.SaveVehicleAccident();
        }

        private void details_clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AccidentDetailsVehicle(), false);
        }

        private void person_clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PersonInCharge(), false);
        }

        private void other_person_clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OtherPersonVehicle(), false);
        }

        private void police_clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PoliceDetails(), false);
        }

        private void witness_clicked(object sender, EventArgs e)
        {
            var query = App.data.GetVehicleWitnesses(App.CurrentApp.AccidentRecord.RecID);

            if (query.Count > 0)
                Navigation.PushAsync(new Witnesses(), false);
            else
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var response = await Application.Current.MainPage.DisplayAlert("Witnesses?", "Are there witnesses?\n", "   Yes   ", "   No   ");
                    if (response)
                        Navigation.PushAsync(new Witnesses(), false);
                    else
                    {
                        App.CurrentApp.AccidentRecord.c_witness = true;
                        update_tick_marks();
                    }
                });
        }

        private void update_tick_marks()
        {
            App.CurrentApp.AccidentRecord.bComplete = App.CurrentApp.AccidentRecord.c_details
                                                   && App.CurrentApp.AccidentRecord.c_you
                                                   && App.CurrentApp.AccidentRecord.c_them
                                                   && App.CurrentApp.AccidentRecord.c_police
                                                   && App.CurrentApp.AccidentRecord.c_witness
                                                   && App.CurrentApp.AccidentRecord.c_photographs
                                                   && App.CurrentApp.AccidentRecord.c_drawings;

            details_tick.IsVisible = App.CurrentApp.AccidentRecord.c_details;
            person_in_charge_tick.IsVisible = App.CurrentApp.AccidentRecord.c_you;
            other_person_tick.IsVisible = App.CurrentApp.AccidentRecord.c_them;
            police_tick.IsVisible = App.CurrentApp.AccidentRecord.c_police;
            witness_tick.IsVisible = App.CurrentApp.AccidentRecord.c_witness;
            photographs_tick.IsVisible = App.CurrentApp.AccidentRecord.c_photographs;
            drawings_tick.IsVisible = App.CurrentApp.AccidentRecord.c_drawings;
            //insurance_details_image?
        }

        protected override bool OnBackButtonPressed()
        {
            App.data.SaveVehicleAccident();
            base.OnBackButtonPressed();
            return false;
        }

        private void photos_clicked(object sender, EventArgs e)
        {
            App.net.HeaderRecord = new Header();
            //App.net.table_init.CreateHeader();
            App.net.HeaderRecord.iRecordType = 0;
            App.CurrentApp.camera_vehicle = 5;
            Navigation.PushAsync(new Camera(), false);
        }

        private void drawings_clicked(object sender, EventArgs e)
        {
            App.net.HeaderRecord = new Header();
            //App.net.table_init.CreateHeader();
            App.net.HeaderRecord.iRecordType = 0;
            App.CurrentApp.camera_vehicle = 5;
            App.net.root_item_number = 0;
            App.net.drawing_type = "normal";
            App.net.HeaderRecord.udi_cont = String.Format("9{0:0000000}", App.net.AccidentRecord.RecID);
            Navigation.PushAsync(new Drawings(), false);
        }
    }
}