using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTouchToLr.Models
{
    public class SysexModel
    {
        public SysexModel(SysExMessage msg)
        {
            this.msg = msg;
        }

        public SysExMessage msg { get; set; }
    }
}
