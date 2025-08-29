using ThieuNhiTT.Web.DataAccess;
using ThieuNhiTT.Web.Models;

namespace ThieuNhiTT.Web.Services
{
  public class LessonService : ILessonService
  {
    private readonly IRepository<Lesson> _lessonRepository;
    private readonly ILogger<LessonService> _logger;

    public LessonService(IRepository<Lesson> lessonRepository, ILogger<LessonService> logger)
    {
      _lessonRepository = lessonRepository;
      _logger = logger;
    }

    public IEnumerable<Lesson> GetAllLessonsByBookId(string bookId, string filePath)
    {
      if (string.IsNullOrEmpty(filePath))
      {
				filePath = $"Data\\{bookId}.json";
			}
      
      _logger.LogDebug("Getting lessons for book {BookId} from {FilePath}", bookId, filePath);
      var lessons = _lessonRepository.GetAll(filePath);
      var filteredLessons = lessons.Where(l => l.BookId == bookId);
      _logger.LogInformation("Retrieved {LessonCount} lessons for book {BookId}", filteredLessons.Count(), bookId);
      return filteredLessons;
    }

    public Lesson GetLessonById(string lessonId, string filePath)
    {
      _logger.LogDebug("Getting lesson with ID {LessonId} from {FilePath}", lessonId, filePath);
      var lesson = _lessonRepository.GetById("LessonId", lessonId, filePath);
      if (lesson != null)
      {
        _logger.LogInformation("Found lesson {LessonId}: {LessonTitle}", lessonId, lesson.LessonTitle);
      }
      else
      {
        _logger.LogWarning("Lesson not found with ID {LessonId}", lessonId);
      }
      return lesson;
    }

    public void CreateLesson(Lesson lesson, string filePath)
    {
      _logger.LogInformation("Creating new lesson {LessonId}: {LessonTitle}", lesson.LessonId, lesson.LessonTitle);
      _lessonRepository.Add(lesson, filePath);
      _logger.LogInformation("Successfully created lesson {LessonId}", lesson.LessonId);
    }

    public void UpdateLesson(Lesson lesson, string filePath)
    {
      _logger.LogInformation("Updating lesson {LessonId}: {LessonTitle}", lesson.LessonId, lesson.LessonTitle);
      _lessonRepository.Update(lesson, filePath);
      _logger.LogInformation("Successfully updated lesson {LessonId}", lesson.LessonId);
    }

    public void DeleteLesson(string lessonId, string filePath)
    {
      _logger.LogInformation("Deleting lesson {LessonId}", lessonId);
      _lessonRepository.Delete("LessonId", lessonId, filePath);
      _logger.LogInformation("Successfully deleted lesson {LessonId}", lessonId);
    }
  }
}
