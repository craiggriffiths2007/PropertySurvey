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
	public partial class InstructionsVehicle : CarouselPage
    {
		public InstructionsVehicle ()
		{
			InitializeComponent ();

            Text0.Text = "1. YOU MUST STOP\n\n"
                       + "The law insists that you stop for a reasonable time if any vehicle (other than your own) has been damaged or if anyone has been injured, or if any dog, sheep, pig, goat, mule, donkey, horse or cattle has been injured (cats for some reason don’t seem to matter).\n\n";

            Text1.Text = "2. ATTEND TO INJURIES\n\n"
                       + "If there are serious injuries, call for an ambulance and the police. Give whatever help you can to casualties but remember unnecessary movement, however slight, may aggravate injuries – only move people "
                       + "if they are in serious danger from fire or from other traffic. If you make the call yourself stay calm and be clear – about where you are, the number of casualties and type of vehicles. If any tanker vehicle "
                       + "is involved provide details of its HAZCHEM label. Stop all smoking in case of spillage of fuel or chemicals. Remember to check whether it’s necessary to warn oncoming traffic of the accident.\n\n";

            Text2.Text = "3. EXCHANGE DETAILS\n\n"
                       + "Note the name, address, telephone number, registration number and make of the vehicle of other driver(s). Ask for their insurance company's name too. Find out if the driver is the owner of the vehicle, if "
                       + "not, get the owner's details.\n\n"
                       + "You should only provide our Company's insurance details to someone with reasonable grounds for requiring this information.\n\n";

            Text3.Text = "4. NOTE ANY WITNESSES\n\n"
                       + "It is important to get witnesses' details as quickly as possible - they may not stay at the scene for long.\n"
                       + "Names, addresses, and telephone numbers of as many independent witnesses as possible. Registration number of any vehicle whose occupants may have seen what happened.\n\n";

            Text4.Text = "5. KEEP QUIET\n\n"
                       + "Don't admit liability, apologise or offer payment even if you think the accident was your fault. If you admit blame, it may invalidate our insurance.\n\n";

            Text5.Text = "6. WRITE NOTES\n\n"
                       + "Take careful notes, covering the following:\n"
                       + "Time and date of accident.\n"
                       + "The state of the traffic ( eg. heavy, fast moving).\n"
                       + "Weather conditions and visibility.\n"
                       + "Road surface conditions.\n"
                       + "Any signals which were being made by you or anyone else.\n"
                       + "Details of other vehicles, their colours, their condition, wether they had any lights on etc.\n"
                       + "What was said by other people and you.\n"
                       + "The station, collar number of the police officer involved.\n\n";

            Text6.Text = "7. MAKE A SKETCH MAP OF THE SCENE SHOWING DETAILS OF THE DAMAGE\n\n"
                       + "The immediate road lay-out with names, widths and which way is north. The positions of vehicles immediately before and after the incident - note any distance between vehicles, from road signs or\n"
                       + "junctions, away from the kerb etc.\n"
                       + "Make a note of any skid marks, position of witnesses.\n";

            Text7.Text = "8. TAKE PHOTOGRAPHS\n\n"
                       + "Take photographs of the accident scene and damage prior to the vehicles being moved where possible.\n\n";

            Text8.Text = "9. COMPLETE ACCIDENT REPORT\n\n"
                       + "The accident form should be fully completed and sent to the company within 24 hours.\n\n"
                       + "If the vehicle cannot be driven to base, telephone the office and obtain the name of an approved Repairer for the make of vehicle and arrange for it to be taken to the garage.\n"
                       + "Take photographs of the accident scene and damage prior to the vehicles being moved where possible.\n"
                       + "Prosecution - If as a result of an accident you receive a note of intended prosecution from the police it should be passed immediately to a Company contact. It will then be sent to our insurance broker.\n"
                       + "Take photographs of the accident scene and damage prior to the vehicles being moved where possible.\n\n";

            Text9.Text = "10. POLICE INVOLVEMENT\n\n"
                       + "If anyone is hurt you must call the police. Also call them if you think the other driver has commited an offence or if you've damaged someone's property but can't find them to tell. In some circumstances, \n"
                       + "the police may ask you to produce your insurance certificate (either immediately, or within 24 hours at a police station of your choice). The police may ask you for a statement. You don't have to make one\n"
                       + "straight away - you may be in a state of shock and wish to gather your thoughts.\n\n"
                       + "You can make a statement some hours later when you had time to think.\n";
        }

        protected override void OnCurrentPageChanged()
        {
            page_num.Text = (this.Children.IndexOf(CurrentPage) + 1).ToString() + "/" + this.Children.Count().ToString();
        }

        private async void accept_button_clicked(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new AccidentMenuVehicle(), this);
            await Navigation.PopAsync(false);
        }
    }
}