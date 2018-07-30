using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using System.Web.UI;

namespace AutomationDemo.TestUtilities
{
    public class Reporter
    {
        static string resultName;
        static string resultDirectory = Environment.CurrentDirectory;
        static string reportDirectory = "./TestReports";
        static string screenshotDirectory = "./ScreenShots";

        static string htmlresultDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location + "/../../../") + "/TestResults";
        static string htmlreportDirectory = resultDirectory + "/TestReports";
        static string htmlscreenshotDirectory = resultDirectory + "/ScreenShots";


        static string tableBody = string.Empty;
        static string tableStyle = "table{font-family: arial, sans-serif;border-collapse: collapse;padding: 8px;color:#FFFFFF;width: 1200px;margin-left: auto; margin-right: auto;}";
        static string headerStyle = "th{border: 1px solid #dddddd;text-align: left;padding: 8px;background-color: #1882c9;}";
        static string dataStyle = "td{border: 1px solid #dddddd;text-align: left;padding: 8px;color:#000000;}";
        static string rowStyle = "tr:nth-child(even) {background-color: #a8dcff;} tr:nth-child(odd) {background-color: #92caef;}";
        static string htmlHead = string.Format("<head><style>{0} {1} {2} {3}</style></head>", tableStyle, headerStyle, dataStyle, rowStyle);
        static string htmlBody = "<table><tr><th >Test:{TESTNAME}</th><th >Browser: {BROWSER}</th></tr><table><table><tr><th>Test Step</th><th>Description</th><th>Status</th><th>Time</th></tr> {ROWS} </table>";
        static string htmlScreenshots = "<b><p style='color:blue;font-size:20px;text-align: center'>Screenshots of the flow</p></b> {SCBODY}";
        string html = string.Format("<!DOCTYPE html><html>{0}<body>{1} {2} </body></html>", htmlHead, htmlBody, htmlScreenshots);
        static string imageBody = @"<img src='{IMGSRC}' style='margin:auto; width:1200px;display:block'/><br />";
        string finalHtml = string.Empty;
        static string finalSingleImage = string.Empty;
        static string finalSingleImagebody = string.Empty;
        static IWebDriver driver;
        static int failCount = 0;
        static string colorCode = string.Empty;
        private static string failureScreenshot;

        //string consolewriter = @"<table><tr><td>{STEPNAME}</td><td>{STEPDESCRIPTION}</td><td><a href='{IMAGEURL}'> screenshot </a></td></tr></table>";
        public Reporter(string scenarioName, IWebDriver browser)
        {
            driver = browser;
            resultName = scenarioName;
            html = html.Replace("{TESTNAME}", resultName);
            html = html.Replace("{BROWSER}", browser.GetType().Name);
            this.finalHtml = string.Empty;
            finalSingleImage = string.Empty;
            finalSingleImagebody = string.Empty;
            tableBody = string.Empty;
            if (!File.Exists(resultDirectory))
            {
                System.IO.Directory.CreateDirectory(resultDirectory);
            }
            if (!File.Exists(reportDirectory))
            {
                System.IO.Directory.CreateDirectory(reportDirectory);
            }
            if (!File.Exists(reportDirectory))
            {
                System.IO.Directory.CreateDirectory(screenshotDirectory);
            }
        }

        public static void Pass(string stepname, string description, bool takeScreenshot = true)
        {
            string screenShot = "/" + resultName + DateTime.Now.ToString("ddMMyyyyhhmmssffff") + ".png";
            string screenshotName = screenshotDirectory + screenShot;
            string _htmlscreenshotDirectory = htmlscreenshotDirectory + screenShot;
            tableBody += string.Format("<tr><td>{0}</td><td>{1}</td><td>Pass</td><td>{2}</td>", stepname, description, DateTime.Now.ToString());
            if (takeScreenshot)
            {
                TakeScreenshot(screenshotName);
                finalSingleImage = imageBody.Replace("{IMGSRC}", _htmlscreenshotDirectory);
                finalSingleImagebody += finalSingleImage;
            }
            ConsoleWriter(description, screenshotName);
        }

        public static void EventDone(string stepname, string description, bool takeScreenshot = false)
        {
            string screenShot = "/" + resultName + DateTime.Now.ToString("ddMMyyyyhhmmssffff") + ".png";
            string screenshotName = screenshotDirectory + screenShot;
            string _htmlscreenshotDirectory = htmlscreenshotDirectory + screenShot;
            tableBody += string.Format("<tr><td>{0}</td><td>{1}</td><td>Success</td><td>{2}</td>", stepname, description, DateTime.Now.ToString());
            if (takeScreenshot)
            {
                TakeScreenshot(screenshotName);
                finalSingleImage = imageBody.Replace("{IMGSRC}", _htmlscreenshotDirectory);
                finalSingleImagebody += finalSingleImage;
            }
            ConsoleWriter(description, screenshotName);
        }

