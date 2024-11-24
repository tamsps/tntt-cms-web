using System.Net;
using System.Text.Json;
using System.Linq;
using ThieuNhiTT.Web.Models;

namespace ThieuNhiTT.Web.DataAccess
{
  public class JsonRepository<T> : IRepository<T>
  {
    public IEnumerable<T> GetAll(string filePath)
    {
      var json = File.ReadAllText(filePath);
      return JsonSerializer.Deserialize<IEnumerable<T>>(json);
    }

    public T GetById(string id, string filePath)
    {
      var json = File.ReadAllText(filePath);
      var items = JsonSerializer.Deserialize<IEnumerable<T>>(json);
      return items.FirstOrDefault(item => item.GetType().GetProperty("BookId")?.GetValue(item)?.ToString() == id);
    }

    public void Add(T item, string filePath)
    {
      var items = GetAll(filePath).ToList();
      items.Add(item);
      Save(items, filePath);
    }

    public void Update(T item, string filePath)
    {
      var items = GetAll(filePath).ToList();
      var existingItemIndex = items.FindIndex(i => i.Equals(item));
      if (existingItemIndex >= 0)
      {
        items[existingItemIndex] = item;
        Save(items, filePath);
      }
    }

    public void Delete(string id, string filePath)
    {
      var items = GetAll(filePath).ToList();
      var itemToRemove = items.FirstOrDefault(i => i.GetType().GetProperty("BookId")?.GetValue(i)?.ToString() == id);
      if (itemToRemove != null)
      {
        items.Remove(itemToRemove);
        Save(items, filePath);
      }
    }

    private void Save(IEnumerable<T> items, string filePath)
    {
      var json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
      File.WriteAllText(filePath, json);
    }
  }
}

