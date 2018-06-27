using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APU.Gasolutions
{
    public class FacturaPrueba
    {
    }
}


//// NOTA: El código generado puede requerir, como mínimo, .NET Framework 4.5 o .NET Core/Standard 2.0.
///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2")]
//[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2", IsNullable = false)]
//public partial class Invoice
//{

//    private UBLExtensionsUBLExtension[] uBLExtensionsField;

//    private decimal uBLVersionIDField;

//    private decimal customizationIDField;

//    private string idField;

//    private System.DateTime issueDateField;

//    private byte invoiceTypeCodeField;

//    private string documentCurrencyCodeField;

//    private Signature signatureField;

//    private AccountingSupplierParty accountingSupplierPartyField;

//    private AccountingCustomerParty accountingCustomerPartyField;

//    private TaxTotal taxTotalField;

//    private LegalMonetaryTotal legalMonetaryTotalField;

//    private InvoiceLine invoiceLineField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlArrayAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")]
//    [System.Xml.Serialization.XmlArrayItemAttribute("UBLExtension", IsNullable = false)]
//    public UBLExtensionsUBLExtension[] UBLExtensions
//    {
//        get
//        {
//            return this.uBLExtensionsField;
//        }
//        set
//        {
//            this.uBLExtensionsField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public decimal UBLVersionID
//    {
//        get
//        {
//            return this.uBLVersionIDField;
//        }
//        set
//        {
//            this.uBLVersionIDField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public decimal CustomizationID
//    {
//        get
//        {
//            return this.customizationIDField;
//        }
//        set
//        {
//            this.customizationIDField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string ID
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
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2", DataType = "date")]
//    public System.DateTime IssueDate
//    {
//        get
//        {
//            return this.issueDateField;
//        }
//        set
//        {
//            this.issueDateField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public byte InvoiceTypeCode
//    {
//        get
//        {
//            return this.invoiceTypeCodeField;
//        }
//        set
//        {
//            this.invoiceTypeCodeField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string DocumentCurrencyCode
//    {
//        get
//        {
//            return this.documentCurrencyCodeField;
//        }
//        set
//        {
//            this.documentCurrencyCodeField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//    public Signature Signature
//    {
//        get
//        {
//            return this.signatureField;
//        }
//        set
//        {
//            this.signatureField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//    public AccountingSupplierParty AccountingSupplierParty
//    {
//        get
//        {
//            return this.accountingSupplierPartyField;
//        }
//        set
//        {
//            this.accountingSupplierPartyField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//    public AccountingCustomerParty AccountingCustomerParty
//    {
//        get
//        {
//            return this.accountingCustomerPartyField;
//        }
//        set
//        {
//            this.accountingCustomerPartyField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//    public TaxTotal TaxTotal
//    {
//        get
//        {
//            return this.taxTotalField;
//        }
//        set
//        {
//            this.taxTotalField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//    public LegalMonetaryTotal LegalMonetaryTotal
//    {
//        get
//        {
//            return this.legalMonetaryTotalField;
//        }
//        set
//        {
//            this.legalMonetaryTotalField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//    public InvoiceLine InvoiceLine
//    {
//        get
//        {
//            return this.invoiceLineField;
//        }
//        set
//        {
//            this.invoiceLineField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")]
//public partial class UBLExtensionsUBLExtension
//{
//    private UBLExtensionsUBLExtensionExtensionContent extensionContentField;

//    /// <remarks/>
//    public UBLExtensionsUBLExtensionExtensionContent ExtensionContent
//    {
//        get
//        {
//            return this.extensionContentField;
//        }
//        set
//        {
//            this.extensionContentField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")]
//public partial class UBLExtensionsUBLExtensionExtensionContent
//{
//    private AdditionalInformation additionalInformationField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1")]
//    public AdditionalInformation AdditionalInformation
//    {
//        get
//        {
//            return this.additionalInformationField;
//        }
//        set
//        {
//            this.additionalInformationField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1")]
//[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1", IsNullable = false)]
//public partial class AdditionalInformation
//{

