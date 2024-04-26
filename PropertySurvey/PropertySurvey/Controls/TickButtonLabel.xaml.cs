using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MartControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TickButtonLabel : Xamarin.Forms.StackLayout
    {
        public delegate void OnSelectionHasChanged(object sender, EventArgs e);
        public event OnSelectionHasChanged Clicked;

        public static readonly BindableProperty ButtonStateProperty = BindableProperty.Create("ButtonState", typeof(bool), typeof(TickButtonLabel), default(bool), BindingMode.TwoWay);

        private bool bAutoSwitch = true;

        public bool AutoSwitch
        {
            get { return bAutoSwitch; }
            set { bAutoSwitch = value; }
        }
        public TickButtonLabel()
        {
            InitializeComponent();
        }
        public bool ButtonState
        {
            get { return (bool)GetValue(ButtonStateProperty); }
            set { SetValue(ButtonStateProperty, value); }
        }
        private void OnButton(object sender, EventArgs e)
        {
            if (bAutoSwitch == true)
            {
                if (ButtonState == true)
                {
                    ButtonState = false;
                }
                else
                {
                    ButtonState = true;
                }
                SetButtonImage();
            }
            Clicked?.Invoke(this, new EventArgs());
        }
        private void layout_changed(object sender, EventArgs e)
        {
            SetButtonImage();
        }
        private void SetButtonImage()
        {
            if (ButtonState == true)
            {
                TheButton.ImageSource = "green_tick.png";
            }
            else
            {
                TheButton.ImageSource = "";
            }
        }
        public string LabelText
        {
            get { return TheLabel.Text; }
            set { TheLabel.Text = value; }
        }
        public int ButtonWidth
        {
            get { return (int)TheButton.WidthRequest; }
            set { TheButton.WidthRequest = value; }
        }
    }
}