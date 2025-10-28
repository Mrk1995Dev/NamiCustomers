using NamiCustomers.Abstractions.Dtos.Vehicles;
using System.ComponentModel.DataAnnotations;

namespace NamiCustomers.Abstractions.Dtos.Subscribers
{
    public class SubscriberDto
    {
        public int Id { get; init; }
        [Display(Name = "نام")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "نام خانوادگی")]
        [Required]
        public string Family { get; set; }
        [Display(Name = "آدرس")]
        public string? Address { get; set; }
        public int? CityId { get; set; }
        public string? CityName { get; set; }
        [Display(Name = "تلفن")]
        public string? Phone { get; set; }
        [Display(Name = "کد ملی")]
        [Required]
        public string NationalCode { get; set; }
        [Required]
        [Display(Name = "موبایل")]
        public string Mobile { get; set; }
        [Display(Name = "جنسیت")]
        public string? Sex { get; set; }
        public ICollection<VehicleModelDto>? VehicleModels { get; set; }
        [Display(Name = "نوع مشتری")]
        [Required(ErrorMessage = "لطفاً مقدار {0} را وارد نمایید.")]
        public int SubscriberType { get; set; }

        [Display(Name = "تاریخ تولد میلادی")]
        public DateTime? BrithDate { get; set; }

        [StringLength(maximumLength: 10, ErrorMessage = "طول {0} میبایست  {1} کاراکتر باشد", MinimumLength = 10)]
        public string? BrithDatePersian { get; set; }

    }
}
