using System.ComponentModel.DataAnnotations;

namespace NamiCustomers.Domain.Entities.Subscribers
{
    [Auditable]
    public class Subscriber: IBaseEntity<int>
    {
        public int Id { get; set; }
        public int? CityId { get; set; }
        public City? City { get; set; }

        public string FullName => $"{Name} {Family}";

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفاً مقدار {0} را وارد نمایید.")]
        [StringLength(maximumLength: 50, ErrorMessage = "طول {0} میبایست بین {2} تا {1} کاراکتر باشد", MinimumLength = 2)]
        public string? Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفاً مقدار {0} را وارد نمایید.")]
        [StringLength(maximumLength: 50, ErrorMessage = "طول {0} میبایست بین {2} تا {1} کاراکتر باشد", MinimumLength = 2)]
        public string? Family { get; set; }

        [Display(Name = "کد ملی")]
        [StringLength(maximumLength: 10, ErrorMessage = "طول {0} میبایست  {1} کاراکتر باشد", MinimumLength = 2)]
        public string? NatinalCode { get; set; }

        [Display(Name = "شماره شناسنامه ")]
        [StringLength(maximumLength: 10, ErrorMessage = "طول {0} میبایست  {1} کاراکتر باشد", MinimumLength = 2)]
        public string? IdNumber { get; set; }


        [Display(Name = "نام پدر")]
        [StringLength(maximumLength: 50, ErrorMessage = "طول {0} میبایست  {1} کاراکتر باشد", MinimumLength = 2)]
        public string? FathersName { get; set; }

        [Display(Name = "تاریخ تولد")]
        [StringLength(maximumLength: 10, ErrorMessage = "طول {0} میبایست  {1} کاراکتر باشد", MinimumLength = 10)]
        public string? BrithDatePersian { get; set; }

        [Display(Name = "تاریخ تولد میلادی")]
        //[StringLength(maximumLength: 10, ErrorMessage = "طول {0} میبایست  {1} کاراکتر باشد", MinimumLength = 10)]
        public DateTime? BrithDate { get; set; }

        [Display(Name = "تلفن همراه")]
        [StringLength(maximumLength: 11, ErrorMessage = "طول {0} میبایست  {1} کاراکتر باشد", MinimumLength = 12)]
        public string? Mobile { get; set; }

        [Display(Name = "تلفن")]
        [StringLength(maximumLength: 15, ErrorMessage = "طول {0} میبایست  {1} کاراکتر باشد", MinimumLength = 2)]
        public string? Phone { get; set; }

        [Display(Name = "آدرس")]
        [StringLength(maximumLength: 250, ErrorMessage = "طول {0} میبایست  {1} کاراکتر باشد", MinimumLength = 2)]
        public string? Address { get; set; }

        [Display(Name = "کد پستی")]
        [StringLength(maximumLength: 17, ErrorMessage = "طول {0} میبایست  {1} کاراکتر باشد", MinimumLength = 2)]
        public string? PostalCode { get; set; }

        public string? NationalCode { get; set; }
        
        public string? Sex { get; set; }
        public ICollection<VehicleModel>? VehicleModels { get; set; }

        public ICollection<SubscriberCode>? SubscriberCodes { get; set; }
        
    }
}
