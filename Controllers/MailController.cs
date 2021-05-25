using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Quartz;
using Quartz.Impl;

namespace Abc.MvcWebUI.Controllers
{
    //    public class EmailJob : IJob
    //    {
    //        private string _base = "taahagokce@gmail.com";
    //        public string _receiver;

    //        public async Task Execute(IJobExecutionContext context)
    //        {
    //            using (var message = new MailMessage("from(lolfakebarzo@gmail.com)", " to(taahagokce@gmail.com)"))
    //            {
    //                message.From = new MailAddress("lolfakebarzo@gmail.com");
    //                message.Subject = "Mail başlık";
    //                message.Body = "Mail içerik kısmı";
    //                using (SmtpClient client = new SmtpClient

    //                {
    //                    EnableSsl = true,
    //                    Host = "smtp.gmail.com",
    //                    Port = 587,
    //                    Credentials = new NetworkCredential("Deneme Deneme", "")
    //                })
    //                {
    //                    client.Send(message);
    //                }
    //            }
    //        }

    //}
    //        public class CampaignPushJobScheduler
    //        {
    //            public static void Start()
    //            {
    //                var scheduler = StdSchedulerFactory.GetDefaultScheduler();
    //                scheduler.Start();

    //                IJobDetail job = JobBuilder.Create<EmailJob>().Build();

    //                ITrigger trigger = TriggerBuilder.Create()
    //                    .WithDailyTimeIntervalSchedule
    //                    (s =>
    //                            s
    //                                .OnEveryDay() //hergün çalışacağı bilgisi
    //                                .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(13,
    //                                    30)) //hergün hangi saatte çalışacağı bilgisi
    //                    )
    //                    .Build();

    //                scheduler.ScheduleJob(job, trigger);
    //            }
    //        }


    //public class Mail
    //{
    //    public void sent(string sender,string password,string displayName ,string receiver, string subject,string body)
    //    {


    //        MailMessage msg = new MailMessage();

    //        msg.Subject = subject; //"konu başlığı";
    //        msg.From = new MailAddress(sender,displayName);
    //        msg.To.Add(receiver);//"AlıcıAdı            msg.IsBodyHtml = true;//html görüntüleri varmı yollancak mesajda ona göre bu seçenek aktif edilir
    //        msg.Body = body;  // "mesajıb buray yazarız html olarakta atabiliriz";


    //        //SMTP/Gönderici bilgilerinin yer aldığı erişim/doğrulama bilgileri
    //        SmtpClient smtp = new SmtpClient("smtp.siteadi.com", 587); //Bu alanda gönderim yapacak hizmetin smtp adresini ve size verilen portu girmelisiniz.
    //        NetworkCredential AccountInfo = new NetworkCredential(sender, password);
    //        smtp.UseDefaultCredentials = false; //Standart doğrulama kullanılsın mı? -> Yalnızca gönderici özellikle istiyor ise TRUE işaretlenir.
    //        smtp.Credentials = AccountInfo;
    //        smtp.EnableSsl = false; //SSL kullanılarak mı gönderilsin...
    //        smtp.EnableSsl = true;


    //        try
    //        {
    //            smtp.Send(msg);

    //        }
    //        catch (Exception e)
    //        {
    //         var error =  string.Format("gönderme hatası : {0} ", e.Message);
    //            Console.WriteLine(e);
    //            throw;

    //        }




    //    }
    //}




    public class MailController : Controller
    {
        // GET: Mail
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string konu,string mesaj,string eposta)
        {
          
            Mail mail = new Mail();
            mail.Gonder(konu, mesaj, eposta);
            return View();


        }


        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


    }






    public class Mail
    {
        public void Gonder(string konu, string mesaj, string eposta)
        {
           
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new NetworkCredential("lolfakebarzo@gmail.com", "taha1919gokce"); // Gönderici bilgilerini giriyoruz
                smtp.Port = 587; // Mail uzantınıza göre bu değişebilir
                smtp.Host = "smtp.gmail.com"; // Gmail veya hotmail ise onların host bilgisini almanız gerekiyor 
                smtp.EnableSsl = true;
                mail.IsBodyHtml = true;// HTML tagleri kullanarak mail gönderebilmek için aktif ediyoruz
                mail.From = new MailAddress("lolfakebarzo@gmail.com"); // Gönderen mail adresini yazıyoruz
                mail.To.Add(eposta); // Gönderilecek mail adresini ekliyoruz
                mail.Subject = konu; // Mesaja konuyu ekliyoruz
                mail.Body = mesaj; // Mesajın içeriğini ekliyoruz

                smtp.Send(mail); // Mesajı gönderiyoruz
                
            }
            catch (Exception x)
            {
                var error = string.Format("gönderme hatası : {0} ", x.Message);
            }
        }
    }
}


