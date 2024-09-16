using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ObligatoriskOpgave1
{
    public class TrophiesRepository
    {
        private int _nextId = 1;
        private readonly List<Trophy> _trophies = new();

        public TrophiesRepository()
        {
            _trophies.Add(new Trophy() { Id = _nextId++, Competition = "Swimming", Year = 1999 });
            _trophies.Add(new Trophy() { Id = _nextId++, Competition = "Climbing", Year = 2020 });
            _trophies.Add(new Trophy() { Id = _nextId++, Competition = "Jumping", Year = 1993 });
            _trophies.Add(new Trophy() { Id = _nextId++, Competition = "Football", Year = 1977 });
            _trophies.Add(new Trophy() { Id = _nextId++, Competition = "Basketball", Year = 2011 });
        }

        public Trophy Add(Trophy trophy)
        {
            trophy.Validate();
            trophy.Id = _nextId++;
            _trophies.Add(trophy);
            return trophy;
        }

        public IEnumerable<Trophy?> Get(int? yearAfter = null, int? yearBefore = null, string? orderBy = null)
        {
            IEnumerable<Trophy> result = new List<Trophy>(_trophies);


            if (yearAfter != null)
            {
                result = result.Where(m => m.Year >= yearAfter);
            }
            if (yearBefore != null)
            {
                result = result.Where(m => m.Year <= yearBefore);
            }


            if (orderBy != null)
            {
                orderBy = orderBy.ToLower();
                switch (orderBy)
                {
                    case "competition":
                    case "competition_asc":
                        result = result.OrderBy(m => m.Competition);
                        break;
                    case "competition_desc":
                        result = result.OrderByDescending(m => m.Competition);
                        break;
                    case "year":
                    case "year_asc":
                        result = result.OrderBy(m => m.Year);
                        break;
                    case "year_desc":
                        result = result.OrderByDescending(m => m.Year);
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        public Trophy? GetById(int id)
        {
            return _trophies.Find(a => a.Id == id);
        }

        public Trophy? Remove(int id)
        {
            Trophy? trophyToRemove = GetById(id);
            if (trophyToRemove != null)
            {
                _trophies.Remove(trophyToRemove);
                return trophyToRemove;
            }
            return null;
        }

        public Trophy? Update(int id, Trophy trophy)
        {
            trophy.Validate();
            Trophy? trophyToUpdate = GetById(id);
            if (trophyToUpdate != null)
            {
                trophyToUpdate.Competition = trophy.Competition;
                trophyToUpdate.Year = trophy.Year;
                return trophyToUpdate;
            }
            return null;
        }
    }
}
