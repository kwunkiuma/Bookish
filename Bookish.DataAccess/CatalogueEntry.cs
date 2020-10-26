namespace Bookish.DataAccess
{
    public class CatalogueEntry
    {
        public string Title{ get; set; }
        public string Author { get; set; }
        public string Isbn { get; set; }
        public int NumCopies { get; set; }
        public int NumAvailable { get; set; }
    }
}