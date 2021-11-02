using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Devoir_02.Models.Entity
{
    public class Question
    {
        [Key]
        public int QuestionID { get; set; }
        public string Text { get; set; }

        public int Weight { get; set; }

        public string Type { get; set; }

        public int CategoryID { get; set; }

        [JsonIgnore]
        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }
        [JsonIgnore]
        public virtual List<Option> Options { get; set; }
        
        [JsonIgnore]
        public virtual List<QuestionQuiz> QuestionQuizzes { get; set; }
    }
}
