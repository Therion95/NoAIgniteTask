using System;

namespace NoAIgniteTask // Note: actual namespace depends on the project name.
{
    public class Program
    {
        public static void Main(string[] dates)
        {
            DatesOrganiser datesOrganiser = new DatesOrganiser(dates);
            Console.ReadKey();
        }
    }
}