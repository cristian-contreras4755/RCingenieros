//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace APU.Negocio
//{
//    class NotaDebitoGasolutionsXml
//    {
//    }
//}



// NOTA: El código generado puede requerir, como mínimo, .NET Framework 4.5 o .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class DebitNote
{
    private string idField;
    private System.DateTime issueDateField;
    private string documentCurrencyCodeField;
    private DebitNoteDiscrepancyResponse discrepancyResponseField;
    private DebitNoteBillingReference billingReferenceField;
    private DebitNoteAccountingSupplierParty accountingSupplierPartyField;
    private DebitNoteAccountingCustomerParty accountingCustomerPartyField;
    private DebitNoteRequestedMonetaryTotal requestedMonetaryTotalField;
    private DebitNoteDebitNoteLine[] debitNoteLineField;
    private DebitNoteAdditionalInformation additionalInformationField;

    /// <remarks/>
    public string ID
    {
        get { return this.idField; }
        set { this.idField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
    public System.DateTime IssueDate
    {
        get { return this.issueDateField; }
        set { this.issueDateField = value; }
    }

    /// <remarks/>
    public string DocumentCurrencyCode
    {
        get { return this.documentCurrencyCodeField; }
        set { this.documentCurrencyCodeField = value; }
    }

    /// <remarks/>
    public DebitNoteDiscrepancyResponse DiscrepancyResponse
    {
        get { return this.discrepancyResponseField; }
        set { this.discrepancyResponseField = value; }
    }

    /// <remarks/>
    public DebitNoteBillingReference BillingReference
    {
        get { return this.billingReferenceField; }
        set { this.billingReferenceField = value; }
    }

    /// <remarks/>
    public DebitNoteAccountingSupplierParty AccountingSupplierParty
    {
        get { return this.accountingSupplierPartyField; }
        set { this.accountingSupplierPartyField = value; }
    }

    /// <remarks/>
    public DebitNoteAccountingCustomerParty AccountingCustomerParty
    {
        get { return this.accountingCustomerPartyField; }
        set { this.accountingCustomerPartyField = value; }
    }

    /// <remarks/>
    public DebitNoteRequestedMonetaryTotal RequestedMonetaryTotal
    {
        get { return this.requestedMonetaryTotalField; }
        set { this.requestedMonetaryTotalField = value; }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("DebitNoteLine")]
    public DebitNoteDebitNoteLine[] DebitNoteLine
    {
        get { return this.debitNoteLineField; }
        set { this.debitNoteLineField = value; }
    }

    /// <remarks/>
    public DebitNoteAdditionalInformation AdditionalInformation
    {
        get { return this.additionalInformationField; }
        set { this.additionalInformationField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DebitNoteDiscrepancyResponse
{
    private string referenceIDField;
    private string responseCodeField;
    private string descriptionField;

    /// <remarks/>
    public string ReferenceID
    {
        get { return this.referenceIDField; }
        set { this.referenceIDField = value; }
    }

    /// <remarks/>
    public string ResponseCode
    {
        get { return this.responseCodeField; }
        set { this.responseCodeField = value; }
    }

    /// <remarks/>
    public string Description
    {
        get { return this.descriptionField; }
        set { this.descriptionField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DebitNoteBillingReference
{
    private DebitNoteBillingReferenceInvoiceDocumentReference invoiceDocumentReferenceField;

    /// <remarks/>
    public DebitNoteBillingReferenceInvoiceDocumentReference InvoiceDocumentReference
    {
        get { return this.invoiceDocumentReferenceField; }
        set { this.invoiceDocumentReferenceField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DebitNoteBillingReferenceInvoiceDocumentReference
{
    private string idField;
    private string documentTypeCodeField;

    /// <remarks/>
    public string ID
    {
        get { return this.idField; }
        set { this.idField = value; }
    }

    /// <remarks/>
    public string DocumentTypeCode
    {
        get { return this.documentTypeCodeField; }
        set { this.documentTypeCodeField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DebitNoteAccountingSupplierParty
{
    private string customerAssignedAccountIDField;
    private string additionalAccountIDField;
    private DebitNoteAccountingSupplierPartyParty partyField;

    /// <remarks/>
    public string CustomerAssignedAccountID
    {
        get { return this.customerAssignedAccountIDField; }
        set { this.customerAssignedAccountIDField = value; }
    }

    /// <remarks/>
    public string AdditionalAccountID
    {
        get { return this.additionalAccountIDField; }
        set { this.additionalAccountIDField = value; }
    }

    /// <remarks/>
    public DebitNoteAccountingSupplierPartyParty Party
    {
        get { return this.partyField; }
        set { this.partyField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DebitNoteAccountingSupplierPartyParty
{
    private DebitNoteAccountingSupplierPartyPartyPartyName partyNameField;
    private DebitNoteAccountingSupplierPartyPartyPostalAddress postalAddressField;
    private DebitNoteAccountingSupplierPartyPartyPartyLegalEntity partyLegalEntityField;

    /// <remarks/>
    public DebitNoteAccountingSupplierPartyPartyPartyName PartyName
    {
        get { return this.partyNameField; }
        set { this.partyNameField = value; }
    }

    /// <remarks/>
    public DebitNoteAccountingSupplierPartyPartyPostalAddress PostalAddress
    {
        get { return this.postalAddressField; }
        set { this.postalAddressField = value; }
    }

    /// <remarks/>
    public DebitNoteAccountingSupplierPartyPartyPartyLegalEntity PartyLegalEntity
    {
        get { return this.partyLegalEntityField; }
        set { this.partyLegalEntityField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DebitNoteAccountingSupplierPartyPartyPartyName
{
    private string nameField;

    /// <remarks/>
    public string Name
    {
        get { return this.nameField; }
        set { this.nameField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DebitNoteAccountingSupplierPartyPartyPostalAddress
{
    private string idField;
    private string streetNameField;
    private string cityNameField;
    private string countrySubentityField;
    private string districtField;
    private DebitNoteAccountingSupplierPartyPartyPostalAddressCountry countryField;

    /// <remarks/>
    public string ID
    {
        get { return this.idField; }
        set { this.idField = value; }
    }

    /// <remarks/>
    public string StreetName
    {
        get { return this.streetNameField; }
        set { this.streetNameField = value; }
    }

    /// <remarks/>
    public string CityName
    {
        get { return this.cityNameField; }
        set { this.cityNameField = value; }
    }

    /// <remarks/>
    public string CountrySubentity
    {
        get { return this.countrySubentityField; }
        set { this.countrySubentityField = value; }
    }

    /// <remarks/>
    public string District
    {
        get { return this.districtField; }
        set { this.districtField = value; }
    }

    /// <remarks/>
    public DebitNoteAccountingSupplierPartyPartyPostalAddressCountry Country
    {
        get { return this.countryField; }
        set { this.countryField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DebitNoteAccountingSupplierPartyPartyPostalAddressCountry
{
    private string identificationCodeField;

    /// <remarks/>
    public string IdentificationCode
    {
        get { return this.identificationCodeField; }
        set { this.identificationCodeField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DebitNoteAccountingSupplierPartyPartyPartyLegalEntity
{
    private string registrationNameField;

    /// <remarks/>
    public string RegistrationName
    {
        get { return this.registrationNameField; }
        set { this.registrationNameField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DebitNoteAccountingCustomerParty
{
    private string customerAssignedAccountIDField;
    private string additionalAccountIDField;
    private DebitNoteAccountingCustomerPartyParty partyField;

    /// <remarks/>
    public string CustomerAssignedAccountID
    {
        get { return this.customerAssignedAccountIDField; }
        set { this.customerAssignedAccountIDField = value; }
    }

    /// <remarks/>
    public string AdditionalAccountID
    {
        get { return this.additionalAccountIDField; }
        set { this.additionalAccountIDField = value; }
    }

    /// <remarks/>
    public DebitNoteAccountingCustomerPartyParty Party
    {
        get { return this.partyField; }
        set { this.partyField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DebitNoteAccountingCustomerPartyParty
{
    private DebitNoteAccountingCustomerPartyPartyPartyLegalEntity partyLegalEntityField;

    /// <remarks/>
    public DebitNoteAccountingCustomerPartyPartyPartyLegalEntity PartyLegalEntity
    {
        get { return this.partyLegalEntityField; }
        set { this.partyLegalEntityField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DebitNoteAccountingCustomerPartyPartyPartyLegalEntity
{
    private string registrationNameField;

    /// <remarks/>
    public string RegistrationName
    {
        get { return this.registrationNameField; }
        set { this.registrationNameField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DebitNoteRequestedMonetaryTotal
{
    private decimal payableAmountField;

    /// <remarks/>
    public decimal PayableAmount
    {
        get { return this.payableAmountField; }
        set { this.payableAmountField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DebitNoteDebitNoteLine
{
    private string idField;
    private decimal debitedQuantityField;
    private string unitCodeField;
    private decimal lineExtensionAmountField;
    private DebitNoteDebitNoteLinePricingReference pricingReferenceField;
    private DebitNoteDebitNoteLineTaxTotal taxTotalField;
    private DebitNoteDebitNoteLineItem itemField;
    private DebitNoteDebitNoteLinePrice priceField;

    /// <remarks/>
    public string ID
    {
        get { return this.idField; }
        set { this.idField = value; }
    }

    /// <remarks/>
    public decimal DebitedQuantity
    {
        get { return this.debitedQuantityField; }
        set { this.debitedQuantityField = value; }
    }

    /// <remarks/>
    public string UnitCode
    {
        get { return this.unitCodeField; }
        set { this.unitCodeField = value; }
    }

    /// <remarks/>
    public decimal LineExtensionAmount
    {
        get { return this.lineExtensionAmountField; }
        set { this.lineExtensionAmountField = value; }
    }

    /// <remarks/>
    public DebitNoteDebitNoteLinePricingReference PricingReference
    {
        get { return this.pricingReferenceField; }
        set { this.pricingReferenceField = value; }
    }

    /// <remarks/>
    public DebitNoteDebitNoteLineTaxTotal TaxTotal
    {
        get { return this.taxTotalField; }
        set { this.taxTotalField = value; }
    }

    /// <remarks/>
    public DebitNoteDebitNoteLineItem Item
    {
        get { return this.itemField; }
        set { this.itemField = value; }
    }

    /// <remarks/>
    public DebitNoteDebitNoteLinePrice Price
    {
        get { return this.priceField; }
        set { this.priceField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DebitNoteDebitNoteLinePricingReference
{
    private DebitNoteDebitNoteLinePricingReferenceAlternativeConditionPrice alternativeConditionPriceField;

    /// <remarks/>
    public DebitNoteDebitNoteLinePricingReferenceAlternativeConditionPrice AlternativeConditionPrice
    {
        get { return this.alternativeConditionPriceField; }
        set { this.alternativeConditionPriceField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DebitNoteDebitNoteLinePricingReferenceAlternativeConditionPrice
{
    private decimal priceAmountField;
    private string priceTypeCodeField;

    /// <remarks/>
    public decimal PriceAmount
    {
        get { return this.priceAmountField; }
        set { this.priceAmountField = value; }
    }

    /// <remarks/>
    public string PriceTypeCode
    {
        get { return this.priceTypeCodeField; }
        set { this.priceTypeCodeField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DebitNoteDebitNoteLineTaxTotal
{
    private decimal taxAmountField;
    private DebitNoteDebitNoteLineTaxTotalTaxSubtotal taxSubtotalField;

    /// <remarks/>
    public decimal TaxAmount
    {
        get { return this.taxAmountField; }
        set { this.taxAmountField = value; }
    }

    /// <remarks/>
    public DebitNoteDebitNoteLineTaxTotalTaxSubtotal TaxSubtotal
    {
        get { return this.taxSubtotalField; }
        set { this.taxSubtotalField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DebitNoteDebitNoteLineTaxTotalTaxSubtotal
{
    private decimal taxAmountField;
    private DebitNoteDebitNoteLineTaxTotalTaxSubtotalTaxCategory taxCategoryField;

    /// <remarks/>
    public decimal TaxAmount
    {
        get { return this.taxAmountField; }
        set { this.taxAmountField = value; }
    }

    /// <remarks/>
    public DebitNoteDebitNoteLineTaxTotalTaxSubtotalTaxCategory TaxCategory
    {
        get { return this.taxCategoryField; }
        set { this.taxCategoryField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DebitNoteDebitNoteLineTaxTotalTaxSubtotalTaxCategory
{
    private byte taxExemptionReasonCodeField;
    private DebitNoteDebitNoteLineTaxTotalTaxSubtotalTaxCategoryTaxScheme taxSchemeField;

    /// <remarks/>
    public byte TaxExemptionReasonCode
    {
        get { return this.taxExemptionReasonCodeField; }
        set { this.taxExemptionReasonCodeField = value; }
    }

    /// <remarks/>
    public DebitNoteDebitNoteLineTaxTotalTaxSubtotalTaxCategoryTaxScheme TaxScheme
    {
        get { return this.taxSchemeField; }
        set { this.taxSchemeField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DebitNoteDebitNoteLineTaxTotalTaxSubtotalTaxCategoryTaxScheme
{
    private ushort idField;
    private string nameField;
    private string taxTypeCodeField;

    /// <remarks/>
    public ushort ID
    {
        get { return this.idField; }
        set { this.idField = value; }
    }

    /// <remarks/>
    public string Name
    {
        get { return this.nameField; }
        set { this.nameField = value; }
    }

    /// <remarks/>
    public string TaxTypeCode
    {
        get { return this.taxTypeCodeField; }
        set { this.taxTypeCodeField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DebitNoteDebitNoteLineItem
{
    private string descriptionField;
    private DebitNoteDebitNoteLineItemSellersItemIdentification sellersItemIdentificationField;

    /// <remarks/>
    public string Description
    {
        get { return this.descriptionField; }
        set { this.descriptionField = value; }
    }

    /// <remarks/>
    public DebitNoteDebitNoteLineItemSellersItemIdentification SellersItemIdentification
    {
        get { return this.sellersItemIdentificationField; }
        set { this.sellersItemIdentificationField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DebitNoteDebitNoteLineItemSellersItemIdentification
{
    private string idField;

    /// <remarks/>
    public string ID
    {
        get { return this.idField; }
        set { this.idField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DebitNoteDebitNoteLinePrice
{
    private decimal priceAmountField;

    /// <remarks/>
    public decimal PriceAmount
    {
        get { return this.priceAmountField; }
        set { this.priceAmountField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DebitNoteAdditionalInformation
{
    private DebitNoteAdditionalInformationAdditionalMonetaryTotal additionalMonetaryTotalField;

    /// <remarks/>
    public DebitNoteAdditionalInformationAdditionalMonetaryTotal AdditionalMonetaryTotal
    {
        get { return this.additionalMonetaryTotalField; }
        set { this.additionalMonetaryTotalField = value; }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class DebitNoteAdditionalInformationAdditionalMonetaryTotal
{
    private ushort idField;
    private decimal payableAmountField;

    /// <remarks/>
    public ushort ID
    {
        get { return this.idField; }
        set { this.idField = value; }
    }

    /// <remarks/>
    public decimal PayableAmount
    {
        get { return this.payableAmountField; }
        set { this.payableAmountField = value; }
    }
}