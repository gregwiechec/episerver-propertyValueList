using System.ComponentModel;
using EPiServer.Core;

namespace PropertyValueList
{
    public abstract class PropertyValueListBackingType<T> : PropertyList<T>
    {
        public override IPropertyControl CreatePropertyControl()
        {
            return null;
        }

        public override PropertyData ParseToObject(string value)
        {
            var scalarList = CreateSelfInstance();
            scalarList.ParseToSelf(value);
            return scalarList;
        }

        protected override T ParseItem(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var typeConverter = new TypeConverter();
                return (T)(typeConverter.ConvertTo(value, typeof(T)));
            }
            return default(T);
        }

        protected abstract PropertyValueListBackingType<T> CreateSelfInstance();
    }
}