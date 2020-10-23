namespace Bookish.DataAccess
{
    public class Account
    {
        public string Username { get; set; }
        public string Pass { get; set; }

        public override string ToString()
        {
            return $"{Username}, {Pass}";
        }
    }
}