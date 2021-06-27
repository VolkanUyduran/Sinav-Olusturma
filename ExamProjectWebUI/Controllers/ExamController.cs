
using Business.Abstract;
using Entities.Concrete;
using Entities.Dto.Exam;
using ExamProjectWebUI.Models;
using HtmlAgilityPack;
using Mapster;
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

        public IActionResult ExamList()
        {
            var examList = _examService.GetAll();
            return View(examList);
        }


        //Sınav Silme
        [HttpPost]
        public JsonResult DeleteExam(int id)
        {
            if (id != 0)
            {
                var deleteExam = _examService.Delete(id);
                if (deleteExam != null)
                {
                    return Json(true);
                }
            }

            return Json(false);

        }

        public IActionResult ExamEntrance(int id)
        {

            if (id != 0)
            {
                var model = _examService.GetById(id);

                return View(model);
            }
            else
            {
                var exList = _examService.GetAll();
                return RedirectToAction("ExamList", exList);
            }
        }


        //Sınavı tamamlama post action'ı
        [HttpPost]
        public JsonResult EnterExam(int examId, string[] correctAnswers)
        {
            if (examId != 0 && correctAnswers != null)
            {
                var completeExam = _examService.CompletedExam(examId, correctAnswers);

                return Json(new { success = completeExam });
            }
            else
            {
                return Json(new { });
            }
        }
    }
}
