using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Diagnostics;

namespace VoiceBot
{
    public partial class Form1 : Form
    {
        SpeechSynthesizer s = new SpeechSynthesizer();
        Boolean wake = true;
        Choices list = new Choices();
        public Form1()
        {

            SpeechRecognitionEngine rec = new SpeechRecognitionEngine();
            list.Add(new String[]
            {   "hello",
                "how are you?",
                "what time is it",
                "what is today",
                "open bing",
                "open google",
                "wake",
                "sleep",
                "restart",
                "update",
                "thank you"
            });
            Grammar gr = new Grammar(new GrammarBuilder(list));

            try
            {
                rec.RequestRecognizerUpdate();
                rec.LoadGrammar(gr);
                rec.SpeechRecognized += rec_SpeechRecognized;
                rec.SetInputToDefaultAudioDevice();
                rec.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch
            {
                return;
            }
            s.SelectVoiceByHints(VoiceGender.Female);
            //initial voice from program
            s.Speak("hello, my name is Medusa.");

            InitializeComponent();
        }

        public void restart()
        {
            Process.Start(@"C:\Users\Medusa\medusa.exe");
            Environment.Exit(0);
        }
        public void say(String h)
        {
            s.Speak(h);
        }
        private void rec_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            String r = e.Result.Text;

            if (r == "wake") wake = true;
            if (r == "sleep") wake = false;

            if (wake == true)
            {                
                if (r == "restart" || r == "update") {restart();}
                if (r == "hello")//what you speak here
                { 
                    say("hi");//what the program responds with
                }
                if (r == "how are you?") {say("great, and you?");}
                if (r == "what time is it"){say(DateTime.Now.ToString("h:mm tt"));}
                if (r == "what is today"){say(DateTime.Now.ToString("M/d/yyyy"));}
                if (r == "open bing") { Process.Start("http://bing.com"); }
                if (r == "open google"){Process.Start("http://google.com");}
                if (r == "thank you"){say("you're welcome");}
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