//    private AdditionalInformationAdditionalMonetaryTotal[] additionalMonetaryTotalField;

//    private AdditionalInformationAdditionalProperty additionalPropertyField;

//    private AdditionalInformationSUNATTransaction sUNATTransactionField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute("AdditionalMonetaryTotal")]
//    public AdditionalInformationAdditionalMonetaryTotal[] AdditionalMonetaryTotal
//    {
//        get
//        {
//            return this.additionalMonetaryTotalField;
//        }
//        set
//        {
//            this.additionalMonetaryTotalField = value;
//        }
//    }

//    /// <remarks/>
//    public AdditionalInformationAdditionalProperty AdditionalProperty
//    {
//        get
//        {
//            return this.additionalPropertyField;
//        }
//        set
//        {
//            this.additionalPropertyField = value;
//        }
//    }

//    /// <remarks/>
//    public AdditionalInformationSUNATTransaction SUNATTransaction
//    {
//        get
//        {
//            return this.sUNATTransactionField;
//        }
//        set
//        {
//            this.sUNATTransactionField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1")]
//public partial class AdditionalInformationAdditionalMonetaryTotal
//{

//    private string idField;

//    private PayableAmount payableAmountField;

//    private string[] textField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string ID
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
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public PayableAmount PayableAmount
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

//    /// <remarks/>
//    [System.Xml.Serialization.XmlTextAttribute()]
//    public string[] Text
//    {
//        get
//        {
//            return this.textField;
//        }
//        set
//        {
//            this.textField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2", IsNullable = false)]
//public partial class PayableAmount
//{

//    private string currencyIDField;

//    private decimal valueField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlAttributeAttribute()]
//    public string currencyID
//    {
//        get
//        {
//            return this.currencyIDField;
//        }
//        set
//        {
//            this.currencyIDField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlTextAttribute()]
//    public decimal Value
//    {
//        get
//        {
//            return this.valueField;
//        }
//        set
//        {
//            this.valueField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1")]
//public partial class AdditionalInformationAdditionalProperty
//{

//    private string idField;

//    private string valueField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string ID
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
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string Value
//    {
//        get
//        {
//            return this.valueField;
//        }
//        set
//        {
//            this.valueField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1")]
//public partial class AdditionalInformationSUNATTransaction
//{

//    private string idField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string ID
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
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2", IsNullable = false)]
//public partial class Signature
//{

//    private string idField;

//    private SignatureSignatoryParty signatoryPartyField;

//    private SignatureDigitalSignatureAttachment digitalSignatureAttachmentField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string ID
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
//    public SignatureSignatoryParty SignatoryParty
//    {
//        get
//        {
//            return this.signatoryPartyField;
//        }
//        set
//        {
//            this.signatoryPartyField = value;
//        }
//    }

//    /// <remarks/>
//    public SignatureDigitalSignatureAttachment DigitalSignatureAttachment
//    {
//        get
//        {
//            return this.digitalSignatureAttachmentField;
//        }
//        set
//        {
//            this.digitalSignatureAttachmentField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//public partial class SignatureSignatoryParty
//{

//    private string partyIdentificationField;

//    private SignatureSignatoryPartyPartyName partyNameField;

//    /// <remarks/>
//    public string PartyIdentification
//    {
//        get
//        {
//            return this.partyIdentificationField;
//        }
//        set
//        {
//            this.partyIdentificationField = value;
//        }
//    }

//    /// <remarks/>
//    public SignatureSignatoryPartyPartyName PartyName
//    {
//        get
//        {
//            return this.partyNameField;
//        }
//        set
//        {
//            this.partyNameField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//public partial class SignatureSignatoryPartyPartyName
//{

//    private string nameField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string Name
//    {
//        get
//        {
//            return this.nameField;
//        }
//        set
//        {
//            this.nameField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//public partial class SignatureDigitalSignatureAttachment
//{

//    private SignatureDigitalSignatureAttachmentExternalReference externalReferenceField;

