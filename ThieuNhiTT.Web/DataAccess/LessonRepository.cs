using System.Net;
using ThieuNhiTT.Web.Models;

namespace ThieuNhiTT.Web.DataAccess
{
	public class LessonRepository : ILessonRepository
	{
		private readonly JsonContext _context;

		public LessonRepository(string jsonFilePath, string lessonFilePath)
		{
			_context = new JsonContext(jsonFilePath, lessonFilePath);
		}

		public void AddLesson(Lesson newLesson)
		{
			_context.Lessons.Add(newLesson);
			_context.SaveChanges();
		}

		public void DeleteLesson(string bookId, string lessonId)
		{
			var lessonToDelete = GetLessonById(bookId ,lessonId);
			if (lessonToDelete != null)
			{
				_context.Lessons.Remove(lessonToDelete);
				_context.SaveChanges();
			}
		}

		public List<Lesson> GetAllLessons()
		{
			 return _context.Lessons;
		}

		
		public Lesson GetLessonById(string bookId, string lessonId)
		{
			  return _context.Lessons.FirstOrDefault(lesson => lesson.LessonId == lessonId && lesson.BookId.Equals(bookId));
		}

		public List<Question> GetQuestionsByLessonId(string bookId, string lessonId, int lessonIndex)
		{
			var lesson = GetLessonById(bookId, lessonId);
			return lesson != null && lesson.Questions.Count > 0
					? lesson.Questions
					: new List<Question>();
		}

		public void UpdateLesson(Lesson updatedLesson)
		{
			var existingLesson = GetLessonById(updatedLesson.BookId, updatedLesson.LessonId);
			if (existingLesson != null)
			{
				existingLesson.LessonTitle = updatedLesson.LessonTitle;
				existingLesson.MainContent = updatedLesson.MainContent;
				existingLesson.Questions = updatedLesson.Questions;
				existingLesson.MemoContent = updatedLesson.MemoContent;
				existingLesson.Actions = updatedLesson.Actions;
				_context.SaveChanges();
			}
		}
		 public List<Lesson> GetAllLessonByBookId(string bookId)
		{
		   	return _context.Lessons.FindAll(lesson => lesson.BookId.Equals(bookId));
		}
	}
}

