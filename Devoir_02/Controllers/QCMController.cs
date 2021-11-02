using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Devoir_02.Models.Entity;
using Devoir_02.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Devoir_02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QCMController : ControllerBase
    {
        private IQCMRepository _qcmRepository;

        public QCMController(IQCMRepository quizzesRepository)
        {
            _qcmRepository = quizzesRepository;
        }

        [HttpGet("question/{id}")]
        public IActionResult GetOptionsFromQuestion(int id)
        {
            List<Option> options = _qcmRepository.GetOptionsOfQuestion(id);
            if(options.Count <= 0)
            {
                return NotFound("Invalid QuestionID(no options found)");
            }
            return Ok(options);
        }


        [HttpGet("quiz/{id}")]
        public IActionResult GetQuestionsFromQuiz(int id)
        {
            List<Question> questions = _qcmRepository.GetQuestionsOfQuiz(id);
            if (questions.Count <= 0)
            {
                return NotFound("Invalid QuizID(no questions found)");
            }
            return Ok(questions);
        }

        [HttpGet("quiz")]
        public IActionResult GetFinalScoreFromAllQuizzes()
        {
            //Dictionary<int, int> scores = new Dictionary<int, int>(); key = QuizID , value = score
            List<QuizScore> scores = new List<QuizScore>();
            double currentScore;
            foreach(Quiz q in _qcmRepository.GetQuizzes())//parcours de chaque quiz
            {
                Dictionary<int, int> checkboxesScore = new Dictionary<int, int>(); //key = QuestionID , value = number of right answer
                Dictionary<int, int> numberOfCheckboxAnswerVisited = new Dictionary<int, int>(); //key = QuestionID , value = number of answer visited
                currentScore = 0;
                foreach(Answer a in _qcmRepository.GetAnswersOfQuiz(q.QuizID))//parcours de chaque réponses d'un quiz
                {
                    if(a.Option.Question.Type == "multiplechoice" && a.Option.IsRight == 1)//si question choix multiple
                    {
                        currentScore= currentScore + a.Option.Question.Weight;
                    }
                    else if(a.Option.Question.Type == "checkboxes")//si question avec plusieurs réponses possibles, alors on regarde s'il a tout les bonnes réponses
                    {
                        //Incrémentation du compteur de visite des réponses d'une question
                        if (numberOfCheckboxAnswerVisited.ContainsKey(a.Option.QuestionID))
                        {
                            numberOfCheckboxAnswerVisited[a.Option.QuestionID] = numberOfCheckboxAnswerVisited[a.Option.QuestionID] + 1;
                        }
                        else//Initialisation du compteur de visite des réponses d'une question + le compteur du score pour cette question
                        {
                            numberOfCheckboxAnswerVisited.Add(a.Option.QuestionID, 1);
                            checkboxesScore.Add(a.Option.QuestionID, 0);
                        }
                        if (a.Option.IsRight == 1)//Si la réponse est correct
                        {
                            if (checkboxesScore[a.Option.QuestionID] != -1)//Vérifie qu'il n'a pas donné une mauvais réponse                        
                                checkboxesScore[a.Option.QuestionID] = checkboxesScore[a.Option.QuestionID] + 1; 
                        }

                        if(a.Option.IsRight == 0)//Si la réponse est incorrect alors il ne peut pas avoir de point pour la question                                                   
                            checkboxesScore[a.Option.QuestionID] = -1;

                        if(numberOfCheckboxAnswerVisited[a.Option.QuestionID] == _qcmRepository.GetNumberOfAnswerOfQuestioninQuiz(q.QuizID, a.Option.QuestionID))
                        {                      
                            if(checkboxesScore[a.Option.QuestionID] == _qcmRepository.GetNumberOfRightOptionFromQuestion(a.Option.QuestionID))
                                currentScore += a.Option.Question.Weight;
                        }
                    }
                }
                scores.Add(new QuizScore { QuizID = q.QuizID, Score = currentScore  / _qcmRepository.GetMaxScoreOfQuiz(q.QuizID) * 100} );
            }
            return Ok(scores);
        }

        [HttpPost("quiz")]
        public IActionResult CreateNewQuiz([FromBody]List<QuizRequestData> request)
        {
            bool isValid = true;
            foreach(QuizRequestData qr in request)// Vérifie si la requête est valide ou non
            {
                if(qr.Category == null || qr.NumberOfQuestions == 0)
                {
                    isValid = false;
                    break;
                }
            }

            if (isValid)
            {          
                int newQuizId = _qcmRepository.AddQuiz(new Quiz());
                foreach(QuizRequestData qr in request)
                {
                    int categoryId = _qcmRepository.GetCategoryID(qr.Category);
                    List<Question> randomQuestions = _qcmRepository.GetNRandomQuestionsFromCategory(categoryId, qr.NumberOfQuestions);

                    foreach (Question q in randomQuestions)
                    {
                        _qcmRepository.AddQuestionQuizzes(new QuestionQuiz { QuestionID = q.QuestionID, QuizID = newQuizId });
                    }
                }
                return Ok(newQuizId);
            }
            return BadRequest("Problem with the body of the request");
        }

        [HttpPost("quiz/{quizId}/answers")]
        public IActionResult SubmitAnswer([FromBody] List<int> answers, int quizId)
        {
            if (_qcmRepository.QuizExist(quizId))
            {
                if (answers.Count >= 1)
                {
                    foreach (int optionId in answers)
                    {
                        _qcmRepository.AddAnswer(new Answer { OptionID = optionId, QuizID = quizId });
                    }
                    return Ok("Answer(s) successfully submitted !");
                }
                return BadRequest("Body of the request is empty or invalid");
            }

            return BadRequest("Invalid QuizID(no quiz found)");
        }


    }
    public class QuizScore
    {
        public int QuizID { get; set; }
        public double Score { get; set; }
    }

    public class QuizRequestData
    {
        public string Category { get; set; }
        public int NumberOfQuestions { get; set; }
    }
}
