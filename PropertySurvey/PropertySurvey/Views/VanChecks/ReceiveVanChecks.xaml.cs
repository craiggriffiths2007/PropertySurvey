using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays((-1 * diff)).Date;
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReceiveVanChecks : ContentPage
    {
        XDocument id_4d;

        int total_images;
        int current_image;

        bool bDownloadOnly = false;

        string sendResponse = "";

        StringBuilder postData = new StringBuilder(128000);

        public ReceiveVanChecks()
        {
            InitializeComponent();


            DownloadVans();
        }

        private void DownloadVans()
        {
            bDownloadOnly = true;

            Uri thisuri = new Uri(App.net.App_Settings.set_url + "/WM7Communication/DownloadVansList");
            HttpHelper helper = new HttpHelper(thisuri, "POST",
            new KeyValuePair<string, string>("ContractFile", App.net.App_Settings.set_branchcode));
            helper.ResponseComplete += new HttpResponseCompleteEventHandler(this.CommandComplete);
            helper.Execute();
        }

        private void CommandComplete(HttpResponseCompleteEventArgs e)
        {
            sendResponse = e.Response;

            Device.BeginInvokeOnMainThread(CompleteDownload);
        }

        private void CompleteDownload()
        {
            if (bDownloadOnly == true)
            {
                bool bContinue = true;
                if (sendResponse == "<none>" || sendResponse == "")
                {

                }
                else
                {
                    string reg = "";

                    if (sendResponse == "nointernet")
                    {
                        complete_label.Text = "No Internet";
                        complete_label.IsVisible = true;
                        //id_4d = "";
                    }
                    else
                    {
                        id_4d = XDocument.Parse(sendResponse);
                        App.CurrentApp.VanChecksHeader = null;

                    }

                    bool bHeaderCreated = false;

                    if (sendResponse == "nointernet")
                        complete_label.Text = "No Internet";

                    List<VanChecksHeader> headers = App.data.GetVanChecksByDateandBranch(DateTime.Now.StartOfWeek(DayOfWeek.Monday).ToShortDateString(), App.net.App_Settings.set_branchcode);

                    if (headers.Count == 0 && sendResponse != "nointernet")
                    {
                        foreach (var word in id_4d.Element("complete").Elements())
                        {
                            foreach (var word2 in word.Element("recordcodes").Elements("van"))
                            {
                                string type = (string)word2.Element("type");

                                reg = (string)word2.Element("reg");
                                if (bHeaderCreated == false)
                                {
                                    App.net.table_init.CreateVanCheck();
                                    App.CurrentApp.VanChecksHeader.check_date = DateTime.Now.StartOfWeek(DayOfWeek.Monday).ToShortDateString();
                                    App.CurrentApp.VanChecksHeader.spare_s_1 = App.net.App_Settings.set_branchcode;
                                    App.data.SaveVanChecksHeader();

                                    bHeaderCreated = true;
                                }
                                if ((type == null) || (type == "van"))
                                {
                                    App.net.table_init.CreateVan();
                                    App.CurrentApp.WeeklyVanCheckSheet.vehicle_reg = reg;
                                    App.net.WeeklyVanCheckSheet.is_complete = 1;

                                    App.CurrentApp.WeeklyVanCheckSheet.branch = App.net.App_Settings.set_branchcode;
                                    CopyLastWeeksDiagramImages(type);
                                    App.data.SaveVanChecksVan();
                                }
                                if (type == "car")
                                {
                                    App.net.table_init.CreateCar();
                                    App.CurrentApp.CarPanelSheet.vehicle_reg = reg;
                                    App.net.CarPanelSheet.is_complete = 1;
                                    //App.CurrentApp.CarPanelSheet.branch = App.net.App_Settings.set_branchcode;
                                    CopyLastWeeksDiagramImages(type);
                                    App.data.SaveVanChecksCar();
                                }
                                if (type == "hgv")
                                {
                                    App.net.table_init.CreateDelivery();
                                    App.CurrentApp.DeliveryVehicleCheckList.vehicle_registration = reg;
                                    App.net.DeliveryVehicleCheckList.is_complete = 1;
                                    //App.CurrentApp.DeliveryVehicleCheckList.branch = TheSettings.set_branchcode;
                                    //CopyLastWeeksDiagramImages();
                                    App.data.SaveVanChecksDelivery();
                                }
                                if (type == "deliveryvan")
                                {
                                    App.net.table_init.CreateDeliveryVan();
                                    App.CurrentApp.DeliveryVanVehicleCheckList.vehicle_registration = reg;
                                    App.net.DeliveryVanVehicleCheckList.is_complete = 1;
                                    //App.CurrentApp.WeeklyVanCheckSheet.branch = TheSettings.set_branchcode;
                                    CopyLastWeeksDiagramImages(type);
                                    App.data.SaveVanChecksDeliveryVan();
                                }
                            }

                            if (App.CurrentApp.VanChecksHeader != null)
                            {
                                App.data.SaveVanChecks();
                            }
                        }
                    }
                }
            }
            if (sendResponse != "nointernet")
            {
                Navigation.PopAsync(false);
            }
        }

        private void CopyLastWeeksDiagramImages(string type)
        {
            int i;

            try
            {
                string LastMonday = DateTime.Now.StartOfWeek(DayOfWeek.Monday).AddDays(-7).ToShortDateString();

                List<VanChecksHeader> headers = App.data.GetVanChecksByDateandBranch(LastMonday, App.net.App_Settings.set_branchcode);

                foreach (var item in headers)
                {

                    switch (type)
                    {
                        //////////////////////////////////////////////////
                        //
                        //  Weekly Van Checks
                        //
                        //////////////////////////////////////////////////
                        case "van":
                            List<WeeklyVanCheckSheet> weekly_van = App.data.GetWeeklyVanChecksByIDandReg(item.unique_id, App.CurrentApp.WeeklyVanCheckSheet.vehicle_reg);
                            foreach (var item2 in weekly_van)
                            {
                                App.CurrentApp.WeeklyVanCheckSheet.damage_back = item2.damage_back;
                                App.CurrentApp.WeeklyVanCheckSheet.damage_front = item2.damage_front;
                                App.CurrentApp.WeeklyVanCheckSheet.damage_driver = item2.damage_driver;
                                App.CurrentApp.WeeklyVanCheckSheet.damage_pass = item2.damage_pass;

                                for (i = 0; i < 6; i++)
                                {
                                    string sfname = "";
                                    string dfname = "";
                                    string angle = "";
                                    switch (i)
                                    {
                                        case 0: angle = "pad"; break;
                                        case 1: angle = "drd"; break;
                                        case 2: angle = "frd"; break;
                                        case 3: angle = "red"; break;
                                        case 4: angle = "cab"; break;
                                        case 5: angle = "ins"; break;
                                    }
                                    int item_no = 0;
                                    string check_type = "";
                                    check_type = "v";
                                    item_no = App.CurrentApp.WeeklyVanCheckSheet.item_no;

                                    sfname = string.Format("Photos/VC/" + item.unique_id + "_{0:00000000}_" + check_type + "_" + angle + ".jpg", item2.item_no);
                                    dfname = string.Format("Photos/VC/" + App.CurrentApp.VanChecksHeader.unique_id + "_{0:00000000}_" + check_type + "_" + angle + ".jpg", item_no);
                                    if (App.files.FileExists(sfname) == true && App.files.FileExists(dfname) == false)
                                    {
                                        App.files.CopyFile(sfname, dfname);
                                    }
                                }
                            }
                            break;

                        //////////////////////////////////////////////////
                        //
                        //  Car Panel Checks
                        //
                        //////////////////////////////////////////////////
                        case "car":
                            List<CarPanelSheet> car_panel = App.data.GetCarPanelSheetByIDandReg(item.unique_id, App.CurrentApp.CarPanelSheet.vehicle_reg);

                            foreach (var item2 in car_panel)
                            {
                                App.CurrentApp.CarPanelSheet.damage_back = item2.damage_back;
                                App.CurrentApp.CarPanelSheet.damage_front = item2.damage_front;
                                App.CurrentApp.CarPanelSheet.damage_driver = item2.damage_driver;
                                App.CurrentApp.CarPanelSheet.damage_pass = item2.damage_pass;

                                for (i = 0; i < 6; i++)
                                {
                                    string sfname = "";
                                    string dfname = "";
                                    string angle = "";
                                    switch (i)
                                    {
                                        case 0: angle = "pad"; break;
                                        case 1: angle = "drd"; break;
                                        case 2: angle = "frd"; break;
                                        case 3: angle = "red"; break;
                                        case 4: angle = "cab"; break;
                                        case 5: angle = "ins"; break;
                                    }
                                    int item_no = 0;
                                    string check_type = "";
                                    check_type = "c";
                                    item_no = App.CurrentApp.CarPanelSheet.item_no;

                                    sfname = string.Format("/Photos/VC/" + item.unique_id + "_{0:00000000}_" + check_type + "_" + angle + ".jpg", item2.item_no);
                                    dfname = string.Format("/Photos/VC/" + App.CurrentApp.VanChecksHeader.unique_id + "_{0:00000000}_" + check_type + "_" + angle + ".jpg", item_no);
                                    if (App.files.FileExists(sfname) == true && App.files.FileExists(dfname) == false)
                                    {
                                        App.files.CopyFile(sfname, dfname);
                                    }
                                }
                            }
                            break;
                        //////////////////////////////////////////////////
                        //
                        //  Delivery Van
                        //
                        //////////////////////////////////////////////////
                        case "deliveryvan":
                            List<DeliveryVanVehicleCheckList> delivery_van_vehicle = App.data.GetDeliveryVanVehicleCheckListByIDandReg(item.unique_id, App.CurrentApp.DeliveryVanVehicleCheckList.vehicle_registration);

                            foreach (var item2 in delivery_van_vehicle)
                            {
                                App.CurrentApp.DeliveryVanVehicleCheckList.damage_back = item2.damage_back;
                                App.CurrentApp.DeliveryVanVehicleCheckList.damage_front = item2.damage_front;
                                App.CurrentApp.DeliveryVanVehicleCheckList.damage_driver = item2.damage_driver;
                                App.CurrentApp.DeliveryVanVehicleCheckList.damage_pass = item2.damage_pass;
                                for (i = 0; i < 6; i++)
                                {
                                    string sfname = "";
                                    string dfname = "";
                                    string angle = "";
                                    switch (i)
                                    {
                                        case 0: angle = "pad"; break;
                                        case 1: angle = "drd"; break;
                                        case 2: angle = "frd"; break;
                                        case 3: angle = "red"; break;
                                        case 4: angle = "cab"; break;
                                        case 5: angle = "ins"; break;
                                    }
                                    int item_no = 0;
                                    string check_type = "";
                                    check_type = "a";
                                    item_no = App.CurrentApp.DeliveryVanVehicleCheckList.item_no;

                                    sfname = string.Format("/Photos/VC/" + item.unique_id + "_{0:00000000}_" + check_type + "_" + angle + ".jpg", item2.item_no);
                                    dfname = string.Format("/Photos/VC/" + App.CurrentApp.VanChecksHeader.unique_id + "_{0:00000000}_" + check_type + "_" + angle + ".jpg", item_no);
                                    if (App.files.FileExists(sfname) == true && App.files.FileExists(dfname) == false)
                                    {
                                        App.files.CopyFile(sfname, dfname);
                                    }
                                }
                            }
                            break;


                        //////////////////////////////////////////////////
                        //
                        //  Delivery Van HGV
                        //
                        //////////////////////////////////////////////////
                        case "hgv":
                            List<DeliveryVehicleCheckList> delivery_vehicle = App.data.GetDeliveryVehicleCheckListByIDandReg(item.unique_id, App.CurrentApp.DeliveryVehicleCheckList.vehicle_registration);

                            foreach (var item2 in delivery_vehicle)
                            {
                                App.CurrentApp.DeliveryVehicleCheckList.damage_back = item2.damage_back;
                                App.CurrentApp.DeliveryVehicleCheckList.damage_front = item2.damage_front;
                                App.CurrentApp.DeliveryVehicleCheckList.damage_driver = item2.damage_driver;
                                App.CurrentApp.DeliveryVehicleCheckList.damage_pass = item2.damage_pass;

                                for (i = 0; i < 6; i++)
                                {
                                    string sfname = "";
                                    string dfname = "";
                                    string angle = "";
                                    switch (i)
                                    {
                                        case 0: angle = "pad"; break;
                                        case 1: angle = "drd"; break;
                                        case 2: angle = "frd"; break;
                                        case 3: angle = "red"; break;
                                        case 4: angle = "cab"; break;
                                        case 5: angle = "ins"; break;
                                    }
                                    int item_no = 0;
                                    string check_type = "";
                                    check_type = "d";
                                    item_no = App.CurrentApp.DeliveryVehicleCheckList.item_no;

                                    sfname = string.Format("/Photos/VC/" + item.unique_id + "_{0:00000000}_" + check_type + "_" + angle + ".jpg", item2.item_no);
                                    dfname = string.Format("/Photos/VC/" + App.CurrentApp.VanChecksHeader.unique_id + "_{0:00000000}_" + check_type + "_" + angle + ".jpg", item_no);
                                    if (App.files.FileExists(sfname) == true && App.files.FileExists(dfname) == false)
                                    {
                                        App.files.CopyFile(sfname, dfname);
                                    }
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

    }
}