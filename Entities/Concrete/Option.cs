﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
   public class Option : BaseEntity
    {
        [Key]
        public int OptionId { get; set; }
        public string OptionName { get; set; }
        public string OptionContent { get; set; }
        public bool CorrectOption { get; set; }

        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
        
    }
}
