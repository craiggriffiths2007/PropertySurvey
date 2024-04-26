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
    public partial class Images : ContentPage
    {
        int inovids = 0;
        int inophotos = 0;
        int inodrawings = 0;

        public Images()
        {
            InitializeComponent();
            App.net.load_template_image = false;

            if (App.net.CurrentItem == "timber")
            {
                if (App.CurrentApp.TimberRecord.timber_item != "Window")
                {
                    ral_signature.IsVisible = true;
                }
                else
                {

                }
            }
        }
        protected override void OnAppearing()
        {
            if (App.net.RootItem == "panel")
            {
                inophotos = App.net.PanelRecord.no_of_photos;
                inodrawings = App.net.PanelRecord.no_of_pics;
                inovids = App.net.PanelRecord.no_of_vids;
            }
            if (App.net.RootItem == "upvc")
            {
                inophotos = App.net.UPVCRecord.no_of_photos;
                inodrawings = App.net.UPVCRecord.no_of_pics;
                inovids = App.net.UPVCRecord.no_of_vids;
            }
            if (App.net.RootItem == "glass")
            {
                inophotos = App.net.GlassRecord.no_of_photos;
                inodrawings = App.net.GlassRecord.no_of_pics;
                inovids = App.net.GlassRecord.no_of_vids;
            }
            if (App.net.RootItem == "alum")
            {
                inophotos = App.net.AlumRecord.no_of_photos;
                inodrawings = App.net.AlumRecord.no_of_pics;
                inovids = App.net.AlumRecord.no_of_vids;
            }
            if (App.net.RootItem == "garage")
            {
                inophotos = App.net.GarageRecord.no_of_photos;
                inodrawings = App.net.GarageRecord.no_of_pics;
                inovids = App.net.GarageRecord.no_of_vids;
            }
            if (App.net.RootItem == "timber")
            {
                inophotos = App.net.TimberRecord.no_of_photos;
                inodrawings = App.net.TimberRecord.no_of_pics;
                inovids = App.net.TimberRecord.no_of_vids;

                timber_drawings.IsVisible = true;
            }
            if (App.net.RootItem == "cons")
            {
                inophotos = App.net.ConsRecord.no_of_photos;
                inodrawings = App.net.ConsRecord.no_of_pics;
                inovids = App.net.ConsRecord.no_of_vids;
            }
            if (App.net.RootItem == "bifold")
            {
                inophotos = App.net.BifoldRecord.no_of_photos;
                inodrawings = App.net.BifoldRecord.no_of_pics;
                inovids = App.net.BifoldRecord.no_of_vids;
            }
            if (App.net.RootItem == "lock")
            {
                inophotos = App.net.LockingRecord.no_of_photos;
                inodrawings = App.net.LockingRecord.no_of_pics;
                inovids = App.net.LockingRecord.no_of_vids;
            }
            if (App.net.RootItem == "comp")
            {
                inophotos = App.net.CompRecord.no_of_photos;
                inodrawings = App.net.CompRecord.no_of_pics;
                inovids = App.net.CompRecord.no_of_vids;
            }
            if (inovids > 0)
            {
                novideos.Text = "Video - " + inovids.ToString();
            }
            else
            {
                novideos.Text = "Video";
            }
            if (inophotos > 0)
            {
                nophotos.Text = "Photo - " + inophotos.ToString();
            }
            else
            {
                nophotos.Text = "Photo";
            }
            if (inodrawings > 0)
            {
                nodrawings.Text = "Drawing - " + inodrawings.ToString();
            }
            else
            {
                nodrawings.Text = "Drawing";
            }
            if (App.net.CurrentItem == "timber")
            {
                if (App.net.TimberRecord.bSectionDrawn == true)
                {
                    section_drawing.Text = "Section - OK";
                }
                if (App.net.TimberRecord.bMouldingDrawn == true)
                {
                    beading_drawing.Text = "Beading - OK";
                }
                if (App.net.TimberRecord.new_sash_required != 1)
                {
                    sash_drawing.IsVisible = false;
                }
                if (App.net.TimberRecord.bSashDrawn == true)
                {
                    sash_drawing.Text = "Sash - OK";
                }
                if (App.net.HeaderRecord.iRecordType > 0)
                {
                    ral_signature.IsVisible = false;
                }
                else
                {
                    if (App.net.TimberRecord.b_signed == true)
                    {
                        ral_signature.Text = "Colour Signature - OK";
                    }
                }
            }

            App.data.SaveItem();
        }

        private void OnPhotographs(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Camera(), false);
        }

        private async void OnDrawings(object sender, EventArgs e)
        {
            App.net.drawing_type = "normal";
            if (inodrawings == 0 && App.net.HeaderRecord.iRecordType == 0)
            {
                Navigation.InsertPageBefore(new Drawings(), this);
                Navigation.InsertPageBefore(new OrTemplate(), this);
                await Navigation.PopAsync(false);
            }
            else
            {
                await Navigation.PushAsync(new Drawings(), false);
            }
        }

        private void OnSection(object sender, EventArgs e)
        {
            App.net.drawing_type = "section_template";
            if (App.net.TimberRecord.bSectionDrawn == true || App.net.HeaderRecord.iRecordType > 0)
            {
                App.net.drawing_edit_mode = true;
                Navigation.PushAsync(new DrawingPage(), false);
            }
            else
            {
                Navigation.PushAsync(new OrTemplate(), false);
            }
        }

        private void OnBeading(object sender, EventArgs e)
        {
            App.net.drawing_type = "beading_template";
            if (App.net.TimberRecord.bMouldingDrawn == true || App.net.HeaderRecord.iRecordType > 0)
            {
                App.net.drawing_edit_mode = true;
                Navigation.PushAsync(new DrawingPage(), false);
            }
            else
            {
                Navigation.PushAsync(new OrTemplate(), false);
            }
        }

        private void OnSash(object sender, EventArgs e)
        {
            App.net.drawing_type = "sash";
            if (App.net.TimberRecord.bSashDrawn == true || App.net.HeaderRecord.iRecordType > 0)
            {
                App.net.drawing_edit_mode = true;
                Navigation.PushAsync(new DrawingPage(), false);
            }
            else
            {
                App.net.TimberRecord.bSashDrawn = true;
                Navigation.PushAsync(new OrTemplate(), false);
            }
        }

        private void OnVideos(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Videos(), false);
        }

        private void OnChangedTest(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ItemChanged(), false);
        }

        private async void OnRAL(object sender, EventArgs e)
        {
            if (App.net.CurrentItem == "timber")
            {
                if (App.CurrentApp.TimberRecord.b_signed == true)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        var response = await Application.Current.MainPage.DisplayAlert("", "Already Signed. Do you want to sign it again?", "   Yes   ", "   No   ");
                        if (response)
                        {
                            await Navigation.PushAsync(new RALSignature(), false);
                        }
                    });
                }
                else
                {
                    App.CurrentApp.TimberRecord.b_signed = true;
                    await Navigation.PushAsync(new RALSignature(), false);
                }
            }
        }
    }
}