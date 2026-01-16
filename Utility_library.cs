using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace UtilityLibrary
{
    public static class Guard
    {
        public static void AgainstNull(object? value, string name)
        {
            if (value == null)
                throw new ArgumentNullException(name);
        }

        public static void AgainstOutOfRange(int value, string name)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(name);
        }
    }
    public static class Helpers
    {
        public static int SafeParseInt(string? input, int defaultValue = 0)
        {
            return int.TryParse(input, out int result) ? result : defaultValue;
        }

        public static DateTime SafeParseDate(string? input, DateTime defaultValue)
        {
            return DateTime.TryParse(input, out DateTime result) ? result : defaultValue;
        }

        public static TResult SafeExecute<TResult>(Func<TResult> func,TResult fallback,Action<Exception>? onError = null)
        {
            Guard.AgainstNull(func, nameof(func));

            try
            {
                return func();
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);
                return fallback;
            }
        }

        public static void Retry(Action action,int retryCount = 3,int delayMs = 0,Action<Exception, int>? onRetry = null)
        {
            Guard.AgainstNull(action, nameof(action));
            Guard.AgainstOutOfRange(retryCount, nameof(retryCount));

            for (int attempt = 1; attempt <= retryCount; attempt++)
            {
                try
                {
                    action();
                    return;
                }
                catch (Exception ex)
                {
                    onRetry?.Invoke(ex, attempt);

                    if (attempt == retryCount)
                        throw;

                    if (delayMs > 0)
                        Thread.Sleep(delayMs);
                }
            }
        }

        public static bool IsDateInRange(DateTime date, DateTime start, DateTime end)
        {
            return date >= start && date <= end;
        }
    }
    public static class Extensions
    {
        // String Extensions
        public static bool IsNullOrEmpty(this string? value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static string CleanString(this string? input)
        {
            return string.IsNullOrWhiteSpace(input) ? string.Empty : input.Trim();
        }

        public static string ToTitleCase(this string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            return string.Join(" ",
                value.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                     .Select(w => char.ToUpper(w[0]) + w.Substring(1).ToLower()));
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T>? source)
        {
            return source == null || !source.Any();
        }

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T>? source,Func<T, TKey> keySelector)
        {
            if (source == null)
                yield break;

            Guard.AgainstNull(keySelector, nameof(keySelector));

            var seen = new HashSet<TKey>();

            foreach (var item in source)
            {
                if (seen.Add(keySelector(item)))
                    yield return item;
            }
        }
        public static bool IsEven(this int number)
        {
            return number % 2 == 0;
        }

        public static bool IsBetween(this int number, int min, int max)
        {
            return number >= min && number <= max;
        }
        public static bool IsWeekend(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday ||
                   date.DayOfWeek == DayOfWeek.Sunday;
        }

        public static DateTime StartOfDay(this DateTime date)
        {
            return date.Date;
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("|||| Utility Library ||||");

            // String utilities
            string text = " Hello World  ";
            Console.WriteLine(text.CleanString().ToTitleCase());

            // Numeric utilities
            int num = 13;
            Console.WriteLine(num.IsEven());
            Console.WriteLine(num.IsBetween(5, 15));

            // Collection utilities
            var names = new List<string> { "A", "B", "C", "D" };
            Console.WriteLine(string.Join(", ", names.DistinctBy(x => x)));

            // Date utilities
            Console.WriteLine(DateTime.Now.IsWeekend());

            Console.WriteLine(DateTime.Now.StartOfDay());
        }
    }
}