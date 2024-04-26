using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Widget;
using MartControls;
using PropertySurvey.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;
using Orientation = Android.Widget.Orientation;


[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace PropertySurvey.Droid
{
    public class CustomPickerRenderer : Xamarin.Forms.Platform.Android.AppCompat.PickerRenderer//,  ViewRenderer<Picker, EditText>  
    {
        IElementController ElementController => Element;

        public CustomPickerRenderer(Context context) : base(context)
        {
            AutoPackage = false;
            
            Dispose(false);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            //Control.Click += Control_Click;
        }

        protected override void Dispose(bool disposing)
        {
            if (false)//(disposing)
            {
                //Control.Click -= Control_Click;
                //var picker = (Picker)Element;
                //picker.PropertyChanged -= Control_Click;
            }


            base.Dispose(disposing);
        }
        /*
        private void Control_Click(object sender, EventArgs e)
        {
            Picker model = Element;
            model.Title = "changed";

            var picker = new NumberPicker(Context);

            if (model.Items != null && model.Items.Any())
            {
                // set style here

                picker.MaxValue = model.Items.Count - 1;
                picker.MinValue = 0;

                picker.SetBackgroundColor(Color.Purple);
                picker.SetDisplayedValues(model.Items.ToArray());
                picker.WrapSelectorWheel = false;
                picker.Value = model.SelectedIndex;

            }


            var layout = new LinearLayout(Context) { Orientation = Orientation.Vertical };
            layout.AddView(picker);
            layout.SetBackgroundColor(Color.Purple);

            var titleView = new TextView(Context);

            titleView.Text = "hmmmm";
            titleView.SetBackgroundColor(Color.ForestGreen);

            ElementController.SetValueFromRenderer(VisualElement.IsFocusedProperty, true);

            var builder = new AlertDialog.Builder(Context);
            builder.SetView(layout);

            builder.SetTitle(model.Title ?? "");

            //builder.SetCustomTitle(titleView);
            builder.SetNegativeButton("Cancel  ", (s, a) =>
            {
                ElementController.SetValueFromRenderer(VisualElement.IsFocusedProperty, false);
                // It is possible for the Content of the Page to be changed when Focus is changed.
                // In this case, we'll lose our Control.
                Control?.ClearFocus();
                _dialog = null;
            });
            builder.SetPositiveButton("Ok ", (s, a) =>
            {
                ElementController.SetValueFromRenderer(Picker.SelectedIndexProperty, picker.Value);
                // It is possible for the Content of the Page to be changed on SelectedIndexChanged.
                // In this case, the Element & Control will no longer exist.
                if (Element != null)
                {
                    if (model.Items.Count > 0 && Element.SelectedIndex >= 0)
                        Control.Text = model.Items[Element.SelectedIndex];
                    ElementController.SetValueFromRenderer(VisualElement.IsFocusedProperty, false);
                    // It is also possible for the Content of the Page to be changed when Focus is changed.
                    // In this case, we'll lose our Control.
                    Control?.ClearFocus();
                }
                _dialog = null;
            });

            Control.Text = "Control";

            _dialog = builder.Create();
            _dialog.DismissEvent += (ssender, args) =>
            {
                ElementController?.SetValueFromRenderer(VisualElement.IsFocusedProperty, false);
            };
            _dialog.Show();
        }
        */


    }
}