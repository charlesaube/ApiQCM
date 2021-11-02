using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Devoir_02.Models.Entity
{
    public class Quiz
    {
        [Key]
        public int QuizID { get; set; }
        [JsonIgnore]
        public virtual List<Answer> Answers { get; set; }
        [JsonIgnore]
        public virtual List<QuestionQuiz> QuestionQuizzes { get; set; }
    }
}
