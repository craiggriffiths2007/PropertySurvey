using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PropertySurvey
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewLeaded : ContentPage
    {
        public ViewLeaded(t_current_item parent_item, MartControls.t_leading_types leading_type)
        {
            InitializeComponent();

            switch (parent_item)
            {
                case t_current_item.item_aluminium: BindingContext = App.net.AlumRecord as AlumTable; break;
                case t_current_item.item_composite: BindingContext = App.net.CompRecord as CompositeTable; break;
                case t_current_item.item_timber: BindingContext = App.net.TimberRecord as TimberTable; break;
                case t_current_item.item_upvc: BindingContext = App.net.UPVCRecord as UPVCTable; break;
                case t_current_item.item_glass:
                    BindingContext = App.net.GlassRecord as GlassTable;

                    switch (leading_type)
                    {
                        case MartControls.t_leading_types.lt_georgian_lead:
                        case MartControls.t_leading_types.lt_georgian_bar:
                            trim_30mm_answer.button_binding = "gb_trim";
                            trim_30mm_answer.IsVisible = true;
                            break;
                    }
                    break;
                    // make lead bar controls visible
            }

            switch (leading_type)
            {
                case MartControls.t_leading_types.lt_diamond_lead:
                case MartControls.t_leading_types.lt_georgian_lead:
                    single_or_double_answer.IsVisible = true;
                    type_of_lead_answer.IsVisible = true;
                    break;
                case MartControls.t_leading_types.lt_back_to_back:
                    lead_thickness_answer.IsVisible = false;
                    spacer_thickness_answer.IsVisible = true;
                    overall_spacer_width_answer.IsVisible = true;
                    diamond_width_answer.LabelText = "Spacing Width";
                    diamond_height_answer.LabelText = "Spacing Height";
                    break;
                case MartControls.t_leading_types.lt_georgian_bar:
                    anti_rattle_answer.IsVisible = true;
                    anti_rattle_answer.set_button_list(SurveyFitterButtonLists.georgian_bar_anti_rattle_button_list);
                    break;
            }
        }
    }
}