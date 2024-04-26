using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public static class window_lock_logic
    {
        public static List<string> gearbox_list = new List<string>() { "...", "Offset", "Inline" };
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WindowLock : ContentPage
    {
        enum drawing_types { dt_none, dt_line, dt_text, dt_lock, dt_deadlock, dt_hook, dt_latch, dt_cam, dt_mushroom, dt_bolt_in, dt_bolt_out };
        private class DrawItem
        {
            public drawing_types drawing_type;
            public SKPoint at;
            public string text = "";
            public SKPath path;
            public SKColor color = SKColors.Black;
        }

        private SKBitmap background_bitmap = null;
        private class Latch
        {
            public drawing_types type;
            public SKPoint at;
        }

        List<Latch> latches = new List<Latch>();

        drawing_types selected_item = drawing_types.dt_none;

        float screen_ratio = 1.0f;

        public SKPaint skPaint;
        private List<DrawItem> drawing_items = new List<DrawItem>();

        private SKBitmap lock_bitmap = null;
        private SKBitmap lock_db = null;
        private SKBitmap lock_hook = null;
        private SKBitmap lock_latch = null;
        private SKBitmap lock_cam = null;
        private SKBitmap lock_mush = null;

        private SKBitmap check_in = null;
        private SKBitmap check_out = null;

        SKPoint mouse_point, last_mouse_point;

        SKPoint currentPoint;

        SKPoint lock_position = new SKPoint(80.0f, 200.0f);

        bool locks_scaled = false;

        int current_enter = 0;

        string size1 = "", size2 = "";
        string[] enter_values = new string[9];

        public WindowLock()
        {
            InitializeComponent();
            switch (App.net.CurrentItem)
            {
                case "upvc": BindingContext = App.net.UPVCRecord as UPVCTable; break;
                case "alum": BindingContext = App.net.AlumRecord as AlumTable; break;
                case "timber": BindingContext = App.net.TimberRecord as TimberTable; break;
                case "lock": BindingContext = App.net.LockingRecord as LockingTable; break;
            }
            gearbox_button.set_button_list(window_lock_logic.gearbox_list);

            enter_values[0] = "";
            enter_values[1] = "";
            enter_values[2] = "";
            enter_values[3] = "";
            enter_values[4] = "";
            enter_values[5] = "";
            enter_values[6] = "";
            enter_values[7] = "";
            enter_values[8] = "";

            LoadBitmaps();

            background_bitmap = new SKBitmap();
        }

        protected override bool OnBackButtonPressed()
        {
            SaveValues();

            base.OnBackButtonPressed();
            return false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }

        private void SaveValues()
        {
            GetEnterValue();
            if (App.net.CurrentItem == "lock")
            {
                App.net.LockingRecord.bWindowComplete = true;
                App.net.LockingRecord.bLockComplete = true;
                App.net.LockingRecord.l_size1 = size1;
                App.net.LockingRecord.l_size2 = size2;
                App.net.LockingRecord.lock_position = lock_position.Y;

                App.net.LockingRecord.l_sizeA = enter_values[0];
                App.net.LockingRecord.l_sizeB = enter_values[1];
                App.net.LockingRecord.l_sizeC = enter_values[2];
                App.net.LockingRecord.l_sizeD = enter_values[3];
                App.net.LockingRecord.l_sizeE = enter_values[4];
                App.net.LockingRecord.l_sizeF = enter_values[5];
                App.net.LockingRecord.l_sizeG = enter_values[6];

                int i = 0;
                foreach (var latch in latches)
                {
                    if (latch.type != drawing_types.dt_none)
                    {
                        switch (i)
                        {
                            case 0: App.net.LockingRecord.l_itype1 = (int)latch.type; break;
                            case 1: App.net.LockingRecord.l_itype2 = (int)latch.type; break;
                            case 2: App.net.LockingRecord.l_itype3 = (int)latch.type; break;
                            case 3: App.net.LockingRecord.l_itype4 = (int)latch.type; break;
                            case 4: App.net.LockingRecord.l_itype5 = (int)latch.type; break;
                            case 5: App.net.LockingRecord.l_itype6 = (int)latch.type; break;
                            case 6: App.net.LockingRecord.l_itype7 = (int)latch.type; break;
                        }
                        switch (i)
                        {
                            case 0: App.net.LockingRecord.l_fpos1 = latch.at.Y; break;
                            case 1: App.net.LockingRecord.l_fpos2 = latch.at.Y; break;
                            case 2: App.net.LockingRecord.l_fpos3 = latch.at.Y; break;
                            case 3: App.net.LockingRecord.l_fpos4 = latch.at.Y; break;
                            case 4: App.net.LockingRecord.l_fpos5 = latch.at.Y; break;
                            case 5: App.net.LockingRecord.l_fpos6 = latch.at.Y; break;
                            case 6: App.net.LockingRecord.l_fpos7 = latch.at.Y; break;
                        }
                        switch (i)
                        {
                            case 0: App.net.LockingRecord.l_sizeA = enter_values[0]; break;
                            case 1: App.net.LockingRecord.l_sizeB = enter_values[1]; break;
                            case 2: App.net.LockingRecord.l_sizeC = enter_values[2]; break;
                            case 3: App.net.LockingRecord.l_sizeD = enter_values[3]; break;
                            case 4: App.net.LockingRecord.l_sizeE = enter_values[4]; break;
                            case 5: App.net.LockingRecord.l_sizeF = enter_values[5]; break;
                            case 6: App.net.LockingRecord.l_sizeG = enter_values[6]; break;
                        }
                        i++;
                    }
                }
                App.net.LockingRecord.l_num = i;
            }
            if (App.net.CurrentItem == "timber")
            {
                App.net.TimberRecord.bWindowComplete = true;
                App.net.TimberRecord.bLockComplete = true;
                App.net.TimberRecord.l_size1 = size1;
                App.net.TimberRecord.l_size2 = size2;
                App.net.TimberRecord.lock_position = lock_position.Y;

                App.net.TimberRecord.l_sizeA = enter_values[0];
                App.net.TimberRecord.l_sizeB = enter_values[1];
                App.net.TimberRecord.l_sizeC = enter_values[2];
                App.net.TimberRecord.l_sizeD = enter_values[3];
                App.net.TimberRecord.l_sizeE = enter_values[4];
                App.net.TimberRecord.l_sizeF = enter_values[5];
                App.net.TimberRecord.l_sizeG = enter_values[6];

                int i = 0;
                foreach (var latch in latches)
                {
                    if (latch.type != drawing_types.dt_none)
                    {
                        switch (i)
                        {
                            case 0: App.net.TimberRecord.l_itype1 = (int)latch.type; break;
                            case 1: App.net.TimberRecord.l_itype2 = (int)latch.type; break;
                            case 2: App.net.TimberRecord.l_itype3 = (int)latch.type; break;
                            case 3: App.net.TimberRecord.l_itype4 = (int)latch.type; break;
                            case 4: App.net.TimberRecord.l_itype5 = (int)latch.type; break;
                            case 5: App.net.TimberRecord.l_itype6 = (int)latch.type; break;
                            case 6: App.net.TimberRecord.l_itype7 = (int)latch.type; break;
                        }
                        switch (i)
                        {
                            case 0: App.net.TimberRecord.l_fpos1 = latch.at.Y; break;
                            case 1: App.net.TimberRecord.l_fpos2 = latch.at.Y; break;
                            case 2: App.net.TimberRecord.l_fpos3 = latch.at.Y; break;
                            case 3: App.net.TimberRecord.l_fpos4 = latch.at.Y; break;
                            case 4: App.net.TimberRecord.l_fpos5 = latch.at.Y; break;
                            case 5: App.net.TimberRecord.l_fpos6 = latch.at.Y; break;
                            case 6: App.net.TimberRecord.l_fpos7 = latch.at.Y; break;
                        }
                        switch (i)
                        {
                            case 0: App.net.TimberRecord.l_sizeA = enter_values[0]; break;
                            case 1: App.net.TimberRecord.l_sizeB = enter_values[1]; break;
                            case 2: App.net.TimberRecord.l_sizeC = enter_values[2]; break;
                            case 3: App.net.TimberRecord.l_sizeD = enter_values[3]; break;
                            case 4: App.net.TimberRecord.l_sizeE = enter_values[4]; break;
                            case 5: App.net.TimberRecord.l_sizeF = enter_values[5]; break;
                            case 6: App.net.TimberRecord.l_sizeG = enter_values[6]; break;
                        }
                        i++;
                    }
                }
                App.net.TimberRecord.l_num = i;
            }
            if (App.net.CurrentItem == "alum")
            {
                App.net.AlumRecord.bWindowComplete = true;
                App.net.AlumRecord.bLockComplete = 1;
                App.net.AlumRecord.l_size1 = size1;
                App.net.AlumRecord.l_size2 = size2;
                App.net.AlumRecord.lock_position = lock_position.Y;

                App.net.AlumRecord.l_sizeA = enter_values[0];
                App.net.AlumRecord.l_sizeB = enter_values[1];
                App.net.AlumRecord.l_sizeC = enter_values[2];
                App.net.AlumRecord.l_sizeD = enter_values[3];
                App.net.AlumRecord.l_sizeE = enter_values[4];
                App.net.AlumRecord.l_sizeF = enter_values[5];
                App.net.AlumRecord.l_sizeG = enter_values[6];

                int i = 0;
                foreach (var latch in latches)
                {
                    if (latch.type != drawing_types.dt_none)
                    {
                        switch (i)
                        {
                            case 0: App.net.AlumRecord.l_itype1 = (int)latch.type; break;
                            case 1: App.net.AlumRecord.l_itype2 = (int)latch.type; break;
                            case 2: App.net.AlumRecord.l_itype3 = (int)latch.type; break;
                            case 3: App.net.AlumRecord.l_itype4 = (int)latch.type; break;
                            case 4: App.net.AlumRecord.l_itype5 = (int)latch.type; break;
                            case 5: App.net.AlumRecord.l_itype6 = (int)latch.type; break;
                            case 6: App.net.AlumRecord.l_itype7 = (int)latch.type; break;
                        }
                        switch (i)
                        {
                            case 0: App.net.AlumRecord.l_fpos1 = latch.at.Y; break;
                            case 1: App.net.AlumRecord.l_fpos2 = latch.at.Y; break;
                            case 2: App.net.AlumRecord.l_fpos3 = latch.at.Y; break;
                            case 3: App.net.AlumRecord.l_fpos4 = latch.at.Y; break;
                            case 4: App.net.AlumRecord.l_fpos5 = latch.at.Y; break;
                            case 5: App.net.AlumRecord.l_fpos6 = latch.at.Y; break;
                            case 6: App.net.AlumRecord.l_fpos7 = latch.at.Y; break;
                        }
                        switch (i)
                        {
                            case 0: App.net.AlumRecord.l_sizeA = enter_values[0]; break;
                            case 1: App.net.AlumRecord.l_sizeB = enter_values[1]; break;
                            case 2: App.net.AlumRecord.l_sizeC = enter_values[2]; break;
                            case 3: App.net.AlumRecord.l_sizeD = enter_values[3]; break;
                            case 4: App.net.AlumRecord.l_sizeE = enter_values[4]; break;
                            case 5: App.net.AlumRecord.l_sizeF = enter_values[5]; break;
                            case 6: App.net.AlumRecord.l_sizeG = enter_values[6]; break;
                        }
                        i++;
                    }
                }
                App.net.AlumRecord.l_num = i;
            }
            if (App.net.CurrentItem == "upvc")
            {
                App.net.UPVCRecord.bWindowComplete = true;
                App.net.UPVCRecord.bLockComplete = true;
                App.net.UPVCRecord.l_size1 = size1;
                App.net.UPVCRecord.l_size2 = size2;
                App.net.UPVCRecord.lock_position = lock_position.Y;

                App.net.UPVCRecord.l_sizeA = enter_values[0];
                App.net.UPVCRecord.l_sizeB = enter_values[1];
                App.net.UPVCRecord.l_sizeC = enter_values[2];
                App.net.UPVCRecord.l_sizeD = enter_values[3];
                App.net.UPVCRecord.l_sizeE = enter_values[4];
                App.net.UPVCRecord.l_sizeF = enter_values[5];
                App.net.UPVCRecord.l_sizeG = enter_values[6];

                int i = 0;
                foreach (var latch in latches)
                {
                    if (latch.type != drawing_types.dt_none)
                    {
                        switch (i)
                        {
                            case 0: App.net.UPVCRecord.l_itype1 = (int)latch.type; break;
                            case 1: App.net.UPVCRecord.l_itype2 = (int)latch.type; break;
                            case 2: App.net.UPVCRecord.l_itype3 = (int)latch.type; break;
                            case 3: App.net.UPVCRecord.l_itype4 = (int)latch.type; break;
                            case 4: App.net.UPVCRecord.l_itype5 = (int)latch.type; break;
                            case 5: App.net.UPVCRecord.l_itype6 = (int)latch.type; break;
                            case 6: App.net.UPVCRecord.l_itype7 = (int)latch.type; break;
                        }
                        switch (i)
                        {
                            case 0: App.net.UPVCRecord.l_fpos1 = latch.at.Y; break;
                            case 1: App.net.UPVCRecord.l_fpos2 = latch.at.Y; break;
                            case 2: App.net.UPVCRecord.l_fpos3 = latch.at.Y; break;
                            case 3: App.net.UPVCRecord.l_fpos4 = latch.at.Y; break;
                            case 4: App.net.UPVCRecord.l_fpos5 = latch.at.Y; break;
                            case 5: App.net.UPVCRecord.l_fpos6 = latch.at.Y; break;
                            case 6: App.net.UPVCRecord.l_fpos7 = latch.at.Y; break;
                        }
                        switch (i)
                        {
                            case 0: App.net.UPVCRecord.l_sizeA = enter_values[0]; break;
                            case 1: App.net.UPVCRecord.l_sizeB = enter_values[1]; break;
                            case 2: App.net.UPVCRecord.l_sizeC = enter_values[2]; break;
                            case 3: App.net.UPVCRecord.l_sizeD = enter_values[3]; break;
                            case 4: App.net.UPVCRecord.l_sizeE = enter_values[4]; break;
                            case 5: App.net.UPVCRecord.l_sizeF = enter_values[5]; break;
                            case 6: App.net.UPVCRecord.l_sizeG = enter_values[6]; break;
                        }
                        i++;
                    }
                }
                App.net.UPVCRecord.l_num = i;
            }

            DrawBitmap();
            //save image
            string fname = string.Format("Drawings/{0:00000000}_dLO", App.net.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}00.jpg", App.net.root_item_number);

            if (background_bitmap != null)
            {
                // create an image and then get the PNG (or any other) encoded data
                using (var image = SKImage.FromBitmap(background_bitmap))
                if (image != null)
                {
                    using (var data = image.Encode(SKEncodedImageFormat.Png, 80))
                    {
                        //byte[] dd = data.ToArray();
                        App.files.SaveBinary(fname, data.ToArray());
                    }
                }
            }
        }

        private void DrawBitmap()
        {
            SKCanvas canvas = new SKCanvas(background_bitmap);

            var touchPathStroke = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                TextSize = 23
            };

            for (int i = 0; i < drawing_items.Count; i++)
                draw_item(drawing_items[i], touchPathStroke, canvas);
        }

        private void LoadValues()
        {
            if (App.net.CurrentItem == "lock")
            {
                size1 = App.net.LockingRecord.l_size1;
                size2 = App.net.LockingRecord.l_size2;

                //lock_position.Y = App.net.LockingRecord.lock_position;

                enter_values[0] = App.net.LockingRecord.l_sizeA;
                enter_values[1] = App.net.LockingRecord.l_sizeB;
                enter_values[2] = App.net.LockingRecord.l_sizeC;
                enter_values[3] = App.net.LockingRecord.l_sizeD;
                enter_values[4] = App.net.LockingRecord.l_sizeE;
                enter_values[5] = App.net.LockingRecord.l_sizeF;
                enter_values[6] = App.net.LockingRecord.l_sizeG;

                latches.Clear();
                Latch latch = new Latch();
                latch.type = drawing_types.dt_none;
                latch.at = new SKPoint(dtop(80), dtop(80) - (lock_db.Height * 0.5f));
                latches.Add(latch);
                latch = new Latch();
                latch.type = drawing_types.dt_none;
                latch.at = new SKPoint(dtop(80), dtop(440) - (lock_db.Height * 0.5f));
                latches.Add(latch);
                for (int i = 0; i < App.net.LockingRecord.l_num; i++)
                {
                    latch = new Latch();
                    drawing_types foo = drawing_types.dt_none;
                    switch (i)
                    {
                        case 0: foo = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.LockingRecord.l_itype1); break;
                        case 1: foo = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.LockingRecord.l_itype2); break;
                        case 2: foo = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.LockingRecord.l_itype3); break;
                        case 3: foo = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.LockingRecord.l_itype4); break;
                        case 4: foo = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.LockingRecord.l_itype5); break;
                        case 5: foo = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.LockingRecord.l_itype6); break;
                        case 6: foo = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.LockingRecord.l_itype7); break;
                    }
                    latch.type = foo;
                    switch (i)
                    {
                        case 0: latch.at = new SKPoint(dtop(80) - lock_db.Width, App.net.LockingRecord.l_fpos1); break;
                        case 1: latch.at = new SKPoint(dtop(80) - lock_db.Width, App.net.LockingRecord.l_fpos2); break;
                        case 2: latch.at = new SKPoint(dtop(80) - lock_db.Width, App.net.LockingRecord.l_fpos3); break;
                        case 3: latch.at = new SKPoint(dtop(80) - lock_db.Width, App.net.LockingRecord.l_fpos4); break;
                        case 4: latch.at = new SKPoint(dtop(80) - lock_db.Width, App.net.LockingRecord.l_fpos5); break;
                        case 5: latch.at = new SKPoint(dtop(80) - lock_db.Width, App.net.LockingRecord.l_fpos6); break;
                        case 6: latch.at = new SKPoint(dtop(80) - lock_db.Width, App.net.LockingRecord.l_fpos7); break;
                    }
                    switch (i)
                    {
                        case 0: enter_values[0] = App.net.LockingRecord.l_sizeA; break;
                        case 1: enter_values[1] = App.net.LockingRecord.l_sizeB; break;
                        case 2: enter_values[2] = App.net.LockingRecord.l_sizeC; break;
                        case 3: enter_values[3] = App.net.LockingRecord.l_sizeD; break;
                        case 4: enter_values[4] = App.net.LockingRecord.l_sizeE; break;
                        case 5: enter_values[5] = App.net.LockingRecord.l_sizeF; break;
                        case 6: enter_values[6] = App.net.LockingRecord.l_sizeG; break;
                    }

                    latches.Add(latch);
                }
                latches.Sort((x, y) => x.at.Y.CompareTo(y.at.Y));
            }

            if (App.net.CurrentItem == "timber")
            {
                size1 = App.net.TimberRecord.l_size1;
                size2 = App.net.TimberRecord.l_size2;

                //lock_position.Y = App.net.TimberRecord.lock_position;

                enter_values[0] = App.net.TimberRecord.l_sizeA;
                enter_values[1] = App.net.TimberRecord.l_sizeB;
                enter_values[2] = App.net.TimberRecord.l_sizeC;
                enter_values[3] = App.net.TimberRecord.l_sizeD;
                enter_values[4] = App.net.TimberRecord.l_sizeE;
                enter_values[5] = App.net.TimberRecord.l_sizeF;
                enter_values[6] = App.net.TimberRecord.l_sizeG;

                latches.Clear();
                Latch latch = new Latch();
                latch.type = drawing_types.dt_none;
                latch.at = new SKPoint(dtop(80), dtop(80) - (lock_db.Height * 0.5f));
                latches.Add(latch);
                latch = new Latch();
                latch.type = drawing_types.dt_none;
                latch.at = new SKPoint(dtop(80), dtop(440) - (lock_db.Height * 0.5f));
                latches.Add(latch);
                for (int i = 0; i < App.net.TimberRecord.l_num; i++)
                {
                    latch = new Latch();
                    drawing_types foo = drawing_types.dt_none;
                    switch (i)
                    {
                        case 0: foo = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.TimberRecord.l_itype1); break;
                        case 1: foo = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.TimberRecord.l_itype2); break;
                        case 2: foo = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.TimberRecord.l_itype3); break;
                        case 3: foo = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.TimberRecord.l_itype4); break;
                        case 4: foo = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.TimberRecord.l_itype5); break;
                        case 5: foo = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.TimberRecord.l_itype6); break;
                        case 6: foo = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.TimberRecord.l_itype7); break;
                    }
                    latch.type = foo;
                    switch (i)
                    {
                        case 0: latch.at = new SKPoint(dtop(80) - lock_db.Width, App.net.TimberRecord.l_fpos1); break;
                        case 1: latch.at = new SKPoint(dtop(80) - lock_db.Width, App.net.TimberRecord.l_fpos2); break;
                        case 2: latch.at = new SKPoint(dtop(80) - lock_db.Width, App.net.TimberRecord.l_fpos3); break;
                        case 3: latch.at = new SKPoint(dtop(80) - lock_db.Width, App.net.TimberRecord.l_fpos4); break;
                        case 4: latch.at = new SKPoint(dtop(80) - lock_db.Width, App.net.TimberRecord.l_fpos5); break;
                        case 5: latch.at = new SKPoint(dtop(80) - lock_db.Width, App.net.TimberRecord.l_fpos6); break;
                        case 6: latch.at = new SKPoint(dtop(80) - lock_db.Width, App.net.TimberRecord.l_fpos7); break;
                    }
                    switch (i)
                    {
                        case 0: enter_values[0] = App.net.TimberRecord.l_sizeA; break;
                        case 1: enter_values[1] = App.net.TimberRecord.l_sizeB; break;
                        case 2: enter_values[2] = App.net.TimberRecord.l_sizeC; break;
                        case 3: enter_values[3] = App.net.TimberRecord.l_sizeD; break;
                        case 4: enter_values[4] = App.net.TimberRecord.l_sizeE; break;
                        case 5: enter_values[5] = App.net.TimberRecord.l_sizeF; break;
                        case 6: enter_values[6] = App.net.TimberRecord.l_sizeG; break;
                    }

                    latches.Add(latch);
                }
                latches.Sort((x, y) => x.at.Y.CompareTo(y.at.Y));
            }
            if (App.net.CurrentItem == "alum")
            {
                size1 = App.net.AlumRecord.l_size1;
                size2 = App.net.AlumRecord.l_size2;

                //lock_position.Y = App.net.AlumRecord.lock_position;

                enter_values[0] = App.net.AlumRecord.l_sizeA;
                enter_values[1] = App.net.AlumRecord.l_sizeB;
                enter_values[2] = App.net.AlumRecord.l_sizeC;
                enter_values[3] = App.net.AlumRecord.l_sizeD;
                enter_values[4] = App.net.AlumRecord.l_sizeE;
                enter_values[5] = App.net.AlumRecord.l_sizeF;
                enter_values[6] = App.net.AlumRecord.l_sizeG;

                latches.Clear();
                Latch latch = new Latch();
                latch.type = drawing_types.dt_none;
                latch.at = new SKPoint(dtop(80), dtop(80) - (lock_db.Height * 0.5f));
                latches.Add(latch);
                latch = new Latch();
                latch.type = drawing_types.dt_none;
                latch.at = new SKPoint(dtop(80), dtop(440) - (lock_db.Height * 0.5f));
                latches.Add(latch);
                for (int i = 0; i < App.net.AlumRecord.l_num; i++)
                {
                    latch = new Latch();
                    drawing_types foo = drawing_types.dt_none;
                    switch (i)
                    {
                        case 0: foo = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.AlumRecord.l_itype1); break;
                        case 1: foo = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.AlumRecord.l_itype2); break;
                        case 2: foo = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.AlumRecord.l_itype3); break;
                        case 3: foo = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.AlumRecord.l_itype4); break;
                        case 4: foo = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.AlumRecord.l_itype5); break;
                        case 5: foo = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.AlumRecord.l_itype6); break;
                        case 6: foo = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.AlumRecord.l_itype7); break;
                    }
                    latch.type = foo;
                    switch (i)
                    {
                        case 0: latch.at = new SKPoint(dtop(80) - lock_db.Width, App.net.AlumRecord.l_fpos1); break;
                        case 1: latch.at = new SKPoint(dtop(80) - lock_db.Width, App.net.AlumRecord.l_fpos2); break;
                        case 2: latch.at = new SKPoint(dtop(80) - lock_db.Width, App.net.AlumRecord.l_fpos3); break;
                        case 3: latch.at = new SKPoint(dtop(80) - lock_db.Width, App.net.AlumRecord.l_fpos4); break;
                        case 4: latch.at = new SKPoint(dtop(80) - lock_db.Width, App.net.AlumRecord.l_fpos5); break;
                        case 5: latch.at = new SKPoint(dtop(80) - lock_db.Width, App.net.AlumRecord.l_fpos6); break;
                        case 6: latch.at = new SKPoint(dtop(80) - lock_db.Width, App.net.AlumRecord.l_fpos7); break;
                    }
                    switch (i)
                    {
                        case 0: enter_values[0] = App.net.AlumRecord.l_sizeA; break;
                        case 1: enter_values[1] = App.net.AlumRecord.l_sizeB; break;
                        case 2: enter_values[2] = App.net.AlumRecord.l_sizeC; break;
                        case 3: enter_values[3] = App.net.AlumRecord.l_sizeD; break;
                        case 4: enter_values[4] = App.net.AlumRecord.l_sizeE; break;
                        case 5: enter_values[5] = App.net.AlumRecord.l_sizeF; break;
                        case 6: enter_values[6] = App.net.AlumRecord.l_sizeG; break;
                    }

                    latches.Add(latch);
                }
                latches.Sort((x, y) => x.at.Y.CompareTo(y.at.Y));
            }
            if (App.net.CurrentItem == "upvc")
            {
                size1 = App.net.UPVCRecord.l_size1;
                size2 = App.net.UPVCRecord.l_size2;

                //lock_position.Y = App.net.UPVCRecord.lock_position;

                enter_values[0] = App.net.UPVCRecord.l_sizeA;
                enter_values[1] = App.net.UPVCRecord.l_sizeB;
                enter_values[2] = App.net.UPVCRecord.l_sizeC;
                enter_values[3] = App.net.UPVCRecord.l_sizeD;
                enter_values[4] = App.net.UPVCRecord.l_sizeE;
                enter_values[5] = App.net.UPVCRecord.l_sizeF;
                enter_values[6] = App.net.UPVCRecord.l_sizeG;

                latches.Clear();
                Latch latch = new Latch();
                latch.type = drawing_types.dt_none;
                latch.at = new SKPoint(dtop(80), dtop(80) - (lock_db.Height * 0.5f));
                latches.Add(latch);
                latch = new Latch();
                latch.type = drawing_types.dt_none;
                latch.at = new SKPoint(dtop(80), dtop(440) - (lock_db.Height * 0.5f));
                latches.Add(latch);
                for (int i = 0; i < App.net.UPVCRecord.l_num; i++)
                {
                    latch = new Latch();
                    drawing_types foo = drawing_types.dt_none;
                    switch (i)
                    {
                        case 0: foo = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.UPVCRecord.l_itype1); break;
                        case 1: foo = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.UPVCRecord.l_itype2); break;
                        case 2: foo = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.UPVCRecord.l_itype3); break;
                        case 3: foo = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.UPVCRecord.l_itype4); break;
                        case 4: foo = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.UPVCRecord.l_itype5); break;
                        case 5: foo = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.UPVCRecord.l_itype6); break;
                        case 6: foo = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.UPVCRecord.l_itype7); break;
                    }
                    latch.type = foo;
                    switch (i)
                    {
                        case 0: latch.at = new SKPoint(dtop(80) - lock_db.Width, App.net.UPVCRecord.l_fpos1); break;
                        case 1: latch.at = new SKPoint(dtop(80) - lock_db.Width, App.net.UPVCRecord.l_fpos2); break;
                        case 2: latch.at = new SKPoint(dtop(80) - lock_db.Width, App.net.UPVCRecord.l_fpos3); break;
                        case 3: latch.at = new SKPoint(dtop(80) - lock_db.Width, App.net.UPVCRecord.l_fpos4); break;
                        case 4: latch.at = new SKPoint(dtop(80) - lock_db.Width, App.net.UPVCRecord.l_fpos5); break;
                        case 5: latch.at = new SKPoint(dtop(80) - lock_db.Width, App.net.UPVCRecord.l_fpos6); break;
                        case 6: latch.at = new SKPoint(dtop(80) - lock_db.Width, App.net.UPVCRecord.l_fpos7); break;
                    }
                    switch (i)
                    {
                        case 0: enter_values[0] = App.net.UPVCRecord.l_sizeA; break;
                        case 1: enter_values[1] = App.net.UPVCRecord.l_sizeB; break;
                        case 2: enter_values[2] = App.net.UPVCRecord.l_sizeC; break;
                        case 3: enter_values[3] = App.net.UPVCRecord.l_sizeD; break;
                        case 4: enter_values[4] = App.net.UPVCRecord.l_sizeE; break;
                        case 5: enter_values[5] = App.net.UPVCRecord.l_sizeF; break;
                        case 6: enter_values[6] = App.net.UPVCRecord.l_sizeG; break;
                    }

                    latches.Add(latch);
                }
                latches.Sort((x, y) => x.at.Y.CompareTo(y.at.Y));
            }
            SetEnterValues();
            ((SKCanvasView)SkCanvasView).InvalidateSurface();
        }

        private void LoadBitmaps()
        {
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream("PropertySurvey.Images.windowlock.png"))
            { lock_bitmap = SKBitmap.Decode(stream); }
            using (Stream stream = assembly.GetManifestResourceStream("PropertySurvey.Images.lock_db.png"))
            { lock_db = SKBitmap.Decode(stream); }
            using (Stream stream = assembly.GetManifestResourceStream("PropertySurvey.Images.lock_hook.png"))
            { lock_hook = SKBitmap.Decode(stream); }
            using (Stream stream = assembly.GetManifestResourceStream("PropertySurvey.Images.lock_latch.png"))
            { lock_latch = SKBitmap.Decode(stream); }
            using (Stream stream = assembly.GetManifestResourceStream("PropertySurvey.Images.lock_cam.png"))
            { lock_cam = SKBitmap.Decode(stream); }
            using (Stream stream = assembly.GetManifestResourceStream("PropertySurvey.Images.lock_mush.png"))
            { lock_mush = SKBitmap.Decode(stream); }

            using (Stream stream = assembly.GetManifestResourceStream("PropertySurvey.Images.check_in.png"))
            { check_in = SKBitmap.Decode(stream); }
            using (Stream stream = assembly.GetManifestResourceStream("PropertySurvey.Images.check_out.png"))
            { check_out = SKBitmap.Decode(stream); }
        }

        private void SetEnterValues()
        {
            if (current_enter == 0)
            {
                distance_text.Text = "Distance 1";
                value_entry.Text = size1;
            }
            else
            {
                //if (current_enter == 1)
                //{
                //    distance_text.Text = "Distance 2";
                //    value_entry.Text = size2;
                //}
                //else
                {
                    distance_text.Text = "Distance " + GetLetter(current_enter - 1);
                    value_entry.Text = enter_values[current_enter - 1];
                }
            }
        }

        private void GetEnterValue()
        {
            if (current_enter == 0)
                size1 = value_entry.Text;
            else
                //if (current_enter == 1)
                //    size2 = value_entry.Text;
                //else
                enter_values[current_enter - 1] = value_entry.Text;
        }

        private void dist_left_click(object sender, EventArgs e)
        {
            GetEnterValue();

            current_enter--;
            if (current_enter < 0)
                current_enter = 0;
            SetEnterValues();
            value_entry.Focus();
        }

        private void dist_right_click(object sender, EventArgs e)
        {
            GetEnterValue();

            current_enter++;
            if (current_enter > latches.Count - 1)
                current_enter = latches.Count - 1;
            SetEnterValues();
            value_entry.Focus();
        }

        private void OnPainting(object sender, SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.White);

            screen_ratio = (float)info.Width / 360.0f;

            if (locks_scaled == false)
            {
                lock_bitmap = lock_bitmap.Resize(new SKImageInfo(lock_bitmap.Width * (int)screen_ratio, lock_bitmap.Width * (int)screen_ratio), SKFilterQuality.High);
                lock_db = lock_db.Resize(new SKImageInfo(lock_db.Width * (int)screen_ratio, lock_db.Width * (int)screen_ratio), SKFilterQuality.High);
                lock_hook = lock_hook.Resize(new SKImageInfo(lock_hook.Width * (int)screen_ratio, lock_hook.Width * (int)screen_ratio), SKFilterQuality.High);
                lock_latch = lock_latch.Resize(new SKImageInfo(lock_latch.Width * (int)screen_ratio, lock_latch.Width * (int)screen_ratio), SKFilterQuality.High);
                lock_cam = lock_cam.Resize(new SKImageInfo(lock_cam.Width * (int)screen_ratio, lock_cam.Width * (int)screen_ratio), SKFilterQuality.High);
                lock_mush = lock_mush.Resize(new SKImageInfo(lock_mush.Width * (int)screen_ratio, lock_mush.Width * (int)screen_ratio), SKFilterQuality.High);

                check_in = check_in.Resize(new SKImageInfo(check_in.Width * (int)screen_ratio, check_in.Width * (int)screen_ratio), SKFilterQuality.High);
                check_out = check_out.Resize(new SKImageInfo(check_out.Width * (int)screen_ratio, check_out.Width * (int)screen_ratio), SKFilterQuality.High);

                LoadValues();
                SetEnterValues();

                locks_scaled = true;
            }

            var touchPathStroke = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                TextSize = 23
            };

            DrawLock();

            for (int i = 0; i < drawing_items.Count; i++)
                draw_item(drawing_items[i], touchPathStroke, canvas);
        }

        private void DrawLock()
        {
            drawing_items.Clear();

            AddLine(new SKPoint(dtop(80), dtop(80)), new SKPoint(dtop(80), dtop(440)), SKColors.Black);//Left
            AddLine(new SKPoint(dtop(80), dtop(80)), new SKPoint(dtop(150), dtop(80)), SKColors.Black);//Left
            AddLine(new SKPoint(dtop(80), dtop(440)), new SKPoint(dtop(150), dtop(440)), SKColors.Black);//Left

            AddBitmap(new SKPoint(dtop(lock_position.X), dtop(lock_position.Y)), drawing_types.dt_lock);

            switch (App.net.CurrentItem)
            {
                case "upvc":
                    if (App.CurrentApp.UPVCRecord.right_bolt == 1)
                        AddBitmap(new SKPoint(dtop(100), dtop(441)), drawing_types.dt_bolt_in);
                    else
                        AddBitmap(new SKPoint(dtop(100), dtop(441)), drawing_types.dt_bolt_out);
                    break;
                case "alum":
                    if (App.CurrentApp.AlumRecord.right_bolt == 1)
                        AddBitmap(new SKPoint(dtop(100), dtop(441)), drawing_types.dt_bolt_in);
                    else
                        AddBitmap(new SKPoint(dtop(100), dtop(441)), drawing_types.dt_bolt_out);
                    break;
                case "timber":
                    if (App.CurrentApp.TimberRecord.right_bolt == 1)
                        AddBitmap(new SKPoint(dtop(100), dtop(441)), drawing_types.dt_bolt_in);
                    else
                        AddBitmap(new SKPoint(dtop(100), dtop(441)), drawing_types.dt_bolt_out);
                    break;
                case "lock":
                    if (App.CurrentApp.LockingRecord.right_bolt == 1)
                        AddBitmap(new SKPoint(dtop(100), dtop(441)), drawing_types.dt_bolt_in);
                    else
                        AddBitmap(new SKPoint(dtop(100), dtop(441)), drawing_types.dt_bolt_out);
                    break;
            }

            switch (App.net.CurrentItem)
            {
                case "upvc":
                    if (App.CurrentApp.UPVCRecord.left_bolt == 1)
                        AddBitmap(new SKPoint(dtop(100), dtop(50)), drawing_types.dt_bolt_in);
                    else
                        AddBitmap(new SKPoint(dtop(100), dtop(50)), drawing_types.dt_bolt_out);
                    break;
                case "alum":
                    if (App.CurrentApp.AlumRecord.left_bolt == 1)
                        AddBitmap(new SKPoint(dtop(100), dtop(50)), drawing_types.dt_bolt_in);
                    else
                        AddBitmap(new SKPoint(dtop(100), dtop(50)), drawing_types.dt_bolt_out);
                    break;
                case "timber":
                    if (App.CurrentApp.TimberRecord.left_bolt == 1)
                        AddBitmap(new SKPoint(dtop(100), dtop(50)), drawing_types.dt_bolt_in);
                    else
                        AddBitmap(new SKPoint(dtop(100), dtop(50)), drawing_types.dt_bolt_out);
                    break;
                case "lock":
                    if (App.CurrentApp.LockingRecord.left_bolt == 1)
                        AddBitmap(new SKPoint(dtop(100), dtop(50)), drawing_types.dt_bolt_in);
                    else
                        AddBitmap(new SKPoint(dtop(100), dtop(50)), drawing_types.dt_bolt_out);
                    break;
            }

            if (selected_item == drawing_types.dt_deadlock ||
               selected_item == drawing_types.dt_hook ||
               selected_item == drawing_types.dt_latch ||
               selected_item == drawing_types.dt_cam ||
               selected_item == drawing_types.dt_mushroom)
            {
                AddBitmap(new SKPoint(mouse_point.X - (lock_db.Height * 0.5f), mouse_point.Y - (lock_db.Height * 0.5f)), selected_item);
            }

            foreach (var latch in latches)
            {
                AddBitmap(latch.at, latch.type);
            }

            if (latches.Count > 1)
            {
                int count = latches.Count - 1;
                for (int i = 0; i < count; i++)
                {
                    SKPoint point1 = new SKPoint(dtop(45), latches[i].at.Y + (lock_db.Height * 0.5f));
                    SKPoint point2 = new SKPoint(dtop(45), latches[i + 1].at.Y + (lock_db.Height * 0.5f));
                    SKPoint point3 = new SKPoint(point1.X + dtop(5), point1.Y + dtop(5));
                    SKPoint point4 = new SKPoint(point1.X - dtop(5), point1.Y + dtop(5));
                    SKPoint point5 = new SKPoint(point2.X + dtop(5), point2.Y - dtop(5));
                    SKPoint point6 = new SKPoint(point2.X - dtop(5), point2.Y - dtop(5));
                    AddLine(point1, point2, SKColors.LightGray);
                    AddLine(point1, point3, SKColors.LightGray);
                    AddLine(point1, point4, SKColors.LightGray);
                    AddLine(point2, point5, SKColors.LightGray);
                    AddLine(point2, point6, SKColors.LightGray);
                    AddText(new SKPoint(dtop(15), latches[i].at.Y + (lock_db.Height * 0.5f) + (((latches[i + 1].at.Y + (lock_db.Height * 0.5f)) - latches[i].at.Y)) * 0.5f), GetLetter(i));
                }
            }

            int y_diff = -70;
            AddBitmap(new SKPoint(dtop(220), dtop(240 + y_diff)), drawing_types.dt_deadlock);
            AddBitmap(new SKPoint(dtop(220), dtop(290 + y_diff)), drawing_types.dt_hook);
            AddBitmap(new SKPoint(dtop(220), dtop(340 + y_diff)), drawing_types.dt_latch);
            AddBitmap(new SKPoint(dtop(220), dtop(390 + y_diff)), drawing_types.dt_cam);
            AddBitmap(new SKPoint(dtop(220), dtop(440 + y_diff)), drawing_types.dt_mushroom);

            y_diff += 25;
            AddText(new SKPoint(dtop(260), dtop(240 + y_diff)), "Deadlock");
            AddText(new SKPoint(dtop(260), dtop(290 + y_diff)), "Hook");
            AddText(new SKPoint(dtop(260), dtop(340 + y_diff)), "Latch");
            AddText(new SKPoint(dtop(260), dtop(390 + y_diff)), "Cam");
            AddText(new SKPoint(dtop(260), dtop(440 + y_diff)), "Mushroom");
        }

        private string GetLetter(int val)
        {
            switch (val)
            {
                case 0: return "A";
                case 1: return "B";
                case 2: return "C";
                case 3: return "D";
                case 4: return "E";
                case 5: return "F";
                case 6: return "G";
                case 7: return "H";
                case 8: return "I";
            }
            return "?";
        }
        private void AddText(SKPoint at, string text)
        {
            DrawItem text_item = new DrawItem();
            text_item.text = text;
            text_item.at = at;
            text_item.drawing_type = drawing_types.dt_text;
            drawing_items.Add(text_item);
        }

        private void AddBitmap(SKPoint at, drawing_types type)
        {
            DrawItem bmp_item = new DrawItem();
            bmp_item.at = at;
            bmp_item.drawing_type = type;
            drawing_items.Add(bmp_item);
        }

        private void AddLine(SKPoint from, SKPoint to, SKColor color)
        {
            DrawItem line_item = new DrawItem();
            line_item.drawing_type = drawing_types.dt_line;
            line_item.path = new SKPath();
            line_item.path.MoveTo(from);
            line_item.path.LineTo(to);
            line_item.color = color;
            drawing_items.Add(line_item);
        }

        private float dtop(float dips)
        {
            return dips * screen_ratio;
        }

        private float ptod(float dips)
        {
            return dips / screen_ratio;
        }

        int invert(int bolt)
        {
            if (bolt == 1)
                return 0;
            else
                return 1;
        }

        void draw_item(DrawItem drawing_item, SKPaint a_path, SKCanvas canvas)
        {
            switch (drawing_item.drawing_type)
            {
                case drawing_types.dt_line:
                    a_path.StrokeWidth = 5;
                    a_path.Color = drawing_item.color;
                    a_path.Style = SKPaintStyle.Stroke;
                    canvas.DrawPath(drawing_item.path, a_path);
                    break;
                case drawing_types.dt_text:
                    //if (App.net.TextEntryText != null && App.net.TextEntryText != "")
                    //{
                    //    drawing_item.text = App.net.TextEntryText;
                    //}
                    a_path.Color = SKColors.Black;
                    a_path.Style = SKPaintStyle.Fill;
                    // fint size needs to be dpi?
                    a_path.TextSize = 50.0f;
                    canvas.DrawText(drawing_item.text, drawing_item.at.X, drawing_item.at.Y, a_path);
                    break;
                case drawing_types.dt_lock: canvas.DrawBitmap(lock_bitmap, drawing_item.at); break;
                case drawing_types.dt_deadlock: canvas.DrawBitmap(lock_db, drawing_item.at); break;
                case drawing_types.dt_hook: canvas.DrawBitmap(lock_hook, drawing_item.at); break;
                case drawing_types.dt_latch: canvas.DrawBitmap(lock_latch, drawing_item.at); break;
                case drawing_types.dt_cam: canvas.DrawBitmap(lock_cam, drawing_item.at); break;
                case drawing_types.dt_mushroom: canvas.DrawBitmap(lock_mush, drawing_item.at); break;
                case drawing_types.dt_bolt_in: canvas.DrawBitmap(check_in, drawing_item.at); break;
                case drawing_types.dt_bolt_out: canvas.DrawBitmap(check_out, drawing_item.at); break;
            }
        }

        private void OnTouch(object sender, SKTouchEventArgs e)
        {
            int y_diff = -70;

            switch (e.ActionType)
            {
                case SKTouchAction.Pressed:
                    mouse_point = e.Location;
                    last_mouse_point = mouse_point;

                    for (int i = latches.Count - 1; i >= 0; i--)
                    {
                        if (latches[i].type != drawing_types.dt_none)
                        {
                            if (mouse_point.X > latches[i].at.X - (lock_db.Width * 0.5f) && mouse_point.X < latches[i].at.X + lock_db.Width + (lock_db.Width * 0.5f) &&
                                mouse_point.Y > latches[i].at.Y - (lock_db.Height * 0.5f) && mouse_point.Y < latches[i].at.Y + lock_db.Height + (lock_db.Height * 0.5f))
                            {
                                selected_item = latches[i].type;
                                latches.RemoveAt(i);
                            }
                        }
                    }

                    //if (mouse_point.X > dtop(lock_position.X) && mouse_point.X < dtop(lock_position.X) + lock_bitmap.Width &&
                    //   mouse_point.Y > dtop(lock_position.Y) && mouse_point.Y < dtop(lock_position.Y) + lock_bitmap.Height)
                    //    selected_item = drawing_types.dt_lock;

                    if (mouse_point.X > dtop(220) && mouse_point.X < dtop(220) + lock_db.Width &&
                        mouse_point.Y > dtop(240 + y_diff) && mouse_point.Y < dtop(240 + y_diff) + lock_db.Height)
                        selected_item = drawing_types.dt_deadlock;
                    if (mouse_point.X > dtop(220) && mouse_point.X < dtop(220) + lock_db.Width &&
                        mouse_point.Y > dtop(290 + y_diff) && mouse_point.Y < dtop(290 + y_diff) + lock_db.Height)
                        selected_item = drawing_types.dt_hook;
                    if (mouse_point.X > dtop(220) && mouse_point.X < dtop(220) + lock_db.Width &&
                        mouse_point.Y > dtop(340 + y_diff) && mouse_point.Y < dtop(340 + y_diff) + lock_db.Height)
                        selected_item = drawing_types.dt_latch;
                    if (mouse_point.X > dtop(220) && mouse_point.X < dtop(220) + lock_db.Width &&
                        mouse_point.Y > dtop(390 + y_diff) && mouse_point.Y < dtop(390 + y_diff) + lock_db.Height)
                        selected_item = drawing_types.dt_cam;
                    if (mouse_point.X > dtop(220) && mouse_point.X < dtop(220) + lock_db.Width &&
                        mouse_point.Y > dtop(440 + y_diff) && mouse_point.Y < dtop(440 + y_diff) + lock_db.Height)
                        selected_item = drawing_types.dt_mushroom;

                    if (mouse_point.X > dtop(100) && mouse_point.X < dtop(130) &&
                        mouse_point.Y > dtop(50) && mouse_point.Y < dtop(80))
                    {
                        switch (App.net.CurrentItem)
                        {
                            case "upvc": App.CurrentApp.UPVCRecord.left_bolt = invert(App.CurrentApp.UPVCRecord.left_bolt); break;
                            case "alum": App.CurrentApp.AlumRecord.left_bolt = invert(App.CurrentApp.AlumRecord.left_bolt); break;
                            case "timber": App.CurrentApp.TimberRecord.left_bolt = invert(App.CurrentApp.TimberRecord.left_bolt); break;
                            case "lock": App.CurrentApp.LockingRecord.left_bolt = invert(App.CurrentApp.LockingRecord.left_bolt); break;
                        }
                    }

                    if (mouse_point.X > dtop(100) && mouse_point.X < dtop(130) &&
                        mouse_point.Y > dtop(441) && mouse_point.Y < dtop(471))
                    {
                        switch (App.net.CurrentItem)
                        {
                            case "upvc": App.CurrentApp.UPVCRecord.right_bolt = invert(App.CurrentApp.UPVCRecord.right_bolt); break;
                            case "alum": App.CurrentApp.AlumRecord.right_bolt = invert(App.CurrentApp.AlumRecord.right_bolt); break;
                            case "timber": App.CurrentApp.TimberRecord.right_bolt = invert(App.CurrentApp.TimberRecord.right_bolt); break;
                            case "lock": App.CurrentApp.LockingRecord.right_bolt = invert(App.CurrentApp.LockingRecord.right_bolt); break;
                        }
                    }
                    break;

                case SKTouchAction.Moved:
                    last_mouse_point = mouse_point;
                    mouse_point = e.Location;
                    currentPoint += mouse_point - last_mouse_point;

                    if (selected_item == drawing_types.dt_lock)
                    {
                        lock_position.Y += ptod(mouse_point.Y - last_mouse_point.Y);
                    }
                    break;

                case SKTouchAction.Released:
                    if (selected_item == drawing_types.dt_deadlock ||
                       selected_item == drawing_types.dt_hook ||
                       selected_item == drawing_types.dt_latch ||
                       selected_item == drawing_types.dt_cam ||
                       selected_item == drawing_types.dt_mushroom)
                    {
                        if (latches.Count < 6 && (mouse_point.X < dtop(80)))
                        {
                            Latch latch = new Latch();
                            latch.type = selected_item;
                            latch.at = new SKPoint(dtop(80) - lock_db.Width, mouse_point.Y);
                            latches.Add(latch);
                            latches.Sort((x, y) => x.at.Y.CompareTo(y.at.Y));
                        }
                    }
                    selected_item = drawing_types.dt_none;
                    break;
            }
            e.Handled = true;
            ((SKCanvasView)sender).InvalidateSurface();
        }
    }
}