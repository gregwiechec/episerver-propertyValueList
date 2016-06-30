using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PropertyValueList.TypeDescriptor
{
    public class PropertyValueListAssociatedMetadataTypeTypeDescriptionProvider : AssociatedMetadataTypeTypeDescriptionProvider
    {
        private readonly string _propertyName;
        private readonly Type _propertyType;

        public PropertyValueListAssociatedMetadataTypeTypeDescriptionProvider(Type type, string propertyName, Type propertyType) : base(type)
        {
            _propertyName = propertyName;
            _propertyType = propertyType;
        }

        public PropertyValueListAssociatedMetadataTypeTypeDescriptionProvider(Type type, Type associatedMetadataType, string propertyName, Type propertyType) : base(type, associatedMetadataType)
        {
            _propertyName = propertyName;
            _propertyType = propertyType;
        }

        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            return new PropertyListAssociatedMetadataTypeTypeDescriptor(base.GetTypeDescriptor(objectType, instance), _propertyName, _propertyType);
        }
    }
}