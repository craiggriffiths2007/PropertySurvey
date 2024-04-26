using PropertySurvey;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MartControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WidthHeight : StackLayout
	{
        public delegate void OnSelectionHasChanged(object sender, EventArgs e);
        public event OnSelectionHasChanged OnSelectionChanged;

        public WidthHeight ()
		{
			InitializeComponent ();
        }

        // A few controls work in pre-set sizes in feet
        // Disable the diagonal warning for these
        t_units units_to_use = t_units.units_mm;

        public string width_label_text  { set { width_label.Text = value; } }
        public string height_label_text { set { height_label.Text = value; } }
        public string width_binding     { set { width_entry.SetBinding(Entry.TextProperty, value); } }
        public string height_binding    { set { height_entry.SetBinding(Entry.TextProperty, value); } }
        public string width_text        { set { width_entry.Text = value;  } }
        public string height_text       { set { height_entry.Text = value; } }

        public t_units units
        {
            set
            {
                units_to_use = value;
                switch (units_to_use)
                {
                    case t_units.units_feet:
                        width_units.Text = "ft";
                        height_units.Text = "ft";
                        width_height_warning.IsVisible = false;
                        break;
                    case t_units.units_inches:
                        width_units.Text = "in";
                        height_units.Text = "in";
                        width_height_warning.IsVisible = false;
                        break;
                    default:
                        width_units.Text = "mm";
                        height_units.Text = "mm";
                        if (width_entry.Text != null && height_entry.Text != null) // Prevent exception when loading
                            update_width_height_warning();
                        break;
                }
            }
        }

        public void update_width_height_warning()
        {
            DiagonalCheck(width_entry.Text, height_entry.Text);
        }

        public void DiagonalCheck(string width, string height)
        {
            if (width.Length > 0 && height.Length > 0 && width.IndexOf(".") < 0 && height.IndexOf(".") < 0)
            {
                double fw = Convert.ToDouble(width);
                double fh = Convert.ToDouble(height);
                double zdist = Math.Sqrt((fw * fw) + (fh * fh));

                zdist = Math.Round(zdist, 0);

                width_height_warning.IsVisible = true;
                width_height_warning.Text = "Diagonal distance should be: " + zdist.ToString() + "mm";
            }
            else
                width_height_warning.IsVisible = false;
        }

        public string validation_error_string(string error_text1, string error_text2)
        {
            string result = "";

            if (this.IsVisible)
            {
                if (width_entry.Text == null || width_entry.Text.Length == 0)
                    result = error_text1;

                if (height_entry.Text == null || height_entry.Text.Length == 0)
                    result = result + error_text2;
            }

            return result;
        }

        private void width_height_changed(object sender, EventArgs e)
        {
            if (units_to_use == t_units.units_mm)
                update_width_height_warning();

            OnSelectionChanged?.Invoke(this, new EventArgs());
        }

        private void layout_changed(object sender, EventArgs e)
        {
            update_width_height_warning();
        }
    }
}