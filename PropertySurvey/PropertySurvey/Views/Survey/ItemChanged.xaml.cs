using SignaturePad.Forms;
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
	public partial class ItemChanged : ContentPage
	{
        string item_type;

        public ItemChanged ()
		{
			InitializeComponent ();

            signaturePad.CaptionText = "";
            printed_name.Text = App.net.HeaderRecord.uc_name;
        }

        private void SignatureChanged(object sender, EventArgs e)
        {
            save_button.IsEnabled = true;
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            if (true)//(save_button.IsEnabled == true)
            {
                Save();
            }
            return true;
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            Save();
        }

        private async void Save()
        {
            string fname = "";
            SaveData();
            using (var bitmap = await signaturePad.GetImageStreamAsync(SignatureImageFormat.Jpeg, Color.Black, Color.White, 1f))
            {
                int num = App.net.random.Next(100000);
                fname = string.Format("Signatures/{0:00000000}_s{1:0}", App.CurrentApp.HeaderRecord.udi_cont, item_type);
                fname = fname + string.Format("{0:00}____.jpg", App.CurrentApp.loaded_item_number);
                App.files.SaveStream(fname, bitmap);
            }
            await Navigation.PopAsync(false);
        }

        private void SaveData()
        {
            switch (App.CurrentApp.CurrentItem)
            {
                case "timber":
                    App.CurrentApp.TimberRecord.ChangeItemTo = changed_to.Text;
                    App.CurrentApp.TimberRecord.print_name = printed_name.Text;
                    App.CurrentApp.TimberRecord.bDifferentFromOriginal = true;
                    App.data.SaveTimber();
                    item_type = "T";
                    break;
                case "alum":
                    App.CurrentApp.AlumRecord.ChangeItemTo = changed_to.Text;
                    App.CurrentApp.AlumRecord.print_name = printed_name.Text;
                    App.CurrentApp.AlumRecord.bDifferentFromOriginal = true;
                    App.data.SaveAlum();
                    item_type = "A";
                    break;
                case "upvc":
                    App.CurrentApp.UPVCRecord.ChangeItemTo = changed_to.Text;
                    App.CurrentApp.UPVCRecord.print_name = printed_name.Text;
                    App.CurrentApp.UPVCRecord.bDifferentFromOriginal = true;
                    App.data.SaveUPVC();
                    item_type = "U";
                    break;
                case "comp":
                    App.CurrentApp.CompRecord.ChangeItemTo = changed_to.Text;
                    App.CurrentApp.CompRecord.print_name = printed_name.Text;
                    App.CurrentApp.CompRecord.bDifferentFromOriginal = true;
                    App.data.SaveComp();
                    item_type = "C";
                    break;
                case "panel":
                    App.CurrentApp.PanelRecord.ChangeItemTo = changed_to.Text;
                    App.CurrentApp.PanelRecord.print_name = printed_name.Text;
                    App.CurrentApp.PanelRecord.bDifferentFromOriginal = true;
                    App.data.SavePanel();
                    item_type = "P";
                    break;
                case "garage":
                    App.CurrentApp.GarageRecord.ChangeItemTo = changed_to.Text;
                    App.CurrentApp.GarageRecord.print_name = printed_name.Text;
                    App.CurrentApp.GarageRecord.bDifferentFromOriginal = true;
                    App.data.SaveGarage();
                    item_type = "G";
                    break;
                case "cons":
                    App.CurrentApp.ConsRecord.ChangeItemTo = changed_to.Text;
                    App.CurrentApp.ConsRecord.print_name = printed_name.Text;
                    App.CurrentApp.ConsRecord.bDifferentFromOriginal = true;
                    App.data.SaveCons();
                    item_type = "C";
                    break;
                case "lock":
                    App.CurrentApp.LockingRecord.ChangeItemTo = changed_to.Text;
                    App.CurrentApp.LockingRecord.print_name = printed_name.Text;
                    App.CurrentApp.LockingRecord.bDifferentFromOriginal = true;
                    App.data.SaveLocking();
                    item_type = "T";
                    break;
                case "glass":
                    App.CurrentApp.GlassRecord.ChangeItemTo = changed_to.Text;
                    App.CurrentApp.GlassRecord.print_name = printed_name.Text;
                    App.CurrentApp.GlassRecord.bDifferentFromOriginal = true;
                    App.data.SaveGlass();
                    item_type = "L";
                    break;
                case "green":
                    App.CurrentApp.GreenRecord.ChangeItemTo = changed_to.Text;
                    App.CurrentApp.GreenRecord.print_name = printed_name.Text;
                    App.CurrentApp.GreenRecord.bDifferentFromOriginal = true;
                    App.data.SaveGreen();
                    item_type = "R";
                    break;
                case "bifold":
                    App.CurrentApp.BifoldRecord.ChangeItemTo = changed_to.Text;
                    App.CurrentApp.BifoldRecord.print_name = printed_name.Text;
                    App.CurrentApp.BifoldRecord.bDifferentFromOriginal = true;
                    App.data.SaveBifold();
                    item_type = "B";
                    break;
            }
        }
    }
}