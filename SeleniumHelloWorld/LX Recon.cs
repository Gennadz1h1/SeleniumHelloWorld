using Nest;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static OpenQA.Selenium.Support.UI.WebDriverWait

namespace TestSelect
{
    class TestSelect
    {
        IWebDriver m_driver;


        [Test]
        public void TestSelectDemo()
        {
            m_driver = new ChromeDriver("C:\\Users\\henadz.zhukau");
            m_driver.Url = "https://lx-onb-recon-ui.web.app/dashboard";
            m_driver.Manage().Window.Maximize();
            WebElement link = (WebElement)m_driver.FindElement(By.XPath("//*[@id='app']/div/div/div[2]/button/p"));
            link.Click();
            // сюда я вставлял элемент ожидания, так как ошибка у меня в том что не находит элемент emailTextBox, это один из примеров  m_driver.Manage().Timeouts().ImplicitWait(5, TimeUnit.Second);
            IWebElement emailTextBox = m_driver.FindElement(By.XPath("//*[@id='identifierId']"));
            IWebElement signUpButton = m_driver.FindElement(By.XPath("//*[@id='identifierNext']/div/button/div[3]"));

            emailTextBox.SendKeys("henadz.zhukau@leverx.com");
            signUpButton.Click();

        }
    }
}
