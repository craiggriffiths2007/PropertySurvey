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
    public partial class AddMinusLabel : Xamarin.Forms.StackLayout
    {
        bool bMultipler = false;
        private int min = 0;

        public delegate void OnSelectionHasChanged(object sender, EventArgs e);
        public event OnSelectionHasChanged OnSelectionChanged;

        public static readonly BindableProperty ValProperty = BindableProperty.Create("Val", typeof(int), typeof(AddMinusLabel), default(int), BindingMode.TwoWay);
        public static readonly BindableProperty MaxProperty = BindableProperty.Create("Max", typeof(int), typeof(AddMinusLabel), default(int), BindingMode.OneWay);

        public AddMinusLabel()
        {
            InitializeComponent();
        }
        public bool Multiplier
        {
            get { return bMultipler; }
            set { bMultipler = value; }
        }
        public int Min
        {
            get { return min; }
            set { min = value; }
        }
        public int Max
        {
            get { return (int)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }
        public int Val
        {
            get { return (int)GetValue(ValProperty); }
            set { SetValue(ValProperty, value); SetValueText(); }
        }
        public string LabelText
        {
            get { return TheLabel.Text; }
            set { TheLabel.Text = value; }
        }
        public void SetValueText()
        {
            if (Val < min)
                Val = min;
            if (Val > Max && Max >= min) // Second can fail on loading, plus if Max comes from a binding and contains a stupid value,
                Val = Max;               // then this generates and endless loop until android dies with stack overflow.
            if (bMultipler == true)
            {
                if (Val > 0)
                    TheValue.Text = Val.ToString() + "x";
                else
                    TheValue.Text = "0";
            }
            else
                TheValue.Text = Val.ToString() + "/" + Max.ToString();
        }
        private void OnMinusButton(object sender, EventArgs e)
        {
            Val--;
            SetValueText();
            OnSelectionChanged?.Invoke(this, new EventArgs());
        }
        private void OnAddButton(object sender, EventArgs e)
        {
            Val++;
            SetValueText();
            OnSelectionChanged?.Invoke(this, new EventArgs());
        }
        private void layout_changed(object sender, EventArgs e)
        {
            SetValueText();
        }

        private void OnChanged(object sender, EventArgs e)
        {

        }
    }
}