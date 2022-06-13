namespace NoAIgniteTask
{
    public class Date
    {
        public string Day { get; }
        public string Month { get; }
        public string Year { get; }

        public string[] SplitDates { get; }
        public char Separator { get; }

        public Date(string date, char separator)
        {
            Separator = separator;
            var dateParts = date.Split(Separator);
            if (DatesValidator.CheckIfFirstPartOfDateIsYear(dateParts))
            {
                Year = dateParts[0];
                Day = dateParts[2];
            }
            else
            {
                Year = dateParts[2];
                Day = dateParts[0];
            }
            Month = dateParts[1];
            
            SplitDates = dateParts;
        }
    }
}