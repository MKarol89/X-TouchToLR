using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTouchToLr.Engine;

namespace XTouchToLr.Data
{
    public static class LrParameters
    {
        public static List<Parameter> listParameter = new List<Parameter>();

        public static void Start()
        {
            listParameter.Add(new Parameter("Temperature", 10000f, 3000f));
            listParameter.Add(new Parameter("Tint", 150f, -150f));
            listParameter.Add(new Parameter("Exposure", 5f, -5f));
            listParameter.Add(new Parameter("Contrast", 100f, -100f));
            listParameter.Add(new Parameter("Highlights", 100f, -100f));
            listParameter.Add(new Parameter("Shadows", 100f, -100f));
            listParameter.Add(new Parameter("Whites", 100f, -100f));
            listParameter.Add(new Parameter("Blacks", 100f, -100f));
            listParameter.Add(new Parameter("Clarity", 100f, -100f));
            listParameter.Add(new Parameter("Vibrance", 100f, -100f));
            listParameter.Add(new Parameter("Saturation", 100f, -100f));
            listParameter.Add(new Parameter("Dehaze", 100f, -100f));

            listParameter.Add(new Parameter("ParametricHighlights", 100f, -100f));
            listParameter.Add(new Parameter("ParametricLights", 100f, -100f));
            listParameter.Add(new Parameter("ParametricDarks", 100f, -100f));
            listParameter.Add(new Parameter("ParametricShadows", 100f, -100f));

            listParameter.Add(new Parameter("ParametricShadowSplit", 70, 10f));
            listParameter.Add(new Parameter("ParametricMidtoneSplit", 80, 20f));
            listParameter.Add(new Parameter("ParametricHighlightSplit", 90f, 30f));

            listParameter.Add(new Parameter("SaturationAdjustmentRed", 100f, -100f));
            listParameter.Add(new Parameter("SaturationAdjustmentOrange", 100f, -100f));
            listParameter.Add(new Parameter("SaturationAdjustmentYellow", 100f, -100f));
            listParameter.Add(new Parameter("SaturationAdjustmentGreen", 100f, -100f));
            listParameter.Add(new Parameter("SaturationAdjustmentAqua", 100f, -100f));
            listParameter.Add(new Parameter("SaturationAdjustmentBlue", 100f, -100f));
            listParameter.Add(new Parameter("SaturationAdjustmentPurple", 100f, -100f));
            listParameter.Add(new Parameter("SaturationAdjustmentMagenta", 100f, -100f));

            listParameter.Add(new Parameter("HueAdjustmentRed", 100f, -100f));
            listParameter.Add(new Parameter("HueAdjustmentOrange", 100f, -100f));
            listParameter.Add(new Parameter("HueAdjustmentYellow", 100f, -100f));
            listParameter.Add(new Parameter("HueAdjustmentGreen", 100f, -100f));
            listParameter.Add(new Parameter("HueAdjustmentAqua", 100f, -100f));
            listParameter.Add(new Parameter("HueAdjustmentBlue", 100f, -100f));
            listParameter.Add(new Parameter("HueAdjustmentPurple", 100f, -100f));
            listParameter.Add(new Parameter("HueAdjustmentMagenta", 100f, -100f));

            listParameter.Add(new Parameter("LuminanceAdjustmentRed", 100f, -100f));
            listParameter.Add(new Parameter("LuminanceAdjustmentOrange", 100f, -100f));
            listParameter.Add(new Parameter("LuminanceAdjustmentYellow", 100f, -100f));
            listParameter.Add(new Parameter("LuminanceAdjustmentGreen", 100f, -100f));
            listParameter.Add(new Parameter("LuminanceAdjustmentAqua", 100f, -100f));
            listParameter.Add(new Parameter("LuminanceAdjustmentBlue", 100f, -100f));
            listParameter.Add(new Parameter("LuminanceAdjustmentPurple", 100f, -100f));
            listParameter.Add(new Parameter("LuminanceAdjustmentMagenta", 100f, -100f));

            listParameter.Add(new Parameter("PostCropVignetteAmount", 100f, -100f));
            listParameter.Add(new Parameter("PostCropVignetteMidpoint", 100f, 0f));
            listParameter.Add(new Parameter("PostCropVignetteFeather", 100f, 0f));
            listParameter.Add(new Parameter("PostCropVignetteRoundness", 100f, -100f));
            listParameter.Add(new Parameter("PostCropVignetteHighlightContrast", 100f, 0f));

            listParameter.Add(new Parameter("straightenAngle", 45f, -45f));
            listParameter.Add(new Parameter("CropLeft", 1f, 0f));
            listParameter.Add(new Parameter("CropTop", 1f, 0f));
            listParameter.Add(new Parameter("CropRight", 1f, 0f));
            listParameter.Add(new Parameter("CropBottom", 1f, 0f));

        }
        
