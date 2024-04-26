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
    public partial class ToolChecks : ContentPage
    {
        public List<string> menu_items = new List<string>() {
            "DELETE",
            "RESEND",};

        public class ListData
        {
            public int uid { get; set; }
            public string sdate { get; set; }

            public bool bSent { get; set; }
            public string back_colour { get; set; }

            public ListData(int uID, string _sdate, string _spcode, bool _bSent)
            {
                this.uid = uID;

                this.sdate = _sdate;

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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DrawList();
        }

        public ToolChecks()
        {
            InitializeComponent();
        }

        private void OnAdd(object sender, EventArgs e)
        {
            CreateToolCheck();
            App.data.SaveToolsRecord();

            Navigation.PushAsync(new ToolCheck(), false);
        }

        void CreateToolCheck()
        {
            App.net.ToolsRecord = new ToolsTable();

            App.net.ToolsRecord.date_done = DateTime.Today.ToShortDateString();
            App.net.ToolsRecord.signature_filename = "";
            App.net.ToolsRecord.signature_filename2 = "";
            App.net.ToolsRecord.signature_printed = "";
            App.net.ToolsRecord.registration = "";
            App.net.ToolsRecord.branch = "";
            App.net.ToolsRecord.CheckID = "";
            App.net.ToolsRecord.photo_filename = "";
        }

        private async void OnDelete(object sender, EventArgs e)
        {
            if (selected_data != null)
            {
                var answer = await DisplayAlert("Delete tool check sheet?", "", "   Yes   ", "   No   ");
                if (answer == true)
                {
                    App.data.DeleteToolsCheck(selected_data.uid);
                    DrawList();
                }
            }
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
            if (selected_data != null)
            {
                bSelected = true;
                App.data.LoadToolsCheck(selected_data.uid);
                Navigation.PushAsync(new ToolCheck(), false);
            }
        }

        private void OnSelect(object sender, EventArgs e)
        {
            DoSelect();
        }

        private void DrawList()
        {
            List<ListData> dataSource = new List<ListData>();

            var query = App.data.GetToolsChecks();

            foreach (var item in query)
            {
                DateTime delete_date = DateTime.Now.AddDays(-30);

                if (item.date_done == null || (DateTime.Parse(item.date_done) < delete_date))
                {
                    App.data.DeleteToolsCheck(item.RecID);
                }
                else
                {
                    dataSource.Add(new ListData(item.RecID, item.date_done + "  -  " + item.registration, "code", item.bSent));
                }
            }

            listView.ItemsSource = dataSource;
        }
    }
}