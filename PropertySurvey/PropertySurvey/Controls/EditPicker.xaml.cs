using System;
using System.Collections.Generic;
using Xamarin.Forms;
using PropertySurvey;


namespace MartControls
{
    public partial class EditPicker : Xamarin.Forms.StackLayout
    {
        public delegate void OnTextChanged(object sender, EventArgs e);
        public event OnTextChanged OnTextChangedEvent;
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(EditPicker), default(string), BindingMode.TwoWay);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public string TextBinding { set { enter.SetBinding(Entry.TextProperty, value); } }
        public string picker_title { set { pick.Title = value; } }
        public int max_text_length { set { enter.MaxLength = value; } }

        public void SetPickerItems(List<String> items)
        {
            //if(items!=null)
            //    items.Sort();
            pick.ItemsSource = items;

            enter.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeSentence);
        }

        // Use this when you want to make the picker bit disabled, but still allow the user to type something
        public void set_picker_enabled (bool enable)
        {
            if (enable)
            {
                the_button.ImageSource = "dropdown.png";
                the_button.IsEnabled = true;
            }
            else
            {
                the_button.ImageSource = null;
                the_button.IsEnabled = false;
            }
        }

        public EditPicker()
        {
            InitializeComponent();

            the_button.ImageSource = "dropdown.png";

            enter.Text = Text;
            enter.TextChanged += OnTextChangedInternal;

            //enter.Keyboard = Keyboard.Create(KeyboardFlags.CapitalizeSentence);
        }

        

        private void OnTextChangedInternal(object sender, TextChangedEventArgs e)
        {
            Text = e.NewTextValue;
            
            // check if a handler is assigned
            OnTextChangedEvent?.Invoke(this, new EventArgs());
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TextProperty.PropertyName)
            {
                Device.BeginInvokeOnMainThread(ChangeLogo);
            }
        }

        private void ChangeLogo()
        {
            enter.Text = Text;

            if (enter.Text != null && enter.Text.Length > 0)
                the_button.ImageSource = "grey_dropdown.png";
            else
                the_button.ImageSource = "dropdown.png";
        }

        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pick.SelectedItem != null)
                enter.Text = pick.SelectedItem.ToString();
            else // Else occurs when we a) have a selected item and subsequently b) change the list
                enter.Text = "";
        }

        private void OnButton(object sender, EventArgs e)
        {
            //pick.IsEnabled = true;
            pick.Focus();

        }

        public string validation_error_string(string error_text)
        {
            if (this.IsVisible && (enter.Text == null || enter.Text.Length == 0))
                return error_text;
            else
                return "";
        }
    }
}