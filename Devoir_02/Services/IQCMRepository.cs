using Devoir_02.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Devoir_02.Services
{
    public interface IQCMRepository
    {
        List<Option> GetOptionsOfQuestion(int questionId);
        List<Question> GetQuestionsOfQuiz(int quizId);
        List<Quiz> GetQuizzes();
        bool QuizExist(int quizId);
        int GetMaxScoreOfQuiz(int quizId);
        List<Answer> GetAnswersOfQuiz(int quizId);
        int GetNumberOfAnswerOfQuestioninQuiz(int quizId, int questionId);
        int GetCategoryID(string desc);
        List<Question> GetNRandomQuestionsFromCategory(int categoryId, int n);
        int GetNumberOfRightOptionFromQuestion(int questionId);
        int AddQuiz(Quiz quiz);
        void AddAnswer(Answer answer);
        void AddQuestionQuizzes(QuestionQuiz questionQuiz);
    }
}
