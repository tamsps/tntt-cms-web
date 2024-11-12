using System.Text.Json.Serialization;

namespace ThieuNhiTT.Web.Models
{
		public class BookCollection
		{
				[JsonPropertyName("books")]
				public List<Book> Books { get; set; }
		}
}
