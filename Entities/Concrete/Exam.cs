using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
    public class Exam : BaseEntity
    {
        [Key]
        public int ExamId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
