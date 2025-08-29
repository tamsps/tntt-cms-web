namespace ThieuNhiTT.Web.Services
{
		using System.Collections.Generic;
		using ThieuNhiTT.Web.DataAccess;
		using ThieuNhiTT.Web.Models;

  public class BookService : IBookService
  {
    private readonly IRepository<Book> _bookRepository;
    private readonly ILogger<BookService> _logger;

    public BookService(IRepository<Book> bookRepository, ILogger<BookService> logger)
    {
      _bookRepository = bookRepository;
      _logger = logger;
    }

    public IEnumerable<Book> GetAllBooks(string filePath)
    {
      _logger.LogDebug("Getting all books from {FilePath}", filePath);
      var books = _bookRepository.GetAll(filePath);
      _logger.LogInformation("Retrieved {BookCount} books", books.Count());
      return books;
    }

    public Book GetBookById(string bookId, string filePath)
    {
      _logger.LogDebug("Getting book with ID {BookId} from {FilePath}", bookId, filePath);
      var book = _bookRepository.GetById("BookId", bookId, filePath);
      if (book != null)
      {
        _logger.LogInformation("Found book {BookId}: {BookTitle}", bookId, book.BookTitle);
      }
      else
      {
        _logger.LogWarning("Book not found with ID {BookId}", bookId);
      }
      return book;
    }

    public void CreateBook(Book book, string filePath)
    {
      _logger.LogInformation("Creating new book {BookId}: {BookTitle}", book.BookId, book.BookTitle);
      _bookRepository.Add(book, filePath);
      _logger.LogInformation("Successfully created book {BookId}", book.BookId);
    }

    public void UpdateBook(Book book, string filePath)
    {
      _logger.LogInformation("Updating book {BookId}: {BookTitle}", book.BookId, book.BookTitle);
      _bookRepository.Update(book, filePath);
      _logger.LogInformation("Successfully updated book {BookId}", book.BookId);
    }

    public void DeleteBook(string bookId, string filePath)
    {
      _logger.LogInformation("Deleting book {BookId}", bookId);
      _bookRepository.Delete("BookId", bookId, filePath);
      _logger.LogInformation("Successfully deleted book {BookId}", bookId);
    }
  }

}
