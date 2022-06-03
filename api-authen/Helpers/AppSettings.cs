namespace Trading.Authen.Api.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUser { set; get;}
        public string SmtpPass { set; get;}
        public string FromMail { set; get;}
    }
}