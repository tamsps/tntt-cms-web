using System.Globalization;
using ThieuNhiTT.Web.DataAccess;
using ThieuNhiTT.Web.Models;

namespace ThieuNhiTT.Web.Services
{
	public class NewsService:INewsService
	{
		private readonly IRepository<News> _newsRepository;

		public NewsService(IRepository<News> newsRepository)
		{
			_newsRepository = newsRepository;
		}

		public void CreateNews(News news, string filePath)
		{
			_newsRepository.Add(news, filePath);
		}

		public void DeleteNews(string newsId, string filePath)
		{
			_newsRepository.Delete("NewsId", newsId, filePath);
		}

		public IEnumerable<News> GetAllNews(string filePath) => _newsRepository.GetAll(filePath);


		public News GetNewsById(string newsId, string filePath) => _newsRepository.GetById("NewsId",newsId, filePath);

		public IEnumerable<News> GetNewsHome(string filePath)
		{
			string[] formats = { "dd/MM/yyyy", "d/M/yyyy","d/MM/yyyy", "dd/M/yyyy" };
			var newsList = GetAllNews(filePath).Where(d=>d.IsShowMainHome || d.IsShowSmallHome).OrderByDescending(d=>
			{
				// Try parsing with multiple formats
				if (DateTime.TryParseExact(d.DatePublish, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
				{
					return parsedDate;
				}
				else
				{
					return DateTime.MinValue;
				}
			});
			return newsList;
		}

		public void UpdateNews(News news, string filePath)
		{
			_newsRepository.Update(news, filePath);
		}
	}
}
