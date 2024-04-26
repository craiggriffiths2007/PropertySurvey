using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MartControls
{
    public partial class YesNoQuestionLabel : Xamarin.Forms.StackLayout
    {
        public delegate void OnSelectionHasChanged(object sender, EventArgs e);
        public event OnSelectionHasChanged OnSelectionChanged;

        public event OnSelectionHasChanged OnQuestionButton;

        public List<string> button_list = new List<string>() { "...", "Yes", "No" };

        public int ButtonWidth
        {
            get { return (int)TheButton.WidthRequest; }
            set { TheButton.WidthRequest = value; }
        }

        public string LabelText
        {
            get { return TheLabel.Text; }
            set { TheLabel.Text = value; }
        }

        public static readonly BindableProperty ButtonStateProperty = BindableProperty.Create("ButtonState", typeof(int), typeof(YesNo), default(int), BindingMode.TwoWay);

        public int ButtonState
        {
            get { return (int)GetValue(ButtonStateProperty); }
            set { SetValue(ButtonStateProperty, value); }
        }

        public static readonly BindableProperty bCompleteProperty = BindableProperty.Create("bComplete", typeof(bool), typeof(YesNo), default(bool), BindingMode.TwoWay);

        public bool bComplete
        {
            get { return (bool)GetValue(bCompleteProperty); }
            set { SetValue(bCompleteProperty, value); }
        }


        public YesNoQuestionLabel()
        {
            InitializeComponent();
        }

        public void set_button_list(List<String> items)
        {
            button_list = items;
        }

        private void SetButton()
        {
            TheButton.Text = button_list[ButtonState];

            switch(ButtonState)
            {
                case 0: QuestionButton.ImageSource = "na.png";break;
                case 1: if (bComplete == true) { QuestionButton.ImageSource = "green_tick.png"; } else { QuestionButton.ImageSource = "question.png"; }; break;
                case 2: QuestionButton.ImageSource = "na.png"; break;
            }
        }

        private void OnButton(object sender, EventArgs e)
        {
            ButtonState++;
            if (ButtonState >= button_list.Count)
                ButtonState = 1;

            SetButton();
            OnSelectionChanged?.Invoke(this, new EventArgs());
        }


        private void layout_changed(object sender, EventArgs e)
        {
            // This is needed when we go back into an existing item.
            // It causes the button to display the value set when we previously created/edited the item.
            SetButton();
        }

        private void OnQuestionButtonPress(object sender, EventArgs e)
        {
            OnQuestionButton?.Invoke(this, new EventArgs());
        }
    }
}