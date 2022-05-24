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
            m_driver = new ChromeDriver("C:\\Users\\henadz.zhukau");
        }
        private const string ExpectedLogo = "RECON";
        private const string ExpectedUserDP = "Aliaksandr Semchanka";
        private const string ExpectedURTonDP = "test";
        private const string ExpectedFilterByCategories = "Categories";
        private const string ExpectedActualReconName = "test";
        private const string ExpectedReconRP = "New Year";
        private const string ExpectedFilterByDepartmentLXP = "Department";
        private const string ExpectedDepartment = "Administrator";

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


            emailTextBox.SendKeys("email");
            nextButton.Click();


            m_driver.SwitchTo().Window(m_driver.WindowHandles[1]);

            Thread.Sleep(2000);

            //Store locator values of password text box button
            IWebElement webElement = m_driver.FindElement(By.XPath("//input[@type='password']"));
            webElement.Click();
            IWebElement passwordTextBox = webElement;
            passwordTextBox.SendKeys("password");

            IWebElement next1Button = m_driver.FindElement(By.XPath("//*[@id='passwordNext']/div/button/span"));
            next1Button.Click();


            m_driver.SwitchTo().Window(m_driver.WindowHandles[1]);

            Thread.Sleep(2000);

            //Store locator values of Another Way button
            IWebElement AnotherButton = m_driver.FindElement(By.XPath("//*[@id='view_container']/div/div/div[2]/div/div[2]/div[2]/div[2]/div/div/button/span"));
            AnotherButton.Click();


            m_driver.SwitchTo().Window(m_driver.WindowHandles[1]);

            Thread.Sleep(2000);

            //Store locator values of Google Authenticztor button
            IWebElement AutButton = m_driver.FindElement(By.XPath("//*[@id='view_container']/div/div/div[2]/div/div[1]/div/form/span/section/div/div/div[1]/ul/li[3]/div/div[2]"));
            AutButton.Click();

            m_driver.SwitchTo().Window(m_driver.WindowHandles[1]);

            Thread.Sleep(2000);

            // Store setting key of TOTT
            byte[] key = Base32Encoding.ToBytes("seting key".Replace(" ", "").ToUpperInvariant());
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

            // Find 'Logo' element
            IWebElement ActualLogo = m_driver.FindElement(By.XPath("//*[@id='app']/div/div[1]/a/div/div/div[1]"));
            var ActualLogoText = ActualLogo.Text;

            // Check that logo's text = "RECON"
            Assert.AreEqual(ExpectedLogo, ActualLogoText, "Recon logo is appears after authorisation");
            

        }

        [Test]
        public void DashboardDemo()

        {
           // Authorization Demo
            signUpDemo();
            // Find "Filter by User" Button
            Thread.Sleep(2000);
            IWebElement filterByUserButton = m_driver.FindElement(By.XPath("//*[@id='app']/div/div[3]/div[1]/div[1]/div[1]"));
            filterByUserButton.Click();

            // Find "Search" field and fill this field
            Thread.Sleep(2000);
            IWebElement SearchInDropdown = m_driver.FindElement(By.XPath("//*[@id='app']/div/div[3]/div[1]/div[1]/div[2]/div[2]/div[1]/div/input"));
            SearchInDropdown.Click();
            IWebElement SearchInDropdownTextBox = SearchInDropdown;
            SearchInDropdownTextBox.SendKeys("Aliaksandr Semchanka");

            // Find and click on the checkbox
            IWebElement checkBoxByUserButton = m_driver.FindElement(By.XPath("//*[@id='app']/div/div[3]/div[1]/div[1]/div[2]/div[2]/div[2]/div/div[2]"));
            checkBoxByUserButton.Click();

            // Find user name who sent a recon on the UserReconItem
            Thread.Sleep(2000);
            IWebElement ReconUserItem = m_driver.FindElement(By.XPath("//*[@id='app']/div/div[3]/div[2]/div/div[2]/div/div/div[1]/div[1]/div/div[2]/div[1]/div[2]"));

            //Check that user name who sent a recon = selected user (???)
            var ActualUserDP = ReconUserItem.Text;
            Assert.AreEqual(ExpectedUserDP, ActualUserDP, "Filtered by Aliaksandr Semchanka");

            // Find and click "Clear filters" button
            IWebElement clearFiltersButton = m_driver.FindElement(By.XPath("//*[@id='app']/div/div[3]/div[1]/div[1]/div[2]/div[2]/button"));
            clearFiltersButton.Click();

            Thread.Sleep(1000);
            //Clear "Search" field
            SearchInDropdown.Click();
            SearchInDropdown.Clear();

            // Find 'Search by recon' field and fill that field  
            IWebElement SearchByRecon = m_driver.FindElement(By.XPath("//*[@id='app']/div/div[3]/div[1]/div[2]/div/input"));
            SearchByRecon.Click();
            IWebElement SearchByReconTextBox = SearchByRecon;
            SearchByReconTextBox.SendKeys("test");

            // Find recon name on the UserReconItem
            Thread.Sleep(2000);
            IWebElement NameReconUserItem = m_driver.FindElement(By.XPath("//*[@id='app']/div/div[3]/div[2]/div/div[1]/div/div/div[1]/div[3]"));

            // Check that NameURI = filled data
            var ActuaReconNameDP = NameReconUserItem.Text;

            //Check that recon name contains "test"
            Assert.LessOrEqual(ExpectedURTonDP, ActuaReconNameDP, "Recon name = %text%");

            // Go to the Recons page
            IWebElement ReconsPage = m_driver.FindElement(By.XPath("//*[@id='app']/div/div[2]/div[1]/a[2]"));
            ReconsPage.Click();

            // Find "Fiter By Categories" button 
            Thread.Sleep(2000);
            IWebElement FiterByCategories = m_driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[3]/div[1]/div[1]/div[1]/div/div[2]/div[2]"));
            var ActualFilterByCategories = FiterByCategories.Text;

            // Check that "Fiter By Categories" button is appears
            Assert.AreEqual(ExpectedFilterByCategories, ActualFilterByCategories, "'Fiter By Categories' button is appears");

        }

        [Test]
        public void ReconsDemo1()
        {
            //Authorization and Dashboard demo
            DashboardDemo();

            // Find 'Search by title' field and fill that field  
            IWebElement SearchByTitle = m_driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[3]/div[1]/div[2]/div/input"));
            SearchByTitle.Click();
            IWebElement SearchByTitleTextBox = SearchByTitle;
            SearchByTitleTextBox.SendKeys("test");

            Thread.Sleep(2000);
            // Find Recon Name
            IWebElement ReconName = m_driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[3]/div[2]/div/div[1]/div/div/div[1]/div/div[2]/div[1]/p"));
            var ActualReconName = ReconName.Text;

            // Check that recon name contains "test" 
            Assert.LessOrEqual(ExpectedActualReconName, ActualReconName, "Recon name = % text % ");

            // Click on the Filter By Categories button
            IWebElement FiterByCategories = m_driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[3]/div[1]/div[1]/div[1]/div/div[2]/div[2]"));
            FiterByCategories.Click();
 
            Thread.Sleep(2000);
            // Find "Search" field and fill this field
            IWebElement SearchInDropdownR = m_driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[3]/div[1]/div[1]/div[2]/div[2]/div[1]/div/input"));
            SearchInDropdownR.Click();
            IWebElement SearchInDropdownRTextBox = SearchInDropdownR;
            SearchInDropdownRTextBox.SendKeys("New Year");

            // Find and click on the checkbox "New Year"
            IWebElement checkBoxBycategoriesButton = m_driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[3]/div[1]/div[1]/div[2]/div[2]/div[2]/div"));
            checkBoxBycategoriesButton.Click();

            Thread.Sleep(2000);
        
            //Check that recons filtered by "New Year" category
            //var ActualReconRP = checkBoxBycategoriesButton.TagName;
            //Assert.AreEqual(ExpectedReconRP, ActualReconRP, "Filtered by 'New Year' category");

            // Find and click "Clear filters" button
            IWebElement clearFiltersButton = m_driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[3]/div[1]/div[1]/div[2]/div[2]/button"));
            clearFiltersButton.Click();

            Thread.Sleep(1000);
            //Clear "Search" field
            SearchInDropdownR.Click();
            SearchInDropdownR.Clear();

            Thread.Sleep(1000);
            //Clear "Search by title" field
            SearchByTitle.Click();
            SearchByTitle.Clear();

            Thread.Sleep(1000);
       //     //Find and click "Give a recon" field
           IWebElement GiveReconButton = m_driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[3]/div[2]/div/div[1]/div/div/div[1]/div/div[2]/div[2]/button"));
            GiveReconButton.Click();
            Thread.Sleep(5000);
            
            //while (m_driver.WindowHandles.Count == 1)
            //{
            //  Thread.Sleep(200);
            //}

            //     m_driver.SwitchTo().Window(m_driver.WindowHandles[1]);

            //     WebDriverWait wait = new WebDriverWait(m_driver, TimeSpan.FromSeconds(1));

            //     wait.Until(e => e.FindElements(By.XPath("//*[@id='modal - root']/div/div/div/div[3]/div[1]/div/div[5]']")).Count > 0);

            // Find and click 'Give a recon' button on some recon
            IWebElement UserInModalWindow = m_driver.FindElement(By.XPath("//*[@id='modal - root']/div/div/div/div[3]/div[1]/div/div[2]/div[1]/div"));
            UserInModalWindow.Click();  

            //Find recon name 
            IWebElement ReconNameMW = m_driver.FindElement(By.XPath("//*[@id='modal - root']/div/div/div/div[3]/div[1]/div/div[2]"));
            var ActualReconNameMW = ReconNameMW.Text;

            // Find and click 'Give a recon' button in the modal window
           IWebElement GiveAReconButton = m_driver.FindElement(By.XPath("//*[@id='modal - root']/div/div/div/div[6]/button"));
            GiveAReconButton.Click();

            //Selected to window
           // m_driver.SwitchTo().Window(m_driver.WindowHandles[0]);
            Thread.Sleep(3000);

            //Find and click Logo
            IWebElement DasboardButton = m_driver.FindElement(By.XPath("//*[@id='app']/div/div[1]/a"));
            DasboardButton.Click();

            Thread.Sleep(2000);

            //Find name for first UserRecon Item on the Dasboard page
           IWebElement FirstReconDP = m_driver.FindElement(By.XPath("//*[@id='app']/div/div[3]/div[2]/div/div[1]/div/div/div[1]/div[3]"));
            var ExpectedReconNameMW = FirstReconDP.Text;

            // Check that name for first UserRecon Item on the Dasboard page = name gived recon
            Assert.AreEqual(ExpectedReconNameMW, ActualReconNameMW, "Gived recon is first on the Dashboard page");

            // Go to Recon Page
            IWebElement ReconsPage = m_driver.FindElement(By.XPath("//*[@id='app']/div/div[2]/div[1]/a[2]"));
            ReconsPage.Click();

            // Find and click on the avatar. User go to Profile Page
            IWebElement AvatarButton = m_driver.FindElement(By.XPath("//*[@id='app']/div/div[1]/a"));
            var ExpectedUserNameRP = AvatarButton.Text;
            AvatarButton.Click();

            Thread.Sleep(2000);

            // Find user name on the Profile page
            IWebElement UserNamePP = m_driver.FindElement(By.XPath("//*[@id='app']/div/div[3]/div[1]/div/div[2]/div[2]"));
            var ActualUserNamePP = UserNamePP.Text;

            // Check that user name = name on the avatar
            Assert.AreEqual(ExpectedUserNameRP, ActualUserNamePP, "Profile page is appears");
        }

        [Test]
        public void ProfileDemo()
        {
            //Authorization and Dashboard demo
            ReconsDemo1();

            // Find 'Search by recon' field and fill that field
            IWebElement SearchByRecon = m_driver.FindElement(By.XPath("//*[@id='app']/div/div[3]/div[1]/div/div[4]/div[1]/div/div[2]/div/input"));
            SearchByRecon.Click();
            IWebElement SearchByReconTextBox = SearchByRecon;
            SearchByReconTextBox.SendKeys("test");

            Thread.Sleep(2000);
            // Find Recon Name
            IWebElement ReconName = m_driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[3]/div[2]/div/div[1]/div/div/div[1]/div/div[2]/div[1]/p"));
            var ActualReconName = ReconName.Text;

            // Check that recon name contains "test" 
            Assert.LessOrEqual(ExpectedActualReconName, ActualReconName, "Recon name = % text % ");

            IWebElement FiterByUsers = m_driver.FindElement(By.XPath("//*[@id='app']/div/div[3]/div[1]/div/div[4]/div[1]/div/div[1]/div[2]/div[2]/div[1]/div/input"));
            FiterByUsers.Click();

            Thread.Sleep(2000);
            // Find "Search" field and fill this field
            IWebElement SearchInDropdownP = m_driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[3]/div[1]/div[1]/div[2]/div[2]/div[1]/div/input"));
            SearchInDropdownP.Click();
            IWebElement SearchInDropdownPTextBox = SearchInDropdownP;
            SearchInDropdownPTextBox.SendKeys("Stanislau Mandryk");
            var ExpectedRUserNamePP = SearchInDropdownP.Text;

            // Find and click on the checkbox "Stanislau Mandryk"
            IWebElement checkBoxByUsersButton = m_driver.FindElement(By.XPath("//*[@id='app']/div/div[3]/div[1]/div/div[4]/div[1]/div/div[1]/div[2]/div[2]/div[2]/div[5]"));
            checkBoxByUsersButton.Click();

            Thread.Sleep(2000);

            IWebElement UsersNamePP = m_driver.FindElement(By.XPath("//*[@id='app']/div/div[3]/div[1]/div/div[4]/div[1]/div/div[1]/div[2]/div[2]/div[2]/div[5]"));
            var ActualRUserNamePP = UsersNamePP.Text;

            // Check that User Name = choused name
            Assert.LessOrEqual(ExpectedRUserNamePP, ActualRUserNamePP, "User Name = choused name");

            Thread.Sleep(2000);

            // Find and click on the Back Button
            IWebElement BackButton = m_driver.FindElement(By.XPath("//*[@id='app']/div/div[3]/div[1]/div/div[1]/button/svg"));
            BackButton.Click();

            // Find and click on the LXPeople Button
            IWebElement LXPeopleButton = m_driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[2]/div[1]/a[3]"));
            LXPeopleButton.Click();

            // Check thet Filter by department button is appears
            IWebElement FilterByDepartmentLXP = m_driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[3]/div[1]/div[1]/div[1]/div/div[2]"));
            var ActualFilterByDepartmentLXP = FilterByDepartmentLXP.Text;

            // Check that User Name = choused name
            Assert.AreEqual(ExpectedFilterByDepartmentLXP, ActualFilterByDepartmentLXP, "Filter by Department is appears");
            //*[@id="app"]/div[1]/div[3]/div[1]/div[1]/div[1]/div/div[2] //*[@id="app"]/div[1]/div[3]/div[1]/div[1]/div[1]/div/div[2]
        }

        public void LXPeopleDemo()
        {
            //Authorization, Dashboard and Recons demo
            ProfileDemo();

            // Find 'Search by name' field and fill that field
            IWebElement SearchByName = m_driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[3]/div[1]/div[2]/div/input"));
            SearchByName.Click();
            IWebElement SearchByNameTextBox = SearchByName;
            SearchByNameTextBox.SendKeys("Ivan Lukyanau");

            Thread.Sleep(2000);
            // Find and click "Filter by department" Button
            IWebElement FilterByDepartmentLXP = m_driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[3]/div[1]/div[1]/div[1]/div/div[2]"));
            FilterByDepartmentLXP.Click();
            Thread.Sleep(2000);

            // Find and fill "Search" field
            IWebElement SearchFieldD = m_driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[3]/div[1]/div[1]/div[2]/div[2]/div[1]/div/input"));
            SearchByName.Click();
            IWebElement SearchFieldDtextBox = SearchFieldD;
            SearchFieldDtextBox.SendKeys("Administration");

            Thread.Sleep(2000);
            // Click on the check box "Administrator"
            IWebElement CheckBoxDD = m_driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[3]/div[1]/div[1]/div[2]/div[2]/div[2]/div"));
            CheckBoxDD.Click();

            //Check that recons filtered by "Administrator" department
            //var ActualDepartment = CheckBoxDD.TagName;
            //Assert.AreEqual(ExpectedDepartment, ActualDepartment, "Filtered by 'Administrator' department");

            // Find and click "Clear filter" button
            IWebElement ClearFilterDD = m_driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[3]/div[1]/div[1]/div[2]/div[2]/button"));
            ClearFilterDD.Click();
            Thread.Sleep(2000);

            // Clear "Search" field 
            SearchFieldD.Clear();
            Thread.Sleep(2000);

            // Clear "Search by name" field
            SearchByName.Clear();
            Thread.Sleep(2000);

            //Find user name
            IWebElement UserNameLX = m_driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[3]/div[2]/div/div[4]/div/div[1]/div[2]/div[1]"));
            var ExpextedUserName = UserNameLX.Text;

            // Find some user
            IWebElement UserLX = m_driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[3]/div[2]/div/div[4]/div"));
            UserLX.Click();
            Thread.Sleep(2000);

            //*[@id="app"]/div[1]/div[3]/div[2]/div/div[4]/div/div[1]/div[2]/div[1] - user name

            // Find and click View on the some user
            IWebElement ViewButton = m_driver.FindElement(By.XPath("//*[@id='app']/div[1]/div[3]/div[2]/div/div[4]/div/div[2]/button/div[1]"));
            ViewButton.Click();
            Thread.Sleep(2000);

            // Find User Name on the LX People/User page
            IWebElement UserNameLXU = m_driver.FindElement(By.XPath("//*[@id='app']/div/div[3]/div[1]/div/div/div[3]/div/div[1]/div[2]/div[1]"));
            var ActualUserName = UserNameLXU.Text;

            // Check that User name = Choosed user
            Assert.AreEqual(ExpextedUserName, ActualUserName, "User name = Choosed user");




        }


    }

}
