using System;
using System.ComponentModel;
using System.Web;
using EPiServer.Framework.Localization;
using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing;

namespace PropertyValueList.TypeDescriptor
{
    public class PropertyValueListExtensibleMetadataProvider : ExtensibleMetadataProvider
    {
        private readonly string _propertyName;
        private readonly Type _propertyType;

        public PropertyValueListExtensibleMetadataProvider(MetadataHandlerRegistry metadataHandlerRegistry,
            LocalizationService localizationService, ServiceAccessor<HttpContextBase> httpContext, string propertyName, Type propertyType)
            : base(metadataHandlerRegistry, localizationService, httpContext)
        {
            _propertyName = propertyName;
            _propertyType = propertyType;
        }

        protected override ICustomTypeDescriptor GetTypeDescriptor(Type type)
        {
            return new PropertyValueListAssociatedMetadataTypeTypeDescriptionProvider(type, _propertyName, _propertyType).GetTypeDescriptor(type);
        }
    }
}