using System.Xml.Linq;

namespace ObligatoriskOpgave1
{
    public class Trophy
    {
        public int Id { get; set; }
        public string Competition { get; set; }
        public int Year { get; set; }

        public override string ToString()
        {
            return $"{{{nameof(Id)}={Id.ToString()}, {nameof(Competition)}={Competition}, {nameof(Year)}={Year.ToString()}}}";
        }

        public void ValidateCompetition()
        {
            if (Competition == null) throw new ArgumentNullException("Competition is null");
            if (Competition.Length < 3) throw new ArgumentException("Competition must be at least 4 characters: " + Competition);
        }

        public void ValidateYear()
        {
            if (Year < 1970 || Year > 2024) throw new ArgumentOutOfRangeException("Birth year must be between 1970 and 2024: " + Year);
        }

        public void Validate()
        {
            ValidateCompetition();
            ValidateYear();
        }
    }
}
