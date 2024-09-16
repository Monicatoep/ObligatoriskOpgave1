using ObligatoriskOpgave1;
using System;
using System.Numerics;

namespace ObligatoriskOpgave1Test
{
    [TestClass]
    public class TrophyTest
    {
        private Trophy trophy = new() { Id = 1, Competition = "Climbing", Year = 1980 };
        private Trophy trophyCompetitionNull = new() { Id = 2, Competition = null, Year = 1980 };
        private Trophy trophyCompetitionShort = new() { Id = 3, Competition = "BB", Year = 1980 };
        private Trophy trophyYearLow = new() { Id = 4, Competition = "Swimming", Year = 1950 };
        private Trophy trophyYearHigh = new() { Id = 5, Competition = "Jumping", Year = 2025 };

        [TestMethod()]
        public void ToStringTest()
        {
            string str = trophy.ToString();
            Assert.AreEqual("{Id=1, Competition=Climbing, Year=1980}", str);
        }

        [TestMethod()]
        public void ValidateCompetitionTest()
        {
            trophy.ValidateCompetition();
            Assert.ThrowsException<ArgumentNullException>(() => trophyCompetitionNull.ValidateCompetition());
            Assert.ThrowsException<ArgumentException>(() => trophyCompetitionShort.ValidateCompetition());
        }


        [TestMethod()]
        public void ValidateYearTest()
        {
            trophy.ValidateYear();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => trophyYearLow.ValidateYear());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => trophyYearHigh.ValidateYear());
        }


        //Tester grænseværdier ved hjælp af datarow
        [TestMethod()]
        [DataRow(1970)]
        [DataRow(1971)]
        [DataRow(2023)]
        [DataRow(2024)]
        public void ValidateYearsTest(int year)
        {
            trophy.Year = year;
            trophy.ValidateYear();
        }

        //Tester grænseværdier ved hjælp af datarow
        [TestMethod()]
        [DataRow(1969)]
        [DataRow(2025)]
        public void ValidateYearsFailTest(int year)
        {
            trophy.Year = year;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => trophy.ValidateYear());
        }

        [TestMethod()]
        public void ValidateTest()
        {
            trophy.Validate();
            Assert.ThrowsException<ArgumentNullException>(() => trophyCompetitionNull.Validate());
            Assert.ThrowsException<ArgumentException>(() => trophyCompetitionShort.Validate());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => trophyYearHigh.Validate());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => trophyYearLow.Validate());
        }
    }
}