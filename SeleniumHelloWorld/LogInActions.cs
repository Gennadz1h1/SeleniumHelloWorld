using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using OtpNet;
using System.Text;
using OpenQA.Selenium.Interactions;

namespace SeleniumHelloWorld
{
    internal class LogInActions
    {
        public static void signUpDemo(IWebDriver m_driver)
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

            // Use Environment
            Environment.GetEnvironmentVariable("email");
            var email = Environment.GetEnvironmentVariable("email");
            emailTextBox.SendKeys(email);
            nextButton.Click();

            m_driver.SwitchTo().Window(m_driver.WindowHandles[1]);

            Thread.Sleep(2000);

            //Store locator values of password text box button
            IWebElement passwordTextBox = m_driver.FindElement(By.XPath("//input[@type='password']"));
            passwordTextBox.Click();
            Environment.GetEnvironmentVariable("password");
            var password = Environment.GetEnvironmentVariable("password");
            passwordTextBox.SendKeys(password);

            IWebElement next1Button = m_driver.FindElement(By.XPath("//*[@id='passwordNext']/div/button/span"));
            next1Button.Click();


            m_driver.SwitchTo().Window(m_driver.WindowHandles[1]);

            Thread.Sleep(2000);

            //Store locator values of Another Way button
            IWebElement AnotherButton = m_driver.FindElement(By.XPath("//span[text()='Другой способ']"));
            AnotherButton.Click();


            m_driver.SwitchTo().Window(m_driver.WindowHandles[1]);

            Thread.Sleep(2000);

            //Store locator values of Google Authenticztor button
            IWebElement AutButton = m_driver.FindElement(By.XPath("//strong[text()='Google Authenticator']"));
            AutButton.Click();

            m_driver.SwitchTo().Window(m_driver.WindowHandles[1]);

            Thread.Sleep(2000);

            // Store setting key of TOTT
            Environment.GetEnvironmentVariable("setting key");
            var settingkey = Environment.GetEnvironmentVariable("setting key");
            byte[] key = Base32Encoding.ToBytes(settingkey.Replace(" ", "").ToUpperInvariant());
            var totp = new Totp(key);

            IWebElement websElement = m_driver.FindElement(By.XPath("//*[@id='totpPin']"));
            websElement.Click();
            // Generation TOTT 
            IWebElement TOTTextBox = websElement;
            var totpCode = totp.ComputeTotp(DateTime.UtcNow);

            TOTTextBox.SendKeys(totpCode);

            IWebElement AuttButton = m_driver.FindElement(By.XPath("//*[@id='totpNext']/div/button/span"));
            AuttButton.Click();

            // Select first window
            m_driver.SwitchTo().Window(m_driver.WindowHandles[0]);
            Thread.Sleep(5000);
        }
    }
}
