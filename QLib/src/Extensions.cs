using System;
using System.Collections.Generic;
using System.Text;

namespace QLib
{

    // Date Utils
    public static class DateExt
    {
        public static string ShortForm(this DateTime date)
        {
            return date.Month + "/" + date.Day;
        }
    }

    // utilities
    public static class StringExt
    {
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
        public static DateTime ZeroTime(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        }
        public static bool IsSameAs(this DateTime date, DateTime other)
        {
            if (date.Day == other.Day &&
                date.Month == other.Month &&
                date.Year == other.Year)
                return true;
            return false;
        }
        public static bool IsToday(this DateTime date, int offset = 0)
        {
            return IsSameAs(date, Utils.Now.AddDays(offset));
        }
        public static bool IsThisMinDate(this DateTime date)
        {
            return date == DateTime.MinValue;
        }
    }


    public static class ListExtension
    {

        public static void Shuffle<T>(this IList<T> ts)
        {
            System.Random random = new Random();
            var count = ts.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i)
            {
                var r = random.Next(i, count);
                var tmp = ts[i];
                ts[i] = ts[r];
                ts[r] = tmp;
            }
        }
        public static bool Contains<T>(this T[] arr, T arrObj)
        {
            if (arr == null || arr.Length == 0)
                return false;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Equals(arrObj))
                    return true;
            }
            return false;
        }
    }
    public static class RandomExtension
    {
        public static int GetRange(this Random random, int minInclusive, int maxInclusive)
        {
            return random.Next(minInclusive, maxInclusive);
        }
        public static int GetRangeInclusive(this Random random, int minInclusive, int maxInclusive)
        {
            return random.Next(minInclusive, maxInclusive + 1);
        }
    }
}
