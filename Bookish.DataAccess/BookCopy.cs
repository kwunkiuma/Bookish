namespace Bookish.DataAccess
{
    class BookCopy
    {
        public int CopyId { get; set; }
        public string Isbn { get; set; }

        public override string ToString()
        {
            return $"{CopyId} - {Isbn}";
        }
    }
}