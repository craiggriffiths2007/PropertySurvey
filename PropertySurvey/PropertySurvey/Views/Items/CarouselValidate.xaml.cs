using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public abstract partial class CarouselValidate : ContentPage
    {
        int last_page = 0;
        protected bool changed_question_required = true; // Overwritten in derived classes if they don't want the item changed screen.
        protected bool validation_required = true;       // Overwritten in derived classes when validation is not required
        protected bool saves_required = true;            // Overwritten in derived classes when calls to save not required
        protected bool save_on_pagechange = false;       // Overwritten in derived classes when we want to do a save between pages
        protected bool show_image_button = true;         // Overwritten in derived classes when we don't want the image button

        public CarouselValidate()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (show_image_button)
            {
                if(validate_drawings_and_pictures().Length > 0)
                   images_button.IconImageSource = "images.png";
                else
                   images_button.IconImageSource = "images_complete.png";
            }
            else
                images_button.IconImageSource = "";
        }

        /*
        public void SetPageNumber()
        {
            if (this.Children.Count > 1)
            {
                page_num.Text = (this.Children.IndexOf(CurrentPage) + 1).ToString() + "/" + this.Children.Count().ToString();
            }
            else
            {
                page_num.Text = "";
            }
        }
        */

        private void OnImagesClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Images(), false);
        }

        protected abstract string validate_page();
        protected abstract string validate_drawings_and_pictures();
        protected abstract void save_item(bool complete);

        protected virtual bool already_signed() // Override this for the main 11 items
        {
            return false;
        }

        /*
        protected virtual void set_page(int page_num) // This version gets called for anything with just one page
        {
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            int new_page = this.Children.IndexOf(CurrentPage);
            bool allow_change;
            string error_text = "";

            // new_page < last_page means they have swiped left, always allow, 
            // new_page = last_page either means we are loading or they just failed a swipe right and we are returning to same page
            if (new_page <= last_page || !validation_required)
                allow_change = true;   
            else
            {
                // To bypass validation for testing, change the next line to error_text = "";
                // Do not check into GiT like that.
                error_text = validate_page(last_page);
                allow_change = error_text == "";
            }

            if (allow_change)
            {
                if (save_on_pagechange)
                    App.data.SaveItemAsync();

                SetPageNumber();
                last_page = new_page;
            }
            else
            {
                DisplayAlert("Missing information", "Please complete :\n\n" + error_text, "OK");
                set_page(last_page);
            }
        }
        */

        protected override bool OnBackButtonPressed()
        {
            //int last_page = this.Children.Count() - 1;
            int page_num;
            string error_text = "";

            base.OnBackButtonPressed();

            if (validation_required)
            {
                //for (page_num = 0; page_num <= last_page; page_num++)
                //    error_text += validate_page(page_num);
                error_text = validate_page();

                if (App.net.CurrentItem == App.net.RootItem) // Prevent photo/drawing validation when in a sub-item
                    error_text += validate_drawings_and_pictures();
            }

            if (error_text != "")
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var response = await Application.Current.MainPage.DisplayAlert("Missing information",
                        "Please complete :\n\n" + error_text + "\n\nClose Anyway?\n", "   Yes   ", "   No   ");
                    if (response)
                    {
                        save_item(false);
                        await this.Navigation.PopAsync(false);
                    }
                });
            }
            else if (changed_question_required && App.net.CurrentItem == App.net.RootItem) // Inherited class wants this question, and it's not a sub-item
            {
                string question_text;
                if (already_signed())
                    question_text = "This has already been signed to say it is different to the original, do you want to sign again?";
                else
                    question_text = "Is the item going to be different from the original?";

                Device.BeginInvokeOnMainThread(async () =>
                {
                    var response = await Application.Current.MainPage.DisplayAlert("", question_text, "   Yes   ", "   No   ");
                    if (response)
                        Navigation.InsertPageBefore(new ItemChanged(), this);

                    save_item(true);
                    //return true;
                    await this.Navigation.PopAsync(false);
                });
            }
            else
            {
                if (saves_required)
                    save_item(true);
                //return true;
                this.Navigation.PopAsync(false);
            }

            //base.OnBackButtonPressed();
            return true;
        }
    }
}