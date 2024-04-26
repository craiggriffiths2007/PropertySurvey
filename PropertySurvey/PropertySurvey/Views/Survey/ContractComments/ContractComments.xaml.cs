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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContractComments : ContentPage
    {
        string sendResponse = "";
        StringBuilder postData = new StringBuilder();
        string comment_text_to_send = "";

        public class ListData
        {
            public int uid { get; set; }
            public string logged_by { get; set; }
            public string date { get; set; }
            public string time { get; set; }
            public string comment { get; set; }

            public ListData(string _date, string _time, string _logged_by, string _comment)
            {
                this.logged_by = _logged_by;
                this.date = _date;
                this.time = _time;
                this.comment = _comment;
            }
        }
        
        public void GetComments()
        {
            string comments = "";

            act_ind.IsRunning = true;

            if (App.CurrentApp.add_comment == 1)
            {
                App.CurrentApp.add_comment = 0;
                comments = App.net.App_Settings.ContractComments;
                App.data.SaveSettings();
            }
            else
            {
                comments = "";
            }

            // Send PDA code and Branch code
            XDocument srcTree = new XDocument(
            new XComment("This is a comment"),
            new XElement("User",
            new XElement("ContractCommentUpdate", comments),
            new XElement("TheContractNumber", cont_num.Text),
            new XElement("PhoneSerial", App.net.phone_serial)));

            App.CurrentApp.comment_to_add = "";

            int i;
            byte[] data;

            postData = new StringBuilder();
            postData.Append(srcTree.ToString());

            Uri thisuri = new Uri(App.net.App_Settings.set_url + "/WM7Communication/WM7DownloadContractComments");
            HttpHelper helper = new HttpHelper(thisuri, "POST",
            new KeyValuePair<string, string>("ContractFile", "<JobData>" + postData.ToString() + "</JobData>"));
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
            act_ind.IsRunning = false;
            if (sendResponse == "<InvalidSerial>")
            {
                DisplayAlert("", "Invalid serial number", "OK");
                //MessageBox.Show("Invalid serial number.");
                //NavigationService.GoBack();
            }
            else
            {
                if (sendResponse == "<Locked>")
                {
                    DisplayAlert("", "The contract is locked at head office however the comment has gone into a que and will be added later.", "OK");
                }
                else
                {
                    if (sendResponse == "<NotFound>")
                    {
                        DisplayAlert("", "Contract not found.", "OK");
                        //NavigationService.GoBack();
                    }
                    else
                    {
                        if (sendResponse == "<NoComments>")
                        {
                            List<ListData> dataSource = new List<ListData>();
                            listView.ItemsSource = dataSource;
                        }
                        else
                        {
                            // Parse comments ad add to list
                            if (sendResponse.Substring(0, 18) == "<ContractComments>")
                            {
                                List<ListData> dataSource = new List<ListData>();

                                try
                                {
                                    bool bInfo = false;
                                    bool bComments = false;
                                    XDocument xml = XDocument.Parse(sendResponse);

                                    foreach (var word in xml.Element("ContractComments").Elements())
                                    {
                                        string ContractName = "";

                                        try
                                        {
                                            ContractName = (string)word.Element("ContractName");
                                        }
                                        catch (Exception e)
                                        {

                                        }

                                        if (ContractName != null && ContractName.Length > 0)
                                        {
                                            string ContractAdd1 = (string)word.Element("ContractAdd1");
                                            string ContractAdd2 = (string)word.Element("ContractAdd2");
                                            string ContractAdd3 = (string)word.Element("ContractAdd3");
                                            string ContractAdd4 = (string)word.Element("ContractAdd4");
                                            string ContractPCode = (string)word.Element("ContractPCode");

                                            string ContractHPhone = (string)word.Element("ContractHPhone");
                                            string ContractWPhone = (string)word.Element("ContractWPhone");
                                            string ContractMPhone = (string)word.Element("ContractMPhone");

                                            string ContractPhoneAdd1 = (string)word.Element("ContractAddPhone1");
                                            string ContractPhoneAdd2 = (string)word.Element("ContractAddPhone2");

                                            App.net.App_Settings.Contractnumber = cont_num.Text;

                                            App.net.App_Settings.ContractName = ContractName;
                                            App.net.App_Settings.ContractAdd1 = ContractAdd1;
                                            App.net.App_Settings.ContractAdd2 = ContractAdd2;
                                            App.net.App_Settings.ContractAdd3 = ContractAdd3;
                                            App.net.App_Settings.ContractAdd4 = ContractAdd4;
                                            App.net.App_Settings.ContractPCode = ContractPCode;
                                            App.net.App_Settings.ContractHPhone = ContractHPhone;
                                            App.net.App_Settings.ContractWPhone = ContractWPhone;
                                            App.net.App_Settings.ContractMPhone = ContractMPhone;

                                            App.net.App_Settings.ContractAddPhone1 = ContractPhoneAdd1;
                                            App.net.App_Settings.ContractAddPhone2 = ContractPhoneAdd2;


                                            //TheSettings.ContractComments = App.CurrentApp.comment_to_add;

                                            App.data.SaveSettings();

                                            bInfo = true;
                                        }
                                        else
                                        {
                                            string CommentDate = (string)word.Element("CommentDate");
                                            if (CommentDate != null)
                                            {
                                                string CommentTime = (string)word.Element("CommentTime");
                                                string CommentUser = (string)word.Element("CommentUser");
                                                string CommentText = (string)word.Element("CommentText");
                                                bComments = true;
                                                dataSource.Add(new ListData(CommentDate, CommentTime, CommentUser, CommentText.Trim()));
                                            }
                                        }
                                    }

                                    dataSource.Reverse();

                                    if (bComments == true)
                                    {
                                        add_comment_button.IsEnabled = true;
                                        listView.ItemsSource = dataSource;
                                    }

                                    if (bInfo == true)
                                    {
                                        view_info_button.IsEnabled = true;
                                        //NavigationService.Navigate(new Uri("/ContractInfo.xaml", UriKind.Relative));
                                    }
                                }
                                catch (Exception e)
                                {
                                    //MessageBox.Show("There was an error parsing the messages.");
                                    listView.ItemsSource = dataSource;
                                }
                            }
                        }
                    }
                }
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (App.CurrentApp.add_comment == 1)
            {
                // App.CurrentApp.add_comment = 0;
                comment_text_to_send = App.CurrentApp.comment_to_add;

                GetComments();
            }
        }

        public ContractComments()
        {
            InitializeComponent();

            cont_num.Focus();
        }

        private void OnFind(object sender, EventArgs e)
        {
            comment_text_to_send = "";
            GetComments();
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private void OnViewInfo(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ContractInfo(), false);
        }

        private void OnAddComment(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ContractCommentsAdd(), false);
        }
    }
}