namespace ThieuNhiTT.Web.Models
{
	public class EmailSenderOptions
	{
		public string Username { get; set; }
		public string DisplayName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Host { get; set; }
		public int Port { get; set; }
		public bool EnableSSL { get; set; }
	}
	public class EmailReceiver
	{
		public string Username { get; set; }
		public string DisplayName { get; set; }
		public string Email { get; set; }
	}
}
