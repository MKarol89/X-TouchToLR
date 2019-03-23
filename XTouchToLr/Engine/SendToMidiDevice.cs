using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XTouchToLr.Data;

namespace XTouchToLr.Engine
{
    public static class SendToMidiDevice
    {
        private static List<Parameter> ListLrParameters = LrParameters.listParameter;

        public static ManualResetEvent rstEvent = new ManualResetEvent(false);

        public static void Send(string name)
        {
            if (name == "Encoder")
            {
                switch (GlobalSettings.EncoderMenuPosition)
                {
                    case 1:
                        MidiSend(ChannelCommand.NoteOn, 0, 40, 127);
                        MidiSend(ChannelCommand.NoteOn, 0, 42, 0);
                        MidiSend(ChannelCommand.NoteOn, 0, 44, 0);
                        MidiSend(ChannelCommand.NoteOn, 0, 41, 0);
                        MidiSend(ChannelCommand.NoteOn, 0, 43, 0);
                        MidiSend(ChannelCommand.NoteOn, 0, 45, 0);

                        Send("ParametricHighlights");
                        Send("ParametricLights");
                        Send("ParametricDarks");
                        Send("ParametricShadows");
                        Send("ParametricShadowSplit");
                        Send("ParametricMidtoneSplit");
                        Send("ParametricHighlightSplit");
                        MidiSend(ChannelCommand.Controller, 0, 55, 0);

                        break;

                    case 2:
                        MidiSend(ChannelCommand.NoteOn, 0, 40, 0);
                        MidiSend(ChannelCommand.NoteOn, 0, 42, 127);
                        MidiSend(ChannelCommand.NoteOn, 0, 44, 0);
                        MidiSend(ChannelCommand.NoteOn, 0, 41, 0);
                        MidiSend(ChannelCommand.NoteOn, 0, 43, 0);
                        MidiSend(ChannelCommand.NoteOn, 0, 45, 0);

                        Send("SaturationAdjustmentRed");
                        Send("SaturationAdjustmentOrange");
                        Send("SaturationAdjustmentYellow");
                        Send("SaturationAdjustmentGreen");
                        Send("SaturationAdjustmentAqua");
                        Send("SaturationAdjustmentBlue");
                        Send("SaturationAdjustmentPurple");
                        Send("SaturationAdjustmentMagenta");

                        break;

                    case 3:
                        MidiSend(ChannelCommand.NoteOn, 0, 40, 0);
                        MidiSend(ChannelCommand.NoteOn, 0, 42, 0);
                        MidiSend(ChannelCommand.NoteOn, 0, 44, 127);
                        MidiSend(ChannelCommand.NoteOn, 0, 41, 0);
                        MidiSend(ChannelCommand.NoteOn, 0, 43, 0);
                        MidiSend(ChannelCommand.NoteOn, 0, 45, 0);

                        Send("HueAdjustmentRed");
                        Send("HueAdjustmentOrange");
                        Send("HueAdjustmentYellow");
                        Send("HueAdjustmentGreen");
                        Send("HueAdjustmentAqua");
                        Send("HueAdjustmentBlue");
                        Send("HueAdjustmentPurple");
                        Send("HueAdjustmentMagenta");
                        break;

                    case 4:
                        MidiSend(ChannelCommand.NoteOn, 0, 40, 0);
                        MidiSend(ChannelCommand.NoteOn, 0, 42, 0);
                        MidiSend(ChannelCommand.NoteOn, 0, 44, 0);
                        MidiSend(ChannelCommand.NoteOn, 0, 41, 127);
                        MidiSend(ChannelCommand.NoteOn, 0, 43, 0);
                        MidiSend(ChannelCommand.NoteOn, 0, 45, 0);

                        Send("LuminanceAdjustmentRed");
                        Send("LuminanceAdjustmentOrange");
                        Send("LuminanceAdjustmentYellow");
                        Send("LuminanceAdjustmentGreen");
                        Send("LuminanceAdjustmentAqua");
                        Send("LuminanceAdjustmentBlue");
                        Send("LuminanceAdjustmentPurple");
                        Send("LuminanceAdjustmentMagenta");
                        break;

                    case 5:
                        MidiSend(ChannelCommand.NoteOn, 0, 40, 0);
                        MidiSend(ChannelCommand.NoteOn, 0, 42, 0);
                        MidiSend(ChannelCommand.NoteOn, 0, 44, 0);
                        MidiSend(ChannelCommand.NoteOn, 0, 41, 0);
                        MidiSend(ChannelCommand.NoteOn, 0, 43, 127);
                        MidiSend(ChannelCommand.NoteOn, 0, 45, 0);

                        Send("PostCropVignetteAmount");
                        Send("PostCropVignetteMidpoint");
                        Send("PostCropVignetteFeather");
                        Send("PostCropVignetteRoundness");
                        Send("PostCropVignetteHighlightContrast");
                        


                        TurnOffEncoderDisplay(EncodersMidiNumber.Encoder1);
                        TurnOffEncoderDisplay(EncodersMidiNumber.Encoder7);
                        TurnOffEncoderDisplay(EncodersMidiNumber.Encoder8);

                        break;

                    case 6:
                        MidiSend(ChannelCommand.NoteOn, 0, 40, 0);
                        MidiSend(ChannelCommand.NoteOn, 0, 42, 0);
                        MidiSend(ChannelCommand.NoteOn, 0, 44, 0);
                        MidiSend(ChannelCommand.NoteOn, 0, 41, 0);
                        MidiSend(ChannelCommand.NoteOn, 0, 43, 0);
                        MidiSend(ChannelCommand.NoteOn, 0, 45, 127);
                        break;

                    default:
                        break;
                }     
            }   //Encoders MENU
            

            if (name == "Null")
            {
                MidiSend(ChannelCommand.PitchWheel, 4, 0, 0);
                MidiSend(ChannelCommand.PitchWheel, 5, 0, 0);
                MidiSend(ChannelCommand.PitchWheel, 6, 0, 0);
                MidiSend(ChannelCommand.PitchWheel, 7, 0, 0);
            }

            foreach (var item in ListLrParameters)
            {
                if (GlobalSettings.faderPage == 1)
                {

                    if (item.name == "Temperature" && item.name == name)
                    {

                        var valueRange = item.maxValue - item.minValue;
                        var pitchRange = 16384;
                        var step = valueRange / pitchRange;
                        var value = item.value;

                        if (value < item.minValue)
                        {
                            value = item.minValue;
                        }

                        if (value > item.maxValue)
                        {
                            value = item.maxValue;
                        }

                        var pitchValue = (value - item.minValue) / step;
                        int data2 = (int)(pitchValue / 127);
                        int data1 = (int)pitchValue % 127;

                        if (data2 > 127) { data2 = 127; };
                        if (data1 > 127) { data1 = 127; };

                        //Console.WriteLine(ListLrParameters.Count);

                        MidiSend(ChannelCommand.PitchWheel, 0, data1, data2);

                    }

                    if (item.name == "Tint" && item.name == name)
                    {

                        var valueRange = item.maxValue - item.minValue;
                        var pitchRange = 16384;
                        var step = valueRange / pitchRange;
                        var value = item.value;

                        if (value < item.minValue)
                        {
                            value = item.minValue;
                        }

                        if (value > item.maxValue)
                        {
                            value = item.maxValue;
                        }

                        var pitchValue = (value - item.minValue) / step;
                        int data2 = (int)(pitchValue / 127);
                        int data1 = (int)pitchValue % 127;

                        if (data2 > 127) { data2 = 127; };
                        if (data1 > 127) { data1 = 127; };

                        //Console.WriteLine(ListLrParameters.Count);

                        MidiSend(ChannelCommand.PitchWheel, 1, data1, data2);

                    }

                    if (item.name == "Exposure" && item.name == name)
                    {

                        var valueRange = item.maxValue - item.minValue;
                        var pitchRange = 16384;
                        var step = valueRange / pitchRange;
                        var value = item.value;

                        if (value < item.minValue)
                        {
                            value = item.minValue;
                        }

                        if (value > item.maxValue)
                        {
                            value = item.maxValue;
                        }

                        var pitchValue = (value - item.minValue) / step;
                        int data2 = (int)(pitchValue / 127);
                        int data1 = (int)pitchValue % 127;

                        if (data2 > 127) { data2 = 127; };
                        if (data1 > 127) { data1 = 127; };

                        //Console.WriteLine(ListLrParameters.Count);

                        MidiSend(ChannelCommand.PitchWheel, 2, data1, data2);

                    }

                    if (item.name == "Contrast" && item.name == name)
                    {

                        var valueRange = item.maxValue - item.minValue;
                        var pitchRange = 16384;
                        var step = valueRange / pitchRange;
                        var value = item.value;

                        if (value < item.minValue)
                        {
                            value = item.minValue;
                        }

                        if (value > item.maxValue)
                        {
                            value = item.maxValue;
                        }

                        var pitchValue = (value - item.minValue) / step;
                        int data2 = (int)(pitchValue / 127);
                        int data1 = (int)pitchValue % 127;

                        if (data2 > 127) { data2 = 127; };
                        if (data1 > 127) { data1 = 127; };

                        //Console.WriteLine(ListLrParameters.Count);

                        MidiSend(ChannelCommand.PitchWheel, 3, data1, data2);

                    }

                    if (item.name == "Highlights" && item.name == name)
                    {

                        var valueRange = item.maxValue - item.minValue;
                        var pitchRange = 16384;
                        var step = valueRange / pitchRange;
                        var value = item.value;

                        if (value < item.minValue)
                        {
                            value = item.minValue;
                        }

                        if (value > item.maxValue)
                        {
                            value = item.maxValue;
                        }

                        var pitchValue = (value - item.minValue) / step;
                        int data2 = (int)(pitchValue / 127);
                        int data1 = (int)pitchValue % 127;

                        if (data2 > 127) { data2 = 127; };
                        if (data1 > 127) { data1 = 127; };

                        //Console.WriteLine(ListLrParameters.Count);

                        MidiSend(ChannelCommand.PitchWheel, 4, data1, data2);

                    }

                    if (item.name == "Shadows" && item.name == name)
                    {

                        var valueRange = item.maxValue - item.minValue;
                        var pitchRange = 16384;
                        var step = valueRange / pitchRange;
                        var value = item.value;

                        if (value < item.minValue)
                        {
                            value = item.minValue;
                        }

                        if (value > item.maxValue)
                        {
                            value = item.maxValue;
                        }

                        var pitchValue = (value - item.minValue) / step;
                        int data2 = (int)(pitchValue / 127);
                        int data1 = (int)pitchValue % 127;

                        if (data2 > 127) { data2 = 127; };
                        if (data1 > 127) { data1 = 127; };

                       // Console.WriteLine(ListLrParameters.Count);

                        MidiSend(ChannelCommand.PitchWheel, 5, data1, data2);

                    }

                    if (item.name == "Whites" && item.name == name)
                    {

                        var valueRange = item.maxValue - item.minValue;
                        var pitchRange = 16384;
                        var step = valueRange / pitchRange;
                        var value = item.value;

                        if (value < item.minValue)
                        {
                            value = item.minValue;
                        }

                        if (value > item.maxValue)
                        {
                            value = item.maxValue;
                        }

                        var pitchValue = (value - item.minValue) / step;
                        int data2 = (int)(pitchValue / 127);
                        int data1 = (int)pitchValue % 127;

                        if (data2 > 127) { data2 = 127; };
                        if (data1 > 127) { data1 = 127; };

                       // Console.WriteLine(ListLrParameters.Count);

                        MidiSend(ChannelCommand.PitchWheel, 6, data1, data2);

                    }

                    if (item.name == "Blacks" && item.name == name)
                    {

                        var valueRange = item.maxValue - item.minValue;
                        var pitchRange = 16384;
                        var step = valueRange / pitchRange;
                        var value = item.value;

                        if (value < item.minValue)
                        {
                            value = item.minValue;
                        }

                        if (value > item.maxValue)
                        {
                            value = item.maxValue;
                        }

                        var pitchValue = (value - item.minValue) / step;
                        int data2 = (int)(pitchValue / 127);
                        int data1 = (int)pitchValue % 127;

                        if (data2 > 127) { data2 = 127; };
                        if (data1 > 127) { data1 = 127; };

                       // Console.WriteLine(ListLrParameters.Count);

                        MidiSend(ChannelCommand.PitchWheel, 7, data1, data2);
                    }
                }

                if (GlobalSettings.faderPage == 2)
                {
                    if (item.name == "Clarity" && item.name == name)
                    {

                        var valueRange = item.maxValue - item.minValue;
                        var pitchRange = 16384;
                        var step = valueRange / pitchRange;
                        var value = item.value;

                        if (value < item.minValue)
                        {
                            value = item.minValue;
                        }

                        if (value > item.maxValue)
                        {
                            value = item.maxValue;
                        }

                        var pitchValue = (value - item.minValue) / step;
                        int data2 = (int)(pitchValue / 127);
                        int data1 = (int)pitchValue % 127;

                        if (data2 > 127) { data2 = 127; };
                        if (data1 > 127) { data1 = 127; };

                      //  Console.WriteLine(ListLrParameters.Count);

                        MidiSend(ChannelCommand.PitchWheel, 0, data1, data2);

                    }

                    if (item.name == "Vibrance" && item.name == name)
                    {

                        var valueRange = item.maxValue - item.minValue;
                        var pitchRange = 16384;
                        var step = valueRange / pitchRange;
                        var value = item.value;

                        if (value < item.minValue)
                        {
                            value = item.minValue;
                        }

                        if (value > item.maxValue)
                        {
                            value = item.maxValue;
                        }

                        var pitchValue = (value - item.minValue) / step;
                        int data2 = (int)(pitchValue / 127);
                        int data1 = (int)pitchValue % 127;

                        if (data2 > 127) { data2 = 127; };
                        if (data1 > 127) { data1 = 127; };

                       // Console.WriteLine(ListLrParameters.Count);

                        MidiSend(ChannelCommand.PitchWheel, 1, data1, data2);

                    }

                    if (item.name == "Saturation" && item.name == name)
                    {

                        var valueRange = item.maxValue - item.minValue;
                        var pitchRange = 16384;
                        var step = valueRange / pitchRange;
                        var value = item.value;

                        if (value < item.minValue)
                        {
                            value = item.minValue;
                        }

                        if (value > item.maxValue)
                        {
                            value = item.maxValue;
                        }

                        var pitchValue = (value - item.minValue) / step;
                        int data2 = (int)(pitchValue / 127);
                        int data1 = (int)pitchValue % 127;

                        if (data2 > 127) { data2 = 127; };
                        if (data1 > 127) { data1 = 127; };

                       // Console.WriteLine(ListLrParameters.Count);

                        MidiSend(ChannelCommand.PitchWheel, 2, data1, data2);

                    }

                    if (item.name == "Dehaze" && item.name == name)
                    {

                        var valueRange = item.maxValue - item.minValue;
                        var pitchRange = 16384;
                        var step = valueRange / pitchRange;
                        var value = item.value;

                        if (value < item.minValue)
                        {
                            value = item.minValue;
                        }

                        if (value > item.maxValue)
                        {
                            value = item.maxValue;
                        }

                        var pitchValue = (value - item.minValue) / step;
                        int data2 = (int)(pitchValue / 127);
                        int data1 = (int)pitchValue % 127;

                        if (data2 > 127) { data2 = 127; };
                        if (data1 > 127) { data1 = 127; };

                      //  Console.WriteLine(ListLrParameters.Count);

                        MidiSend(ChannelCommand.PitchWheel, 3, data1, data2);

                    }
                }

                if (GlobalSettings.EncoderMenuPosition == 1)
                {
                    if (item.name == "ParametricHighlights" && item.name == name)
                    {
                        var valueRange = item.maxValue - item.minValue;
                        var encoderRange = 11;
                        var step = valueRange / encoderRange;
                        var value = item.value;

                        if (value < item.minValue)
                        {
                            value = item.minValue;
                        }

                        if (value > item.maxValue)
                        {
                            value = item.maxValue;
                        }

                        var pitchValue = (value - item.minValue) / step;
                        int data1 = (int)(pitchValue) + 1;

                        if (data1 > 11) { data1 = 11; };

                        MidiSend(ChannelCommand.Controller, 0, 48, data1);

                    }

                    if (item.name == "ParametricLights" && item.name == name)
                    {
                        var valueRange = item.maxValue - item.minValue;
                        var encoderRange = 11;
                        var step = valueRange / encoderRange;
                        var value = item.value;

                        if (value < item.minValue)
                        {
                            value = item.minValue;
                        }

                        if (value > item.maxValue)
                        {
                            value = item.maxValue;
                        }

                        var pitchValue = (value - item.minValue) / step;
                        int data1 = (int)(pitchValue) + 1;

                        if (data1 > 11) { data1 = 11; };

                        MidiSend(ChannelCommand.Controller, 0, 49, data1);

                    }

                    if (item.name == "ParametricDarks" && item.name == name)
                    {
                        var valueRange = item.maxValue - item.minValue;
                        var encoderRange = 11;
                        var step = valueRange / encoderRange;
                        var value = item.value;

                        if (value < item.minValue)
                        {
                            value = item.minValue;
                        }

                        if (value > item.maxValue)
                        {
                            value = item.maxValue;
                        }

                        var pitchValue = (value - item.minValue) / step;
                        int data1 = (int)(pitchValue) + 1;

                        if (data1 > 11) { data1 = 11; };

                        MidiSend(ChannelCommand.Controller, 0, 50, data1);

                    }

                    if (item.name == "ParametricShadows" && item.name == name)
                    {
                        var valueRange = item.maxValue - item.minValue;
                        var encoderRange = 11;
                        var step = valueRange / encoderRange;
                        var value = item.value;

                        if (value < item.minValue)
                        {
                            value = item.minValue;
                        }

                        if (value > item.maxValue)
                        {
                            value = item.maxValue;
                        }

                        var pitchValue = (value - item.minValue) / step;
                        int data1 = (int)(pitchValue) + 1;

                        if (data1 > 11) { data1 = 11; };

                        MidiSend(ChannelCommand.Controller, 0, 51, data1);

                    }

                    if (item.name == "ParametricShadowSplit" && item.name == name)
                    {
                        var valueRange = item.maxValue - item.minValue;
                        var encoderRange = 11;
                        var step = valueRange / encoderRange;
                        var value = item.value;

                        if (value < item.minValue)
                        {
                            value = item.minValue;
                        }

                        if (value > item.maxValue)
                        {
                            value = item.maxValue;
                        }

                        var pitchValue = (value - item.minValue) / step;
                        int data1 = (int)(pitchValue) + 1;

                        if (data1 > 11) { data1 = 11; };

                        MidiSend(ChannelCommand.Controller, 0, 52, data1);

                    }

                    if (item.name == "ParametricMidtoneSplit" && item.name == name)
                    {
                        var valueRange = item.maxValue - item.minValue;
                        var encoderRange = 11;
                        var step = valueRange / encoderRange;
                        var value = item.value;

                        if (value < item.minValue)
                        {
                            value = item.minValue;
                        }

                        if (value > item.maxValue)
                        {
                            value = item.maxValue;
                        }

                        var pitchValue = (value - item.minValue) / step;
                        int data1 = (int)(pitchValue) + 1;

                        if (data1 > 11) { data1 = 11; };

                        MidiSend(ChannelCommand.Controller, 0, 53, data1);

                    }

                    if (item.name == "ParametricHighlightSplit" && item.name == name)
                    {
                        var valueRange = item.maxValue - item.minValue;
                        var encoderRange = 11;
                        var step = valueRange / encoderRange;
                        var value = item.value;

                        if (value < item.minValue)
                        {
                            value = item.minValue;
                        }

                        if (value > item.maxValue)
                        {
                            value = item.maxValue;
                        }

                        var pitchValue = (value - item.minValue) / step;
                        int data1 = (int)(pitchValue) + 1;

                        if (data1 > 11) { data1 = 11; };

                        MidiSend(ChannelCommand.Controller, 0, 54, data1);

                    }
                }

                if (GlobalSettings.EncoderMenuPosition == 2)
                {
                    EncoderParameter(name, item, "SaturationAdjustmentRed", 48);
                    EncoderParameter(name, item, "SaturationAdjustmentOrange", 49);
                    EncoderParameter(name, item, "SaturationAdjustmentYellow", 50);
                    EncoderParameter(name, item, "SaturationAdjustmentGreen", 51);
                    EncoderParameter(name, item, "SaturationAdjustmentAqua", 52);
                    EncoderParameter(name, item, "SaturationAdjustmentBlue", 53);
                    EncoderParameter(name, item, "SaturationAdjustmentPurple", 54);
                    EncoderParameter(name, item, "SaturationAdjustmentMagenta", 55);

                }

                if (GlobalSettings.EncoderMenuPosition == 3)
                {
                    EncoderParameter(name, item, "HueAdjustmentRed", 48);
                    EncoderParameter(name, item, "HueAdjustmentOrange", 49);
                    EncoderParameter(name, item, "HueAdjustmentYellow", 50);
                    EncoderParameter(name, item, "HueAdjustmentGreen", 51);
                    EncoderParameter(name, item, "HueAdjustmentAqua", 52);
                    EncoderParameter(name, item, "HueAdjustmentBlue", 53);
                    EncoderParameter(name, item, "HueAdjustmentPurple", 54);
                    EncoderParameter(name, item, "HueAdjustmentMagenta", 55);

                }

                if (GlobalSettings.EncoderMenuPosition == 4)
                {
                    EncoderParameter(name, item, "LuminanceAdjustmentRed", 48);
                    EncoderParameter(name, item, "LuminanceAdjustmentOrange", 49);
                    EncoderParameter(name, item, "LuminanceAdjustmentYellow", 50);
                    EncoderParameter(name, item, "LuminanceAdjustmentGreen", 51);
                    EncoderParameter(name, item, "LuminanceAdjustmentAqua", 52);
                    EncoderParameter(name, item, "LuminanceAdjustmentBlue", 53);
                    EncoderParameter(name, item, "LuminanceAdjustmentPurple", 54);
                    EncoderParameter(name, item, "LuminanceAdjustmentMagenta", 55);
                }

                if (GlobalSettings.EncoderMenuPosition == 5)
                {
                    EncoderParameter(name, item, "PostCropVignetteStyle", 48);
                    EncoderParameter(name, item, "PostCropVignetteAmount", 49);
                    EncoderParameter(name, item, "PostCropVignetteMidpoint", 50);
                    EncoderParameter(name, item, "PostCropVignetteFeather", 51);
                    EncoderParameter(name, item, "PostCropVignetteRoundness", 52);
                    EncoderParameter(name, item, "PostCropVignetteHighlightContrast", 53);
                    //EncoderParameter(name, item, "LuminanceAdjustmentPurple", 54);
                    //EncoderParameter(name, item, "CropBottom", 55);
                }
            }
        }

