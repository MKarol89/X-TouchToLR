using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTouchToLr.Engine
{
    public static class Display
    {
        private static List<Dsp> DisplayList = new List<Dsp>();

        public static void Start()
        {
            DisplayList.Add(new Dsp(1, "", "00", "38"));
            DisplayList.Add(new Dsp(2, "", "07", "3F"));
            DisplayList.Add(new Dsp(3, "", "0E", "46"));
            DisplayList.Add(new Dsp(4, "", "15", "4D"));
            DisplayList.Add(new Dsp(5, "", "1C", "54"));
            DisplayList.Add(new Dsp(6, "", "23", "5B"));
            DisplayList.Add(new Dsp(7, "", "2A", "62"));
            DisplayList.Add(new Dsp(8, "", "31", "69"));
        }

        public static void Send(int displayNumber, string txt1, string txt2)
        {
            var model = DisplayList.Where(x => x.Number == displayNumber).FirstOrDefault();

            
                string source = "F0 00 00 66 14 12";

                byte[] resultTxt = Encoding.ASCII.GetBytes(txt1);
                byte[] resultTxt2 = Encoding.ASCII.GetBytes(txt2);


                byte[] result = source.Split(' ').Select(item => Convert.ToByte(item, 16)).ToArray();

                byte[] resultPageFirstLine = model.FirstLine.Split(' ').Select(item => Convert.ToByte(item, 16)).ToArray();
                byte[] resultPageSecondLine = model.SecondLine.Split(' ').Select(item => Convert.ToByte(item, 16)).ToArray();


                int lenght = result.Length + resultTxt.Length + 2;
                int lenght2 = result.Length + resultTxt2.Length + 2;


                byte[] sum = new byte[lenght];
                byte[] sum2 = new byte[lenght2];


                result.CopyTo(sum, 0);
                resultPageFirstLine.CopyTo(sum, result.Length);
                resultTxt.CopyTo(sum, result.Length+1);
                sum[lenght - 1] = 247;

                result.CopyTo(sum2, 0);
                resultPageSecondLine.CopyTo(sum2, result.Length);
                resultTxt2.CopyTo(sum2, result.Length + 1);
                sum2[lenght - 1] = 247;

                SysExMessage sysEx = new SysExMessage(sum);
                SysExMessage sysEx2 = new SysExMessage(sum2);


                SendToMidiDevice.SysExSend(sysEx);
                SendToMidiDevice.SysExSend(sysEx2);


            
        }


    }

    public class Dsp
    {
        public Dsp(int number, string text, string firstLine, string secondLine)
        {
            Number = number;
            Text = text;
            FirstLine = firstLine;
            SecondLine = secondLine;
        }

        public int Number { get; set; }
        public string Text { get; set; }
        public string FirstLine { get; set; }
        public string SecondLine { get; set; }
    }

}
