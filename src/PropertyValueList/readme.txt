Using PropertyValueList<T>

[SiteContentType(GUID = "B1B589EB-6961-42B2-B275-AAD21991A5C6")]
public class CollectionTestPage : EPiServer.Core.PageData
{
    [BackingType(typeof(PropertyIntList))]
     public virtual IList<int> CollectionInt2 { get; set; }
}

or with custom InnerPropertyUIHint attribute

[SiteContentType(GUID = "A7B8FF39-ED99-474E-9FFB-2518D49EAD61")]
public class CollectionTestPageString : EPiServer.Core.PageData
{
    [BackingType(typeof(PropertyExtendedStringList))]
    [InnerPropertyUIHint(UIHint.Textarea)]
    public virtual IList<string> CollectionInt2 { get; set; }
}