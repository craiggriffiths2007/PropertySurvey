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
    public partial class WarningMessage : StackLayout
    {
        public WarningMessage()
        {
            InitializeComponent();
        }

        public string Text
        {
            get { return TheMessage.Text; }
            set { TheMessage.Text = value; }
        }
    }
}