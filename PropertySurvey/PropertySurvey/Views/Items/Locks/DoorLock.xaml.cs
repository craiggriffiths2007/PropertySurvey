using System;
using System.Linq;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
 	public partial class DoorLock : DoorLockShared
    {
        drawing_types selected_item = drawing_types.dt_none;
        SKPoint mouse_point, last_mouse_point;
        SKPoint currentPoint;
        int current_enter = 0;
        SKBitmap background_bitmap = null;

        public DoorLock ()
		{
			InitializeComponent ();

            background_bitmap = new SKBitmap();
        }

        protected override bool OnBackButtonPressed()
        {
            SaveValues();

            base.OnBackButtonPressed();
            return false;
        }

        private void SaveValues()
        {
            GetEnterValue();
            if (App.net.CurrentItem == "lock")
            {
                App.net.LockingRecord.bLockComplete = true;
                App.net.LockingRecord.bDoorComplete = true;
                App.net.LockingRecord.l_size1 = size1;
                App.net.LockingRecord.l_size2 = size2;

                App.net.LockingRecord.l_sizeA = enter_values[0];
                App.net.LockingRecord.l_sizeB = enter_values[1];
                App.net.LockingRecord.l_sizeC = enter_values[2];
                App.net.LockingRecord.l_sizeD = enter_values[3];
                App.net.LockingRecord.l_sizeE = enter_values[4];
                App.net.LockingRecord.l_sizeF = enter_values[5];
                App.net.LockingRecord.l_sizeG = enter_values[6];

                App.net.LockingRecord.l_num = latches.Count();
                int i = 0;
                foreach (var latch in latches)
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
            if (App.net.CurrentItem == "timber")
            {
                App.net.TimberRecord.bLockComplete = true;
                App.net.TimberRecord.bDoorComplete = true;
                App.net.TimberRecord.l_size1 = size1;
                App.net.TimberRecord.l_size2 = size2;

                App.net.TimberRecord.l_sizeA = enter_values[0];
                App.net.TimberRecord.l_sizeB = enter_values[1];
                App.net.TimberRecord.l_sizeC = enter_values[2];
                App.net.TimberRecord.l_sizeD = enter_values[3];
                App.net.TimberRecord.l_sizeE = enter_values[4];
                App.net.TimberRecord.l_sizeF = enter_values[5];
                App.net.TimberRecord.l_sizeG = enter_values[6];

                App.net.TimberRecord.l_num = latches.Count();
                int i = 0;
                foreach(var latch in latches)
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
            if (App.net.CurrentItem == "alum")
            {
                App.net.AlumRecord.bLockComplete = 1;
                App.net.AlumRecord.bDoorComplete = true;
                App.net.AlumRecord.l_size1 = size1;
                App.net.AlumRecord.l_size2 = size2;

                App.net.AlumRecord.l_sizeA = enter_values[0];
                App.net.AlumRecord.l_sizeB = enter_values[1];
                App.net.AlumRecord.l_sizeC = enter_values[2];
                App.net.AlumRecord.l_sizeD = enter_values[3];
                App.net.AlumRecord.l_sizeE = enter_values[4];
                App.net.AlumRecord.l_sizeF = enter_values[5];
                App.net.AlumRecord.l_sizeG = enter_values[6];

                App.net.AlumRecord.l_num = latches.Count();
                int i = 0;
                foreach (var latch in latches)
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
            if (App.net.CurrentItem == "upvc")
            {
                App.net.UPVCRecord.bLockComplete = true;
                App.net.UPVCRecord.bDoorComplete = true;
                App.net.UPVCRecord.l_size1 = size1;
                App.net.UPVCRecord.l_size2 = size2;

                App.net.UPVCRecord.l_sizeA = enter_values[0];
                App.net.UPVCRecord.l_sizeB = enter_values[1];
                App.net.UPVCRecord.l_sizeC = enter_values[2];
                App.net.UPVCRecord.l_sizeD = enter_values[3];
                App.net.UPVCRecord.l_sizeE = enter_values[4];
                App.net.UPVCRecord.l_sizeF = enter_values[5];
                App.net.UPVCRecord.l_sizeG = enter_values[6];

                App.net.UPVCRecord.l_num = latches.Count();
                int i = 0;
                foreach (var latch in latches)
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

        private void SetEnterValues()
        {
            if (current_enter == 0)
            {
                distance_text.Text = "Distance 1";
                value_entry.Text= size1;
            }
            else
            {
                if (current_enter == 1)
                {
                    distance_text.Text = "Distance 2";
                    value_entry.Text = size2;
                }
                else
                {
                    distance_text.Text = "Distance " + GetLetter(current_enter - 2);
                    value_entry.Text = enter_values[current_enter - 2];
                }
            }
        }

        private void GetEnterValue()
        {
            if (current_enter == 0)
                size1 = value_entry.Text;
            else
            if (current_enter == 1)
                        size2 = value_entry.Text;
                    else
                        enter_values[current_enter - 2] = value_entry.Text;
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
            if (current_enter > latches.Count)
                current_enter = latches.Count;
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
                resize_bitmaps();

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

            AddLine(new SKPoint(dtop(80), dtop(20)), new SKPoint(dtop(80), dtop(500)), SKColors.Black);//Left

            AddBitmap(new SKPoint(dtop(lock_position.X),dtop(lock_position.Y)), drawing_types.dt_lock);

            if(selected_item == drawing_types.dt_deadlock ||
               selected_item == drawing_types.dt_hook ||
               selected_item == drawing_types.dt_latch ||
               selected_item == drawing_types.dt_cam ||
               selected_item == drawing_types.dt_mushroom)
            {
                AddBitmap(new SKPoint(mouse_point.X - (lock_db.Height * 0.5f), mouse_point.Y - (lock_db.Height * 0.5f)), selected_item);
            }

            draw_latches();

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

        private float ptod(float dips)
        {
            return dips / screen_ratio;
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
                        if (mouse_point.X > latches[i].at.X - (lock_db.Width * 0.5f) && mouse_point.X < latches[i].at.X + lock_db.Width + (lock_db.Width *0.5f) &&
                            mouse_point.Y > latches[i].at.Y - (lock_db.Height * 0.5f) && mouse_point.Y < latches[i].at.Y + lock_db.Height + (lock_db.Height * 0.5f))
                        {
                            selected_item = latches[i].type;
                            latches.RemoveAt(i);
                        }
                    }

                    if (mouse_point.X > dtop(lock_position.X) && mouse_point.X < dtop(lock_position.X) + lock_bitmap.Width &&
                       mouse_point.Y > dtop(lock_position.Y) && mouse_point.Y < dtop(lock_position.Y) + lock_bitmap.Height)
                        selected_item = drawing_types.dt_lock;
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
                        if(latches.Count < 6 && (mouse_point.X< dtop(80)))
                        {
                            Latch latch = new Latch();
                            latch.type = selected_item;
                            latch.at = new SKPoint(dtop(80) - lock_db.Width, mouse_point.Y - (lock_db.Width * 0.5f));
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