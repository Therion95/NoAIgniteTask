using System;
using System.Collections.Generic;
using NoAIgniteTask;
using NUnit.Framework;

namespace NoAIgniteTests
{
    public class Tests
    {
        [Test]
        [TestCase("12.12.2021")]
        [TestCase("12/12-2021")]
        [TestCase("12-12-2021")]
        public void Check_SplitDateLength_ShouldBeThreeParts(string date)
        {
            DatesValidator datesValidator = new();
            var splitDate = datesValidator.CheckIfDateSplitToThreeParts(date);
            Assert.AreEqual(3, splitDate.Length);
            Assert.AreEqual("12", splitDate[0]);
            Assert.AreEqual("12", splitDate[1]);
            Assert.AreEqual("2021", splitDate[2]);
        }

        [TestCase("12.12")]
        [TestCase("12/12-2021-2021")]
        public void Check_SplitDateLength_ShouldThrowExIfInputWrong(string date)
        {
            DatesValidator datesValidator = new();
            Assert.Throws<ArgumentOutOfRangeException>(() => datesValidator.CheckIfDateSplitToThreeParts(date));
        }

        [Test]
        public void Check_IfFirstPartOfDateIsYear_ReturnTrue()
        {
            var firstArray = new[] {"12", "12", "2021"};
            var secondArray = new[] {"2021", "12", "12"};

            var firstResult = DatesValidator.CheckIfFirstPartOfDateIsYear(firstArray);
            var secondResult = DatesValidator.CheckIfFirstPartOfDateIsYear(secondArray);

            Assert.IsFalse(firstResult);
            Assert.IsTrue(secondResult);
        }

        [Test]
        public void Check_DatePartsAreAssignToCorrectProperties_DependsOnInput()
        {
            var firstArray = new[] {"12", "12", "2021"};
            var secondArray = new[] {"2021", "12", "12"};
            DatesValidator datesValidator = new();
            DatesValidator datesValidator2 = new();

            datesValidator.AssignValuesToPropertiesAndAddToArray(firstArray);
            datesValidator2.AssignValuesToPropertiesAndAddToArray(secondArray);

            Assert.AreEqual(firstArray[2], datesValidator.Year);
            Assert.AreEqual(secondArray[0], datesValidator2.Year);
        }

        [Test]
        public void Check_DatesAreSortedChronologically()
        {
            var firstDate = new Date("12.12.2021", '.'); //testing different days
            var secondDate = new Date("14.12.2021", '.'); //testing different days
            var thirdDate = new Date("12-06-2000", '-'); //testing different months
            var fourthDate = new Date("12-05-2000", '-'); //testing different months
            var fifthDate = new Date("1995/12/12", '/'); //testing different years
            var sixthDate = new Date("1990/12/13", '/'); //testing different years

            DateSorter dateSorter = new();
            DateSorter dateSorter2 = new();
            DateSorter dateSorter3 = new();

            dateSorter.ListOfBothDates.Add(firstDate);
            dateSorter.ListOfBothDates.Add(secondDate);
            dateSorter2.ListOfBothDates.Add(thirdDate);
            dateSorter2.ListOfBothDates.Add(fourthDate);
            dateSorter3.ListOfBothDates.Add(fifthDate);
            dateSorter3.ListOfBothDates.Add(sixthDate);

            dateSorter.SortDates();
            dateSorter2.SortDates();
            dateSorter3.SortDates();


            //List should be ordered chronologically
            Assert.AreEqual(new List<Date>() {firstDate, secondDate}, dateSorter.ListOfBothDates);
            Assert.AreEqual(new List<Date>() {fourthDate, thirdDate}, dateSorter2.ListOfBothDates);
            Assert.AreEqual(new List<Date>() {sixthDate, fifthDate}, dateSorter3.ListOfBothDates);
        }

        [Test]
        public void Check_FinalResultIfIsCorrect()
        {
            var firstDate = new Date("12.12.2021", '.');
            var secondDate = new Date("14.12.2021", '.');
            var thirdDate = new Date("12-06-2000", '-');
            var fourthDate = new Date("12-05-2000", '-');
            var fifthDate = new Date("1995/12/12", '/');
            var sixthDate = new Date("1990/12/13", '/');

            DateSorter dateSorter = new();
            DateSorter dateSorter2 = new();
            DateSorter dateSorter3 = new();

            dateSorter.ListOfBothDates.Add(firstDate);
            dateSorter.ListOfBothDates.Add(secondDate);
            dateSorter2.ListOfBothDates.Add(thirdDate);
            dateSorter2.ListOfBothDates.Add(fourthDate);
            dateSorter3.ListOfBothDates.Add(fifthDate);
            dateSorter3.ListOfBothDates.Add(sixthDate);

            dateSorter.SortDates();
            dateSorter2.SortDates();
            dateSorter3.SortDates();

            Assert.AreEqual("12-14.12.2021", dateSorter.ToString());
            Assert.AreEqual("12-05 - 12-06-2000", dateSorter2.ToString());
            Assert.AreEqual("1990/12/13 - 1995/12/12", dateSorter3.ToString());
        }

        [Test]
        public void Check_InputDatesSplitToTwoParts_ThrowsExIfNot()
        {
            var array = new[] {"12.12.1212", "12.12.1213", "12.12.2021"};
            var array2 = new[] {"12.12.1212"};
            DatesValidator datesValidator = new();
            Assert.Throws<ArgumentException>(() => datesValidator.CheckIfInputEqualsTwo(array));
            Assert.Throws<ArgumentException>(() => datesValidator.CheckIfInputEqualsTwo(array2));
        }

        [Test]
        public void TryParse_PartOfDateAsStringToInt_ThrowsExIfError()
        {
            DatesValidator datesValidator = new();
            var datePart1 = (day: PartsOfDate.Day, "date");
            var datePart2 = (month: PartsOfDate.Month, "simply text");

            Assert.Throws<ArgumentException>(() => datesValidator.CheckPartOfDate(datePart1));
            Assert.Throws<ArgumentException>(() => datesValidator.CheckPartOfDate(datePart2));
        }

        [Test]
        public void Check_PartOfDateIfInCorrectRange_ThrowsExIfNot()
        {
            DatesValidator datesValidator = new() { Month = "6" };
            var datePart1 = (day: PartsOfDate.Day, "43");
            var datePart2 = (month: PartsOfDate.Month, "13");
            var datePart3 = (year: PartsOfDate.Year, "-1");

            Assert.Throws<ArgumentOutOfRangeException>(() => datesValidator.CheckPartOfDate(datePart1));
            Assert.Throws<ArgumentOutOfRangeException>(() => datesValidator.CheckPartOfDate(datePart2));
            Assert.Throws<ArgumentOutOfRangeException>(() => datesValidator.CheckPartOfDate(datePart3));
        }

        [Test]
        public void Check_EveryPartOfDateIfCorrect_ThrowsExIfNot()
        {
            var array = new[] {"12.12.1212", "12.12.1213", "12.12.2021"};
            var array2 = new[] {"12.12.1212"};
            DatesValidator datesValidator = new();
            Assert.Throws<ArgumentException>(() => datesValidator.CheckIfInputEqualsTwo(array));
            Assert.Throws<ArgumentException>(() => datesValidator.CheckIfInputEqualsTwo(array2));
        }
    }
}    