using Devoir_02.Models;
using Devoir_02.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devoir_02.Services
{
    public class QCMRepository : IQCMRepository
    {
        private QCMContext _dbcontext;
        public QCMRepository(QCMContext dbcontext)
        {
            this._dbcontext = dbcontext;
        }

        public void AddAnswer(Answer answer)
        {
            _dbcontext.Answers.Add(answer);
            _dbcontext.SaveChanges();
        }

        public void AddQuestionQuizzes(QuestionQuiz questionQuiz)
        {
            _dbcontext.QuestionQuizzes.Add(questionQuiz);
            _dbcontext.SaveChanges();
        }

        public int AddQuiz(Quiz quiz)
        {
            _dbcontext.Quizzes.Add(quiz);
            _dbcontext.SaveChanges();
            return quiz.QuizID;
        }

        public List<Answer> GetAnswersOfQuiz(int quizId)
        {
            return _dbcontext.Answers.Where(a => a.QuizID == quizId).Include(a=>a.Option).ThenInclude(x=>x.Question).ToList();
        }

        public int GetCategoryID(string desc)
        {
            return _dbcontext.Categories.Where(c => c.Description == desc).First().CategoryID;
        }

        public int GetMaxScoreOfQuiz(int quizId)
        {
            List<QuestionQuiz> questionQuizzes = _dbcontext.QuestionQuizzes.Where(q => q.QuizID == quizId).Include(q => q.Question).ToList();
            int maxScoreOfQuiz = 0;
            foreach (QuestionQuiz q in questionQuizzes)
            {
                maxScoreOfQuiz = maxScoreOfQuiz + q.Question.Weight;
            }
            return maxScoreOfQuiz;
        }

        public List<Question> GetNRandomQuestionsFromCategory(int categoryId, int n)
        {
            var questions =  _dbcontext.Questions.Where(q => q.CategoryID == categoryId).ToList();
            var rnd = new Random();
            return questions.OrderBy(x => rnd.Next()).Take(n).ToList();
        }

        public int GetNumberOfAnswerOfQuestioninQuiz(int quizId, int questionId)
        {
            return _dbcontext.Answers.Where(a => a.Option.QuestionID == questionId && a.QuizID == quizId).Count();
        }

        public int GetNumberOfRightOptionFromQuestion(int questionId)
        {
            return _dbcontext.Options.Where(o => o.QuestionID == questionId && o.IsRight == 1).Count();
        }

        public List<Option> GetOptionsOfQuestion(int questionId)
        {
            return _dbcontext.Options.Where(o => o.QuestionID == questionId).ToList();
        }

        public List<Question> GetQuestionsOfQuiz(int quizId)
        {
            List<QuestionQuiz> questionQuizzes = _dbcontext.QuestionQuizzes.Where(q => q.QuizID == quizId).Include(q=> q.Question).ToList();
            List<Question> questions = new List<Question>();
            foreach(QuestionQuiz q in questionQuizzes)
            {
                questions.Add(q.Question);
            }
            return questions;
        }

        public List<Quiz> GetQuizzes()
        {
            return _dbcontext.Quizzes.ToList();
        }

        public bool QuizExist(int quizId)
        {
            Quiz quiz = _dbcontext.Quizzes.Where(q => q.QuizID == quizId).FirstOrDefault();
            if(quiz == null)
            {
                return false;
            }
            return true;
        }
    }
}
