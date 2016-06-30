using System;
using EPiServer.Framework.DataAnnotations;
using EPiServer.PlugIn;

namespace PropertyValueList.ConcreteProperties
{
    [EditorHint(PropertyIntListEditorDescriptor.UIHint)]
    [PropertyDefinitionTypePlugIn(DisplayName = "Int List", Description = "Int List")]
    [Serializable]
    public class PropertyIntListBackingType : PropertyValueListBackingType<int>
    {
        protected override PropertyValueListBackingType<int> CreateSelfInstance()
        {
            return new PropertyIntListBackingType();
        }
    }
}