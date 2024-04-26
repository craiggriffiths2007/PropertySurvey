using System;
using Xamarin.Forms;
using System.Collections.Generic;
using Xamarin.Forms.Xaml;

namespace MartControls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewSubItem : StackLayout
	{
        public delegate void OnViewClicked(object sender, EventArgs e);
        public event OnViewClicked OnViewClickedEvent;
        public static readonly BindableProperty ButtonStateProperty = BindableProperty.Create("ButtonState", typeof(int), typeof(YesNo), default(int), BindingMode.TwoWay);
        public string LabelText { set { the_label.Text = value; } }
        private List<string> button_list = YesNo_button_default_list.button_list;

        public ViewSubItem ()
		{
			InitializeComponent ();
		}

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

            view_button.IsVisible = ButtonState == 1;
        }

        private void view_clicked(object sender, EventArgs e)
        {
            OnViewClickedEvent?.Invoke(this, new EventArgs());
        }
    }
}