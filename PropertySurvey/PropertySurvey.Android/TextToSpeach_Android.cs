using Android.Speech.Tts;
using Xamarin.Forms;
using PropertySurvey.Droid;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

[assembly: Dependency(typeof(TextToSpeech_Android))]
namespace PropertySurvey.Droid
{
    public class TextToSpeech_Android : Java.Lang.Object, ITextToSpeech, TextToSpeech.IOnInitListener
    {
        TextToSpeech speaker;
        string toSpeak;

        public void Speak(string text)
        {
            toSpeak = text;
            if (speaker == null)
            {
                speaker = new TextToSpeech(MainActivity.Instance, this);

                //List<Voice> voices = speaker.Voices.ToList();

                //speaker.SetVoice(voices[0]);
               // speaker.SetLanguage(Java.Util.Locale.French);
            }
            else
            {
                speaker.SetPitch((float)App.net.App_Settings.voice_pitch / 100.0f);
                speaker.SetSpeechRate((float)App.net.App_Settings.voice_speed / 100.0f);
                
                //speaker.SetAudioAttributes()
                speaker.Speak(toSpeak, QueueMode.Flush, null, null);
                
                Debug.WriteLine("spoke " + toSpeak);
            }
        }

        #region IOnInitListener implementation
        public void OnInit(OperationResult status)
        {
            if (status.Equals(OperationResult.Success))
            {
                speaker.SetPitch((float)App.net.App_Settings.voice_pitch / 100.0f);
                speaker.SetSpeechRate((float)App.net.App_Settings.voice_speed / 100.0f);
                Debug.WriteLine("speaker init");
                speaker.Speak(toSpeak, QueueMode.Flush, null, null);
            }
            else
            {
                Debug.WriteLine("was quiet");
            }
        }
        #endregion
    }
}