using ThieuNhiTT.Web.DataAccess;
using ThieuNhiTT.Web.Models;

namespace ThieuNhiTT.Web.Services
{
	public class LessonService
	{
		private readonly ILessonRepository _lessonRepository;

		public LessonService(ILessonRepository lessonRepository)
		{
			_lessonRepository = lessonRepository;
		}

		public List<Lesson> GetAllLessons() => _lessonRepository.GetAllLessons();

		public Lesson GetLessonById(string bookId, string lessonId) => _lessonRepository.GetLessonById(bookId,lessonId);

		public List<Lesson> GetAllLessonsByBookId(string bookId) => _lessonRepository.GetAllLessonByBookId(bookId);

		public List<Question> GetQuestionsByLesson(string bookId, string lessonId, int lessonIndex) => _lessonRepository.GetQuestionsByLessonId (bookId, lessonId, lessonIndex);

		public void AddLesson(Lesson newLesson) => _lessonRepository.AddLesson(newLesson);

		public void UpdateLesson(Lesson updatedLesson) => _lessonRepository.UpdateLesson(updatedLesson);

		public void DeleteLesson(string bookId, string lessonId) => _lessonRepository.DeleteLesson(bookId, lessonId);
	}
}
