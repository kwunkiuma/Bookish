namespace Bookish.DataAccess
{
    public class Book
    {
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }

        public override string ToString()
        {
            return $"{Isbn} - {Title} by {Author} | {AvailableCopies}/{TotalCopies}";
        }
    }
}
