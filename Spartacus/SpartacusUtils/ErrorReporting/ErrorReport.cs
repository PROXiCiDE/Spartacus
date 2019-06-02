namespace SpartacusUtils.ErrorReporting
{
    internal class ErrorReport : IErrorReport
    {
        public ErrorReport(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}