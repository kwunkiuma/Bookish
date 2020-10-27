using System;

namespace Bookish.DataAccess
{
    public class CopiesEntry
    {
        public int CopyId { get; set; }
        public string Username { get; set; }
        public DateTime DueDate { get; set; }
    }
}
