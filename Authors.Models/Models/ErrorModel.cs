namespace Authors.Models.Models
{
    public class ErrorModel
    {
        public string FieldName { get; set; }

        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}
