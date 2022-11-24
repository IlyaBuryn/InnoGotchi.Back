namespace InnoGotchi.API.Responses
{
    public class ErrorResponse
    {
        public IEnumerable<string>? Errors { get; set; }

        public ErrorResponse(string errorMessage)
        {
            Errors = new List<string>()
            {
                errorMessage
            };
        }
    }
}
