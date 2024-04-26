using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.IO;
using System.Reflection;
using Xamarin.Forms.Internals;
using System.Linq;
using System.Text;

namespace PropertySurvey
{
    public enum drawing_file_types { dft_generic_drawing, dft_generic_photo, dft_handle, dft_garage_roller_open, dft_conservatory_roof_under,
                                     dft_timber_section, dft_timber_beading, dft_timber_sash };

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrawingPage : ContentPage
    {
        enum drawing_types { dt_squiggle, dt_line, dt_rectangle, dt_ellipse, dt_text, dt_erase };
        private class DrawItem
        {
            public drawing_types drawing_type;
            public float X, Y;
            public string text;
            public SKPath path;
        }

        float screen_ratio = 0.0f;

        bool bSaving = false;

        bool bClicked = false; // allow only one tap

        bool IsLandscape = false;

        private List<DrawItem> drawing_items = new List<DrawItem>();
        public SKPaint skPaint;

        private SKBitmap background_bitmap = null; 
        private SKBitmap undo_bitmap = null;
        drawing_types drawing_shape; 
        int last_btn = 1;
        bool there_is_a_draw_in_progress = false;

        public DrawingPage()
        {
            InitializeComponent();

            drawing_shape = drawing_types.dt_rectangle;
            ChangeButton(3);

            if (App.net.drawing_edit_mode == true)
            {
                if (App.net.drawing_type == "section_template")
                {
                    App.net.drawing_edit_filename = string.Format("{0:00000000}_dDT", App.net.HeaderRecord.udi_cont);
                    App.net.drawing_edit_filename = App.net.drawing_edit_filename + string.Format("{0:000}00.jpg", App.net.root_item_number);
                }
                if (App.net.drawing_type == "beading_template")
                {
                    App.net.drawing_edit_filename = string.Format("{0:00000000}_dBT", App.net.HeaderRecord.udi_cont);
                    App.net.drawing_edit_filename = App.net.drawing_edit_filename + string.Format("{0:000}00.jpg", App.net.root_item_number);
                }
                if (App.net.drawing_type == "sash")
                {
                    App.net.drawing_edit_filename = string.Format("{0:00000000}_dET", App.net.HeaderRecord.udi_cont);
                    App.net.drawing_edit_filename = App.net.drawing_edit_filename + string.Format("{0:000}00.jpg", App.net.root_item_number);
                }
                if (App.net.drawing_type == "handles")
                {
                    App.net.drawing_edit_filename = string.Format("{0:00000000}_dAT", App.net.HeaderRecord.udi_cont);
                    App.net.drawing_edit_filename = App.net.drawing_edit_filename + string.Format("{0:000}lk.jpg", App.net.root_item_number);
                }

                if (App.net.drawing_type == "cons_roof_under")
                {
                    App.net.drawing_edit_filename = string.Format("{0:00000000}_dCR", App.net.HeaderRecord.udi_cont);
                    App.net.drawing_edit_filename = App.net.drawing_edit_filename + string.Format("{0:000}00.jpg", App.net.root_item_number);
                }
                
                if (App.net.drawing_type == "garage_roller_open")
                {
                    App.net.drawing_edit_filename = string.Format("{0:00000000}_dRG", App.net.HeaderRecord.udi_cont);
                    App.net.drawing_edit_filename = App.net.drawing_edit_filename + string.Format("{0:000}00.jpg", App.net.root_item_number);
                }

                if (App.net.drawing_type == "FitterParts" || App.net.drawing_type == "rem_additional")
                {
                    if (App.net.HeaderRecord.typeB == "Remedial")
                    {
                        App.net.drawing_edit_filename = string.Format("{0:00000000}_remaddit.jpg", App.net.HeaderRecord.udi_cont);
                    }
                    else
                    {
                        App.net.drawing_edit_filename = string.Format("{0:00000000}_faddition.jpg", App.net.HeaderRecord.udi_cont);
                    }
                    App.net.drawing_edit_mode = false;
                }

                //App.net.drawing_edit_mode = false;
                Loadimage();
            }
            else
            {
                if (App.net.load_template_image == true)
                {
                    if (App.net.drawing_type == "cons_roof_under")
                    {
                        App.net.template_type_to_load = "Cons";
                        App.net.template_to_load = 14;
                        App.net.ConsRecord.cons_roof_under_drawn = true;
                    }
                    if (App.net.drawing_type == "garage_roller_open")
                    {
                        App.net.template_type_to_load = "Garage";
                        App.net.template_to_load = 41;
                        App.net.GarageRecord.additional_drawn = true;
                    }
                    LoadResourceimage();
                }
            }
        }

        void LoadResourceimage()
        {
            string[] fname;

            switch (App.net.template_type_to_load)
            {
                case "Windows": {
                                    fname = App.net.window_fname_list.ToArray();
                                    Assembly assembly = GetType().GetTypeInfo().Assembly;

                                    using (Stream stream = assembly.GetManifestResourceStream(fname[App.net.template_to_load].Replace(".Thumbnail","")))
                                    {
                                        background_bitmap = SKBitmap.Decode(stream);
                                    }
                                }break;
                case "Doors":
                case "Garage":
                    {
                        fname = App.net.doors_fname_list.ToArray();
                        Assembly assembly = GetType().GetTypeInfo().Assembly;

                        using (Stream stream = assembly.GetManifestResourceStream(fname[App.net.template_to_load].Replace(".Thumbnail", "")))
                        {
                            background_bitmap = SKBitmap.Decode(stream);
                        }
                    }
                    break;

                case "Cons":
                    {
                        fname = App.net.cons_fname_list.ToArray();
                        Assembly assembly = GetType().GetTypeInfo().Assembly;

                        using (Stream stream = assembly.GetManifestResourceStream(fname[App.net.template_to_load].Replace(".Thumbnail", "")))
                        {
                            background_bitmap = SKBitmap.Decode(stream);
                        }
                    }
                    break;
                case "Bead":
                    {
                        fname = App.net.bead_fname_list.ToArray();
                        Assembly assembly = GetType().GetTypeInfo().Assembly;

                        using (Stream stream = assembly.GetManifestResourceStream(fname[App.net.template_to_load].Replace(".Thumbnail", "")))
                        {
                            background_bitmap = SKBitmap.Decode(stream);
                        }
                    }
                    break;
                case "Handles":
                    {
                        fname = App.net.handles_fname_list.ToArray();
                        Assembly assembly = GetType().GetTypeInfo().Assembly;

                        using (Stream stream = assembly.GetManifestResourceStream(fname[App.net.template_to_load].Replace(".Thumbnail", "")))
                        {
                            background_bitmap = SKBitmap.Decode(stream);
                        }
                    }
                    break;
                case "Green":
                    {
                        fname = App.net.green_fname_list.ToArray();
                        Assembly assembly = GetType().GetTypeInfo().Assembly;

                        using (Stream stream = assembly.GetManifestResourceStream(fname[App.net.template_to_load].Replace(".Thumbnail", "")))
                        {
                            background_bitmap = SKBitmap.Decode(stream);
                            //SKBitmap.
                        }
                    }
                    break;
            }
        }

        void Loadimage()
        {
            if (App.files.FileExists("Drawings/" + App.net.drawing_edit_filename))
            {
                byte[] data = App.files.LoadBinary("Drawings/" + App.net.drawing_edit_filename);
                Stream stream = new MemoryStream(data);
                background_bitmap = SKBitmap.Decode(stream);
            }
        }

        bool Saveimage()
        {
            string fname = "";

            if (bSaving == false)
            {
                bSaving = true;

                if (App.net.drawing_edit_mode == true)
                {
                    if (App.net.drawing_type == "beading_template")
                    {
                        App.net.TimberRecord.bMouldingDrawn = true;
                    }
                    if (App.net.drawing_type == "section_template")
                    {
                        App.net.TimberRecord.bSectionDrawn = true;
                    }
                    if (App.net.drawing_type == "sash")
                    {
                        App.net.TimberRecord.bSashDrawn = true;
                    }
                    if (App.net.drawing_type == "handles")
                    {
                        //App.net.TimberRecord.bHandleDrawingComplete = true;
                    }

                    fname = "Drawings/" + App.net.drawing_edit_filename;
                }
                else
                {
                    if (App.net.drawing_type != "normal")
                    {
                        if (App.net.drawing_type == "section")
                        {
                            fname = string.Format("Drawings/{0:00000000}_dDT", App.net.HeaderRecord.udi_cont);
                            fname = fname + string.Format("{0:000}00.jpg", App.net.root_item_number);
                        }
                        if (App.net.drawing_type == "section_template")
                        {
                            App.net.TimberRecord.bSectionDrawn = true;

                            fname = string.Format("Drawings/{0:00000000}_dDT", App.net.HeaderRecord.udi_cont);
                            fname = fname + string.Format("{0:000}00.jpg", App.net.root_item_number);
                        }

                        if (App.net.drawing_type == "cons_roof_under")
                        {
                            fname = string.Format("Drawings/{0:00000000}_dCR", App.net.HeaderRecord.udi_cont);
                            fname = fname + string.Format("{0:000}00.jpg", App.net.root_item_number);
                        }

                        if (App.net.drawing_type == "garage_roller_open")
                        {
                            fname = string.Format("Drawings/{0:00000000}_dRG", App.net.HeaderRecord.udi_cont);
                            fname = fname + string.Format("{0:000}00.jpg", App.net.root_item_number);
                        }

                        if (App.net.drawing_type == "canopy1")
                        {
                            fname = string.Format("Drawings/{0:00000000}_dEG", App.net.HeaderRecord.udi_cont);
                            fname = fname + string.Format("{0:000}00.jpg", App.net.root_item_number);
                        }
                        if (App.net.drawing_type == "canopy2")
                        {
                            fname = string.Format("Drawings/{0:00000000}_dEG", App.net.HeaderRecord.udi_cont);
                            fname = fname + string.Format("{0:000}00.jpg", App.net.root_item_number);
                        }
                        if (App.net.drawing_type == "canopy3")
                        {
                            fname = string.Format("Drawings/{0:00000000}_dEG", App.net.HeaderRecord.udi_cont);
                            fname = fname + string.Format("{0:000}00.jpg", App.net.root_item_number);
                        }
                        if (App.net.drawing_type == "canopy4")
                        {
                            fname = string.Format("Drawings/{0:00000000}_dEG", App.net.HeaderRecord.udi_cont);
                            fname = fname + string.Format("{0:000}00.jpg", App.net.root_item_number);
                        }
                        if (App.net.drawing_type == "canopy1b")
                        {
                            fname = string.Format("Drawings/{0:00000000}_dEG", App.net.HeaderRecord.udi_cont);
                            fname = fname + string.Format("{0:000}00.jpg", App.net.root_item_number);
                        }
                        if (App.net.drawing_type == "canopy2b")
                        {
                            fname = string.Format("Drawings/{0:00000000}_dEG", App.net.HeaderRecord.udi_cont);
                            fname = fname + string.Format("{0:000}00.jpg", App.net.root_item_number);
                        }
                        if (App.net.drawing_type == "canopy3b")
                        {
                            fname = string.Format("Drawings/{0:00000000}_dEG", App.net.HeaderRecord.udi_cont);
                            fname = fname + string.Format("{0:000}00.jpg", App.net.root_item_number);
                        }
                        if (App.net.drawing_type == "roller1")
                        {
                            fname = string.Format("Drawings/{0:00000000}_dEG", App.net.HeaderRecord.udi_cont);
                            fname = fname + string.Format("{0:000}00.jpg", App.net.root_item_number);
                        }
                        if (App.net.drawing_type == "roller2")
                        {
                            fname = string.Format("Drawings/{0:00000000}_dEG", App.net.HeaderRecord.udi_cont);
                            fname = fname + string.Format("{0:000}00.jpg", App.net.root_item_number);
                        }
                        if (App.net.drawing_type == "roller3")
                        {
                            fname = string.Format("Drawings/{0:00000000}_dEG", App.net.HeaderRecord.udi_cont);
                            fname = fname + string.Format("{0:000}00.jpg", App.net.root_item_number);
                        }
                        if (App.net.drawing_type == "roller4")
                        {
                            fname = string.Format("Drawings/{0:00000000}_dEG", App.net.HeaderRecord.udi_cont);
                            fname = fname + string.Format("{0:000}00.jpg", App.net.root_item_number);
                        }
                        if (App.net.drawing_type == "sectional1")
                        {
                            fname = string.Format("Drawings/{0:00000000}_dEG", App.net.HeaderRecord.udi_cont);
                            fname = fname + string.Format("{0:000}00.jpg", App.net.root_item_number);
                        }
                        if (App.net.drawing_type == "sectional2")
                        {
                            fname = string.Format("Drawings/{0:00000000}_dEG", App.net.HeaderRecord.udi_cont);
                            fname = fname + string.Format("{0:000}00.jpg", App.net.root_item_number);
                        }
                        if (App.net.drawing_type == "sectional3")
                        {
                            fname = string.Format("Drawings/{0:00000000}_dEG", App.net.HeaderRecord.udi_cont);
                            fname = fname + string.Format("{0:000}00.jpg", App.net.root_item_number);
                        }
                        if (App.net.drawing_type == "sectional4")
                        {
                            fname = string.Format("Drawings/{0:00000000}_dEG", App.net.HeaderRecord.udi_cont);
                            fname = fname + string.Format("{0:000}00.jpg", App.net.root_item_number);
                        }

                        if (App.net.drawing_type == "sash")
                        {
                            fname = string.Format("Drawings/{0:00000000}_dET", App.net.HeaderRecord.udi_cont);
                            fname = fname + string.Format("{0:000}00.jpg", App.net.root_item_number);
                        }
                        if (App.net.drawing_type == "beading")
                        {
                            fname = string.Format("Drawings/{0:00000000}_dBT", App.net.HeaderRecord.udi_cont);
                            fname = fname + string.Format("{0:000}00.jpg", App.net.root_item_number);
                        }
                        if (App.net.drawing_type == "beading_template")
                        {
                            App.net.TimberRecord.bMouldingDrawn = true;

                            fname = string.Format("Drawings/{0:00000000}_dBT", App.net.HeaderRecord.udi_cont);
                            fname = fname + string.Format("{0:000}00.jpg", App.net.root_item_number);
                        }

                        if (App.net.drawing_type == "moulding")
                        {
                            fname = string.Format("Drawings/{0:00000000}_dCT", App.net.HeaderRecord.udi_cont);
                            fname = fname + string.Format("{0:000}00.jpg", App.net.root_item_number);
                        }
                        if (App.net.drawing_type == "handles")
                        {
                            fname = string.Format("Drawings/{0:00000000}_dAT", App.net.HeaderRecord.udi_cont);
                            fname = fname + string.Format("{0:000}lk.jpg", App.net.root_item_number);
                        }
                        if (App.net.drawing_type == "FitterParts" || App.net.drawing_type == "rem_additional")
                        {
                            if (App.net.HeaderRecord.typeB == "Remedial")
                            {
                                fname = string.Format("Drawings/{0:00000000}_remaddit.jpg", App.net.HeaderRecord.udi_cont);
                            }
                            else
                            {
                                fname = string.Format("Drawings/{0:00000000}_faddition.jpg", App.net.HeaderRecord.udi_cont);
                            }
                        }
                        if (App.net.drawing_type == "fitter_additional")
                        {
                            fname = string.Format("Drawings/{0:00000000}_fAD00000.jpg", App.net.HeaderRecord.udi_cont);
                            App.net.HeaderRecord.bad_image_complete = true;
                        }
                        if (App.net.drawing_type == "rem_additional")
                        {
                            fname = string.Format("Drawings/{0:00000000}_remaddit.jpg", App.net.HeaderRecord.udi_cont);
                            App.net.HeaderRecord.bad_image_complete = true;
                        }
                    }
                    else
                    {
                        fname = string.Format("Drawings/{0:00000000}_dAU", App.net.HeaderRecord.udi_cont);
                        fname = fname + string.Format("{0:000}", App.net.root_item_number);
                        fname = fname + string.Format("{0:00}.jpg", App.net.vid_image_num);
                    }
                }
                
                // create an image and then get the PNG (or any other) encoded data
                using (var image = SKImage.FromBitmap(background_bitmap))
                using (var data = image.Encode(SKEncodedImageFormat.Png, 80))
                {
                    //byte[] dd = data.ToArray();
                    App.files.SaveBinary(fname, data.ToArray());
                }


                if (App.net.drawing_type == "handles")
                {
                    if (App.net.CurrentItem == "alum")
                        App.net.AlumRecord.bHandleDrawingComplete = true;
                    else if (App.net.CurrentItem == "bifold")
                        App.net.BifoldRecord.bHandleDrawingComplete = true;
                    else if (App.net.CurrentItem == "comp")
                        App.net.CompRecord.bHandleDrawingComplete = true;
                    else if (App.net.CurrentItem == "timber")
                        App.net.TimberRecord.bHandleDrawingComplete = true;
                    else if (App.net.CurrentItem == "upvc")
                        App.net.UPVCRecord.bHandleDrawingComplete = true;
                }
            }
            return false;
        }

        protected override bool OnBackButtonPressed()
        {
            Saveimage();

            //CountDrawings();

            MessagingCenter.Send(this, "allowLandScapePortraitDrawing");
            base.OnBackButtonPressed();
            return false;
        }

        /*
        void CountDrawings()
        {
            int total_drawings = 0;
            int hival = 0;

            string fname = string.Format("{0:00000000}_dAU", App.net.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}??.jpg", App.net.root_item_number);

            List<string> fileNames = App.files.GetFileList("Drawings", fname);
            total_drawings = fileNames.Count();

            switch (App.net.CurrentItem)
            {
                case "panel": App.net.PanelRecord.no_of_pics = total_drawings; break;
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
        }
        */

        void ChangeButton(int btn)
        {
            switch (last_btn) 
            {
                case 1: squiggle_button.Source = "squig_out.jpg"; break;
                case 2: line_button.Source = "line_out.jpg"; break;
                case 3: rectangle_button.Source = "square_out.jpg"; break;
                case 4: ellipse_button.Source = "circ_out.jpg"; break;
                case 5: text_button.Source = "ay_out.jpg"; break;
                case 6: erase_button.Source = "rubber_out.jpg"; break;
            }
            switch (btn)
            {
                case 1: squiggle_button.Source = "squig_in.jpg"; break;
                case 2: line_button.Source = "line_in.jpg"; break;
                case 3: rectangle_button.Source = "square_in.jpg"; break;
                case 4: ellipse_button.Source = "circ_in.jpg"; break;
                case 5: text_button.Source = "ay_in.jpg"; break;
                case 6: erase_button.Source = "rubber_in.jpg"; break;
            }

            last_btn = btn;
        }

        void draw_one_item(DrawItem drawing_item, SKPaint a_path, SKCanvas canvas)
        {
            switch (drawing_item.drawing_type)
            {
                case drawing_types.dt_squiggle:
                case drawing_types.dt_line:
                case drawing_types.dt_rectangle:
                case drawing_types.dt_ellipse:
                    a_path.StrokeWidth = 5;
                    a_path.Color = SKColors.Black;
                    a_path.Style = SKPaintStyle.Stroke;
                    canvas.DrawPath(drawing_item.path, a_path);
                    break;
                case drawing_types.dt_text:
                    if (App.net.TextEntryText != null && App.net.TextEntryText != "")
                    {
                        drawing_item.text = App.net.TextEntryText;
                    }
                    a_path.Color = SKColors.Black;
                    a_path.Style = SKPaintStyle.Fill;
                    // fint size needs to be dpi?
                    a_path.TextSize = 17.0f * screen_ratio;
                    a_path.IsVerticalText = App.CurrentApp.App_Settings.vertical_text;
                    //canvas.RotateDegrees(45);
                    if (drawing_item.text != null)
                    {
                        if(App.CurrentApp.App_Settings.vertical_text == true)
                        {
                            canvas.DrawText(drawing_item.text, drawing_item.X + 30.0f, drawing_item.Y - ((17.0f * screen_ratio)* drawing_item.text.Length), a_path);
                        }
                        else
                        {
                            //canvas.DrawText(drawing_item.text, drawing_item.X, drawing_item.Y, a_path);

                            var area = SKRect.Create(drawing_item.X, drawing_item.Y, 0, 0);
                            this.DrawText(canvas, drawing_item.text, area, a_path);
                        }
                        
                    }
                    //canvas.RotateDegrees(0);
                    break;
                case drawing_types.dt_erase:
                    a_path.StrokeWidth = 35; // Old one was 15, but seems a bit narrow on android, so trying 25
                    a_path.Color = SKColors.White;
                    a_path.Style = SKPaintStyle.Stroke;
                    canvas.DrawPath(drawing_item.path, a_path);
                    break;
            }
        }

        private void DrawText(SKCanvas canvas, string text, SKRect area, SKPaint paint)
        {
            float lineHeight = paint.TextSize * 1.2f;
            var lines = SplitLines(text, paint, area.Width);
            var height = lines.Count() * lineHeight;

            var y = area.MidY - height / 2;

            foreach (var line in lines)
            {
                y += lineHeight;
                var x = area.MidX - line.Width / 2;
                canvas.DrawText(line.Value, x, y, paint);
            }
        }

        public class Line
        {
            public string Value { get; set; }

            public float Width { get; set; }
        }
        private Line[] SplitLines(string text, SKPaint paint, float maxWidth)
        {
            var spaceWidth = paint.MeasureText(" ");
            var lines = text.Split('\n');

            return lines.SelectMany((line) =>
            {
                var result = new List<Line>();

                var words = line.Split(new[] { " " }, StringSplitOptions.None);

                var lineResult = new StringBuilder();
                float width = 0;
                foreach (var word in words)
                {
                    if (word != null && word.Length > 0)
                    {
                        var wordWidth = paint.MeasureText(word);
                        var wordWithSpaceWidth = wordWidth + spaceWidth;
                        var wordWithSpace = word + " ";

                        if (false)//(width + wordWidth > maxWidth)
                        {
                            result.Add(new Line() { Value = lineResult.ToString(), Width = width });
                            lineResult = new StringBuilder(wordWithSpace);
                            width = wordWithSpaceWidth;
                        }
                        else
                        {
                            lineResult.Append(wordWithSpace);
                            width += wordWithSpaceWidth;
                        }
                    }
                }

                result.Add(new Line() { Value = lineResult.ToString(), Width = width });

                return result.ToArray();
            }).ToArray();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width > height)
            {
                IsLandscape = true;
                // Orientation got changed! Do your changes here
            }
            else
            {
                IsLandscape = false;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Send(this, "preventLandscapeDrawing");
            if (drawing_items.Count>1 && App.net.TextEntryText!=null && App.net.TextEntryText!="")
            { 
                drawing_items[drawing_items.Count - 1].text = App.net.TextEntryText;
            }
            //MessagingCenter.Send(this, "preventLandscape");
            //drawing_item.text = App.net.TextEntryText;
        }

        //during page close setting back to portrait
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //MessagingCenter.Send(this, "allowLandScapePortrait");

        }

