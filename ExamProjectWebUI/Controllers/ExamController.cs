
using Business.Abstract;
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

        public ExamController(IExamService examService)
        {
            _examService = examService;
        }

        public IActionResult CreateExam()
        {
            var titleDto = _examService.GetDataFromWebSite();

            ViewBag.HeadingList = titleDto.HeadingList;//textboxa tüm değerleri yaz
            ViewBag.TextList = titleDto.TextList;
            return View();
          
        }

    }
}
