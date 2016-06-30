using System;
using System.Collections.Generic;
using System.Web;
using EPiServer.Framework.Localization;
using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using EPiServer.Shell.UI.Rest;

namespace PropertyValueList.ConcreteProperties
{
    [EditorDescriptorRegistration(TargetType = typeof(IList<DateTime>), UIHint = UIHint)]
    public class PropertyDateTimeListEditorDescriptor : PropertyValueListEditorDescriptor<DateTime>
    {
        public const string UIHint = "PropertyDateTimeList";

        public PropertyDateTimeListEditorDescriptor(LocalizationService localizationService, IMetadataStoreModelCreator metadataStoreModelCreator, ServiceAccessor<HttpContextBase> httpContextServiceAccessor)
            : base(localizationService, metadataStoreModelCreator, httpContextServiceAccessor)
        {
        }
    }
}