        private void redraw_bitmap()
        {
            if (drawing_items.Count > 0)
            {
                //background_bitmap.CopyTo(undo_bitmap);
                SKCanvas canvas = new SKCanvas(background_bitmap);
                //canvas.Clear(SKColors.White);

                var drawing_paint = new SKPaint
                { // Has some default values, other values get changed depending what we are drawing
                    IsAntialias = true,
                    Style = SKPaintStyle.Stroke,
                    TextSize = 23
                };

                // draw the items
                foreach (var drawing_item in drawing_items)
                    draw_one_item(drawing_item, drawing_paint, canvas);

                drawing_items.Clear();
            }
        }

        // Draws the screen
        private void OnPainting(object sender, SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.White);
            if (IsLandscape == true)
            {
                screen_ratio = (float)info.Width / 512.0f;
            }
            else
            {
                screen_ratio = (float)info.Width / 360.0f;
            }

            if (background_bitmap == null)
            {
                background_bitmap = new SKBitmap((int)info.Width, (int)info.Height);
            }
            if (undo_bitmap == null)
            {
                undo_bitmap = new SKBitmap((int)info.Width, (int)info.Height);
            }
            
            if(background_bitmap.Width!= (int)info.Width || background_bitmap.Height!= (int)info.Height)
            {
                background_bitmap = background_bitmap.Resize(info, SKFilterQuality.High);
            }

