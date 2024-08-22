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
        public void SendMessage(string message)
        {
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();

            //synthesizer.Speak(message);
            synthesizer.SpeakAsync(message);
        }
    }
}
