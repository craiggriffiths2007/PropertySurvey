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
	public partial class MileageList : ContentPage
	{
        public class ListData
        {
            public int uid { get; set; }
            public string sdate { get; set; }
            public string spcode { get; set; }
            public string smiles { get; set; }
            public string fpcode { get; set; }
            public string fmiles { get; set; }
            public bool bSent { get; set; }
            public string back_colour { get; set; }
             
            public ListData(int uID, string _sdate, string _spcode, string _smiles, string _fpcode, string _fmiles, bool _bSent)
            {
                this.uid = uID;

                this.sdate = _sdate;
                this.spcode = _spcode;
                this.smiles = _smiles;
                this.fpcode = _fpcode;
                this.fmiles = _fmiles;
                this.bSent = _bSent;

                if (_bSent == false)
                {
                    this.back_colour = "#b38e91";
                }
                else
                {
                    this.back_colour = "#1881bf";
                }
            }
        }

        ListData selected_data = null;
        bool bSelected = false;

        public MileageList ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            bSelected = false;
            DrawList();
        }

        private void DrawList()
        {
            List<ListData> dataSource = new List<ListData>();

            List<Milage_sheet> sheets = App.data.GetMileageSheets();
            sheets.Reverse();

            foreach (var item in sheets)
            {
                dataSource.Add(new ListData(item.RecID, String.Format("{0:dd/MM/yyyy}", item.sheet_date), item.start_postcode, item.start_mileage, item.finish_postcode, item.end_mileage, item.bSent));
            }
            listView.ItemsSource = dataSource;
            selected_data = null;
        }

        private void OnAdd(object sender, EventArgs e)
        {
            App.data.CreateMileage();
            Navigation.PushAsync(new MileageSheetInput(), false);
        }

        private async void OnDelete(object sender, EventArgs e)
        {
            if (selected_data != null)
            {
                var answer = await DisplayAlert("Delete mileage sheet?", "", "   Yes   ", "   No   ");
                if (answer == true)
                {
                    App.data.DeleteMileageSheet(selected_data.uid);
                    DrawList();
                }
            }
        }

        private void OnSelect(object sender, EventArgs e)
        {
            DoSelect();
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (selected_data == (listView as ListView).SelectedItem as ListData)
            {
                DoSelect();
            }
            else
            {
                selected_data = (listView as ListView).SelectedItem as ListData;
            }
        } 

        private void DoSelect()
        {
            if (selected_data != null && bSelected==false)
            {
                bSelected = true;
                App.data.LoadMileage(selected_data.uid);
                Navigation.PushAsync(new MileageSheetInput(),false);
            }
        }

    }
}
