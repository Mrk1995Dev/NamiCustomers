using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;
using NamiCustomers.Infrastucture.Utilities;

namespace NamiCustomers.MVC.Services;


public interface IBookingService
{


    /// <summary>
    /// دریافت نوبت
    /// </summary>
    /// <returns></returns>
    Task<ResultDto<BookingTurnResponse>> GetBookingTurnAsync(BookingTurnRequest filter);



}
public class BookingService(HttpClient httpClient, ISevenSoftService sevenSoftService) : IBookingService
{
    public async Task<ResultDto<BookingTurnResponse>> GetBookingTurnAsync(BookingTurnRequest filter)
    {




        BookingTurnResponse response = new();
        filter.CountryId = new Guid("de5b7996-131d-44c5-88b9-fc3a511506a0");//todo
        if (filter.CountryId != null)
        {
            var result = await sevenSoftService.GetAllSubCountries(filter.CountryId);
            if (result.Data == null)
                return new ResultDto<BookingTurnResponse>(Infrastucture.Properties.Resources.msgFound
               , true, response);
            var data =result.Data.Select(c => new BookingTurnOptionDto {Selected=(c.UniqueId==filter.SubCountryId.ToString()), Value = c.UniqueId, Text = c.SubCountryLocalizedName }).ToList();
            response.SubCountries.AddRange(data);
            
        }
        if (filter.SubCountryId != null && response.SubCountries.Any(c => c.Selected))
        {
            var result = await sevenSoftService.GetAllCity(filter.SubCountryId.Value);
            if (result.Data == null)
                return new ResultDto<BookingTurnResponse>(Infrastucture.Properties.Resources.msgFound
               , true, response);
            var data=result.Data.Select(c => new BookingTurnOptionDto { Selected = (c.UniqueId == filter.CityId.ToString()), Value = c.UniqueId, Text = c.CityLocalizedName }).ToList();
            response.Cities.AddRange(data);
        }
        if (filter.CityId != null && response.Cities.Any(c => c.Selected))
        {
            var result = await sevenSoftService.GetAllDealer(filter.CityId.Value);
            if (result.Data == null)
                return new ResultDto<BookingTurnResponse>(Infrastucture.Properties.Resources.msgFound
               , true, response);
            var data = result.Data.Select(c => new BookingTurnOptionDto { Selected = (c.UniqueId == filter.DealerId.ToString()), Value = c.UniqueId, Text = c.DealerName }).ToList();
            response.Dealers.AddRange(data);
        }
        if (filter.DealerId != null && response.Dealers.Any(c => c.Selected))
        {
            var result = await sevenSoftService.GetAllBranch(filter.DealerId.Value);
            if (result.Data == null)
                return new ResultDto<BookingTurnResponse>(Infrastucture.Properties.Resources.msgFound
               , true, response);
            var data = result.Data.Select(c => new BookingTurnOptionDto { Selected = (c.UniqueId == filter.BranchId.ToString()), Value = c.UniqueId, Text = c.BranchName }).ToList();
            response.Branches.AddRange(data);
        }

        if (filter.BranchId != null && response.Branches.Any(c => c.Selected))
        {
            var result = await sevenSoftService.GetAllServerGroup(filter.BranchId.Value);
            if (result.Data == null)
                return new ResultDto<BookingTurnResponse>(Infrastucture.Properties.Resources.msgFound
               , true, response);
            var data = result.Data.Select(c => new BookingTurnOptionDto { Selected = (c.UniqueId == filter.ServerGroupId.ToString()), Value = c.UniqueId, Text = c.ServerGroupLocalizedName }).ToList();
            response.ServerGroups.AddRange(data);
        }
        if (filter.ServerGroupId != null && response.ServerGroups.Any(c => c.Selected))
        {
            var result = await sevenSoftService.GetAllServerGroupDate(filter.ServerGroupId.Value,filter.BranchId.Value);
            if (result.Data == null)
                return new ResultDto<BookingTurnResponse>(Infrastucture.Properties.Resources.msgFound
               , true, response);
            var data = result.Data.Select(c => new BookingTurnOptionDto { Selected = (c.Value == filter.ServerGroupDateId.ToString()), Value = c.Value, Text = c.Text }).ToList();
            response.ServerGroupDates.AddRange(data);
        }
        if (filter.ServerGroupDateId != null && response.ServerGroupDates.Any(c => c.Selected))
        {
            var result = await sevenSoftService.GetAllServerGroupTime(filter.ServerGroupDateId.Value,filter.ServerGroupId.Value);
            if (result.Data == null)
                return new ResultDto<BookingTurnResponse>(Infrastucture.Properties.Resources.msgFound
               , true, response);
            var data = result.Data.Select(c => new BookingTurnOptionDto { Selected = (c.Value == filter.ServerGroupTimeId.ToString()), Value = c.Value, Text = c.Text }).ToList();
            response.ServerGroupTimes.AddRange(data);
        }


        return new ResultDto<BookingTurnResponse>(Infrastucture.Properties.Resources.msgReturnDataSuccess, true, response);
    }
}


