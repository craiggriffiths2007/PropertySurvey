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
	public partial class YesNoLabelEntryLabel : StackLayout
    {
        public string TextBinding { set { entry_area.TextBinding = value; } }
        public string button_binding { set { yesno_question.button_binding = value; } }
        public string ButtonLabel { set { yesno_question.LabelText = value; } }

        public bool IsComplete()
        {
            if ((yesno_question.ButtonState == 2 && entry_area.text.Length == 0) || yesno_question.ButtonState==0)
                return false;
            else
                return true;
        }

        public YesNoLabelEntryLabel()
		{
			InitializeComponent ();
		}

        private void layout_changed(object sender, EventArgs e)
        {
            SetEntryVisible();
        }

        private void SetEntryVisible()
        {
            if (yesno_question.ButtonState == 2)
                entry_area.IsVisible = true;
            else
                entry_area.IsVisible = false;
        }

        private void Yesno_question_OnSelectionChanged(object sender, EventArgs e)
        {
            SetEntryVisible();
        }
    }
}