using System;

namespace Net6CoreApiBoilerplateAutofac.Utility.Attributes
{
    public class EnumData : Attribute
    {
        public EnumData(object data)
        {
            Data = data;
        }

        public object Data { get; set; }
    }
}
