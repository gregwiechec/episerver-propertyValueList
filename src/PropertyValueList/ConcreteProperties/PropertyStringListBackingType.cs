using System;
using EPiServer.Framework.DataAnnotations;
using EPiServer.PlugIn;

namespace PropertyValueList.ConcreteProperties
{
    [EditorHint(PropertyStringListEditorDescriptor.UIHint)]
    [PropertyDefinitionTypePlugIn(DisplayName = "String List", Description = "String List")]
    [Serializable]
    public class PropertyStringListBackingType : PropertyValueListBackingType<string>
    {
        protected override PropertyValueListBackingType<string> CreateSelfInstance()
        {
            return new PropertyStringListBackingType();
        }
    }
}