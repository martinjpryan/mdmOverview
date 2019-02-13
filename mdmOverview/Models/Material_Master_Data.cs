using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

[NotMapped]

public partial class Material_Master_Data
{
    public string SAPIDOCNumber{ get; set; }
    public Material_Master_DataMaterialMasterGeneralData MaterialMasterGeneralData { get; set; }

    [System.Xml.Serialization.XmlArrayItemAttribute("MaterialMasterAlternateUoM", IsNullable = false)]
    public List<Material_Master_DataMaterialMasterAlternateUoM> MaterialMasterAlternateUoMDetails { get; set; }
    public Material_Master_DataMaterialMasterShortTextsDetails MaterialMasterShortTextsDetails
    { get; set; }
    [XmlArrayItemAttribute("MaterialMasterPlantData", IsNullable = false)]
    public List<Material_Master_DataMaterialMasterPlantData> MaterialMasterPlantDataDetails { get; set; }
    public List<Material_Master_DataMaterialMasterStorageData> MaterialMasterStorageDataDetails { get; set; }
    public Material_Master_DataSAPstringClassificationDetails SAPstringClassificationDetails{ get; set; }
    public Material_Master_DataSAPstringCharacteristicsDetails SAPstringCharacteristicsDetails { get; set; }
    public Material_Master_Data()
    {
        SAPstringCharacteristicsDetails = new Material_Master_DataSAPstringCharacteristicsDetails();
        SAPstringClassificationDetails = new Material_Master_DataSAPstringClassificationDetails();
        MaterialMasterShortTextsDetails = new Material_Master_DataMaterialMasterShortTextsDetails();
        MaterialMasterGeneralData = new Material_Master_DataMaterialMasterGeneralData();
        MaterialMasterAlternateUoMDetails = new List<Material_Master_DataMaterialMasterAlternateUoM>();
        MaterialMasterPlantDataDetails = new List<Material_Master_DataMaterialMasterPlantData>();
        MaterialMasterStorageDataDetails = new List<Material_Master_DataMaterialMasterStorageData>();
    }

}
public partial class Material_Master_DataMaterialMasterGeneralData
{
    public string MaterialNumber { get; set; }
    public string MaterialStatus { get; set; }
    public string CaseWeightUoM { get; set; }
    public string BaseUoM { get; set; }
    public string MaterialDeleted { get; set; }
    public string LegacyCode { get; set; }
}
public partial class Material_Master_DataMaterialMasterAlternateUoM
{
    public string MaterialNumber{ get; set; }
    public string AlternateUoM { get; set; }
    public string ConversionNumeratorToBaseUoM { get; set; }
    public string ConversionDenominatorToBaseUoM { get; set; }
    public string Length { get; set; }
    public string Width { get; set; }
    public string Height { get; set; }
    public string UnitDimension { get; set; }
    public string Volume { get; set; }
    public string VolumeUnit { get; set; }
}

/// <remarks/>
public partial class Material_Master_DataMaterialMasterShortTextsDetails
{
    public Material_Master_DataMaterialMasterShortTextsDetailsMaterialMasterShortTexts MaterialMasterShortTexts { get; set; }
    public Material_Master_DataMaterialMasterShortTextsDetails()
    {
        MaterialMasterShortTexts = new Material_Master_DataMaterialMasterShortTextsDetailsMaterialMasterShortTexts();
    }

}

public partial class Material_Master_DataMaterialMasterShortTextsDetailsMaterialMasterShortTexts
{
    public string MaterialNumber { get; set; }
    public string Language { get; set; }
    public string MaterialDescription { get; set; }
}

public partial class Material_Master_DataMaterialMasterPlantData
{

    public string MaterialNumber { get; set; }
    public string MaterialStatus { get; set; }
    public string PlantCode { get; set; }
    public string MaterialGrouping { get; set; }
}

public partial class Material_Master_DataMaterialMasterStorageData
{
    public string MaterialNumber { get; set; }
    public string PlantCode { get; set; }
    public string StorageLocation { get; set; }
    public string MaterialStatus { get; set; }
}

public partial class Material_Master_DataSAPstringClassificationDetails
{
    public Material_Master_DataSAPstringClassificationDetailsSAPstringClassification SAPstringClassification { get; set; }
    public Material_Master_DataSAPstringClassificationDetails()
    {
        SAPstringClassification = new Material_Master_DataSAPstringClassificationDetailsSAPstringClassification();
    }

}
public partial class Material_Master_DataSAPstringClassificationDetailsSAPstringClassification
{
    public string stringTable { get; set; }
    public string stringNumber { get; set; }
    public string ClassificationType { get; set; }
    public string Indicator { get; set; }
}

public partial class Material_Master_DataSAPstringCharacteristicsDetails
{
    public Material_Master_DataSAPstringCharacteristicsDetailsSAPstringCharacteristics SAPstringCharacteristics { get; set; }
    public Material_Master_DataSAPstringCharacteristicsDetails()
    {
        SAPstringCharacteristics = new Material_Master_DataSAPstringCharacteristicsDetailsSAPstringCharacteristics();
    }

}

public partial class Material_Master_DataSAPstringCharacteristicsDetailsSAPstringCharacteristics
{
    public string stringTable{ get; set; }
    public string stringNumber { get; set; }
    public string ClassificationType { get; set; }
    public string CharacteristicName { get; set; }
}
