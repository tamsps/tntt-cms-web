namespace ThieuNhiTT.Web.DataAccess
{
		using System.Collections.Generic;
		using System.Linq;
		using ThieuNhiTT.Web.Models;

		public class JsonBookRepository : IBookRepository
		{
				private readonly JsonContext _context;

				public JsonBookRepository(string jsonFilePath, string lessonFilePath)
				{
						_context = new JsonContext(jsonFilePath, lessonFilePath);
				}

				public List<Book> GetAllBooks() => _context.Books;

				public Book GetBookById(string bookId) => _context.Books.FirstOrDefault(book => book.BookId == bookId);

				public List<Lesson> GetLessonsByBookId(string bookId)
				{
						var book = GetBookById(bookId);
						return book?.Lessons ?? new List<Lesson>();
				}

				public List<Question> GetQuestionsByLesson(string bookId, int lessonIndex)
				{
						var book = GetBookById(bookId);
						return book != null && lessonIndex >= 0 && lessonIndex < book.Lessons.Count
								? book.Lessons[lessonIndex].Questions
								: new List<Question>();
				}

				public void AddBook(Book newBook)
				{
						_context.Books.Add(newBook);
						_context.SaveChanges();
				}

				public void UpdateBook(Book updatedBook)
				{
						var existingBook = GetBookById(updatedBook.BookId);
						if (existingBook != null)
						{
								existingBook.BookTitle = updatedBook.BookTitle;
								existingBook.ImageUrl = updatedBook.ImageUrl;
								existingBook.Lessons = updatedBook.Lessons;
								_context.SaveChanges();
						}
				}

				public void DeleteBook(string bookId)
				{
						var bookToDelete = GetBookById(bookId);
						if (bookToDelete != null)
						{
								_context.Books.Remove(bookToDelete);
								_context.SaveChanges();
						}
				}
		}

}
