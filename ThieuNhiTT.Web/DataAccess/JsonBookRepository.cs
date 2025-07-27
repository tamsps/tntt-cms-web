namespace ThieuNhiTT.Web.DataAccess
{
		using System.Collections.Generic;
		using System.Linq;
  using System.Text.Json;
  using ThieuNhiTT.Web.Models;

		public class JsonBookRepository<T> : IRepository<T>
    {
    private readonly ILogger<JsonBookRepository<T>> _logger;

    public JsonBookRepository(ILogger<JsonBookRepository<T>> logger)
    {
      _logger = logger;
    }
    public IEnumerable<T> GetAll(string filePath)
    {
      _logger.LogDebug("Reading all items from file {FilePath}", filePath);
      try
      {
        var json = File.ReadAllText(filePath);
        var items = JsonSerializer.Deserialize<IEnumerable<T>>(json);
        _logger.LogDebug("Successfully loaded {ItemCount} items from {FilePath}", items?.Count() ?? 0, filePath);
        return items;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Failed to read items from file {FilePath}", filePath);
        throw;
      }
    }

    public T GetById(string fielddName, string fieldValue,  string filePath)
    {
      _logger.LogDebug("Getting item by {FieldName}={FieldValue} from {FilePath}", fielddName, fieldValue, filePath);
      try
      {
        var json = File.ReadAllText(filePath);
        var items = JsonSerializer.Deserialize<IEnumerable<T>>(json);
        var item = items.FirstOrDefault(item => Convert.ToInt16(item.GetType().GetProperty(fielddName)?.GetValue(item)?.ToString()) == Convert.ToInt32(fieldValue));
        
        if (item != null)
        {
          _logger.LogDebug("Found item with {FieldName}={FieldValue}", fielddName, fieldValue);
        }
        else
        {
          _logger.LogDebug("No item found with {FieldName}={FieldValue}", fielddName, fieldValue);
        }
        
        return item;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Failed to get item by {FieldName}={FieldValue} from {FilePath}", fielddName, fieldValue, filePath);
        throw;
      }
    }

    public void Add(T item, string filePath)
    {
      _logger.LogDebug("Adding new item to {FilePath}", filePath);
      try
      {
        var items = GetAll(filePath).ToList();
        items.Add(item);
        Save(items, filePath);
        _logger.LogInformation("Successfully added item to {FilePath}", filePath);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Failed to add item to {FilePath}", filePath);
        throw;
      }
    }

    public void Update(T item, string filePath)
    {
      _logger.LogDebug("Updating item in {FilePath}", filePath);
      try
      {
        var items = GetAll(filePath).ToList();
        var existingItemIndex = items.FindIndex(i => i.Equals(item));
        if (existingItemIndex >= 0)
        {
          items[existingItemIndex] = item;
          Save(items, filePath);
          _logger.LogInformation("Successfully updated item in {FilePath}", filePath);
        }
        else
        {
          _logger.LogWarning("Item not found for update in {FilePath}", filePath);
        }
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Failed to update item in {FilePath}", filePath);
        throw;
      }
    }

    public void Delete(string fielddName, string fieldValue, string filePath)
    {
      _logger.LogDebug("Deleting item with {FieldName}={FieldValue} from {FilePath}", fielddName, fieldValue, filePath);
      try
      {
        var items = GetAll(filePath).ToList();
        var itemToRemove = items.FirstOrDefault(i => Convert.ToInt16(i.GetType().GetProperty(fielddName)?.GetValue(i)?.ToString()) == Convert.ToInt32(fieldValue));
        if (itemToRemove != null)
        {
          items.Remove(itemToRemove);
          Save(items, filePath);
          _logger.LogInformation("Successfully deleted item with {FieldName}={FieldValue} from {FilePath}", fielddName, fieldValue, filePath);
        }
        else
        {
          _logger.LogWarning("Item not found for deletion with {FieldName}={FieldValue} in {FilePath}", fielddName, fieldValue, filePath);
        }
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Failed to delete item with {FieldName}={FieldValue} from {FilePath}", fielddName, fieldValue, filePath);
        throw;
      }
    }

    private void Save(IEnumerable<T> items, string filePath)
    {
      _logger.LogDebug("Saving {ItemCount} items to {FilePath}", items.Count(), filePath);
      try
      {
        var json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
        _logger.LogDebug("Successfully saved {ItemCount} items to {FilePath}", items.Count(), filePath);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Failed to save items to {FilePath}", filePath);
        throw;
      }
    }
  }

}
