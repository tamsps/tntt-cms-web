using System.Text.Json.Serialization;

namespace ThieuNhiTT.Web.Models
{
		public class Book
		{
				[JsonPropertyName("bookId")]
				public string BookId { get; set; }

				[JsonPropertyName("bookTitle")]
				public string BookTitle { get; set; }

				[JsonPropertyName("imageUrl")]
				public string ImageUrl { get; set; }

				[JsonPropertyName("lessons")]
				public List<Lesson> Lessons { get; set; }
		}
}
