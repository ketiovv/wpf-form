namespace footballers.Model
{
    public class Player
    {
        #region properties

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public uint Age { get; set; }
        public uint Weight { get; set; }

        #endregion

        #region constructors

        public Player(string firstName, string lastName, uint age, uint weight)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Weight = weight;
        }
        public Player(string firstName, string lastName) : this(firstName, lastName, 30, 75) { }

        #endregion

        #region methods

        public override string ToString()
        {
            return $"{FirstName} {LastName}, Age: {Age}, Weight: {Weight} kg";
        }

        #endregion
    }
}
