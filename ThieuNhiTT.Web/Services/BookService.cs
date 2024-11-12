namespace ThieuNhiTT.Web.Services
{
		using System.Collections.Generic;
		using ThieuNhiTT.Web.DataAccess;
		using ThieuNhiTT.Web.Models;

		public class BookService
		{
				private readonly IBookRepository _bookRepository;

				public BookService(IBookRepository bookRepository)
				{
						_bookRepository = bookRepository;
				}

				public List<Book> GetAllBooks() => _bookRepository.GetAllBooks();

				public Book GetBookById(string bookId) => _bookRepository.GetBookById(bookId);

				public List<Lesson> GetLessonsByBookId(string bookId) => _bookRepository.GetLessonsByBookId(bookId);

				public List<Question> GetQuestionsByLesson(string bookId, int lessonIndex) => _bookRepository.GetQuestionsByLesson(bookId, lessonIndex);

				public void AddBook(Book newBook) => _bookRepository.AddBook(newBook);

				public void UpdateBook(Book updatedBook) => _bookRepository.UpdateBook(updatedBook);

				public void DeleteBook(string bookId) => _bookRepository.DeleteBook(bookId);
		}

}
