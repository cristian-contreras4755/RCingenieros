//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace APU.Gasolutions
//{
//    public class FacturaGasolutionsXml
//    {
//    }
//}

// NOTA: El código generado puede requerir, como mínimo, .NET Framework 4.5 o .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class Invoice
{
    private string idField;
    private System.DateTime issueDateField;
    private string invoiceTypeCodeField;
    private string documentCurrencyCodeField;
    private InvoiceAccountingSupplierParty accountingSupplierPartyField;
    private InvoiceAccountingCustomerParty accountingCustomerPartyField;
    private InvoiceTaxTotal taxTotalField;
    private InvoiceLegalMonetaryTotal legalMonetaryTotalField;
    private InvoiceInvoiceLine[] invoiceLineField;
    private InvoiceAdditionalInformation additionalInformationField;
    //private InvoiceAdjuntos adjuntosField;

    /// <remarks/>
    public string ID
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

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
    public System.DateTime IssueDate
    {
        get
        {
            return this.issueDateField;
        }
        set
        {
            this.issueDateField = value;
        }
    }

    /// <remarks/>
    public string InvoiceTypeCode
    {
        get
        {
            return this.invoiceTypeCodeField;
        }
        set
        {
            this.invoiceTypeCodeField = value;
        }
    }

    /// <remarks/>
    public string DocumentCurrencyCode
    {
        get
        {
            return this.documentCurrencyCodeField;
        }
        set
        {
            this.documentCurrencyCodeField = value;
        }
    }

    /// <remarks/>
    public InvoiceAccountingSupplierParty AccountingSupplierParty
    {
        get
        {
            return this.accountingSupplierPartyField;
        }
        set
        {
            this.accountingSupplierPartyField = value;
        }
    }

    /// <remarks/>
    public InvoiceAccountingCustomerParty AccountingCustomerParty
    {
        get
        {
            return this.accountingCustomerPartyField;
        }
        set
        {
            this.accountingCustomerPartyField = value;
        }
    }

    /// <remarks/>
    public InvoiceTaxTotal TaxTotal
    {
        get
        {
            return this.taxTotalField;
        }
        set
        {
            this.taxTotalField = value;
        }
    }

    /// <remarks/>
    public InvoiceLegalMonetaryTotal LegalMonetaryTotal
    {
        get
        {
            return this.legalMonetaryTotalField;
        }
        set
        {
            this.legalMonetaryTotalField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("InvoiceLine")]
    public InvoiceInvoiceLine[] InvoiceLine
    {
        get
        {
            return this.invoiceLineField;
        }
        set
        {
            this.invoiceLineField = value;
        }
    }

    /// <remarks/>
    public InvoiceAdditionalInformation AdditionalInformation
    {
        get
        {
            return this.additionalInformationField;
        }
        set
        {
            this.additionalInformationField = value;
        }
    }

    ///// <remarks/>
    //public InvoiceAdjuntos Adjuntos
    //{
    //    get
    //    {
    //        return this.adjuntosField;
    //    }
    //    set
    //    {
    //        this.adjuntosField = value;
    //    }
    //}
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InvoiceAccountingSupplierParty
{
    private string customerAssignedAccountIDField;
    private string additionalAccountIdField;
    private InvoiceAccountingSupplierPartyParty partyField;

    /// <remarks/>
    public string CustomerAssignedAccountID
    {
        get
        {
            return this.customerAssignedAccountIDField;
        }
        set
        {
            this.customerAssignedAccountIDField = value;
        }
    }

    /// <remarks/>
    public string AdditionalAccountID
    {
        get
        {
            return this.additionalAccountIdField;
        }
        set
        {
            this.additionalAccountIdField = value;
        }
    }

    /// <remarks/>
    public InvoiceAccountingSupplierPartyParty Party
    {
        get
        {
            return this.partyField;
        }
        set
        {
            this.partyField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InvoiceAccountingSupplierPartyParty
{
    private InvoiceAccountingSupplierPartyPartyPartyName partyNameField;
    private InvoiceAccountingSupplierPartyPartyPostalAddress postalAddressField;
    private InvoiceAccountingSupplierPartyPartyPartyLegalEntity partyLegalEntityField;

    /// <remarks/>
    public InvoiceAccountingSupplierPartyPartyPartyName PartyName
    {
        get
        {
            return this.partyNameField;
        }
        set
        {
            this.partyNameField = value;
        }
    }

    /// <remarks/>
    public InvoiceAccountingSupplierPartyPartyPostalAddress PostalAddress
    {
        get
        {
            return this.postalAddressField;
        }
        set
        {
            this.postalAddressField = value;
        }
    }

    /// <remarks/>
    public InvoiceAccountingSupplierPartyPartyPartyLegalEntity PartyLegalEntity
    {
        get
        {
            return this.partyLegalEntityField;
        }
        set
        {
            this.partyLegalEntityField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InvoiceAccountingSupplierPartyPartyPartyName
{
    private string nameField;
    /// <remarks/>
    public string Name
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
public partial class InvoiceAccountingSupplierPartyPartyPostalAddress
{
    private string idField;
    private string streetNameField;
    private string cityNameField;
    private string countrySubentityField;
    private string districtField;
    private InvoiceAccountingSupplierPartyPartyPostalAddressCountry countryField;

    /// <remarks/>
    public string ID
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

    /// <remarks/>
    public string StreetName
    {
        get
        {
            return this.streetNameField;
        }
        set
        {
            this.streetNameField = value;
        }
    }

    /// <remarks/>
    public string CityName
    {
        get
        {
            return this.cityNameField;
        }
        set
        {
            this.cityNameField = value;
        }
    }

    /// <remarks/>
    public string CountrySubentity
    {
        get
        {
            return this.countrySubentityField;
        }
        set
        {
            this.countrySubentityField = value;
        }
    }

    /// <remarks/>
    public string District
    {
        get
        {
            return this.districtField;
        }
        set
        {
            this.districtField = value;
        }
    }

    /// <remarks/>
    public InvoiceAccountingSupplierPartyPartyPostalAddressCountry Country
    {
        get
        {
            return this.countryField;
        }
        set
        {
            this.countryField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InvoiceAccountingSupplierPartyPartyPostalAddressCountry
{
    private string identificationCodeField;

    /// <remarks/>
    public string IdentificationCode
    {
        get
        {
            return this.identificationCodeField;
        }
        set
        {
            this.identificationCodeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InvoiceAccountingSupplierPartyPartyPartyLegalEntity
{
    private string registrationNameField;

    /// <remarks/>
    public string RegistrationName
    {
        get
        {
            return this.registrationNameField;
        }
        set
        {
            this.registrationNameField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InvoiceAccountingCustomerParty
{
    private string customerAssignedAccountIDField;
    private string additionalAccountIdField;
    private InvoiceAccountingCustomerPartyParty partyField;

    /// <remarks/>
    public string CustomerAssignedAccountID
    {
        get
        {
            return this.customerAssignedAccountIDField;
        }
        set
        {
            this.customerAssignedAccountIDField = value;
        }
    }

    /// <remarks/>
    public string AdditionalAccountID
    {
        get
        {
            return this.additionalAccountIdField;
        }
        set
        {
            this.additionalAccountIdField = value;
        }
    }

    /// <remarks/>
    public InvoiceAccountingCustomerPartyParty Party
    {
        get
        {
            return this.partyField;
        }
        set
        {
            this.partyField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InvoiceAccountingCustomerPartyParty
{
    private InvoiceAccountingCustomerPartyPartyPartyLegalEntity partyLegalEntityField;

    /// <remarks/>
    public InvoiceAccountingCustomerPartyPartyPartyLegalEntity PartyLegalEntity
    {
        get
        {
            return this.partyLegalEntityField;
        }
        set
        {
            this.partyLegalEntityField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InvoiceAccountingCustomerPartyPartyPartyLegalEntity
{
    private string registrationNameField;

    /// <remarks/>
    public string RegistrationName
    {
        get
        {
            return this.registrationNameField;
        }
        set
        {
            this.registrationNameField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InvoiceTaxTotal
{
    private decimal taxAmountField;
    private InvoiceTaxTotalTaxSubtotal taxSubtotalField;

    /// <remarks/>
    public decimal TaxAmount
    {
        get
        {
            return this.taxAmountField;
        }
        set
        {
            this.taxAmountField = value;
        }
    }

    /// <remarks/>
    public InvoiceTaxTotalTaxSubtotal TaxSubtotal
    {
        get
        {
            return this.taxSubtotalField;
        }
        set
        {
            this.taxSubtotalField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InvoiceTaxTotalTaxSubtotal
{
    private decimal taxAmountField;
    private InvoiceTaxTotalTaxSubtotalTaxCategory taxCategoryField;

    /// <remarks/>
    public decimal TaxAmount
    {
        get
        {
            return this.taxAmountField;
        }
        set
        {
            this.taxAmountField = value;
        }
    }

    /// <remarks/>
    public InvoiceTaxTotalTaxSubtotalTaxCategory TaxCategory
    {
        get
        {
            return this.taxCategoryField;
        }
        set
        {
            this.taxCategoryField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InvoiceTaxTotalTaxSubtotalTaxCategory
{
    private InvoiceTaxTotalTaxSubtotalTaxCategoryTaxScheme taxSchemeField;

    /// <remarks/>
    public InvoiceTaxTotalTaxSubtotalTaxCategoryTaxScheme TaxScheme
    {
        get
        {
            return this.taxSchemeField;
        }
        set
        {
            this.taxSchemeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InvoiceTaxTotalTaxSubtotalTaxCategoryTaxScheme
{
    private ushort idField;
    private string nameField;
    private string taxTypeCodeField;

    /// <remarks/>
    public ushort ID
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

    /// <remarks/>
    public string Name
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
    public string TaxTypeCode
    {
        get
        {
            return this.taxTypeCodeField;
        }
        set
        {
            this.taxTypeCodeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InvoiceLegalMonetaryTotal
{
    private decimal payableAmountField;

    /// <remarks/>
    public decimal PayableAmount
    {
        get
        {
            return this.payableAmountField;
        }
        set
        {
            this.payableAmountField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InvoiceInvoiceLine
{
    private int idField;
    private decimal invoicedQuantityField;
    private string unitCodeField;
    private decimal lineExtensionAmountField;
    private InvoiceInvoiceLinePricingReference pricingReferenceField;
    private InvoiceInvoiceLineTaxTotal taxTotalField;
    private InvoiceInvoiceLineItem itemField;
    private InvoiceInvoiceLinePrice priceField;

    /// <remarks/>
    public int ID
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

    /// <remarks/>
    public decimal InvoicedQuantity
    {
        get
        {
            return this.invoicedQuantityField;
        }
        set
        {
            this.invoicedQuantityField = value;
        }
    }

    /// <remarks/>
    public string UnitCode
    {
        get
        {
            return this.unitCodeField;
        }
        set
        {
            this.unitCodeField = value;
        }
    }

    /// <remarks/>
    public decimal LineExtensionAmount
    {
        get
        {
            return this.lineExtensionAmountField;
        }
        set
        {
            this.lineExtensionAmountField = value;
        }
    }

    /// <remarks/>
    public InvoiceInvoiceLinePricingReference PricingReference
    {
        get
        {
            return this.pricingReferenceField;
        }
        set
        {
            this.pricingReferenceField = value;
        }
    }

    /// <remarks/>
    public InvoiceInvoiceLineTaxTotal TaxTotal
    {
        get
        {
            return this.taxTotalField;
        }
        set
        {
            this.taxTotalField = value;
        }
    }

    /// <remarks/>
    public InvoiceInvoiceLineItem Item
    {
        get
        {
            return this.itemField;
        }
        set
        {
            this.itemField = value;
        }
    }

    /// <remarks/>
    public InvoiceInvoiceLinePrice Price
    {
        get
        {
            return this.priceField;
        }
        set
        {
            this.priceField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InvoiceInvoiceLinePricingReference
{
    private InvoiceInvoiceLinePricingReferenceAlternativeConditionPrice alternativeConditionPriceField;

    /// <remarks/>
    public InvoiceInvoiceLinePricingReferenceAlternativeConditionPrice AlternativeConditionPrice
    {
        get
        {
            return this.alternativeConditionPriceField;
        }
        set
        {
            this.alternativeConditionPriceField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InvoiceInvoiceLinePricingReferenceAlternativeConditionPrice
{
    private decimal priceAmountField;
    private byte priceTypeCodeField;

    /// <remarks/>
    public decimal PriceAmount
    {
        get
        {
            return this.priceAmountField;
        }
        set
        {
            this.priceAmountField = value;
        }
    }

    /// <remarks/>
    public byte PriceTypeCode
    {
        get
        {
            return this.priceTypeCodeField;
        }
        set
        {
            this.priceTypeCodeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InvoiceInvoiceLineTaxTotal
{
    private decimal taxAmountField;
    private InvoiceInvoiceLineTaxTotalTaxSubtotal taxSubtotalField;

    /// <remarks/>
    public decimal TaxAmount
    {
        get
        {
            return this.taxAmountField;
        }
        set
        {
            this.taxAmountField = value;
        }
    }

    /// <remarks/>
    public InvoiceInvoiceLineTaxTotalTaxSubtotal TaxSubtotal
    {
        get
        {
            return this.taxSubtotalField;
        }
        set
        {
            this.taxSubtotalField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InvoiceInvoiceLineTaxTotalTaxSubtotal
{
    private decimal taxAmountField;
    private InvoiceInvoiceLineTaxTotalTaxSubtotalTaxCategory taxCategoryField;

    /// <remarks/>
    public decimal TaxAmount
    {
        get
        {
            return this.taxAmountField;
        }
        set
        {
            this.taxAmountField = value;
        }
    }

    /// <remarks/>
    public InvoiceInvoiceLineTaxTotalTaxSubtotalTaxCategory TaxCategory
    {
        get
        {
            return this.taxCategoryField;
        }
        set
        {
            this.taxCategoryField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InvoiceInvoiceLineTaxTotalTaxSubtotalTaxCategory
{
    private byte taxExemptionReasonCodeField;
    private InvoiceInvoiceLineTaxTotalTaxSubtotalTaxCategoryTaxScheme taxSchemeField;

    /// <remarks/>
    public byte TaxExemptionReasonCode
    {
        get
        {
            return this.taxExemptionReasonCodeField;
        }
        set
        {
            this.taxExemptionReasonCodeField = value;
        }
    }

    /// <remarks/>
    public InvoiceInvoiceLineTaxTotalTaxSubtotalTaxCategoryTaxScheme TaxScheme
    {
        get
        {
            return this.taxSchemeField;
        }
        set
        {
            this.taxSchemeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InvoiceInvoiceLineTaxTotalTaxSubtotalTaxCategoryTaxScheme
{
    private ushort idField;
    private string nameField;
    private string taxTypeCodeField;

    /// <remarks/>
    public ushort ID
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

    /// <remarks/>
    public string Name
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
    public string TaxTypeCode
    {
        get
        {
            return this.taxTypeCodeField;
        }
        set
        {
            this.taxTypeCodeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InvoiceInvoiceLineItem
{
    private string descriptionField;
    private InvoiceInvoiceLineItemSellersItemIdentification sellersItemIdentificationField;

    /// <remarks/>
    public string Description
    {
        get
        {
            return this.descriptionField;
        }
        set
        {
            this.descriptionField = value;
        }
    }

    /// <remarks/>
    public InvoiceInvoiceLineItemSellersItemIdentification SellersItemIdentification
    {
        get
        {
            return this.sellersItemIdentificationField;
        }
        set
        {
            this.sellersItemIdentificationField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InvoiceInvoiceLineItemSellersItemIdentification
{
    private string idField;

    /// <remarks/>
    public string ID
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
public partial class InvoiceInvoiceLinePrice
{
    private decimal priceAmountField;

    /// <remarks/>
    public decimal PriceAmount
    {
        get
        {
            return this.priceAmountField;
        }
        set
        {
            this.priceAmountField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InvoiceAdditionalInformation
{
    private InvoiceAdditionalInformationAdditionalMonetaryTotal additionalMonetaryTotalField;

    private InvoiceAdditionalInformationAdditionalProperty additionalPropertyField;

    private InvoiceAdditionalInformationSUNATTransaction sUNATTransactionField;

    /// <remarks/>
    public InvoiceAdditionalInformationAdditionalMonetaryTotal AdditionalMonetaryTotal
    {
        get
        {
            return this.additionalMonetaryTotalField;
        }
        set
        {
            this.additionalMonetaryTotalField = value;
        }
    }

    /// <remarks/>
    public InvoiceAdditionalInformationAdditionalProperty AdditionalProperty
    {
        get
        {
            return this.additionalPropertyField;
        }
        set
        {
            this.additionalPropertyField = value;
        }
    }

    /// <remarks/>
    public InvoiceAdditionalInformationSUNATTransaction SUNATTransaction
    {
        get
        {
            return this.sUNATTransactionField;
        }
        set
        {
            this.sUNATTransactionField = value;
        }
    }
}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
//public partial class InvoiceAdditionalInformationAdditionalMonetaryTotal
//{
//    private ushort idField;
//    private decimal payableAmountField;

//    /// <remarks/>
//    public ushort ID
//    {
//        get
//        {
//            return this.idField;
//        }
//        set
//        {
//            this.idField = value;
//        }
//    }

//    /// <remarks/>
//    public decimal PayableAmount
//    {
//        get
//        {
//            return this.payableAmountField;
//        }
//        set
//        {
//            this.payableAmountField = value;
//        }
//    }
//}
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InvoiceAdditionalInformationAdditionalMonetaryTotal
{
    private object[] itemsField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ID", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("PayableAmount", typeof(InvoiceAdditionalInformationAdditionalMonetaryTotalPayableAmount))]
    public object[] Items
    {
        get
        {
            return this.itemsField;
        }
        set
        {
            this.itemsField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InvoiceAdditionalInformationAdditionalMonetaryTotalPayableAmount
{
    private string currencyIDField;
    private decimal valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string currencyID
    {
        get { return this.currencyIDField; }
        set { this.currencyIDField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public decimal Value
    {
        get { return this.valueField; }
        set { this.valueField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class InvoiceAdditionalInformationAdditionalProperty
{

    private string idField;

    private string valueField;

    /// <remarks/>
    public string ID
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

    /// <remarks/>
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
public partial class InvoiceAdditionalInformationSUNATTransaction
{

    private string idField;

    /// <remarks/>
    public string ID
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
public partial class InvoiceAdjuntos
{
    private object libre1Field;
    private object libre2Field;
    private object libre3Field;
    private object libre4Field;
    private object libre5Field;
    private object libre6Field;
    private object observacionField;
    private object montoPalabrasField;
    private object mailReceptorField;
    private object impresoraField;
    private object copiasField;

    /// <remarks/>
    public object Libre1
    {
        get
        {
            return this.libre1Field;
        }
        set
        {
            this.libre1Field = value;
        }
    }

    /// <remarks/>
    public object Libre2
    {
        get
        {
            return this.libre2Field;
        }
        set
        {
            this.libre2Field = value;
        }
    }

    /// <remarks/>
    public object Libre3
    {
        get
        {
            return this.libre3Field;
        }
        set
        {
            this.libre3Field = value;
        }
    }

    /// <remarks/>
    public object Libre4
    {
        get
        {
            return this.libre4Field;
        }
        set
        {
            this.libre4Field = value;
        }
    }

    /// <remarks/>
    public object Libre5
    {
        get
        {
            return this.libre5Field;
        }
        set
        {
            this.libre5Field = value;
        }
    }

    /// <remarks/>
    public object Libre6
    {
        get
        {
            return this.libre6Field;
        }
        set
        {
            this.libre6Field = value;
        }
    }

    /// <remarks/>
    public object Observacion
    {
        get
        {
            return this.observacionField;
        }
        set
        {
            this.observacionField = value;
        }
    }

    /// <remarks/>
    public object MontoPalabras
    {
        get
        {
            return this.montoPalabrasField;
        }
        set
        {
            this.montoPalabrasField = value;
        }
    }

    /// <remarks/>
    public object MailReceptor
    {
        get
        {
            return this.mailReceptorField;
        }
        set
        {
            this.mailReceptorField = value;
        }
    }

    /// <remarks/>
    public object Impresora
    {
        get
        {
            return this.impresoraField;
        }
        set
        {
            this.impresoraField = value;
        }
    }

    /// <remarks/>
    public object Copias
    {
        get
        {
            return this.copiasField;
        }
        set
        {
            this.copiasField = value;
        }
    }
}