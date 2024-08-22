using chess.console.Speech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace chess.console.ConsoleOrSpeech
{
    public class SpeechCommunicator : ICommunicator
    {
        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        public void SendMessage(string message)
        {
            

            //synthesizer.Speak(message);
            synthesizer.SpeakAsyncCancelAll();
            synthesizer.SpeakAsync(message);
        }
        
    }
}
