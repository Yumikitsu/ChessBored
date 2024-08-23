using chess.console.Speech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess.console.ConsoleOrSpeech
{
    public class ConsoleCommunicator : ICommunicator
    {
        public void SendMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void SendNonAsyncMessage(string message)
        {
            //never used, interface wants it though
        }
    }
}
