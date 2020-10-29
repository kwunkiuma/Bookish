using System.Collections.Generic;
using Bookish.DataAccess;

namespace Bookish.Web.Models
{
    public class LoansViewModel
    {
        public IEnumerable<BookLoan> BookLoans { get; }

        public LoansViewModel(IEnumerable<BookLoan> bookLoans)
        {
            BookLoans = bookLoans;
        }
    }
}