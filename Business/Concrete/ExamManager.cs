using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto.Exam;
using HtmlAgilityPack;
using Mapster;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Business.Concrete
{
    public class ExamManager : IExamService
    {
        private readonly IExamDal _examDal;
        private readonly IQuestionDal _questionDal;
        private readonly IOptionDal _optionDal;

        public ExamManager(IExamDal examDal, IQuestionDal questionDal, IOptionDal optionDal)
        {
            _examDal = examDal;
            _questionDal = questionDal;
            _optionDal = optionDal;
        }

        public void Create(Exam entity)
        {
            _examDal.Insert(entity);
        }

        public Exam Delete(int id)
        {
            var findExam = _examDal.Get(x => x.ExamId == id);
            if (findExam != null)
            {
                findExam.IsDeleted = true;
                _examDal.Update(findExam);

                var findQuestionList = _questionDal.List(x => !x.IsDeleted && x.ExamId == id);
                foreach (var question in findQuestionList)
                {
                    question.IsDeleted = true;
                    _questionDal.Update(question);

                    var findOptionList = _optionDal.List(x => x.QuestionId == question.QuestionId);
                    foreach (var op in findOptionList)
                    {

                        op.IsDeleted = true;
                        _optionDal.Update(op);          
                    }

                }

                return findExam;
            }


            return null;



        }

        public List<Exam> GetAll()
        {
            return _examDal.List();
        }

        public ExamDto GetById(int id)
        {

            ExamDto modal = new ExamDto();
            var findExam = _examDal.Get(x=>x.ExamId==id);
            modal.ExamId = findExam.ExamId;
            modal.Title = findExam.Title;
            modal.Content = findExam.Content;

            var findQuestionList = _questionDal.List(x => x.ExamId == id);
            var resQuestion = findQuestionList.Adapt<List<QuestionDto>>();
            modal.Questions = resQuestion;
            List<Option> opList = new List<Option>();


            foreach (var question in findQuestionList)
            {
                int QuestionId = question.QuestionId;
                var findOptionList = _optionDal.List(x => x.QuestionId == question.QuestionId);

                foreach (var option in findOptionList)
                {
                    opList.Add(option);
                }
            }

            var resOption = opList.Adapt<List<OptionDto>>();
            modal.Options = resOption;

            return modal;
        }

        public void Update(Exam entity)
        {
            _examDal.Update(entity);
        }

        public TitleDto GetDataFromWebSite()
        {

            int counter = 0;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string link = "http://wired.com/most-recent/";
            Uri url = new Uri(link);
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string html = client.DownloadString(url);
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(html);

            var secilenhtml = @"//*[@id=""app-root""]/div/div[3]/div/div[2]/div/div[1]/div/div/ul"; // veriyi çekeceğimiz div alanı kategori listesi xpath adresi

            var titleDto = new TitleDto();

            var secilenHtmlList = document.DocumentNode.SelectNodes(secilenhtml); //selectnodes methoduyla verdiğimiz xpathin htmlini getiriyoruz.

            if (secilenHtmlList != null)
            {
                foreach (var items in secilenHtmlList)
                {
                    foreach (var innerItem in items.SelectNodes("li"))//her ul un içindeki li de dön
                    {
                        try
                        {
                            titleDto.TextList.Add(getData(innerItem.SelectNodes("a")[0].Attributes["href"].Value));
                            counter++;
                        }
                        catch (Exception)
                        {
                            continue;
                        }

                        titleDto.HeadingList.Add(innerItem.SelectNodes("div//a//h2")[0].InnerHtml);
                        if (counter >= 5)
                            break;
                    }
                }
            }

            return titleDto;

        }

        private string getData(string eklink)
        {
            string link = "http://wired.com" + eklink;  //link değişkenine çekeceğimiz web sayafasının linkini yazıyoruz.
            Uri url = new Uri(link); //Uri tipinde değişeken linkimizi veriyoruz.
            WebClient client = new WebClient(); // webclient nesnesini kullanıyoruz bağlanmak için.
            client.Encoding = Encoding.UTF8; //türkçe karakter sorunu yapmaması için encoding utf8 yapıyoruz.
            string html = client.DownloadString(url); // siteye bağlanıp tüm sayfanın html içeriğini çekiyoruz.
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument(); //kütüphanemizi kullanıp htmldocument oluşturuyoruz.
            document.LoadHtml(html);//documunt değişkeninin html ine çektiğimiz htmli veriyoruz

            // var secilenhtml = @"//*[@id=""app-root""]/div/div[3]/div/div[3]/div[1]/div[2]/main/article/div[1]"; // veriyi çekeceğimiz div alanı kategori listesi xpath adresi
            StringBuilder stringBuilder = new StringBuilder();
            HtmlNodeCollection secilenHtmlList = document.DocumentNode.SelectNodes("/html/body/div[1]/div/main/article/div[2]/div/div[1]/div[1]/div[1]"); //selectnodes methoduyla verdiğimiz xpathin htmlini getiriyoruz.

            if (secilenHtmlList != null)
            {
                foreach (var items in secilenHtmlList)
                {
                    if (items.SelectNodes("p") != null)
                    {
                        foreach (var innerItem in items.SelectNodes("p"))//her ul un içindeki li de dön
                        {
                            if (innerItem != null && innerItem.InnerHtml != null)
                            {
                                stringBuilder.Append(innerItem.InnerHtml); // gelen değeri 
                            }


                        }
                    }

                }
            }

            return stringBuilder.ToString();
        }

        public List<string> CompletedExam(int examId, string[] correctAnswers)
        {

            var findExam = _examDal.Get(x=>x.ExamId==examId);
            var findQuestionList = _questionDal.List(x => x.ExamId == examId);
            int c = 0;
            List<string> bgList = new List<string>();

            foreach (var question in findQuestionList)
            {
                var findOptionList = _optionDal.List(x => x.QuestionId == question.QuestionId);
                foreach (var option in findOptionList)
                {
                    if (option.OptionName == correctAnswers[c] && option.CorrectOption)
                    {
                        bgList.Add("green");
                        //Gönderilen cevapla veri tabanındaki cevap dogru(karsılaştırma)
                        break;

                    }
                    else if(option.OptionName == correctAnswers[c] && !option.CorrectOption)
                    {
                        bgList.Add("red");
                        //yanlissa
                       

                    }
                }
                c++;
            }

            return bgList;
        }

    }
}
