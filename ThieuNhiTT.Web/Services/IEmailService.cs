namespace ThieuNhiTT.Web.Services
{
	public interface IEmailService
	{
		public string smtpHost { get; set; }
		public string smtEmail { get; set; }
		public  int smtpPort { get; set; }
		public  string smtpUsername { get; set; }
		public  string displayName { get; set; }
		public  string smtpPassword { get; set; }
		public  bool enableSsl { get; set; }

		public Task<bool> SendEmailAsync(string toEmail, string subject, string body, string fromEmail = null);
		
	}
}
