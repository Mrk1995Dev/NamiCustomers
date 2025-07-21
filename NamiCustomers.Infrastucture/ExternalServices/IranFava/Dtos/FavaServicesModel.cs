using Newtonsoft.Json;

namespace NamiCustomers.Infrastucture.ExternalServices.IranFava.Dtos
{

    public class InquirySignResult
    {
        public string message { get; set; }
        public int resultCode { get; set; }
        public bool success { get; set; }
        public string state { get; set; }
        public string documentLink { get; set; }
        public string signedDocumentLink { get; set; }
    }


    public class CreateSignResult
    {
        public string message { get; set; }
        public int resultCode { get; set; }
        //public bool success { get; set; }
        public string responseBody { get; set; }
        public List<CreateSignResponseBodyResult> responseBodyList => JsonConvert.DeserializeObject<List<CreateSignResponseBodyResult>>(responseBody);
    }
    public class FavaRequest
    {
        public string title { get; set; }

        public string description { get; set; }

        public string documentName { get; set; }

        public string documentData { get; set; }

        public string recipientUsername { get; set; }

        public string documentParameter { get; set; }
    }


    public class DataField
    {
        public string dataFieldType { get; set; }
        public int pageNumber { get; set; }
        public string tag { get; set; }
        public double topRel { get; set; }
        public double leftRel { get; set; }
        public double heightRel { get; set; }
        public double widthRel { get; set; }
        public int productId { get; set; }
    }

    public class documentParameter
    {
        public List<DataField> dataFields { get; set; } = new List<DataField>();
        public SignatureImageTextParameter signatureImageTextParameter { get; set; } = new SignatureImageTextParameter();
    }

    public class SignatureImageTextParameter
    {
        public string customText { get; set; }
        public bool name { get; set; }
        public bool signDate { get; set; }
    }
}
