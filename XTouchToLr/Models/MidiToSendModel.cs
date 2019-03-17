using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTouchToLr.Models
{
    public class MidiToSendModel
    {
        public MidiToSendModel(ChannelCommand channelCommand, int b1, int b2, int b3)
        {
            this.channelCommand = channelCommand;
            B1 = b1;
            B2 = b2;
            B3 = b3;
        }

        public ChannelCommand channelCommand { get; set; }
        public int B1 { get; set; }
        public int B2 { get; set; }
        public int B3 { get; set; }
        
    }
}
