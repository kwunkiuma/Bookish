using System;

namespace Bookish.DataAccess
{
    class Loans
    {
        public int CopyId { get; set; }
        public int LenderId { get; set; }
        public DateTime DueDate { get; set; }

        public override string ToString()
        {
            return $"{CopyId} -> {LenderId} | {DueDate.ToShortDateString()}";
        }
    }
}
