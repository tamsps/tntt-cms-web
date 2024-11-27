namespace ThieuNhiTT.Web.DataAccess
{
		using System.Collections.Generic;
		using System.Linq;
  using System.Text.Json;
  using ThieuNhiTT.Web.Models;

		public class JsonBookRepository<T> : IRepository<T>
    {
    public IEnumerable<T> GetAll(string filePath)
    {
      var json = File.ReadAllText(filePath);

      return JsonSerializer.Deserialize<IEnumerable<T>>(json);
    }

    public T GetById(string fielddName, string fieldValue,  string filePath)
    {
      var json = File.ReadAllText(filePath);
      var items = JsonSerializer.Deserialize<IEnumerable<T>>(json);
      return items.FirstOrDefault(item => Convert.ToInt16(item.GetType().GetProperty(fielddName)?.GetValue(item)?.ToString()) == Convert.ToInt32(fieldValue));
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

    public void Delete(string fielddName, string fieldValue, string filePath)
    {
      var items = GetAll(filePath).ToList();
      var itemToRemove = items.FirstOrDefault(i => Convert.ToInt16(i.GetType().GetProperty(fielddName)?.GetValue(i)?.ToString()) == Convert.ToInt32(fieldValue));
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
