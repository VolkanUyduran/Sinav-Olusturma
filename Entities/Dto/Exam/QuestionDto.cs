using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dto.Exam
{
    public class QuestionDto
    {
        public string QuestionContent { get; set; }

        public int ExamId { get; set; }

        public List<OptionDto> Options { get; set; } = new List<OptionDto>();
    }
}
