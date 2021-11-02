using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Devoir_02.Models.Entity
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public virtual List<Question> Questions { get; set; }
    }
}
