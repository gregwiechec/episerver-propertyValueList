using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PropertyValueList.TypeDescriptor
{
    public class PropertyListPropertyDescriptorWrapper : PropertyDescriptor
    {
        private readonly PropertyDescriptor _descriptor;
        private readonly bool _isReadOnly;

        public override Type ComponentType => _descriptor.ComponentType;


        public override AttributeCollection Attributes
        {
            get
            {
                var attributeCollection = base.Attributes.OfType<Attribute>().ToList();
                var uiHintAttribute = attributeCollection.OfType<UIHintAttribute>().FirstOrDefault();
                if (uiHintAttribute != null)
                {
                    attributeCollection.Remove(uiHintAttribute);
                }

                var innerPropertyUiHintAttribute = attributeCollection.OfType<InnerPropertyUIHintAttribute>().FirstOrDefault();
                if (innerPropertyUiHintAttribute != null)
                {
                    attributeCollection.Remove(innerPropertyUiHintAttribute);
                    attributeCollection.Add(new UIHintAttribute(innerPropertyUiHintAttribute.UIHint));
                }

                return new AttributeCollection(attributeCollection.ToArray());
            }
        }


        public override bool IsReadOnly
        {
            get
            {
                if (!_isReadOnly)
                    return _descriptor.IsReadOnly;
                return true;
            }
        }

        public override Type PropertyType { get; }

        public override bool SupportsChangeEvents => _descriptor.SupportsChangeEvents;

        public PropertyListPropertyDescriptorWrapper(PropertyDescriptor descriptor, Type propertyType,
            Attribute[] newAttributes) : base(descriptor, newAttributes)
        {
            _descriptor = descriptor;
            PropertyType = propertyType;
            var readOnlyAttribute = newAttributes.OfType<ReadOnlyAttribute>().FirstOrDefault();
            _isReadOnly = readOnlyAttribute != null && readOnlyAttribute.IsReadOnly;
        }

        public override void AddValueChanged(object component, EventHandler handler)
        {
            _descriptor.AddValueChanged(component, handler);
        }

        public override bool CanResetValue(object component)
        {
            return _descriptor.CanResetValue(component);
        }

        public override object GetValue(object component)
        {
            return _descriptor.GetValue(component);
        }

        public override void RemoveValueChanged(object component, EventHandler handler)
        {
            _descriptor.RemoveValueChanged(component, handler);
        }

        public override void ResetValue(object component)
        {
            _descriptor.ResetValue(component);
        }

        public override void SetValue(object component, object value)
        {
            _descriptor.SetValue(component, value);
        }

        public override bool ShouldSerializeValue(object component)
        {
            return _descriptor.ShouldSerializeValue(component);
        }
    }
}