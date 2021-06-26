using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dto.Exam
{
    public class TitleDto
    {
        public List<string> HeadingList { get; set; } = new List<string>();
        public List<string> TextList { get; set; } = new List<string>();
    }
}
