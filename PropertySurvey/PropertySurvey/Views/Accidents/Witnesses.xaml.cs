using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Witnesses : ContentPage
	{
        public class ListData
        {
            public int uid { get; set; }
            public string name { get; set; }
            public string telephone { get; set; }
            public string back_colour { get; set; }

            public ListData(int uID, string _name, string _telephone, bool complete)
            {
                this.uid = uID;

                this.name = _name;
                this.telephone = _telephone;

                if (complete)
                    this.back_colour = "#7ccb7e";
                else
                    this.back_colour = "DarkRed";
            }
        }

        ListData selected_data = null;
        bool bSelected = false;
        bool all_that_exist_are_complete = true;

        public Witnesses ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DrawList();
        }

        private void DoSelect()
        {
            if (selected_data == (listView as ListView).SelectedItem as ListData && bSelected == false)
            {
                bSelected = true;
                App.net.WitnessRecord = App.data.GetVehicleWitness(selected_data.uid);
                if (App.net.WitnessRecord != null)
                    Navigation.PushAsync(new Witness(), false);
            }
            else
                selected_data = (listView as ListView).SelectedItem as ListData;
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            DoSelect();
        }

        public void CreateWitness()
        {
            App.CurrentApp.WitnessRecord = new WhitnessesData();

            App.CurrentApp.WitnessRecord.AccidentRecID = App.CurrentApp.AccidentRecord.RecID;
            App.CurrentApp.WitnessRecord.p_name = "";
            App.CurrentApp.WitnessRecord.p_add1 = "";
            App.CurrentApp.WitnessRecord.p_add2 = "";
            App.CurrentApp.WitnessRecord.p_add3 = "";
            App.CurrentApp.WitnessRecord.p_pcode = "";
            App.CurrentApp.WitnessRecord.p_wittel = "";
        }

        private void OnAdd(object sender, EventArgs e)
        {
            CreateWitness();
            Navigation.PushAsync(new Witness(), false);
        }

        private void OnDelete(object sender, EventArgs e)
        {
            if (selected_data != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var response = await Application.Current.MainPage.DisplayAlert("Confirm", "Delete witness?\n", "   Yes   ", "   No   ");
                    if (response)
                    {
                        App.data.DeleteVehicleWitness(selected_data.uid);
                        DrawList();
                    }
                });
            }
        }

        private void OnSelect(object sender, EventArgs e)
        {
            DoSelect();
        }

        private void DrawList()
        {
            all_that_exist_are_complete = true;
            List<ListData> dataSource = new List<ListData>();

            var query = App.data.GetVehicleWitnesses(App.CurrentApp.AccidentRecord.RecID);
            foreach (var item in query)
            {
                dataSource.Add(new ListData(item.RecID, item.p_name, item.p_wittel, item.complete));
                if (!item.complete)
                    all_that_exist_are_complete = false;
            }
            listView.ItemsSource = dataSource;
            selected_data = null;
            bSelected = false;
        }

        protected override bool OnBackButtonPressed()
        {

            App.CurrentApp.AccidentRecord.c_witness = all_that_exist_are_complete;
            base.OnBackButtonPressed();
            this.Navigation.PopAsync(false);
            return true;
        }
    }
}