        private static void TurnOffEncoderDisplay(EncodersMidiNumber encodersMidiNumber)
        {
            MidiSend(ChannelCommand.Controller, 0, (int)encodersMidiNumber, 0);
        }

        private static void EncoderParameter(string name, Parameter item, string masterName, byte data)
        {
            if (item.name == masterName && item.name == name)
            {
                var valueRange = item.maxValue - item.minValue;
                var encoderRange = 11;
                var step = valueRange / encoderRange;
                var value = item.value;

                if (value < item.minValue)
                {
                    value = item.minValue;
                }

                if (value > item.maxValue)
                {
                    value = item.maxValue;
                }

                var pitchValue = (value - item.minValue) / step;
                int data1 = (int)(pitchValue) + 1;

                if (data1 > 11) { data1 = 11; };

                MidiSend(ChannelCommand.Controller, 0, data, data1);
            }
        }

        public static void MidiSend(ChannelCommand channelCommand, int midiChannel, int data1, int data2)
        {
            GlobalSettings.queueMidi.Enqueue(new Models.MidiToSendModel(channelCommand, midiChannel, data1, data2));

            rstEvent.Set();
        }

        public static void SysExSend(SysExMessage msg)
        {
            GlobalSettings.queueMidiSysex.Enqueue(new Models.SysexModel(msg));

            rstEvent.Set();
        }

