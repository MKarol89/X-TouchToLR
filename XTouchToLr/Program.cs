using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XTouchToLr.Data;
using XTouchToLr.Engine;

namespace XTouchToLr
{
    public class Program
    {
        static void Main(string[] args)
        {
            Display.Start();

            LrParameters.Start();
            GlobalSettings.InitialSettings();

            Thread socketThread = new Thread(Sockets.StartClient);
            socketThread.Start();

            Thread midiSendingThread = new Thread(SendToMidiDevice.ThreadSendingMidi);
            midiSendingThread.Start();
            
            


            Sockets.StartUploadClient();

            MidiIn.Start();

            Console.Read();
        }
    }


}

