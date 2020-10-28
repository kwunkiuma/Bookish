using System;

namespace Bookish.DataAccess
{
    public class BookCopy
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int CopyId { get; set; }
        public string Username { get; set; }
        public DateTime DueDate { get; set; }
    }
}
