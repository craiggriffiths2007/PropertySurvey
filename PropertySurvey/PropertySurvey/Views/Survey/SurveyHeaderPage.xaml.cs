using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
//using Rox;


namespace PropertySurvey
{
    public partial class PropertySurveyItemPage : ContentPage
    {
        public List<string> menu_items_a = new List<string>() { "SUBCONTRACT", "SECURITY SURVEY", "REPUDIATION" };
        public List<string> menu_items_b = new List<string>() { "SUBCONTRACT", "SECURITY SURVEY" };
        public List<string> menu_items_c = new List<string>() { "SUBCONTRACT", "REPUDIATION" };

        int menu_type = 0;

        bool bFirstLoad = true;

        public PropertySurveyItemPage()
        {
            InitializeComponent();
            BindingContext = App.net.HeaderRecord as Header;

            if(App.net.HeaderRecord.iRecordType>0)
            {
                surveyor_area.IsVisible = false;
                fitter_area.IsVisible = true;
            }
            if(timearr.Text=="")
                App.net.HeaderRecord.stimea = DateTime.Now.ToShortTimeString();

            if (App.CurrentApp.HeaderRecord.new_sspare7 !=null && App.CurrentApp.HeaderRecord.new_sspare7.Length > 0)
            {
                gfd_ins.Source = "global_door.png";
                TapGestureRecognizer tapGFD = new TapGestureRecognizer();
                tapGFD.Tapped += OnGFD;
                gfd_ins.GestureRecognizers.Add(tapGFD);
            }

            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += OnGMap;
            gmap.GestureRecognizers.Add(tap);

            TapGestureRecognizer tap2 = new TapGestureRecognizer();
            tap2.Tapped += OnTlightClicked;
            tlight.GestureRecognizers.Add(tap2);

            uc_inciden.Text = ((Header)BindingContext).uc_inceden.Substring(0, 10);

            uc_excess.Text = "£" + ((Header)BindingContext).uc_excess.ToString();

            damage_button.Text = App.net.HeaderRecord.COD_String.Replace(" ", "\n");
            //damage_button2.Text = App.net.HeaderRecord.COD_String.Replace(" ", "\t\n");
            if (App.net.HeaderRecord.COD_String.Count(Char.IsWhiteSpace) > 2)
            {
                damage_button.FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button));
                //damage_button2.FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button));
            }

            if (App.net.HeaderRecord.fname1.Contains("DLG01") ||
            App.net.HeaderRecord.fname1.Contains("DLG02") ||
            App.net.HeaderRecord.fname1.Contains("DLG03") ||
            App.net.HeaderRecord.fname1.Contains("DLG04") ||
            App.net.HeaderRecord.fname1.Contains("DLG05") ||
            App.net.HeaderRecord.fname1.Contains("DLG06") ||
            App.net.HeaderRecord.fname1.Contains("DLG07") ||
            App.net.HeaderRecord.fname1.Contains("DLG08") ||
            App.net.HeaderRecord.fname1.Contains("DLG09"))
            {
                inf_company.Text = App.net.HeaderRecord.sn_name + " (DLG)".Replace("Legal & General", "L n G");// + App.net.HeaderRecord.fname1+")";
            }
            else
            {
                inf_company.Text = App.net.HeaderRecord.sn_name.Replace("Legal & General", "L n G");
            }

            
            //if (App.net.HeaderRecord.fname1.Contains("DLG") || App.net.HeaderRecord.sn_name.Contains("Direct Line") || ((App.net.HeaderRecord.sn_name.Contains("Zurich") || App.net.HeaderRecord.fname1.Contains("ZUR")) && !App.net.HeaderRecord.sn_name.Contains("(comm)")
            //          /*&& !App.net.HeaderRecord.fname1.Contains("ZUR41")
            //            && !App.net.HeaderRecord.fname1.Contains("ZUR42")
            //            && !App.net.HeaderRecord.fname1.Contains("ZUR43")
            //            && !App.net.HeaderRecord.fname1.Contains("ZUR44")
            //            && !App.net.HeaderRecord.fname1.Contains("ZUR99")
            //            && !App.net.HeaderRecord.fname1.Contains("ZUR100")
            //            && !App.net.HeaderRecord.fname1.Contains("ZUR101")
            //            && !App.net.HeaderRecord.fname1.Contains("ZUR102")
            //            && !App.net.HeaderRecord.fname1.Contains("ZUR110")*/))
            //{
            //    App.net.HeaderRecord.readditimage = true;
            //    if (App.net.HeaderRecord.COD_String == "Theft")
            //    {
            //        //menu_items.Add("Security Survey");
            //        App.net.HeaderRecord.ss_bIsSecuritySurvey = 1;
            //    }
            //    else
            //    {
            //        App.net.HeaderRecord.ss_bIsSecuritySurvey = 2;
            //    }
            //}
            

