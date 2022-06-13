using System;

namespace NoAIgniteTask
{
    public class DatesValidator
    {
        public string? Day { get; set; }
        public string? Month { get; set; }
        public string? Year { get; set; }

        private readonly (PartsOfDate, string)[] _partsOfDate = new (PartsOfDate, string)[3];
        
        private int _maxLastDayOfTheMonth;
        private const int MaxLastMonthOfTheYear = 12;
        private const int MaxDefaultYear = 5000;

        public void ValidateDate(string date)
        {
            try
            {
                string[] dateParts = CheckIfDateSplitToThreeParts(date);
                AssignValuesToPropertiesAndAddToArray(dateParts);
                foreach (var part in _partsOfDate)
                {
                    CheckPartOfDate(part);
                }
            }
            catch (Exception ex)
            {
                if (ex is ArgumentOutOfRangeException or ArgumentException)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                    Environment.Exit(0);    
                }
            }
            
        }
        
        public void CheckIfInputEqualsTwo(string[] dates)
        {
            if (dates.Length != 2)
            {
                throw new ArgumentException("Wrong amount of parameters");
            }
        }

        public string[] CheckIfDateSplitToThreeParts(string date)
        {
            var dateParts = date.Split('.', '/', '-');
            if (dateParts.Length != 3)
            {
                throw new ArgumentOutOfRangeException("At least one of the inputed dates is too short or too long");
            }
            return dateParts;
        }

        public void AssignValuesToPropertiesAndAddToArray(string[] dateParts)
        {
            if (CheckIfFirstPartOfDateIsYear(dateParts))
            {
                Year = dateParts[0];
                Day = dateParts[2];
            }
            else
            {
                Day = dateParts[0];
                Year = dateParts[2];
            }
            Month = dateParts[1];

            _partsOfDate[0] = (PartsOfDate.Day, Day);
            _partsOfDate[1] = (PartsOfDate.Month, Month);
            _partsOfDate[2] = (PartsOfDate.Year, Year);
        }

        public static bool CheckIfFirstPartOfDateIsYear(string[] dateParts) => dateParts[0].Length == 4;
        
        private void SetupMaxValueOfTheDay()
        {
            _maxLastDayOfTheMonth = int.Parse(Month) switch
            {
                1 => 31,
                2 when int.Parse(Year) % 4 == 0 => _maxLastDayOfTheMonth = 29,
                2 => 28,
                3 => 31,
                4 => 30,
                5 => 31,
                6 => 30,
                7 => 31,
                8 => 31,
                9 => 30,
                10 => 31,
                11 => 30,
                12 => 31,
                _ => throw new ArgumentOutOfRangeException("Provided month is out of range")
            };
        }
        
        public void CheckPartOfDate((PartsOfDate, string) partOfDate)
        {
            if (!int.TryParse(partOfDate.Item2, out int number))
            {
                throw new ArgumentException("Provided input cannot be converted to number");
            }
            var convertedPartOfDate = Convert.ToInt32(partOfDate.Item2);
            if (partOfDate.Item1 == PartsOfDate.Day)
            {
                SetupMaxValueOfTheDay();
                if (convertedPartOfDate > _maxLastDayOfTheMonth || convertedPartOfDate < 0)
                {
                    throw new ArgumentOutOfRangeException("Provided day is out of range");
                }
            }
            else if (partOfDate.Item1 == PartsOfDate.Month && convertedPartOfDate is > MaxLastMonthOfTheYear or < 0)
            {
                throw new ArgumentOutOfRangeException("Provided month is out of range");
            }
            else if (partOfDate.Item1 == PartsOfDate.Year && convertedPartOfDate is > MaxDefaultYear or < 0)
            {
                throw new ArgumentOutOfRangeException("Provided year is out of range. The maximum default year is 5000");
            }
        }
    }
}