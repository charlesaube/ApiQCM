using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Devoir_02.Models.Entity
{
    public class Answer
    {
        [Key]
        public int AnswerID { get; set; }
        public int OptionID { get; set; }
        public int QuizID{ get; set; }
        [JsonIgnore]
        [ForeignKey("OptionID")]
        public virtual Option Option { get; set; }
        [JsonIgnore]
        [ForeignKey("QuizID")]
        public virtual Quiz Quiz { get; set; }
    }
}
