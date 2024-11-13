using System.Text.Json.Serialization;

namespace ThieuNhiTT.Web.Models
{
		public class Lesson
		{
				public string BookId { get; set; }  // To link the lesson with the book
				public string LessonId { get; set; } // Unique identifier for each lesson
			  [JsonPropertyName("mainContent")]
				public List<string> MainContent { get; set; }

				[JsonPropertyName("questions")]
				public List<Question> Questions { get; set; }

				[JsonPropertyName("memoContent")]
				public List<string> MemoContent { get; set; }

				[JsonPropertyName("actions")]
				public List<string> Actions { get; set; }
	}
}
