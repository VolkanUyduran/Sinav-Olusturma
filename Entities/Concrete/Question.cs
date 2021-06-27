using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
    public class Question : BaseEntity
    {
        [Key]
        public int QuestionId { get; set; }
        public int QuestionNumber { get; set; }
        public string QuestionContent { get; set; }

        public int ExamId { get; set; }
        public virtual Exam Exam { get; set; }

        public ICollection<Option> Options { get; set; }

    }
}
