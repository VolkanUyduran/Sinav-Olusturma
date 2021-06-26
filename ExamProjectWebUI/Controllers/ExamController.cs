
using Business.Abstract;
using Entities.Concrete;
using Entities.Dto.Exam;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExamProjectWebUI.Controllers
{
    [Authorize]
    public class ExamController : Controller
    {
        private readonly IExamService _examService;
        private readonly IQuestionService _questionService;
        private readonly IOptionService _optionService;

        public ExamController(IExamService examService, IQuestionService questionService, IOptionService optionService)
        {
            _examService = examService;
            _questionService = questionService;
            _optionService = optionService;
        }

        public IActionResult CreateExam()
        {
            var titleDto = _examService.GetDataFromWebSite();

            ViewBag.HeadingList = titleDto.HeadingList;//textboxa tüm değerleri yaz
            ViewBag.TextList = titleDto.TextList;
            return View();
          
        }

        [HttpPost]
        public JsonResult CreateExam(string[] questiones, string[] choices, string[] correctAnswers, string header, string text)
        {

            if (questiones != null && choices != null && correctAnswers != null && header != null)
            {
                Exam ex = new Exam();
                ex.Title = header;
                ex.Content = text;
                _examService.Create(ex);
                var exID = ex.ExamId; //sınav id
                int a = 0;
                int b = 6;
                int k = 0;

                foreach (var item in questiones)
                {
                    Question q = new Question();
                    q.QuestionContent = item;
                    q.QuestionNumber = 1;
                    q.ExamId = exID; //soruya sınavId atılıyor
                    _questionService.Create(q);
                    var questionID = q.QuestionId; //soruId bir değişkende tutuluyor
                    for (int i = a; i <= b; i += 2)
                    {
                        Option op = new Option();
                        op.OptionContent = choices[i];
                        op.OptionName = choices[i + 1];
                        //op.CorrectOption = true;
                        op.QuestionId = questionID; //cevaba soruId atılıyor             
                        var opId = op.OptionId;


                        if (op.OptionName == correctAnswers[k])
                        {
                            op.CorrectOption = true;
                        }

                        //if (op.OptionName == correctAnswers[k])
                        //{
                        //    CorrectAnswer correct = new CorrectAnswer();
                        //    correct.Correct = correctAnswers[k];
                        //    correct.OptionId = opId;
                        //    _correctAnswer.Create(correct);
                        //}

                        _optionService.Create(op);
                    }
                    k++;
                    a += 8;
                    b += 8;

                }
                return Json(new { result = 1, message = "Succes." });

            }

            else
            {

                return Json(new { result = 0, message = "Failed." });
            }


        }


    }
}
