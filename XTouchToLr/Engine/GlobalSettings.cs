using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTouchToLr.Models;

namespace XTouchToLr.Engine
{
    public static class GlobalSettings
    {
        public static int faderPage { get; set; }
        public static bool sendingMidi { get; set; }

        public static Queue<MidiToSendModel> queueMidi = new Queue<MidiToSendModel>();
        public static Queue<SysexModel> queueMidiSysex = new Queue<SysexModel>();
        public static List<SaveParametersModel> saveParameterList = new List<SaveParametersModel>();

        public static bool IsSaving { get; set; }

        public static int EncoderMenuPosition { get; set; }

        //public static int threadCount { get; set; }

        public static string[] strSplit { get; set; }

        public static void InitialSettings()
        {
            faderPage = 1;
            EncoderMenuPosition = 1;

            SendToMidiDevice.Send("Encoder");

            Display.Send(1, "High.  ", "Tone   ");
            Display.Send(2, "Lights ", "Tone   ");
            Display.Send(3, "Darks  ", "Tone   ");
            Display.Send(4, "Shadow ", "Tone   ");
            Display.Send(5, "Shadow ", "Split  ");
            Display.Send(6, "Midtone", "Split  ");
            Display.Send(7, "High.  ", "Split  ");
            Display.Send(8, "       ", "       ");

            SendToMidiDevice.MidiSend(ChannelCommand.NoteOn, 0, 95, 0);

            SendToMidiDevice.MidiSend(ChannelCommand.NoteOn, 0, 80, 0);
            SendToMidiDevice.MidiSend(ChannelCommand.NoteOn, 0, 81, 0);
            SendToMidiDevice.MidiSend(ChannelCommand.NoteOn, 0, 82, 0);
            SendToMidiDevice.MidiSend(ChannelCommand.NoteOn, 0, 83, 0);

        }
    }
}
