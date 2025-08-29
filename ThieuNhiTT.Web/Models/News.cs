using System.Text.Json.Serialization;

namespace ThieuNhiTT.Web.Models
{
	public class News
	{
		[JsonPropertyName("newsId")]
		public string NewsId { get; set; }

		[JsonPropertyName("newsTitle")]
		public string NewsTitle { get; set; }

		[JsonPropertyName("lessonId")]
		public string LessonId { get; set; }

		[JsonPropertyName("type")]
		public string Type { get; set; }

		[JsonPropertyName("shortDescription")]
		public string ShortDescription { get; set; }

		[JsonPropertyName("isShowMainHome")]
		public bool IsShowMainHome { get; set; }

		[JsonPropertyName("isShowSmallHome")]
		public bool IsShowSmallHome { get; set; }

		[JsonPropertyName("mainContent")]
		public List<string> MainContent { get; set; }

		[JsonPropertyName("datePublish")]
		public string DatePublish { get; set; }

		[JsonPropertyName("author")]
		public string Author { get; set; }

		[JsonPropertyName("newsImage")]
		public string NewsImage { get; set; }
	}
}
