namespace NamiCustomers.Application.Services.SevenSoftServices.Dtos;
 
    public class SevenSoftCompanyInfoResponse
    {
        public int Code { get; set; }
        public string UniqueId { get; set; }
        /// <summary>
        /// نام کامل شرکت
        /// </summary>
        public string CompanyInfoListLocalizedName { get; set; }
        public object CompanyInfoListName { get; set; }
        /// <summary>
        /// اقامتگاه قانونی
        /// 
        /// </summary>
        public string CompanyInfoListAddress { get; set; }
        /// <summary>
        /// تلفن
        /// </summary>
        public string CompanyInfoListTel { get; set; }
        /// <summary>
        /// فکس
        /// </summary>
        public object CompanyInfoListFax { get; set; }
        public string CompanyInfoListNationCode { get; set; }
        /// <summary>
        /// شماره ثبت
        /// </summary>
        public string CompanyInfoListRegisterCode { get; set; }
        /// <summary>
        /// رییس هیات مدیره
        /// </summary>
        public string CompanyInfoListManagerName { get; set; }
        public object CompanyInfoListEmail { get; set; }
        public object CompanyInfoListWebsite { get; set; }
        public string CompanyInfoListHoldingCompanyId { get; set; }
        public string CompanyInfoListHoldingCompany { get; set; }
        public int CompanyInfoListAreaCategoryId { get; set; }
        public string CompanyInfoListAreaCategory { get; set; }
        public string CompanyInfPostalCOde { get; set; }
    }

