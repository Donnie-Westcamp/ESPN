using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System;
using System.IO;
using ESPN;
using System.Threading;

namespace Espn_Automation_VECTOR
{
    class ESPN_Automation
    {
        //defining driver, and output file paths 
        IWebDriver driver;
        String headlineFile = "..\\headlines.txt";
        String ssFile = "..\\homepage.png";

        [SetUp]
        public void TestInitialize()
        {
            //all tests are navigating to espn so need this before each test
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.espn.com");
            //asserting that page loads by verifying account button is present/displayed
            Assert.IsTrue(driver.FindElement(By.Id("global-user-trigger")).Displayed);
        }


        [Test]
        public void TestCase1()
        {
            String headline;

            headline = driver.FindElement(By.XPath("/html/body/div[5]/section/section/div/section[2]/section[1]/section/a/div/div[3]/h1")).Text;

            using (StreamWriter w = File.AppendText(headlineFile))
            {
                w.WriteLine("|" + DateTime.Now + "|" + headline + "|");
            }

            Screenshot snap = ((ITakesScreenshot)driver).GetScreenshot();

            snap.SaveAsFile(ssFile);

        }

        [Test]
        public void TestCase2()
        {
            //Clicking account/profile
            driver.FindElement(By.Id("global-user-trigger")).Click();


            //Clicking Add Favorite Button
            Utilities.WaitForElementToAppear(driver, By.XPath("/html/body/div[5]/div[2]/header/div[2]/ul/li[2]/div/div/ul[2]/li/div/div/a"));
            driver.FindElement(By.XPath("/html/body/div[5]/div[2]/header/div[2]/ul/li[2]/div/div/ul[2]/li/div/div/a")).Click();

            //Handeling iframe from favorites
            driver.SwitchTo().Frame(driver.FindElement(By.Id("favorites-manager-iframe")));

            //Clicking MLB Follow Button
            Utilities.WaitForElementToAppear(driver, By.XPath("/html/body/div[2]/div/div/section/div/div/section/div/section/section[1]/div/ul/li[4]/div[2]/div/button"));
            driver.FindElement(By.XPath("/html/body/div[2]/div/div/section/div/div/section/div/section/section[1]/div/ul/li[4]/div[2]/div/button")).Click();

            //Asserting MLB was added to Follow List
            Utilities.WaitForElementToAppear(driver, By.XPath("/html/body/div[2]/div/div/section/div/div/section/div/section/section[2]/div[1]/ul[3]/li[2]/div[1]/div[2]/h2"));
            Assert.IsTrue(driver.FindElement(By.XPath("/html/body/div[2]/div/div/section/div/div/section/div/section/section[2]/div[1]/ul[3]/li[2]/div[1]/div[2]/h2")).Text.Contains("MLB"));
        }

        [TearDown]
        public void Teardown()
        {
            driver.Close();
            driver.Quit();
        }

    }
}