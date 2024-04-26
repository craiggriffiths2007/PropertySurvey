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
    public partial class ContractCommentsAdd : ContentPage
    {
        public ContractCommentsAdd()
        {
            InitializeComponent();
        }

        private void OnAddComment(object sender, EventArgs e)
        {
            if (add_comment.Text.Length == 0)
            {
                DisplayAlert("", "Please enter a comment.", "");
            }
            else
            {
                App.CurrentApp.add_comment = 1;
                App.CurrentApp.comment_to_add = add_comment.Text;
                App.net.App_Settings.ContractComments = add_comment.Text;
                App.data.SaveSettings();

                Navigation.PopAsync(false);
            }
        }

        private void OnClearComment(object sender, EventArgs e)
        {
            add_comment.Text = "";
        }
    }
}