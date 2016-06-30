using System;
using EPiServer.Framework.DataAnnotations;
using EPiServer.PlugIn;

namespace PropertyValueList.ConcreteProperties
{
    [EditorHint(PropertyDateTimeListEditorDescriptor.UIHint)]
    [PropertyDefinitionTypePlugIn(DisplayName = "DateTime List", Description = "DateTime List")]
    [Serializable]
    public class PropertyDateTimeListBackingType : PropertyValueListBackingType<DateTime>
    {
        protected override PropertyValueListBackingType<DateTime> CreateSelfInstance()
        {
            return new PropertyDateTimeListBackingType();
        }
    }
}