using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTouchToLr.Data;

namespace XTouchToLr.Engine
{
    public static class MidiIn
    {
        public static void Start()
        {
            var x = Sanford.Multimedia.Midi.InputDevice.DeviceCount;


            for (int i = 0; i < x; i++)
            {
                var b = InputDevice.GetDeviceCapabilities(i);

                Console.WriteLine("ID: " + i + " Driver version: " + b.driverVersion + " Mid: " + b.mid + " name: " + b.name + " pid: " + b.pid + " Support: " + b.support);


            }



            InputDevice inDevice = new InputDevice(0);
            //ChannelStopper stopper = new ChannelStopper();

            inDevice.ChannelMessageReceived += delegate (object sender, ChannelMessageEventArgs e)
            {
                //stopper.Process(e.Message);
                //Console.WriteLine(e.Message.Bytes[0] + "." + e.Message.Bytes[1] + "." + e.Message.Bytes[2]);

                byte b0 = e.Message.Bytes[0];
                byte b1 = e.Message.Bytes[1];
                byte b2 = e.Message.Bytes[2];

                LrParameters.SetParameterByMidi(b0, b1, b2);

            };

            inDevice.StartRecording();
        }
    }
}
