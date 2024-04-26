using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BifoldDoorType : ContentPage
	{
        string value1 = "", value2 = "", value3 = "", value4 = "", value5 = "", value6 = "", value7 = "", value8 = "";

        public BifoldDoorType ()
		{
			InitializeComponent ();
            BindingContext = App.net.BifoldRecord as BifoldTable;

            if (App.net.BifoldRecord.door_type == "KAT PVCu Bifold Doors")
            {
                switch (App.net.BifoldRecord.number_of_doors)
                {
                    case 2:
                        value1 = "202";
                        value2 = "202b";
                        label1.Text = "2.0.2";
                        button1.ImageSource = "kat_202.png";
                        label2.Text = "2.0.2b";
                        button2.ImageSource = "kat_202b.png";
                        break;
                    case 3:
                        value1 = "321";
                        value2 = "330";
                        value3 = "321b";
                        value4 = "330b";
                        label1.Text = "3.2.1";
                        button1.ImageSource = "kat_321.png";
                        label2.Text = "3.3.0";
                        button2.ImageSource = "kat_330.png";
                        label3.Text = "3.2.1b";
                        button3.ImageSource = "kat_321b.png";
                        label4.Text = "3.3.0b";
                        button4.ImageSource = "kat_330b.png";
                        break;
                    case 4:
                        value1 = "413";
                        value2 = "422";
                        value3 = "440";
                        value4 = "413b";
                        value5 = "422b";
                        value6 = "440b";
                        label1.Text = "4.1.3";
                        button1.ImageSource = "kat_413.png";
                        label2.Text = "4.2.2";
                        button2.ImageSource = "kat_422.png";
                        label3.Text = "4.4.0";
                        button3.ImageSource = "kat_440.png";
                        label4.Text = "4.1.3b";
                        button4.ImageSource = "kat_413b.png";
                        label5.Text = "4.2.2b";
                        button5.ImageSource = "kat_422b.png";
                        label6.Text = "4.4.0b";
                        button6.ImageSource = "kat_440b.png";
                        break;
                    case 5:
                        value1 = "532";
                        value2 = "541";
                        value3 = "550";
                        value4 = "532b";
                        value5 = "541b";
                        value6 = "550b";
                        label1.Text = "5.3.2";
                        button1.ImageSource = "kat_532.png";
                        label2.Text = "5.4.1";
                        button2.ImageSource = "kat_541.png";
                        label3.Text = "5.5.0";
                        button3.ImageSource = "kat_550.png";
                        label4.Text = "5.3.2b";
                        button4.ImageSource = "kat_532b.png";
                        label5.Text = "5.4.1b";
                        button5.ImageSource = "kat_541b.png";
                        label6.Text = "5.5.0b";
                        button6.ImageSource = "kat_550b.png";
                        break;
                    case 6:
                        value1 = "615";
                        value2 = "624";
                        value3 = "633";
                        value4 = "660";
                        value5 = "615b";
                        value6 = "624b";
                        value7 = "633b";
                        value8 = "660b";
                        label1.Text = "6.1.5";
                        button1.ImageSource = "kat_615.png";
                        label2.Text = "6.2.4";
                        button2.ImageSource = "kat_624.png";
                        label3.Text = "6.3.3";
                        button3.ImageSource = "kat_633.png";
                        label4.Text = "6.6.0";
                        button4.ImageSource = "kat_660.png";
                        label5.Text = "6.1.5b";
                        button5.ImageSource = "kat_615b.png";
                        label6.Text = "6.2.4b";
                        button6.ImageSource = "kat_624b.png";
                        label7.Text = "6.3.3b";
                        button7.ImageSource = "kat_633b.png";
                        label8.Text = "6.6.0b";
                        button8.ImageSource = "kat_660b.png";
                        break;
                    case 7:
                        value1 = "770";
                        value2 = "770b";
                        label1.Text = "7.7.0";
                        button1.ImageSource = "kat_770.png";
                        label2.Text = "7.7.0b";
                        button2.ImageSource = "kat_770b.png";
                        break;
                }
            }
            else
            {
                switch (App.net.BifoldRecord.number_of_doors)
                {
                    case 1:
                        value1 = "1a";
                        value2 = "1b";
                        label1.Text = "Traffic Door Right to Left";
                        button1.ImageSource = "bf1a.jpg";
                        label2.Text = "Traffic Door Left to Right";
                        button2.ImageSource = "bf1b.jpg";
                        break;
                    case 2:
                        value1 = "2a";
                        value2 = "2b";
                        value3 = "2c";
                        label1.Text = "Section Left to Right";
                        button1.ImageSource = "bf2a.jpg";
                        label2.Text = "Section Right to Left";
                        button2.ImageSource = "bf2b.jpg";
                        label3.Text = "Section Centre Opening";
                        button3.ImageSource = "bf2c.jpg";
                        break;
                    case 3:
                        value1 = "3a";
                        value2 = "3a2";
                        value3 = "3b";
                        value4 = "3b2";
                        label1.Text = "Section Left to Right";
                        button1.ImageSource = "bf3a.jpg";
                        label2.Text = "Section Left to Right";
                        button2.ImageSource = "bf3a2.jpg";
                        label3.Text = "Section Right to Left";
                        button3.ImageSource = "bf3b.jpg";
                        label4.Text = "Section Right to Left";
                        button4.ImageSource = "bf3b2.jpg";
                        break;
                    case 4:
                        value1 = "4a";
                        value2 = "4b";
                        value3 = "4c";
                        value4 = "4d";
                        label1.Text = "Section Left to Right";
                        button1.ImageSource = "bf4a.jpg";
                        label2.Text = "Section Right to Left";
                        button2.ImageSource = "bf4b.jpg";
                        label3.Text = "Section (1 to Left, 3 to Right)";
                        button3.ImageSource = "bf4c.jpg";
                        label4.Text = "Section (1 to Right, 3 to Left)";
                        button4.ImageSource = "bf4d.jpg";
                        break;
                    case 5:
                        value1 = "5a";
                        value2 = "5a2";
                        value3 = "5b";
                        value4 = "5b2";
                        value5 = "5c";
                        value6 = "5d";
                        label1.Text = "Section Left to Right";
                        button1.ImageSource = "bf5a.jpg";
                        label2.Text = "Section Left to Right";
                        button2.ImageSource = "bf5a2.jpg";
                        label3.Text = "Section Right to Left";
                        button3.ImageSource = "bf5b.jpg";
                        label4.Text = "Section Right to Left";
                        button4.ImageSource = "bf5b2.jpg";
                        label5.Text = "Section (2 to Left, 3 to Right)";
                        button5.ImageSource = "bf5c.jpg";
                        label6.Text = "Section (3 to Left, 2 to Right)";
                        button6.ImageSource = "bf5d.jpg";
                        break;
                    case 6:
                        value1 = "6a";
                        value2 = "6b";
                        value3 = "6c";
                        value4 = "6d";
                        value5 = "6e";
                        label1.Text = "Section Left to Right";
                        button1.ImageSource = "bf6a.jpg";
                        label2.Text = "Section Right to Left";
                        button2.ImageSource = "bf6b.jpg";
                        label3.Text = "Section (1 to Left, 5 to Right)";
                        button3.ImageSource = "bf6c.jpg";
                        label4.Text = "Section (5 to Left, 1 to Right)";
                        button4.ImageSource = "bf6d.jpg";
                        label5.Text = "Section (3 to Left, 3 to Right)";
                        button5.ImageSource = "bf6e.jpg";
                        break;
                    case 7:
                        value1 = "7a";
                        value2 = "7a2";
                        value3 = "7b";
                        value4 = "7b2";
                        value5 = "7c";
                        value6 = "7d";
                        value7 = "7e";
                        value8 = "7f";
                        label1.Text = "Section Left to Right";
                        button1.ImageSource = "bf7a.jpg";
                        label2.Text = "Section Left to Right";
                        button2.ImageSource = "bf7a2.jpg";
                        label3.Text = "Section Right to Left";
                        button3.ImageSource = "bf7b.jpg";
                        label4.Text = "Section Right to Left";
                        button4.ImageSource = "bf7b2.jpg";
                        label5.Text = "Section (2 to Left, 5 to Right)";
                        button5.ImageSource = "bf7c.jpg";
                        label6.Text = "Section (5 to Left, 2 to Right)";
                        button6.ImageSource = "bf7d.jpg";
                        label7.Text = "Section (3 to Left, 4 to Right)";
                        button7.ImageSource = "bf7e.jpg";
                        label8.Text = "Section (4 to Left, 3 to Right)";
                        button8.ImageSource = "bf7f.jpg";
                        break;
                }
            }

            if (label1.Text == "")
            {
                label1.IsVisible = false;
                button1.IsVisible = false;
            }
            if (label2.Text == "")
            {
                label2.IsVisible = false;
                button2.IsVisible = false;
            }
            if (label2.Text == "")
            {
                label2.IsVisible = false;
                button2.IsVisible = false;
            }
            if (label3.Text == "")
            {
                label3.IsVisible = false;
                button3.IsVisible = false;
            }
            if (label4.Text == "")
            {
                label4.IsVisible = false;
                button4.IsVisible = false;
            }
            if (label5.Text == "")
            {
                label5.IsVisible = false;
                button5.IsVisible = false;
            }
            if (label6.Text == "")
            {
                label6.IsVisible = false;
                button6.IsVisible = false;
            }
            if (label7.Text == "")
            {
                label7.IsVisible = false;
                button7.IsVisible = false;
            }
            if (label8.Text == "")
            {
                label8.IsVisible = false;
                button8.IsVisible = false;
            }
        }

        private void button1clicked(object sender, EventArgs e)
        {
            App.net.BifoldRecord.number_of_doors_text = value1;
            Navigation.PopAsync(false);
        }

        private void button2clicked(object sender, EventArgs e)
        {
            App.net.BifoldRecord.number_of_doors_text = value2;
            Navigation.PopAsync(false);
        }

        private void button3clicked(object sender, EventArgs e)
        {
            App.net.BifoldRecord.number_of_doors_text = value3;
            Navigation.PopAsync(false);
        }

        private void button4clicked(object sender, EventArgs e)
        {
            App.net.BifoldRecord.number_of_doors_text = value4;
            Navigation.PopAsync(false);
        }

        private void button5clicked(object sender, EventArgs e)
        {
            App.net.BifoldRecord.number_of_doors_text = value5;
            Navigation.PopAsync(false);
        }

        private void button6clicked(object sender, EventArgs e)
        {
            App.net.BifoldRecord.number_of_doors_text = value6;
            Navigation.PopAsync(false);
        }

        private void button7clicked(object sender, EventArgs e)
        {
            App.net.BifoldRecord.number_of_doors_text = value7;
            Navigation.PopAsync(false);
        }

        private void button8clicked(object sender, EventArgs e)
        {
            App.net.BifoldRecord.number_of_doors_text = value8;
            Navigation.PopAsync(false);
        }
    }
}