namespace Authors.Models.Exceptions
{
    public class AuthorNotFound : BaseApiException
    {
        public override string Message { get; }

        public AuthorNotFound(string message) : base(System.Net.HttpStatusCode.NotFound)
        {
            Message = message;
        }
    }
}
