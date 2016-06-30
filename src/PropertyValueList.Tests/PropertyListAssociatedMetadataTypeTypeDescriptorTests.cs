using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NUnit.Framework;
using PropertyValueList.TypeDescriptor;
using RangeAttribute = System.ComponentModel.DataAnnotations.RangeAttribute;

namespace PropertyValueList.Tests
{
    [TestFixture]
    public class PropertyListAssociatedMetadataTypeTypeDescriptorTests
    {
        [Test]
        public void When_using_custom_TypeDescriptor_Then_property_type_is_replaced()
        {
            TestClass customClass;
            customClass = new TestClass();

            var defaultTypeDescriptor = System.ComponentModel.TypeDescriptor.GetProvider(typeof(TestClass)).GetTypeDescriptor(typeof(TestClass));
            var typeTypeDescriptor = new PropertyListAssociatedMetadataTypeTypeDescriptor(defaultTypeDescriptor,nameof(customClass.ListProperty), typeof(int));
            
            var propertyInfos = customClass.GetType().GetProperties().ToList();
            Assert.That(propertyInfos.Count, Is.EqualTo(1));
            Assert.That(propertyInfos[0].PropertyType, Is.EqualTo(typeof(IList<int>)));

            var customPropertyInfos = typeTypeDescriptor.GetProperties().OfType<PropertyDescriptor>().ToList();
            Assert.That(customPropertyInfos.Count, Is.EqualTo(1));
            Assert.That(customPropertyInfos[0].PropertyType, Is.EqualTo(typeof(int)));
        }

        [Test]
        public void When_using_custom_TypeDescriptor_Then_all_property_attributes_are_available()
        {
            TestClassWithAttributes customClass;

            var defaultTypeDescriptor = System.ComponentModel.TypeDescriptor.GetProvider(typeof(TestClassWithAttributes)).GetTypeDescriptor(typeof(TestClassWithAttributes));
            var typeTypeDescriptor = new PropertyListAssociatedMetadataTypeTypeDescriptor(defaultTypeDescriptor,nameof(customClass.ListProperty), typeof(int));

            var customPropertyInfos = typeTypeDescriptor.GetProperties().OfType<PropertyDescriptor>().ToList()[0];
            var attributesList = customPropertyInfos.Attributes.OfType<Attribute>().ToList();
            Assert.That(attributesList.FirstOrDefault(a=>a is DisplayAttribute), Is.Not.Null);
            Assert.That(attributesList.FirstOrDefault(a=>a is System.ComponentModel.DataAnnotations.RangeAttribute), Is.Not.Null);
            
        }
        [Test]
        public void When_using_custom_TypeDescriptor_Then_InnerPropertyUIHintAttribute_attribute_is_replaced()
        {
            TestClassWithInnerPropertyUIHintAttribute customClass = new TestClassWithInnerPropertyUIHintAttribute();

            var defaultTypeDescriptor = System.ComponentModel.TypeDescriptor.GetProvider(typeof(TestClassWithInnerPropertyUIHintAttribute)).GetTypeDescriptor(typeof(TestClassWithInnerPropertyUIHintAttribute));
            var typeTypeDescriptor = new PropertyListAssociatedMetadataTypeTypeDescriptor(defaultTypeDescriptor,nameof(customClass.ListProperty), typeof(int));

            var attr = customClass.GetType().GetProperties().ToList()[0].GetCustomAttributes(true).OfType<InnerPropertyUIHintAttribute>().FirstOrDefault();
            Assert.That(attr, Is.Not.Null);

            var customPropertyInfos = typeTypeDescriptor.GetProperties().OfType<PropertyDescriptor>().ToList()[0];
            var innerPropertyUIHintAttribute = customPropertyInfos.Attributes.OfType<InnerPropertyUIHintAttribute>().FirstOrDefault();
            Assert.That(innerPropertyUIHintAttribute, Is.Null);

            var uiHintAttribute = customPropertyInfos.Attributes.OfType<UIHintAttribute>().FirstOrDefault();
            Assert.That(uiHintAttribute, Is.Not.Null);
            Assert.That(uiHintAttribute.UIHint, Is.EqualTo("Test"));
        }
        
        public class TestClass
        {
            public virtual IList<int> ListProperty { get; set; }
        }
        
        public class TestClassWithAttributes
        {
            [System.ComponentModel.DataAnnotations.Range(1,100)]
            [Display(Name = "Test")]
            public virtual IList<int> ListProperty { get; set; }
        }        
        public class TestClassWithInnerPropertyUIHintAttribute
        {
            [InnerPropertyUIHintAttribute("Test")]
            public virtual IList<int> ListProperty { get; set; }
        }
    }
}
