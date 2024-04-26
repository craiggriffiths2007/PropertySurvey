using System;
using Xamarin.Forms;
using System.Collections.Generic;
using Xamarin.Forms.Xaml;

namespace MartControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewButtonLabel : StackLayout
    {
        public ViewButtonLabel()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty ButtonStateProperty = BindableProperty.Create("ButtonState", typeof(int), typeof(YesNo), default(int), BindingMode.TwoWay);
        public string LabelText { set { the_label.Text = value; } }
        private List<string> button_list = YesNo_button_default_list.button_list;

        private int ButtonState
        {
            get { return (int)GetValue(ButtonStateProperty); }
        }

        public string button_binding
        {
            set { this.SetBinding(ButtonStateProperty, value); }
        }

        public void set_button_list(List<String> items)
        {
            button_list = items;
        }

        private void layout_changed(object sender, EventArgs e)
        {
            if (ButtonState > 0 && ButtonState < button_list.Count)
                the_text.Text = button_list[ButtonState];
            else
                the_text.Text = "Unknown";
        }
    }
}