//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace APU.Gasolutions
//{
//    public class NotaCreditoXml
//    {
//    }
//}



// NOTA: El código generado puede requerir, como mínimo, .NET Framework 4.5 o .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class CreditNote
{
    private string idField;
    private System.DateTime issueDateField;
    private string documentCurrencyCodeField;
    private CreditNoteDiscrepancyResponse discrepancyResponseField;
    private CreditNoteBillingReference billingReferenceField;
    private CreditNoteAccountingSupplierParty accountingSupplierPartyField;
    private CreditNoteAccountingCustomerParty accountingCustomerPartyField;
    private CreditNoteTaxTotal taxTotalField;
    private CreditNoteLegalMonetaryTotal legalMonetaryTotalField;
    private CreditNoteCreditNoteLine[] creditNoteLineField;
    private CreditNoteAdditionalInformation additionalInformationField;

    /// <remarks/>
    public string ID
    {
        get{ return this.idField; }
        set { this.idField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
    public System.DateTime IssueDate
    {
        get{ return this.issueDateField; }
        set{ this.issueDateField = value; }
    }

    /// <remarks/>
    public string DocumentCurrencyCode
    {
        get{ return this.documentCurrencyCodeField; }
        set{ this.documentCurrencyCodeField = value; }
    }

    /// <remarks/>
    public CreditNoteDiscrepancyResponse DiscrepancyResponse
    {
        get{ return this.discrepancyResponseField; }
        set{ this.discrepancyResponseField = value; }
    }

    /// <remarks/>
    public CreditNoteBillingReference BillingReference
    {
        get{ return this.billingReferenceField; }
        set{ this.billingReferenceField = value; }
    }

    /// <remarks/>
    public CreditNoteAccountingSupplierParty AccountingSupplierParty
    {
        get{ return this.accountingSupplierPartyField; }
        set{ this.accountingSupplierPartyField = value; }
    }

    /// <remarks/>
    public CreditNoteAccountingCustomerParty AccountingCustomerParty
    {
        get{ return this.accountingCustomerPartyField; }
        set{ this.accountingCustomerPartyField = value; }
    }

    /// <remarks/>
    public CreditNoteTaxTotal TaxTotal
    {
        get{ return this.taxTotalField; }
        set{ this.taxTotalField = value; }
    }

    /// <remarks/>
    public CreditNoteLegalMonetaryTotal LegalMonetaryTotal
    {
        get{ return this.legalMonetaryTotalField; }
        set{ this.legalMonetaryTotalField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("CreditNoteLine")]
    public CreditNoteCreditNoteLine[] CreditNoteLine
    {
        get{ return this.creditNoteLineField; }
        set{ this.creditNoteLineField = value; }
    }

    /// <remarks/>
    public CreditNoteAdditionalInformation AdditionalInformation
    {
        get{ return this.additionalInformationField; }
        set{ this.additionalInformationField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteDiscrepancyResponse
{
    private string referenceIDField;
    private string responseCodeField;
    private string descriptionField;

    /// <remarks/>
    public string ReferenceID
    {
        get{ return this.referenceIDField; }
        set{ this.referenceIDField = value; }
    }

    /// <remarks/>
    public string ResponseCode
    {
        get{ return this.responseCodeField; }
        set{ this.responseCodeField = value; }
    }

    /// <remarks/>
    public string Description
    {
        get{ return this.descriptionField; }
        set{ this.descriptionField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteBillingReference
{
    private CreditNoteBillingReferenceInvoiceDocumentReference invoiceDocumentReferenceField;

    /// <remarks/>
    public CreditNoteBillingReferenceInvoiceDocumentReference InvoiceDocumentReference
    {
        get{ return this.invoiceDocumentReferenceField; }
        set{ this.invoiceDocumentReferenceField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteBillingReferenceInvoiceDocumentReference
{
    private string idField;
    private string documentTypeCodeField;

    /// <remarks/>
    public string ID
    {
        get{ return this.idField; }
        set{ this.idField = value; }
    }

    /// <remarks/>
    public string DocumentTypeCode
    {
        get{ return this.documentTypeCodeField; }
        set{ this.documentTypeCodeField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteAccountingSupplierParty
{
    private string customerAssignedAccountIDField;
    private byte additionalAccountIDField;
    private CreditNoteAccountingSupplierPartyParty partyField;

    /// <remarks/>
    public string CustomerAssignedAccountID
    {
        get{ return this.customerAssignedAccountIDField; }
        set{ this.customerAssignedAccountIDField = value; }
    }

    /// <remarks/>
    public byte AdditionalAccountID
    {
        get{ return this.additionalAccountIDField; }
        set{ this.additionalAccountIDField = value; }
    }

    /// <remarks/>
    public CreditNoteAccountingSupplierPartyParty Party
    {
        get{ return this.partyField; }
        set{ this.partyField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteAccountingSupplierPartyParty
{
    private CreditNoteAccountingSupplierPartyPartyPartyName partyNameField;
    private CreditNoteAccountingSupplierPartyPartyPostalAddress postalAddressField;
    private CreditNoteAccountingSupplierPartyPartyPartyLegalEntity partyLegalEntityField;

    /// <remarks/>
    public CreditNoteAccountingSupplierPartyPartyPartyName PartyName
    {
        get{ return this.partyNameField; }
        set{ this.partyNameField = value; }
    }

    /// <remarks/>
    public CreditNoteAccountingSupplierPartyPartyPostalAddress PostalAddress
    {
        get{ return this.postalAddressField; }
        set{ this.postalAddressField = value; }
    }

    /// <remarks/>
    public CreditNoteAccountingSupplierPartyPartyPartyLegalEntity PartyLegalEntity
    {
        get{ return this.partyLegalEntityField; }
        set{ this.partyLegalEntityField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteAccountingSupplierPartyPartyPartyName
{
    private string nameField;

    /// <remarks/>
    public string Name
    {
        get{ return this.nameField; }
        set{ this.nameField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteAccountingSupplierPartyPartyPostalAddress
{
    private string idField;
    private string streetNameField;
    private string cityNameField;
    private string countrySubentityField;
    private string districtField;
    private CreditNoteAccountingSupplierPartyPartyPostalAddressCountry countryField;

    /// <remarks/>
    public string ID
    {
        get{ return this.idField; }
        set{ this.idField = value; }
    }

    /// <remarks/>
    public string StreetName
    {
        get{ return this.streetNameField; }
        set{ this.streetNameField = value; }
    }

    /// <remarks/>
    public string CityName
    {
        get{ return this.cityNameField; }
        set{ this.cityNameField = value; }
    }

    /// <remarks/>
    public string CountrySubentity
    {
        get{ return this.countrySubentityField; }
        set{ this.countrySubentityField = value; }
    }

    /// <remarks/>
    public string District
    {
        get{ return this.districtField; }
        set{ this.districtField = value; }
    }

    /// <remarks/>
    public CreditNoteAccountingSupplierPartyPartyPostalAddressCountry Country
    {
        get{ return this.countryField; }
        set{ this.countryField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteAccountingSupplierPartyPartyPostalAddressCountry
{
    private string identificationCodeField;

    /// <remarks/>
    public string IdentificationCode
    {
        get{ return this.identificationCodeField; }
        set{ this.identificationCodeField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteAccountingSupplierPartyPartyPartyLegalEntity
{
    private string registrationNameField;

    /// <remarks/>
    public string RegistrationName
    {
        get{ return this.registrationNameField; }
        set{ this.registrationNameField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteAccountingCustomerParty
{

    private string customerAssignedAccountIDField;

    private string additionalAccountIDField;

    private CreditNoteAccountingCustomerPartyParty partyField;

    /// <remarks/>
    public string CustomerAssignedAccountID
    {
        get{ return this.customerAssignedAccountIDField; }
        set{ this.customerAssignedAccountIDField = value; }
    }

    /// <remarks/>
    public string AdditionalAccountID
    {
        get{ return this.additionalAccountIDField; }
        set{ this.additionalAccountIDField = value; }
    }

    /// <remarks/>
    public CreditNoteAccountingCustomerPartyParty Party
    {
        get{ return this.partyField; }
        set{ this.partyField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteAccountingCustomerPartyParty
{
    private CreditNoteAccountingCustomerPartyPartyPartyLegalEntity partyLegalEntityField;

    /// <remarks/>
    public CreditNoteAccountingCustomerPartyPartyPartyLegalEntity PartyLegalEntity
    {
        get{ return this.partyLegalEntityField; }
        set{ this.partyLegalEntityField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteAccountingCustomerPartyPartyPartyLegalEntity
{
    private string registrationNameField;

    /// <remarks/>
    public string RegistrationName
    {
        get{ return this.registrationNameField; }
        set{ this.registrationNameField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteTaxTotal
{
    private decimal taxAmountField;
    private CreditNoteTaxTotalTaxSubtotal taxSubtotalField;

    /// <remarks/>
    public decimal TaxAmount
    {
        get{ return this.taxAmountField; }
        set{ this.taxAmountField = value; }
    }

    /// <remarks/>
    public CreditNoteTaxTotalTaxSubtotal TaxSubtotal
    {
        get{ return this.taxSubtotalField; }
        set{ this.taxSubtotalField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteTaxTotalTaxSubtotal
{
    private decimal taxAmountField;
    private CreditNoteTaxTotalTaxSubtotalTaxCategory taxCategoryField;

    /// <remarks/>
    public decimal TaxAmount
    {
        get{ return this.taxAmountField; }
        set{ this.taxAmountField = value; }
    }

    /// <remarks/>
    public CreditNoteTaxTotalTaxSubtotalTaxCategory TaxCategory
    {
        get{ return this.taxCategoryField; }
        set{ this.taxCategoryField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteTaxTotalTaxSubtotalTaxCategory
{
    private CreditNoteTaxTotalTaxSubtotalTaxCategoryTaxScheme taxSchemeField;

    /// <remarks/>
    public CreditNoteTaxTotalTaxSubtotalTaxCategoryTaxScheme TaxScheme
    {
        get{ return this.taxSchemeField; }
        set{ this.taxSchemeField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteTaxTotalTaxSubtotalTaxCategoryTaxScheme
{
    private ushort idField;
    private string nameField;

    /// <remarks/>
    public ushort ID
    {
        get{ return this.idField; }
        set{ this.idField = value; }
    }

    /// <remarks/>
    public string Name
    {
        get{ return this.nameField; }
        set{ this.nameField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteLegalMonetaryTotal
{
    private ushort payableAmountField;

    /// <remarks/>
    public ushort PayableAmount
    {
        get{ return this.payableAmountField; }
        set{ this.payableAmountField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteCreditNoteLine
{
    private byte idField;
    private byte creditedQuantityField;
    private string unitCodeField;
    private decimal lineExtensionAmountField;
    private CreditNoteCreditNoteLinePricingReference pricingReferenceField;
    private CreditNoteCreditNoteLineTaxTotal taxTotalField;
    private CreditNoteCreditNoteLineItem itemField;
    private CreditNoteCreditNoteLinePrice priceField;

    /// <remarks/>
    public byte ID
    {
        get{ return this.idField; }
        set{ this.idField = value; }
    }

    /// <remarks/>
    public byte CreditedQuantity
    {
        get{ return this.creditedQuantityField; }
        set{ this.creditedQuantityField = value; }
    }

    /// <remarks/>
    public string UnitCode
    {
        get{ return this.unitCodeField; }
        set{ this.unitCodeField = value; }
    }

    /// <remarks/>
    public decimal LineExtensionAmount
    {
        get{ return this.lineExtensionAmountField; }
        set{ this.lineExtensionAmountField = value; }
    }

    /// <remarks/>
    public CreditNoteCreditNoteLinePricingReference PricingReference
    {
        get{ return this.pricingReferenceField; }
        set{ this.pricingReferenceField = value; }
    }

    /// <remarks/>
    public CreditNoteCreditNoteLineTaxTotal TaxTotal
    {
        get{ return this.taxTotalField; }
        set{ this.taxTotalField = value; }
    }

    /// <remarks/>
    public CreditNoteCreditNoteLineItem Item
    {
        get{ return this.itemField; }
        set{ this.itemField = value; }
    }

    /// <remarks/>
    public CreditNoteCreditNoteLinePrice Price
    {
        get{ return this.priceField; }
        set{ this.priceField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteCreditNoteLinePricingReference
{
    private CreditNoteCreditNoteLinePricingReferenceAlternativeConditionPrice alternativeConditionPriceField;

    /// <remarks/>
    public CreditNoteCreditNoteLinePricingReferenceAlternativeConditionPrice AlternativeConditionPrice
    {
        get{ return this.alternativeConditionPriceField; }
        set{ this.alternativeConditionPriceField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteCreditNoteLinePricingReferenceAlternativeConditionPrice
{
    private ushort priceAmountField;
    private string priceTypeCodeField;

    /// <remarks/>
    public ushort PriceAmount
    {
        get{ return this.priceAmountField; }
        set{ this.priceAmountField = value; }
    }

    /// <remarks/>
    public string PriceTypeCode
    {
        get{ return this.priceTypeCodeField; }
        set{ this.priceTypeCodeField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteCreditNoteLineTaxTotal
{
    private decimal taxAmountField;
    private CreditNoteCreditNoteLineTaxTotalTaxSubtotal taxSubtotalField;

    /// <remarks/>
    public decimal TaxAmount
    {
        get{ return this.taxAmountField; }
        set{ this.taxAmountField = value; }
    }

    /// <remarks/>
    public CreditNoteCreditNoteLineTaxTotalTaxSubtotal TaxSubtotal
    {
        get{ return this.taxSubtotalField; }
        set{ this.taxSubtotalField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteCreditNoteLineTaxTotalTaxSubtotal
{
    private decimal taxAmountField;
    private CreditNoteCreditNoteLineTaxTotalTaxSubtotalTaxCategory taxCategoryField;

    /// <remarks/>
    public decimal TaxAmount
    {
        get{ return this.taxAmountField; }
        set{ this.taxAmountField = value; }
    }

    /// <remarks/>
    public CreditNoteCreditNoteLineTaxTotalTaxSubtotalTaxCategory TaxCategory
    {
        get{ return this.taxCategoryField; }
        set{ this.taxCategoryField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteCreditNoteLineTaxTotalTaxSubtotalTaxCategory
{
    private byte taxExemptionReasonCodeField;
    private CreditNoteCreditNoteLineTaxTotalTaxSubtotalTaxCategoryTaxScheme taxSchemeField;

    /// <remarks/>
    public byte TaxExemptionReasonCode
    {
        get{ return this.taxExemptionReasonCodeField; }
        set{ this.taxExemptionReasonCodeField = value; }
    }

    /// <remarks/>
    public CreditNoteCreditNoteLineTaxTotalTaxSubtotalTaxCategoryTaxScheme TaxScheme
    {
        get{ return this.taxSchemeField; }
        set{ this.taxSchemeField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteCreditNoteLineTaxTotalTaxSubtotalTaxCategoryTaxScheme
{
    private ushort idField;
    private string nameField;
    private string taxTypeCodeField;

    /// <remarks/>
    public ushort ID
    {
        get{ return this.idField; }
        set{ this.idField = value; }
    }

    /// <remarks/>
    public string Name
    {
        get{ return this.nameField; }
        set{ this.nameField = value; }
    }

    /// <remarks/>
    public string TaxTypeCode
    {
        get{ return this.taxTypeCodeField; }
        set{ this.taxTypeCodeField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteCreditNoteLineItem
{
    private string descriptionField;
    private CreditNoteCreditNoteLineItemSellersItemIdentification sellersItemIdentificationField;

    /// <remarks/>
    public string Description
    {
        get{ return this.descriptionField; }
        set{ this.descriptionField = value; }
    }

    /// <remarks/>
    public CreditNoteCreditNoteLineItemSellersItemIdentification SellersItemIdentification
    {
        get{ return this.sellersItemIdentificationField; }
        set{ this.sellersItemIdentificationField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteCreditNoteLineItemSellersItemIdentification
{
    private string idField;

    /// <remarks/>
    public string ID
    {
        get{ return this.idField; }
        set{ this.idField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteCreditNoteLinePrice
{
    private decimal priceAmountField;

    /// <remarks/>
    public decimal PriceAmount
    {
        get{ return this.priceAmountField; }
        set{ this.priceAmountField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteAdditionalInformation
{
    private CreditNoteAdditionalInformationAdditionalMonetaryTotal additionalMonetaryTotalField;
    private CreditNoteAdditionalInformationAdditionalProperty additionalPropertyField;

    /// <remarks/>
    public CreditNoteAdditionalInformationAdditionalMonetaryTotal AdditionalMonetaryTotal
    {
        get{ return this.additionalMonetaryTotalField; }
        set{ this.additionalMonetaryTotalField = value; }
    }

    /// <remarks/>
    public CreditNoteAdditionalInformationAdditionalProperty AdditionalProperty
    {
        get{ return this.additionalPropertyField; }
        set{ this.additionalPropertyField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteAdditionalInformationAdditionalMonetaryTotal
{
    private ushort idField;
    private decimal payableAmountField;

    /// <remarks/>
    public ushort ID
    {
        get{ return this.idField; }
        set{ this.idField = value; }
    }

    /// <remarks/>
    public decimal PayableAmount
    {
        get{ return this.payableAmountField; }
        set{ this.payableAmountField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class CreditNoteAdditionalInformationAdditionalProperty
{
    private ushort idField;
    private string valueField;

    /// <remarks/>
    public ushort ID
    {
        get{ return this.idField; }
        set{ this.idField = value; }
    }

    /// <remarks/>
    public string Value
    {
        get{ return this.valueField; }
        set{ this.valueField = value; }
    }
}