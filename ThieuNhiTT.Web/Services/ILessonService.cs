using ThieuNhiTT.Web.Models;

namespace ThieuNhiTT.Web.Services
{
  public interface ILessonService
  {
    IEnumerable<Lesson> GetAllLessonsByBookId(string bookId, string filePath);
    Lesson GetLessonById(string lessonId, string filePath);
    void CreateLesson(Lesson lesson, string filePath);
    void UpdateLesson(Lesson lesson, string filePath);
    void DeleteLesson(string lessonId, string filePath);
  }
}
