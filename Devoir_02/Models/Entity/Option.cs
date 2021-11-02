using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Devoir_02.Models.Entity
{
    public class Option
    {
        [Key]
        public int OptionID { get; set; }
        public string Text { get; set; }
        public int IsRight { get; set; }
        public int QuestionID { get; set; }
        [JsonIgnore]
        [ForeignKey("QuestionID")]
        public virtual Question Question { get; set; }
        [JsonIgnore]
        public virtual List<Answer> Answers { get; set; }
    }
}