        public static void ThreadSendingMidi()
        {
            while (true)
            {
                rstEvent.WaitOne();

                while (GlobalSettings.queueMidi.Count() > 0)
                {
                    var mod = GlobalSettings.queueMidi.Dequeue();

                    var sendingMidi = GlobalSettings.sendingMidi;

                    if (!sendingMidi)
                    {
                        sendingMidi = true;

                        using (OutputDevice outDevice = new OutputDevice(1))
                        {
                            ChannelMessageBuilder builder = new ChannelMessageBuilder();

                            builder.Command = mod.channelCommand;
                            builder.MidiChannel = mod.B1;
                            builder.Data1 = mod.B2;
                            builder.Data2 = mod.B3;
                            builder.Build();

                            outDevice.Send(builder.Result);

                            //Console.WriteLine(midiChannel + data1 + data2);
                        }

                        sendingMidi = false;
                    }
                }

                while (GlobalSettings.queueMidiSysex.Count() > 0)
                {
                    var model = GlobalSettings.queueMidiSysex.Dequeue();

                    using (OutputDevice outDevice = new OutputDevice(1))
                    {


                        outDevice.Send(model.msg);

                        //Console.WriteLine(midiChannel + data1 + data2);
                    }
                }

                rstEvent.Reset();
            }
        }
    }

    public enum EncodersMidiNumber
    {
        Encoder1 = 48,
        Encoder2 = 49,
        Encoder3 = 50,
        Encoder4 = 51,
        Encoder5 = 52,
        Encoder6 = 53,
        Encoder7 = 54,
        Encoder8 = 55
    }
}
