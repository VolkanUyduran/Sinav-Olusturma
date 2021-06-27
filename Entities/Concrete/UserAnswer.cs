using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
    public class UserAnswer : BaseEntity
    {
        [Key]
        public int UserAnswerId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual  Option Option { get; set; }
        public int OptionId { get; set; }
        public bool CorrectOption { get; set; }

    }
}