            if (undo_bitmap.Width != (int)info.Width || background_bitmap.Height != (int)info.Height)
            {
                undo_bitmap = undo_bitmap.Resize(info, SKFilterQuality.High);
            }

            canvas.DrawBitmap(background_bitmap, 0, 0);

            var touchPathStroke = new SKPaint
            { // Has some default values, other values get changed depending what we are drawing
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                TextSize = 23
            };

            // draw the path that is currently being created (if any)
            if (there_is_a_draw_in_progress)
                draw_one_item(drawing_items[drawing_items.Count - 1], touchPathStroke, canvas);
        }

        private void OnTouch(object sender, SKTouchEventArgs e)
        {
            switch (e.ActionType)
            {
                case SKTouchAction.Pressed:
                    if (bClicked == false)
                    {
                        bClicked = true;

                        // start of a stroke
                        background_bitmap.CopyTo(undo_bitmap);
                        switch (drawing_shape)
                        {
                            case drawing_types.dt_squiggle:
                                DrawItem squiggle_item = new DrawItem();
                                squiggle_item.drawing_type = drawing_types.dt_squiggle;
                                squiggle_item.path = new SKPath();
                                squiggle_item.path.MoveTo(e.Location);
                                drawing_items.Add(squiggle_item);
                                there_is_a_draw_in_progress = true;
                                break;
                            case drawing_types.dt_line:
                                DrawItem line_item = new DrawItem();
                                line_item.drawing_type = drawing_types.dt_line;
                                line_item.X = e.Location.X;
                                line_item.Y = e.Location.Y;
                                line_item.path = new SKPath();
                                line_item.path.MoveTo(e.Location);
                                line_item.path.LineTo(e.Location);
                                drawing_items.Add(line_item);
                                there_is_a_draw_in_progress = true;
                                break;
                            case drawing_types.dt_rectangle: // Rectangle - Just create a 1 pixel line for now, OnMove will change it to a rectangle
                                DrawItem rectangle_item = new DrawItem();
                                rectangle_item.drawing_type = drawing_types.dt_rectangle;
                                rectangle_item.X = e.Location.X;
                                rectangle_item.Y = e.Location.Y;
                                rectangle_item.path = new SKPath();
                                rectangle_item.path.MoveTo(e.Location);
                                rectangle_item.path.LineTo(e.Location);
                                drawing_items.Add(rectangle_item);
                                there_is_a_draw_in_progress = true;
                                break;
                            case drawing_types.dt_ellipse:
                                DrawItem ellipse_item = new DrawItem();
                                ellipse_item.drawing_type = drawing_types.dt_ellipse;
                                ellipse_item.X = e.Location.X;
                                ellipse_item.Y = e.Location.Y;
                                ellipse_item.path = new SKPath();
                                SKRect oval_rect = new SKRect();
                                oval_rect.Left = e.Location.X;
                                oval_rect.Top = e.Location.Y;
                                oval_rect.Right = e.Location.X + 1;
                                oval_rect.Bottom = e.Location.Y + 1;
                                ellipse_item.path.AddOval(oval_rect);
                                drawing_items.Add(ellipse_item);
                                there_is_a_draw_in_progress = true;
                                break;
                            case drawing_types.dt_text:
                                DrawItem text_item = new DrawItem();
                                text_item.drawing_type = drawing_types.dt_text;
                                text_item.X = e.Location.X - 60;
                                text_item.Y = e.Location.Y - 60;
                                text_item.path = null;
                                drawing_items.Add(text_item);
                                there_is_a_draw_in_progress = true;
                                break;
                            case drawing_types.dt_erase:
                                DrawItem erase_item = new DrawItem();
                                erase_item.drawing_type = drawing_types.dt_erase;
                                erase_item.path = new SKPath();
                                erase_item.path.MoveTo(e.Location);
                                drawing_items.Add(erase_item);
                                there_is_a_draw_in_progress = true;
                                break;
                        }
                    }
                    break;
                case SKTouchAction.Moved: // the stroke, while pressed
                    if(drawing_items.Count>0)
                    switch (drawing_shape)
                    {
                        case drawing_types.dt_squiggle:
                        case drawing_types.dt_erase:
                            drawing_items[drawing_items.Count - 1].path.LineTo(e.Location);
                            break;
                        case drawing_types.dt_line:
                            drawing_items[drawing_items.Count - 1].path.Rewind();
                            drawing_items[drawing_items.Count - 1].path.MoveTo(drawing_items[drawing_items.Count - 1].X, drawing_items[drawing_items.Count - 1].Y);
                            drawing_items[drawing_items.Count - 1].path.LineTo(e.Location);

                            // This would be better, but .X won't update as if it's read only.
                            //drawing_items[drawing_items.Count - 1].path.Points[1].X = e.Location.X;
                            //drawing_items[drawing_items.Count - 1].path.Points[1].Y = e.Location.Y;
                            break;
                        case drawing_types.dt_rectangle:
                            drawing_items[drawing_items.Count - 1].path.Rewind();
                            drawing_items[drawing_items.Count - 1].path.MoveTo(drawing_items[drawing_items.Count - 1].X, drawing_items[drawing_items.Count - 1].Y);
                            drawing_items[drawing_items.Count - 1].path.LineTo(e.Location.X, drawing_items[drawing_items.Count - 1].Y);
                            drawing_items[drawing_items.Count - 1].path.LineTo(e.Location);
                            drawing_items[drawing_items.Count - 1].path.LineTo(drawing_items[drawing_items.Count - 1].X, e.Location.Y);
                            drawing_items[drawing_items.Count - 1].path.LineTo(drawing_items[drawing_items.Count - 1].X, drawing_items[drawing_items.Count - 1].Y);
                            break;
                        case drawing_types.dt_ellipse:
                            drawing_items[drawing_items.Count - 1].path.Rewind();

                            SKRect oval_rect = new SKRect();
                            oval_rect.Left = drawing_items[drawing_items.Count - 1].X - (e.Location.X - drawing_items[drawing_items.Count - 1].X);
                            oval_rect.Top = drawing_items[drawing_items.Count - 1].Y - (e.Location.Y - drawing_items[drawing_items.Count - 1].Y);
                            oval_rect.Right = e.Location.X;
                            oval_rect.Bottom = e.Location.Y;
                            drawing_items[drawing_items.Count - 1].path.AddOval(oval_rect);

                            break;
                        case drawing_types.dt_text:
                            drawing_items[drawing_items.Count - 1].X = e.Location.X- 60;
                            drawing_items[drawing_items.Count - 1].Y = e.Location.Y - 60;
                            break;
                    }
                    break;
                case SKTouchAction.Released:
                    if (bClicked == true)
                    {
                        bClicked = false;
                        redraw_bitmap();
                    }
                    there_is_a_draw_in_progress = false;
                    break;
            }

            // we have handled these events
            e.Handled = true;

            // update the UI
            ((SKCanvasView)sender).InvalidateSurface();
        }

        // Events to respond to buttons along buttom of screen
        private void squiggle_button_Click(object sender, EventArgs e)
        {
            drawing_shape = drawing_types.dt_squiggle;
            ChangeButton(1);
        }

        private void line_button_Click(object sender, EventArgs e)
        {
            drawing_shape = drawing_types.dt_line;
            ChangeButton(2);
        }

        private void rectangle_button_Click(object sender, EventArgs e)
        {
            drawing_shape = drawing_types.dt_rectangle;
            ChangeButton(3);
        }

        private void ellipse_button_Click(object sender, EventArgs e)
        {
            drawing_shape = drawing_types.dt_ellipse;
            ChangeButton(4);
        }

        private void text_button_Click(object sender, EventArgs e)
        {
            drawing_shape = drawing_types.dt_text;
            ChangeButton(5);
            Navigation.PushAsync(new DrawingTextPage(), false);
        }

        private void erase_button_Click(object sender, EventArgs e)
        {
            drawing_shape = drawing_types.dt_erase;
            ChangeButton(6);
        }

        private void undo_button_Click(object sender, EventArgs e)
        { 
            undo_bitmap.CopyTo(background_bitmap);
            drawing_items.Clear();
            SkCanvasView.InvalidateSurface(); 
        }

        private async void exit_button_Click(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Clear canvas?", "", "   Yes   ", "   No   ");
            if (answer)
            {
                background_bitmap.Erase(SKColor.Parse("ffffff"));
                undo_bitmap.Erase(SKColor.Parse("ffffff"));
                drawing_items.Clear();
                SkCanvasView.InvalidateSurface();
                switch(App.net.drawing_type)
                {
                    case "handles":
                        App.net.drawing_edit_mode = false;
                        App.net.load_template_image = true;
                        Navigation.InsertPageBefore(new TemplateHandles(), this);
                        await Navigation.PopAsync(false);
                        break;
                    case "garage_roller_open":
                        App.net.template_type_to_load = "Garage";
                        App.net.template_to_load = 38;
                        App.net.drawing_edit_mode = false;
                        LoadResourceimage();
                        break;
                    default:
                        /*
                        App.net.drawing_edit_mode = false;
                        App.net.load_template_image = false;
                        Navigation.InsertPageBefore(new OrTemplate(), this);
                        await Navigation.PopAsync(false);
                        */
                        break;
                }
            }
        }
    }
}