            App.net.HeaderRecord.readditimage = false;
            //App.net.HeaderRecord.ss_bIsSecuritySurvey = 1;
            if (true) //
            {
                if (App.net.HeaderRecord.fname1.Contains("DLG") || App.net.HeaderRecord.sn_name.Contains("Direct Line") || ((App.net.HeaderRecord.sn_name.Contains("Zurich") || App.net.HeaderRecord.fname1.Contains("ZUR")) && !App.net.HeaderRecord.sn_name.Contains("(comm)")
                      /*&& !App.net.HeaderRecord.fname1.Contains("ZUR41")
                        && !App.net.HeaderRecord.fname1.Contains("ZUR42")
                        && !App.net.HeaderRecord.fname1.Contains("ZUR43")
                        && !App.net.HeaderRecord.fname1.Contains("ZUR44")
                        && !App.net.HeaderRecord.fname1.Contains("ZUR99")
                        && !App.net.HeaderRecord.fname1.Contains("ZUR100")
                        && !App.net.HeaderRecord.fname1.Contains("ZUR101")
                        && !App.net.HeaderRecord.fname1.Contains("ZUR102")
                        && !App.net.HeaderRecord.fname1.Contains("ZUR110")*/))
                {
                    App.net.HeaderRecord.readditimage = true;
                    App.net.HeaderRecord.ss_bIsSecuritySurvey = 2;
                    if (App.net.HeaderRecord.COD_String == "Theft")
                        App.net.HeaderRecord.ss_bIsSecuritySurvey = 1;
                    else
                        if (App.net.HeaderRecord.ss_bIsSecuritySurvey == 0)
                        App.net.HeaderRecord.ss_bIsSecuritySurvey = 2;
                }
            }

            if (App.net.HeaderRecord.type == "Complaint")
            {
                Title = "Complaint";
            }
            App.CurrentApp.fitter_header_photo_type = 0;

