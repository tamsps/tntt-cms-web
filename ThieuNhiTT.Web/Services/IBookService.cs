using ThieuNhiTT.Web.Models;

namespace ThieuNhiTT.Web.Services
{
  public interface IBookService
  {
    IEnumerable<Book> GetAllBooks(string filePath);
    Book GetBookById(string bookId, string filePath);
    void CreateBook(Book book, string filePath);
    void UpdateBook(Book book, string filePath);
    void DeleteBook(string bookId, string filePath);
  }
}
