using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dto.Exam
{
    public class ExamDto
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public List<QuestionDto> Questions { get; set; } = new List<QuestionDto>();

    }
}
