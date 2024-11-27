namespace ThieuNhiTT.Web.Services
{
		using System.Collections.Generic;
		using ThieuNhiTT.Web.DataAccess;
		using ThieuNhiTT.Web.Models;

  public class BookService : IBookService
  {
    private readonly IRepository<Book> _bookRepository;

    public BookService(IRepository<Book> bookRepository)
    {
      _bookRepository = bookRepository;
    }

    public IEnumerable<Book> GetAllBooks(string filePath) => _bookRepository.GetAll(filePath);

    public Book GetBookById(string bookId, string filePath) => _bookRepository.GetById("BookId",bookId, filePath);

    public void CreateBook(Book book, string filePath)
    {
      _bookRepository.Add(book, filePath);
    }

    public void UpdateBook(Book book, string filePath)
    {
      _bookRepository.Update(book, filePath);
    }

    public void DeleteBook(string bookId, string filePath)
    {
      _bookRepository.Delete("BookId", bookId, filePath);
    }
  }

}
