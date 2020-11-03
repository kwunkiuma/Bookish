namespace Bookish.Web.Models
{
    public class NewBookViewModel
    {
        public string Title { get; }
        public string Author { get; }
        public string Isbn { get; }
        public int TotalCopies { get; }
        public string ErrorMessage { get; }

        public NewBookViewModel(string title, string author, string isbn, int totalCopies, bool isbnExists = false)
        {
            Title = title;
            Author = author;
            Isbn = isbn;
            TotalCopies = totalCopies;
            ErrorMessage = isbnExists
                ? "This book already exists"
                : "";
        }
    }
}