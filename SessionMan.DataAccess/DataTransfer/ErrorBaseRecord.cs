namespace SessionMan.DataAccess.DataTransfer
{
    public record ErrorBaseRecord
    {
        public string Title { get; set; }
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}