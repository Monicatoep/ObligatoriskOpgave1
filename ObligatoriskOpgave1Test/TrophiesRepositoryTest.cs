using ObligatoriskOpgave1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ObligatoriskOpgave1Test
{
    [TestClass]
    public class TrophiesRepositoryTest
    {
        private TrophiesRepository _trophies;
        private readonly Trophy trophyBadCompetition = new() { Competition = "BB", Year = 2020 };
        private readonly Trophy trophyBadYear = new() { Competition = "Skiing", Year = 1819 };

        [TestInitialize]
        public void Init()
        {
            _trophies = new TrophiesRepository();
        }

        [TestMethod]
        public void AddTest()
        {
            Assert.ThrowsException<ArgumentException>(() => _trophies.Add(trophyBadCompetition));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _trophies.Add(trophyBadYear));

            Trophy trophyToAdd = new() { Competition = "Swimming", Year = 2019 };
            _trophies.Add(trophyToAdd);

            Assert.AreEqual(6, _trophies.Get().Count());
            Assert.AreEqual("Swimming", _trophies.GetById(6).Competition);
        }

        [TestMethod]
        public void GetTest()
        {
            IEnumerable<Trophy> trophies = _trophies.Get();
            Assert.AreEqual(5, trophies.Count());
            Assert.AreEqual(trophies.First().Competition, "Swimming");

            IEnumerable<Trophy> trophiesYearAfter = _trophies.Get(yearAfter: 1990);
            Assert.AreEqual(4, trophiesYearAfter.Count());

            IEnumerable<Trophy> trophiesYearBefore = _trophies.Get(yearBefore: 1980);
            Assert.AreEqual(1, trophiesYearBefore.Count());

            IEnumerable<Trophy> sortedTrophiesCompetition = _trophies.Get(orderBy: "competition");
            Assert.AreEqual(sortedTrophiesCompetition.First().Competition, "Basketball");

            IEnumerable<Trophy> sortedTrophiesYear = _trophies.Get(orderBy: "year");
            Assert.AreEqual(sortedTrophiesYear.First().Competition, "Football");
        }

        [TestMethod]
        public void GetByIdTest()
        {
            Assert.IsNull(_trophies.GetById(100));
            Assert.IsNotNull(_trophies.GetById(1));
            Assert.AreEqual("Swimming", _trophies.GetById(1).Competition);
            Assert.AreEqual("Jumping", _trophies.GetById(3).Competition);
        }

        [TestMethod]
        public void RemoveTest()
        {
            Assert.IsNull(_trophies.Remove(100));
            Assert.AreEqual(1, _trophies.Remove(1)?.Id);
            Assert.AreEqual(4, _trophies.Get().Count());
        }

        [TestMethod]
        public void UpdateTest()
        {
            Trophy? trophyToBeUpdated = _trophies.GetById(1);

            Assert.ThrowsException<ArgumentException>(() => _trophies.Update(trophyToBeUpdated.Id, trophyBadCompetition));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _trophies.Update(trophyToBeUpdated.Id, trophyBadYear));

            Trophy trophyToUpdateWith = new() { Competition = "Handball", Year = 1984 };
            Assert.IsNull(_trophies.Update(100, trophyToUpdateWith));

            _trophies.Update(trophyToBeUpdated.Id, trophyToUpdateWith);

            Assert.AreEqual(5, _trophies.Get().Count());
            Assert.AreEqual(_trophies.GetById(1).Competition, trophyToUpdateWith.Competition);
        }
    }
}
