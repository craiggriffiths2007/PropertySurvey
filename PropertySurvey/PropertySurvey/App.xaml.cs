using AudioPlayEx;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//using MartControls;
using Xamarin.Essentials;
using Plugin.Media;
using System.IO;
using Xamarin.Forms.PlatformConfiguration;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PropertySurvey
{
    public enum t_units { units_mm, units_inches, units_feet, units_degrees }
    public enum t_current_item
    {
        item_upvc, item_panel, item_glass, item_aluminium, item_garage, item_timber,
        item_bifolding, item_conservatory, item_locking_system, item_composite, item_greenhouse
    }

    public class DeviceInfo
    {
        protected static DeviceInfo _instance;
        double width;
        double height;

        static DeviceInfo()
        {
            _instance = new DeviceInfo();
            
        }
        protected DeviceInfo()
        {
        }

        public static bool IsOrientationPortrait()
        {
            return _instance.height > _instance.width;
        }

        public static void SetSize(double width, double height)
        {
            _instance.width = width;
            _instance.height = height;
        }
    }

    public partial class App : Application
    {
        public string survey_code = "h1";
        public string fitting_code = "h03";

        public string original_contract_number = "";

        public string live_url = "http://192.168.137.15:7293";
        public bool receive_test_data = false;

        public bool bSavingPhoto = false;

        private bool bDoLogin = false;

        public bool bCreateIndex = false;

        public bool bCopyDatabaseFileToDownloads = false;
        public bool bCopyDatabaseFileFromDownloads = false;

        public bool bDoValidation = true;

        public bool camera_landscape_mode = false;

        public int current_van_picture = 0;

        public int photo_file_size = 0;

        public string web_string = "";

        public byte[] photo_file;

        public bool bUpdateAvailable = true;

        public int ScreenWidth = 0;
        public int ScreenHeight = 0;

        public static byte[] cameraImage = null;

        public bool is_landscape = false;
        //public string cereal = "jOFgo6Pn8xeNc6KnQyApA6UauUw=";
        public string cereal = "jOFgo6Pn8xeNc6KnQyApA6UauUw=";

        public string RootItem = "";
        public string CurrentItem = ""; //{ set { /* Set your variable here*/ return; } }
        public bool drawing_edit_mode = false;
        public int drawing_edit_current;
        public int video_mode;
        public string video_preview_filename;

        public int header_photo_type;           // 1 = Survey Front of house
        public int fitter_header_photo_type;    // 1 = Survey Front of house

        public string current_contract_number;

        public string app_version = "25/01/2022";
        public string app_version_update = "";

        public string app_platform = "";

        public bool update_available = false;

        public string date_app;
        public string date_app_update;

        public string phone_serial;

        //public int app_version_number = 40;
        public int app_version_number = 999; // set to 999 for store version so it does'nt update

        public int override_version;
        public int allow_phone8_camera;

        public int camera_vehicle = 0;

        public int total_photos = 0;

        public int number_of_items;

        public string DeviceID = "";

        public string glass_overall_width;

        public app_settings App_Settings;

        public static ISaveAndLoad fileService;

        public Random random = new Random();
        // camera
        //public Handler mBackgroundHandler;

        public int photos_required = 0;
        public int photos_taken = 0;
        public bool photos_enable_increment = true;

        public string current_contract = "";
        public string current_comments = "";
        public int current_line_state = 0;
        public string current_line_number = "344";
        public string photo_file_name = "filename2";

        public string secure_user_name;
        public string secure_serial;

        public string drawing_edit_filename = "";
        public string TextEntryText;
        public string drawing_type;

        public int cur_image_number = 0;

        public int os_number; // 7 or 8

        public int current_item_number;
        public int loaded_item_number;
        public int root_item_number;

        public int total_current_photos;

        public int hinge_message = 0;

        public string ynq = "\n\n             ( YES )                         ( NO )";

        public int total_delivery;
        public int total_delivery_van;
        public int total_vans;
        public int total_cars;

        public int total_incomplete_delivery;
        public int total_incomplete_delivery_van;
        public int total_incomplete_vans;
        public int total_incomplete_cars;

        public int total_panels;
        public int total_alum;
        public int total_cons;
        public int total_garage;
        public int total_timber;
        public int total_upvc;
        public int total_glass;
        public int total_lock;
        public int total_comp;
        public int total_green;
        public int total_bifold;

        public int bCameraCapturing = 0;

        public string contract_comments_contract_no;

        public int vid_image_num;
        public int total_items;
        public int hns_sign_type = 0;
        public Image template_image;

        public int num_to_edit;

        public bool load_template_image;

        public int template_to_load;
        public string template_type_to_load;

        public int flick_change_page = 1;
        public int flick_velocity = 1000;

        public string selected_postcode;
        public string selected_name;

        public string video_type;

        public int image_number = 0;
        public string photo_fname = "";

        public int add_comment = 0;
        public string comment_to_add = "";

        public double drawing_v_offset = 0;

        public int go_into_items = 0;

        public List<string> window_fname_list;
        public List<string> doors_fname_list;
        public List<string> cons_fname_list;
        public List<string> bead_fname_list;
        public List<string> handles_fname_list;
        public List<string> green_fname_list;
        public List<string> cod_list;
        public List<string> backing_glass;
        public List<string> backing_glass_6mm;
        public List<string> backing_glass_6mm_lami;
        public List<string> panel_colour;
        public List<string> room_location;
        public List<string> type_of_item;
        public List<string> type_of_item2;
        public List<string> additional_locks;
        public List<string> additional_locks_alum;
        public List<string> lock_make;
        public List<string> cills;
        public List<string> t_vents;
        public List<string> hard_colour;
        public List<string> hard_colour_timb;
        public List<string> spacer_thickness;
        public List<string> spacer_thickness2;
        public List<string> spacer_thickness3;
        public List<string> locks_choose;
        public List<string> glass_type;
        public List<string> glass_type_greenhouse;
        public List<string> spacer_colour_new;
        public List<string> special_glass_back;
        public List<string> docl_comp;
        public List<string> docl_non;
        public List<string> docl_non_tim;
        public List<string> docl_comp_noa;
        public List<string> docl_non_noa;
        public List<string> docl_non_tim_noa;
        public List<string> ral_codes;
        public List<string> ral_names;
        public List<string> ral_string;
        public List<string> reasonnorepair;
        public List<string> glass_spacer_thickness;
        public List<string> handle_colour;
        public List<string> hinge_type;
        public List<string> bead_type;

        public List<string> standard_and_stain;

        public List<string> locks_required;
        public List<string> locks_required_poe;
        public List<string> letter_box_colour_list;
        public List<string> letter_box_pos_list;
        public List<string> pet_flap_size_list;
        public List<string> pet_flap_type_list;
        public List<string> wer_rating;
        public List<string> replace_reason;

        private static Functions _data;

        public TableInitialization table_init;
        public Header HeaderRecord;
        public SurvImage ImageRecord;

        public Message_Text MessageRecord;
        public PanelTable PanelRecord;
        public AlumTable AlumRecord;
        public ConsTable ConsRecord;
        public GarageTable GarageRecord;
        public TimberTable TimberRecord;
        public UPVCTable UPVCRecord;
        public LockingTable LockingRecord;
        public GlassTable GlassRecord;
        public CompositeTable CompRecord;
        public GreenTable GreenRecord;

        public BifoldTable BifoldRecord;

        public LaddersTable LadderRecord;

        public ToolsTable ToolsRecord;

        public FAccidentsTable FAccidentsRecord;

        public Milage_sheet MileageRecord;

        public Accident_sheet AccidentRecord;

        public WhitnessesData WitnessRecord;

        public VanChecksHeader VanChecksHeader;
        public DeliveryVehicleCheckList DeliveryVehicleCheckList;
        public DeliveryVanVehicleCheckList DeliveryVanVehicleCheckList;
        public WeeklyVanCheckSheet WeeklyVanCheckSheet;
        public CarPanelSheet CarPanelSheet;

        public List<DeliveryVehicleCheckList> DeliveryVehicleCheckLists = new List<DeliveryVehicleCheckList>();
        public List<DeliveryVanVehicleCheckList> DeliveryVanVehicleCheckLists = new List<DeliveryVanVehicleCheckList>();
        public List<WeeklyVanCheckSheet> WeeklyVanCheckSheets = new List<WeeklyVanCheckSheet>();
        public List<CarPanelSheet> CarPanelSheets = new List<CarPanelSheet>();

        public MotorSheet motor_sheet;

        public Camera pCamera;

        public bool taking_photo = false;

        public void SaveCameraImage(byte[] cam_image)
        {
            cameraImage = cam_image;
        }

        public void MakeVideoFilename()
        {
            App.net.photo_fname = "";
            string dname = "";

            App.net.photo_fname = string.Format(dname + "{0:00000000}_vAZ", App.net.HeaderRecord.udi_cont);
            App.net.photo_fname = App.net.photo_fname + string.Format("{0:000}", App.net.root_item_number);
            App.net.photo_fname = App.net.photo_fname + string.Format("{0:00}.mp4", App.net.image_number);
        }

        public bool DoRep()
        {
            if (!App.CurrentApp.HeaderRecord.sn_name.Contains("MA Assist Ltd")
                && !App.CurrentApp.HeaderRecord.fname1.Contains("ZUR41")
                && !App.CurrentApp.HeaderRecord.fname1.Contains("ZUR42")
                && !App.CurrentApp.HeaderRecord.fname1.Contains("ZUR43")
                && !App.CurrentApp.HeaderRecord.fname1.Contains("ZUR44")
                && !App.CurrentApp.HeaderRecord.fname1.Contains("ZUR99")
                && !App.CurrentApp.HeaderRecord.fname1.Contains("ZUR100")
                && !App.CurrentApp.HeaderRecord.fname1.Contains("ZUR101")
                && !App.CurrentApp.HeaderRecord.fname1.Contains("ZUR102")
                && !App.CurrentApp.HeaderRecord.fname1.Contains("ZUR110")
                && !App.CurrentApp.HeaderRecord.fname1.Contains("ESI01")
                && !App.CurrentApp.HeaderRecord.fname1.Contains("ESI02")
                && !App.CurrentApp.HeaderRecord.fname1.Contains("ESI03")
                && !App.CurrentApp.HeaderRecord.fname1.Contains("ESI04")
                && !App.CurrentApp.HeaderRecord.fname1.Contains("ESI05"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CountItems()
        {
            //return;
            App.CurrentApp.HeaderRecord.current_item_number = 0;
            {
                App.CurrentApp.total_panels = 0;
                List<PanelTable> query = App.data.GetPanelsByContractNotSubItem(App.net.HeaderRecord.udi_cont);
                foreach (var item in query)
                {
                    if (item.upvc_item_number == 0)
                    {
                        App.CurrentApp.total_panels++;
                    }
                    if (item.item_number > App.CurrentApp.HeaderRecord.current_item_number)
                    {
                        App.CurrentApp.HeaderRecord.current_item_number = item.item_number;
                    }
                    App.net.PanelRecord = item;
                    App.data.SavePanel();
                }
                App.CurrentApp.HeaderRecord.total_panels = App.CurrentApp.total_panels;
            }
            {
                App.CurrentApp.total_alum = 0;
                var query = App.data.GetAlumsByContract(App.net.HeaderRecord.udi_cont);
                foreach (var item in query)
                {
                    App.CurrentApp.total_alum++;
                    if (item.item_number > App.CurrentApp.HeaderRecord.current_item_number)
                    {
                        App.CurrentApp.HeaderRecord.current_item_number = item.item_number;
                    }
                    App.net.AlumRecord = item;
                    App.data.SaveAlum();
                }
                App.CurrentApp.HeaderRecord.total_alum = App.CurrentApp.total_alum;
            }
            {
                App.CurrentApp.total_cons = 0;
                var query = App.data.GetConssByContract(App.net.HeaderRecord.udi_cont);
                foreach (var item in query)
                {
                    App.CurrentApp.total_cons++;
                    if (item.item_number > App.CurrentApp.HeaderRecord.current_item_number)
                    {
                        App.CurrentApp.HeaderRecord.current_item_number = item.item_number;
                    }
                    App.net.ConsRecord = item;
                    App.data.SaveCons();

                }
                App.CurrentApp.HeaderRecord.total_cons = App.CurrentApp.total_cons;
            }

            {
                App.CurrentApp.total_comp = 0;
                var query = App.data.GetCompByContract(App.net.HeaderRecord.udi_cont);
                foreach (var item in query)
                {
                    App.CurrentApp.total_comp++;
                    if (item.item_number > App.CurrentApp.HeaderRecord.current_item_number)
                    {
                        App.CurrentApp.HeaderRecord.current_item_number = item.item_number;
                    }
                    App.net.CompRecord = item;
                    App.data.SaveComp();

                }
                App.CurrentApp.HeaderRecord.total_comp = App.CurrentApp.total_comp;
            }

            {
                App.CurrentApp.total_garage = 0;
                var query = App.data.GetGaragesByContract(App.net.HeaderRecord.udi_cont);
                foreach (var item in query)
                {
                    App.CurrentApp.total_garage++;
                    if (item.item_number > App.CurrentApp.HeaderRecord.current_item_number)
                    {
                        App.CurrentApp.HeaderRecord.current_item_number = item.item_number;
                    }
                    App.net.GarageRecord = item;
                    App.data.SaveGarage();

                }
                App.CurrentApp.HeaderRecord.total_garage = App.CurrentApp.total_garage;
            }

            {
                App.CurrentApp.total_timber = 0;
                var query = App.data.GetTimberByContract(App.net.HeaderRecord.udi_cont);
                foreach (var item in query)
                {
                    App.CurrentApp.total_timber++;

                    if (item.item_number > App.CurrentApp.HeaderRecord.current_item_number)
                    {
                        App.CurrentApp.HeaderRecord.current_item_number = item.item_number;
                    }
                    App.net.TimberRecord = item;
                    App.data.SaveTimber();

                }
                App.CurrentApp.HeaderRecord.total_timber = App.CurrentApp.total_timber;
            }
            {
                App.CurrentApp.total_upvc = 0;
                var query = App.data.GetUPVCsByContract(App.net.HeaderRecord.udi_cont);
                foreach (var item in query)
                {
                    App.CurrentApp.total_upvc++;
                    if (item.item_number > App.CurrentApp.HeaderRecord.current_item_number)
                    {
                        App.CurrentApp.HeaderRecord.current_item_number = item.item_number;
                    }
                    App.net.UPVCRecord = item;
                    App.data.SaveUPVC();
                }
                App.CurrentApp.HeaderRecord.total_upvc = App.CurrentApp.total_upvc;
            }

            {
                App.CurrentApp.total_glass = 0;
                var query = App.data.GetGlasssByContract(App.net.HeaderRecord.udi_cont);
                foreach (var item in query)
                {
                    App.CurrentApp.total_glass++;
                    if (item.item_number > App.CurrentApp.HeaderRecord.current_item_number)
                    {
                        App.CurrentApp.HeaderRecord.current_item_number = item.item_number;
                    }
                    App.net.GlassRecord = item;
                    App.data.SaveGlass();
                }
                App.CurrentApp.HeaderRecord.total_glass = App.CurrentApp.total_glass;
            }
            {
                App.CurrentApp.total_lock = 0;
                var query = App.data.GetLockByContract(App.net.HeaderRecord.udi_cont);
                foreach (var item in query)
                {
                    App.CurrentApp.total_lock++;
                    if (item.item_number > App.CurrentApp.HeaderRecord.current_item_number)
                    {
                        App.CurrentApp.HeaderRecord.current_item_number = item.item_number;
                    }

                    App.net.LockingRecord = item;
                    App.data.SaveLocking();
                }
                App.CurrentApp.HeaderRecord.total_lock = App.CurrentApp.total_lock;
            }
            {
                App.CurrentApp.total_bifold = 0;
                var query = App.data.GetBifoldsByContract(App.net.HeaderRecord.udi_cont);
                foreach (var item in query)
                {
                    App.CurrentApp.total_bifold++;
                    if (item.item_number > App.CurrentApp.HeaderRecord.current_item_number)
                    {
                        App.CurrentApp.HeaderRecord.current_item_number = item.item_number;
                    }

                    App.net.BifoldRecord = item;
                    App.data.SaveBifold();
                }
                App.CurrentApp.HeaderRecord.total_bifold = App.CurrentApp.total_bifold;
            }
            App.CurrentApp.HeaderRecord.current_item_number++;

            App.net.total_items = App.net.HeaderRecord.total_upvc;
            App.net.total_items += App.net.HeaderRecord.total_panels;
            App.net.total_items += App.net.HeaderRecord.total_glass;
            App.net.total_items += App.net.HeaderRecord.total_alum;
            App.net.total_items += App.net.HeaderRecord.total_garage;
            App.net.total_items += App.net.HeaderRecord.total_timber;
            App.net.total_items += App.net.HeaderRecord.total_cons;
            // App.net.total_items += App.net.HeaderRecord.fit_no_of_videos;
            App.net.total_items += App.net.HeaderRecord.total_lock;
            App.net.total_items += App.net.HeaderRecord.total_comp;
            App.net.total_items += App.net.HeaderRecord.total_green;
            App.net.total_items += App.net.HeaderRecord.total_bifold;
            App.net.HeaderRecord.si_numitem = App.net.total_items;
        }

        public bool IsNumber(string text)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(text);
        }

        public void ShutterSound()
        {
            DependencyService.Get<IAudio>().PlayAudioFile("shutter.mp3");
        }

        public void FocusSound()
        {
            DependencyService.Get<IAudio>().PlayAudioFile("focus.mp3");
        }

        public bool TakePhoto()
        {
            if (taking_photo == false)
            {
                taking_photo = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void PhotoComplete()
        {
            taking_photo = false;
        }

        public App()
        {
            InitializeComponent();

            app = this;
            table_init = new TableInitialization();

            fileService = fileService = DependencyService.Get<ISaveAndLoad>();
            fileService.CreateDirectory("");
            fileService.CreateDirectory("Drawings");
            fileService.CreateDirectory("Photos");
            fileService.CreateDirectory("Signatures");
            fileService.CreateDirectory("Drawings/VC");
            fileService.CreateDirectory("Photos/VC");
            fileService.CreateDirectory("Signatures/VC");

            if (bCopyDatabaseFileToDownloads == true)
            {
                //string path = files.GetLocalFilePath("PropertySurveySQLite.db3");
                byte[] bin = files.LoadBinary("PropertySurveySQLite.db3");
                files.SaveBinaryToDownloads("PropertySurveySQLite.db3", bin);

                /*
                try
                {

                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    mail.From = new MailAddress("craig.griffiths2007@gmail.com");
                    mail.To.Add("cgriffiths@martindales.ltd.uk");
                    mail.Subject = "Message Subject";
                    mail.Body = "Message Body";
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("craig.griffiths2007@gmail.com", "r2d232425272");
                    SmtpServer.EnableSsl = true;
                    ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors) {
                        return true;
                    };
                    SmtpServer.Send(mail);
                    //Toast.MakeText(Application.Context, "Mail Send Sucessufully", ToastLength.Short).Show();
                }

                catch (Exception ex)
                {
                    //Toast.MakeText(Application.Context, ex.ToString(), ToastLength.Long);
                }
                */
            }

            if (bCopyDatabaseFileFromDownloads == true)
            {
                //string path = files.GetLocalFilePath("PropertySurveySQLite.db3");

                byte[] bin = files.LoadBinaryFromDownloads("PropertySurveySQLite2.db3");
                bool does = files.FileExists("PropertySurveySQLite.db3");
                files.DeleteFile("PropertySurveySQLite.db3");
                does = files.FileExists("PropertySurveySQLite.db3");
                files.SaveBinary("PropertySurveySQLite.db3", bin);
                does = files.FileExists("PropertySurveySQLite.db3");
            }

            data.LoadSettings();

            App.net.App_Settings.voice_pitch = 0;
            App.net.App_Settings.voice_pitch = 100;

            App.net.App_Settings.voice_speed = 0;
            App.net.App_Settings.voice_speed = 100;

            App.net.App_Settings.send_data_file = false;



            //DependencyService.Get<ITextToSpeech>().Speak("Welcome");
            App.net.App_Settings.iemi = "na";// DependencyService.Get<ICameraHelper>().GetIdentifier();

            data.SaveSettings();

            CrossMedia.Current.Initialize();

            app_platform = Xamarin.Essentials.DeviceInfo.DeviceType.ToString();

            MakeLists();

            if (bDoLogin == true)
                MainPage = new NavigationPage(new Login());
            else
                MainPage = new NavigationPage(new MainPage());
        }
        
        async Task<byte[]> DownloadImageAsync(string imageUrl)
        {
            var _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(15) };

            try
            {
                using (var httpResponse = await _httpClient.GetAsync(imageUrl))
                {
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        return await httpResponse.Content.ReadAsByteArrayAsync();
                    }
                    else
                    {
                        //Url is Invalid
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                //Handle Exception
                return null;
            }
        }

        public static Functions data
        {
            get
            {
                if (_data == null)
                {
                    _data = new Functions(files.GetLocalFilePath("PropertySurveySQLite.db3"));
                }
                return _data;
            }
        }


        private static App app;
        public static App net
        {
            get { return app; }
        }

        public static ISaveAndLoad files
        {
            get { return fileService; }
        }

        public static App CurrentApp
        {
            get { return app; }
        }

        public void MakeLists()
        {
            locks_required = new List<string>() { "Multipoint locking system", "Sash/Deadlock", "Espag locking System", "Cockspur Handles" };
            locks_required_poe = new List<string>() { "Multipoint locking system", "Sash/Deadlock", "Espag locking System", "Cockspur Handles", "Smashed Glass for Entry", "Casement Stays", "Smashed Glass Then.. \n  Opened With Key" };
            cod_list = new List<string>() { "Accidental Damage", "Bad Workmanship", "Claim inconsistency", "Domestic", "Fire", "Flood",
                "Ground Movement", "Impact", "Loss of keys", "Malicious", "Storm Damage", "Theft", "Wear + Tear" };
            backing_glass = new List<string>() { "None", "Clear", "Artic", "Autumn Leaf","Chantilly","Charcoal Stick","Contora","Cotswold","Digital","EverGlade",
                 "Flemish","Florielle","Mayflower","Minster","Oak","Pellerine","Satin","Stipolite","Sycamore","Taffetta","Trad. lead light","Warwick"};

            backing_glass_6mm = new List<string>() { "None", "Clear", "Autumn Leaf", "Cotswold", "EverGlade", "Flemish", "Stipolite" };
            backing_glass_6mm_lami = new List<string>() { "Clear", "Stipolite" };

            panel_colour = new List<string>() { "Standard White", "830 White", "C156 White", "D/Plas White", "Oak", "Oak on White", "Pearl White",
                "Rosewood", "Rosewood on White", "Saphire White", "Woodgrain 013", "Woodgrain 021", "Woodgrain 013 on White", "Woodgrain 021 on White", "Ultra White" };
            //sb_colour = new List<string>() { "", "None", "Silver", "Gold", "Brown", "White" };
            //sb_colour2 = new List<string>() { "", "None", "No 1 Silver", "No 2 Black", "No 3 White", "No 6 Bronze" };

            room_location = new List<string>() { "Bathroom", "Bedroom", "Conservatory", "Dining room", "En-suite", "Garage", "Greenhouse",
                                                 "Hallway", "Kitchen", "Landing", "Living room", "Office", "Outside", "Porch", "Shed", "Utility" };
            type_of_item = new List<string>() { "Door", "Window", "French Doors", "Conventional Sliding Patio", "Tilt & Slide Patio", "Combi Frame" };
            type_of_item2 = new List<string>() { "Door", "Window", "French Doors", "Conventional Sliding Patio", "Tilt & Slide Patio", "Combi Frame", "Porch", "Bay Window", "Conservatory" };
            additional_locks = new List<string>() { "Euro Locks", "Sash Jammers", "Shoot Bolts", "Snap Locks", "Multi Purpose Bolts", "Sashe Lock", "Deadlock", "Chubb Bolts", "None" };
            additional_locks_alum = new List<string>() { "None", "Snap Locks", "Multi Purpose Bolts", "Chubb Bolts", "Copydex Bolts" };
            lock_make = new List<string>() { "MILA", "GU", "MACO", "AVOCET/WMS", "FUHR", "FERCO", "SIGNIA", "YALE", "WINKNAUS", "AUBI" };
            cills = new List<string>() { "None", "150", "180", "Stubb" };
            t_vents = new List<string>() { "Head of frame", "Top of sash", "None" };
            hard_colour = new List<string>() { "Brass", "Gold", "White", "Silver", "Black", "Brown", "None" };
            hard_colour_timb = new List<string>() { "None", "Brass", "Gold", "White", "Black", "Brown", "Georgian Brass", "Satin Gold", "Satin Silver", "Chrome" };
            spacer_thickness = new List<string>() { "12mm", "14mm", "16mm", "18mm", "20mm" };
            spacer_thickness2 = new List<string>() { "6mm", "8mm", "10mm", "12mm", "14mm", "16mm", "18mm", "20mm" };
            spacer_thickness3 = new List<string>() { "2x 6mm Ali", "2x 8mm Ali", "2x 10mm Ali", "2x 12mm Ali" };

            locks_choose = new List<string>() { "None", "Casemont Stays", "Cockspur", "Espag", "Dead Lock & Yale Lock", "Sash Lock", "Sash", "Lock & Yale Lock", "Sash Lock & Chubb Bolts" };
            glass_type = new List<string>() { "4mm" , "6mm" , "4mm Tough" , "6mm Tough" , "8mm Tough" , "10mm Tough" , "12mm Tough" , "15mm Tough" , "19mm Tough" , "4mm Tough + 6.4mm Lami", "6mm Tough + 6.4mm Lami", "6.4mm Laminated"
                , "7.5mm Laminated" , "8.8mm Laminated", "9.5mm Laminated", "10.8mm Laminated" , "11.5mm Laminated" , "13.5mm Laminated", "3mm Horticultural" };
            glass_type_greenhouse = new List<string>() { "3mm Horticultural", "4mm Tough", "2mm Perspex", "4mm Perspex" };
            spacer_colour_new = new List<string>() { "None", "Black super spacer", "Brown - Ali", "Gold - Ali", "Silver - Ali", "White - Ali", "", "Grey - Warm Edge - Plastic", "Black - Warm Edge - Plastic" };

            special_glass_back = new List<string>() { "Diamond Leaded", "Georgian Leaded", "Queen Anne Lead", "Georgian Bar", "Tinted Glass", "Collect and copy", "Back to Back Spacer", "None" };

            docl_comp = new List<string>() { "Not applicable", "Pilkingtons K", "Argon gas filled", "Pilkingtons K & Argon gas filled", "Grade \"C\"-  Clima Guard A + Argon filled", "Grade \"B\"-  Clima Guard A + Argon + BSS.", "Grade \"A\"- Clima Guard A, Low Iron + Argon filled." };
            docl_non = new List<string>() { "Not applicable", "Grade \"C\"-  Clima Guard A + Argon filled", "Grade \"B\"-  Clima Guard A + Argon + BSS.", "Grade \"A\"- Clima Guard A, Low Iron + Argon filled + Warm edge bar." };
            docl_non_tim = new List<string>() { "Not applicable", "Repair only", "Grade \"C\"-  Clima Guard A + Argon filled", "Grade \"B\"-  Clima Guard A + Argon + BSS.", "Grade \"A\"- Clima Guard A, Low Iron + Argon filled + Warm edge bar." };

            docl_comp_noa = new List<string>() { "Not applicable", "Pilkingtons K", "Argon gas filled", "Pilkingtons K & Argon gas filled", "Grade \"C\"-  Clima Guard A + Argon filled", "Grade \"B\"-  Clima Guard A + Argon + BSS." };
            docl_non_noa = new List<string>() { "Not applicable", "Grade \"C\"-  Clima Guard A + Argon filled", "Grade \"B\"-  Clima Guard A + Argon + BSS." };
            docl_non_tim_noa = new List<string>() { "Not applicable", "Grade \"C\"-  Clima Guard A + Argon filled", "Repair only", "Grade \"B\"-  Clima Guard A + Argon + BSS." };

            reasonnorepair = new List<string>() { "Obsolete parts", "Item split", "Cracked weld", "Excessive damage", "Upgrading", "Domestic" };

            glass_spacer_thickness = new List<string>() { "16mm", "18mm", "20mm" };

            handle_colour = new List<string>() { "None", "Shiny brass", "Satin gold", "Chrome", "Satin silver", "White", "Black", "Brown" };

            hinge_type = new List<string>() { "None", "Fire hinge", "Tri hinge", "Std hinge" };

            bead_type = new List<string>() { "Standard", "Scocia", "Ovolo" };

            replace_reason = new List<string> { "", "Obsolete parts", "Item split", "Cracked weld", "Excessive damage", "Upgrading", "Domestic" };
            wer_rating = new List<string>() { "", "A", "B", "C", "UNKNOWN" };

            standard_and_stain = new List<string>() { "", "Sample sent to MTF", "New Mahogony", "Old Mahogony", "Pine", "Dark Oak", "Teak", "Light Oak", "Walnut", "Clear", "Ebony", "Orange", "Yellow", "Green", "Blue", "Red", "Black", "White" };

            letter_box_colour_list = new List<string>() { "None", "Black", "Chrome", "Gold", "Satin Silver", "White" };
            letter_box_pos_list = new List<string>() { "n/a", "Midrail", "Middle", "Bottom" };
            pet_flap_size_list = new List<string>() { "None", "Small", "Medium", "Large" };
            pet_flap_type_list = new List<string>() { "n/a", "Cat", "Dog" };

            window_fname_list = new List<string>() { "PropertySurvey.Images.Thumbnail.window1.jpg"
                , "PropertySurvey.Images.Thumbnail.window2.jpg"
                , "PropertySurvey.Images.Thumbnail.window3.jpg"
                , "PropertySurvey.Images.Thumbnail.window4.jpg"
                , "PropertySurvey.Images.Thumbnail.window5.jpg"
                , "PropertySurvey.Images.Thumbnail.window6.jpg"
                , "PropertySurvey.Images.Thumbnail.window7.jpg"
                , "PropertySurvey.Images.Thumbnail.window8.jpg"
                , "PropertySurvey.Images.Thumbnail.window9.jpg"
                , "PropertySurvey.Images.Thumbnail.window10.jpg"
                , "PropertySurvey.Images.Thumbnail.window11.jpg"
                , "PropertySurvey.Images.Thumbnail.window12.jpg"
                , "PropertySurvey.Images.Thumbnail.window13.jpg"
                , "PropertySurvey.Images.Thumbnail.window14.jpg"
                , "PropertySurvey.Images.Thumbnail.window15.jpg"
                , "PropertySurvey.Images.Thumbnail.window16.jpg"
                , "PropertySurvey.Images.Thumbnail.window17.jpg"
                , "PropertySurvey.Images.Thumbnail.window18.jpg"
                , "PropertySurvey.Images.Thumbnail.window19.jpg"
                , "PropertySurvey.Images.Thumbnail.window20.jpg"
                , "PropertySurvey.Images.Thumbnail.window16b.jpg" };
            doors_fname_list = new List<string>()
            {
                    "PropertySurvey.Images.Thumbnail.door1.jpg"
                    , "PropertySurvey.Images.Thumbnail.door1_b.jpg"
                    , "PropertySurvey.Images.Thumbnail.door2.jpg"
                    , "PropertySurvey.Images.Thumbnail.door2_b.jpg"
                    , "PropertySurvey.Images.Thumbnail.door3.jpg"
                    , "PropertySurvey.Images.Thumbnail.door3_b.jpg"
                    , "PropertySurvey.Images.Thumbnail.door4.jpg"
                    , "PropertySurvey.Images.Thumbnail.door4_b.jpg"
                    , "PropertySurvey.Images.Thumbnail.door5.jpg"
                    , "PropertySurvey.Images.Thumbnail.door5_b.jpg"
                    , "PropertySurvey.Images.Thumbnail.door6.jpg"
                    , "PropertySurvey.Images.Thumbnail.door6_b.jpg"
                    , "PropertySurvey.Images.Thumbnail.door7.jpg"
                    , "PropertySurvey.Images.Thumbnail.door7_b.jpg"
                    , "PropertySurvey.Images.Thumbnail.door7b.jpg"
                    , "PropertySurvey.Images.Thumbnail.door7b_b.jpg"
                    , "PropertySurvey.Images.Thumbnail.wooden_door_1.jpg"
                    , "PropertySurvey.Images.Thumbnail.wooden_door_2.jpg"
                    , "PropertySurvey.Images.Thumbnail.wooden_door_3.jpg"
                    , "PropertySurvey.Images.Thumbnail.wooden_door_4.jpg"
                    , "PropertySurvey.Images.Thumbnail.door_b_1.jpg"
                    , "PropertySurvey.Images.Thumbnail.door_b_2.jpg"
                    , "PropertySurvey.Images.Thumbnail.door_b_3.jpg"
                    , "PropertySurvey.Images.Thumbnail.door_b_4.jpg"
                    , "PropertySurvey.Images.Thumbnail.door_b_5.jpg"
                    , "PropertySurvey.Images.Thumbnail.door_b_6.jpg"
                    , "PropertySurvey.Images.Thumbnail.garage_1a.jpg"
                    , "PropertySurvey.Images.Thumbnail.garage_1b.jpg"
                    , "PropertySurvey.Images.Thumbnail.garage_2a.jpg"
                    , "PropertySurvey.Images.Thumbnail.garage_2b.jpg"
                    , "PropertySurvey.Images.Thumbnail.cons6.jpg"
                    , "PropertySurvey.Images.Thumbnail.cons7.jpg"
                    , "PropertySurvey.Images.Thumbnail.cons8.jpg"
                    , "PropertySurvey.Images.Thumbnail.door8.jpg"
                    , "PropertySurvey.Images.Thumbnail.door9.jpg"
                    , "PropertySurvey.Images.Thumbnail.door10.jpg"
                    , "PropertySurvey.Images.Thumbnail.door11.jpg"
                    , "PropertySurvey.Images.Thumbnail.door12.jpg"
                    , "PropertySurvey.Images.Thumbnail.door13.jpg"
                    , "PropertySurvey.Images.Thumbnail.door14.jpg"
                    , "PropertySurvey.Images.Thumbnail.door15.jpg"
                    , "PropertySurvey.Images.Thumbnail.rollergarage.jpg"
            };
            cons_fname_list = new List<string>()
            {
                    /*, "PropertySurvey.Images.Thumbnail.cons1.jpg"
                    , "PropertySurvey.Images.Thumbnail.cons2.jpg"
                    , "PropertySurvey.Images.Thumbnail.cons3.jpg"
                    , "PropertySurvey.Images.Thumbnail.cons4.jpg"
                    , "PropertySurvey.Images.Thumbnail.cons5.jpg"
                    , "PropertySurvey.Images.Thumbnail.cons6.jpg"
                    , "PropertySurvey.Images.Thumbnail.cons7.jpg"
                    , "PropertySurvey.Images.Thumbnail.cons8.jpg"*/
                     "PropertySurvey.Images.Thumbnail.cons9.jpg"
                    , "PropertySurvey.Images.Thumbnail.cons10.jpg"
                    //, "PropertySurvey.Images.Thumbnail.cons11.jpg"
                    , "PropertySurvey.Images.Thumbnail.cons12.jpg"
                    , "PropertySurvey.Images.Thumbnail.cons13.jpg"
                    , "PropertySurvey.Images.Thumbnail.cons14.jpg"
                    , "PropertySurvey.Images.Thumbnail.cons15.jpg"
                    , "PropertySurvey.Images.Thumbnail.cons16.jpg"
                    , "PropertySurvey.Images.Thumbnail.cons17.jpg"
                    , "PropertySurvey.Images.Thumbnail.cons18.jpg"
                    , "PropertySurvey.Images.Thumbnail.cons19.jpg"
                    , "PropertySurvey.Images.Thumbnail.cons20.jpg"
                    , "PropertySurvey.Images.Thumbnail.cons21.jpg"
                    , "PropertySurvey.Images.Thumbnail.cons22.jpg"
                    , "PropertySurvey.Images.Thumbnail.cons23.jpg"
                    , "PropertySurvey.Images.Thumbnail.cons24.jpg"
                    , "PropertySurvey.Images.Thumbnail.cons25.jpg"
                    , "PropertySurvey.Images.Thumbnail.cons26.jpg"
                    , "PropertySurvey.Images.Thumbnail.cons27.jpg"
                    , "PropertySurvey.Images.Thumbnail.cons28.jpg"};
            bead_fname_list = new List<string>()
            {
                     "PropertySurvey.Images.Thumbnail.section1.jpg"
                    , "PropertySurvey.Images.Thumbnail.section2.jpg"
                    , "PropertySurvey.Images.Thumbnail.section3.jpg"
                    , "PropertySurvey.Images.Thumbnail.section4.jpg"
                    , "PropertySurvey.Images.Thumbnail.section5.jpg"
                    , "PropertySurvey.Images.Thumbnail.section6.jpg"
                    , "PropertySurvey.Images.Thumbnail.section7.jpg"
                    , "PropertySurvey.Images.Thumbnail.section8.jpg"
                    , "PropertySurvey.Images.Thumbnail.section9.jpg"
                    , "PropertySurvey.Images.Thumbnail.section10.jpg"
                    , "PropertySurvey.Images.Thumbnail.bead1.jpg"
                    , "PropertySurvey.Images.Thumbnail.bead2.jpg"
                    , "PropertySurvey.Images.Thumbnail.bead3.jpg"
                    , "PropertySurvey.Images.Thumbnail.bead4.jpg"
                    , "PropertySurvey.Images.Thumbnail.bead5.jpg"
                    , "PropertySurvey.Images.Thumbnail.bead6.jpg"
                    , "PropertySurvey.Images.Thumbnail.section_a.jpg"
                    , "PropertySurvey.Images.Thumbnail.section_b.jpg"
                    , "PropertySurvey.Images.Thumbnail.section_c.jpg"
                    , "PropertySurvey.Images.Thumbnail.section_d.jpg"
                    , "PropertySurvey.Images.Thumbnail.section_e.jpg"
                    , "PropertySurvey.Images.Thumbnail.section_f.jpg"
                    , "PropertySurvey.Images.Thumbnail.section_g.jpg"
                    , "PropertySurvey.Images.Thumbnail.section_h.jpg"
                    , "PropertySurvey.Images.Thumbnail.section_i.jpg"
                    , "PropertySurvey.Images.Thumbnail.section_j.jpg"
                    , "PropertySurvey.Images.Thumbnail.section_11.jpg"
                    , "PropertySurvey.Images.Thumbnail.section_k.jpg"
                    , "PropertySurvey.Images.Thumbnail.section_l.jpg"
            };
            handles_fname_list = new List<string>()
            {
                   "PropertySurvey.Images.Thumbnail.handle1.jpg"
                   , "PropertySurvey.Images.Thumbnail.handle2.jpg"
                   , "PropertySurvey.Images.Thumbnail.handle3.jpg"
                   , "PropertySurvey.Images.Thumbnail.handle4.jpg"
                   , "PropertySurvey.Images.Thumbnail.handle5.jpg"
                   , "PropertySurvey.Images.Thumbnail.handle6.jpg"
                   , "PropertySurvey.Images.Thumbnail.handle7.jpg"
                   , "PropertySurvey.Images.Thumbnail.handle8.jpg"
                   , "PropertySurvey.Images.Thumbnail.handle9.jpg"

            };
            green_fname_list = new List<string>()
            {
                   "PropertySurvey.Images.Thumbnail.green1.jpg"
                   , "PropertySurvey.Images.Thumbnail.green2.jpg"
                   , "PropertySurvey.Images.Thumbnail.green3.jpg"
                   , "PropertySurvey.Images.Thumbnail.green4.jpg"

            };
        }

        public void CreatePhotoFilename()
        {
            App.net.photo_fname = "";
            string dname = "";

            if (App.CurrentApp.camera_vehicle == 3 || App.CurrentApp.camera_vehicle == 4 || App.CurrentApp.camera_vehicle == 5)
            {
                switch (App.CurrentApp.camera_vehicle)
                {
                    case 3:App.net.photo_fname = string.Format(string.Format("{0:00000000}", App.CurrentApp.LadderRecord.RecID) + "_LadPi{0:000}.jpg", App.net.image_number); break;
                    case 4:App.net.photo_fname = string.Format(string.Format("8{0:0000000}", App.CurrentApp.FAccidentsRecord.RecID) + "_FAcci{0:000}.jpg", App.net.image_number); break;
                    case 5:App.net.photo_fname = string.Format(string.Format("9{0:0000000}", App.CurrentApp.AccidentRecord.RecID) + "_photo_{0:00}.jpg", App.net.image_number); break;
                }
            }
            else
            {
                if (App.CurrentApp.camera_vehicle == 2)
                {
                    /*
                    if (!isf.DirectoryExists("Photos/SS"))
                    {
                        isf.CreateDirectory("Photos/SS");
                    }
                    */
                    App.net.photo_fname = string.Format(App.CurrentApp.HeaderRecord.udi_cont + "_SS___{0:000}.jpg", App.net.image_number);
                }
                else
                {

                    if (App.CurrentApp.camera_vehicle == 1)
                    {
                        string check_type = "";
                        int item_no = 0;

                        switch (App.CurrentApp.CurrentItem)
                        {
                            case "deliveryvan": item_no = App.CurrentApp.DeliveryVanVehicleCheckList.item_no; check_type = "a"; break;
                            case "delivery": item_no = App.CurrentApp.DeliveryVehicleCheckList.item_no; check_type = "d"; break;
                            case "van": item_no = App.CurrentApp.WeeklyVanCheckSheet.item_no; check_type = "v"; break;
                            case "car": item_no = App.CurrentApp.CarPanelSheet.item_no; check_type = "c"; break;
                        }

                        App.net.photo_fname = string.Format("VC/" + App.CurrentApp.VanChecksHeader.unique_id + "_{0:00000000}_" + check_type + "_{1:000}.jpg", item_no, App.net.image_number);

                    }
                    else
                    {
                            if (App.net.HeaderRecord.iRecordType > 0 && App.net.HeaderRecord.typeB != "Securing" && App.net.CurrentItem == "")
                            {
                                switch (App.net.fitter_header_photo_type)
                                {
                                    case 1:
                                        if (App.net.HeaderRecord.typeB == "Remedial")
                                        {
                                            App.net.photo_fname = string.Format(dname + "{0:00000000}_rAZRem", App.net.HeaderRecord.udi_cont);
                                            App.net.photo_fname = App.net.photo_fname + string.Format("{0:00}.jpg", App.net.image_number);
                                        }
                                        else
                                        {
                                            App.net.photo_fname = string.Format(dname + "{0:00000000}_fAZFit", App.net.HeaderRecord.udi_cont);
                                            App.net.photo_fname = App.net.photo_fname + string.Format("{0:00}.jpg", App.net.image_number);
                                        }
                                        break;
                                    case 9:
                                        App.net.photo_fname = string.Format(dname + "{0:00000000}_fAZFit", App.net.HeaderRecord.udi_cont);
                                        App.net.photo_fname = App.net.photo_fname + string.Format("{0:00}.jpg", App.net.image_number);
                                        break;
                                    default:
                                        if (App.net.HeaderRecord.typeB == "Remedial")
                                        {
                                            App.net.photo_fname = string.Format(dname + "{0:00000000}_rAZRem", App.net.HeaderRecord.udi_cont);
                                            App.net.photo_fname = App.net.photo_fname + string.Format("{0:00}.jpg", App.net.image_number);
                                        }
                                        else
                                        {
                                            App.net.photo_fname = string.Format(dname + "{0:00000000}_fAZFit", App.net.HeaderRecord.udi_cont);
                                            App.net.photo_fname = App.net.photo_fname + string.Format("{0:00}.jpg", App.net.image_number);
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                if (App.net.HeaderRecord.b_mrk == true)
                                {
                                    App.net.photo_fname = string.Format(dname + "{0:00000000}_kAZSpk", App.net.HeaderRecord.udi_cont);
                                    App.net.photo_fname = App.net.photo_fname + string.Format("{0:00}.jpg", App.net.image_number);
                                }
                                else
                                {
                                    if ((App.net.HeaderRecord.typeB == "Securing" || App.net.HeaderRecord.type == "Complaint") && App.net.fitter_header_photo_type == 1 && App.net.CurrentItem == "")
                                    {
                                        if (App.net.HeaderRecord.typeB == "Securing")
                                        {
                                            App.net.photo_fname = string.Format(dname + "{0:00000000}_sAZIns", App.net.HeaderRecord.udi_cont);
                                            App.net.photo_fname = App.net.photo_fname + string.Format("{0:00}.jpg", App.net.image_number);
                                        }
                                        if (App.net.HeaderRecord.type == "Complaint")
                                        {
                                            App.net.photo_fname = string.Format(dname + "{0:00000000}_kAZCmp", App.net.HeaderRecord.udi_cont);
                                            App.net.photo_fname = App.net.photo_fname + string.Format("{0:00}.jpg", App.net.image_number);
                                        }
                                    }
                                    else
                                    {
                                        App.net.photo_fname = string.Format(dname + "{0:00000000}_cAZ", App.net.HeaderRecord.udi_cont);
                                        App.net.photo_fname = App.net.photo_fname + string.Format("{0:000}", App.net.root_item_number);
                                        App.net.photo_fname = App.net.photo_fname + string.Format("{0:00}.jpg", App.net.image_number);

                                        switch (App.net.header_photo_type)
                                        {
                                            case 1:
                                                App.net.photo_fname = string.Format(dname + "{0:00000000}_cAH", App.net.HeaderRecord.udi_cont);
                                                App.net.photo_fname = App.net.photo_fname + "000";
                                                App.net.photo_fname = App.net.photo_fname + string.Format("{0:00}.jpg", App.net.image_number);
                                                break;
                                        }
                                    }

                                    /*
                                    if (App.net.HeaderRecord.typeB == "Securing")
                                    {
                                        fname = string.Format("Photos\\{0:00000000}_sAZIns", App.net.HeaderRecord.udi_cont);
                                        fname = fname + string.Format("{0:000}*.jpg", App.net.root_item_number);
                                    }
                                    else
                                    {
                                        fname = string.Format("Photos\\{0:00000000}_cAZ", App.net.HeaderRecord.udi_cont);
                                        fname = fname + string.Format("{0:000}*.jpg", App.net.root_item_number);
                                    }
                                    */

                                }
                            }
                        }
                    }
                }
            }
    

        public int ResumeAtPropertySurveyId { get; set; }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            App.data.SaveItem();
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
