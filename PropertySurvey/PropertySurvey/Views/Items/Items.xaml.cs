using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Items : ContentPage
    {
        public Items()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            SetItemNumbers();
        }

        void SetItemNumbers()
        {
            int total_incomplete = 0;
            total_incomplete += App.net.HeaderRecord.incomplete_upvc;
            total_incomplete += App.net.HeaderRecord.incomplete_panels;
            total_incomplete += App.net.HeaderRecord.incomplete_glass;
            total_incomplete += App.net.HeaderRecord.incomplete_alum;
            total_incomplete += App.net.HeaderRecord.incomplete_garage;
            total_incomplete += App.net.HeaderRecord.incomplete_timber;
            total_incomplete += App.net.HeaderRecord.incomplete_cons;
            total_incomplete += App.net.HeaderRecord.incomplete_lock;
            total_incomplete += App.net.HeaderRecord.incomplete_comp;
            total_incomplete += App.net.HeaderRecord.incomplete_green;
            total_incomplete += App.net.HeaderRecord.incomplete_bifold;

            App.net.total_items = App.net.HeaderRecord.total_upvc - App.net.HeaderRecord.incomplete_upvc;
            App.net.total_items += App.net.HeaderRecord.total_panels - App.net.HeaderRecord.incomplete_panels;
            App.net.total_items += App.net.HeaderRecord.total_glass - App.net.HeaderRecord.incomplete_glass;
            App.net.total_items += App.net.HeaderRecord.total_alum - App.net.HeaderRecord.incomplete_alum;
            App.net.total_items += App.net.HeaderRecord.total_garage - App.net.HeaderRecord.incomplete_garage;
            App.net.total_items += App.net.HeaderRecord.total_timber - App.net.HeaderRecord.incomplete_timber;
            App.net.total_items += App.net.HeaderRecord.total_cons - App.net.HeaderRecord.incomplete_cons;
           // App.net.total_items += App.net.HeaderRecord.fit_no_of_videos - App.net.HeaderRecord.incomplete_upvc;
            App.net.total_items += App.net.HeaderRecord.total_lock - App.net.HeaderRecord.incomplete_lock;
            App.net.total_items += App.net.HeaderRecord.total_comp - App.net.HeaderRecord.incomplete_comp;
            App.net.total_items += App.net.HeaderRecord.total_green - App.net.HeaderRecord.incomplete_green;
            App.net.total_items += App.net.HeaderRecord.total_bifold - App.net.HeaderRecord.incomplete_bifold;
            App.net.HeaderRecord.si_numitem = App.net.total_items;

            if (App.net.HeaderRecord.total_upvc >0)
            {
                i1.Text = "UPVC - " + App.net.HeaderRecord.total_upvc.ToString();
                if (App.net.HeaderRecord.incomplete_upvc > 0)
                    i1.TextColor = Color.DarkRed;
                else
                    i1.TextColor = Color.DarkGreen;
            }
            else
            {
                i1.Text = "UPVC";
            }
            if (App.net.HeaderRecord.total_panels > 0)
            {
                i2.Text = "Panel - " + App.net.HeaderRecord.total_panels.ToString();
                if (App.net.HeaderRecord.incomplete_panels > 0)
                    i2.TextColor = Color.DarkRed;
                else
                    i2.TextColor = Color.DarkGreen;
            }
            else
            {
                i2.Text = "Panel";
            }
            if (App.net.HeaderRecord.total_glass > 0)
            {
                i3.Text = "Glass Only - " + App.net.HeaderRecord.total_glass.ToString();
                if (App.net.HeaderRecord.incomplete_glass > 0)
                    i3.TextColor = Color.DarkRed;
                else
                    i3.TextColor = Color.DarkGreen;
            }
            else
            {
                i3.Text = "Glass Only";
            }
            if (App.net.HeaderRecord.total_alum > 0)
            {
                i4.Text = "Aluminium - " + App.net.HeaderRecord.total_alum.ToString();
                if (App.net.HeaderRecord.incomplete_alum > 0)
                    i4.TextColor = Color.DarkRed;
                else
                    i4.TextColor = Color.DarkGreen;
            }
            else
            {
                i4.Text = "Aluminium";
            }
            if (App.net.HeaderRecord.total_garage > 0)
            {
                i5.Text = "Garage - " + App.net.HeaderRecord.total_garage.ToString();
                if (App.net.HeaderRecord.incomplete_garage > 0)
                    i5.TextColor = Color.DarkRed;
                else
                    i5.TextColor = Color.DarkGreen;
            }
            else
            {
                i5.Text = "Garage";
            }
            if (App.net.HeaderRecord.total_timber > 0)
            {
                i6.Text = "Timber - " + App.net.HeaderRecord.total_timber.ToString();
                if (App.net.HeaderRecord.incomplete_timber > 0)
                    i6.TextColor = Color.DarkRed;
                else
                    i6.TextColor = Color.DarkGreen;
            }
            else
            {
                i6.Text = "Timber";
            }
            if (App.net.HeaderRecord.total_bifold > 0)
            {
                i6a.Text = "Bifolding Door - " + App.net.HeaderRecord.total_bifold.ToString();
                if (App.net.HeaderRecord.incomplete_bifold > 0)
                    i6a.TextColor = Color.DarkRed;
                else
                    i6a.TextColor = Color.DarkGreen;
            }
            else
            {
                i6a.Text = "Bifolding Door";
            }
            if (App.net.HeaderRecord.total_cons > 0)
            {
                i7.Text = "Conservatory Roof - " + App.net.HeaderRecord.total_cons.ToString();
                if (App.net.HeaderRecord.incomplete_cons > 0)
                    i7.TextColor = Color.DarkRed;
                else
                    i7.TextColor = Color.DarkGreen;
            }
            else
            {
                i7.Text = "Conservatory Roof";
            }
            if (App.net.HeaderRecord.total_lock > 0)
            {
                i8.Text = "Locking System - " + App.net.HeaderRecord.total_lock.ToString();
                if (App.net.HeaderRecord.incomplete_lock > 0)
                    i8.TextColor = Color.DarkRed;
                else
                    i8.TextColor = Color.DarkGreen;
            }
            else
            {
                i8.Text = "Locking System";
            }
            if (App.net.HeaderRecord.total_comp > 0)
            {
                i9.Text = "Composite Door - " + App.net.HeaderRecord.total_comp.ToString();
                if (App.net.HeaderRecord.incomplete_comp > 0)
                    i9.TextColor = Color.DarkRed;
                else
                    i9.TextColor = Color.DarkGreen;
            }
            else
            {
                i9.Text = "Composite Door";
            }
            if (App.net.HeaderRecord.total_green > 0)
            {
                i10.Text = "Greenhouse/Sheds - " + App.net.HeaderRecord.total_green.ToString();
                if (App.net.HeaderRecord.incomplete_green > 0)
                    i10.TextColor = Color.DarkRed;
                else
                    i10.TextColor = Color.DarkGreen;
            }
            else
            {
                i10.Text = "Greenhouse/Sheds";
            }

            if (App.net.HeaderRecord.si_numitem > 0)
            {
                if (total_incomplete > 0)
                {
                    Title = "Items - " + App.net.HeaderRecord.si_numitem.ToString();// + "    (" + total_incomplete.ToString() + " Incomplete)";
                }
                else
                    Title = "Items - " + App.net.HeaderRecord.si_numitem.ToString();
            }
            else
            {
                Title = "Items";
            }
        }

        private void OnUPVC(object sender, EventArgs e)
        {
            App.net.RootItem = "upvc";
            App.net.CurrentItem = "upvc";
            Navigation.PushAsync(new AllBrowse(), false);
        }

        private void OnTimber(object sender, EventArgs e)
        {
            App.net.RootItem = "timber";
            App.net.CurrentItem = "timber";
            Navigation.PushAsync(new AllBrowse(), false);
        }

        private void OnGlass (object sender, EventArgs e)
        {
            App.net.RootItem = "glass";
            App.net.CurrentItem = "glass";
            Navigation.PushAsync(new AllBrowse(), false);
        }

        private void OnAlum(object sender, EventArgs e)
        {
            App.net.RootItem = "alum";
            App.net.CurrentItem = "alum";
            Navigation.PushAsync(new AllBrowse(), false);
        }

        private void OnPanel(object sender, EventArgs e)
        {
            App.net.RootItem = "panel";
            App.net.CurrentItem = "panel";
            Navigation.PushAsync(new AllBrowse(), false);
        }

        private void OnGreen(object sender, EventArgs e)
        {
            App.net.RootItem = "green";
            App.net.CurrentItem = "green";
            Navigation.PushAsync(new AllBrowse(), false);
        }

        private void OnCompositeDoor(object sender, EventArgs e)
        {
            App.net.RootItem = "comp";
            App.net.CurrentItem = "comp";
            Navigation.PushAsync(new AllBrowse(), false);
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            Navigation.PopAsync(false);
            return true;
        }
        private void OnLocking(object sender, EventArgs e)
        {
            App.net.RootItem = "lock";
            App.net.CurrentItem = "lock";
            Navigation.PushAsync(new AllBrowse(), false);
        }
        
        private void OnBifold(object sender, EventArgs e)
        {
            App.net.RootItem = "bifold";
            App.net.CurrentItem = "bifold";
            Navigation.PushAsync(new AllBrowse(), false);
        }

        private void OnConservatory(object sender, EventArgs e)
        {
            App.net.RootItem = "cons";
            App.net.CurrentItem = "cons";
            Navigation.PushAsync(new AllBrowse(), false);
        }

        private void OnGarage(object sender, EventArgs e)
        {
            App.net.RootItem = "garage";
            App.net.CurrentItem = "garage";
            Navigation.PushAsync(new AllBrowse(), false);
        }
    }
}
