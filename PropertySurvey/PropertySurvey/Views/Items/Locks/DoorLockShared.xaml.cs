using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.Reflection;
using SkiaSharp;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DoorLockShared : ContentPage
	{
        protected enum drawing_types { dt_none, dt_line, dt_text, dt_lock, dt_deadlock, dt_hook, dt_latch, dt_cam, dt_mushroom };
        protected class DrawItem
        {
            public drawing_types drawing_type;
            public SKPoint at;
            public string text = "";
            public SKPath path;
            public SKColor color = SKColors.Black;
        }

        protected class Latch
        {
            public drawing_types type;
            public SKPoint at;
        }

        protected string[] enter_values = new string[7];
        protected List<DrawItem> drawing_items = new List<DrawItem>();
        protected string size1 = "", size2 = "";
        protected List<Latch> latches = new List<Latch>();
        protected SKPoint lock_position = new SKPoint(80.0f, 240.0f);
        protected float screen_ratio = 1.0f;
        protected bool locks_scaled = false;
        protected SKBitmap lock_bitmap = null;
        protected SKBitmap lock_db = null;
        protected SKBitmap lock_hook = null;
        protected SKBitmap lock_latch = null;
        protected SKBitmap lock_cam = null;
        protected SKBitmap lock_mush = null;

        public DoorLockShared ()
		{
			InitializeComponent ();
            LoadBitmaps();

            enter_values[0] = "";
            enter_values[1] = "";
            enter_values[2] = "";
            enter_values[3] = "";
            enter_values[4] = "";
            enter_values[5] = "";
            enter_values[6] = "";
        }

        protected float dtop(float dips)
        {
            return dips * screen_ratio;
        }

        protected void LoadBitmaps()
        {
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream("PropertySurvey.Images.lock.png")) { lock_bitmap = SKBitmap.Decode(stream); }
            using (Stream stream = assembly.GetManifestResourceStream("PropertySurvey.Images.lock_db.png")) { lock_db = SKBitmap.Decode(stream); }
            using (Stream stream = assembly.GetManifestResourceStream("PropertySurvey.Images.lock_hook.png")) { lock_hook = SKBitmap.Decode(stream); }
            using (Stream stream = assembly.GetManifestResourceStream("PropertySurvey.Images.lock_latch.png")) { lock_latch = SKBitmap.Decode(stream); }
            using (Stream stream = assembly.GetManifestResourceStream("PropertySurvey.Images.lock_cam.png")) { lock_cam = SKBitmap.Decode(stream); }
            using (Stream stream = assembly.GetManifestResourceStream("PropertySurvey.Images.lock_mush.png")) { lock_mush = SKBitmap.Decode(stream); }
        }

        protected void resize_bitmaps()
        {
            lock_bitmap = lock_bitmap.Resize(new SKImageInfo(lock_bitmap.Width * (int)screen_ratio, lock_bitmap.Width * (int)screen_ratio), SKFilterQuality.High);
            lock_db = lock_db.Resize(new SKImageInfo(lock_db.Width * (int)screen_ratio, lock_db.Width * (int)screen_ratio), SKFilterQuality.High);
            lock_hook = lock_hook.Resize(new SKImageInfo(lock_hook.Width * (int)screen_ratio, lock_hook.Width * (int)screen_ratio), SKFilterQuality.High);
            lock_latch = lock_latch.Resize(new SKImageInfo(lock_latch.Width * (int)screen_ratio, lock_latch.Width * (int)screen_ratio), SKFilterQuality.High);
            lock_cam = lock_cam.Resize(new SKImageInfo(lock_cam.Width * (int)screen_ratio, lock_cam.Width * (int)screen_ratio), SKFilterQuality.High);
            lock_mush = lock_mush.Resize(new SKImageInfo(lock_mush.Width * (int)screen_ratio, lock_mush.Width * (int)screen_ratio), SKFilterQuality.High);
        }

        protected void LoadValues()
        {
            if (App.net.CurrentItem == "lock")
            {
                size1 = App.net.LockingRecord.l_size1;
                size2 = App.net.LockingRecord.l_size2;

                enter_values[0] = App.net.LockingRecord.l_sizeA;
                enter_values[1] = App.net.LockingRecord.l_sizeB;
                enter_values[2] = App.net.LockingRecord.l_sizeC;
                enter_values[3] = App.net.LockingRecord.l_sizeD;
                enter_values[4] = App.net.LockingRecord.l_sizeE;
                enter_values[5] = App.net.LockingRecord.l_sizeF;
                enter_values[6] = App.net.LockingRecord.l_sizeG;

                latches.Clear();
                for (int i = 0; i < App.net.LockingRecord.l_num; i++)
                {
                    Latch latch = new Latch();
                    latch.type = drawing_types.dt_none;
                    switch (i)
                    {
                        case 0: latch.type = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.LockingRecord.l_itype1); break;
                        case 1: latch.type = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.LockingRecord.l_itype2); break;
                        case 2: latch.type = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.LockingRecord.l_itype3); break;
                        case 3: latch.type = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.LockingRecord.l_itype4); break;
                        case 4: latch.type = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.LockingRecord.l_itype5); break;
                        case 5: latch.type = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.LockingRecord.l_itype6); break;
                        case 6: latch.type = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.LockingRecord.l_itype7); break;
                    }

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

                enter_values[0] = App.net.TimberRecord.l_sizeA;
                enter_values[1] = App.net.TimberRecord.l_sizeB;
                enter_values[2] = App.net.TimberRecord.l_sizeC;
                enter_values[3] = App.net.TimberRecord.l_sizeD;
                enter_values[4] = App.net.TimberRecord.l_sizeE;
                enter_values[5] = App.net.TimberRecord.l_sizeF;
                enter_values[6] = App.net.TimberRecord.l_sizeG;

                latches.Clear();
                for (int i = 0; i < App.net.TimberRecord.l_num; i++)
                {
                    Latch latch = new Latch();
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
                enter_values[0] = App.net.AlumRecord.l_sizeA;
                enter_values[1] = App.net.AlumRecord.l_sizeB;
                enter_values[2] = App.net.AlumRecord.l_sizeC;
                enter_values[3] = App.net.AlumRecord.l_sizeD;
                enter_values[4] = App.net.AlumRecord.l_sizeE;
                enter_values[5] = App.net.AlumRecord.l_sizeF;
                enter_values[6] = App.net.AlumRecord.l_sizeG;

                latches.Clear();

                for (int i = 0; i < App.net.AlumRecord.l_num; i++)
                {
                    Latch latch = new Latch();
                    latch.type = drawing_types.dt_none;
                    switch (i)
                    {
                        case 0: latch.type = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.AlumRecord.l_itype1); break;
                        case 1: latch.type = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.AlumRecord.l_itype2); break;
                        case 2: latch.type = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.AlumRecord.l_itype3); break;
                        case 3: latch.type = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.AlumRecord.l_itype4); break;
                        case 4: latch.type = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.AlumRecord.l_itype5); break;
                        case 5: latch.type = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.AlumRecord.l_itype6); break;
                        case 6: latch.type = (drawing_types)Enum.ToObject(typeof(drawing_types), App.net.AlumRecord.l_itype7); break;
                    }

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
                enter_values[0] = App.net.UPVCRecord.l_sizeA;
                enter_values[1] = App.net.UPVCRecord.l_sizeB;
                enter_values[2] = App.net.UPVCRecord.l_sizeC;
                enter_values[3] = App.net.UPVCRecord.l_sizeD;
                enter_values[4] = App.net.UPVCRecord.l_sizeE;
                enter_values[5] = App.net.UPVCRecord.l_sizeF;
                enter_values[6] = App.net.UPVCRecord.l_sizeG;

                latches.Clear();

                for (int i = 0; i < App.net.UPVCRecord.l_num; i++)
                {
                    Latch latch = new Latch();
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
        }

        protected void AddText(SKPoint at, string text)
        {
            DrawItem text_item = new DrawItem();
            text_item.text = text;
            text_item.at = at;
            text_item.drawing_type = drawing_types.dt_text;
            drawing_items.Add(text_item);
        }

        protected void AddLine(SKPoint from, SKPoint to, SKColor color)
        {
            DrawItem line_item = new DrawItem();
            line_item.drawing_type = drawing_types.dt_line;
            line_item.path = new SKPath();
            line_item.path.MoveTo(from);
            line_item.path.LineTo(to);
            line_item.color = color;
            drawing_items.Add(line_item);
        }

        protected void AddBitmap(SKPoint at, drawing_types type)
        {
            DrawItem bmp_item = new DrawItem();
            bmp_item.at = at;
            bmp_item.drawing_type = type;
            drawing_items.Add(bmp_item);
        }

        protected string GetLetter(int val)
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

        protected void draw_latches()
        {

            foreach (var latch in latches)
                AddBitmap(latch.at, latch.type);

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
        }

        protected void draw_item(DrawItem drawing_item, SKPaint a_path, SKCanvas canvas)
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
            }
        }
    }
}