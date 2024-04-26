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
	public partial class UnfinishedCode : ContentPage
	{
        string result = "";
        public UnfinishedCode ()
		{
			InitializeComponent ();

            cont_num.Text = "Contract number : " + App.CurrentApp.HeaderRecord.udi_cont;
            unfin_code.Text = "";
            GetHash();
        }

        private void OnCancel(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new CustomerCareCard(), this);
            this.Navigation.PopAsync(false);
        }

        private void OnDone(object sender, EventArgs e)
        {
            result = result.Replace("\0", "");

            if (unfin_code.Text.ToLower() == result)
            {
                App.net.HeaderRecord.bDone = true;


                App.CurrentApp.HeaderRecord.f_sign_date = DateTime.Today.ToShortDateString();
                App.data.SaveHeader();

                Navigation.InsertPageBefore(new CustomerCareCard(), this);
                this.Navigation.PopAsync(false);
            }
            else
            {
                DisplayAlert("Alert","The code is incorrect","OK");
            }
        }

        void GetHash()
        {
            //char* str=pApp->pAppData->access->temp.udi_cont;

            string cont_num = App.CurrentApp.HeaderRecord.udi_cont;
            //string cont_num = "01232120";

            string str_key = "JIHGFEDCBA";
            char[] key = new char[20];

            char[] str_res = new char[20];

            key = str_key.ToCharArray();

            char[] contnum = new char[20];

            contnum = cont_num.ToCharArray();

            int i;
            char[] txt = new char[32000]; //txt[32000];
            char[] vS_Key = new char[80];

            int vL_Quelle, vL_KeyPos, vL_KeyNum, vL_Dest, vL_KeyLength;

            for (i = 0; i < 20; i++)
            {
                str_res[i] = (char)0;
            }

            for (i = 0; i < 8; i++)
            {
                if (contnum[i] < 48 || contnum[i] > 57)
                {
                    contnum[i] = (char)48;
                }
            }

            //CString cstr;
            //cstr=str;
            //m_Input.GetWindowTextW( cstr );
            //str=(LPCTSTR)cstr;

            //wcstombs( str, cstr.GetBuffer(cstr.GetLength()), cstr.GetLength() );

            //str = cstr.GetBuffer(cstr.GetLength());
            //GC.GetTotalMemory(false);

            //strcpy( vS_Key , "JIHGFEDCBA" );
            vL_KeyLength = str_key.Length;

            for (int n = 1; n < (contnum.Length + 1); n++)
            {
                vL_Quelle = contnum[n - 1];

                vL_KeyPos = n % vL_KeyLength;
                vL_KeyNum = str_key[(int)vL_KeyPos - 1];
                vL_Dest = vL_Quelle ^ vL_KeyNum;

                if (vL_Dest < 48)
                {
                    vL_Dest = vL_Dest + 48;
                }
                else
                {
                    if (vL_Dest > 57 && vL_Dest < 65)
                    {
                        vL_Dest = vL_Dest + 10;
                    }
                    else
                    {
                        if (vL_Dest > 122)
                        {
                            vL_Dest = 121;
                        }
                    }
                    str_res[n - 1] = (char)vL_Dest;
                }
            }
            result = new string(str_res);
        }
    }
}