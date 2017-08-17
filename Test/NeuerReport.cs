
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class ReportDataSet
{

    private ReportDataSetDataItems dataItemsField;

    private string nameField;

    private ushort idField;

    /// <remarks/>
    public ReportDataSetDataItems DataItems
    {
        get
        {
            return this.dataItemsField;
        }
        set
        {
            this.dataItemsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public ushort id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ReportDataSetDataItems
{

    private ReportDataSetDataItemsDataItem dataItemField;

    /// <remarks/>
    public ReportDataSetDataItemsDataItem DataItem
    {
        get
        {
            return this.dataItemField;
        }
        set
        {
            this.dataItemField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ReportDataSetDataItemsDataItem
{

    private ReportDataSetDataItemsDataItemColumn[] columnsField;

    private ReportDataSetDataItemsDataItemDataItems dataItemsField;

    private string nameField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Column", IsNullable = false)]
    public ReportDataSetDataItemsDataItemColumn[] Columns
    {
        get
        {
            return this.columnsField;
        }
        set
        {
            this.columnsField = value;
        }
    }

    /// <remarks/>
    public ReportDataSetDataItemsDataItemDataItems DataItems
    {
        get
        {
            return this.dataItemsField;
        }
        set
        {
            this.dataItemsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ReportDataSetDataItemsDataItemColumn
{

    private string nameField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
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

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ReportDataSetDataItemsDataItemDataItems
{

    private ReportDataSetDataItemsDataItemDataItemsDataItem dataItemField;

    /// <remarks/>
    public ReportDataSetDataItemsDataItemDataItemsDataItem DataItem
    {
        get
        {
            return this.dataItemField;
        }
        set
        {
            this.dataItemField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ReportDataSetDataItemsDataItemDataItemsDataItem
{

    private ReportDataSetDataItemsDataItemDataItemsDataItemColumn[] columnsField;

    private ReportDataSetDataItemsDataItemDataItemsDataItemDataItems dataItemsField;

    private string nameField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Column", IsNullable = false)]
    public ReportDataSetDataItemsDataItemDataItemsDataItemColumn[] Columns
    {
        get
        {
            return this.columnsField;
        }
        set
        {
            this.columnsField = value;
        }
    }

    /// <remarks/>
    public ReportDataSetDataItemsDataItemDataItemsDataItemDataItems DataItems
    {
        get
        {
            return this.dataItemsField;
        }
        set
        {
            this.dataItemsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ReportDataSetDataItemsDataItemDataItemsDataItemColumn
{

    private string nameField;

    private string decimalformatterField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string decimalformatter
    {
        get
        {
            return this.decimalformatterField;
        }
        set
        {
            this.decimalformatterField = value;
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

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ReportDataSetDataItemsDataItemDataItemsDataItemDataItems
{

    private ReportDataSetDataItemsDataItemDataItemsDataItemDataItemsDataItem dataItemField;

    /// <remarks/>
    public ReportDataSetDataItemsDataItemDataItemsDataItemDataItemsDataItem DataItem
    {
        get
        {
            return this.dataItemField;
        }
        set
        {
            this.dataItemField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ReportDataSetDataItemsDataItemDataItemsDataItemDataItemsDataItem
{

    private ReportDataSetDataItemsDataItemDataItemsDataItemDataItemsDataItemDataItem[] dataItemsField;

    private string nameField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("DataItem", IsNullable = false)]
    public ReportDataSetDataItemsDataItemDataItemsDataItemDataItemsDataItemDataItem[] DataItems
    {
        get
        {
            return this.dataItemsField;
        }
        set
        {
            this.dataItemsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ReportDataSetDataItemsDataItemDataItemsDataItemDataItemsDataItemDataItem
{

    private ReportDataSetDataItemsDataItemDataItemsDataItemDataItemsDataItemDataItemColumn[] columnsField;

    private ReportDataSetDataItemsDataItemDataItemsDataItemDataItemsDataItemDataItemDataItem[] dataItemsField;

    private string nameField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Column", IsNullable = false)]
    public ReportDataSetDataItemsDataItemDataItemsDataItemDataItemsDataItemDataItemColumn[] Columns
    {
        get
        {
            return this.columnsField;
        }
        set
        {
            this.columnsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("DataItem", IsNullable = false)]
    public ReportDataSetDataItemsDataItemDataItemsDataItemDataItemsDataItemDataItemDataItem[] DataItems
    {
        get
        {
            return this.dataItemsField;
        }
        set
        {
            this.dataItemsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ReportDataSetDataItemsDataItemDataItemsDataItemDataItemsDataItemDataItemColumn
{

    private string nameField;

    private string decimalformatterField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string decimalformatter
    {
        get
        {
            return this.decimalformatterField;
        }
        set
        {
            this.decimalformatterField = value;
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

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ReportDataSetDataItemsDataItemDataItemsDataItemDataItemsDataItemDataItemDataItem
{

    private ReportDataSetDataItemsDataItemDataItemsDataItemDataItemsDataItemDataItemDataItemColumn[] columnsField;

    private string nameField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Column", IsNullable = false)]
    public ReportDataSetDataItemsDataItemDataItemsDataItemDataItemsDataItemDataItemDataItemColumn[] Columns
    {
        get
        {
            return this.columnsField;
        }
        set
        {
            this.columnsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ReportDataSetDataItemsDataItemDataItemsDataItemDataItemsDataItemDataItemDataItemColumn
{

    private string nameField;

    private string decimalformatterField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string decimalformatter
    {
        get
        {
            return this.decimalformatterField;
        }
        set
        {
            this.decimalformatterField = value;
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

