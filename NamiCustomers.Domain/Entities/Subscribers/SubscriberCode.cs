using System.ComponentModel.DataAnnotations;

namespace NamiCustomers.Domain.Entities.Subscribers;

public class SubscriberCode : IBaseEntity<int>
{
    public int Id { get; set; }
    [Display(Name = "تلفن همراه")]
    [StringLength(maximumLength: 11, ErrorMessage = "طول {0} میبایست  {1} کاراکتر باشد", MinimumLength = 12)]
    public string Mobile { get; set; }
    public string AuthCode { get; set; }
    public bool Used { get; set; }

}
