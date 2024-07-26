namespace ApiTestParser.Models
{
    public record App
    {
        public string AppId {  get; set; } = string.Empty;
        public string AppSecret {  get; set; } = string.Empty;
        public App(string AppId, string AppSecret) 
        {
            this.AppId = AppId;
            this.AppSecret = AppSecret;
        }
    }
}
