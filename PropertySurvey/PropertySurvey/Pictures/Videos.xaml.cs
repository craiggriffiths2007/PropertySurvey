using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Videos : ContentPage
    {
        List<string> fileNames = new List<string>();

        int total_videos = 0;
        public class ListData
        {
            public ImageSource thumbnail { get; set; }
            public long file_size { get; set; }
            public string filename { get; set; }
            public string only_filename { get; set; }

            public string size_formatted { get; set; }
            public ListData(ImageSource thumbnail, string filename,string only_filename, long file_size)
            {
                this.thumbnail = thumbnail;
                this.filename = filename;
                this.only_filename = only_filename;
                this.file_size = file_size / (long)1000.0f;
                this.size_formatted = String.Format("{0:0,0}", file_size);
            }
        }

        bool bSelected = false;

        ListData selected_data = null;

        private void CreateList()
        {
            List<ListData> dataSource = new List<ListData>();

            foreach (var item in fileNames)
            {
                string fname = item.Substring(0, item.Length - 3) + "jpg";
                bool bexists = App.files.FileExists(Path.Combine("Photos", fname));
                if (bexists == true)
                {
                    dataSource.Add(new ListData(ImageSource.FromFile(App.files.GetLocalFilePath(Path.Combine("Photos", fname))), App.files.GetLocalFilePath(Path.Combine("Photos", item)), item, App.files.FileSize(Path.Combine("Photos", item))));
                }
            }
            listView.ItemsSource = dataSource;
        }

        public Videos()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            bSelected = false;
            selected_data = null;
            UpdateFileList();
            CreateList();
        }

        private async void OnVideo(object sender, EventArgs e)
        {
            App.net.MakeVideoFilename();

            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return;
            }

            var file = await CrossMedia.Current.TakeVideoAsync(new Plugin.Media.Abstractions.StoreVideoOptions
            {
                Directory = "Photos",
                Name = App.net.photo_fname,
                Quality = Plugin.Media.Abstractions.VideoQuality.Medium,
                //DesiredSize = 5000000,
                CompressionQuality = 50,
                DesiredLength = System.TimeSpan.FromSeconds(10),

            }); ;

            if (file == null)
                return;

            byte[] vidbytes = ReadFully(file.GetStream());
            App.files.SaveBinary(Path.Combine("Photos", App.net.photo_fname), vidbytes);

            //byte[] bytes = DependencyService.Get<ICameraHelper>().GenerateThumbImage(App.files.GetLocalFilePath(Path.Combine("Photos", App.net.photo_fname)), 1);

            App.net.photo_fname = App.net.photo_fname.Substring(0, App.net.photo_fname.Length - 3) + "jpg";

            //App.files.SaveBinary(Path.Combine("Photos", App.net.photo_fname), bytes);

            UpdateFileList();
            CreateList();
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        private void UpdateFileList()
        {
            string fname = "";
            string dname = "Photos";

            int hival = 0;

            fname = string.Format("{0:00000000}_vAZ", App.net.HeaderRecord.udi_cont);
            fname += string.Format("{0:000}??.mp4", App.net.root_item_number);

            fileNames = App.files.GetFileList(dname, fname);

            total_videos = fileNames.Count();

            if(total_videos>0)
            {
                message.IsVisible = true;
            }
            else
            {
                message.IsVisible = false;
            }

            App.net.image_number = 0;
            for (int i = total_videos - 1; i > -1; i--)
            {
                if (fileNames[i].Length > 8)
                {
                    hival = Convert.ToInt32(fileNames[i].Substring(15, 2));

                    if (hival > App.net.image_number)
                    {
                        App.net.image_number = hival;
                    }
                }
            }
            App.net.image_number++;
        }

        private void DoSelect()
        {
            if (selected_data == (listView as ListView).SelectedItem as ListData && bSelected == false)
            {
                bSelected = true;
                Navigation.PushAsync(new VideoPlayback(selected_data.filename), false);
            }
            else
            {
                selected_data = (listView as ListView).SelectedItem as ListData;
            }
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            DoSelect();
        }

        private async void OnDel(object sender, EventArgs e)
        {
            if (selected_data != null)
            {
                var answer = await DisplayAlert("Delete this Video?", "", "   Yes   ", "   No   ");
                if (answer)
                {
                    if (App.files.FileExists("Photos/" + selected_data.only_filename) == true)
                    {
                        App.files.DeleteFile("Photos/" + selected_data.only_filename);
                        UpdateFileList();
                        CreateList();
                    }
                }
            }
        }
        protected override bool OnBackButtonPressed()
        {
            switch (App.net.CurrentItem)
            {
                case "panel": App.net.PanelRecord.no_of_vids = total_videos; break;
                case "upvc": App.net.UPVCRecord.no_of_vids = total_videos; break;
                case "glass": App.net.GlassRecord.no_of_vids = total_videos; break;
                case "alum": App.net.AlumRecord.no_of_vids = total_videos; break;
                case "garage": App.net.GarageRecord.no_of_vids = total_videos; break;
                case "timber": App.net.TimberRecord.no_of_vids = total_videos; break;
                case "cons": App.net.ConsRecord.no_of_vids = total_videos; break;
                case "lock": App.net.LockingRecord.no_of_vids = total_videos; break;
                case "comp": App.net.CompRecord.no_of_vids = total_videos; break;
                case "green": App.net.GreenRecord.no_of_vids = total_videos; break;
                case "bifold": App.net.BifoldRecord.no_of_vids = total_videos; break;
            }

            base.OnBackButtonPressed();
            return false;
        }
    }
}