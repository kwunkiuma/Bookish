namespace Bookish.DataAccess
{
    class Account
    {
        public int UserId { get; set; }
        public string Username { get; set; }

        public override string ToString()
        {
            return $"{UserId} - {Username}";
        }
    }
}