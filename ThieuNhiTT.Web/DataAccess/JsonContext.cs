namespace ThieuNhiTT.Web.DataAccess
{
		using System.IO;
		using System.Text.Json;
		using ThieuNhiTT.Web.Models;

		public class JsonContext
		{
				private readonly string _filePath;
				private BookCollection _bookCollection;

				public JsonContext(string filePath)
				{
						_filePath = filePath;
						LoadData();
				}

				private void LoadData()
				{
						if (File.Exists(_filePath))
						{
								string json = File.ReadAllText(_filePath);
								_bookCollection = JsonSerializer.Deserialize<BookCollection>(json) ?? new BookCollection { Books = new List<Book>() };
						}
						else
						{
								_bookCollection = new BookCollection { Books = new List<Book>() };
						}
				}

				public List<Book> Books => _bookCollection.Books;

				public void SaveChanges()
				{
						string json = JsonSerializer.Serialize(_bookCollection, new JsonSerializerOptions { WriteIndented = true });
						File.WriteAllText(_filePath, json);
				}
		}

}
