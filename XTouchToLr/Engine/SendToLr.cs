using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTouchToLr.Data;

namespace XTouchToLr.Engine
{
    public static class SendToLr
    {
        public static List<Parameter> listParameters = LrParameters.listParameter;

        //public static void Send()
        //{
        //    foreach (var item in listParameters)
        //    {
        //        Sockets.sendToLr(item.name, item.value.ToString());
        //    }
        //}

        public static void Send(string name)
        {
            var model = listParameters.Where(x => x.name == name).FirstOrDefault();

            Sockets.sendToLr(model.name, model.value.ToString());

        }

        public static void Send(string name, string value)
        {
            Sockets.sendToLr(name, value);

        }

        internal static void SendReset(string name)
        {
            Sockets.sendToLr(name, "reset");
        }
    }
}
