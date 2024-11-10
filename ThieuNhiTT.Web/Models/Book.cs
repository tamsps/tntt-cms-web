namespace ThieuNhiTT.Web.Models
{
		public class Book
		{
				public string BookId { get; set; }
				public string BookTitle { get; set; }
				public string ImageUrl { get; set; }
				public List<Lesson> Lessons { get; set; }
		}
}
