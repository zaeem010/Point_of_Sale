namespace IMS.Models
{
    public class ToasterResult
    {
        public bool Success { get; set; } = false;

        public string Message { get; set; } = string.Empty;

        public string CssClass { get; set; } = string.Empty;

        public int Delay { get; set; } = 3500;
    }
}
