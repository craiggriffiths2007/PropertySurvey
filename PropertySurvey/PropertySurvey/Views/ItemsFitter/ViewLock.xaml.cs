using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;

namespace PropertySurvey
{
    public enum view_lock_type { vlt_door, vlt_window }

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewLock : DoorLockShared
	{
        view_lock_type lock_type;
        int num_distances;

        public ViewLock (view_lock_type _lock_type)
		{
			InitializeComponent ();
            lock_type = _lock_type;
            if (lock_type == view_lock_type.vlt_door)
            {
                distance2_label.IsVisible = true;
                distance_2_answer.IsVisible = true;
            }
        }

        private void OnPainting(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.White);
            screen_ratio = (float)info.Width / 360.0f;
            if (!locks_scaled)
            {
                resize_bitmaps();
                LoadValues();

                if (lock_type == view_lock_type.vlt_door)
                    num_distances = latches.Count - 1;
                else
                    num_distances = latches.Count + 1;

                DrawLock();
                draw_values();
                locks_scaled = true;
            }

            //((SKCanvasView)SkCanvasView).InvalidateSurface();

            var touchPathStroke = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                TextSize = 23
            };

            for (int i = 0; i < drawing_items.Count; i++)
                draw_item(drawing_items[i], touchPathStroke, canvas);
        }

        private void DrawLock()
        {
            drawing_items.Clear();

            AddLine(new SKPoint(dtop(80), dtop(20)), new SKPoint(dtop(80), dtop(500)), SKColors.Black);//Left
            AddBitmap(new SKPoint(dtop(lock_position.X), dtop(lock_position.Y)), drawing_types.dt_lock);

            draw_latches();
        }

        private void set_one_distance(Label distance_label, Label distance_answer, int index)
        {
            if (index < num_distances)
            {
                distance_label.IsVisible = true;
                distance_answer.IsVisible = true;
                distance_answer.Text = enter_values[index];
            }
        }

        private void draw_values()
        {
            distance_1_answer.Text = size1.ToString();
            distance_2_answer.Text = size2.ToString();
            set_one_distance(distance_A_label, distance_A_answer, 0);
            set_one_distance(distance_B_label, distance_B_answer, 1);
            set_one_distance(distance_C_label, distance_C_answer, 2);
            set_one_distance(distance_D_label, distance_D_answer, 3);
            set_one_distance(distance_E_label, distance_E_answer, 4);
            set_one_distance(distance_F_label, distance_F_answer, 5);
            set_one_distance(distance_G_label, distance_G_answer, 6);
        }
    }
}