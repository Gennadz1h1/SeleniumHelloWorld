using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using OtpNet;
using System.Text;

namespace TestSelect
{
    class TestSelect
    {
        IWebDriver m_driver;


        [OneTimeSetUp]
        public void startBrowser()
        {
            m_driver = new ChromeDriver("{{path to driver}}");
        }


        [Test]
        public void signUpDemo()
        {
            m_driver.Url = "https://lx-onb-recon-ui.web.app/auth";
            m_driver.Manage().Window.Maximize();
            IWebElement signUpButton = m_driver.FindElement(By.XPath("//*[@id='app']/div/div/div[2]/button"));
            signUpButton.Click();

            //wait until new window will be captured
            while (m_driver.WindowHandles.Count == 1)
            {
                Thread.Sleep(200);
            }

            m_driver.SwitchTo().Window(m_driver.WindowHandles[1]);

            //Setting up driver wait
            WebDriverWait wait = new WebDriverWait(m_driver, TimeSpan.FromSeconds(5));
            //Setup wait ... until
            wait.Until(e => e.FindElements(By.XPath("//*[@id='identifierId']")).Count > 0);

            // Store locator values of email text box and sign up button
            IWebElement emailTextBox = m_driver.FindElement(By.XPath("//*[@id='identifierId']"));
            IWebElement nextButton = m_driver.FindElement(By.XPath("//*[@id='identifierNext']/div/button"));


            emailTextBox.SendKeys("{{email}}");
            nextButton.Click();


            m_driver.SwitchTo().Window(m_driver.WindowHandles[1]);

            Thread.Sleep(2000);

            //Store locator values of password text box button
            IWebElement webElement = m_driver.FindElement(By.XPath("//input[@type='password']"));
            webElement.Click();
            IWebElement passwordTextBox = webElement;
            passwordTextBox.SendKeys("{{password}}");

            IWebElement next1Button = m_driver.FindElement(By.XPath("//*[@id='passwordNext']/div/button/span"));
            next1Button.Click();


            m_driver.SwitchTo().Window(m_driver.WindowHandles[1]);

            Thread.Sleep(2000);

            //Store locator values of Another Way button
            IWebElement AnotherButton = m_driver.FindElement(By.XPath("//*[@id='view_container']/div/div/div[2]/div/div[2]/div[2]/div[2]/div/div/button/span"));
            AnotherButton.Click();


            m_driver.SwitchTo().Window(m_driver.WindowHandles[1]);

            Thread.Sleep(2000);

            IWebElement AutButton = m_driver.FindElement(By.XPath("//*[@id='view_container']/div/div/div[2]/div/div[1]/div/form/span/section/div/div/div[1]/ul/li[3]/div/div[2]"));
            AutButton.Click();

            m_driver.SwitchTo().Window(m_driver.WindowHandles[1]);

            Thread.Sleep(2000);

            byte[] key = Base32Encoding.ToBytes("{{setting key}}".Replace(" ", "").ToUpperInvariant());
            var totp = new Totp(key);

            IWebElement websElement = m_driver.FindElement(By.XPath("//*[@id='totpPin']"));
            websElement.Click();
            IWebElement TOTTextBox = websElement;
            var totpCode = totp.ComputeTotp(DateTime.UtcNow);

            TOTTextBox.SendKeys(totpCode);

            IWebElement AuttButton = m_driver.FindElement(By.XPath("//*[@id='totpNext']/div/button/span"));
            AuttButton.Click();

        }

    }

}
