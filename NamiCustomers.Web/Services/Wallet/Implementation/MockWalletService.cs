using NamiCustomers.Abstractions.Dtos;
using NamiCustomers.Web.Services.Wallet.Contract;
using NamiCustomers.Web.Services.Wallet.Dto;

namespace NamiCustomers.Web.Services.Wallet.Implementation;

public class MockWalletService : IWalletService
{
    private readonly List<WalletAccountDto> accounts =
    [
        new()
        {
            Type = WalletType.Toman,
            Title = "کیف پول تومانی",
            Balance = 2_450_000,
            Description = "قابل استفاده برای خدمات، قطعات و نوبت تعمیرگاه",
            CanCharge = true
        },
        new()
        {
            Type = WalletType.Gift,
            Title = "کیف پول هدیه",
            Balance = 750_000,
            Description = "اعتبار هدیه باشگاه مشتریان؛ فقط برای خدمات و قطعات",
            ExpiresAtPersian = "۱۴۰۵/۱۲/۲۹",
            CanCharge = false
        }
    ];

    private readonly List<WalletTransactionDto> transactions =
    [
        new()
        {
            Id = "tx-7",
            WalletType = WalletType.Toman,
            Title = "پرداخت اجرت خدمات دوره‌ای",
            Description = "تعمیرگاه مرکزی تهران",
            Amount = -850_000,
            DatePersian = "۱۴۰۵/۰۶/۱۱",
            TimePersian = "۱۱:۲۰"
        },
        new()
        {
            Id = "tx-6",
            WalletType = WalletType.Gift,
            Title = "هدیه کمپین تابستانه",
            Description = "اعتبار باشگاه مشتریان نامی",
            Amount = 250_000,
            DatePersian = "۱۴۰۵/۰۶/۰۸",
            TimePersian = "۰۹:۱۵"
        },
        new()
        {
            Id = "tx-5",
            WalletType = WalletType.Toman,
            Title = "شارژ کیف پول",
            Description = "پرداخت اینترنتی",
            Amount = 1_000_000,
            DatePersian = "۱۴۰۵/۰۶/۰۵",
            TimePersian = "۱۸:۴۲"
        },
        new()
        {
            Id = "tx-4",
            WalletType = WalletType.Toman,
            Title = "خرید قطعه یدکی",
            Description = "فیلتر روغن و هوا",
            Amount = -320_000,
            DatePersian = "۱۴۰۵/۰۵/۲۸",
            TimePersian = "۱۶:۰۵"
        },
        new()
        {
            Id = "tx-3",
            WalletType = WalletType.Gift,
            Title = "هدیه باشگاه مشتریان",
            Description = "به مناسبت تمدید گارانتی",
            Amount = 500_000,
            DatePersian = "۱۴۰۵/۰۵/۲۰",
            TimePersian = "۱۰:۳۰"
        },
        new()
        {
            Id = "tx-2",
            WalletType = WalletType.Toman,
            Title = "بازگشت وجه گارانتی",
            Description = "برگشت هزینه قطعه تعویضی",
            Amount = 120_000,
            DatePersian = "۱۴۰۵/۰۵/۱۲",
            TimePersian = "۱۴:۱۸"
        },
        new()
        {
            Id = "tx-1",
            WalletType = WalletType.Toman,
            Title = "پرداخت نوبت تعمیرگاه",
            Description = "عاملیت سعدی",
            Amount = -180_000,
            DatePersian = "۱۴۰۵/۰۴/۲۶",
            TimePersian = "۰۸:۵۰"
        }
    ];

    public Task<ResultDto<WalletOverviewDto>> GetOverviewAsync()
    {
        return Task.FromResult(ResultDto.Success(CloneOverview()));
    }

    public async Task<ResultDto<WalletOverviewDto>> ChargeTomanAsync(long amount)
    {
        await Task.Delay(450);

        if (amount < 50_000)
        {
            return ResultDto.Failure<WalletOverviewDto>("حداقل مبلغ شارژ ۵۰٬۰۰۰ تومان است.");
        }

        var toman = accounts.First(account => account.Type == WalletType.Toman);
        toman.Balance += amount;

        transactions.Insert(0, new WalletTransactionDto
        {
            Id = $"tx-{Guid.NewGuid():N}"[..12],
            WalletType = WalletType.Toman,
            Title = "شارژ کیف پول",
            Description = "پرداخت آزمایشی (ماک)",
            Amount = amount,
            DatePersian = "۱۴۰۵/۰۶/۱۱",
            TimePersian = DateTime.Now.ToString("HH:mm")
        });

        return ResultDto.Success(CloneOverview(), "شارژ با موفقیت ثبت شد.");
    }

    private WalletOverviewDto CloneOverview()
    {
        return new WalletOverviewDto
        {
            Accounts = accounts.Select(account => new WalletAccountDto
            {
                Type = account.Type,
                Title = account.Title,
                Balance = account.Balance,
                Description = account.Description,
                ExpiresAtPersian = account.ExpiresAtPersian,
                CanCharge = account.CanCharge
            }).ToList(),
            Transactions = transactions.Select(item => new WalletTransactionDto
            {
                Id = item.Id,
                WalletType = item.WalletType,
                Title = item.Title,
                Description = item.Description,
                Amount = item.Amount,
                DatePersian = item.DatePersian,
                TimePersian = item.TimePersian
            }).ToList()
        };
    }
}
