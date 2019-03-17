using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTouchToLr.Data;

namespace XTouchToLr.Models
{
    public class SaveParametersModel
    {
        public SaveParametersModel(int id, List<Parameter> parameterList)
        {
            Id = id;
            ParameterList = parameterList;
        }

        public int Id { get; set; }
        public List<Parameter> ParameterList { get; set; }
    }
}
