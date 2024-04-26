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
	public partial class Diagram : ContentPage
    {
        enum drawing_types { dt_none, dt_scratch, dt_dent, dt_hole, dt_trash, dt_van, dt_text };
        private class DrawItem
        {
            public drawing_types drawing_type;
            public SKPoint at;
            public string text = "";
            public SKPath path;
            public SKColor color = SKColors.Black;
        }

        private class Latch
        {
            public drawing_types type;
            public SKPoint at;
        }

        private SKBitmap background_bitmap = null;

        List<Latch> latches = new List<Latch>();

        private List<DrawItem> drawing_items = new List<DrawItem>();

        private SKBitmap scratch_bitmap = null;
        private SKBitmap dent_bitmap = null;
        private SKBitmap hole_bitmap = null;
        private SKBitmap trash_can_bitmap = null;
        private SKBitmap van_bitmap = null;

        drawing_types selected_item = drawing_types.dt_none;

        bool things_scaled = false;

        float screen_ratio = 0.0f;

        SKPoint mouse_point, last_mouse_point;

        SKPoint currentPoint;

        List<DamageLabels> damage_labels;

        public Diagram ()
		{
			InitializeComponent ();

            LoadBitmaps();

            SetDescriptionText();

            LoadLatches();
        }

        private void LoadLatches()
        {
            List<DamageLabels> labels = App.data.GetDamageLabels();
            latches = new List<Latch>();

            foreach (var label in labels)
            {
                Latch latch = new Latch();
                latch.at = new SKPoint(label.pos_x, label.pos_y);
                switch(label.label_type)
                {
                    case 1:
                        latch.type = drawing_types.dt_scratch;break;
                    case 2:
                        latch.type = drawing_types.dt_dent; break;
                    case 3:
                        latch.type = drawing_types.dt_hole; break;
                }
                latches.Add(latch);
            }
        }

        private void SaveLatches()
        {
            List<DamageLabels> labels = new List<DamageLabels>();

            foreach (var latch in latches)
            {
                DamageLabels label = new DamageLabels();

                switch (latch.type)
                {
                    case drawing_types.dt_scratch:
                        label.label_type = 1; break;
                    case drawing_types.dt_dent:
                        label.label_type = 2; break;
                    case drawing_types.dt_hole:
                        label.label_type = 3; break;
                }
                label.pos_x = (int)latch.at.X;
                label.pos_y = (int)latch.at.Y;
                switch (App.CurrentApp.CurrentItem)
                {
                    case "deliveryvan": label.item_no = App.CurrentApp.DeliveryVanVehicleCheckList.item_no;
                        label.CheckID = App.CurrentApp.VanChecksHeader.unique_id;
                        label.vehicle_type = "deliveryvan";
                        label.angle_num = App.CurrentApp.current_van_picture;
                        break;
                    case "delivery": label.item_no = App.CurrentApp.DeliveryVehicleCheckList.item_no;
                        label.CheckID = App.CurrentApp.VanChecksHeader.unique_id;
                        label.vehicle_type = "delivery";
                        label.angle_num = App.CurrentApp.current_van_picture;
                        break;
                    case "van": label.item_no = App.CurrentApp.WeeklyVanCheckSheet.item_no;
                        label.CheckID = App.CurrentApp.VanChecksHeader.unique_id;
                        label.vehicle_type = "van";
                        label.angle_num = App.CurrentApp.current_van_picture;
                        break;
                    case "car": label.item_no = App.CurrentApp.CarPanelSheet.item_no;
                        label.CheckID = App.CurrentApp.VanChecksHeader.unique_id;
                        label.vehicle_type = "car";
                        label.angle_num = App.CurrentApp.current_van_picture;
                        break;
                }
                labels.Add(label);
            }
            App.data.SaveDamageLabels(labels);
        }

        private void LoadBitmaps()
        {
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream("PropertySurvey.Images.damage_scratch.png"))
            {
                scratch_bitmap = SKBitmap.Decode(stream);
            }
            using (Stream stream = assembly.GetManifestResourceStream("PropertySurvey.Images.damage_dent.png"))
            {
                dent_bitmap = SKBitmap.Decode(stream);
            }
            using (Stream stream = assembly.GetManifestResourceStream("PropertySurvey.Images.damage_hole.png"))
            {
                hole_bitmap = SKBitmap.Decode(stream);
            }
            using (Stream stream = assembly.GetManifestResourceStream("PropertySurvey.Images.trash-can.png"))
            {
                trash_can_bitmap = SKBitmap.Decode(stream);
            }
            LoadImage();
        }

        /*
        void LoadImageFromFile(string fname)
        {
            if (App.files.FileExists(fname))
            {
                byte[] data = App.files.LoadBinary(fname);
                Stream stream = new MemoryStream(data);
                van_bitmap = SKBitmap.Decode(stream);
            }
        }*/

        protected override bool OnBackButtonPressed()
        {
            if (damage_desc_text.Text.Length == 0)
            {
                damage_desc_text.Text = " ";
            }
            GetDescriptionTextAndSaveThumbnail();
            App.data.DeleteDamageLabels();
            SaveLatches();


            base.OnBackButtonPressed();
            return false;
        }

        private void GetDescriptionTextAndSaveThumbnail()
        {
            string check_type = "";
            int item_no = 0;
            string fname = "";
            int i;

            switch (App.CurrentApp.CurrentItem)
            {
                case "deliveryvan": item_no = App.CurrentApp.DeliveryVanVehicleCheckList.item_no; check_type = "a"; break;
                case "delivery": item_no = App.CurrentApp.DeliveryVehicleCheckList.item_no; check_type = "d"; break;
                case "van": item_no = App.CurrentApp.WeeklyVanCheckSheet.item_no; check_type = "v"; break;
                case "car": item_no = App.CurrentApp.CarPanelSheet.item_no; check_type = "c"; break;
            }

            switch (App.CurrentApp.current_van_picture)
            {
                case 1:
                    fname = string.Format("Photos/VC/" + App.CurrentApp.VanChecksHeader.unique_id + "_{0:00000000}_" + check_type + "_pad.jpg", item_no);
                    switch (App.CurrentApp.CurrentItem)
                    {
                        case "deliveryvan": App.CurrentApp.DeliveryVanVehicleCheckList.damage_pass = damage_desc_text.Text; break;
                        case "delivery": App.CurrentApp.DeliveryVehicleCheckList.damage_pass = damage_desc_text.Text; break;
                        case "van": App.CurrentApp.WeeklyVanCheckSheet.damage_pass = damage_desc_text.Text; break;
                        case "car": App.CurrentApp.CarPanelSheet.damage_pass = damage_desc_text.Text; break;
                    }
                    break;
                case 2:
                    fname = string.Format("Photos/VC/" + App.CurrentApp.VanChecksHeader.unique_id + "_{0:00000000}_" + check_type + "_drd.jpg", item_no);
                    switch (App.CurrentApp.CurrentItem)
                    {
                        case "deliveryvan": App.CurrentApp.DeliveryVanVehicleCheckList.damage_driver = damage_desc_text.Text; break;
                        case "delivery": App.CurrentApp.DeliveryVehicleCheckList.damage_driver = damage_desc_text.Text; break;
                        case "van": App.CurrentApp.WeeklyVanCheckSheet.damage_driver = damage_desc_text.Text; break;
                        case "car": App.CurrentApp.CarPanelSheet.damage_driver = damage_desc_text.Text; break;
                    }
                    break;
                case 3:
                    fname = string.Format("Photos/VC/" + App.CurrentApp.VanChecksHeader.unique_id + "_{0:00000000}_" + check_type + "_frd.jpg", item_no);
                    switch (App.CurrentApp.CurrentItem)
                    {
                        case "deliveryvan": App.CurrentApp.DeliveryVanVehicleCheckList.damage_front = damage_desc_text.Text; break;
                        case "delivery": App.CurrentApp.DeliveryVehicleCheckList.damage_front = damage_desc_text.Text; break;
                        case "van": App.CurrentApp.WeeklyVanCheckSheet.damage_front = damage_desc_text.Text; break;
                        case "car": App.CurrentApp.CarPanelSheet.damage_front = damage_desc_text.Text; break;
                    }
                    break;
                case 4:
                    fname = string.Format("Photos/VC/" + App.CurrentApp.VanChecksHeader.unique_id + "_{0:00000000}_" + check_type + "_red.jpg", item_no);
                    switch (App.CurrentApp.CurrentItem)
                    {
                        case "deliveryvan": App.CurrentApp.DeliveryVanVehicleCheckList.damage_back = damage_desc_text.Text; break;
                        case "delivery": App.CurrentApp.DeliveryVehicleCheckList.damage_back = damage_desc_text.Text; break;
                        case "van": App.CurrentApp.WeeklyVanCheckSheet.damage_back = damage_desc_text.Text; break;
                        case "car": App.CurrentApp.CarPanelSheet.damage_back = damage_desc_text.Text; break;
                    }
                    break;
            }
            RedrawBitmap(false); // without trash
            SkCanvasView.InvalidateSurface();
            // create an image and then get the PNG (or any other) encoded data
            using (var image = SKImage.FromBitmap(background_bitmap))
            using (var data = image.Encode(SKEncodedImageFormat.Png, 80))
            {
                byte[] dd = data.ToArray();
                App.files.SaveBinary(fname, data.ToArray());
            }

        }

        private void SetDescriptionText()
        {
            switch (App.CurrentApp.current_van_picture)
            {
                case 1:
                    switch (App.CurrentApp.CurrentItem)
                    {
                        case "deliveryvan": damage_desc_text.Text = App.CurrentApp.DeliveryVanVehicleCheckList.damage_pass; break;
                        case "delivery": damage_desc_text.Text = App.CurrentApp.DeliveryVehicleCheckList.damage_pass; break;
                        case "van": damage_desc_text.Text = App.CurrentApp.WeeklyVanCheckSheet.damage_pass; break;
                        case "car": damage_desc_text.Text = App.CurrentApp.CarPanelSheet.damage_pass; break;
                    }
                    break;
                case 2:
                    switch (App.CurrentApp.CurrentItem)
                    {
                        case "deliveryvan": damage_desc_text.Text = App.CurrentApp.DeliveryVanVehicleCheckList.damage_driver; break;
                        case "delivery": damage_desc_text.Text = App.CurrentApp.DeliveryVehicleCheckList.damage_driver; break;
                        case "van": damage_desc_text.Text = App.CurrentApp.WeeklyVanCheckSheet.damage_driver; break;
                        case "car": damage_desc_text.Text = App.CurrentApp.CarPanelSheet.damage_driver; break;
                    }
                    break;
                case 3:
                    switch (App.CurrentApp.CurrentItem)
                    {
                        case "deliveryvan": damage_desc_text.Text = App.CurrentApp.DeliveryVanVehicleCheckList.damage_front; break;
                        case "delivery": damage_desc_text.Text = App.CurrentApp.DeliveryVehicleCheckList.damage_front; break;
                        case "van": damage_desc_text.Text = App.CurrentApp.WeeklyVanCheckSheet.damage_front; break;
                        case "car": damage_desc_text.Text = App.CurrentApp.CarPanelSheet.damage_front; break;
                    }
                    break;
                    break;
                case 4:
                    switch (App.CurrentApp.CurrentItem)
                    {
                        case "deliveryvan": damage_desc_text.Text = App.CurrentApp.DeliveryVanVehicleCheckList.damage_back; break;
                        case "delivery": damage_desc_text.Text = App.CurrentApp.DeliveryVehicleCheckList.damage_back; break;
                        case "van": damage_desc_text.Text = App.CurrentApp.WeeklyVanCheckSheet.damage_back; break;
                        case "car": damage_desc_text.Text = App.CurrentApp.CarPanelSheet.damage_back; break;
                    }
                    break;
            }
        }

        private void LoadImage()
        {
            Assembly assembly = GetType().GetTypeInfo().Assembly;

            switch (App.CurrentApp.CurrentItem)
            {
                case "deliveryvan":
                    switch(App.CurrentApp.current_van_picture)
                    {
                        case 1:
                            van_bitmap = SKBitmap.Decode(assembly.GetManifestResourceStream("PropertySurvey.Images.hgv_pas.png"));
                            break;
                        case 2:
                            van_bitmap = SKBitmap.Decode(assembly.GetManifestResourceStream("PropertySurvey.Images.hgv_drv.png"));
                            break;
                        case 3:
                            van_bitmap = SKBitmap.Decode(assembly.GetManifestResourceStream("PropertySurvey.Images.hgv_fnt.png"));
                            break;
                        case 4:
                            van_bitmap = SKBitmap.Decode(assembly.GetManifestResourceStream("PropertySurvey.Images.hgv_bak.png"));
                            break;
                    }
                    break;
                case "delivery":
                    switch (App.CurrentApp.current_van_picture)
                    {
                        case 1:
                            van_bitmap = SKBitmap.Decode(assembly.GetManifestResourceStream("PropertySurvey.Images.hgv_pas.png"));
                            break;
                        case 2:
                            van_bitmap = SKBitmap.Decode(assembly.GetManifestResourceStream("PropertySurvey.Images.hgv_drv.png"));
                            break;
                        case 3:
                            van_bitmap = SKBitmap.Decode(assembly.GetManifestResourceStream("PropertySurvey.Images.hgv_fnt.png"));
                            break;
                        case 4:
                            van_bitmap = SKBitmap.Decode(assembly.GetManifestResourceStream("PropertySurvey.Images.hgv_bak.png"));
                            break;
                    }
                    break;
                case "van":
                    switch (App.CurrentApp.current_van_picture)
                    {
                        case 1:
                            van_bitmap = SKBitmap.Decode(assembly.GetManifestResourceStream("PropertySurvey.Images.van1.png"));
                            break;
                        case 2:
                            van_bitmap = SKBitmap.Decode(assembly.GetManifestResourceStream("PropertySurvey.Images.van2.png"));
                            break;
                        case 3:
                            van_bitmap = SKBitmap.Decode(assembly.GetManifestResourceStream("PropertySurvey.Images.van3.png"));
                            break;
                        case 4:
                            van_bitmap = SKBitmap.Decode(assembly.GetManifestResourceStream("PropertySurvey.Images.van4.png"));
                            break;
                    }
                    break;
                case "car":
                    switch (App.CurrentApp.current_van_picture)
                    {
                        case 1:
                            van_bitmap = SKBitmap.Decode(assembly.GetManifestResourceStream("PropertySurvey.Images.car_pas.png"));
                            break;
                        case 2:
                            van_bitmap = SKBitmap.Decode(assembly.GetManifestResourceStream("PropertySurvey.Images.car_drv.png"));
                            break;
                        case 3:
                            van_bitmap = SKBitmap.Decode(assembly.GetManifestResourceStream("PropertySurvey.Images.car_fnt.png"));
                            break;
                        case 4:
                            van_bitmap = SKBitmap.Decode(assembly.GetManifestResourceStream("PropertySurvey.Images.car_bak.png"));
                            break;
                    }
                    break;
            }
        }

        
        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Send(this, "preventPortrait");

        }

        //during page close setting back to portrait
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Send(this, "allowLandScapePortrait");
        }

        private void OnPainting(object sender, SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.White);

            if (background_bitmap == null)
            {
                background_bitmap = new SKBitmap((int)info.Width, (int)info.Height);
            }

            screen_ratio = (float)info.Width / 512.0f;

            if (things_scaled == false)
            {
                scratch_bitmap = scratch_bitmap.Resize(new SKImageInfo(scratch_bitmap.Width * (int)screen_ratio, scratch_bitmap.Width * (int)screen_ratio), SKFilterQuality.High);
                dent_bitmap = dent_bitmap.Resize(new SKImageInfo(dent_bitmap.Width * (int)screen_ratio, dent_bitmap.Width * (int)screen_ratio), SKFilterQuality.High);
                hole_bitmap = hole_bitmap.Resize(new SKImageInfo(hole_bitmap.Width * (int)screen_ratio, hole_bitmap.Width * (int)screen_ratio), SKFilterQuality.High);
                trash_can_bitmap = trash_can_bitmap.Resize(new SKImageInfo(trash_can_bitmap.Width * (int)screen_ratio, trash_can_bitmap.Width * (int)screen_ratio), SKFilterQuality.High);

                LoadValues();
                //SetEnterValues();
                float scale = dtop(450.0f) / van_bitmap.Width;
                van_bitmap = van_bitmap.Resize(new SKImageInfo((int)dtop(450.0f), (int)((float)van_bitmap.Height * scale)), SKFilterQuality.High);

                things_scaled = true;
            }

            var touchPathStroke = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                TextSize = 23
            };

            DrawVan();

            for (int i = 0; i < drawing_items.Count; i++)
                draw_item(drawing_items[i], touchPathStroke, canvas);
        }

        private void DrawVan()
        {
            drawing_items.Clear();

            if (selected_item == drawing_types.dt_scratch ||
                   selected_item == drawing_types.dt_dent ||
                   selected_item == drawing_types.dt_hole)
            {
                AddBitmap(new SKPoint(mouse_point.X - (hole_bitmap.Height * 0.5f), mouse_point.Y - (hole_bitmap.Height * 0.5f)), selected_item);
            }

            foreach (var latch in latches)
            {
                AddBitmap(latch.at, latch.type);
            }

            AddBitmap(new SKPoint(dtop(0), dtop(0)), drawing_types.dt_van);

            AddBitmap(new SKPoint(dtop(450), dtop(20)), drawing_types.dt_scratch);
            AddBitmap(new SKPoint(dtop(450), dtop(80)), drawing_types.dt_dent);
            AddBitmap(new SKPoint(dtop(450), dtop(150)), drawing_types.dt_hole);
            //AddBitmap(new SKPoint(dtop(440), dtop(220)), drawing_types.dt_trash);

            AddText(new SKPoint(dtop(450), dtop(20)), "Scratch");
            AddText(new SKPoint(dtop(450), dtop(80)), "Dent");
            AddText(new SKPoint(dtop(450), dtop(150)), "Hole");

        }

        void draw_item(DrawItem drawing_item, SKPaint a_path, SKCanvas canvas)
        {
            switch (drawing_item.drawing_type)
            {
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
                case drawing_types.dt_scratch: canvas.DrawBitmap(scratch_bitmap, drawing_item.at); break;
                case drawing_types.dt_dent: canvas.DrawBitmap(dent_bitmap, drawing_item.at); break;
                case drawing_types.dt_hole: canvas.DrawBitmap(hole_bitmap, drawing_item.at); break;
                case drawing_types.dt_trash: canvas.DrawBitmap(trash_can_bitmap, drawing_item.at); break;
                case drawing_types.dt_van: canvas.DrawBitmap(van_bitmap, drawing_item.at); break;
            }
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

        private float dtop(float dips)
        {
            return dips * screen_ratio;
        }

        private float ptod(float dips)
        {
            return dips / screen_ratio;
        }

        private void LoadValues()
        {

        }

        private void OnTouch(object sender, SKTouchEventArgs e)
        {
            switch (e.ActionType)
            {
                case SKTouchAction.Pressed:
                    mouse_point = e.Location;
                    last_mouse_point = mouse_point;

                    for (int i = latches.Count - 1; i >= 0; i--)
                    {
                        if (mouse_point.X > latches[i].at.X - (hole_bitmap.Width * 0.5f) && mouse_point.X < latches[i].at.X + hole_bitmap.Width + (hole_bitmap.Width * 0.5f) &&
                            mouse_point.Y > latches[i].at.Y - (hole_bitmap.Height * 0.5f) && mouse_point.Y < latches[i].at.Y + hole_bitmap.Height + (hole_bitmap.Height * 0.5f))
                        {
                            selected_item = latches[i].type;
                            latches.RemoveAt(i);
                            break;
                        }
                    }

                    //if (mouse_point.X > dtop(lock_position.X) && mouse_point.X < dtop(lock_position.X) + lock_bitmap.Width &&
                    //   mouse_point.Y > dtop(lock_position.Y) && mouse_point.Y < dtop(lock_position.Y) + lock_bitmap.Height)
                    //    selected_item = drawing_types.dt_lock;
                    if (mouse_point.X > dtop(450) && mouse_point.X < dtop(450) + hole_bitmap.Width &&
                        mouse_point.Y > dtop(20) && mouse_point.Y < dtop(20) + hole_bitmap.Height)
                        selected_item = drawing_types.dt_scratch;
                    else
                    if (mouse_point.X > dtop(450) && mouse_point.X < dtop(450) + hole_bitmap.Width &&
                        mouse_point.Y > dtop(80) && mouse_point.Y < dtop(80) + hole_bitmap.Height)
                        selected_item = drawing_types.dt_dent;
                    else
                    if (mouse_point.X > dtop(450) && mouse_point.X < dtop(450) + hole_bitmap.Width &&
                        mouse_point.Y > dtop(150) && mouse_point.Y < dtop(150) + hole_bitmap.Height)
                        selected_item = drawing_types.dt_hole;
                    //if (mouse_point.X > dtop(220) && mouse_point.X < dtop(220) + lock_db.Width &&
                    //    mouse_point.Y > dtop(390 + y_diff) && mouse_point.Y < dtop(390 + y_diff) + lock_db.Height)
                    //    selected_item = drawing_types.dt_cam;
                    //if (mouse_point.X > dtop(220) && mouse_point.X < dtop(220) + lock_db.Width &&
                    //    mouse_point.Y > dtop(440 + y_diff) && mouse_point.Y < dtop(440 + y_diff) + lock_db.Height)
                    //    selected_item = drawing_types.dt_mushroom;
                    break;

                case SKTouchAction.Moved:
                    last_mouse_point = mouse_point;
                    mouse_point = e.Location;
                    currentPoint += mouse_point - last_mouse_point;

                    //if (selected_item == drawing_types.dt_lock)
                    //{
                    //    lock_position.Y += ptod(mouse_point.Y - last_mouse_point.Y);
                    //}
                    break;

                case SKTouchAction.Released:
                    if (selected_item == drawing_types.dt_scratch ||
                       selected_item == drawing_types.dt_dent ||
                       selected_item == drawing_types.dt_hole )
                    {
                        if (latches.Count < 6 && (mouse_point.X < dtop(450)))
                        {
                            Latch latch = new Latch();
                            latch.type = selected_item;
                            latch.at = new SKPoint(mouse_point.X - (hole_bitmap.Width * 0.5f), mouse_point.Y - (hole_bitmap.Width * 0.5f));
                            latches.Add(latch);
                            latches.Sort((x, y) => x.at.Y.CompareTo(y.at.Y));
                        }
                    }
                    selected_item = drawing_types.dt_none;
                    RedrawBitmap();
                    break;
            }
            e.Handled = true;
            ((SKCanvasView)sender).InvalidateSurface();
        }

        private void Damage_desc_text_Unfocused(object sender, FocusEventArgs e)
        {
            //RedrawBitmap();
        }

        void RedrawBitmap(bool bBin = true)
        {
            SKCanvas canvas = new SKCanvas(background_bitmap);
            //background_bitmap
            canvas.Clear(SKColors.White);
            bBin = false;
            var drawing_paint = new SKPaint
            { // Has some default values, other values get changed depending what we are drawing
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                TextSize = 23
            };

            // draw the items
            foreach (var drawing_item in drawing_items)
            { 
                if(!(bBin == false && drawing_item.drawing_type == drawing_types.dt_trash))
                    draw_item(drawing_item, drawing_paint, canvas);
            }
        }
    }
}