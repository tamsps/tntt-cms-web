using ThieuNhiTT.Web.Models;

namespace ThieuNhiTT.Web.Services
{
	public interface INewsService
	{
		IEnumerable<News> GetAllNews(string filePath);
		IEnumerable<News> GetNewsHome(string filePath);
		News GetNewsById(string newsId, string filePath);
		void CreateNews(News news, string filePath);
		void UpdateNews(News news, string filePath);
		void DeleteNews(string newsId, string filePath);

	}
}