//    /// <remarks/>
//    public SignatureDigitalSignatureAttachmentExternalReference ExternalReference
//    {
//        get
//        {
//            return this.externalReferenceField;
//        }
//        set
//        {
//            this.externalReferenceField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//public partial class SignatureDigitalSignatureAttachmentExternalReference
//{

//    private string uRIField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string URI
//    {
//        get
//        {
//            return this.uRIField;
//        }
//        set
//        {
//            this.uRIField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2", IsNullable = false)]
//public partial class AccountingSupplierParty
//{

//    private string customerAssignedAccountIDField;

//    private string additionalAccountIDField;

//    private AccountingSupplierPartyParty partyField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string CustomerAssignedAccountID
//    {
//        get
//        {
//            return this.customerAssignedAccountIDField;
//        }
//        set
//        {
//            this.customerAssignedAccountIDField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string AdditionalAccountID
//    {
//        get
//        {
//            return this.additionalAccountIDField;
//        }
//        set
//        {
//            this.additionalAccountIDField = value;
//        }
//    }

//    /// <remarks/>
//    public AccountingSupplierPartyParty Party
//    {
//        get
//        {
//            return this.partyField;
//        }
//        set
//        {
//            this.partyField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//public partial class AccountingSupplierPartyParty
//{

//    private AccountingSupplierPartyPartyPartyName partyNameField;

//    private AccountingSupplierPartyPartyPostalAddress postalAddressField;

//    private AccountingSupplierPartyPartyPartyLegalEntity partyLegalEntityField;

//    /// <remarks/>
//    public AccountingSupplierPartyPartyPartyName PartyName
//    {
//        get
//        {
//            return this.partyNameField;
//        }
//        set
//        {
//            this.partyNameField = value;
//        }
//    }

//    /// <remarks/>
//    public AccountingSupplierPartyPartyPostalAddress PostalAddress
//    {
//        get
//        {
//            return this.postalAddressField;
//        }
//        set
//        {
//            this.postalAddressField = value;
//        }
//    }

//    /// <remarks/>
//    public AccountingSupplierPartyPartyPartyLegalEntity PartyLegalEntity
//    {
//        get
//        {
//            return this.partyLegalEntityField;
//        }
//        set
//        {
//            this.partyLegalEntityField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//public partial class AccountingSupplierPartyPartyPartyName
//{

//    private string nameField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string Name
//    {
//        get
//        {
//            return this.nameField;
//        }
//        set
//        {
//            this.nameField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//public partial class AccountingSupplierPartyPartyPostalAddress
//{

//    private string idField;

//    private string streetNameField;

//    private string citySubdivisionNameField;

//    private string cityNameField;

//    private string countrySubentityField;

//    private string districtField;

//    private AccountingSupplierPartyPartyPostalAddressCountry countryField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string ID
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
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string StreetName
//    {
//        get
//        {
//            return this.streetNameField;
//        }
//        set
//        {
//            this.streetNameField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string CitySubdivisionName
//    {
//        get
//        {
//            return this.citySubdivisionNameField;
//        }
//        set
//        {
//            this.citySubdivisionNameField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string CityName
//    {
//        get
//        {
//            return this.cityNameField;
//        }
//        set
//        {
//            this.cityNameField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string CountrySubentity
//    {
//        get
//        {
//            return this.countrySubentityField;
//        }
//        set
//        {
//            this.countrySubentityField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string District
//    {
//        get
//        {
//            return this.districtField;
//        }
//        set
//        {
//            this.districtField = value;
//        }
//    }

//    /// <remarks/>
//    public AccountingSupplierPartyPartyPostalAddressCountry Country
//    {
//        get
//        {
//            return this.countryField;
//        }
//        set
//        {
//            this.countryField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//public partial class AccountingSupplierPartyPartyPostalAddressCountry
//{

//    private string identificationCodeField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string IdentificationCode
//    {
//        get
//        {
//            return this.identificationCodeField;
//        }
//        set
//        {
//            this.identificationCodeField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//public partial class AccountingSupplierPartyPartyPartyLegalEntity
//{

//    private string registrationNameField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string RegistrationName
//    {
//        get
//        {
//            return this.registrationNameField;
//        }
//        set
//        {
//            this.registrationNameField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2", IsNullable = false)]
//public partial class AccountingCustomerParty
//{

