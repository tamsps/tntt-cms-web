using System.Text.Json.Serialization;

namespace ThieuNhiTT.Web.Models
{
	public class LessonCollection
	{
		[JsonPropertyName("lessons")]
		public List<Lesson> Lessons { get; set; }
	}
}
