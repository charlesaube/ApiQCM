using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Devoir_02.Models.Entity
{
    public class QuestionQuiz
    {
       
        public int QuestionID { get; set; }
        [JsonIgnore]

        [ForeignKey("QuestionID")]
        public virtual Question Question { get; set; }

        public int QuizID { get; set; }
        [JsonIgnore]

        [ForeignKey("QuizID")]
        public virtual Quiz Quiz { get; set; }
    }
}
