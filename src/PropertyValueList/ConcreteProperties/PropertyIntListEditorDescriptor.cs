using System.Collections.Generic;
using System.Web;
using EPiServer.Framework.Localization;
using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using EPiServer.Shell.UI.Rest;

namespace PropertyValueList.ConcreteProperties
{
    [EditorDescriptorRegistration(TargetType = typeof(IList<int>), UIHint = UIHint)]
    public class PropertyIntListEditorDescriptor : PropertyValueListEditorDescriptor<int>
    {
        public const string UIHint = "PropertyIntList";

        public PropertyIntListEditorDescriptor(LocalizationService localizationService, IMetadataStoreModelCreator metadataStoreModelCreator, ServiceAccessor<HttpContextBase> httpContextServiceAccessor)
            : base(localizationService, metadataStoreModelCreator, httpContextServiceAccessor)
        {
        }
    }
}