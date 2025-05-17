namespace NamiCustomers.Infrastucture.ExternalServices.Email.Dtos
{
    public class RegisterColor
    {
        public List<EmailTask>? EmailTasks { get; set; }

        public string? Subject { get; set; }

        public string? Dealername { get; set; }
        public string? Dealertyp { get; set; }
        public string? SubsName { get; set; }
        public string? subfamily { get; set; }
        public string? subnatinal { get; set; }
        public string? submobile { get; set; }
        public string? tomailDealer { get; set; }
        public string? firstcolor { get; set; }
        public string? thesecondcolor { get; set; }
        public string? thethirdcolor { get; set; }
    }
    public class EmailTaskColor
    {
        public string? ToEmail { get; set; }
        public string? FullName { get; set; }
    }
}