public class BookingTurnRequest
{
    public Guid CountryId { get; set; }
    public Guid? SubCountryId { get; set; }
    public Guid? CityId { get; set; }
    public Guid? DealerId { get; set; }
    public Guid? BranchId { get; set; }

    public Guid? ServerGroupId { get; set; }
    public Guid? ServerGroupDateId { get; set; }
    public string? ServerGroupTimeId { get; set; }
    public int Kilometer { get; set; }
    public string? Description { get; set; }
}
public class BookingTurnResponse
{
    public BookingTurnResponse()
    {
        this.SubCountries.Add(new BookingTurnOptionDto { Selected = false, Text = "لطفا  استان را انتخاب نمایید", Value = Guid.Empty.ToString() });
        this.Cities.Add(new BookingTurnOptionDto { Selected = false, Text = "لطفا شهر را انتخاب نمایید", Value = Guid.Empty.ToString() });
        this.Dealers.Add(new BookingTurnOptionDto { Selected = false, Text = "لطفا عاملیت را انتخاب نمایید", Value = Guid.Empty.ToString() });
        this.Branches.Add(new BookingTurnOptionDto { Selected = false, Text = "لطفا شعبه را انتخاب نمایید", Value = Guid.Empty.ToString() });
        this.ServerGroups.Add(new BookingTurnOptionDto { Selected = false, Text = "لطفا نوع خدمت را انتخاب نمایید", Value = Guid.Empty.ToString() });
        this.ServerGroupDates.Add(new BookingTurnOptionDto { Selected = false, Text = "لطفا  تاریخ را انتخاب نمایید", Value = Guid.Empty.ToString() });
        this.ServerGroupTimes.Add(new BookingTurnOptionDto { Selected = false, Text = "لطفا  زمان را انتخاب نمایید", Value = Guid.Empty.ToString() });

    }
    /// <summary>
    /// استان ها
    /// </summary>
    public List<BookingTurnOptionDto> SubCountries { get; set; } = new();
    public List<BookingTurnOptionDto> Cities { get; set; } = new();
    public List<BookingTurnOptionDto> Dealers { get; set; } = new();
    public List<BookingTurnOptionDto> Branches { get; set; } = new();

    public List<BookingTurnOptionDto> ServerGroups { get; set; } = new();
    public List<BookingTurnOptionDto> ServerGroupDates { get; set; } = new();
    public List<BookingTurnOptionDto> ServerGroupTimes { get; set; } = new();


    public Guid? CountryId { get; set; }
    public Guid? SubCountryId { get; set; }
    public Guid? CityId { get; set; }
    public Guid? DealerId { get; set; }
    public Guid? BranchId { get; set; }
    public Guid? ServerGroupId { get; set; }
    public Guid? ServerGroupDateId { get; set; }
    public Guid? ServerGroupTimeId { get; set; }
    public int Kilometer { get; set; }


    public string? Description { get; set; }

}
public class BookingTurnOptionDto : SelectListItem
{
     
}
