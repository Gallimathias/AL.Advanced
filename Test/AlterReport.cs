
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:www.navision.com:Report")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:www.navision.com:Report", IsNullable = false)]
public partial class Report
{

    private string titleField;

    private string hTMLHeadField;

    private ReportPage[] pageField;

    private string hTMLFootField;

    /// <remarks/>
    public string Title
    {
        get
        {
            return this.titleField;
        }
        set
        {
            this.titleField = value;
        }
    }

    /// <remarks/>
    public string HTMLHead
    {
        get
        {
            return this.hTMLHeadField;
        }
        set
        {
            this.hTMLHeadField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Page")]
    public ReportPage[] Page
    {
        get
        {
            return this.pageField;
        }
        set
        {
            this.pageField = value;
        }
    }

    /// <remarks/>
    public string HTMLFoot
    {
        get
        {
            return this.hTMLFootField;
        }
        set
        {
            this.hTMLFootField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:www.navision.com:Report")]
public partial class ReportPage
{

    private byte pageNoField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte PageNo
    {
        get
        {
            return this.pageNoField;
        }
        set
        {
            this.pageNoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

