namespace Bookish.Web.Models
{
    public class NewBookViewModel
    {
        public string Title { get; }
        public string Author { get; }
        public string Isbn { get; }
        public int TotalCopies { get; }
        public string Message { get; }

        public NewBookViewModel(string title, string author, string isbn, int totalCopies, string message="")
        {
            Title = title;
            Author = author;
            Isbn = isbn;
            TotalCopies = totalCopies;
            Message = message;
        }
    }
}