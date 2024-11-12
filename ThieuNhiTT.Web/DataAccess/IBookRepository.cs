namespace ThieuNhiTT.Web.DataAccess
{
		using System.Collections.Generic;
		using ThieuNhiTT.Web.Models;

		public interface IBookRepository
		{
				List<Book> GetAllBooks();
				Book GetBookById(string bookId);
				List<Lesson> GetLessonsByBookId(string bookId);
				List<Question> GetQuestionsByLesson(string bookId, int lessonIndex);
				void AddBook(Book newBook);
				void UpdateBook(Book updatedBook);
				void DeleteBook(string bookId);
		}

}
