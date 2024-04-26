using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LeadBarPosition : ContentPage
	{
        enum drawing_types { dt_line, dt_text };
        private class DrawItem
        {
            public drawing_types drawing_type;
            public float X, Y;
            public string text = "";
            public SKPath path;
            public SKColor color = SKColors.Black;
        }

        public SKPaint skPaint;
        private List<DrawItem> drawing_items = new List<DrawItem>();

        private SKBitmap background_bitmap = null;

        private bool bLeftRight = true;

        SKPoint mouse_point, last_mouse_point;

        int window_width = 0;
        int window_height = 0;
        float square_width = 0;
        float square_height = 0;

        float screen_window_width = 0.0f;
        float screen_window_height = 0.0f;

        float screen_square_width = 0.0f;
        float screen_square_height = 0.0f;

        float ratio = 0.0f;

        float screen_width = 0.0f;
        float screen_height = 0.0f;

        float screen_sizeA = 0.0f;
        float screen_sizeB = 0.0f;
        float screen_sizeC = 0.0f;
        float screen_sizeD = 0.0f;

        float sizeA = 0.0f;
        float sizeB = 0.0f;
        float sizeC = 0.0f;
        float sizeD = 0.0f;

        SKPoint currentPoint;

        SKRect screen_window_rect;

        public LeadBarPosition()
		{
			InitializeComponent ();
            //BindingContext = App.net.GlassRecord as GlassTable;
            SetLeftRight();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }

        private void InitializeSizes()
        {
            window_width = Convert.ToInt32(App.net.GlassRecord.glass_width);
            window_height = Convert.ToInt32(App.net.GlassRecord.glass_height);
            //square_width = Convert.ToInt32(App.net.GlassRecord.lead_CWidth);
            //square_height = Convert.ToInt32(App.net.GlassRecord.lead_CHeight);
            square_width = App.net.GlassRecord.lead_CWidthf;
            square_height = App.net.GlassRecord.lead_CHeightf;

            ratio = (float)window_width / screen_width;
            if ((float)window_height / screen_height > ratio)
            {
                ratio = (float)window_height / screen_height;

                screen_window_height = screen_height;
                screen_window_width = window_width / ratio;
            }
            else
            {
                screen_window_width = screen_width;
                screen_window_height = (float)window_height / ratio;
            }

            screen_square_width = (float)square_width / ratio;
            screen_square_height = (float)square_height / ratio;

            screen_window_rect.Left = (screen_width / 2.0f) - (screen_window_width / 2.0f) + 3.0f;
            screen_window_rect.Right = (screen_width / 2.0f) + (screen_window_width / 2.0f) - 3.0f;
            screen_window_rect.Top = (screen_height / 2.0f) - (screen_window_height / 2.0f) + 3.0f;
            screen_window_rect.Bottom = (screen_height / 2.0f) + (screen_window_height / 2.0f) - 3.0f;

            if(App.net.GlassRecord.sizeA.Length>0)
                sizeA = App.net.GlassRecord.sizeAf;
            if (App.net.GlassRecord.sizeB.Length > 0)
                sizeB = App.net.GlassRecord.sizeBf;
            if (App.net.GlassRecord.sizeC.Length > 0)
                sizeC = App.net.GlassRecord.sizeCf;
            if (App.net.GlassRecord.sizeD.Length > 0)
                sizeD = App.net.GlassRecord.sizeBf;
            currentPoint.X = App.net.GlassRecord.lead_posX;
            currentPoint.Y = App.net.GlassRecord.lead_posY;
        }

        private void DrawWindowFrame()
        {
            AddLine(new SKPoint(screen_window_rect.Left, screen_window_rect.Top), new SKPoint(screen_window_rect.Left, screen_window_rect.Bottom), SKColors.Black);//Left
            AddLine(new SKPoint(screen_window_rect.Right, screen_window_rect.Top), new SKPoint(screen_window_rect.Right, screen_window_rect.Bottom), SKColors.Black);//Right
            AddLine(new SKPoint(screen_window_rect.Left, screen_window_rect.Top), new SKPoint(screen_window_rect.Right, screen_window_rect.Top), SKColors.Black);//Top
            AddLine(new SKPoint(screen_window_rect.Left, screen_window_rect.Bottom), new SKPoint(screen_window_rect.Right, screen_window_rect.Bottom), SKColors.Black);//Bottom
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
        
        private void AddText(SKPoint at, SKColor color, string text)
        {
            DrawItem text_item = new DrawItem();
            text_item.drawing_type = drawing_types.dt_text;
            text_item.X = at.X;
            text_item.Y = at.Y;
            text_item.color = color;
            text_item.path = null;
            text_item.text = text;
            drawing_items.Add(text_item);
        }
        
        private void DrawLeadorBarLines()
        {
            float fromX, fromY, toX, toY;

            int vertical_num = (window_height / (int)square_height) + 1;
            int horizontal_num = (window_width / (int)square_width) + 1;
            bool bFirst = true;

            for (int i = 0; i < vertical_num; i++)
            {
                fromX = screen_window_rect.Left;
                fromY = screen_window_rect.Top + currentPoint.Y + (i * screen_square_height);
                toX = screen_window_rect.Right;
                toY = screen_window_rect.Top + currentPoint.Y + (i * screen_square_height);

                if (toY < screen_window_rect.Bottom && App.net.GlassRecord.gb_trim==2 ||
                    (toY < screen_window_rect.Bottom - (30.0f / ratio) &&
                    toY > screen_window_rect.Top + (30.0f / ratio)))
                {
                    AddLine(new SKPoint(fromX, fromY), new SKPoint(toX, toY), SKColors.DarkGray);
                    screen_sizeA = (screen_window_rect.Bottom - toY);
                    sizeA = (screen_sizeA * ratio) + ((window_height / 1000.0f) * 5.0f);
                    if (bFirst == true)
                    {
                        bFirst = false;
                        screen_sizeD = toY - screen_window_rect.Top;
                    }
                    sizeD = screen_sizeD * ratio;
                }
            }
            bFirst = true;
            for (int i = 0; i < horizontal_num; i++)
            {
                fromX = screen_window_rect.Left + currentPoint.X + (i * screen_square_width);
                fromY = screen_window_rect.Top;
                toX = screen_window_rect.Left + currentPoint.X + (i * screen_square_width);
                toY = screen_window_rect.Bottom;

                if (toX < screen_window_rect.Right && App.net.GlassRecord.gb_trim == 2 ||
                    (toX < screen_window_rect.Right - (30.0f / ratio) &&
                    toX > screen_window_rect.Left + (30.0f / ratio)))
                {
                    AddLine(new SKPoint(fromX, fromY), new SKPoint(toX, toY), SKColors.DarkGray);

                    if (bFirst == true)
                    {
                        bFirst = false;
                        screen_sizeB = toX - screen_window_rect.Left;
                    }
                    sizeB = screen_sizeB * ratio;
                    screen_sizeC = (screen_window_rect.Right - toX);
                    sizeC = (screen_sizeC * ratio) +((window_width/1000.0f)*5.0f);// +5.0f;
                }
            }

        }

        private void DrawDiamondLines()
        {
            float fromX, fromY, toX, toY;

            int vertical_num = (window_height / (int)square_height) + 1;
            int horizontal_num = (window_width / (int)square_width) + 1;
            int diagonal_num;

            for (int i = 0; i < vertical_num; i++)
            {
                fromX = screen_window_rect.Left;
                fromY = screen_window_rect.Top + currentPoint.Y + (i * screen_square_height);
                toX = screen_window_rect.Right;
                toY = screen_window_rect.Top + currentPoint.Y + (i * screen_square_height);

                if (toY < screen_window_rect.Bottom)
                {
                    screen_sizeA = (screen_window_rect.Bottom - toY);
                    sizeA = (screen_sizeA * ratio) + ((window_height / 1000.0f) * 5.0f);
                    screen_sizeD = currentPoint.Y;
                    sizeD = screen_sizeD * ratio;
                }
            }
            for (int i = 0; i < horizontal_num; i++)
            {
                fromX = screen_window_rect.Left + currentPoint.X + (i * screen_square_width);
                fromY = screen_window_rect.Top;
                toX = screen_window_rect.Left + currentPoint.X + (i * screen_square_width);
                toY = screen_window_rect.Bottom;

                if (toX < screen_window_rect.Right)
                {
                    screen_sizeB = currentPoint.X;
                    sizeB = screen_sizeB * ratio;
                    screen_sizeC = (screen_window_rect.Right - toX);
                    sizeC = (screen_sizeC * ratio) + ((window_height / 1000.0f) * 5.0f);
                }
            }

            diagonal_num = (int)Math.Sqrt((vertical_num * vertical_num) + (horizontal_num * horizontal_num));

            for (int i = -1; i < diagonal_num + 9; i++)
            {
                fromX = screen_window_rect.Left + (screen_sizeB + (i * screen_square_width)) + ((screen_sizeD * screen_square_width) / screen_square_height);
                fromY = screen_window_rect.Top;
                toX = screen_window_rect.Left;
                toY = screen_window_rect.Top + (screen_sizeD + (i * screen_square_height)) + ((screen_sizeB * screen_square_height) / screen_square_width);
                if (toY > screen_window_rect.Bottom)
                {
                    float back_dist = toY - screen_window_rect.Bottom;
                    toX += ((back_dist * screen_square_width) / screen_square_height);
                    toY = screen_window_rect.Bottom;
                }
                if (fromX > screen_window_rect.Right)
                {
                    float back_dist = fromX - screen_window_rect.Right;
                    fromX = screen_window_rect.Right;
                    fromY += ((back_dist * screen_square_height) / screen_square_width);
                }
                if(((int)fromX != (int)toX) && ((int)fromY != (int)toY))
                    AddLine(new SKPoint(fromX, fromY), new SKPoint(toX, toY), SKColors.DarkGray);
            }

            for (int i = -1; i < diagonal_num + 9; i++)
            {
                fromX = (screen_window_rect.Right - (screen_sizeC + (i * screen_square_width))) - ((screen_sizeD * screen_square_width) / screen_square_height);
                fromY = screen_window_rect.Top;
                toX = screen_window_rect.Right;
                toY = screen_window_rect.Top + (screen_sizeD + (i * screen_square_height)) + ((screen_sizeC * screen_square_height) / screen_square_width);
                if (toY > screen_window_rect.Bottom)
                {
                    float back_dist = toY - screen_window_rect.Bottom;
                    toX -= ((back_dist * screen_square_width) / screen_square_height);
                    toY = screen_window_rect.Bottom;
                }

                if (fromX < screen_window_rect.Left)
                {
                    float back_dist = screen_window_rect.Left - fromX;
                    fromX = screen_window_rect.Left;
                    fromY += ((back_dist * screen_square_height) / screen_square_width);
                }
                if (((int)fromX != (int)toX) && ((int)fromY != (int)toY))
                    AddLine(new SKPoint(fromX, fromY), new SKPoint(toX, toY), SKColors.DarkGray);
            }
        }

        private void DrawMeasurements()
        {
            float fromX, fromY, toX, toY;

            fromX = screen_window_rect.Left;
            fromY = screen_window_rect.Bottom - screen_sizeA;
            toX = screen_window_rect.Left + 370.0f;
            toY = screen_window_rect.Bottom - screen_sizeA;
            AddLine(new SKPoint(fromX, fromY), new SKPoint(toX, toY), SKColors.Green);
            AddText(new SKPoint(toX, toY - 10), SKColors.Green, "A");

            fromX = screen_window_rect.Right;
            fromY = screen_window_rect.Top + screen_sizeD;
            toX = screen_window_rect.Right - 370.0f;
            toY = screen_window_rect.Top + screen_sizeD;
            AddLine(new SKPoint(fromX, fromY), new SKPoint(toX, toY), SKColors.Purple);
            AddText(new SKPoint(toX - 80, toY + 90), SKColors.Purple, "D");

            fromX = screen_window_rect.Left + screen_sizeB;
            fromY = screen_window_rect.Bottom;
            toX = screen_window_rect.Left + screen_sizeB;
            toY = screen_window_rect.Bottom - 370.0f;
            AddLine(new SKPoint(fromX, fromY), new SKPoint(toX, toY), SKColors.Red);
            AddText(new SKPoint(toX + 10, toY), SKColors.Red, "B");

            fromX = screen_window_rect.Right - screen_sizeC;
            fromY = screen_window_rect.Top;
            toX = screen_window_rect.Right - screen_sizeC;
            toY = screen_window_rect.Top + 370.0f;
            AddLine(new SKPoint(fromX, fromY), new SKPoint(toX, toY), SKColors.Blue);
            AddText(new SKPoint(toX - 80, toY + 90), SKColors.Blue, "C");

            size_a.Text = String.Format("{0:0.0}", sizeA + 0.5f);
            size_b.Text = String.Format("{0:0.0}", sizeB + 0.5f);
            size_c.Text = String.Format("{0:0.0}", sizeC + 0.5f);
            size_d.Text = String.Format("{0:0.0}", sizeD + 0.5f);
        }

        private void OnTouch(object sender, SkiaSharp.Views.Forms.SKTouchEventArgs e)
        {
            switch (e.ActionType)
            {
                case SKTouchAction.Pressed:
                    mouse_point = e.Location;
                    last_mouse_point = mouse_point;
                    break;

                case SKTouchAction.Moved:
                    last_mouse_point = mouse_point;
                    mouse_point = e.Location;
                    if(bLeftRight==true)
                        currentPoint.X += mouse_point.X * 0.1f - last_mouse_point.X * 0.1f;
                    else
                        currentPoint.Y += mouse_point.Y * 0.1f - last_mouse_point.Y * 0.1f;
                    while (currentPoint.Y > screen_square_height)
                        currentPoint.Y -= screen_square_height;
                    while (currentPoint.Y < 0)
                        currentPoint.Y += screen_square_height;
                    while (currentPoint.X > screen_square_width)
                        currentPoint.X -= screen_square_width;
                    while (currentPoint.X < 0)
                        currentPoint.X += screen_square_width;
                    break;

                case SKTouchAction.Released:

                    break;
            }
            e.Handled = true;
            ((SKCanvasView)sender).InvalidateSurface();
        }

        private void OnPainting(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.White);

            if (background_bitmap == null)
            {
                background_bitmap = new SKBitmap((int)info.Width, (int)info.Height);
                screen_width = (float)info.Width;
                screen_height = (float)info.Height;
                background_bitmap.Erase(SKColor.Parse("ffffff"));
                InitializeSizes();
            }
            if (background_bitmap.Width != (int)info.Width || background_bitmap.Height != (int)info.Height)
            {
                background_bitmap = background_bitmap.Resize(info, SKFilterQuality.High);
                screen_width = (float)info.Width;
                screen_height = (float)info.Height;
                InitializeSizes();
            }

            canvas.DrawBitmap(background_bitmap, 0, 0);

            var touchPathStroke = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                TextSize = 23
            };

            drawing_items.Clear();
            DrawWindowFrame();
            switch (App.net.GlassRecord.special_glass)
            {
                case "Diamond Leaded": DrawDiamondLines(); break;
                case "Georgian Leaded": 
                case "Back to Back Spacer": 
                case "Georgian Bar": DrawLeadorBarLines(); break;
            }            
            DrawMeasurements();

            for (int i=0; i< drawing_items.Count;i++)
                draw_item(drawing_items[i], touchPathStroke, canvas);
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
                    a_path.Color = drawing_item.color;
                    a_path.Style = SKPaintStyle.Fill;
                    a_path.TextSize = 80.0f;
                    canvas.DrawText(drawing_item.text, drawing_item.X, drawing_item.Y, a_path);
                    break;
            }
        }

        private void Save()             
        {
            App.net.GlassRecord.sizeA = String.Format("{0:0.0}", sizeA + 0.5f);
            App.net.GlassRecord.sizeB = String.Format("{0:0.0}", sizeB + 0.5f);
            App.net.GlassRecord.sizeC = String.Format("{0:0.0}", sizeC + 0.5f);
            App.net.GlassRecord.sizeD = String.Format("{0:0.0}", sizeD + 0.5f);

            App.net.GlassRecord.sizeAf = sizeA;
            App.net.GlassRecord.sizeBf = sizeB;
            App.net.GlassRecord.sizeCf = sizeC;
            App.net.GlassRecord.sizeDf = sizeD;

            App.net.GlassRecord.lead_posX = currentPoint.X;
            App.net.GlassRecord.lead_posY = currentPoint.Y;

            switch (App.net.GlassRecord.special_glass)
            {
                case "Diamond Leaded": App.net.GlassRecord.lead_bDiamondComplete = true; break;
                case "Georgian Leaded": App.net.GlassRecord.lead_bGeorgianComplete = true; break;
                case "Back to Back Spacer": App.net.GlassRecord.lead_bGeorgianComplete = true; break;
                case "Georgian Bar": App.net.GlassRecord.lead_bBarComplete = true; break;
            }

            //redraw_bitmap();

            //save lead image
            string fname = string.Format("Drawings/{0:00000000}_dLI", App.net.HeaderRecord.udi_cont);
            //string fname = string.Format("Drawings/{0:00000000}_dAU", App.net.HeaderRecord.udi_cont);
            fname = fname + string.Format("{0:000}00.jpg", App.net.root_item_number);

            //fname = string.Format("Drawings/{0:00000000}_dAU", App.net.HeaderRecord.udi_cont);
            //fname = fname + string.Format("{0:000}", App.net.root_item_number);
            //fname = fname + string.Format("{0:00}.jpg", App.net.vid_image_num);
            // create an image and then get the PNG (or any other) encoded data
            //SkCanvasView.InvalidateSurface();
            DrawBitmap();

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

        private void Button_Clicked(object sender, EventArgs e)
        {
            //Save();
            if (bLeftRight == true)
                bLeftRight = false;
            else
                bLeftRight = true;

            SetLeftRight();
            //Navigation.PopAsync(false);
        }

        private void SetLeftRight()
        {
            if (bLeftRight == true)
                left_right.ImageSource = "left_right.png";
            else
                left_right.ImageSource = "up_down.png";
        }

        protected override bool OnBackButtonPressed()
        {
            Save();
        
            base.OnBackButtonPressed();
            return false;
        }

    }
}