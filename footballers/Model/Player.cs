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

        public Player()
        {
        }

        public Player(string firstName, string lastName, uint age, uint weight)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Weight = weight;
        }

        #endregion

        #region methods

        public void Copy(Player player)
        {
            FirstName = player.FirstName;
            LastName = player.LastName;
            Age = player.Age;
            Weight = player.Weight;
        }
        public override string ToString()
        {
            return (FirstName + ", " + LastName + ", " + Age + "lat, " + Weight + "kg");
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Player player = obj as Player;
            return Age == player.Age && FirstName == player.FirstName && LastName == player.LastName && Weight == player.Weight;
        }

        #endregion
    }
}
