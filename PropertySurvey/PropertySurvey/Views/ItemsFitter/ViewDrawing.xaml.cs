using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewDrawing : ContentPage
    {
        List<string> filename_list;
        int current_pic_num = 0;
        ToolbarItem picture_number_toolitem = null;
        string directory = "Drawings/";

        public ViewDrawing(drawing_file_types drawing_parent)
        {
            InitializeComponent();

            string filename;
            switch (drawing_parent)
            {
                case drawing_file_types.dft_generic_drawing:
                case drawing_file_types.dft_generic_photo:
                    string fname;
                    if (drawing_parent == drawing_file_types.dft_generic_drawing)
                        fname = string.Format("{0:00000000}_dAU", App.net.HeaderRecord.udi_cont);
                    else
                    {
                        fname = string.Format("{0:00000000}_cAZ", App.net.HeaderRecord.udi_cont);
                        directory = "Photos/";
                        image.Rotation = 90;
                    }

                    fname += string.Format("{0:000}??.jpg", App.net.root_item_number);
                    filename_list = App.files.GetFileList(directory, fname);

                    if (filename_list.Count > 0)
                        filename = directory + filename_list[0];
                    else
                        filename = "";

                    add_toolbar_if_required();
                    break;

                default:
                    filename = SurveyFitterSharedLogic.drawing_filename(drawing_parent);
                    break;
            }

            load_image(filename);
        }

        private void add_toolbar_if_required()
        {
            if (filename_list.Count > 1)
            {
                ToolbarItem toolbar_item = new ToolbarItem() { Icon = "left.png", Command = new Command(async () => { this.OnPrevious(); }) };
                this.ToolbarItems.Add(toolbar_item);
                picture_number_toolitem = new ToolbarItem();
                this.ToolbarItems.Add(picture_number_toolitem);
                toolbar_item = new ToolbarItem() { Icon = "right.png", Command = new Command(async () => { this.OnNext(); }) };
                this.ToolbarItems.Add(toolbar_item);
            }
        }

        private void load_image(string filename)
        {
            image.Source = ImageSource.FromFile(App.files.CreatePathToFile(filename));

            if (picture_number_toolitem != null) // When there is more than one drawing
                picture_number_toolitem.Text = (current_pic_num + 1).ToString() + "/" + filename_list.Count.ToString();
        }

        private void OnPrevious()
        {
            --current_pic_num;
            if (current_pic_num < 0)
                current_pic_num = filename_list.Count - 1;
            load_image(directory + filename_list[current_pic_num]);
        }

        private void OnNext()
        {
            ++current_pic_num;
            if (current_pic_num >= filename_list.Count)
                current_pic_num = 0;
            load_image(directory + filename_list[current_pic_num]);
        }
    }
}
