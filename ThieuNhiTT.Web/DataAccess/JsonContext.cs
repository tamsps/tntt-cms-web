namespace ThieuNhiTT.Web.DataAccess
{
	using System.IO;
	using System.Text.Json;
	using ThieuNhiTT.Web.Models;

	public class JsonContext
	{
		private readonly string _bookFilePath;
		private readonly string _lessonFilePath;
		private BookCollection _bookCollection;
		private LessonCollection _lessonCollection;
		public List<Book> Books => _bookCollection.Books;
		public List<Lesson> Lessons => _lessonCollection.Lessons;

		public JsonContext(string bookFilePath, string lessonFilePath)
		{
			_bookFilePath = bookFilePath;
			_lessonFilePath = lessonFilePath;
			LoadBookData();
			LoadLessonData();
		}

		private void LoadBookData()
		{
			if (File.Exists(_bookFilePath))
			{
				string json = File.ReadAllText(_bookFilePath);
				_bookCollection = JsonSerializer.Deserialize<BookCollection>(json) ?? new BookCollection { Books = new List<Book>() };
			}
			else
			{
				_bookCollection = new BookCollection { Books = new List<Book>() };
			}
		}
		private void LoadLessonData()
		{
			if (File.Exists(_lessonFilePath))
			{
				string json = File.ReadAllText(_lessonFilePath);
				_lessonCollection = JsonSerializer.Deserialize<LessonCollection>(json) ?? new LessonCollection { Lessons = new List<Lesson>() };
			}
			else
			{
				_lessonCollection = new LessonCollection { Lessons = new List<Lesson>() };
			}
		}
		public void SaveChanges()
		{
			string json = JsonSerializer.Serialize(_bookCollection, new JsonSerializerOptions { WriteIndented = true });
			File.WriteAllText(_bookFilePath, json);
		}
	}

}
