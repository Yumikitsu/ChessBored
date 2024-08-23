using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess.console.Speech
{
    public interface ICommunicator
    {
        void SendMessage(string message);
        void SendNonAsyncMessage(string message);
    }
}
