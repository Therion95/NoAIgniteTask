using System;
using System.Collections.Generic;
using System.Linq;

namespace NoAIgniteTask
{
    public class DatesOrganiser
    {
        public DatesOrganiser(string[] dates)
        {
            DatesValidator datesValidator = new();
            DateSorter dateSorter = new();
            try
            {
                datesValidator.CheckIfInputEqualsTwo(dates);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                Environment.Exit(0);
            }
            var separator = '.';
            var possibleSeparators = new List<char>() {'.', '/', '-'};
        
            foreach (var date in dates)
            {
                datesValidator.ValidateDate(date);
                foreach (var sep in possibleSeparators.Where(date.Contains))
                {
                    separator = sep;
                }
                Date dateConverted = new (date, separator);
                dateSorter.ListOfBothDates.Add(dateConverted);
            }
            dateSorter.SortDates();
            Console.WriteLine(dateSorter);
        }
    }
}