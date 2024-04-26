using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Messages : ContentPage
    {
        public class ListData : INotifyPropertyChanged
        {
            public int uid { get; set; }
            public string fromname { get; set; }
            public string date { get; set; }
            public bool bRead { get; set; }
            public string subject { get; set; }

            public string _back_colour { get; set; }

            public string back_colour { get { return _back_colour; } set { _back_colour = value; NotifyPropertyChanged("back_colour"); } }

            public event PropertyChangedEventHandler PropertyChanged;
            private void NotifyPropertyChanged(String propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            public ListData(int uID, string _fromname, string _date, bool _bRead, string _subject)
            {
                this.uid = uID;
                this.fromname = _fromname;
                this.date = _date;
                this.bRead = _bRead;
                this.subject = _subject;
                this.back_colour = _fromname;

                if (bRead == true)
                {
                    this.back_colour = "#1881bf";
                }
                else
                {
                    this.back_colour = "#7ccb7e";
                }
            }
        }

        ListData selected_data = null;

        public class ListDatas : ObservableCollection<ListData>
        {
            //public ListDatas()
            //{

            //}
        }

        public static ListDatas dataSource = new ListDatas();

        public Messages()
        {
            InitializeComponent();


            App.net.App_Settings.new_mail = App.data.CountUnsentMessages();
            if (App.net.App_Settings.new_mail > 0)
            {
                Title = "Mail - " + App.net.App_Settings.new_mail.ToString();
            }
            else
            {
                Title = "Mail";
            }

            DrawList();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }

        private void DrawList()
        {
            List<Message_Text> messages = App.data.GetMessages();
            messages.Reverse();

            dataSource.Clear();
            foreach (var item in messages)
            {
                string sentdate = String.Format("{0:d}", item.message_date);
                string subject = "";

                if (item.message_text.Length > 29)
                    subject = item.message_text.Substring(0, 29) + "..";
                else
                    subject = item.message_text;

                dataSource.Add(new ListData(item.RecID, item.from, sentdate, item.bRead, subject));
            }

            listView.ItemsSource = dataSource;
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            selected_data = (listView as ListView).SelectedItem as ListData;

            if (selected_data != null)
            {
                DoSelect();
            }
        }

        private void DoSelect()
        {
            Message_Text message = App.data.GetMessage(selected_data.uid);

            GetImages(message.ID);

            message_text.Text = message.message_text;

            message.bRead = true;
            App.data.SaveMessage(message);
            selected_data.bRead = true;

            var item = dataSource.FirstOrDefault(i => i.uid == selected_data.uid);

            App.net.App_Settings.new_mail = App.data.CountUnsentMessages();
            if (App.net.App_Settings.new_mail > 0)
            {
                Title = "Mail - " + App.net.App_Settings.new_mail.ToString();
            }
            else
            {
                Title = "Mail";
            }

            if (item != null)
                item.back_colour = "#1881bf";
        }

        protected override bool OnBackButtonPressed()
        {
            App.net.App_Settings.new_mail = App.data.CountUnsentMessages();
            App.data.SaveSettings();
            base.OnBackButtonPressed();
            return false;
        }

        private void GetImages(string message_id)
        {
            int i = 1;

            image1.Source = null;
            image2.Source = null;
            image3.Source = null;
            image4.Source = null;
            image5.Source = null;
            image1.IsVisible = false;
            image2.IsVisible = false;
            image3.IsVisible = false;
            image4.IsVisible = false;
            image5.IsVisible = false;

            List<string> fileNames;
            fileNames = App.files.GetFileList("Photos", "Message*");

            foreach (var item in fileNames)
            {
                if (item.Substring(7, 5) == message_id)
                {
                    switch (i)
                    {
                        case 1: image1.Source = ImageSource.FromFile(App.files.CreatePathToFile("Photos/" + item)); image1.IsVisible = true; break;
                        case 2: image2.Source = ImageSource.FromFile(App.files.CreatePathToFile("Photos/" + item)); image2.IsVisible = true; break;
                        case 3: image3.Source = ImageSource.FromFile(App.files.CreatePathToFile("Photos/" + item)); image3.IsVisible = true; break;
                        case 4: image4.Source = ImageSource.FromFile(App.files.CreatePathToFile("Photos/" + item)); image4.IsVisible = true; break;
                        case 5: image5.Source = ImageSource.FromFile(App.files.CreatePathToFile("Photos/" + item)); image5.IsVisible = true; break;
                    }
                }
            }
        }
    }
}