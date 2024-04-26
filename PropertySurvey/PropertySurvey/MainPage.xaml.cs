using AudioPlayEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace PropertySurvey
{
    [ContentProperty("Source")]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }
        
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
            {
                return null;
            }
            // Do your translation lookup here, using whatever method you require
            var imageSource = ImageSource.FromResource(Source);

            return imageSource;
        }
    }

    public partial class MainPage : ContentPage
    {
        public List<string> menu_items = new List<string>() {
            "SETTINGS",
            "RESEND",
            "DELETE",
            "MILEAGE SHEETS",
            "HEALTH AND SAFETY",
            "VAN CHECKS",
            "LADDER CHECKS",
            "WORK ACCIDENT",
            "VEHICLE ACCIDENT",
            "CONTRACT COMMENTS",
            "STOCK ORDER",
            "TOOL CHECKS"};

        public class ListData
        {
            public int RecID { get; set; }
            public string contract { get; set; }
            public string Name { get; set; }
            public bool bComplete { get; set; }
            public bool bSent { get; set; }
            public bool bSpecial { get; set; }
            public string colour { get; set; }
            public string back_colour { get; set; }
            public string text_colour { get; set; }
            public string fore_colour { get; set; }
            public string stateimage { get; set; }
            public string Postcode { get; set; }
            public string jobtime { get; set; }
            public int iRecordType { get; set; }

            public ListData(int uID, string jobID, string Name, bool bComplete, bool bSent, bool _bSpecial, string pcode, string time)
            {
                this.RecID = uID;
                this.contract = jobID;
                this.Name = Name;
                this.bComplete = bComplete;
                this.bSent = bSent;
                this.bSpecial = _bSpecial;
                this.Postcode = pcode;
                this.jobtime = time;

                if (bSent == true)
                {
                    this.back_colour = "#1881bf";
                }
                else
                {
                    if (bComplete == true)
                    {
                        this.back_colour = "#7ccb7e";
                    }
                    else
                    {
                        this.back_colour = "#bfbfbf";
                    }
                }
                if (bSpecial == true)
                {
                    this.text_colour = "DarkRed";
                    /*
                    if (bComplete == true)
                    {
                        this.text_colour = "DarkRed";
                    }
                    else
                    {
                        this.text_colour = "DarkRed";
                    }*/

                }
                else
                {
                    this.text_colour = "#444444";
                }
            }
        }

        bool bSelected = false;


        public MainPage()
        {
            InitializeComponent();

            menu_pick.ItemsSource = menu_items;

            datepicker1.Date = DateTime.Today;
            diagnostics_area.IsVisible = App.net.App_Settings.set_ownercode == "Z987";


            //Device.BeginInvokeOnMainThread(SetUpdateAvailableButton);
            //DependencyService.Get<IAudio>().PlayAudioFile("loading.mp3");
        }

        private void CreateList()
        {
            List<ListData> dataSource = new List<ListData>();

            List<Header_Index> query = App.data.GetHeaderIndexByDate(datepicker1.Date);
            query = query.OrderBy(x => x.udi_start).ToList();


            bSelected = false;
            foreach (var item in query)
            {
                string jobtime = String.Format("{0:t}", item.udi_start.Substring(item.udi_start.Length - 8, 5)) + " - " + String.Format("{0:t}", item.udi_fin.Substring(item.udi_fin.Length - 8, 5));

                if (item.iRecordType == 0)
                {
                    if (item.b_mrk == true)
                    {
                        dataSource.Add(new ListData(item.header_rec_id, "V  " + item.udi_cont, item.uc_name, item.bDone, item.bSent, item.bSpecialIns, item.uc_postcode, jobtime));
                    }
                    else
                    {
                        if (item.typeA == "Complaint")
                        {
                            dataSource.Add(new ListData(item.header_rec_id, "C  " + item.udi_cont, item.uc_name, item.bDone, item.bSent, item.bSpecialIns, item.uc_postcode, jobtime));
                        }
                        else
                        {
                            dataSource.Add(new ListData(item.header_rec_id, "S  " + item.udi_cont, item.uc_name, item.bDone, item.bSent, item.bSpecialIns, item.uc_postcode, jobtime));
                        }
                    }
                }
                else // if (item.iRecordType == 1 || item.iRecordType == 2)
                {
                    switch (item.typeA)
                    {
                        case "Unfinished": dataSource.Add(new ListData(item.header_rec_id, "U  " + item.udi_cont, item.uc_name, item.bDone, item.bSent, item.bSpecialIns, item.uc_postcode, jobtime)); break;
                        case "Remedial": dataSource.Add(new ListData(item.header_rec_id, "R  " + item.udi_cont, item.uc_name, item.bDone, item.bSent, item.bSpecialIns, item.uc_postcode, jobtime)); break;
                        case "Securing": dataSource.Add(new ListData(item.header_rec_id, "I  " + item.udi_cont, item.uc_name, item.bDone, item.bSent, item.bSpecialIns, item.uc_postcode, jobtime)); break;
                        case "Complaint": dataSource.Add(new ListData(item.header_rec_id, "C  " + item.udi_cont, item.uc_name, item.bDone, item.bSent, item.bSpecialIns, item.uc_postcode, jobtime)); break;
                        default: dataSource.Add(new ListData(item.header_rec_id, "F  " + item.udi_cont, item.uc_name, item.bDone, item.bSent, item.bSpecialIns, item.uc_postcode, jobtime)); break;
                    }
                }
            }
            listView.ItemsSource = dataSource;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (App.net.App_Settings.set_branchcode != "")
            {
                List<VanChecksHeader> headers = App.data.GetVanChecksByDateandBranch(DateTime.Now.StartOfWeek(DayOfWeek.Monday).ToShortDateString(), App.net.App_Settings.set_branchcode);
                if (headers.Count() == 0 || headers.ElementAt(0).bSent == false)
                {
                    VanChecksIcon.IconImageSource = "Van.png";
                }
                ToolChecksIcon.IconImageSource = "tools.png";
            }
            else
                VanChecksIcon.IconImageSource = "";
            bSelected = false;
            ((App)App.Current).ResumeAtPropertySurveyId = -1;
            
            if(App.net.App_Settings.new_mail>0)
            {
                mail_button.ImageSource = "Envelope2.png";
            }
            else
            {
                mail_button.ImageSource = "Envelope1.png";
            }
            App.net.camera_landscape_mode = false;
            if (App.net.bCreateIndex == true)
            {
                App.data.CreateHeaderIndex();

                App.net.bCreateIndex = false;
            }
            CreateList();
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PropertySurveyItemPage
            {
                BindingContext = new Header()
            }, false);
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (((App)App.Current).ResumeAtPropertySurveyId == (e.Item as ListData).RecID)
            {
                if (App.net.App_Settings.new_mail > 0)
                {
                    DisplayAlert("", "Read mail before continuing", "OK");
                    Navigation.PushAsync(new Messages(), false);
                }
                else
                {
                    if (bSelected == false)
                    {
                        bSelected = true;
                        App.net.HeaderRecord = App.data.GetHeader(((App)App.Current).ResumeAtPropertySurveyId);

                        if (App.net.HeaderRecord.b_mrk == true)
                        {
                            if (App.net.HeaderRecord.bDamTicked == false)
                            {
                                Navigation.PushAsync(new Damage(), false);
                            }
                            else
                            {
                                Navigation.PushAsync(new SpotCheckHeader(), false);
                            }
                        }
                        else
                        {
                            if (App.net.HeaderRecord.iRecordType == 0)
                            {
                                if (App.net.HeaderRecord.bDamTicked == false)
                                {
                                    Navigation.PushAsync(new Damage(), false);
                                }
                                else
                                {
                                    Navigation.PushAsync(new PropertySurveyItemPage(), false);
                                }
                            }
                            else
                            {
                                if (App.net.HeaderRecord.bDone == true)
                                {
                                    Navigation.PushAsync(new FitterHeader(), false);
                                }
                                else
                                {
                                    Navigation.PushAsync(new FitterWarning(), false);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                ((App)App.Current).ResumeAtPropertySurveyId = (e.Item as ListData).RecID;
            }
        }

        /*
        private void OnReceiveClicked(object sender, EventArgs e)
        {
            if (App.net.App_Settings.set_ownercode == "")
                Navigation.PushAsync(new SettingsPage(), false);
            else
                Navigation.PushAsync(new SendSurveys());
            //Navigation.PushAsync(new SendReceive());
        }
        */

        private void OnDateSelected(object sender, DateChangedEventArgs e)
        {
            CreateList();
        }
        protected override bool OnBackButtonPressed()
        {


            base.OnBackButtonPressed();
            return true;
        }
        private void OnBack(object sender, EventArgs e)
        {
            datepicker1.Date = datepicker1.Date.AddDays(-1);
            App.data.SaveSettings();
        }

        private void OnNext(object sender, EventArgs e)
        {
            datepicker1.Date = datepicker1.Date.AddDays(1);
        }

        private void OnToday(object sender, EventArgs e)
        {
            datepicker1.Date = DateTime.Today;
        }

        private void OnMess(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Messages(), false);
        }

        private void OnSel(object sender, EventArgs e)
        {
            if (((App)App.Current).ResumeAtPropertySurveyId > -1)
            {
                if (App.net.App_Settings.new_mail > 0)
                {
                    DisplayAlert("", "Please read new mail to continue", "OK");
                }
                else
                {
                    App.net.HeaderRecord = App.data.GetHeader(((App)App.Current).ResumeAtPropertySurveyId);

                    if (App.net.HeaderRecord.bDamTicked == false)
                    {
                        Navigation.PushAsync(new Damage(), false);
                    }
                    else
                    {
                        Navigation.PushAsync(new PropertySurveyItemPage(), false);
                    }
                }
            }
        }

        private void OnMenuClicked(object sender, EventArgs e)
        {
            
            menu_pick.Focus();
        }

        private void OnMenuChanged(object sender, EventArgs e)
        {
            /*
            "SETTINGS",
            "MILEAGE SHEETS",
            "HEALTH AND SAFETY",
            "VAN CHECKS",
            "LADDER CHECKS",
            "RESEND",
            "DELETE",
            "WORK ACCIDENT",
            "VEHICLE ACCIDENT",
            "CONTRACT COMMENTS",
            "STOCK ORDER"};
             */
            if (menu_pick.SelectedIndex > -1)
            switch (menu_pick.Items[menu_pick.SelectedIndex])
            {
                case "SETTINGS":
                    Navigation.PushAsync(new SettingsPage(), false); break;
                case "MILEAGE SHEETS":
                    if (App.net.App_Settings.set_ownercode == "")
                        Navigation.PushAsync(new SettingsPage(), false);
                    else
                        Navigation.PushAsync(new MileageList(), false); 
                    break;

                case "VAN CHECKS": Navigation.PushAsync(new VanChecks(), false); break;
                case "RESEND": ResendContract(); break;
                case "DELETE": DeleteContract(); break;
                case "WORK ACCIDENT": Navigation.PushAsync(new WorkAccidents(), false); break;
                case "VEHICLE ACCIDENT": Navigation.PushAsync(new AccidentsVehicle(), false); break;
                case "LADDER CHECKS": if (App.net.App_Settings.able_to_ladder_check == 1) { Navigation.PushAsync(new Ladders(), false); } else { DisplayAlert("", "Not authorized", "OK"); } break;
                case "CONTRACT COMMENTS": if (App.net.App_Settings.able_to_send_comments == 1) { Navigation.PushAsync(new ContractComments(), false); } else { DisplayAlert("", "Not authorized", "OK"); } break;
                case "STOCK ORDER": Device.OpenUri(new Uri("http://192.168.137.15:7293/stock_mobile.html")); break;
                case "TOOL CHECKS": Navigation.PushAsync(new ToolChecks(), false); break;
            }
        }

        private async void DeleteContract()
        {
            if (((App)App.Current).ResumeAtPropertySurveyId>0)
            {
                var answer = await DisplayAlert("Delete Contract?", "", "   Yes   ", "   No   ");
                if (answer == true)
                {
                    App.net.HeaderRecord = App.data.GetHeader(((App)App.Current).ResumeAtPropertySurveyId);
                    App.data.DeleteContract(App.net.HeaderRecord.udi_cont);
                    App.data.CreateHeaderIndex();
                    CreateList();
                }
            }
        }

        private async void ResendContract()
        {
            if (((App)App.Current).ResumeAtPropertySurveyId > 0)
            {
                var answer = await DisplayAlert("Resend Contract?", "", "   Yes   ", "   No   ");
                if (answer == true)
                {
                    App.net.HeaderRecord = App.data.GetHeader(((App)App.Current).ResumeAtPropertySurveyId);
                    App.net.HeaderRecord.bSent = false;
                    App.data.SaveHeader();
                    App.data.CreateHeaderIndex();
                    CreateList();
                }
            }
        }

        private void OnMileageClicked(object sender, EventArgs e)
        {
            if (App.net.App_Settings.set_ownercode == "")
                Navigation.PushAsync(new SettingsPage(), false);
            else
                Navigation.PushAsync(new MileageList(), false);
        }

        private void OnSettingsClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingsPage(), false);
        }

        private void OnSendClicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new SendPictures());

            
            if (App.net.App_Settings.set_ownercode == "")
                Navigation.PushAsync(new SettingsPage(), false);
            else
            {
                App.net.bCreateIndex = true;
                if (App.net.App_Settings.set_usertype == "SUR" || App.net.App_Settings.set_usertype == "SAT")
                    Navigation.PushAsync(new SendSurveys());
                if (App.net.App_Settings.set_usertype == "FIT")
                    Navigation.PushAsync(new SendFittings());
            }
            
        }

        private void OnReceiveClicked(object sender, EventArgs e)
        {
            if (App.net.App_Settings.set_ownercode == "")
                Navigation.PushAsync(new SettingsPage(), false);
            else
            {
                App.net.bCreateIndex = true;
                if (App.net.App_Settings.set_usertype == "SUR" || App.net.App_Settings.set_usertype == "SAT")
                    Navigation.PushAsync(new ReceiveSurveys());
                if (App.net.App_Settings.set_usertype == "FIT")
                    Navigation.PushAsync(new ReceiveFitting());
            }
        }

        private void OnVanChecks(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VanChecks(), false);
        }

        private void OnUpdate(object sender, EventArgs e)
        {

        }

        private void OnTools(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ToolChecks(), false);
        }

        private void diagnostics_click(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DiagnosticsPage(), false);
        }
    }
}
