using System;

namespace PropertyValueList
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class InnerPropertyUIHintAttribute : Attribute
    {
        public string UIHint { get; set; }
        public string PresentationLayer { get; set; }
        public object[] ControlParameters { get; set; }

        public InnerPropertyUIHintAttribute(string uiHint)
        {
            UIHint = uiHint;
        }

        public InnerPropertyUIHintAttribute(string uiHint, string presentationLayer)
        {
            UIHint = uiHint;
            PresentationLayer = presentationLayer;
        }

        public InnerPropertyUIHintAttribute(string uiHint, string presentationLayer, params object[] controlParameters)
        {
            UIHint = uiHint;
            PresentationLayer = presentationLayer;
            ControlParameters = controlParameters;
        }
    }
}