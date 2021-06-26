using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
   public class CorrectAnswer
    {
        [Key]
        public int CorrectId { get; set; }
        public string Correct { get; set; }

        public int OptionId { get; set; }
        public virtual Option Option { get; set; }
    }
}
