using ThieuNhiTT.Web.DataAccess;
using ThieuNhiTT.Web.Models;

namespace ThieuNhiTT.Web.Services
{
  public class LessonService : ILessonService
  {
    private readonly IRepository<Lesson> _lessonRepository;

    public LessonService(IRepository<Lesson> lessonRepository)
    {
      _lessonRepository = lessonRepository;
    }

    public IEnumerable<Lesson> GetAllLessonsByBookId(string bookId, string filePath)
    {
      if (string.IsNullOrEmpty(filePath))
      {
				filePath = $"Data\\{bookId}.json";
			}
      var lessons = _lessonRepository.GetAll(filePath);
      return lessons.Where(l => l.BookId == bookId);
    }

    public Lesson GetLessonById(string lessonId, string filePath)
    {
      return _lessonRepository.GetById("LessonId", lessonId, filePath);
    }

    public void CreateLesson(Lesson lesson, string filePath)
    {
      _lessonRepository.Add(lesson, filePath);
    }

    public void UpdateLesson(Lesson lesson, string filePath)
    {
      _lessonRepository.Update(lesson, filePath);
    }

    public void DeleteLesson(string lessonId, string filePath)
    {
      _lessonRepository.Delete("LessonId", lessonId, filePath);
    }
  }
}
