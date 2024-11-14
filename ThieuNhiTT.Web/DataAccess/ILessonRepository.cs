using ThieuNhiTT.Web.Models;

namespace ThieuNhiTT.Web.DataAccess
{
	public interface ILessonRepository
	{
		List<Lesson> GetAllLessons();
		Lesson GetLessonById(string bookId, string lessonId);
		List<Question> GetQuestionsByLessonId(string bookId, string lessonId, int lessonIndex);
		List<Lesson> GetAllLessonByBookId(string bookId);
		void AddLesson(Lesson newLesson);
		void UpdateLesson(Lesson updatedLesson);
		void DeleteLesson(string bookId, string lessonId);
	}
}