//    private ulong customerAssignedAccountIDField;

//    private byte additionalAccountIDField;

//    private AccountingCustomerPartyParty partyField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public ulong CustomerAssignedAccountID
//    {
//        get
//        {
//            return this.customerAssignedAccountIDField;
//        }
//        set
//        {
//            this.customerAssignedAccountIDField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public byte AdditionalAccountID
//    {
//        get
//        {
//            return this.additionalAccountIDField;
//        }
//        set
//        {
//            this.additionalAccountIDField = value;
//        }
//    }

//    /// <remarks/>
//    public AccountingCustomerPartyParty Party
//    {
//        get
//        {
//            return this.partyField;
//        }
//        set
//        {
//            this.partyField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//public partial class AccountingCustomerPartyParty
//{

//    private AccountingCustomerPartyPartyPartyLegalEntity partyLegalEntityField;

//    /// <remarks/>
//    public AccountingCustomerPartyPartyPartyLegalEntity PartyLegalEntity
//    {
//        get
//        {
//            return this.partyLegalEntityField;
//        }
//        set
//        {
//            this.partyLegalEntityField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//public partial class AccountingCustomerPartyPartyPartyLegalEntity
//{

//    private string registrationNameField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string RegistrationName
//    {
//        get
//        {
//            return this.registrationNameField;
//        }
//        set
//        {
//            this.registrationNameField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2", IsNullable = false)]
//public partial class TaxTotal
//{

//    private TaxAmount taxAmountField;

//    private TaxTotalTaxSubtotal taxSubtotalField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public TaxAmount TaxAmount
//    {
//        get
//        {
//            return this.taxAmountField;
//        }
//        set
//        {
//            this.taxAmountField = value;
//        }
//    }

//    /// <remarks/>
//    public TaxTotalTaxSubtotal TaxSubtotal
//    {
//        get
//        {
//            return this.taxSubtotalField;
//        }
//        set
//        {
//            this.taxSubtotalField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2", IsNullable = false)]
//public partial class TaxAmount
//{

//    private string currencyIDField;

//    private decimal valueField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlAttributeAttribute()]
//    public string currencyID
//    {
//        get
//        {
//            return this.currencyIDField;
//        }
//        set
//        {
//            this.currencyIDField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlTextAttribute()]
//    public decimal Value
//    {
//        get
//        {
//            return this.valueField;
//        }
//        set
//        {
//            this.valueField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//public partial class TaxTotalTaxSubtotal
//{

//    private TaxAmount taxAmountField;

//    private TaxTotalTaxSubtotalTaxCategory taxCategoryField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public TaxAmount TaxAmount
//    {
//        get
//        {
//            return this.taxAmountField;
//        }
//        set
//        {
//            this.taxAmountField = value;
//        }
//    }

//    /// <remarks/>
//    public TaxTotalTaxSubtotalTaxCategory TaxCategory
//    {
//        get
//        {
//            return this.taxCategoryField;
//        }
//        set
//        {
//            this.taxCategoryField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//public partial class TaxTotalTaxSubtotalTaxCategory
//{

//    private TaxTotalTaxSubtotalTaxCategoryTaxScheme taxSchemeField;

//    /// <remarks/>
//    public TaxTotalTaxSubtotalTaxCategoryTaxScheme TaxScheme
//    {
//        get
//        {
//            return this.taxSchemeField;
//        }
//        set
//        {
//            this.taxSchemeField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//public partial class TaxTotalTaxSubtotalTaxCategoryTaxScheme
//{

//    private string idField;

//    private string nameField;

//    private string taxTypeCodeField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string ID
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
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string Name
//    {
//        get
//        {
//            return this.nameField;
//        }
//        set
//        {
//            this.nameField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string TaxTypeCode
//    {
//        get
//        {
//            return this.taxTypeCodeField;
//        }
//        set
//        {
//            this.taxTypeCodeField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2", IsNullable = false)]
//public partial class LegalMonetaryTotal
//{

