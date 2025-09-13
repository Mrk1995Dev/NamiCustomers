namespace NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;


public class SevenSubscriberResponse
{
    public int Code { get; set; }
    public string UniqueId { get; set; }
    public string Name { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string FatherName { get; set; }
    public bool IsForeignNational { get; set; }
    public string NationalCode { get; set; }
    public string NationalId { get; set; }
    public object EconomicCode { get; set; }
    public string NationalCodeOrId { get; set; }
    public string IdNumber { get; set; }
    public int Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public string Mobile { get; set; }
    public string Address { get; set; }
    public string StrBirthDate { get; set; }
    public object Description { get; set; }
    public object JobTitle { get; set; }
    public bool IsLegalSubscriber { get; set; }
    public object SubscriberTypeName { get; set; }
    public int SubscriberTypeId { get; set; }
    public string SubCountryId { get; set; }
    public string SubCountry { get; set; }
    public string CityId { get; set; }
    public string CityName { get; set; }
    public string Tel { get; set; }
    public string DealerId { get; set; }
    public string DealerName { get; set; }
    public string DealerNo { get; set; }
    public string BranchId { get; set; }
    public string BranchName { get; set; }
    public string BranchNo { get; set; }
    public string? VinNumber { get; set; }
    public object MotorNumber { get; set; }
    public object BodyColor { get; set; }
    public object SubscriberMorality { get; set; }
    public object RegisterDateTime { get; set; }
    public object StrRegisterDateTime { get; set; }
    public object ExpireDateTime { get; set; }
    public object StrExpireDateTime { get; set; }
    public object Active { get; set; }
    public object Mileage { get; set; }
    public object Duration { get; set; }
    public object Clock { get; set; }
    public bool InActive { get; set; }
    public string BirthCityId { get; set; }
    public string BirthCityName { get; set; }
    public string IssueCityId { get; set; }
    public string IssueCityName { get; set; }
    public DateTime? IssueDate { get; set; }
    public string StrIssueDate { get; set; }
    public object RegisterCityId { get; set; }
    public string RegisterCityName { get; set; }
    public bool IsOwnCompany { get; set; }
    public Officeaddress OfficeAddress { get; set; }
    public Homeaddress HomeAddress { get; set; }
    public Homecontactinfo HomeContactInfo { get; set; }
    public Officecontactinfo OfficeContactInfo { get; set; }
    public object DetailedCode { get; set; }
    public object ProductYear { get; set; }
    public object ChassisPlate { get; set; }
    public object ChassisVehicleModelName { get; set; }
    public string SubCountryName { get; set; }
    public object CompanyName { get; set; }
    public object ManagerName { get; set; }
    public object Attorney { get; set; }
    public object AttorneyDate { get; set; }
    public object StrAttorneyDate { get; set; }
    public object NotaryCode { get; set; }
    public object DecodePassword { get; set; }
    public object SubscriberTypes { get; set; }
    public object RealEconomicCode { get; set; }
    public string AccountNumber { get; set; }
    public string ShabaNumber { get; set; }
    public string IssueDateSubCountryId { get; set; }
    public string BirthDateSubCountryId { get; set; }
    public object ProductionDate { get; set; }
    public object StrProductionDate { get; set; }
    public int TotalCount { get; set; }
    public string ClientId { get; set; }
    public bool IsDirty { get; set; }
}

public class Officeaddress
{
    public object MainStreet { get; set; }
    public object Street { get; set; }
    public object Alley { get; set; }
    public object Number { get; set; }
    public object Floor { get; set; }
    public object Unit { get; set; }
    public object PostalCode { get; set; }
    public string ClientId { get; set; }
    public bool IsDirty { get; set; }
}

public class Homeaddress
{
    public string MainStreet { get; set; }
    public string Street { get; set; }
    public string Alley { get; set; }
    public string Number { get; set; }
    public string Floor { get; set; }
    public string Unit { get; set; }
    public string PostalCode { get; set; }
    public string ClientId { get; set; }
    public bool IsDirty { get; set; }
}

public class Homecontactinfo
{
    public string Tel { get; set; }
    public string Mobile { get; set; }
    public object Fax { get; set; }
    public string Email { get; set; }
    public object Website { get; set; }
}

public class Officecontactinfo
{
    public object Tel { get; set; }
    public object Mobile { get; set; }
    public object Fax { get; set; }
    public object Email { get; set; }
    public object Website { get; set; }
}