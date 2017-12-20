namespace Snapbook.Web.Infrastructure.Extensions
{
    using System;

    public static class StringExtensions
    {
        public static string CalculateDateDifference(this string date)
        {
            DateTime date1 = DateTime.Parse(date);
            DateTime date2 = DateTime.Now;

            int oldMonth = date2.Month;
            while (oldMonth == date2.Month)
            {
                date1 = date1.AddDays(-1);
                date2 = date2.AddDays(-1);
            }

            int years = 0, months = 0, days = 0;
            
            // getting number of years
            while (date2.CompareTo(date1) >= 0)
            {
                years++;
                date2 = date2.AddYears(-1);
            }
            date2 = date2.AddYears(1);
            years--;
            
            // getting number of months and days
            oldMonth = date2.Month;
            while (date2.CompareTo(date1) >= 0)
            {
                days++;
                date2 = date2.AddDays(-1);
                if ((date2.CompareTo(date1) >= 0) && (oldMonth != date2.Month))
                {
                    months++;
                    days = 0;
                    oldMonth = date2.Month;
                }
            }
            date2 = date2.AddDays(1);
            days--;

            if (years > 0) return $"posted {years.ToString()} years ago";
            if (months > 0) return $"posted {months.ToString()} months ago";
            if (days > 0) return $"posted {days.ToString()} days ago";
            else return "posted today";
        }

        public static string CutText(this string text)
        {
            if (text == null || text.Length <= 50)
            {
                return text;
            }

            var shortText = text.Substring(0, 50) + "...";
            return shortText;
        }
    }
}