            //SetMenu();
            UpdateValues();
        }

        private void OnGFD(object sender, EventArgs e)
        {
            //App.net.web_string = App.CurrentApp.HeaderRecord.new_sspare7;
            //Navigation.PushAsync(new WebViewView(), false);
            Device.OpenUri(new Uri(App.CurrentApp.HeaderRecord.new_sspare7));
        }

        private void SetMenu()
        {
            menu_type = 0;
            if (App.net.HeaderRecord.readditimage == true && App.net.HeaderRecord.ss_bIsSecuritySurvey == 1)
                menu_type = 2;

            if (App.net.HeaderRecord.funfinoth!="")
            {
                if (menu_type == 2)
                    menu_type = 1;
                else
                    menu_type = 3;
            }
            if (App.net.HeaderRecord.type == "Complaint")
            {
                menu_options.Icon = "";
            }
            else
            {
                if (menu_type > 0)
                {
                    bool bComplete = true;
                    if (App.net.HeaderRecord.readditimage == true &&
                        App.net.HeaderRecord.ss_bIsSecuritySurvey == 1 &&
                        App.net.HeaderRecord.ss_bIsComplete != 1)
                    {
                        bComplete = false;
                    }
                    if (App.net.HeaderRecord.funfinoth != "" && App.net.HeaderRecord.i_spare1 != 1 && App.net.DoRep() == true)
                        bComplete = false;

                    if (App.net.HeaderRecord.bSubFin == false)
                        bComplete = false;

                    if (bComplete == true || App.net.HeaderRecord.iRecordType>0)
                    {
                        menu_options.Icon = "menu_button_complete.png";
                    }
                    else
                    { 
                        menu_options.Icon = "menu_button.png";
                    }
                }
                else
                {
                    if (App.net.HeaderRecord.bSubFin == false && App.net.HeaderRecord.iRecordType == 0)
                        menu_options.Icon = "subcontract.png";
                    else
                        menu_options.Icon = "subcontract_complete.png";
                }
            }

            switch(menu_type)
            {
                case 1: menu_pick.ItemsSource = menu_items_a; break;
                case 2: menu_pick.ItemsSource = menu_items_b; break;
                case 3: menu_pick.ItemsSource = menu_items_c; break;
            }
        }

        private void UpdateValues()
        {
            items_button.Text = "Items - " + App.net.HeaderRecord.si_numitem.ToString();
            items_button2.Text = "Items - " + App.net.HeaderRecord.si_numitem.ToString();

            if (App.net.HeaderRecord.type == "Complaint")
            {
                special_requirements_icon.Icon = "";
                excess_icon.Icon = "";
                summary_icon.Icon = "";
                health_and_safety_icon.Icon = "";
                menu_options.Icon = "";
            }
            else
            {
                if (App.net.HeaderRecord.bSRFin == true || App.net.HeaderRecord.iRecordType>0)
                    special_requirements_icon.Icon = "special_requirements_done.png";
                else
                    special_requirements_icon.Icon = "special_requirements.png";

                if (App.net.HeaderRecord.bMOPFin == true || App.net.HeaderRecord.iRecordType > 0)
                    excess_icon.Icon = "excess_complete.png";
                else
                    excess_icon.Icon = "excess.png";

                if (App.net.HeaderRecord.bSumFin == true || App.net.HeaderRecord.iRecordType > 0)
                    summary_icon.Icon = "job_summary_complete.png";
                else
                    summary_icon.Icon = "job_summary.png";

                if (App.net.HeaderRecord.bHazFin == true || App.net.HeaderRecord.iRecordType > 0)
                    health_and_safety_icon.Icon = "health_and_safety_complete.png";
                else
                    health_and_safety_icon.Icon = "health_and_safety.png";
            }

            if (App.net.HeaderRecord.udi_jobtext.Length == 0 &&
                (App.net.HeaderRecord.policy_number == null || App.net.HeaderRecord.policy_number.Length == 0) &&
                App.net.HeaderRecord.udi_inst.Length == 0)
            {
                App.net.HeaderRecord.bInfoSeen = true;
            }

            if (App.net.HeaderRecord.type == "Complaint")
            {
                if (App.net.HeaderRecord.front_house_photos > 4 || App.net.HeaderRecord.iRecordType > 0)
                    house_pics.TextColor = Color.DarkGreen;
            }
            else
            {
                if (App.net.HeaderRecord.front_house_photos > 0 || App.net.HeaderRecord.iRecordType > 0)
                    house_pics.TextColor = Color.DarkGreen;
            }
            if (App.net.HeaderRecord.bRepFin == true || App.net.HeaderRecord.iRecordType > 0)
                report_button.TextColor = Color.DarkGreen;

            if (App.net.HeaderRecord.si_numitem > 0 || App.net.HeaderRecord.iRecordType > 0)
                items_button.TextColor = Color.DarkGreen;

            if (/*App.net.HeaderRecord.bInfoSeen == true ||*/ App.net.HeaderRecord.iRecordType > 0)
                InfoButton.TextColor = Color.DarkGreen;
            else
                if (App.net.HeaderRecord.udi_jobtext.Length > 2)
                    InfoButton.TextColor = Color.DarkRed;
                else
                    InfoButton.TextColor = Color.DarkGreen;

            if (App.net.HeaderRecord.stimea == "00/00")
            {
                App.net.HeaderRecord.stimea = "00:00";
            }
            if (App.net.HeaderRecord.stimea != "00:00")
            {
                timearr.Text = "Arrived\n" + App.net.HeaderRecord.stimea;
                timearr.TextColor = Color.DarkGreen;
            }
            else
            {
                timearr.Text = "Set Time\nArrived";
            }

            //if (App.net.HeaderRecord.si_inum == "2")
            //    EnRouteButton.TextColor = Color.DarkRed;
            if (App.net.HeaderRecord.si_inum != "")
                EnRouteButton.TextColor = Color.DarkGreen;

            switch (App.net.HeaderRecord.udi_tlight)
            {
                case 0: tlight.Source = "TLightRed"; break;
                case 1: tlight.Source = "TLightOrange"; break;
                case 2: tlight.Source = "TLightGreen"; break;
            }
            if (App.net.HeaderRecord.type == "Complaint")
            {
                house_pics.Text = "Photo - " + App.net.HeaderRecord.front_house_photos.ToString();
                house_pics2.Text = "Photo - " + App.net.HeaderRecord.front_house_photos.ToString();
            }
            else
            {
                house_pics.Text = "House\nPhoto - " + App.net.HeaderRecord.front_house_photos.ToString();
                house_pics2.Text = "House\nPhoto - " + App.net.HeaderRecord.front_house_photos.ToString();
            }
        }

        protected override bool OnBackButtonPressed()
        {
            string error_text = "";

            if (App.net.HeaderRecord.iRecordType == 0 && App.net.bDoValidation == true)
            {
                if (App.CurrentApp.HeaderRecord.survey_complete == 2 && App.CurrentApp.HeaderRecord.bSumFin == true)
                {
                    if (App.net.HeaderRecord.front_house_photos == 0)
                        error_text = error_text + "Front of house\n";
                }
                else
                {
                    if (App.net.HeaderRecord.type == "Complaint")
                    {
                        if (App.net.HeaderRecord.front_house_photos < 5)
                            error_text = error_text + "5 Photos\n";
                    }
                    else
                    {
                        if (App.net.HeaderRecord.stimea == "00:00")
                            error_text = error_text + "Time arrived\n";

                        if (App.net.HeaderRecord.front_house_photos == 0)
                            error_text = error_text + "Front of house\n";

                        if (App.net.HeaderRecord.bSRFin == false)
                            error_text = error_text + "Special Requirements\n";

                        if (App.net.HeaderRecord.bMOPFin == false)
                            error_text = error_text + "Excess\n";

                        if (App.net.HeaderRecord.bRepFin == false)
                            error_text = error_text + "Report\n";

                        if (App.net.HeaderRecord.bHazFin == false)
                            error_text = error_text + "Health & Safety\n";

                        if (App.net.HeaderRecord.bSubFin == false)
                            error_text = error_text + "Subcontractor\n";

                        if (App.net.HeaderRecord.si_numitem == 0)
                            error_text = error_text + "Items\n";

                        if (App.net.HeaderRecord.bSumFin == false)
                            error_text = error_text + "Job Summary\n";

                        if (App.net.HeaderRecord.readditimage == true &&
                            App.net.HeaderRecord.ss_bIsSecuritySurvey == 1 &&
                            App.net.HeaderRecord.ss_bIsComplete != 1)
                        {
                            error_text = error_text + "Security Survey\n";
                        }
                        if (App.net.HeaderRecord.funfinoth != "" && App.net.HeaderRecord.i_spare1 != 1 && App.net.DoRep() == true)
                            error_text = error_text + "Repudiation\n";
                    }
                }

                if (error_text != "")
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        var response = await Application.Current.MainPage.DisplayAlert("Missing information",
                            "Please complete :\n\n" + error_text + "\n\nClose Anyway?\n", "   Yes   ", "   No   ");
                        if (response)
                        {
                            App.net.HeaderRecord.bDone = false;
                            App.data.SaveHeader();
                            await this.Navigation.PopAsync(false);
                        }
                    });
                }
                else
                {
                    if (App.net.HeaderRecord.bDone == false)
                    {
                        if (App.net.HeaderRecord.iRecordType == 0)
                        {
                            Navigation.InsertPageBefore(new CustomerCareCard(), this);
                        }
                    }
                    if (App.net.HeaderRecord.iRecordType == 0)
                    {
                        Navigation.InsertPageBefore(new MessageToInsurance(), this);
                    }

                    App.net.HeaderRecord.bDone = true;
                    App.data.SaveHeader();
                    this.Navigation.PopAsync(false);
                }
            }
            else
            {
                this.Navigation.PopAsync(false);
            }

            base.OnBackButtonPressed();
            return true;
        }

        /*
        protected override bool OnBackButtonPressed()
        {
            if (App.net.HeaderRecord.bDone == false)
                App.net.HeaderRecord.new_sspare1 = DateTime.Now.ToShortTimeString();

            App.net.HeaderRecord.bDone = true;
            
            if(App.net.HeaderRecord.front_house_photos>0)
                App.net.HeaderRecord.photo_front_of_house = true;

            App.data.SaveHeader();

            Navigation.PopAsync(false);

            return true;
        }*/


        private void OnTlightClicked(object sender, EventArgs e)
        {
            Image lblClicked = (Image)sender;

            if (App.net.HeaderRecord.iRecordType == 0)
            {
                App.net.HeaderRecord.udi_tlight = App.net.HeaderRecord.udi_tlight + 1;

                if (App.net.HeaderRecord.udi_tlight > 2)
                    App.net.HeaderRecord.udi_tlight = 0;

                switch (App.net.HeaderRecord.udi_tlight)
                {
                    case 0: tlight.Source = "TLightRed"; break;
                    case 1: tlight.Source = "TLightOrange"; break;
                    case 2: tlight.Source = "TLightGreen"; break;
                }
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            App.net.CurrentItem = "";
            App.net.camera_vehicle = 0;
            App.net.header_photo_type = 0;
            App.net.fitter_header_photo_type = 0;

            if (App.net.HeaderRecord.stimea.Length > 5)
                App.net.HeaderRecord.stimea = App.net.HeaderRecord.stimea.Substring(0,5);

            if (bFirstLoad == true)
                bFirstLoad = false;
            else
                App.data.SaveHeader();

            SetMenu();
            UpdateValues();
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var HeaderItem = (Header)BindingContext;
            App.data.DeleteHeader(HeaderItem);
            await Navigation.PopAsync(false);
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(false);
        }

        void OnSpeakClicked(object sender, EventArgs e)
        {
            var HeaderItem = (Header)BindingContext;
            //DependencyService.Get<ITextToSpeech>().Speak(PropertySurveyHeader.name + " " + PropertySurveyHeader.Notes);
        }

        private void OnInformation(object sender, EventArgs e)
        {
            App.net.HeaderRecord.bInfoSeen = true;
            Navigation.PushAsync(new SurveyInstructions(), false);
        }

        private void OnItems(object sender, EventArgs e)
        {
            App.data.SaveHeader();
            Navigation.PushAsync(new Items(), false);
        }

        private void OnReport(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Report(), false);
        }

        private void OnPhotos(object sender, EventArgs e)
        {

            App.net.header_photo_type = 1;
            App.net.fitter_header_photo_type = 2;
            //App.net.DB.Headers.DeleteOnSubmit(App.net.HeaderRecord);
            //App.net.DB.Headers.InsertOnSubmit(App.net.HeaderRecord);
            //App.net.DB.SubmitChanges();

            App.net.CurrentItem = "house";

            App.data.SaveHeader();

            Navigation.PushAsync(new Camera(), false);

            /* NEEDS RESTORING
            Navigation.PushAsync(new Camera(), false);
            */
            //Intent intent = new Intent(MediaStore.ActionImageCapture);
            //App._file = new File(App._dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
            //intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(App._file));
            //StartActivityForResult(intent, 0);

            //AcquirePicture();
            //Rox.CameraIos.Init();
            /*
            static readonly File file = new File(Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures), "tmp.jpg");

            var cameraProvider = DependencyService.Get<ICameraProvider>();
            var pictureResult = await cameraProvider.TakePictureAsync();
            */

        }

        private void OnTelephone(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Telephone(), false);
        }

        private async void OnDamage(object sender, EventArgs e)
        {
            if (App.net.HeaderRecord.iRecordType == 0)
            {
                Navigation.InsertPageBefore(new Damage(), this);
                await Navigation.PopAsync(false);
            }
            else
            {
                await Navigation.PushAsync(new Damage(), false);
            }
            //Navigation.PushAsync(new Damage(), false);
        }

        private async void OnSetTimeArrived(object sender, EventArgs e)
        {
            bool bSetTime = false;
            if (App.net.HeaderRecord.stimea != "00:00")
            {
                var answer = await DisplayAlert("Set time arrived to now?", "", "   Yes   ", "   No   ");
                if (answer)
                {
                    bSetTime = true;
                }
            }
            else
            {
                bSetTime = true;
            }

            if(bSetTime==true)
            {
                App.net.HeaderRecord.stimea = DateTime.Now.ToShortTimeString();

                //timearr.Text = "Arrived\t\n" + App.net.HeaderRecord.stimea;
                if (App.net.HeaderRecord.stimea != "00:00")
                {
                    timearr.Text = "Arrived\n" + App.net.HeaderRecord.stimea;
                    timearr.TextColor = Color.DarkGreen;
                }
                else
                {
                    timearr.Text = "Arrived";
                }
            }
        }

        private void OnSetOnRoute(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EnRoute(), false);
        }

        private void OnMenuClicked(object sender, EventArgs e)
        {
            if (App.net.HeaderRecord.type != "Complaint")
            {
                if (menu_type == 0)
                {
                    Navigation.PushAsync(new Subcontract(), false);
                }
                else
                {
                    menu_pick.Focus();
                }
            }
        }

        private void OnMenuChanged(object sender, EventArgs e)
        {
            if(menu_pick.SelectedIndex>-1)
            switch (menu_pick.Items[menu_pick.SelectedIndex])
            {
                    /*case 0: Navigation.PushAsync(new SpecialRequirements(), false);break;
                    case 1: Navigation.PushAsync(new Excess(), false);break;
                    case 2: Navigation.PushAsync(new JobSummary(), false);break;
                    case 3: Navigation.PushAsync(new Hnds(), false);break;{ "SUBCONTRACT", "SECURITY SURVEY", "REPUDIATION" };*/
                case "SUBCONTRACT": Navigation.PushAsync(new Subcontract(), false);break;
                case "SECURITY SURVEY": Navigation.PushAsync(new SecuritySurvey(), false);break;
                case "REPUDIATION": Navigation.PushAsync(new Repudiation(), false); break;
                }
        }

        private void OnGMap(object sender, EventArgs e)
        {
            App.net.web_string = "http://maps.google.com/maps?q=" + App.net.HeaderRecord.uc_add1 + "+" + App.net.HeaderRecord.uc_add2 + "+" +
                                                                App.net.HeaderRecord.uc_add3 + "+" + App.net.HeaderRecord.uc_add4 + "+" + App.net.HeaderRecord.uc_postcode + "+UK" + "&output=html";

            Device.OpenUri(new Uri(App.net.web_string));

            //Navigation.PushAsync(new WebViewView(), false);
        }

        private void OnSpecialRequirementsClicked(object sender, EventArgs e)
        {
            if (App.net.HeaderRecord.type != "Complaint")
            {
                Navigation.PushAsync(new SpecialRequirements(), false);
            }
        }

        private void OnHealthAndSafetyClicked(object sender, EventArgs e)
        {
            if (App.net.HeaderRecord.type != "Complaint")
            {
                Navigation.PushAsync(new Hnds(), false);
            }
        }

        private void OnExcessClicked(object sender, EventArgs e)
        {
            if (App.net.HeaderRecord.type != "Complaint")
            {
                Navigation.PushAsync(new Excess(), false);
            }
        }

        private void OnJobSummaryClicked(object sender, EventArgs e)
        {
            if (App.net.HeaderRecord.type != "Complaint")
            {
                Navigation.PushAsync(new JobSummary(), false);
            }
        }

        private void OnSubcontractClicked(object sender, EventArgs e)
        {
            if (App.net.HeaderRecord.type != "Complaint")
            {
                Navigation.PushAsync(new Subcontract(), false);
            }
        }
    }
}
