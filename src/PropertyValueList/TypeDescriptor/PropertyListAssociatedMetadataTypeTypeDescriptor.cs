using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PropertyValueList.TypeDescriptor
{
    public class PropertyListAssociatedMetadataTypeTypeDescriptor : CustomTypeDescriptor
    {
        private readonly string _propertyName;
        private readonly Type _propertyType;

        public PropertyListAssociatedMetadataTypeTypeDescriptor()
        {
        }

        public PropertyListAssociatedMetadataTypeTypeDescriptor(ICustomTypeDescriptor parent, string propertyName, Type propertyType) : base(parent)
        {
            _propertyName = propertyName;
            _propertyType = propertyType;
        }

        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetModifiedProperties(base.GetProperties(attributes));
        }

        public override PropertyDescriptorCollection GetProperties()
        {
            return GetModifiedProperties(base.GetProperties());
        }

        private PropertyDescriptorCollection GetModifiedProperties(PropertyDescriptorCollection properties)
        {
            var propertyDescriptors = new List<PropertyDescriptor>();
            foreach (PropertyDescriptor property in properties)
            {
                if (property.Name == _propertyName)
                {
                    var myPropertyDescriptorWrapper = new PropertyListPropertyDescriptorWrapper(property, _propertyType, property.Attributes.OfType<Attribute>().ToArray());
                    propertyDescriptors.Add(myPropertyDescriptorWrapper);

                }
                else
                {
                    propertyDescriptors.Add(property);
                }
            }
            return new PropertyDescriptorCollection(propertyDescriptors.ToArray());
        }
    }
}