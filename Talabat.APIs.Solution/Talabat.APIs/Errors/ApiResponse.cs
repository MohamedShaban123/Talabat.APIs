
namespace Talabat.APIs.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public ApiResponse(int statuscode, string? message = null)
        {
            StatusCode = statuscode;
            Message = message ?? GetDefaultMessageForStatusCode(statuscode);
        }

        private string? GetDefaultMessageForStatusCode(int statuscode)
        {
            // new switch feature
            return statuscode switch

            {
                400 => "A bad request , you have made",
                401 => "Authorized , you are not",
                404 => "Resource was not found",
                500 => "Errors are the path to the dark side. Errors lead to anger",
                _ => null,
            };


        }
    }
}