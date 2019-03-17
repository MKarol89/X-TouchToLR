using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTouchToLr.Data;

namespace XTouchToLr.Engine
{
    public static class SaveReadParameters
    {
        public static void Save(int id)
        {
            var saveList = GlobalSettings.saveParameterList;
            var acctualParameters = LrParameters.listParameter;

            var model = saveList.Where(x => x.Id == id).FirstOrDefault();

            if (model != null)
            {
                foreach (var item in acctualParameters)
                {
                    var model2 = model.ParameterList.Where(x => x.name == item.name).FirstOrDefault();

                    model2.value = item.value;
                }
            }
            else
            {
                var list = new List<Parameter>();

                foreach (var item in acctualParameters)
                {
                    list.Add(new Parameter(item.name, item.maxValue, item.minValue) { value = item.value });
                }

                saveList.Add(new Models.SaveParametersModel(id, list));
            }
        }

        public static void Read(int id)
        {
            var saveList = GlobalSettings.saveParameterList;
            var acctualParameters = LrParameters.listParameter;

            var model = saveList.Where(x => x.Id == id).FirstOrDefault();

            if (model != null)
            {
                foreach (var item in model.ParameterList)
                {
                    var model2 = acctualParameters.Where(x => x.name == item.name).FirstOrDefault();

                    model2.value = item.value;

                    SendToMidiDevice.Send(model2.name);
                }
            }

            foreach (var item in saveList.Where(x => x.Id == id).FirstOrDefault().ParameterList)
            {
                SendToLr.Send(item.name, item.value.ToString());
            }
        }
    }
}
