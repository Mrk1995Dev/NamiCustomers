using NamiCustomers.Abstractions.Dtos.Vehicles;
using System.ComponentModel.DataAnnotations;

namespace NamiCustomers.Abstractions.Dtos.Subscribers
{
    public class SubscriberDto
    {
        public int Id { get; init; }
        [Display(Name = "نام")]
        public string Name { get; set; }
        [Display(Name = "نام خانوادگی")]
        public string Family { get; set; }
        [Display(Name = "آدرس")]
        public string? Address { get; set; }
        public int? CityId { get; set; }
        public string? CityName { get; set; }
        [Display(Name = "تلفن")]
        public string? Phone { get; set; }
        [Display(Name = "کد ملی")]
        public string? NationalCode { get; set; }
        [Display(Name = "موبایل")]
        public string? Mobile { get; set; }
        [Display(Name = "جنسیت")]
        public string? Sex { get; set; }
        public ICollection<VehicleModelDto>? VehicleModels { get; set; }
    }
}
