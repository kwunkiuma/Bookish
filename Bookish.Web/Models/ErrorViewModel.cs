namespace Bookish.Web.Models
{
    public class ErrorViewModel
    {
        public int StatusCode { get; }
        public string ErrorMessage { get; }

        public ErrorViewModel(int statusCode)
        {
            StatusCode = statusCode;

            ErrorMessage = statusCode switch
            {
                403 => "Forbidden",
                404 => "Page not found",
                500 => "Internal server error",
                _ => "An error has occurred"
            };
        }
    }
}
