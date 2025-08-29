namespace ThieuNhiTT.Web.DataAccess
{
  public interface IRepository<T>
  {
    IEnumerable<T> GetAll(string filePath);
    T GetById(string fielddName, string fieldValue, string filePath);
    void Add(T item, string filePath);
    void Update(T item, string filePath);
    void Delete(string fielddName, string fieldValue, string filePath);
  }
}