//    private PayableAmount payableAmountField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public PayableAmount PayableAmount
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

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2", IsNullable = false)]
//public partial class InvoiceLine
//{

//    private string idField;

//    private InvoicedQuantity invoicedQuantityField;

//    private LineExtensionAmount lineExtensionAmountField;

//    private InvoiceLinePricingReference pricingReferenceField;

//    private InvoiceLineTaxTotal taxTotalField;

//    private InvoiceLineItem itemField;

//    private InvoiceLinePrice priceField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string ID
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
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public InvoicedQuantity InvoicedQuantity
//    {
//        get
//        {
//            return this.invoicedQuantityField;
//        }
//        set
//        {
//            this.invoicedQuantityField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public LineExtensionAmount LineExtensionAmount
//    {
//        get
//        {
//            return this.lineExtensionAmountField;
//        }
//        set
//        {
//            this.lineExtensionAmountField = value;
//        }
//    }

//    /// <remarks/>
//    public InvoiceLinePricingReference PricingReference
//    {
//        get
//        {
//            return this.pricingReferenceField;
//        }
//        set
//        {
//            this.pricingReferenceField = value;
//        }
//    }

//    /// <remarks/>
//    public InvoiceLineTaxTotal TaxTotal
//    {
//        get
//        {
//            return this.taxTotalField;
//        }
//        set
//        {
//            this.taxTotalField = value;
//        }
//    }

//    /// <remarks/>
//    public InvoiceLineItem Item
//    {
//        get
//        {
//            return this.itemField;
//        }
//        set
//        {
//            this.itemField = value;
//        }
//    }

//    /// <remarks/>
//    public InvoiceLinePrice Price
//    {
//        get
//        {
//            return this.priceField;
//        }
//        set
//        {
//            this.priceField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2", IsNullable = false)]
//public partial class InvoicedQuantity
//{

//    private string unitCodeField;

//    private decimal valueField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlAttributeAttribute()]
//    public string unitCode
//    {
//        get
//        {
//            return this.unitCodeField;
//        }
//        set
//        {
//            this.unitCodeField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlTextAttribute()]
//    public decimal Value
//    {
//        get
//        {
//            return this.valueField;
//        }
//        set
//        {
//            this.valueField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2", IsNullable = false)]
//public partial class LineExtensionAmount
//{

//    private string currencyIDField;

//    private decimal valueField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlAttributeAttribute()]
//    public string currencyID
//    {
//        get
//        {
//            return this.currencyIDField;
//        }
//        set
//        {
//            this.currencyIDField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlTextAttribute()]
//    public decimal Value
//    {
//        get
//        {
//            return this.valueField;
//        }
//        set
//        {
//            this.valueField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//public partial class InvoiceLinePricingReference
//{

//    private InvoiceLinePricingReferenceAlternativeConditionPrice alternativeConditionPriceField;

//    /// <remarks/>
//    public InvoiceLinePricingReferenceAlternativeConditionPrice AlternativeConditionPrice
//    {
//        get
//        {
//            return this.alternativeConditionPriceField;
//        }
//        set
//        {
//            this.alternativeConditionPriceField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//public partial class InvoiceLinePricingReferenceAlternativeConditionPrice
//{

//    private PriceAmount priceAmountField;

//    private byte priceTypeCodeField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public PriceAmount PriceAmount
//    {
//        get
//        {
//            return this.priceAmountField;
//        }
//        set
//        {
//            this.priceAmountField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public byte PriceTypeCode
//    {
//        get
//        {
//            return this.priceTypeCodeField;
//        }
//        set
//        {
//            this.priceTypeCodeField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2", IsNullable = false)]
//public partial class PriceAmount
//{

//    private string currencyIDField;

//    private decimal valueField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlAttributeAttribute()]
//    public string currencyID
//    {
//        get
//        {
//            return this.currencyIDField;
//        }
//        set
//        {
//            this.currencyIDField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlTextAttribute()]
//    public decimal Value
//    {
//        get
//        {
//            return this.valueField;
//        }
//        set
//        {
//            this.valueField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//public partial class InvoiceLineTaxTotal
//{

