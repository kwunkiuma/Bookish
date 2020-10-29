using System;

namespace Bookish.DataAccess
{
    public class BookLoan
    {
        public int CopyId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Isbn { get; set; }
        public DateTime DueDate { get; set; }
    }
}