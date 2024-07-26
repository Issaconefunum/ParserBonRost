namespace ApiTestParser.ConfigurationManager
{
    public class ParserSettings
    {
        public string Url { get; set; } = string.Empty;
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
        public int MaxProductCount { get; set; }
        public int WaitTimeout { get; set; }
    }
}
