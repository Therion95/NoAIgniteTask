using System.Collections.Generic;
using System.Linq;

namespace NoAIgniteTask
{
    public class DateSorter
    {
        public List<Date> ListOfBothDates;

        public DateSorter()
        {
            ListOfBothDates = new List<Date>();
        }

        public void SortDates()
        {
            if (ListOfBothDates[0].Year != ListOfBothDates[1].Year)
            {
                ListOfBothDates = ListOfBothDates.OrderBy(date => date.Year).ToList();
            }
            else if (ListOfBothDates[0].Month != ListOfBothDates[1].Month)
            {
                ListOfBothDates = ListOfBothDates.OrderBy(date => date.Month).ToList();
            }
            else if (ListOfBothDates[0].Day == ListOfBothDates[1].Day)
            {
                ListOfBothDates = ListOfBothDates.OrderBy(date => date.Day).ToList();
            }
        }

        public override string ToString()
        {
            var separator = ListOfBothDates[0].Separator;
            var dateWithYearFirst = ListOfBothDates[0].SplitDates[0].Length == 4;
            if (dateWithYearFirst)
            {
                if (ListOfBothDates[0].Year != ListOfBothDates[1].Year)
                {
                    return ListOfBothDates[0].Year + $"{separator}" + ListOfBothDates[0].Month 
                           + $"{separator}" + ListOfBothDates[0].Day + " - " + ListOfBothDates[1].Year 
                           + $"{separator}" + ListOfBothDates[1].Month + $"{separator}" + ListOfBothDates[1].Day;
                }
                if (ListOfBothDates[0].Month != ListOfBothDates[1].Month)
                {
                    return ListOfBothDates[0].Year + $"{separator}" + ListOfBothDates[0].Day + $"{separator}" + ListOfBothDates[0].Month +
                           " - " + ListOfBothDates[1].Day + $"{separator}" + ListOfBothDates[1].Month;
                }
                if (ListOfBothDates[0].Day != ListOfBothDates[1].Day)
                {
                    return ListOfBothDates[1].Year + $"{separator}" + ListOfBothDates[0].Month + $"{separator}" 
                           + ListOfBothDates[0].Day + "-" + ListOfBothDates[1].Day;
                }

                return ListOfBothDates[0].Year + $"{separator}" + ListOfBothDates[0].Month + $"{separator}" + ListOfBothDates[0].Day;
            }

            if (ListOfBothDates[0].Year != ListOfBothDates[1].Year)
                return ListOfBothDates[0].Day + $"{separator}" + ListOfBothDates[0].Month + $"{separator}"
                       + ListOfBothDates[0].Year + " - " + ListOfBothDates[1].Day +
                       $"{separator}" + ListOfBothDates[1].Month + $"{separator}" + ListOfBothDates[1].Year;
            
            if (ListOfBothDates[0].Month != ListOfBothDates[1].Month)
            {
                return ListOfBothDates[0].Day + $"{separator}" + ListOfBothDates[0].Month + " - "
                       + ListOfBothDates[1].Day + $"{separator}" + ListOfBothDates[1].Month + $"{separator}" +
                       ListOfBothDates[1].Year;
            }

            if (ListOfBothDates[0].Day != ListOfBothDates[1].Day)
            {
                return ListOfBothDates[0].Day + "-" + ListOfBothDates[1].Day + $"{separator}" +
                       ListOfBothDates[1].Month + $"{separator}" + ListOfBothDates[1].Year;
            }

            return ListOfBothDates[0].Day + $"{separator}" + ListOfBothDates[0].Month + $"{separator}" + ListOfBothDates[0].Year;
        }
    }
}