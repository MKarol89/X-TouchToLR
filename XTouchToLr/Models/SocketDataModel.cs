using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTouchToLr.Models
{
    public class SocketDataModel
    {
        public SocketDataModel(string key, float value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }
        public float Value { get; set; }
    }
}