        public static void Fail(string stepname, string description)
        {
            string alertscreenshot = string.Empty;
            failCount = failCount + 1;
            string screenShot = "/" + resultName + DateTime.Now.ToString("ddMMyyyyhhmmssffff") + ".png";
            string screenshotName = screenshotDirectory + screenShot;
            string _htmlscreenshotDirectory = htmlscreenshotDirectory + screenShot;

            // alertscreenshot = "file://PACCPL-G9NCW52/TestResults/ScreenShots" + screenShot;
            failureScreenshot = alertscreenshot;

            tableBody += string.Format("<tr><td>{0}</td><td>{1}</td><td  bgcolor='#FF0000'>Fail</td><td>{2}</td>", stepname, description, DateTime.Now.ToString());
            TakeScreenshot(screenshotName);
            finalSingleImage = imageBody.Replace("{IMGSRC}", _htmlscreenshotDirectory);
            finalSingleImagebody += finalSingleImage;
            ConsoleWriter(description, screenshotName);
            Assert.Fail(description);
        }

        public static void Inconclusive(string stepname, string description)
        {
            failCount = failCount + 1;
            string screenShot = "/" + resultName + DateTime.Now.ToString("ddMMyyyyhhmmssffff") + ".png";
            string screenshotName = screenshotDirectory + screenShot;
            string _htmlscreenshotDirectory = htmlscreenshotDirectory + screenShot;
            tableBody += string.Format("<tr><td>{0}</td><td>{1}</td><td  bgcolor='#FF0000'>Fail</td><td>{2}</td>", stepname, description, DateTime.Now.ToString());
            TakeScreenshot(screenshotName);
            finalSingleImage = imageBody.Replace("{IMGSRC}", _htmlscreenshotDirectory);
            finalSingleImagebody += finalSingleImage;
            ConsoleWriter(description, screenshotName);
            Assert.Inconclusive(description);
        }

        private static void TakeScreenshot(string screenshotPath)
        {
            try
            {
                if (driver != null)
                {
                    Screenshot ss = null;
                    ss = ((ITakesScreenshot)driver).GetScreenshot();


                    lock (ss)
                    {
                        ss.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
                    }
                }

            }
            catch (Exception exception)
            {
                Assert.Fail("Screenshot exception: ", exception.Message);
            }
        }

        public bool CreateTestReport()
        {
            if (tableBody != string.Empty)
            {
                finalHtml = html.Replace("{ROWS}", tableBody);
                finalHtml = finalHtml.Replace("{TESTNAME}", resultName);
                finalHtml = finalHtml.Replace("{SCBODY}", finalSingleImagebody);
                string fileName = resultName + DateTime.Now.ToString("yyyyMMddHHmmss") + ".html";
                string filepath = reportDirectory + "/" + fileName;
                File.Create(filepath).Dispose();
                File.WriteAllText(filepath, finalHtml);
                return true;
            }
            return false;
        }

        public static void Session(string p, string scmsSessionId)
        {
            string screenshotName = screenshotDirectory + "/" + resultName + DateTime.Now.ToString("ddMMyyyyhhmmssffff") + ".png";
            tableBody += string.Format("<tr><td>{0}</td><td>{1}</td><td>Session Information</td><td>{2}</td>", p, scmsSessionId, DateTime.Now.ToString());
            Console.WriteLine(scmsSessionId);
        }

        public static void Warning(string stepname, string description)
        {
            string screenshotName = screenshotDirectory + "/" + resultName + DateTime.Now.ToString("ddMMyyyyhhmmssffff") + ".png";
            tableBody += string.Format("<tr><td>{0}</td><td>{1}</td><td bgcolor='#FFFF00'>Warning!</td><td>{2}</td>", stepname, description, DateTime.Now.ToString());
            TakeScreenshot(screenshotName);
            finalSingleImage = imageBody.Replace("{IMGSRC}", screenshotName);
            finalSingleImagebody += finalSingleImage;
        }

        public static void ConsoleWriter(string stepdescription, string imageURL)
        {
            string writer = "- {STEPDESCRIPTION} [{IMAGEURL}]";
            writer = writer.Replace("{STEPDESCRIPTION}", stepdescription);
            writer = writer.Replace("{IMAGEURL}", imageURL);
            Console.WriteLine(writer);
        }
    }
}
