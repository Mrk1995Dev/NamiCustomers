namespace NamiCustomers.Infrastucture.ExternalServices.IranFava.Dtos
{
    public class CustomersAndCarsResonse
    {
        public bool success { get; set; }
        public string error { get; set; }
        public List<CustomersAndCarsItem> result { get; set; }
    }
    public class CustomersAndCarsItem
    {
        public int id { get; set; }
        public int orderid { get; set; }
        public int cartipid { get; set; }
        public string title { get; set; }
        public string nationalCode { get; set; }
        public string mobile { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string fatherName { get; set; }
        public string birthCertId { get; set; }
        public DateTime birthDate { get; set; }
        public int gender { get; set; }
        public int radif { get; set; }
        public int cityID { get; set; }
        public string city { get; set; }
        public int provinceId { get; set; }
        public string province { get; set; }
        public object tel { get; set; }
        public string postalCode { get; set; }
        public string address { get; set; }
        public DateTime issuingDate { get; set; }
        public string shaba { get; set; }
        public string deliveryDateDescription { get; set; }
        public int orderRejectionStatus { get; set; }
        public string sherkat { get; set; }
        public int eSaleTypeId { get; set; }
        public bool shahkarStatus { get; set; }
        public bool certificateStatus { get; set; }
        public bool plaqueStatus { get; set; }
        public string blackList { get; set; }
        public int saleId { get; set; }
        public string trackingCode { get; set; }
        public string vin { get; set; }
        public string engineNo { get; set; }
        public string chassiNo { get; set; }
        public string vehicle { get; set; }
        public string statusTitle { get; set; }
    }






}
