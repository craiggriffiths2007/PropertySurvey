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
    public partial class AllBrowse : ContentPage
    {
        public class ListData
        {
            public int uid { get; set; }
            public int item_number { get; set; }
            public string item_type { get; set; }
            public string commnts { get; set; }
            public string back_colour { get; set; }
            public string other_info { get; set; }
            public int isComplete { get; set; }
            public string inc_mes { get; set; }

            public ListData(int uID, int _item_number, string _item_type, string _commnts, int _isComplete, string _other_info)
            {
                this.uid = uID;

                this.item_number = _item_number;
                this.item_type = _item_type;
                this.commnts = _commnts;
                this.isComplete = _isComplete;
                this.other_info = _other_info;
                
                if (App.net.HeaderRecord.iRecordType > 0 && App.net.HeaderRecord.typeB != "Securing")
                {
                    this.back_colour = "#1881bf";
                }
                else
                {
                    if (_isComplete == 0 /*&& (App.net.HeaderRecord.iRecordType==0 && App.net.HeaderRecord.typeB != "Securing" )*/ )
                    {
                        this.back_colour = "IndianRed";
                        this.item_type = "INCOMPLETE ( will not be sent )";
                    }
                    else
                    {
                        this.back_colour = "#7ccb7e";
                    }
                }
            }
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            Navigation.PopAsync(false);
            return true;
        }

        ListData selected_data = null;

        bool bSelected = false;
        bool child_screen_active = false;

        private void DrawList()
        {
            List<ListData> dataSource = new List<ListData>();
            string comment = "";
            string other_info = "";
            int max_len = 35;
            //return;
            selected_data = null;
            bSelected = false;

            if (App.net.CurrentItem == "panel")
            {
                App.net.total_panels = 0;
                App.net.HeaderRecord.incomplete_panels = 0;
                List<PanelTable> query = App.data.GetPanelsByContractNotSubItem(App.net.HeaderRecord.udi_cont);
                foreach (var item in query)
                {
                    comment = item.long_sptext;
                    other_info = "W: " + item.wedit + "mm    H: " + item.hedit + "mm";
                    if (comment.Length > max_len)
                    {
                        comment = item.long_sptext.Substring(0, max_len - 2) + "..";
                    }
                    dataSource.Add(new ListData(item.RecID, item.item_number, item.typeedit, comment, item.isComplete, other_info));
                    if ((item.isComplete == 0) && (App.net.HeaderRecord.iRecordType == 0))
                    {
                        App.net.HeaderRecord.incomplete_panels++;
                    }
                    App.net.total_panels++;
                }
                App.net.HeaderRecord.total_panels = App.net.total_panels;
                if(App.net.HeaderRecord.total_panels>0)
                    Title = "Panel - " + App.net.HeaderRecord.total_panels.ToString();
            }

            if (App.net.CurrentItem == "upvc")
            {
                App.net.total_upvc = 0;
                App.net.HeaderRecord.incomplete_upvc = 0;
                var query = App.data.GetUPVCsByContract(App.net.HeaderRecord.udi_cont);
                foreach (var item in query)
                {
                    comment = item.long_comments;
                    other_info = "Int W: " + item.internal_width + "mm    Int H: " + item.internal_height + "mm";
                    if (comment.Length > max_len)
                    {
                        comment = item.long_comments.Substring(0, max_len - 2) + "..";
                    }
                    dataSource.Add(new ListData(item.RecID, item.item_number, item.upvc_item, comment, item.isComplete, other_info));
                    if ((item.isComplete == 0) && (App.net.HeaderRecord.iRecordType == 0))
                    {
                        App.net.HeaderRecord.incomplete_upvc++;
                    }
                    App.net.total_upvc++;
                }
                App.net.HeaderRecord.total_upvc = App.net.total_upvc;
                if (App.net.HeaderRecord.total_upvc > 0)
                    Title = "UPVC - " + App.net.HeaderRecord.total_upvc.ToString();
            }
            
            if (App.net.CurrentItem == "alum")
            {
                App.net.total_alum = 0;
                App.net.HeaderRecord.incomplete_alum = 0;
                var query = App.data.GetAlumsByContract(App.net.HeaderRecord.udi_cont);
                foreach (var item in query)
                {
                    comment = item.long_comments;
                    other_info = "Int W: " + item.internal_width + "mm    Int H: " + item.internal_height + "mm";
                    if (comment.Length > max_len)
                    {
                        comment = item.long_comments.Substring(0, max_len - 2) + "..";
                    }
                    dataSource.Add(new ListData(item.RecID, item.item_number, item.type, comment, item.isComplete, other_info));
                    if ((item.isComplete == 0) && (App.net.HeaderRecord.iRecordType == 0))
                    {
                        App.net.HeaderRecord.incomplete_alum++;
                    }
                    App.net.total_alum++;
                }
                App.net.HeaderRecord.total_alum = App.net.total_alum;
                if (App.net.HeaderRecord.total_alum > 0)
                    Title = "Aluminium - " + App.net.HeaderRecord.total_alum.ToString();
            }
            
            if (App.net.CurrentItem == "timber")
            {
                App.net.total_timber = 0;
                App.net.HeaderRecord.incomplete_timber = 0;
                var query = App.data.GetTimberByContract(App.net.HeaderRecord.udi_cont);
                foreach (var item in query)
                {
                    comment = item.long_timber_comments;
                    other_info = "Int W: " + item.internal_width + "mm    Int H: " + item.internal_height + "mm";
                    if (comment.Length > max_len)
                    {
                        comment = item.long_timber_comments.Substring(0, max_len - 2) + "..";
                    }
                    dataSource.Add(new ListData(item.RecID, item.item_number, item.timber_item, comment, item.isComplete, other_info));
                    if ((item.isComplete == 0) && (App.net.HeaderRecord.iRecordType == 0))
                    {
                        App.net.HeaderRecord.incomplete_timber++;
                    }
                    App.net.total_timber++;
                }
                App.net.HeaderRecord.total_timber = App.net.total_timber;
                if (App.net.HeaderRecord.total_timber > 0)
                    Title = "Timber - " + App.net.HeaderRecord.total_timber.ToString();
            }


            if (App.net.CurrentItem == "glass")
            {
                App.net.total_glass = 0;
                App.net.HeaderRecord.incomplete_glass = 0;
                var query = App.data.GetGlasssByContractNotSubItem(App.net.HeaderRecord.udi_cont);
                foreach (var item in query)
                {
                    comment = item.long_comments;
                    other_info = "W1: " + item.glass_width + "mm    H1: " + item.glass_height + "mm";
                    for (int i = 0; i < item.units_required; i++)
                    {
                        switch (i)
                        {
                            case 1: other_info = other_info + Convert.ToChar(13) + "W2" + ": " + item.glass_width2 + "mm    H2" + ": " + item.glass_height2 + "mm"; break;
                            case 2: other_info = other_info + Convert.ToChar(13) + "W3" + ": " + item.glass_width3 + "mm    H3" + ": " + item.glass_height3 + "mm"; break;
                            case 3: other_info = other_info + Convert.ToChar(13) + "W4" + ": " + item.glass_width4 + "mm    H4" + ": " + item.glass_height4 + "mm"; break;
                            case 4: other_info = other_info + Convert.ToChar(13) + "W5" + ": " + item.glass_width5 + "mm    H5" + ": " + item.glass_height5 + "mm"; break;
                            case 5: other_info = other_info + Convert.ToChar(13) + "W6" + ": " + item.glass_width6 + "mm    H6" + ": " + item.glass_height6 + "mm"; break;
                            case 6: other_info = other_info + Convert.ToChar(13) + "W7" + ": " + item.glass_width7 + "mm    H7" + ": " + item.glass_height7 + "mm"; break;
                            case 7: other_info = other_info + Convert.ToChar(13) + "W8" + ": " + item.glass_width8 + "mm    H8" + ":" + item.glass_height8 + "mm"; break;
                        }
                    }
                    if (comment.Length > max_len)
                    {
                        comment = item.long_comments.Substring(0, max_len - 2) + "..";
                    }
                    dataSource.Add(new ListData(item.RecID, item.item_number, item.glass_type /*+ "  " + item.type */, comment, item.isComplete, other_info));
                    if ((item.isComplete == 0) && (App.net.HeaderRecord.iRecordType == 0))
                    {
                        App.net.HeaderRecord.incomplete_glass++;
                    }
                    App.net.total_glass++;
                }
                App.net.HeaderRecord.total_glass = App.net.total_glass;
                if (App.net.HeaderRecord.total_glass > 0)
                    Title = "Glass - " + App.net.HeaderRecord.total_glass.ToString();
            }

            if (App.net.CurrentItem == "comp")
            {
                App.net.total_comp = 0;
                App.net.HeaderRecord.incomplete_comp = 0;
                var query = App.data.GetCompByContract(App.net.HeaderRecord.udi_cont);
                foreach (var item in query)
                {
                    comment = item.comments;
                    other_info = "Int W: " + item.internal_width + "mm    Int H:" + item.internal_height;
                    if (comment.Length > max_len)
                    {
                        comment = item.comments.Substring(0, max_len - 2) + "..";

                    }
                    dataSource.Add(new ListData(item.RecID, item.item_number, item.door_make, comment, item.isComplete, other_info));
                    if ((item.isComplete == 0) && (App.net.HeaderRecord.iRecordType == 0))
                    {
                        App.net.HeaderRecord.incomplete_comp++;
                    }
                    App.net.total_comp++;
                }
                App.net.HeaderRecord.total_comp = App.net.total_comp;
                if (App.net.HeaderRecord.total_comp > 0)
                    Title = "Composite Door - " + App.net.HeaderRecord.total_comp.ToString();
            }

            if (App.net.CurrentItem == "green")
            {
                App.net.total_green = 0;
                App.net.HeaderRecord.incomplete_green = 0;
                var query = App.data.GetGreensByContract(App.net.HeaderRecord.udi_cont);
                foreach (var item in query)
                {
                    comment = item.summary;
                    other_info = "Base W: " + item.base_size_x + "mm    Base H:" + item.base_size_y;
                    if (comment.Length > max_len)
                    {
                        comment = item.summary.Substring(0, max_len - 2) + "..";
                    }
                    dataSource.Add(new ListData(item.RecID, item.item_number, item.glaze_type, comment, item.isComplete, other_info));
                    if ((item.isComplete == 0) && (App.net.HeaderRecord.iRecordType == 0))
                    {
                        App.net.HeaderRecord.incomplete_green++;
                    }
                    App.net.total_green++;
                }
                App.net.HeaderRecord.total_green = App.net.total_green;
                if (App.net.HeaderRecord.total_green > 0)
                    Title = "Greenhouse/Shed - " + App.net.HeaderRecord.total_green.ToString();
            }


            if (App.net.CurrentItem == "cons")
            {
                App.net.total_cons = 0;
                App.net.HeaderRecord.incomplete_cons = 0;
                //App.net.HeaderRecord.uspot_p2 = 0;
                var query = App.data.GetConssByContract(App.net.HeaderRecord.udi_cont);
                foreach (var item in query)
                {
                    comment = item.long_comments;
                    other_info = item.cause_of_damage; // "Base W: " + item.base_size_x + "mm    Base H:" + item.base_size_y;
                    if (comment.Length > max_len)
                    {
                        comment = item.long_comments.Substring(0, max_len - 2) + "..";
                    }
                    dataSource.Add(new ListData(item.RecID, item.item_number, item.type, comment, item.isComplete, other_info));
                    if ((item.isComplete == 0) && (App.net.HeaderRecord.iRecordType == 0))
                    {
                        App.net.HeaderRecord.incomplete_cons++;
                    }
                    App.net.total_cons++;
                }

                App.CurrentApp.HeaderRecord.total_cons = App.net.total_cons;
                //App.net.HeaderRecord.uspot_p1 = App.net.total_green;
                if (App.net.HeaderRecord.total_cons > 0)
                    Title = "Conservatory - " + App.net.HeaderRecord.total_cons.ToString();
            }

            if (App.net.CurrentItem == "bifold")
            {
                App.net.total_bifold = 0;
                App.net.HeaderRecord.incomplete_bifold = 0;
                var query = App.data.GetBifoldsByContract(App.net.HeaderRecord.udi_cont);
                foreach (var item in query)
                {
                    comment = item.door_type;
                    other_info = "Int w: " + item.internal_width + "mm    Int H:" + item.internal_height;

                    if (comment.Length > max_len)
                    {
                        comment = comment.Substring(0, max_len - 2) + "..";
                    }
                    dataSource.Add(new ListData(item.RecID, item.item_number, item.threshold_type, comment, item.isComplete, other_info));
                    if ((item.isComplete == 0) && (App.net.HeaderRecord.iRecordType == 0))
                    {
                        App.net.HeaderRecord.incomplete_bifold++;
                    }
                    App.net.total_bifold++;
                }

                App.CurrentApp.HeaderRecord.total_bifold = App.net.total_bifold; // number of bifolds
                //App.net.HeaderRecord.uspot_p1 = App.net.total_green;
                if (App.net.HeaderRecord.total_bifold > 0)
                    Title = "Bifolding Door - " + App.net.HeaderRecord.total_bifold.ToString();
            }

            if (App.net.CurrentItem == "garage")
            {
                App.net.total_garage = 0;
                App.net.HeaderRecord.incomplete_garage = 0;
                var query = App.data.GetGaragesByContract(App.net.HeaderRecord.udi_cont);
                foreach (var item in query)
                {
                    comment = item.long_comments;
                    other_info = "W: " + item.actual_door_width + "mm    H: " + item.actual_door_height + "mm";

                    if (comment.Length > max_len)
                    {
                        comment = item.long_comments.Substring(0, max_len - 2) + "..";
                    }
                    dataSource.Add(new ListData(item.RecID, item.item_number, item.type_of_garage, comment, item.isComplete, other_info));
                    if ((item.isComplete == 0) && (App.net.HeaderRecord.iRecordType == 0))
                    {
                        App.net.HeaderRecord.incomplete_garage++;
                    }
                    App.net.total_garage++;
                }

                App.CurrentApp.HeaderRecord.total_garage = App.net.total_garage; // number of bifolds
                //App.net.HeaderRecord.uspot_p1 = App.net.total_green;
                if (App.net.HeaderRecord.total_garage > 0)
                    Title = "Garage - " + App.net.HeaderRecord.total_garage.ToString();
            }

            if (App.net.CurrentItem == "lock")
            {
                App.net.total_lock = 0;
                App.net.HeaderRecord.incomplete_lock = 0;
                var query = App.data.GetLockByContract(App.net.HeaderRecord.udi_cont);
                foreach (var item in query)
                {
                    comment = item.comments;
                    other_info = item.COD_Code;
                    if (comment.Length > max_len)
                    {
                        comment = item.comments.Substring(0, max_len - 2) + "..";
                    }
                    if (item.bMulti == false)
                    {
                        dataSource.Add(new ListData(item.RecID, item.item_number, "General", comment, item.isComplete, other_info));
                    }
                    else
                    {
                        dataSource.Add(new ListData(item.RecID, item.item_number, item.item, comment, item.isComplete, other_info));
                    }
                    if ((item.isComplete == 0) && (App.net.HeaderRecord.iRecordType == 0))
                    {
                        App.net.HeaderRecord.incomplete_lock++;
                    }
                    App.net.total_lock++;
                }

                App.CurrentApp.HeaderRecord.total_lock = App.net.total_lock; // number of bifolds
                //App.net.HeaderRecord.uspot_p1 = App.net.total_green;
                if (App.net.HeaderRecord.total_lock > 0)
                    Title = "Lock - " + App.net.HeaderRecord.total_lock.ToString();
            }

            listView.ItemsSource = dataSource;

        }


        private void AddNew()
        {
            if (App.net.CurrentItem == "upvc")  { AddUPVC(); }
            if (App.net.CurrentItem == "panel") { AddPanel(); }
            if (App.net.CurrentItem == "green") { AddGreen(); }
            if (App.net.CurrentItem == "glass") { AddGlass(); }
            if (App.net.CurrentItem == "alum")  { AddAlum(); }
            if (App.net.CurrentItem == "garage") { AddGarage(); }
            if (App.net.CurrentItem == "timber") { AddTimber(); }
            if (App.net.CurrentItem == "cons") { AddCons(); }
            if (App.net.CurrentItem == "lock") { AddLock(); }
            if (App.net.CurrentItem == "comp") { AddComp(); }
            if (App.net.CurrentItem == "bifold") { AddBifold(); }
        }

        private void AddUPVC()
        {
            App.net.table_init.CreateUpvc();
            App.net.root_item_number = App.net.UPVCRecord.item_number;

            App.data.SaveHeader();

            Navigation.PushAsync(new RepairOrReplace(t_current_item.item_upvc), false);
        }

        private void AddTimber()
        {
            App.net.table_init.CreateTimber();
            App.net.root_item_number = App.net.TimberRecord.item_number;

            App.data.SaveHeader();

            Navigation.PushAsync(new RepairOrReplace(t_current_item.item_timber), false);
        }

        private void AddAlum()
        {
            App.net.table_init.CreateAlum();
            App.net.root_item_number = App.net.AlumRecord.item_number;

            App.data.SaveHeader();

            Navigation.PushAsync(new RepairOrReplace(t_current_item.item_aluminium), false);
        }

        private void AddGlass()
        {
            App.net.table_init.CreateGlass();
            App.net.root_item_number = App.net.GlassRecord.item_number;

            App.data.SaveHeader();

            Navigation.PushAsync(new GlassItem(), false);
        }

        private void AddBifold()
        {
            App.net.table_init.CreateBifold();
            App.net.root_item_number = App.net.BifoldRecord.item_number;

            App.data.SaveHeader();

            Navigation.PushAsync(new RepairOrReplace(t_current_item.item_bifolding), false);
        }

        private void AddCons()
        {
            App.net.table_init.CreateConservatory();
            App.net.root_item_number = App.net.ConsRecord.item_number;

            App.data.SaveHeader();

            Navigation.PushAsync(new RepairOrReplace(t_current_item.item_conservatory), false);
        }

        private void AddPanel()
        {
            App.net.table_init.CreatePanel();
            App.net.root_item_number = App.net.PanelRecord.item_number;

            App.data.SaveHeader();

            Navigation.PushAsync(new Panel(), false);
            //Navigation.PushAsync(new PanelFitter(), false);
            //NavigationService.Navigate(new Uri("/Items/Panel/Panel1.xaml", UriKind.Relative));
        }

        private void AddGreen()
        {
            App.net.table_init.CreateGreenhouse();
            App.net.root_item_number = App.net.GreenRecord.item_number;

            App.data.SaveHeader();

            Navigation.PushAsync(new Greenhouse(), false);
        }

        private void AddLock()
        {
            App.net.table_init.CreateLock();
            App.net.root_item_number = App.net.LockingRecord.item_number;

            App.data.SaveHeader();

            Navigation.PushAsync(new LockTypeChoose(), false);
        }

        private void AddComp()
        {
            App.net.table_init.CreateComposite();
            App.net.root_item_number = App.net.CompRecord.item_number;

            App.data.SaveHeader();

            Navigation.PushAsync(new RepairOrReplace(t_current_item.item_composite), false);
        }

        private void AddGarage()
        {
            App.net.table_init.CreateGarage();
            App.net.root_item_number = App.net.GarageRecord.item_number;

            App.data.SaveHeader();

            Navigation.PushAsync(new GarageItem(), false);
        }

        public AllBrowse()
        {
            InitializeComponent();

            if (App.net.CurrentItem == "panel") { Title = "Panel"; }
            if (App.net.CurrentItem == "upvc") { Title = "UPVC"; }
            if (App.net.CurrentItem == "glass") { Title = "Glass"; }
            if (App.net.CurrentItem == "alum") { Title = "Aluminium"; }
            if (App.net.CurrentItem == "garage") { Title = "Garage"; }
            if (App.net.CurrentItem == "timber") { Title = "Timber"; }
            if (App.net.CurrentItem == "cons") { Title = "Conservatory"; }
            if (App.net.CurrentItem == "lock") { Title = "Locking Mechanism"; }
            if (App.net.CurrentItem == "comp") { Title = "Composite Door"; }
            if (App.net.CurrentItem == "green") { Title = "Greenhouse/Sheds"; }
            if (App.net.CurrentItem == "bifold") { Title = "Folding Sliding Doors"; }

            if (App.net.HeaderRecord.iRecordType > 0)
            {
                add_button.IsVisible = false;
                edit_button.IsVisible = false;
                delete_button.IsVisible = false;
                copy_button.IsVisible = false;
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            bSelected = false;
            child_screen_active = false;
            DrawList();
        }

        private void OnAdd(object sender, EventArgs e)
        {
            AddNew();
        }

        private async void OnDelete(object sender, EventArgs e)
        {
            if (selected_data != null)
            {
                var answer = await DisplayAlert("Delete item?", "", "   Yes   ", "   No   ");
                if (answer == true)
                {
                    switch (App.net.CurrentItem)
                    {
                        case "panel": App.data.DeletePanelByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number); break;
                        case "upvc": App.data.DeleteUPVCByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number); break;
                        case "timber": App.data.DeleteTimberByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number); break;
                        case "glass": App.data.DeleteGlassByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number); break;
                        case "bifold": App.data.DeleteBifoldByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number); break;
                        case "green": App.data.DeleteGreenByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number); break;
                        case "comp": App.data.DeleteCompByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number); break;
                        case "cons": App.data.DeleteConsByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number); break;
                        case "garage": App.data.DeleteGarageByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number); break;
                        case "lock": App.data.DeleteLockByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number); break;
                        case "alum": App.data.DeleteAlumByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number); break;
                    }
                    DrawList();
                }
            }
        }

        private async void OnCopy(object sender, EventArgs e)
        {
            if (selected_data != null)
            {
                var answer = await DisplayAlert("Copy item?", "", "   Yes   ", "   No   ");
                if (answer == true)
                {
                    switch(App.net.CurrentItem)
                    {
                        case "panel": App.data.CopyPanelByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number); break;
                        case "upvc": App.data.CopyUPVCByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number); break;
                        case "timber": App.data.CopyTimberByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number); break;
                        case "glass": App.data.CopyGlassByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number); break;
                        case "bifold": App.data.CopyBifoldByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number); break;
                        case "green": App.data.CopyGreenByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number); break;
                        case "comp": App.data.CopyCompByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number); break;
                        case "cons": App.data.CopyConsByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number); break;
                        case "garage": App.data.CopyGarageByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number); break;
                        case "lock": App.data.CopyLockByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number); break;
                        case "alum": App.data.CopyAlumByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number); break;
                    }
                    DrawList();
                }
            }
        }

        private void OnSelect(object sender, EventArgs e)
        {
            if (bSelected)
                DoSelect();
        }

        private void DoSelect()
        {
            if (!child_screen_active) // Some users manage to tap 3x at exactly the wrong speed, thus causing DoSelect to be called a 2nd time before first is processed.
            {
                child_screen_active = true;

                if (App.net.CurrentItem == "panel")
                {
                    App.net.PanelRecord = App.data.GetPanelByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number);
                    if (App.net.PanelRecord != null)
                    {
                        App.net.loaded_item_number = App.net.PanelRecord.item_number;
                        App.net.root_item_number = App.net.PanelRecord.item_number;
                        if(App.net.HeaderRecord.iRecordType>0)
                            Navigation.PushAsync(new ViewPanel(), false);
                        else
                            Navigation.PushAsync(new Panel(), false);
                    }
                }
                if (App.net.CurrentItem == "upvc")
                {
                    App.net.UPVCRecord = App.data.GetUPVCByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number);
                    if (App.net.UPVCRecord != null)
                    {
                        App.net.loaded_item_number = App.net.UPVCRecord.item_number;
                        App.net.root_item_number = App.net.UPVCRecord.item_number;
                        if (App.net.HeaderRecord.iRecordType > 0)
                            Navigation.PushAsync(new ViewUPVC(), false);
                        else
                            Navigation.PushAsync(new UPVCitem(), false);
                    }
                }
                if (App.net.CurrentItem == "timber")
                {
                    App.net.TimberRecord = App.data.GetTimberByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number);
                    if (App.net.TimberRecord != null)
                    {
                        App.net.loaded_item_number = App.net.TimberRecord.item_number;
                        App.net.root_item_number = App.net.TimberRecord.item_number;
                        if (App.net.HeaderRecord.iRecordType > 0)
                            Navigation.PushAsync(new ViewTimber(), false);
                        else
                            Navigation.PushAsync(new TimberItem(), false);
                    }
                }
                if (App.net.CurrentItem == "glass")
                {
                    App.net.GlassRecord = App.data.GetGlassByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number);
                    if (App.net.GlassRecord != null)
                    {
                        App.net.loaded_item_number = App.net.GlassRecord.item_number;
                        App.net.root_item_number = App.net.GlassRecord.item_number;
                        if (App.net.HeaderRecord.iRecordType > 0)
                            Navigation.PushAsync(new ViewGlass(), false);
                        else
                            Navigation.PushAsync(new GlassItem(), false);
                    }
                }
                if (App.net.CurrentItem == "bifold")
                {
                    App.net.BifoldRecord = App.data.GetBifoldByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number);
                    if (App.net.BifoldRecord != null)
                    {
                        App.net.loaded_item_number = App.net.BifoldRecord.item_number;
                        App.net.root_item_number = App.net.BifoldRecord.item_number;
                        if (App.net.HeaderRecord.iRecordType > 0)
                            Navigation.PushAsync(new ViewBifold(), false);
                        else
                            Navigation.PushAsync(new BifoldItem(), false);
                    }
                }
                if (App.net.CurrentItem == "green")
                {
                    App.net.GreenRecord = App.data.GetGreenByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number);
                    if (App.net.GreenRecord != null)
                    {
                        App.net.loaded_item_number = App.net.GreenRecord.item_number;
                        App.net.root_item_number = App.net.GreenRecord.item_number;
                        if (App.net.HeaderRecord.iRecordType > 0)
                            Navigation.PushAsync(new ViewGreenhouse(), false);
                        else
                            Navigation.PushAsync(new Greenhouse(), false);
                    }
                }
                if (App.net.CurrentItem == "comp")
                {
                    App.net.CompRecord = App.data.GetCompByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number);
                    if (App.net.CompRecord != null)
                    {
                        App.net.loaded_item_number = App.net.CompRecord.item_number;
                        App.net.root_item_number = App.net.CompRecord.item_number;
                        if (App.net.HeaderRecord.iRecordType > 0)
                            Navigation.PushAsync(new ViewComposite(), false);
                        else
                            Navigation.PushAsync(new CompositeDoor(), false);
                    }
                }
                if (App.net.CurrentItem == "cons")
                {
                    App.net.ConsRecord = App.data.GetConsByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number);
                    if (App.net.ConsRecord != null)
                    {
                        App.net.loaded_item_number = App.net.ConsRecord.item_number;
                        App.net.root_item_number = App.net.ConsRecord.item_number;
                        if (App.net.HeaderRecord.iRecordType > 0)
                            Navigation.PushAsync(new ViewConservatory(), false);
                        else
                            Navigation.PushAsync(new ConservatoryItem(), false);
                    }
                }
                if (App.net.CurrentItem == "garage")
                {
                    App.net.GarageRecord = App.data.GetGarageByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number);
                    if (App.net.GarageRecord != null)
                    {
                        App.net.loaded_item_number = App.net.GarageRecord.item_number;
                        App.net.root_item_number = App.net.GarageRecord.item_number;
                        if (App.net.HeaderRecord.iRecordType > 0)
                            Navigation.PushAsync(new ViewGarage(), false);
                        else
                            Navigation.PushAsync(new GarageItem(), false);
                    }
                }
                if (App.net.CurrentItem == "lock")
                {
                    App.net.LockingRecord = App.data.GetLockByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number);
                    if (App.net.LockingRecord != null)
                    {
                        App.net.loaded_item_number = App.net.LockingRecord.item_number;
                        App.net.root_item_number = App.net.LockingRecord.item_number;

                        if (App.net.LockingRecord.bMulti == true)
                        {
                            if (App.net.HeaderRecord.iRecordType > 0)
                                Navigation.PushAsync(new ViewLocking(), false);
                            else
                                Navigation.PushAsync(new LockItem(t_locking_item_mode.multipoint_lock), false);
                        }
                        else
                        {
                            if (App.net.HeaderRecord.iRecordType > 0)
                                Navigation.PushAsync(new ViewLocking(), false);
                            else
                                Navigation.PushAsync(new LockItem(t_locking_item_mode.general_lock), false);
                        }
                    }
                }
                if (App.net.CurrentItem == "alum")
                {
                    App.net.AlumRecord = App.data.GetAlumByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number);
                    if (App.net.AlumRecord != null)
                    {
                        App.net.loaded_item_number = App.net.AlumRecord.item_number;
                        App.net.root_item_number = App.net.AlumRecord.item_number;
                        if (App.net.HeaderRecord.iRecordType > 0)
                            Navigation.PushAsync(new ViewAluminium(), false);
                        else
                            Navigation.PushAsync(new AluminiumItem(), false);
                    }
                }
            }
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (selected_data == (listView as ListView).SelectedItem as ListData && bSelected)
                DoSelect();
            else
            {
                selected_data = (listView as ListView).SelectedItem as ListData;
                bSelected = true;
            }
        }

        private void OnView(object sender, EventArgs e)
        {
            if (bSelected)
            {
                if (App.net.CurrentItem == "panel")
                {
                    App.net.PanelRecord = App.data.GetPanelByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number);
                    if (App.net.PanelRecord != null)
                    {
                        App.net.loaded_item_number = App.net.PanelRecord.item_number;
                        App.net.root_item_number = App.net.PanelRecord.item_number;
                        Navigation.PushAsync(new ViewPanel(), false);
                    }
                }
                if (App.net.CurrentItem == "upvc")
                {
                    App.net.UPVCRecord = App.data.GetUPVCByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number);
                    if (App.net.UPVCRecord != null)
                    {
                        App.net.loaded_item_number = App.net.UPVCRecord.item_number;
                        App.net.root_item_number = App.net.UPVCRecord.item_number;
                        Navigation.PushAsync(new ViewUPVC(), false);
                    }
                }
                if (App.net.CurrentItem == "timber")
                {
                    App.net.TimberRecord = App.data.GetTimberByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number);
                    if (App.net.TimberRecord != null)
                    {
                        App.net.loaded_item_number = App.net.TimberRecord.item_number;
                        App.net.root_item_number = App.net.TimberRecord.item_number;
                        Navigation.PushAsync(new ViewTimber(), false);
                    }
                }
                if (App.net.CurrentItem == "glass")
                {
                    App.net.GlassRecord = App.data.GetGlassByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number);
                    if (App.net.GlassRecord != null)
                    {
                        App.net.loaded_item_number = App.net.GlassRecord.item_number;
                        App.net.root_item_number = App.net.GlassRecord.item_number;
                        Navigation.PushAsync(new ViewGlass(), false);
                    }
                }
                if (App.net.CurrentItem == "bifold")
                {
                    App.net.BifoldRecord = App.data.GetBifoldByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number);
                    if (App.net.BifoldRecord != null)
                    {
                        App.net.loaded_item_number = App.net.BifoldRecord.item_number;
                        App.net.root_item_number = App.net.BifoldRecord.item_number;
                        Navigation.PushAsync(new ViewBifold(), false);
                    }
                }
                if (App.net.CurrentItem == "green")
                {
                    App.net.GreenRecord = App.data.GetGreenByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number);
                    if (App.net.GreenRecord != null)
                    {
                        App.net.loaded_item_number = App.net.GreenRecord.item_number;
                        App.net.root_item_number = App.net.GreenRecord.item_number;
                        Navigation.PushAsync(new ViewGreenhouse(), false);
                    }
                }
                if (App.net.CurrentItem == "comp")
                {
                    App.net.CompRecord = App.data.GetCompByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number);
                    if (App.net.CompRecord != null)
                    {
                        App.net.loaded_item_number = App.net.CompRecord.item_number;
                        App.net.root_item_number = App.net.CompRecord.item_number;
                        Navigation.PushAsync(new ViewComposite(), false);
                    }
                }
                if (App.net.CurrentItem == "cons")
                {
                    App.net.ConsRecord = App.data.GetConsByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number);
                    if (App.net.ConsRecord != null)
                    {
                        App.net.loaded_item_number = App.net.ConsRecord.item_number;
                        App.net.root_item_number = App.net.ConsRecord.item_number;
                        Navigation.PushAsync(new ViewConservatory(), false);
                    }
                }
                if (App.net.CurrentItem == "garage")
                {
                    App.net.GarageRecord = App.data.GetGarageByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number);
                    if (App.net.GarageRecord != null)
                    {
                        App.net.loaded_item_number = App.net.GarageRecord.item_number;
                        App.net.root_item_number = App.net.GarageRecord.item_number;
                        Navigation.PushAsync(new ViewGarage(), false);
                    }
                }
                if (App.net.CurrentItem == "lock")
                {
                    App.net.LockingRecord = App.data.GetLockByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number);
                    if (App.net.LockingRecord != null)
                    {
                        App.net.loaded_item_number = App.net.LockingRecord.item_number;
                        App.net.root_item_number = App.net.LockingRecord.item_number;
                        Navigation.PushAsync(new ViewLocking(), false);
                    }
                }
                if (App.net.CurrentItem == "alum")
                {
                    App.net.AlumRecord = App.data.GetAlumByContractItemNo(App.net.HeaderRecord.udi_cont, selected_data.item_number);
                    if (App.net.AlumRecord != null)
                    {
                        App.net.loaded_item_number = App.net.AlumRecord.item_number;
                        App.net.root_item_number = App.net.AlumRecord.item_number;

                        Navigation.PushAsync(new ViewAluminium(), false);
                    }
                }
            }
        }
    }
}
