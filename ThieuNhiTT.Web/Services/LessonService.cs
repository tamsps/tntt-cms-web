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
      var lessons = _lessonRepository.GetAll(filePath);
      return lessons.Where(l => l.BookId == bookId);
    }

    public Lesson GetLessonById(string lessonId, string filePath)
    {
      return _lessonRepository.GetById(lessonId, filePath);
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
      _lessonRepository.Delete(lessonId, filePath);
    }
  }
}
