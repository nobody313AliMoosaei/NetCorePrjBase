using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCorePrjBase.DL.PublicModels
{
    public class DateTimeModelDTO
    {
        public DateTimeModelDTO(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public DateTime DateTime { get; set; }

        public static DateTimeModelDTO operator -(DateTimeModelDTO a, DateTimeModelDTO b)
        {
            var Year = a.Year - b.Year;
            var Month = a.Month - b.Month;
            var Day = a.Day - b.Day;
            if (Day < 0)
            {
                Month--;
                Day += 30;
            }
            if (Month < 0)
            {
                Year--;
                Month += 12;
            }
            return new DateTimeModelDTO(Year, Month, Day);
        }

        public static bool operator >(DateTimeModelDTO a, DateTimeModelDTO b)
        {
            if (a.Year != b.Year) return a.Year > b.Year;
            else if (a.Month != b.Month) return a.Month > b.Month;
            else if (a.Day != b.Day) return a.Day > b.Day;
            return false;
        }

        public static bool operator <(DateTimeModelDTO a, DateTimeModelDTO b)
        {
            if (a.Year != b.Year) return a.Year < b.Year;
            else if ( a.Month != b.Month) return a.Month < b.Month;
            else if (a.Day != b.Day) return a.Day < b.Day;
            return false;
        }

        public static bool operator ==(DateTimeModelDTO a, DateTimeModelDTO b)
        {
            if (a.Year == b.Year && a.Month == b.Month && a.Day == b.Day) return true;
            return false;
        }

        public static bool operator !=(DateTimeModelDTO a, DateTimeModelDTO b)
        {
            if (a.Year != b.Year || a.Month != b.Month || a.Day != b.Day) return true;
            return false;
        }

        public static bool operator >=(DateTimeModelDTO a, DateTimeModelDTO b)
        => ( a > b || a == b);

        public static bool operator <=(DateTimeModelDTO a, DateTimeModelDTO b)
        => (a < b || a == b);

    }
}
