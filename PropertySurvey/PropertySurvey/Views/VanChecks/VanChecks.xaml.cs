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
    public partial class VanChecks : ContentPage
    {
        public List<string> menu_items = new List<string>() {
            "DELETE",
            "RESEND",};

        public class ListData
        {
            public string uid { get; set; }
            public string sdate { get; set; }
            public string spcode { get; set; }
            public string smiles { get; set; }
            public string fpcode { get; set; }
            public string fmiles { get; set; }
            public string bcode { get; set; }
            public bool bSent { get; set; }
            public bool bDone { get; set; }
            public string back_colour { get; set; }

            public string jtitle { get; set; }

            public ListData(string uID, string _sdate, string _spcode, string _smiles, string _fpcode, string _fmiles, bool _bDone, bool _bSent, string _bcode)
            {
                this.uid = uID;
                this.bcode = _bcode;
                this.sdate = _sdate;
                this.spcode = _spcode;
                this.smiles = _smiles;
                this.fpcode = _fpcode;
                this.fmiles = _fmiles;
                this.bSent = _bSent;
                this.bDone = _bDone;
                if (this.bcode != "")
                    jtitle = "Weekly Branch Van Checks";
                else
                    jtitle = "";

                if (_bSent == false)
                {
                    if (_bDone == false)
                    {
                        if (this.bcode != "")
                            this.back_colour = "#c49fa2";
                        else
                            this.back_colour = "#b38e91";
                    }
                    else
                        this.back_colour = "#7ccb7e";
                }
                else
                    this.back_colour = "#1881bf";
            }
        }

        ListData selected_data = null;
        bool bSelected = false;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DrawList();
            selected_data = null;

            App.CurrentApp.camera_vehicle = 0;
        }

        public VanChecks()
        {
            InitializeComponent();

            DeleteOldVanChecks();

            menu_pick.ItemsSource = menu_items;
        }

        private void DrawList()
        {
            List<ListData> dataSource = new List<ListData>();

            List<VanChecksHeader> headers = App.data.GetAllVanCheckHeadersSorted();
            headers.Reverse();
            foreach (var item in headers)
            {
                dataSource.Add(new ListData(item.unique_id, String.Format("{0:dd/MM/yyyy}", item.check_date), "D-Vans: " + item.total_delivery_van.ToString(), "D-HGVs: " + item.total_delivery.ToString(), "Cars : " + item.total_cars.ToString(), "Vans : " + item.total_vans.ToString(), item.bComplete, item.bSent, item.spare_s_1));
            }

            listView.ItemsSource = dataSource;
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            DoSelect();
        }

        private void DoSelect()
        {
            if (selected_data == (listView as ListView).SelectedItem as ListData && bSelected == false)
            {
                App.data.LoadVanCheckHeader(selected_data.uid);
                Navigation.PushAsync(new VanCheckType(), false);
            }
            else
            {
                selected_data = (listView as ListView).SelectedItem as ListData;
            }
        }

        private void OnSelect(object sender, EventArgs e)
        {
            DoSelect();
        }

        private void OnMenuClicked(object sender, EventArgs e)
        {
            menu_pick.Focus();
        }

        private void OnMenuChanged(object sender, EventArgs e)
        {
            if (menu_pick.SelectedIndex > -1)
                switch (menu_pick.Items[menu_pick.SelectedIndex])
                {
                    case "DELETE":DeleteCheck(); break;
                    case "RESEND":ResendCheck(); break;
                }
        }

        private async void DeleteCheck()
        {
            if (selected_data != null)
            {
                var answer = await DisplayAlert("Delete Checks?", "", "   Yes   ", "   No   ");
                if (answer == true)
                {
                    App.data.DeleteVanCheck(selected_data.uid);
                    DrawList();
                }
            }
        }
        private async void ResendCheck()
        {
            if (selected_data != null)
            {
                var answer = await DisplayAlert("Resen Checks?", "", "   Yes   ", "   No   ");
                if (answer == true)
                {
                    App.data.ResendVanCheck(selected_data.uid);
                    DrawList();
                }
            }
        }

        public void DeleteOldVanChecks()
        {
            List<VanChecksHeader> headers = App.data.GetOldVanCheckHeaders();
            foreach (var item in headers)
            {
                DeleteVanCheckImages(item.unique_id);
            }
            App.data.DeleteOldVanChecks();
        }

        public void DeleteVanCheckImages(string vc_id)
        {
            int i;
            int total;

            List<string> fileNames = App.files.GetFileList("Photos/VC/","*");
            foreach (var filename in fileNames)
            {
                if (App.files.FileExists("Photos/VC/" + filename))
                {
                    if (filename.Substring(0, 25) == vc_id)
                    {
                        App.files.DeleteFile("Photos/VC/" + filename);
                    }
                }
            }
            List<string> fileNames2 = App.files.GetFileList("Signatures/VC/", "*");
            foreach (var filename in fileNames2)
            {
                if (App.files.FileExists("Signatures/VC/" + filename))
                {
                    if (filename.Substring(0, 25) == vc_id)
                    {
                        App.files.DeleteFile("Signatures/VC/" + filename);
                    }
                }
            }
        }

        private void OnSendReceive(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SendVanChecks(), false);
        }

        private void OnReceive(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ReceiveVanChecks(), false);
        }
    }
}