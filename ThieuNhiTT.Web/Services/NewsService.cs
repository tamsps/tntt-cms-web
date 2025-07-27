using System.Globalization;
using ThieuNhiTT.Web.DataAccess;
using ThieuNhiTT.Web.Models;

namespace ThieuNhiTT.Web.Services
{
	public class NewsService:INewsService
	{
		private readonly IRepository<News> _newsRepository;
		private readonly ILogger<NewsService> _logger;

		public NewsService(IRepository<News> newsRepository, ILogger<NewsService> logger)
		{
			_newsRepository = newsRepository;
			_logger = logger;
		}

		public void CreateNews(News news, string filePath)
		{
			_logger.LogInformation("Creating new news {NewsId}: {NewsTitle}", news.NewsId, news.NewsTitle);
			_newsRepository.Add(news, filePath);
			_logger.LogInformation("Successfully created news {NewsId}", news.NewsId);
		}

		public void DeleteNews(string newsId, string filePath)
		{
			_logger.LogInformation("Deleting news {NewsId}", newsId);
			_newsRepository.Delete("NewsId", newsId, filePath);
			_logger.LogInformation("Successfully deleted news {NewsId}", newsId);
		}

		public IEnumerable<News> GetAllNews(string filePath)
		{
			_logger.LogDebug("Getting all news from {FilePath}", filePath);
			var news = _newsRepository.GetAll(filePath);
			_logger.LogInformation("Retrieved {NewsCount} news items", news.Count());
			return news;
		}

		public News GetNewsById(string newsId, string filePath)
		{
			_logger.LogDebug("Getting news with ID {NewsId} from {FilePath}", newsId, filePath);
			var news = _newsRepository.GetById("NewsId", newsId, filePath);
			if (news != null)
			{
				_logger.LogInformation("Found news {NewsId}: {NewsTitle}", newsId, news.NewsTitle);
			}
			else
			{
				_logger.LogWarning("News not found with ID {NewsId}", newsId);
			}
			return news;
		}

		public IEnumerable<News> GetNewsHome(string filePath)
		{
			_logger.LogDebug("Getting home page news from {FilePath}", filePath);
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
			_logger.LogInformation("Retrieved {HomeNewsCount} news items for home page", newsList.Count());
			return newsList;
		}

		public void UpdateNews(News news, string filePath)
		{
			_logger.LogInformation("Updating news {NewsId}: {NewsTitle}", news.NewsId, news.NewsTitle);
			_newsRepository.Update(news, filePath);
			_logger.LogInformation("Successfully updated news {NewsId}", news.NewsId);
		}
	}
}
