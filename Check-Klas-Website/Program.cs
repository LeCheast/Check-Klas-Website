using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;

namespace Check_Klas_Website
{
    class Program
    {
        static void Main(string[] args)
        {
            var issueList = new List<string>();
            IWebDriver driver = new ChromeDriver();
            while (issueList.Count < 1)
            {
                CheckURL(driver, "https://klasresearch.com", "Home Page", issueList);
                CheckURL(driver, "https://klasresearch.com/reports", "Report Page", issueList);
                if (issueList.Count < 1)
                {
                    //wait for a minute to run again
                    Thread.Sleep(60000);
                }
            }
            driver.Close();

            SendEmail(issueList);

            static void CheckURL(IWebDriver driver, string url, string pageName, List<string> issueList)
            {
                try
                {
                    driver.Url = url;
                }
                catch
                {
                    issueList.Add(pageName);
                }
            }
            static void SendEmail(List<string> issueList)
            {
                MailAddress to = new MailAddress("example@domain.com");
                MailAddress from = new MailAddress("example2@gmail.com");
                MailMessage mail = new MailMessage(from, to);
                mail.Subject = "WEBSITE IS DOWN";
                mail.Body = "The following pages were tried and failed: ";
                foreach (var item in issueList)
                {
                    mail.Body += $"{item} ";
                }
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new NetworkCredential("example@gmail.com", "password");
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }

            Console.ReadKey();
        }

    }
}
