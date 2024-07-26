namespace ApiTestParser.ConfigurationManager
{
    public static class Configurations
    {
        public static ParserSettings ParserSettings { get; set; } = new ParserSettings();
        public static void SetPropties(IConfiguration configuration)
        {
            ParserSettings = configuration.GetSection("ParserSettings").Get<ParserSettings>()
                ?? throw new InvalidOperationException("Section not found");
        }
    }
}