        public static void SetParameters(string[] model)
        {
            foreach (var item in model)
            {
                if (item.Length > 0)
                {
                    var split = item.Split(',');

                    if (split.Count() > 1)
                    {
                        string key = split[0].Replace("key = ", "");
                        string value = split[1].Replace("value = ", "");
                        value = value.Replace('.', ',');

                        float result = 0;

                        var tryParseResult = float.TryParse(value, out result);

                        var lrParameter = listParameter.Where(x => x.name == key).FirstOrDefault();

                        if (lrParameter != null && tryParseResult)
                        {
                            lrParameter.value = float.Parse(value);

                            SendToMidiDevice.Send(lrParameter.name);
                        }
                    }
                }
            }
        }

        public static void SetParameterByMidi(byte b0, byte b1, byte b2)
        {
            ////////////////////// ENCODERS ROT.////////////////////////////

            if (GlobalSettings.EncoderMenuPosition == 1)
            {
                if (b0 == 176 && b1 == 16)
                {
                    if (b2 > 0 && b2 < 65)
                    {
                        var model = listParameter.Where(x => x.name == "ParametricHighlights").FirstOrDefault();

                        model.value += b2;

                        if (model.value > model.maxValue)
                        {
                            model.value = model.maxValue;
                        }
                    }
                    else
                    {
                        var model = listParameter.Where(x => x.name == "ParametricHighlights").FirstOrDefault();

                        model.value -= (b2 - 64);

                        if (model.value < model.minValue)
                        {
                            model.value = model.minValue;
                        }
                    }

                    SendToLr.Send("ParametricHighlights");

                }                   //Encoder 1 - ParametricHighlights

                if (b0 == 144 && b1 == 32 && b2 == 127)
                {
                    SendToLr.SendReset("ParametricHighlights");
                }      //Encoder 1 RESET

                if (b0 == 176 && b1 == 17)
                {
                    if (b2 > 0 && b2 < 65)
                    {
                        var model = listParameter.Where(x => x.name == "ParametricLights").FirstOrDefault();

                        model.value += b2;

                        if (model.value > model.maxValue)
                        {
                            model.value = model.maxValue;
                        }
                    }
                    else
                    {
                        var model = listParameter.Where(x => x.name == "ParametricLights").FirstOrDefault();

                        model.value -= (b2 - 64);

                        if (model.value < model.minValue)
                        {
                            model.value = model.minValue;
                        }
                    }

                    SendToLr.Send("ParametricLights");

                }                   //Encoder 2 - ParametricLights

                if (b0 == 144 && b1 == 33 && b2 == 127)
                {
                    SendToLr.SendReset("ParametricLights");
                }      //Encoder 2 RESET

                if (b0 == 176 && b1 == 18)
                {
                    if (b2 > 0 && b2 < 65)
                    {
                        var model = listParameter.Where(x => x.name == "ParametricDarks").FirstOrDefault();

                        model.value += b2;

                        if (model.value > model.maxValue)
                        {
                            model.value = model.maxValue;
                        }
                    }
                    else
                    {
                        var model = listParameter.Where(x => x.name == "ParametricDarks").FirstOrDefault();

                        model.value -= (b2 - 64);

                        if (model.value < model.minValue)
                        {
                            model.value = model.minValue;
                        }
                    }

                    SendToLr.Send("ParametricDarks");

                }                   //Encoder 3 - ParametricDarks

                if (b0 == 144 && b1 == 34 && b2 == 127)
                {
                    SendToLr.SendReset("ParametricDarks");
                }      //Encoder 3 RESET

                if (b0 == 176 && b1 == 19)
                {
                    if (b2 > 0 && b2 < 65)
                    {
                        var model = listParameter.Where(x => x.name == "ParametricShadows").FirstOrDefault();

                        model.value += b2;

                        if (model.value > model.maxValue)
                        {
                            model.value = model.maxValue;
                        }
                    }
                    else
                    {
                        var model = listParameter.Where(x => x.name == "ParametricShadows").FirstOrDefault();

                        model.value -= (b2 - 64);

                        if (model.value < model.minValue)
                        {
                            model.value = model.minValue;
                        }
                    }

                    SendToLr.Send("ParametricShadows");

                }                   //Encoder 4 - ParametricShadows

                if (b0 == 144 && b1 == 35 && b2 == 127)
                {
                    SendToLr.SendReset("ParametricShadows");
                }      //Encoder 4 RESET

                if (b0 == 176 && b1 == 20)
                {
                    if (b2 > 0 && b2 < 65)
                    {
                        var model = listParameter.Where(x => x.name == "ParametricShadowSplit").FirstOrDefault();

                        model.value += b2;

                        if (model.value > model.maxValue)
                        {
                            model.value = model.maxValue;
                        }
                    }
                    else
                    {
                        var model = listParameter.Where(x => x.name == "ParametricShadowSplit").FirstOrDefault();

                        model.value -= (b2 - 64);

                        if (model.value < model.minValue)
                        {
                            model.value = model.minValue;
                        }
                    }

                    SendToLr.Send("ParametricShadowSplit");

                }                   //Encoder 5 - ParametricShadowSplit

                if (b0 == 144 && b1 == 36 && b2 == 127)
                {
                    SendToLr.SendReset("ParametricShadows");
                }      //Encoder 5 RESET

                if (b0 == 176 && b1 == 21)
                {
                    if (b2 > 0 && b2 < 65)
                    {
                        var model = listParameter.Where(x => x.name == "ParametricMidtoneSplit").FirstOrDefault();

                        model.value += b2;

                        if (model.value > model.maxValue)
                        {
                            model.value = model.maxValue;
                        }
                    }
                    else
                    {
                        var model = listParameter.Where(x => x.name == "ParametricMidtoneSplit").FirstOrDefault();

                        model.value -= (b2 - 64);

                        if (model.value < model.minValue)
                        {
                            model.value = model.minValue;
                        }
                    }

                    SendToLr.Send("ParametricMidtoneSplit");

                }                   //Encoder 6 - ParametricMidtoneSplit

                if (b0 == 144 && b1 == 37 && b2 == 127)
                {
                    SendToLr.SendReset("ParametricMidtoneSplit");
                }      //Encoder 6 RESET

                if (b0 == 176 && b1 == 22)
                {
                    if (b2 > 0 && b2 < 65)
                    {
                        var model = listParameter.Where(x => x.name == "ParametricHighlightSplit").FirstOrDefault();

                        model.value += b2;

                        if (model.value > model.maxValue)
                        {
                            model.value = model.maxValue;
                        }
                    }
                    else
                    {
                        var model = listParameter.Where(x => x.name == "ParametricHighlightSplit").FirstOrDefault();

                        model.value -= (b2 - 64);

                        if (model.value < model.minValue)
                        {
                            model.value = model.minValue;
                        }
                    }

                    SendToLr.Send("ParametricHighlightSplit");

                }                   //Encoder 7 - ParametricMidtoneSplit

                if (b0 == 144 && b1 == 38 && b2 == 127)
                {
                    SendToLr.SendReset("ParametricHighlightSplit");
                }      //Encoder 7 RESET
            }

            if (GlobalSettings.EncoderMenuPosition == 2)
            {
                EncoderMethod(b0, b1, b2, 176, 16, "SaturationAdjustmentRed", 144, 32, 127);
                EncoderMethod(b0, b1, b2, 176, 17, "SaturationAdjustmentOrange", 144, 33, 127);
                EncoderMethod(b0, b1, b2, 176, 18, "SaturationAdjustmentYellow", 144, 34, 127);
                EncoderMethod(b0, b1, b2, 176, 19, "SaturationAdjustmentGreen", 144, 35, 127);
                EncoderMethod(b0, b1, b2, 176, 20, "SaturationAdjustmentAqua", 144, 36, 127);
                EncoderMethod(b0, b1, b2, 176, 21, "SaturationAdjustmentBlue", 144, 37, 127);
                EncoderMethod(b0, b1, b2, 176, 22, "SaturationAdjustmentPurple", 144, 38, 127);
                EncoderMethod(b0, b1, b2, 176, 23, "SaturationAdjustmentMagenta", 144, 39, 127);

            }

            if (GlobalSettings.EncoderMenuPosition == 3)
            {
                EncoderMethod(b0, b1, b2, 176, 16, "HueAdjustmentRed", 144, 32, 127);
                EncoderMethod(b0, b1, b2, 176, 17, "HueAdjustmentOrange", 144, 33, 127);
                EncoderMethod(b0, b1, b2, 176, 18, "HueAdjustmentYellow", 144, 34, 127);
                EncoderMethod(b0, b1, b2, 176, 19, "HueAdjustmentGreen", 144, 35, 127);
                EncoderMethod(b0, b1, b2, 176, 20, "HueAdjustmentAqua", 144, 36, 127);
                EncoderMethod(b0, b1, b2, 176, 21, "HueAdjustmentBlue", 144, 37, 127);
                EncoderMethod(b0, b1, b2, 176, 22, "HueAdjustmentPurple", 144, 38, 127);
                EncoderMethod(b0, b1, b2, 176, 23, "HueAdjustmentMagenta", 144, 39, 127);

            }

            if (GlobalSettings.EncoderMenuPosition == 4)
            {
                EncoderMethod(b0, b1, b2, 176, 16, "LuminanceAdjustmentRed", 144, 32, 127);
                EncoderMethod(b0, b1, b2, 176, 17, "LuminanceAdjustmentOrange", 144, 33, 127);
                EncoderMethod(b0, b1, b2, 176, 18, "LuminanceAdjustmentYellow", 144, 34, 127);
                EncoderMethod(b0, b1, b2, 176, 19, "LuminanceAdjustmentGreen", 144, 35, 127);
                EncoderMethod(b0, b1, b2, 176, 20, "LuminanceAdjustmentAqua", 144, 36, 127);
                EncoderMethod(b0, b1, b2, 176, 21, "LuminanceAdjustmentBlue", 144, 37, 127);
                EncoderMethod(b0, b1, b2, 176, 22, "LuminanceAdjustmentPurple", 144, 38, 127);
                EncoderMethod(b0, b1, b2, 176, 23, "LuminanceAdjustmentMagenta", 144, 39, 127);

            }

            if (GlobalSettings.EncoderMenuPosition == 5)
            {
                //EncoderMethod(b0, b1, b2, 176, 16, "LuminanceAdjustmentRed", 144, 32, 127);
                EncoderMethod(b0, b1, b2, 176, 17, "PostCropVignetteAmount", 144, 33, 127);
                EncoderMethod(b0, b1, b2, 176, 18, "PostCropVignetteMidpoint", 144, 34, 127);
                EncoderMethod(b0, b1, b2, 176, 19, "PostCropVignetteFeather", 144, 35, 127);
                EncoderMethod(b0, b1, b2, 176, 20, "PostCropVignetteRoundness", 144, 36, 127);
                EncoderMethod(b0, b1, b2, 176, 21, "PostCropVignetteHighlightContrast", 144, 37, 127);
                //EncoderMethod(b0, b1, b2, 176, 22, "LuminanceAdjustmentPurple", 144, 38, 127);
                //EncoderMethod(b0, b1, b2, 176, 23, "CropBottom", 144, 39, 127);

            }

            //////////////////////ENCODERS MENU/////////////////////////////

            if (b0 == 144 && b1 == 40 && b2 == 127)
            {
                GlobalSettings.EncoderMenuPosition = 1;

                SendToMidiDevice.Send("Encoder");

                Display.Send(1, "High.  ", "Tone   ");
                Display.Send(2, "Lights ", "Tone   ");
                Display.Send(3, "Darks  ", "Tone   ");
                Display.Send(4, "Shadow ", "Tone   ");
                Display.Send(5, "Shadow ", "Split  ");
                Display.Send(6, "Midtone", "Split  ");
                Display.Send(7, "High.  ", "Split  ");
                Display.Send(8, "       ", "       ");



            }   //MENU 1

            if (b0 == 144 && b1 == 42 && b2 == 127)
            {
                GlobalSettings.EncoderMenuPosition = 2;

                SendToMidiDevice.Send("Encoder");

                Display.Send(1, "Red    ", "Satur. ");
                Display.Send(2, "Oragne ", "Satur. ");
                Display.Send(3, "Yellow ", "Satur. ");
                Display.Send(4, "Green  ", "Satur. ");
                Display.Send(5, "Aqua   ", "Satur. ");
                Display.Send(6, "Blue   ", "Satur. ");
                Display.Send(7, "Purple ", "Satur. ");
                Display.Send(8, "Magneta", "Satur. ");

            }   //MENU 2

            if (b0 == 144 && b1 == 44 && b2 == 127)
            {
                GlobalSettings.EncoderMenuPosition = 3;

                SendToMidiDevice.Send("Encoder");

                Display.Send(1, "Red    ", "Hue    ");
                Display.Send(2, "Oragne ", "Hue    ");
                Display.Send(3, "Yellow ", "Hue    ");
                Display.Send(4, "Green  ", "Hue    ");
                Display.Send(5, "Aqua   ", "Hue    ");
                Display.Send(6, "Blue   ", "Hue    ");
                Display.Send(7, "Purple ", "Hue    ");
                Display.Send(8, "Magneta", "Hue    ");
            }  //MENU 3

            if (b0 == 144 && b1 == 41 && b2 == 127)
            {
                GlobalSettings.EncoderMenuPosition = 4;

                SendToMidiDevice.Send("Encoder");

                Display.Send(1, "Red    ", "Lum.   ");
                Display.Send(2, "Oragne ", "Lum.   ");
                Display.Send(3, "Yellow ", "Lum.   ");
                Display.Send(4, "Green  ", "Lum.   ");
                Display.Send(5, "Aqua   ", "Lum.   ");
                Display.Send(6, "Blue   ", "Lum.   ");
                Display.Send(7, "Purple ", "Lum.   ");
                Display.Send(8, "Magneta", "Lum.   ");
            }  //MENU 4

            if (b0 == 144 && b1 == 43 && b2 == 127)
            {
                GlobalSettings.EncoderMenuPosition = 5;

                SendToMidiDevice.Send("Encoder");

                Display.Send(1, "Vignett", "Style  ");
                Display.Send(2, "Vignett", "Amount ");
                Display.Send(3, "Vignett", "MidPoi ");
                Display.Send(4, "Vignett", "Feather");
                Display.Send(5, "Vignett", "Roudnes");
                Display.Send(6, "Vignett", "H.Contr");
                Display.Send(7, "       ", "       ");
                Display.Send(8, "       ", "       ");

            }  //MENU 5

            if (b0 == 144 && b1 == 45 && b2 == 127)
            {
                GlobalSettings.EncoderMenuPosition = 6;

                SendToMidiDevice.Send("Encoder");
            }  //MENU 6


            //////////////////////FADER BANK///////////////////////

            if (b0 == 144 && b1 == 47 && b2 == 127)
            {
                GlobalSettings.faderPage = 2;

                SendToMidiDevice.Send("Clarity");
                SendToMidiDevice.Send("Vibrance");
                SendToMidiDevice.Send("Saturation");
                SendToMidiDevice.Send("Dehaze");
                SendToMidiDevice.Send("Null");


            }       //FADER PAGE 2

            if (b0 == 144 && b1 == 46 && b2 == 127)
            {
                GlobalSettings.faderPage = 1;

                SendToMidiDevice.Send("Temperature");
                SendToMidiDevice.Send("Tint");
                SendToMidiDevice.Send("Exposure");
                SendToMidiDevice.Send("Contrast");
                SendToMidiDevice.Send("Highlights");
                SendToMidiDevice.Send("Shadows");
                SendToMidiDevice.Send("Whites");
                SendToMidiDevice.Send("Blacks");
            }       //FADER PAGE 1

            ///////////////////// ARROWS /////////////////////////

            if (b0 == 144 && b1 == 99 && b2 == 127)
            {
                SendToLr.Send("select", "next");
            }       //NEXT PHOTO

            if (b0 == 144 && b1 == 98 && b2 == 127)
            {
                SendToLr.Send("select", "previous");
            }       //PREVIOUS PHOTO

            //////////////////// RESET ALL ////////////////////////

            if (b0 == 144 && b1 == 50 && b2 == 127)
            {
                SendToLr.Send("select", "resetall");
            }       //NEXT PHOTO

            //////////////////// SAVE BANKS ///////////////////////

            if (b0 == 144 && b1 == 95 && b2 == 127)
            {
                GlobalSettings.IsSaving = true;

                SendToMidiDevice.MidiSend(ChannelCommand.NoteOn, 0, 95, 127);
            }   //SET SAVE

            if (b0 == 144 && b1 == 80 && b2 == 127)
            {
                if (GlobalSettings.IsSaving == true)
                {
                    SaveReadParameters.Save(1);

                    GlobalSettings.IsSaving = false;

                    SendToMidiDevice.MidiSend(ChannelCommand.NoteOn, 0, 95, 0);
                    SendToMidiDevice.MidiSend(ChannelCommand.NoteOn, 0, 80, 127);
                }
                else
                {
                    SaveReadParameters.Read(1);
                }
            }   //BANK 1

            if (b0 == 144 && b1 == 81 && b2 == 127)
            {
                if (GlobalSettings.IsSaving == true)
                {
                    SaveReadParameters.Save(2);

                    GlobalSettings.IsSaving = false;

                    SendToMidiDevice.MidiSend(ChannelCommand.NoteOn, 0, 95, 0);
                    SendToMidiDevice.MidiSend(ChannelCommand.NoteOn, 0, 81, 127);

                }
                else
                {
                    SaveReadParameters.Read(2);
                }
            }   //BANK 2

            if (b0 == 144 && b1 == 82 && b2 == 127)
            {
                if (GlobalSettings.IsSaving == true)
                {
                    SaveReadParameters.Save(3);

                    GlobalSettings.IsSaving = false;

                    SendToMidiDevice.MidiSend(ChannelCommand.NoteOn, 0, 95, 0);
                    SendToMidiDevice.MidiSend(ChannelCommand.NoteOn, 0, 82, 127);

                }
                else
                {
                    SaveReadParameters.Read(3);
                }
            }   //BANK 3

            if (b0 == 144 && b1 == 83 && b2 == 127)
            {
                if (GlobalSettings.IsSaving == true)
                {
                    SaveReadParameters.Save(4);

                    GlobalSettings.IsSaving = false;

                    SendToMidiDevice.MidiSend(ChannelCommand.NoteOn, 0, 95, 0);
                    SendToMidiDevice.MidiSend(ChannelCommand.NoteOn, 0, 83, 127);

                }
                else
                {
                    SaveReadParameters.Read(4);
                }
            }   //BANK 4

            /////////////////// UNDO - REDO ///////////////////

            if (b0 == 144 && b1 == 91 && b2 == 127)
            {
                SendToLr.Send("select", "undo");

                SendToMidiDevice.MidiSend(ChannelCommand.NoteOn, 0, 91, 127);
            }   //UNDO

            if (b0 == 144 && b1 == 91 && b2 == 0)
            {
                SendToMidiDevice.MidiSend(ChannelCommand.NoteOn, 0, 91, 0);
            }     //UNDO BUTTON LIGHT OFF

            if (b0 == 144 && b1 == 92 && b2 == 127)         
            {
                SendToLr.Send("select", "redo");

                SendToMidiDevice.MidiSend(ChannelCommand.NoteOn, 0, 92, 127);
            }   //REDO

            if (b0 == 144 && b1 == 92 && b2 == 0)          
            {
                SendToMidiDevice.MidiSend(ChannelCommand.NoteOn, 0, 92, 0);
            }     //REDO BUTTON LIGHT OFF

            ////////////////////////////  FADERS  ////////////////////////////////////

            if (GlobalSettings.faderPage == 1)
            {
                if (b0 == 224)
                {
                    var model = listParameter.Where(x => x.name == "Temperature").FirstOrDefault();

                    var midiValue = ((b2 + 1) * 128) + (b1 + 1);

                    var valueRange = model.maxValue - model.minValue;
                    var pitchRange = 16384;
                    var step = valueRange / pitchRange;

                    model.value = midiValue * step + model.minValue;

                    if (model.value > model.maxValue)
                    {
                        model.value = model.maxValue;
                    }

                    SendToLr.Send("Temperature");
                }

                if (b0 == 225)
                {
                    var model = listParameter.Where(x => x.name == "Tint").FirstOrDefault();

                    var midiValue = ((b2 + 1) * 128) + (b1 + 1);

                    var valueRange = model.maxValue - model.minValue;
                    var pitchRange = 16384;
                    var step = valueRange / pitchRange;

                    model.value = midiValue * step + model.minValue;

                    if (model.value > model.maxValue)
                    {
                        model.value = model.maxValue;
                    }

                    SendToLr.Send("Tint");
                }

                if (b0 == 226)
                {
                    var model = listParameter.Where(x => x.name == "Exposure").FirstOrDefault();

                    var midiValue = ((b2 + 1) * 128) + (b1 + 1);

                    var valueRange = model.maxValue - model.minValue;
                    var pitchRange = 16384;
                    var step = valueRange / pitchRange;

                    model.value = midiValue * step + model.minValue;

                    if (model.value > model.maxValue)
                    {
                        model.value = model.maxValue;
                    }

                    SendToLr.Send("Exposure");
                }

                if (b0 == 227)
                {
                    var model = listParameter.Where(x => x.name == "Contrast").FirstOrDefault();

                    var midiValue = ((b2 + 1) * 128) + (b1 + 1);

                    var valueRange = model.maxValue - model.minValue;
                    var pitchRange = 16384;
                    var step = valueRange / pitchRange;

                    model.value = midiValue * step + model.minValue;

                    if (model.value > model.maxValue)
                    {
                        model.value = model.maxValue;
                    }

                    SendToLr.Send("Contrast");
                }

                if (b0 == 228)
                {
                    var model = listParameter.Where(x => x.name == "Highlights").FirstOrDefault();

                    var midiValue = ((b2 + 1) * 128) + (b1 + 1);

                    var valueRange = model.maxValue - model.minValue;
                    var pitchRange = 16384;
                    var step = valueRange / pitchRange;

                    model.value = midiValue * step + model.minValue;

                    if (model.value > model.maxValue)
                    {
                        model.value = model.maxValue;
                    }

                    SendToLr.Send("Highlights");
                }

                if (b0 == 229)
                {
                    var model = listParameter.Where(x => x.name == "Shadows").FirstOrDefault();

                    var midiValue = ((b2 + 1) * 128) + (b1 + 1);

                    var valueRange = model.maxValue - model.minValue;
                    var pitchRange = 16384;
                    var step = valueRange / pitchRange;

                    model.value = midiValue * step + model.minValue;

                    if (model.value > model.maxValue)
                    {
                        model.value = model.maxValue;
                    }

                    SendToLr.Send("Shadows");
                }

                if (b0 == 230)
                {
                    var model = listParameter.Where(x => x.name == "Whites").FirstOrDefault();

                    var midiValue = ((b2 + 1) * 128) + (b1 + 1);

                    var valueRange = model.maxValue - model.minValue;
                    var pitchRange = 16384;
                    var step = valueRange / pitchRange;

                    model.value = midiValue * step + model.minValue;

                    if (model.value > model.maxValue)
                    {
                        model.value = model.maxValue;
                    }

                    SendToLr.Send("Whites");
                }

                if (b0 == 231)
                {
                    var model = listParameter.Where(x => x.name == "Blacks").FirstOrDefault();

                    var midiValue = ((b2 + 1) * 128) + (b1 + 1);

                    var valueRange = model.maxValue - model.minValue;
                    var pitchRange = 16384;
                    var step = valueRange / pitchRange;

                    model.value = midiValue * step + model.minValue;

                    if (model.value > model.maxValue)
                    {
                        model.value = model.maxValue;
                    }

                    SendToLr.Send("Blacks");
                }
            }

            if (GlobalSettings.faderPage == 2)
            {
                if (b0 == 224)
                {
                    var model = listParameter.Where(x => x.name == "Clarity").FirstOrDefault();

                    var midiValue = ((b2 + 1) * 128) + (b1 + 1);

                    var valueRange = model.maxValue - model.minValue;
                    var pitchRange = 16384;
                    var step = valueRange / pitchRange;

                    model.value = midiValue * step + model.minValue;

                    if (model.value > model.maxValue)
                    {
                        model.value = model.maxValue;
                    }

                    SendToLr.Send("Clarity");
                }

                if (b0 == 225)
                {
                    var model = listParameter.Where(x => x.name == "Vibrance").FirstOrDefault();

                    var midiValue = ((b2 + 1) * 128) + (b1 + 1);

                    var valueRange = model.maxValue - model.minValue;
                    var pitchRange = 16384;
                    var step = valueRange / pitchRange;

                    model.value = midiValue * step + model.minValue;

                    if (model.value > model.maxValue)
                    {
                        model.value = model.maxValue;
                    }

                    SendToLr.Send("Vibrance");
                }

                if (b0 == 226)
                {
                    var model = listParameter.Where(x => x.name == "Saturation").FirstOrDefault();

                    var midiValue = ((b2 + 1) * 128) + (b1 + 1);

                    var valueRange = model.maxValue - model.minValue;
                    var pitchRange = 16384;
                    var step = valueRange / pitchRange;

                    model.value = midiValue * step + model.minValue;

                    if (model.value > model.maxValue)
                    {
                        model.value = model.maxValue;
                    }

                    SendToLr.Send("Saturation");
                }

                if (b0 == 227)
                {
                    var model = listParameter.Where(x => x.name == "Dehaze").FirstOrDefault();

                    var midiValue = ((b2 + 1) * 128) + (b1 + 1);

                    var valueRange = model.maxValue - model.minValue;
                    var pitchRange = 16384;
                    var step = valueRange / pitchRange;

                    model.value = midiValue * step + model.minValue;

                    if (model.value > model.maxValue)
                    {
                        model.value = model.maxValue;
                    }

                    SendToLr.Send("Dehaze");
                }
            }

            /////////////////// CROP ///////////////////

            if (b0 == 144 && b1 == 86 && b2 == 127)
            {
                GlobalSettings.BigEncoderOption = BigEncoderButtonFunctionMidiValue.straightenAngle;
                BigEncoderDisplayButtons();
            }   //ROTATE

            if (b0 == 144 && b1 == 87 && b2 == 127)
            {
                GlobalSettings.BigEncoderOption = BigEncoderButtonFunctionMidiValue.CropLeft;
                BigEncoderDisplayButtons();
            }   //LEFT

            if (b0 == 144 && b1 == 88 && b2 == 127)
            {
                GlobalSettings.BigEncoderOption = BigEncoderButtonFunctionMidiValue.CropTop;
                BigEncoderDisplayButtons();
            }   //TOP

            if (b0 == 144 && b1 == 89 && b2 == 127)
            {
                GlobalSettings.BigEncoderOption = BigEncoderButtonFunctionMidiValue.CropRight;
                BigEncoderDisplayButtons();
            }   //RIGHT

            if (b0 == 144 && b1 == 90 && b2 == 127)
            {
                GlobalSettings.BigEncoderOption = BigEncoderButtonFunctionMidiValue.CropBottom;
                BigEncoderDisplayButtons();
            }   //BOTTOM

            if (b0 == 144 && b1 == 85 && b2 == 127)
            {
                SendToLr.Send("select", "cropReset");
            }   //CROP RESET

            /////////////////// ACTIVE PANEL ///////////////////

            if (b0 == 144 && b1 == 84 && b2 == 127)
            {
                if (GlobalSettings.ActivePanel != ActivePanel.crop)
                {
                    SendToLr.Send("panel", "crop");
                    SendToMidiDevice.MidiSend(ChannelCommand.NoteOn, 0, 84, 1);
                    GlobalSettings.ActivePanel = ActivePanel.crop;
                }
                else
                {
                    SendToLr.Send("panel", "loupe");
                    SendToMidiDevice.MidiSend(ChannelCommand.NoteOn, 0, 84, 0);
                    GlobalSettings.ActivePanel = ActivePanel.loupe;
                }
            }   //CROP PANEL


            /////////////////// BIG ENCODER ///////////////////
            if (b0 == 176 && b1 == 60)
            {
                BigEncoder(b0, b1, b2);
            }
        }

