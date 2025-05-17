namespace NamiCustomers.Infrastucture.ExternalServices.Email.Dtos
{
    public class RegisterTask
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
    }

    public class EmailTask
    {
        public string? ToEmail { get; set; }
        public string? FullName { get; set; }
    }
}
