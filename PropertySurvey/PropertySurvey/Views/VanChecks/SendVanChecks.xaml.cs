using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendVanChecks : ContentPage
    {
        StringBuilder postData = new StringBuilder(128000);
        StringBuilder FittingImage = new StringBuilder(128000);
        List<string> checks_to_send = new List<string>();
        List<string> images_to_send = new List<string>();

        int total_checks = 0;
        int current_check = 0;

        string error_code = "0";
        string current_4d_code;

        int total_images = 0;
        int current_image = 0;

        int total_items_to_send = 0;
        int current_item_sending = 0;

        XDocument id_4d;

        bool bSentComplete = false;

        string sendResponse = "";

        List<string> fileNames;

        bool bEnd = false;

        public SendVanChecks()
        {
            InitializeComponent();

            Device.BeginInvokeOnMainThread(SendChecks);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DeviceDisplay.KeepScreenOn = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            DeviceDisplay.KeepScreenOn = false;
        }

        public void SendChecks()
        {
            List<VanChecksHeader> checks = App.data.GetUnsentChecks();

            foreach (var item in checks)
            {
                checks_to_send.Add(item.unique_id);
            }

            total_checks = checks_to_send.Count();
            current_check = 0;
            act_ind.IsRunning = true;

            if (total_checks > 0)
            {
                AddChecks();
                CreateImagesList();
                total_items_to_send = images_to_send.Count();
                current_item_sending = 0;
                //SendNextPicture();
                StartSending();
            }
            else
            {
                Navigation.PopAsync(false);
            }
        }

        private void StartSending()
        {
            Uri thisuri = new Uri("http://192.168.137.15:7293/" + "/WM7Communication/SendVanCheck");
            HttpHelper helper = new HttpHelper(thisuri, "POST",
            new KeyValuePair<string, string>("ContractFile", "<ContractFile>" + FittingImage.ToString() + "</ContractFile>"));
            helper.ResponseComplete += new HttpResponseCompleteEventHandler(this.CommandComplete);
            helper.Execute();
        }

        private void CreateImagesList()
        {
            images_to_send = new List<string>();

            fileNames = App.files.GetFileList("Photos/VC", "*.*");

            foreach (var item in fileNames)
            {
                if (item.Substring(0, 8) == checks_to_send[current_check].Substring(0, 8))
                {
                    images_to_send.Add("Photos/VC/" + item);
                }
            }

            fileNames = App.files.GetFileList("Signatures/VC", "*.*");

            foreach (var item in fileNames)
            {
                if (item.Substring(0, 8) == checks_to_send[current_check].Substring(0, 8))
                {
                    images_to_send.Add("Signatures/VC/" + item);
                }
            }

            total_images = images_to_send.Count();
            current_image = 0;
        }

        public void SendNextPicture()
        {
            byte[] data;
            string image_type = "";
            string bLastImage = "no";

            string this_image_filename = images_to_send[current_image];
            string just_file_name;

            just_file_name = this_image_filename.Replace("Photos/VC/", "");
            just_file_name = just_file_name.Replace("Drawings/", "");
            just_file_name = just_file_name.Replace("Signatures/VC/", "");

            current_image++;

            if (current_image == total_images)
            {
                bSentComplete = true;

                bLastImage = "yes";
            }

            StringBuilder localpostData = new StringBuilder(128000);

            string code4d;
            string codepda;
            string codetype;
            string id_4d_s = "";

            //if(false)
            foreach (var word in id_4d.Element("complete").Elements())
            {
                foreach (var word2 in word.Element("recordcodes").Elements("code"))
                {
                    code4d = (string)word2.Element("code_4d");
                    codepda = (string)word2.Element("code_pda");
                    codepda = codepda.PadLeft(8, '0');
                    codetype = (string)word2.Element("code_type");
                    if (codepda == just_file_name.Substring(26, 8))
                    {
                        if(id_4d_s == "")
                            id_4d_s = code4d;
                        //break;
                    }
                }
            }

            try
            {
                data = App.files.LoadBinary(this_image_filename);

                XDocument localTree = new XDocument(
                new XElement("Image",
                new XElement("UserType", App.net.App_Settings.set_usertype),
                new XElement("JobType", "F"),
                new XElement("UserCode", App.net.App_Settings.set_ownercode),
                new XElement("LastImage", bLastImage),
                new XElement("id_4d", id_4d_s),
                new XElement("Filename", just_file_name),
                new XElement("data", System.Convert.ToBase64String(data))));

                localpostData.Append(localTree.ToString());
            }
            catch (Exception e)
            {
                XDocument localTree = new XDocument(
                new XElement("Image",
                new XElement("UserType", App.net.App_Settings.set_usertype),
                new XElement("JobType", "F"),
                new XElement("UserCode", App.net.App_Settings.set_ownercode),
                new XElement("LastImage", bLastImage),
                new XElement("id_4d", id_4d_s),
                new XElement("Filename", just_file_name),
                new XElement("data", "error")));

                localpostData.Append(localTree.ToString());
            }

            Uri thisuri = new Uri("http://192.168.137.15:7293/" + "/WM7Communication/SendVanCheckImage");
            HttpHelper helper = new HttpHelper(thisuri, "POST",
            new KeyValuePair<string, string>("ContractFile", "<ContractFile>" + localpostData.ToString() + "</ContractFile>"));
            helper.ResponseComplete += new HttpResponseCompleteEventHandler(this.CommandComplete);
            helper.Execute();
        }

        private void CommandComplete(HttpResponseCompleteEventArgs e)
        {
            sendResponse = e.Response;

            bool bCancelSend = false;

            string reg = "";

            if (current_image == 0)
            {
                if (bEnd == true)
                {
                    //NavigationService.Navigate(new Uri("/VanChecks/VanChecks.xaml", UriKind.Relative));
                }
                else
                {
                    id_4d = XDocument.Parse(sendResponse);

                    App.net.VanChecksHeader = null;

                    string code4d;
                    string codetype;
                    string codepda;
                    foreach (var word in id_4d.Element("complete").Elements())
                    {
                        error_code = (string)word.Element("errorcode");
                        foreach (var word2 in word.Element("recordcodes").Elements("code"))
                        {
                            code4d = (string)word2.Element("code_4d");
                            codepda = (string)word2.Element("code_pda");
                            codetype = (string)word2.Element("code_type");

                            current_4d_code = code4d;

                            /*
                            switch (codetype)
                            {
                                case "VAN": App.data.SetVanCheckVanSent(Convert.ToInt32(codepda), code4d); break;
                                case "CAR": App.data.SetVanCheckCarSent(Convert.ToInt32(codepda), code4d); break;
                                case "HGV": App.data.SetVanCheckDeliveryVanSent(Convert.ToInt32(codepda), code4d); break;
                                case "HGVDEL": App.data.SetVanCheckDeliverySent(Convert.ToInt32(codepda), code4d); break;
                            }
                            */
                        }
                    }

                    if (error_code == "1")
                    {
                        //MessageBox.Show("One or more vehicle registrations were not found.");
                        //NavigationService.Navigate(new Uri("/VanChecks/VanChecks.xaml", UriKind.Relative));
                        bCancelSend = true;
                    }
                }
            }

            if (bCancelSend == false)
            {
                if (current_image < total_images)
                {
                    current_item_sending++;
                    sending_progress.ProgressTo((1.0f / total_items_to_send) * (float)current_item_sending, 250, Easing.Linear);

                    SendNextPicture();
                }
                else
                {
                    try
                    {
                        //id_4d = XDocument.Parse(sendResponse);
                        //error_code = "1";
                        //foreach (var word in id_4d.Element("complete").Elements())
                        //{
                        //    error_code = (string)word.Element("errorcode");
                        //}

                        if (true)//(error_code == "0")
                        {
                            //if (bSentComplete == true)
                            //{
                            App.data.SetVanCheckHeaderSent(checks_to_send[current_check]);
                            //}
                        }

                        //if (error_code == "1")
                        //{
                            //MessageBox.Show("One or more vehicle registrations were not found.");
                            //NavigationService.Navigate(new Uri("/VanChecks/VanChecks.xaml", UriKind.Relative));
                        //}
                        current_check++;
                        if (current_check < total_checks)
                        {
                            bEnd = false;

                            AddChecks();
                            CreateImagesList();
                            total_items_to_send = images_to_send.Count();
                            current_item_sending = 0;
                            StartSending();
                        }
                        else
                        {
                            Device.BeginInvokeOnMainThread(CompleteDownload);
                            // navigare back
                            //NavigationService.Navigate(new Uri("/VanChecks/VanChecks.xaml", UriKind.Relative));
                        }
                    }
                    catch(Exception e2)
                    {
                        sendResponse = e2.ToString();
                        Device.BeginInvokeOnMainThread(CompleteDownload);
                    }
                }
            }

        }

        private void CompleteDownload()
        {
            if (sendResponse == "nointernet")
            {
                complete_label.Text = "no Internet Connection";
                complete_label.IsVisible = true;
            }
            else
            {
                Navigation.PopAsync(false);
            }
        }

        private void AddChecks()
        {
            App.data.LoadVanCheck(checks_to_send[current_check]);

            XDocument localTree = new XDocument(
            new XElement("Record",
            new XElement("RecordType", "Header"),
            new XElement("UniqueID", App.net.VanChecksHeader.unique_id),
            new XElement("Date", App.net.VanChecksHeader.check_date),
            new XElement("Name", App.net.VanChecksHeader.name),
            new XElement("PDACode", App.net.App_Settings.set_ownercode),
            new XElement("BranchCode", App.net.App_Settings.set_branchcode),
            new XElement("daily_check_type", "Yes"),

            new XElement("end", "end")));

            FittingImage.Append(localTree.ToString());

            AddDeliveryVehicleCheckList();
            AddDeliveryVanVehicleCheckList();
            AddWeeklyVanCheckSheet();
            AddCarPanelSheet();
        }

        private void AddDeliveryVehicleCheckList()
        {
            foreach (var check in App.net.DeliveryVehicleCheckLists)
            {
                XDocument localTree = new XDocument(
                new XElement("Record",
                new XElement("RecordType", "DeliveryVehicleCheckList"),

                new XElement("registration", check.vehicle_registration),
                new XElement("mileage", check.mileage),

                new XElement("check_date", check.date),

                new XElement("destination", check.destination),

                new XElement("item_no", check.item_no.ToString()),
                new XElement("item_no_4d", check.item_no_4D),
                new XElement("driver", check.driver_printed),
                new XElement("checker", check.checked_printed),

                new XElement("name", check.name),

                new XElement("pressure_passenger_front", check.passenger_front_pressure_s),
                new XElement("pressure_passenger_rear", check.passenger_rear_pressure_s),
                new XElement("pressure_driver_front", check.driver_front_pressure_s),
                new XElement("pressure_driver_rear", check.driver_rear_pressure_s),
                new XElement("spare_tyre_pressure", check.spare_tyre_pressure_s),

                new XElement("national_tyres_card", YesNoString(check.national_tyres_card)),
                new XElement("fmg_support_sticker", YesNoString(check.fmg_support_sticker)),
                new XElement("fuel_card", YesNoString(check.fuel_card)),
                new XElement("shell_fuel_card", YesNoString(check.spare_i_1)),
                new XElement("clean_external", YesNoString(check.clean_external)),
                new XElement("clean_internal", YesNoString(check.clean_internal)),
                new XElement("fan_belt", YesNoString(check.fan_belt)),

                new XElement("no_smoking_sticker", YesNoString(check.no_smoking_sticker)),
                new XElement("spray_suppression", YesNoString(check.spray_suppression)),
                new XElement("mirrors", YesNoString(check.mirrors)),
                new XElement("lights", YesNoString(check.lights)),
                new XElement("reflectors", YesNoString(check.reflectors)),
                new XElement("indicators", YesNoString(check.inducators)),
                new XElement("excessive_exhaust_smoke", YesNoString(check.excessive_exhaust_smoke)),

                new XElement("fire_extinguisher", YesNoString(check.fire_extinguisher)),
                new XElement("first_aid_box", YesNoString(check.first_aid_box)),
                new XElement("horn", YesNoString(check.horn)),
                new XElement("oil_and_water_checked", YesNoString(check.oil_and_water_checked)),
                new XElement("accident_pack", YesNoString(check.accident_pack)),
                new XElement("portable_lighting", YesNoString(check.portable_lighting)),

                new XElement("ad_blue_level_check", YesNoString(check.ad_blue_level_check)),
                new XElement("racks_and_poles", YesNoString(check.racks_and_poles)),
                new XElement("ratchet_straps", YesNoString(check.ratchet_straps)),

                new XElement("service_due_sticker", YesNoString(check.service_due_sticker)),
                new XElement("spare_oil", YesNoString(check.spare_oil)),
                new XElement("coolant_anti_freez", YesNoString(check.coolant_anti_freez)),
                new XElement("tyre_pressure", YesNoString(check.tyre_pressure)),
                new XElement("van_height_sticker", YesNoString(check.van_height_sticker)),
                new XElement("van_locks", YesNoString(check.van_locks)),
                new XElement("wheel_nut_check_sticker", YesNoString(check.wheel_nut_check_sticker)),
                new XElement("windscreen_washer", YesNoString(check.windscreen_washer)),
                new XElement("fuel_oil_leaks", YesNoString(check.fuel_oil_leaks)),
                new XElement("battery_security_condition", YesNoString(check.battery_security_condition)),
                new XElement("tyres_and_wheel_fixing", YesNoString(check.tyres_and_wheel_fixing)),
                new XElement("spray_suppression", YesNoString(check.spray_suppression)),
                new XElement("steering", YesNoString(check.steering)),
                new XElement("security_of_load", YesNoString(check.security_of_load)),
                new XElement("mirrors", YesNoString(check.mirrors)),
                new XElement("lights", YesNoString(check.lights)),

                new XElement("wipers", YesNoString(check.wipers)),
                new XElement("washers", YesNoString(check.washers)),
                new XElement("horn_comp", YesNoString(check.horn_comp)),
                new XElement("excessive_exhaust_smoke", YesNoString(check.excessive_exhaust_smoke)),
                new XElement("brakes", YesNoString(check.brakes)),
                new XElement("security_of_body", YesNoString(check.security_of_body)),
                new XElement("markers", YesNoString(check.markers)),
                new XElement("glass_windscreen", YesNoString(check.glass_windscreen)),

                new XElement("receipt_book", YesNoString(check.receipt_book)),
                new XElement("keys_for_branches", YesNoString(check.keys_for_branches_sat)),
                new XElement("pda_phone_accident_pack", YesNoString(check.pda_phone_accident_pack)),
                new XElement("trade_invoices", YesNoString(check.trade_invoices)),
                new XElement("blue_bags", YesNoString(check.blue_bags)),
                new XElement("delivery_lists", YesNoString(check.delivery_lists)),
                new XElement("collection_lists", YesNoString(check.collection_lists)),
                new XElement("trade_delivery_notes", YesNoString(check.trade_delivery_notes)),

                new XElement("storage_area", check.spare_s_2),

                new XElement("damage_passenger", check.damage_pass),
                new XElement("damage_driver", check.damage_driver),
                new XElement("damage_front", check.damage_front),
                new XElement("damage_back", check.damage_back),

                new XElement("report_defects", check.report_defects),

                new XElement("not_complete_reason", check.not_complete_reason),

                //new XElement("national_tyres_card", NAString(check.national_tyres_card)),
                new XElement("naFSS", NAString(check.fmg_support_sticker)),
                new XElement("naUFC", NAString(check.fuel_card)),
                new XElement("naSFC", NAString(check.spare_i_1)),
                new XElement("naCE", NAString(check.clean_external)),
                new XElement("naCI", NAString(check.clean_internal)),

                new XElement("naSBW", NAString(check.security_of_body)),
                new XElement("naRB", NAString(check.receipt_book)),
                new XElement("naPPA", NAString(check.pda_phone_accident_pack)),
                new XElement("naTI", NAString(check.trade_invoices)),
                new XElement("naTDN", NAString(check.trade_delivery_notes)),

                new XElement("naBB", NAString(check.blue_bags)),
                new XElement("naCL", NAString(check.collection_lists)),
                new XElement("naDL", NAString(check.delivery_lists)),
                new XElement("naIn", NAString(check.inducators)),
                new XElement("naKBS", NAString(check.keys_for_branches_sat)),

                //new XElement("fan_belt", NAString(check.fan_belt)),

                new XElement("naNSS", NAString(check.no_smoking_sticker)),
                new XElement("naSS", NAString(check.spray_suppression)),
                //new XElement("mirrors", NAString(check.mirrors)),
                //new XElement("lights", NAString(check.lights)),
                new XElement("naRe", NAString(check.reflectors)),
                new XElement("naI", NAString(check.inducators)),
                new XElement("naEES", NAString(check.excessive_exhaust_smoke)),

                new XElement("naFE", NAString(check.fire_extinguisher)),
                new XElement("naFAB", NAString(check.first_aid_box)),
                new XElement("naH", NAString(check.horn)),
                new XElement("naOWC", NAString(check.oil_and_water_checked)),
                new XElement("naAP", NAString(check.accident_pack)),
                new XElement("naPL", NAString(check.portable_lighting)),

                new XElement("naABLC", NAString(check.ad_blue_level_check)),
                new XElement("naRP", NAString(check.racks_and_poles)),
                new XElement("naRS", NAString(check.ratchet_straps)),

                new XElement("naSDS", NAString(check.service_due_sticker)),
                new XElement("naSO", NAString(check.spare_oil)),
                new XElement("naAFM", NAString(check.coolant_anti_freez)),
                new XElement("naTP", NAString(check.tyre_pressure)),
                new XElement("naVHS", NAString(check.van_height_sticker)),
                new XElement("naVL", NAString(check.van_locks)),
                new XElement("naWNCS", NAString(check.wheel_nut_check_sticker)),
                new XElement("naWW", NAString(check.windscreen_washer)),
                new XElement("naFOL", NAString(check.fuel_oil_leaks)),
                new XElement("naBSC", NAString(check.battery_security_condition)),
                new XElement("naTWF", NAString(check.tyres_and_wheel_fixing)),
                new XElement("naSS", NAString(check.spray_suppression)),
                new XElement("naS", NAString(check.steering)),
                new XElement("naSL", NAString(check.security_of_load)),
                new XElement("naM", NAString(check.mirrors)),
                new XElement("naL", NAString(check.lights)),

                new XElement("naWi", NAString(check.wipers)),
                new XElement("naWa", NAString(check.washers)),
                //new XElement("horn_comp", NAString(check.horn_comp)),
                //new XElement("excessive_exhaust_smoke", NAString(check.excessive_exhaust_smoke)),
                new XElement("naBr", NAString(check.brakes)),
                //new XElement("security_of_body", NAString(check.security_of_body)),
                new XElement("naMa", NAString(check.markers)),
                new XElement("naGWC", NAString(check.glass_windscreen)),

                new XElement("receipt_book", NAString(check.receipt_book)),
                new XElement("keys_for_branches", NAString(check.keys_for_branches_sat)),
                new XElement("pda_phone_accident_pack", NAString(check.pda_phone_accident_pack)),
                new XElement("trade_invoices", NAString(check.trade_invoices)),
                new XElement("blue_bags", NAString(check.blue_bags)),
                new XElement("delivery_lists", NAString(check.delivery_lists)),
                new XElement("collection_lists", NAString(check.collection_lists)),
                new XElement("trade_delivery_notes", NAString(check.trade_delivery_notes)),

                new XElement("daily_check_type", "No"),

                new XElement("end", "end")));

                FittingImage.Append(localTree.ToString());
            }
        }

        private void AddDeliveryVanVehicleCheckList()
        {
            foreach (var check in App.net.DeliveryVanVehicleCheckLists)
            {
                XDocument localTree = new XDocument(
                new XElement("Record",
                new XElement("RecordType", "DeliveryVanVehicleCheckList"),
                new XElement("destination", check.destination),
                new XElement("check_date", check.date),
                new XElement("registration", check.vehicle_registration),
                new XElement("mileage", check.mileage),
                new XElement("item_no", check.item_no.ToString()),
                new XElement("item_no_4d", check.item_no_4D),
                new XElement("driver", check.driver_printed),
                new XElement("checker", check.checked_printed),
                new XElement("name", check.name),
                new XElement("pressure_passenger_front", check.passenger_front_pressure_s),
                new XElement("pressure_passenger_rear", check.passenger_rear_pressure_s),
                new XElement("pressure_driver_front", check.driver_front_pressure_s),
                new XElement("pressure_driver_rear", check.driver_rear_pressure_s),
                new XElement("spare_tyre_pressure", check.spare_tyre_pressure_s),
                new XElement("ats_card", check.ats_card_s),
                new XElement("bodywork_check", check.bodywork_check_s),
                new XElement("breakdown_card", check.breakdown_card_s),
                new XElement("clean_external", check.clean_external_s),
                new XElement("clean_internal", check.clean_internal_s),
                new XElement("fan_belt", check.fan_belt_s),
                new XElement("fire_extinguisher", check.fire_extinguisher_s),
                new XElement("first_aid_box", check.first_aid_box_s),
                new XElement("fuel_card", check.fuel_card_s),
                new XElement("shell_fuel_card", check.spare_s_1),
                new XElement("horn", check.horn_s),
                new XElement("jack", check.jack_s),
                new XElement("jump_leads", check.jump_leads_s),
                new XElement("keys_for_branches", check.keys_for_branches_s),
                new XElement("lights_inducators", check.lights_inducators_s),
                new XElement("oil_water_checked", check.oil_water_checked_s),
                new XElement("racks_poles", check.racks_poles_s),
                new XElement("ratchet_straps", check.ratchet_straps_s),
                new XElement("receipt_book", check.receipt_book_s),
                new XElement("bump_hats", check.bump_hats_s),
                new XElement("service_due_sticker", check.service_due_sticker_s),
                new XElement("spanners_for_rack_removal", check.spanners_for_rack_removal_s),
                new XElement("spare_oil", check.spare_oil_s),
                new XElement("coolant_anti_freeze_mix", check.coolant_anti_freeze_mix_s),
                new XElement("spare_wheel", check.spare_wheel_s),
                new XElement("tow_ropes", check.tow_ropes_s),
                new XElement("tyre_pressure", check.tyre_pressure_s),
                new XElement("van_height_sticker", check.van_height_sticker_s),
                new XElement("van_locks", check.van_locks_s),
                new XElement("wheel_nut_check_sticker", check.wheel_nut_check_sticker_s),
                new XElement("wheelbrace", check.wheelbrace_s),
                new XElement("windscreen_washer", check.windscreen_washer_s),
                new XElement("pda_phone_accident_pack", check.pda_phone_accident_pack_s),
                new XElement("branch_keys", check.branch_keys_s),
                new XElement("storage_area", check.spare_s_2),
                new XElement("damage_passenger", check.damage_pass),
                new XElement("damage_driver", check.damage_driver),
                new XElement("damage_front", check.damage_front),
                new XElement("damage_back", check.damage_back),
                new XElement("not_complete_reason", check.not_complete_reason),
                new XElement("end", "end")));

                FittingImage.Append(localTree.ToString());
            }
        }

        private void AddWeeklyVanCheckSheet()
        {
            foreach (var check in App.net.WeeklyVanCheckSheets)
            {
                XDocument localTree = new XDocument(
                new XElement("Record",
                new XElement("RecordType", "VanCheckSheet"),

                new XElement("check_date", check.date),

                new XElement("registration", check.vehicle_reg),
                new XElement("mileage", check.mileage),
                new XElement("branch", check.branch),
                new XElement("item_no", check.item_no.ToString()),
                new XElement("item_no_4d", check.item_no_4D),
                new XElement("driver", check.driver_printed),
                new XElement("checker", check.checked_printed),

                new XElement("pressure_passenger_front", check.passenger_front_pressure_s),
                new XElement("pressure_passenger_rear", check.passenger_rear_pressure_s),
                new XElement("pressure_driver_front", check.driver_front_pressure_s),
                new XElement("pressure_driver_rear", check.driver_rear_pressure_s),
                new XElement("spare_tyre_pressure", check.spare_tyre_pressure_s),

                new XElement("circuit_breaker", check.circuit_breaker_s),
                new XElement("power_breaker", check.power_breaker_s),
                new XElement("hammer_drill", check.hammer_drill_s),
                new XElement("ordinary_drill", check.ordinary_drill_s),
                new XElement("cordless_drill", check.cordless_drill_s),
                new XElement("spare_battery_and_charger", check.spare_battery_and_charger_s),
                new XElement("circular_saw", check.circular_saw_s),
                new XElement("jig_saw", check.jig_saw_s),
                new XElement("planer_check_blade", check.planer_check_blade_s),
                new XElement("heat_gun", check.heat_gun_s),
                new XElement("sander", check.sander_s),
                new XElement("hoover", check.hoover_s),
                new XElement("halogen_lamp", check.halogen_lamp_s),
                new XElement("extension_lead", check.extension_lead_s),
                new XElement("router", check.router_s),
                new XElement("LetterboxJig", check.new_sspare1),
                new XElement("industrial_ladders", check.industrial_ladders_s),
                new XElement("ladder_clamps", check.ladder_clamps_s),
                new XElement("step_ladders", check.step_ladders_s),
                new XElement("ladder_stopper", check.ladder_stopper_s),
                new XElement("philips_bit", check.philips_bit_s),
                new XElement("screw_box", check.screw_box_s),
                new XElement("tresles_x2", check.tresles_x2_s),
                new XElement("torch_working", check.torch_working_s),
                new XElement("ratchett_straps_x4", check.ratchett_straps_x4_s),
                new XElement("spare_wheel", check.spare_wheel_s),
                new XElement("blue_external_dust_sheet", check.blue_external_dust_sheet_s),
                new XElement("internal_dust_sheets_x3", check.internal_dust_sheets_x3_s),
                new XElement("brush_and_shovel", check.brush_and_shovel_s),
                new XElement("cleaner_bottle", check.cleaner_bottle_s),
                new XElement("ecloth", check.ecloth_s),
                new XElement("mastic_guns", check.mastic_guns_s),
                new XElement("glass_suckers", check.glass_suckers_s),
                new XElement("safety_helmets", check.safety_helmets_s),

                new XElement("safety_helmets_num", check.safety_helmets.ToString()),
                new XElement("gloves_num", check.gloves.ToString()),
                new XElement("wrist_guards_num", check.wrist_guards.ToString()),
                new XElement("goggles_num", check.goggles.ToString()),
                new XElement("ear_defenders_num", check.ear_defenders.ToString()),
                new XElement("dust_masks_num", check.dust_masks.ToString()),

                new XElement("step_ladders_num", check.step_ladders.ToString()),

                new XElement("helmet_manufacture_date", check.ManufactureDateOnHelmet),

                new XElement("gloves", check.gloves_s),
                new XElement("wrist_guards", check.wrist_guards_s),
                new XElement("goggles", check.goggles_s),
                new XElement("ear_defenders", check.ear_defenders_s),
                new XElement("dust_masks", check.dust_masks_s),
                new XElement("customer_care_cards", check.customer_care_cards_s),
                new XElement("completion_forms", check.completion_forms_s),
                new XElement("mandate_forms", check.mandate_forms_s),
                new XElement("quality_manuals", check.quality_manuals_s),
                new XElement("stapler", check.stapler_s),
                new XElement("worksheets", check.worksheets_s),
                new XElement("plasters", check.plasters_s),
                new XElement("dressing", check.dressing_s),
                new XElement("eyewashers", check.eyewashers_s),
                new XElement("steri_wipes", check.steri_wipes_s),
                new XElement("bag", check.bag_s),
                new XElement("flexi_meter", check.flexi_meter_s),
                new XElement("merlin_low_e_detector", check.merlin_low_e_detector_s),
                new XElement("cabin_condition", check.cabin_condition_s),
                new XElement("national_tyres_card", check.national_tyres_card_s),
                new XElement("breakdown_card", check.breakdown_card_s),
                new XElement("fuel_card", check.fuel_card_s),
                new XElement("shell_fuel_card", check.spare_s_1),
                new XElement("shell_points_card", check.shell_points_card_s),
                new XElement("fire_extinguisher", check.fire_extinguisher_s),
                new XElement("jack", check.jack_s),
                new XElement("wheelbrace", check.wheelbrace_s),
                new XElement("jump_leads", check.jump_leads_s),
                new XElement("fan_belt", check.fan_belt_s),
                new XElement("tow_ropes", check.tow_ropes_s),
                new XElement("spare_oil", check.spare_oil_s),
                new XElement("coolant_anti_freeze", check.coolant_anti_freeze_s),
                new XElement("van_height_sticker", check.van_height_sticker_s),
                new XElement("wheel_nut_check_sticker", check.wheel_nut_check_sticker_s),
                new XElement("no_smoking_sticker", check.no_smoking_sticker_s),
                new XElement("racks_and_poles", check.racks_and_poles_s),
                new XElement("tyre_conditions", check.tyre_conditions_s),
                new XElement("van_locks", check.van_locks_s),
                new XElement("oil_and_water_checked", check.oil_and_water_checked_s),
                new XElement("hows_my_driving_sticker", check.hows_my_driving_sticker_s),
                new XElement("windscreen_good_condition", check.windscreen_good_contidion_s),

                new XElement("pda_setup_date", check.PDASetupDate),

                new XElement("accident_pack_on_pda", check.accident_pack_on_pda_s),
                new XElement("hi_vis_vests", check.hi_vis_vests_s),
                new XElement("grinder", check.grinder_s),
                new XElement("fitters_own_power_tools_ladders_on_van", check.fitters_own_power_tools_ladders_on_van.ToString()),

                new XElement("damage_passenger", check.damage_pass),
                new XElement("damage_driver", check.damage_driver),
                new XElement("damage_front", check.damage_front),
                new XElement("damage_back", check.damage_back),

                new XElement("storage_area", check.spare_s_2),

                new XElement("marks_out_of_10", check.marks_out_of_10),

                new XElement("not_complete_reason", check.not_complete_reason),

                new XElement("helmet_manufacture_date2", check.ManufactureDateOnHelmet2),
                new XElement("helmet_manufacture_date3", check.ManufactureDateOnHelmet3),
                new XElement("helmet_manufacture_date4", check.ManufactureDateOnHelmet4),
                new XElement("helmet_manufacture_date5", check.ManufactureDateOnHelmet5),
                new XElement("helmet_manufacture_date6", check.ManufactureDateOnHelmet6),
                new XElement("helmet_manufacture_date7", check.ManufactureDateOnHelmet7),
                new XElement("helmet_manufacture_date8", check.ManufactureDateOnHelmet8),
                new XElement("helmet_manufacture_date9", check.ManufactureDateOnHelmet9),
                new XElement("helmet_manufacture_date10", check.ManufactureDateOnHelmet10),

                new XElement("end", "end")));

                FittingImage.Append(localTree.ToString());
            }
        }

        private void AddCarPanelSheet()
        {
            foreach (var check in App.net.CarPanelSheets)
            {
                XDocument localTree = new XDocument(
                new XElement("Record",
                new XElement("RecordType", "CarPanelSheet"),

                new XElement("registration", check.vehicle_reg),
                new XElement("mileage", check.mileage),

                new XElement("check_date", check.date),

                new XElement("check_no", check.item_no.ToString()),
                new XElement("check_no_4d", check.item_no_4D),
                new XElement("driver", check.driver_printed),
                new XElement("checker", check.checked_printed),

                new XElement("pressure_passenger_front", check.pressure_passenger_front_s),
                new XElement("pressure_passenger_rear", check.pressure_passenger_rear_s),
                new XElement("pressure_driver_front", check.pressure_driver_front_s),
                new XElement("pressure_driver_rear", check.pressure_driver_rear_s),
                new XElement("spare_tyre_pressure", check.spare_tyre_pressure_s),

                new XElement("fuel_card", check.fuel_card_s),
                new XElement("shell_fuel_card", check.shell_fuel_card_s),
                new XElement("shell_points_card", check.shell_points_card_s),
                new XElement("interior_clean", check.interior_clean_s),
                new XElement("oil_level", check.oil_level_s),
                new XElement("water_level", check.water_level_s),
                new XElement("windscreen_wash", check.windscreen_wash_s),
                new XElement("spare_wheel", check.spare_wheel_s),
                new XElement("jack", check.jack_s),
                new XElement("wheel_brace", check.wheel_brace_s),
                new XElement("tools", check.tools_s),
                new XElement("tyre_condition", check.tyre_condition_s),

                new XElement("damage_passenger", check.damage_pass),
                new XElement("damage_driver", check.damage_driver),
                new XElement("damage_front", check.damage_front),
                new XElement("damage_back", check.damage_back),

                new XElement("not_complete_reason", check.not_complete_reason),

                new XElement("end", "end")));

                FittingImage.Append(localTree.ToString());
            }
        }

        private string YesNoString(int yorn)
        {
            switch (yorn)
            {
                case 0: return "";
                case 1: return "Yes";
                case 2: return "No";
                case 3: return "No";
                default: return "";
            }
        }

        private string NAString(int yorn)
        {
            switch (yorn)
            {
                case 0: return "";
                case 1: return "";
                case 2: return "";
                case 3: return "n/a";
                default: return "";
            }
        }

        protected override bool OnBackButtonPressed()
        {
            /*
            DateTime date = DateTime.Now;
            string nString = "Pic" + date.Year.ToString() + date.Month.ToString() + date.Day.ToString() + date.Second.ToString() + date.Millisecond.ToString() + ".jpg";
            var absolutePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = System.IO.Path.Combine(absolutePath, nString);

            var fileStream = new FileStream(filePath, FileMode.Create);
            //await image.CompressAsync(Bitmap.CompressFormat.Jpeg, 50, fileStream);
            fileStream.Close();
            */

            base.OnBackButtonPressed();
            return false;
        }
    }
}
