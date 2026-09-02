using NamiCustomers.Domain.Entities.Dealers;

namespace NamiCustomers.Application.Seeds;

/// <summary>
/// عاملیت‌های نامی خودرو بر اساس صفحه
/// https://namikhodro.com/عاملیت-ها/
/// </summary>
public static class IranDealerSeed
{
    public static object[] Dealers { get; } =
    [
        Dealer(1, null, "شعبه مرکزی", "تهران، خیابان شهید مطهری، خیابان فجر، ساختمان نامی", "021", "41421", 8, DealerType.Sales, 1, true, "info@namikhodro.com"),
        Dealer(2, "701", "ظفرقندی (عاملیت مرکزی)", "تهران، ۴۵ متری رسالت، بعد از ۱۶ متری دوم مجیدیه، نرسیده به خیابان کرمان، پلاک ۹۳۰", "021", "22300973-5", 8, DealerType.SalesAndService, 2),
        Dealer(3, "729", "بخشی", "تهران، خیابان شریعتی، بالاتر از پل سیدخندان، خیابان خواجه عبدالله انصاری", "021", "22841616", 8, DealerType.SalesAndService, 3),
        Dealer(4, "702", "نیکوان", "فروش: شیراز، بلوار امیرکبیر، نبش والفجر. خدمات پس از فروش: شیراز، بلوار سلمان فارسی، جنب پمپ بنزین، کوچه ۱", "071", "90000745، 38333331، 38333332", 17, DealerType.SalesAndService, 4),
        Dealer(5, "703", "جمالی", "بابل، کیلومتر ۳ امیرکلا به بابلسر", "011", "44413201-3", 253, DealerType.SalesAndService, 5),
        Dealer(6, "704", "بازرگانی خودرو ماندگار گلستان", "گرگان، کیلومتر ۱ جاده گنبد", "017", "32179000", 24, DealerType.SalesAndService, 6),
        Dealer(7, "705", "کلهر", "اهواز، ابتدای اتوبان آیت‌الله بهبهانی، ۲۰۰ متر بعد از میدان جمهوری، پلاک ۹۵۷", "061", "35545817", 13, DealerType.SalesAndService, 7),
        Dealer(8, "706", "گیلانی", "آمل، میدان هزار سنگر، کیلومتر ۶ جاده جدید بابل-دابودشت", "011", "4124", 254, DealerType.SalesAndService, 8),
        Dealer(9, "707", "قنبرپور", "بوشهر، بلوار شهید قرنی، قبل از میدان امام علی، مجموعه قنبرپور", "077", "33451163-33451422", 7, DealerType.SalesAndService, 9),
        Dealer(10, "708", "همتی", "قزوین، بلوار شهید بهشتی، بعد از بیمارستان قدس، جنب هلال احمر", "028", "فروش: 33344881 | خدمات: 33347040", 18, DealerType.SalesAndService, 10),
        Dealer(11, "709", "گسترش ایده‌های تجاری گات", "کرمان، بلوار شهید صدوقی، بین بلوار هزار و یک شب جنوبی و بلوار فارابی", "034", "فروش: 32466617 | خدمات: 62466617", 21, DealerType.SalesAndService, 11),
        Dealer(12, "711", "پورات", "قم، خیابان امام خمینی، پلاک ۲۷۵", "025", "فروش: 36622247 | خدمات: 36603900 / 36604238", 19, DealerType.SalesAndService, 12),
        Dealer(13, "712", "اسفندیارپور", "فروش: سیرجان، خیابان مقداد، نبش خیابان رجائی. خدمات پس از فروش: سیرجان، کیلومتر ۳ جاده تهران، روبروی منطقه ویژه اقتصادی", "034", "42261197", 205, DealerType.SalesAndService, 13),
        Dealer(14, "713", "چهره", "کرج، پل فردیس، ابتدای جاده ملارد، بعد از پل سرحدآباد", "026", "36615027", 5, DealerType.SalesAndService, 14),
        Dealer(15, "714", "ثابت قدم", "زنجان، خیابان خیام غربی، روبروی میراث فرهنگی", "024", "33331116-33366000", 14, DealerType.SalesAndService, 15),
        Dealer(16, "715", "تلاش خودرو ایرانیان", "فروش: تبریز، ولیعصر، نرسیده به فلکه معلم، روبروی ناحیه پستی، پلاک ۲۰. خدمات پس از فروش: تبریز، بالاتر از میدان بسیج، جنب کارخانه آناتا", "041", "82868686", 1, DealerType.SalesAndService, 16),
        Dealer(17, "716", "شیری-حنیفی", "همدان، میدان هگمتانه، بلوار بم", "081", "34243470", 30, DealerType.SalesAndService, 17),
        Dealer(18, "717", "نعمتی", "فروش: کرمانشاه، گلریزان، پایین‌تر از چهارراه خرم، پلاک ۸۱۲. خدمات پس از فروش: کرمانشاه، اربابی، خیابان حکیم نظامی", "083", "فروش: 38438346 | خدمات: 38249218", 22, DealerType.SalesAndService, 18),
        Dealer(19, "718", "رضائی", "شاهرود، میدان هفت تیر، جاده کارخانه قند، ابتدای جاده مغان، مجتمع خودرویی رضائی", "023", "31020", 164, DealerType.SalesAndService, 19),
        Dealer(20, "719", "فن‌آوران صنعت خودرو", "رشت، کیلومتر ۳ جاده رشت به فومن، آتشگاه", "013", "33594501-4", 25, DealerType.SalesAndService, 20),
        Dealer(21, "720", "توانگر", "یزد، بلوار مدرس، میدان نماز، ابتدای خیابان ولیعصر، خیابان سعادت", "035", "36241300 / 36241400 / 36241500", 31, DealerType.SalesAndService, 21),
        Dealer(22, "721", "زارعی", "میناب، بلوار سردار سلیمانی، بعد از پمپ بنزین", "076", "42281400-2", 272, DealerType.SalesAndService, 22),
        Dealer(23, "725", "صالحی", "فروش: مشهد، خیابان ملک‌الشعرا بهار، بین ملک‌الشعرا بهار ۴۶ و ۴۸، پلاک ۱۵۱. خدمات پس از فروش: مشهد، خیابان ملک‌الشعرا بهار، خیابان ملک‌الشعرا بهار ۴۸ (سپه ۲)، پلاک ۳", "051", "38553353", 11, DealerType.SalesAndService, 23),
        Dealer(24, "726", "غازی‌زاده", "زاهدان، میدان پانزده خرداد", "054", "33230415", 16, DealerType.SalesAndService, 24),
        Dealer(25, "727", "افشاری‌کیا", "سبزوار، حد فاصل چهارراه کوشک و میدان مادر", "051", "فروش: 44248080 | خدمات: 44248181", 128, DealerType.SalesAndService, 25),
        Dealer(26, "728", "بازرگانی قناد محور موتور", "اصفهان، خیابان امام خمینی، نبش کوچه مینو", "031", "37111", 4, DealerType.SalesAndService, 26),
        Dealer(27, "710", "حموله", "اصفهان، خیابان امام خمینی، خیابان مشیرالدوله شرقی، خیابان مهارت", "031", "33853035", 4, DealerType.SalesAndService, 27, false)
    ];

    private static object Dealer(
        int id,
        string? dealerNo,
        string name,
        string address,
        string prePhone,
        string phone,
        int cityId,
        DealerType dealerType,
        int sort,
        bool isActive = true,
        string? email = null) => new
    {
        Id = id,
        DealerNo = dealerNo,
        DealerName = name,
        DealerAddress = address,
        DealerPrePhone = prePhone,
        DealerPhone = phone,
        DealerMobile = (string?)null,
        Email = email,
        DealerType = dealerType,
        CityId = cityId,
        IsActive = isActive,
        Sort = sort,
        CreateAt = IranLocationSeed.SeedDate,
        IsRemoved = false
    };
}
