using System.Text.Json.Serialization;

namespace ThieuNhiTT.Web.Models
{
		public class Question
		{
				[JsonPropertyName("ask")]
				public string Ask { get; set; }

				[JsonPropertyName("answer")]
				public List<string> Answer { get; set; }
		}
}
