using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EPiServer.Framework.Localization;
using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using EPiServer.Shell.UI.Rest;
using PropertyValueList.TypeDescriptor;

namespace PropertyValueList
{
    public abstract class PropertyValueListEditorDescriptor<T> : EditorDescriptor
    {
        private readonly LocalizationService _localizationService;
        private readonly IMetadataStoreModelCreator _metadataStoreModelCreator;
        private readonly ServiceAccessor<HttpContextBase> _httpContextServiceAccessor;

        protected PropertyValueListEditorDescriptor(LocalizationService localizationService,
            IMetadataStoreModelCreator metadataStoreModelCreator,
            ServiceAccessor<HttpContextBase> httpContextServiceAccessor)
        {
            _localizationService = localizationService;
            _metadataStoreModelCreator = metadataStoreModelCreator;
            _httpContextServiceAccessor = httpContextServiceAccessor;

            ClientEditingClass = "propertyValueList/CustomCollectionWidget";
        }

        public override void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            base.ModifyMetadata(metadata, attributes);

            //Used using ServiceLocator to protect from StructureMap: "Bi-directional dependency relationship detected!"
            var metadataHandlerRegistry = ServiceLocator.Current.GetInstance<MetadataHandlerRegistry>();

            var extensibleMetadataProvider =
                new PropertyValueListExtensibleMetadataProvider(metadataHandlerRegistry, _localizationService,
                    _httpContextServiceAccessor, metadata.PropertyName, typeof(T));

            var modelMetadata =
                extensibleMetadataProvider.GetMetadataForType(
                    () => Activator.CreateInstance(metadata.Parent.Model.GetType()),
                    metadata.Parent.Model.GetType());

            var metadataStoreModel = _metadataStoreModelCreator.Create(modelMetadata);

            var propertyMetadata = metadataStoreModel.Properties.FirstOrDefault(p => p.Name == metadata.PropertyName);

            metadata.CustomEditorSettings["innerPropertySettings"] = propertyMetadata;
        }
    }
}