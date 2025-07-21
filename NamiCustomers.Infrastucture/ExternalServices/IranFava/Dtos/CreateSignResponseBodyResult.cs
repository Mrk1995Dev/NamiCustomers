namespace NamiCustomers.Infrastucture.ExternalServices.IranFava.Dtos
{
    public class CreateSignResponseBodyResult
    {
        public string workflowTicket { get; set; }
        public DateTime updateDate { get; set; }
        public DateTime createDate { get; set; }
        public string title { get; set; }
        public string workflowType { get; set; }
        public List<WorkflowRecipient> workflowRecipients { get; set; } = new List<WorkflowRecipient>();
    }
}