        public static void BigEncoderDisplayButtons()
        {
            var EncoderOption = GlobalSettings.BigEncoderOption;

            SendToMidiDevice.MidiSend(ChannelCommand.NoteOn, 0, (int)EncoderOption, 127);

            foreach (int item in Enum.GetValues(typeof(BigEncoderButtonFunctionMidiValue)))
            {
                if (item != (int)EncoderOption)
                {
                    SendToMidiDevice.MidiSend(ChannelCommand.NoteOn, 0, item, 0);
                }
            }
        }

        private static void BigEncoder(int b0, int b1, int b2)
        {
            var EncoderOption = GlobalSettings.BigEncoderOption;

            var parameter = listParameter.Where(x => x.name == EncoderOption.ToString()).FirstOrDefault();
            var step = (parameter.maxValue - parameter.minValue) / 250;

            if (b2 == 1)
            {
                if (parameter.value < parameter.maxValue)
                {
                    parameter.value += step;
                }
            }
            else
            {
                if (parameter.value > parameter.minValue)
                {
                    parameter.value -= step;
                }
            }

            SendToLr.Send(parameter.name);

        }

        private static void EncoderMethod(byte b0, byte b1, byte b2, byte ifB0rot, byte ifB1rot, string name, byte ifB0but, byte ifB1but, byte ifB2but )
        {
            if (b0 == ifB0rot && b1 == ifB1rot)
            {
                if (b2 > 0 && b2 < ifB1rot)
                {
                    var model = listParameter.Where(x => x.name == name).FirstOrDefault();

                    model.value += b2;

                    if (model.value > model.maxValue)
                    {
                        model.value = model.maxValue;
                    }
                }
                else
                {
                    var model = listParameter.Where(x => x.name == name).FirstOrDefault();

                    model.value -= (b2 - 64);

                    if (model.value < model.minValue)
                    {
                        model.value = model.minValue;
                    }
                }

                SendToLr.Send(name);

            }                   //Encoder 1 - ParametricHighlights

            if (b0 == ifB0but && b1 == ifB1but && b2 == ifB2but)
            {
                SendToLr.SendReset(name);
            }      //Encoder 1 RESET
        }
    }

    public class Parameter
    {
        public Parameter(string name, float maxValue, float minValue)
        {
            this.name = name;
            this.maxValue = maxValue;
            this.minValue = minValue;

        }

        public string name { get; set; }
        public float maxValue { get; set; }
        public float minValue { get; set; }
        public float value { get; set; }
    }
}


public enum BigEncoderButtonFunctionMidiValue
{
    straightenAngle = 86,
    CropLeft = 87,
    CropTop = 88,
    CropRight = 89,
    CropBottom = 90
}

public enum ActivePanel { loupe, crop = 84, dust, redeye, gradient, circularGradient, localized, upright }