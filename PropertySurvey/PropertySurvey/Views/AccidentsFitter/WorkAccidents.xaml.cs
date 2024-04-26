using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
    public class WorkAccidentListData
    {
        public int uid { get; set; }
        public string sdate { get; set; }
        public bool bSent { get; set; }
        public string back_colour { get; set; }

        public WorkAccidentListData(int uID, string _sdate, string _spcode, bool complete, bool _bSent)
        {
            this.uid = uID;
            this.sdate = _sdate;
            this.bSent = _bSent;

            if (bSent)
                this.back_colour = "#1881bf";
            else if (complete)
                this.back_colour = "#7ccb7e";
            else
                this.back_colour = "#b38e91";
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkAccidents : ContentPage
    {
        WorkAccidentListData selected_data = null;
        bool bSelected = false;

        public WorkAccidents()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DrawList();
        }

        private void DoSelect()
        {
            if (selected_data == (listView as ListView).SelectedItem as WorkAccidentListData && bSelected == false)
            {
                bSelected = true;
                App.net.FAccidentsRecord = App.data.GetWorkAccident(selected_data.uid);
                if (App.net.FAccidentsRecord != null)
                    Navigation.PushAsync(new WorkAccident(), false);
            }
            else
                selected_data = (listView as ListView).SelectedItem as WorkAccidentListData;
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            DoSelect();
        }

        private void CreateWorkAccident(bool _near_miss = false)
        {
            App.CurrentApp.FAccidentsRecord = new FAccidentsTable();

            App.CurrentApp.FAccidentsRecord.date_time = DateTime.Now.ToShortDateString();
            App.CurrentApp.FAccidentsRecord.add1 = "";
            App.CurrentApp.FAccidentsRecord.add2 = "";
            App.CurrentApp.FAccidentsRecord.add3 = "";
            App.CurrentApp.FAccidentsRecord.date_happened = DateTime.Now.ToShortDateString();
            App.CurrentApp.FAccidentsRecord.filer_add1 = "";
            App.CurrentApp.FAccidentsRecord.filer_add2 = "";
            App.CurrentApp.FAccidentsRecord.filer_add3 = "";
            App.CurrentApp.FAccidentsRecord.filer_full_name = "";
            App.CurrentApp.FAccidentsRecord.filer_occupation = "";
            App.CurrentApp.FAccidentsRecord.filer_pcode = "";
            App.CurrentApp.FAccidentsRecord.filer_sign_date = "";
            App.CurrentApp.FAccidentsRecord.full_name = "";
            App.CurrentApp.FAccidentsRecord.how_did_accident_happen = "";
            App.CurrentApp.FAccidentsRecord.materials_used_in_treatment = "";
            App.CurrentApp.FAccidentsRecord.num_of_photographs = 0;
            App.CurrentApp.FAccidentsRecord.occupation = "";
            App.CurrentApp.FAccidentsRecord.pcode = "";
            App.CurrentApp.FAccidentsRecord.person_signed = 0;
            App.CurrentApp.FAccidentsRecord.sign_date = "";
            App.CurrentApp.FAccidentsRecord.spare1 = "";
            App.CurrentApp.FAccidentsRecord.supervisor_signed = 0;
            App.CurrentApp.FAccidentsRecord.time_happened = DateTime.Now.ToShortDateString();
            App.CurrentApp.FAccidentsRecord.spare1 = "";
            App.CurrentApp.FAccidentsRecord.spare2 = "";
            App.CurrentApp.FAccidentsRecord.spare3 = "";
            App.CurrentApp.FAccidentsRecord.spare4 = "";
            App.CurrentApp.FAccidentsRecord.spare5 = "";
            App.CurrentApp.FAccidentsRecord.spare6 = "";
            App.CurrentApp.FAccidentsRecord.spare7 = "";
            App.CurrentApp.FAccidentsRecord.spare8 = "";
            App.CurrentApp.FAccidentsRecord.spare9 = "";
            if (_near_miss)
            {
                App.CurrentApp.FAccidentsRecord.spare10 = "Nearmiss";
            }
            else
            {
                App.CurrentApp.FAccidentsRecord.spare10 = "Accident";
            }

            App.CurrentApp.FAccidentsRecord.spare11 = "";
            App.CurrentApp.FAccidentsRecord.spare12 = "";
            App.CurrentApp.FAccidentsRecord.spare13 = DateTime.Now.ToShortDateString();
            App.CurrentApp.FAccidentsRecord.spare14 = "";
            App.CurrentApp.FAccidentsRecord.spare15 = "";
            //App.CurrentApp.SaveWorkAccident();
        }

        private void accident_clicked(object sender, EventArgs e)
        {
            CreateWorkAccident(false);
            Navigation.PushAsync(new WorkAccident(), false);
        }

        private void near_miss_clicked(object sender, EventArgs e)
        {
            CreateWorkAccident(true);
            Navigation.PushAsync(new WorkAccident(), false);
        }

        private void OnDelete(object sender, EventArgs e)
        {
            if (selected_data != null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var response = await Application.Current.MainPage.DisplayAlert("Confirm", "Delete accident report?\n", "   Yes   ", "   No   ");
                    if (response)
                    {
                        App.data.DeleteWorkAccident(selected_data.uid);
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
            List<WorkAccidentListData> dataSource = new List<WorkAccidentListData>();

            var query = App.data.GetWorkAccidents();
            foreach (var item in query)
                dataSource.Add(new WorkAccidentListData(item.RecID, String.Format("{0:dd/MM/yyyy}", item.date_time) + "  -  " + item.spare10, "code", item.bComplete, item.bSent));

            listView.ItemsSource = dataSource;
            selected_data = null;
            bSelected = false;
        }
    }
}