//    private TaxAmount taxAmountField;

//    private InvoiceLineTaxTotalTaxSubtotal taxSubtotalField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public TaxAmount TaxAmount
//    {
//        get
//        {
//            return this.taxAmountField;
//        }
//        set
//        {
//            this.taxAmountField = value;
//        }
//    }

//    /// <remarks/>
//    public InvoiceLineTaxTotalTaxSubtotal TaxSubtotal
//    {
//        get
//        {
//            return this.taxSubtotalField;
//        }
//        set
//        {
//            this.taxSubtotalField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//public partial class InvoiceLineTaxTotalTaxSubtotal
//{

//    private TaxAmount taxAmountField;

//    private InvoiceLineTaxTotalTaxSubtotalTaxCategory taxCategoryField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public TaxAmount TaxAmount
//    {
//        get
//        {
//            return this.taxAmountField;
//        }
//        set
//        {
//            this.taxAmountField = value;
//        }
//    }

//    /// <remarks/>
//    public InvoiceLineTaxTotalTaxSubtotalTaxCategory TaxCategory
//    {
//        get
//        {
//            return this.taxCategoryField;
//        }
//        set
//        {
//            this.taxCategoryField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//public partial class InvoiceLineTaxTotalTaxSubtotalTaxCategory
//{

//    private byte taxExemptionReasonCodeField;

//    private InvoiceLineTaxTotalTaxSubtotalTaxCategoryTaxScheme taxSchemeField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public byte TaxExemptionReasonCode
//    {
//        get
//        {
//            return this.taxExemptionReasonCodeField;
//        }
//        set
//        {
//            this.taxExemptionReasonCodeField = value;
//        }
//    }

//    /// <remarks/>
//    public InvoiceLineTaxTotalTaxSubtotalTaxCategoryTaxScheme TaxScheme
//    {
//        get
//        {
//            return this.taxSchemeField;
//        }
//        set
//        {
//            this.taxSchemeField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//public partial class InvoiceLineTaxTotalTaxSubtotalTaxCategoryTaxScheme
//{

//    private string idField;

//    private string nameField;

//    private string taxTypeCodeField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string ID
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
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string Name
//    {
//        get
//        {
//            return this.nameField;
//        }
//        set
//        {
//            this.nameField = value;
//        }
//    }

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string TaxTypeCode
//    {
//        get
//        {
//            return this.taxTypeCodeField;
//        }
//        set
//        {
//            this.taxTypeCodeField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//public partial class InvoiceLineItem
//{

//    private string descriptionField;

//    private InvoiceLineItemSellersItemIdentification sellersItemIdentificationField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string Description
//    {
//        get
//        {
//            return this.descriptionField;
//        }
//        set
//        {
//            this.descriptionField = value;
//        }
//    }

//    /// <remarks/>
//    public InvoiceLineItemSellersItemIdentification SellersItemIdentification
//    {
//        get
//        {
//            return this.sellersItemIdentificationField;
//        }
//        set
//        {
//            this.sellersItemIdentificationField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//public partial class InvoiceLineItemSellersItemIdentification
//{

//    private string idField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public string ID
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
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
//public partial class InvoiceLinePrice
//{

//    private PriceAmount priceAmountField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
//    public PriceAmount PriceAmount
//    {
//        get
//        {
//            return this.priceAmountField;
//        }
//        set
//        {
//            this.priceAmountField = value;
//        }
//    }
//}

///// <remarks/>
//[System.SerializableAttribute()]
//[System.ComponentModel.DesignerCategoryAttribute("code")]
//[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")]
//[System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2", IsNullable = false)]
//public partial class UBLExtensions
//{

//    private UBLExtensionsUBLExtension[] uBLExtensionField;

//    /// <remarks/>
//    [System.Xml.Serialization.XmlElementAttribute("UBLExtension")]
//    public UBLExtensionsUBLExtension[] UBLExtension
//    {
//        get
//        {
//            return this.uBLExtensionField;
//        }
//        set
//        {
//            this.uBLExtensionField = value;
//        }
//    }
//}