namespace Authors.Models.Exceptions
{
    public class CourseNotFound : BaseApiException
    {
        public override string Message { get; }

        public CourseNotFound(string message) : base(System.Net.HttpStatusCode.NotFound)
        {
            Message = message;
        }
    }
}
