using System.ComponentModel;
using System.Linq.Expressions;

namespace NamiCustomers.Infrastucture.Utilities
{
    public static class LinqUtility
    {
        //moradi
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }
        //moradi
        public static string GetPersianDescription<T>(this T t, string memberName) where T : class
        {
            var memberInfo = t.GetType().GetMember(memberName)[0];
            var descriptionAttribute = memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), inherit: false)[0] as DescriptionAttribute;
            return descriptionAttribute.Description;
        }

        //moradi
        public static IEnumerable<TSource> WhereIf<TSource>(
        this IEnumerable<TSource> source,
        bool condition,
        Expression<Func<TSource, bool>> predicate)
        {
            if (condition)
                return source.AsQueryable().Where(predicate);
            else
                return source;
        }

    }
}
