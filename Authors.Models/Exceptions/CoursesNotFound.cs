namespace Authors.Models.Exceptions
{
    public class CoursesNotFound : BaseApiException
    {
        public override string Message { get; }

        public CoursesNotFound(string message) : base(System.Net.HttpStatusCode.NotFound)
        {
            Message = message;
        }
    }
}
