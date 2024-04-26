using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.IO;

namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Drawings : ContentPage
    {
        List<string> fileNames;
        int current_drawing = 0;
        int total_drawings;
        int i;
        int imageNumber;

        public Drawings()
        {
            InitializeComponent();

            App.net.drawing_edit_mode = false;

            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += OnEdit;

            if (App.net.HeaderRecord.iRecordType > 0)
            {
                add_button.Text = "";
                del_button.Text = "";
            }
            else
            {
                image.GestureRecognizers.Add(tap);
            }
        }

        protected override void OnAppearing()
        {
            DrawScreen();
        }

        private void DrawPictureNumber()
        {
            if (total_drawings > 0)
            {
                picnum.Text = String.Format("{0:#;minus #}", current_drawing + 1) + "/" + String.Format("{0:#;minus #}", total_drawings);
            }
            else
            {
                picnum.Text = "0";
            }
        }

        void DrawScreen()
        {
            CountDrawings();

            if (total_drawings == 0)
            {
                DrawPictureNumber();
                image.Source = null;
            }
            else
            {
                LoadPicture();
            }
        }

        void CountDrawings()
        {
            imageNumber = 0;
            int hival = 0;

            string fname = string.Format("{0:00000000}_dAU", App.net.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}??.jpg", App.net.root_item_number);

            fileNames = App.files.GetFileList("Drawings", fname);
            total_drawings = fileNames.Count();

            for (i = total_drawings - 1; i > -1; i--)
            {
                if (fileNames[i].Length > 8)
                {
                    if (fileNames[i].Substring(0, 8) != App.net.HeaderRecord.udi_cont)
                    {
                        fileNames.RemoveAt(i);
                    }
                    else
                    {
                        try
                        {
                            hival = Convert.ToInt32(fileNames[i].Substring(15, 2));

                            if (hival > imageNumber)
                            {
                                imageNumber = hival;
                            }
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
                else
                {
                    fileNames.RemoveAt(i);
                }
            }
            imageNumber++;
            App.net.vid_image_num = imageNumber;

            if (App.CurrentApp.camera_vehicle == 5 && imageNumber>0)
            {
                App.CurrentApp.AccidentRecord.c_drawings = true;
            }

            total_drawings = fileNames.Count;
            current_drawing = total_drawings - 1;
        }

        void LoadPicture()
        {
            if (fileNames.Count > 0)
            {
                image.Source = ImageSource.FromFile(App.files.CreatePathToFile("Drawings/" + fileNames[current_drawing]));
            }
            else
            {
                image.Source = null;
            }

            DrawPictureNumber();
        }

        private void OnL(object sender, EventArgs e)
        {
            current_drawing = current_drawing - 1;
            if (current_drawing < 0)
            {
                current_drawing = total_drawings - 1;
            }
            LoadPicture();
        }

        private void OnR(object sender, EventArgs e)
        {
            current_drawing = current_drawing + 1;
            if (current_drawing > total_drawings - 1)
            {
                current_drawing = 0;
            }
            LoadPicture();
        }

        private void OnAdd(object sender, EventArgs e)
        {
            if (App.CurrentApp.camera_vehicle == 5)
            {
                Navigation.PushAsync(new DrawingPage(), false);
            }
            else
            {
                if (App.net.HeaderRecord.iRecordType == 0)
                {
                    Navigation.PushAsync(new OrTemplate(), false);
                }
            }
        }

        private async void OnDel(object sender, EventArgs e)
        {
            if (App.net.HeaderRecord.iRecordType == 0)
            {
                if (current_drawing > -1 && current_drawing < total_drawings)
                {
                    var answer = await DisplayAlert("Delete this drawing?", "", "   Yes   ", "   No   ");
                    if (answer)
                    {
                        if (App.files.FileExists("Drawings/" + fileNames[current_drawing]) == true)
                        {
                            App.files.DeleteFile("Drawings/" + fileNames[current_drawing]);
                            DrawScreen();
                            if (current_drawing > total_drawings - 1)
                            {
                                current_drawing = total_drawings - 1;
                            }
                        }
                    }
                }
            }
        }

        private void OnEdit(object sender, EventArgs e)
        {
            if (current_drawing > -1 && current_drawing < total_drawings)
            {
                App.net.drawing_edit_mode = true;
                App.net.drawing_edit_current = current_drawing;

                App.net.drawing_edit_filename = fileNames[current_drawing];
                Navigation.PushAsync(new DrawingPage(), false);
            }
        }

        protected override bool OnBackButtonPressed()
        {
            CountDrawings();
            switch (App.net.CurrentItem)
            {
                case "panel": App.net.PanelRecord.no_of_pics = total_drawings;break;
                case "upvc": App.net.UPVCRecord.no_of_pics = total_drawings; break;
                case "glass": App.net.GlassRecord.no_of_pics = total_drawings; break;
                case "alum": App.net.AlumRecord.no_of_pics = total_drawings; break;
                case "garage": App.net.GarageRecord.no_of_pics = total_drawings; break;
                case "timber": App.net.TimberRecord.no_of_pics = total_drawings; break;
                case "cons": App.net.ConsRecord.no_of_pics = total_drawings; break;
                case "lock": App.net.LockingRecord.no_of_pics = total_drawings; break;
                case "comp": App.net.CompRecord.no_of_pics = total_drawings; break;
                case "green": App.net.GreenRecord.no_of_pics = total_drawings; break;
                case "bifold": App.net.BifoldRecord.no_of_pics = total_drawings; break;
            }
            base.OnBackButtonPressed();
            return false;
        }
    }
}