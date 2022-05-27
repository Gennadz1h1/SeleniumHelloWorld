using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using OtpNet;
using System.Text;
using OpenQA.Selenium.Interactions;
using SeleniumHelloWorld;

namespace TestSelect
{
    class TestSelect
    {
        IWebDriver m_driver;


        [OneTimeSetUp]
        public void startBrowser()
        {
            m_driver = new ChromeDriver("C:\\Users\\henadz.zhukau");
            LogInActions.signUpDemo(m_driver);
        }
        private const string ExpectedLogo = "RECON";
        private const string ExpectedUserDP = "Aliaksandr Semchanka";
        private const string ExpectedURTonDP = "test";
        private const string ExpectedFilterByCategories = "Categories";
        private const string ExpectedActualReconName = "test";
        private const string ExpectedReconRP = "New Year";
        private const string ExpectedFilterByDepartmentLXP = "Department";
        private const string ExpectedDepartment = "Administrator";
        private const string Expectedbutton = "Department";

        [Test]
        [Ignore("USELESS")]
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

            // Use Environment
            Environment.GetEnvironmentVariable ("email");
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

            // Find 'Logo' element
            IWebElement ActualLogo = m_driver.FindElement(By.XPath("//*[@id='app']/div/div[1]/a/div/div/div[1]"));
            var ActualLogoText = ActualLogo.Text;

            // Check that logo's text = "RECON"
            Assert.AreEqual(ExpectedLogo, ActualLogoText, "Recon logo is appears after authorisation");
            

        }

        [Test]
        public void DashboardDemo()

        {
            
            // Find "Filter by User" Button
            Thread.Sleep(2000);
            IWebElement filterByUserButton = m_driver.FindElement(By.XPath("//div[@role='button']"));
            filterByUserButton.Click();

            // Find "Search" field and fill this field
            Thread.Sleep(2000);
            IWebElement SearchInDropdown = m_driver.FindElement(By.XPath("//input[@placeholder = 'Search']"));
            SearchInDropdown.Click();
            IWebElement SearchInDropdownTextBox = SearchInDropdown;
            SearchInDropdownTextBox.SendKeys("Aliaksandr Semchanka");

            // Find and click on the checkbox
            IWebElement checkBoxByUserButton = m_driver.FindElement(By.XPath("//div[text()='Aliaksandr Semchanka']"));
            checkBoxByUserButton.Click();

            // Find user name who sent a recon on the UserReconItem
            Thread.Sleep(2000);
            IWebElement ReconUserItem = m_driver.FindElement(By.XPath("//div[@title='Aliaksandr Semchanka']"));

            //Check that user name who sent a recon = selected user (???)
            var ActualUserDP = ReconUserItem.Text;
            Assert.AreEqual(ExpectedUserDP, ActualUserDP, "Filtered by Aliaksandr Semchanka");

            // Find and click "Clear filters" button
            IWebElement clearFiltersButton = m_driver.FindElement(By.XPath("//button[text()='Clear filters']"));
            clearFiltersButton.Click();

            Thread.Sleep(1000);
            //Clear "Search" field
            SearchInDropdown.Click();
            SearchInDropdown.Clear();

            // Find 'Search by recon' field and fill that field  
            IWebElement SearchByRecon = m_driver.FindElement(By.XPath("//input[@placeholder='Search by recon']"));
            SearchByRecon.Click();
            IWebElement SearchByReconTextBox = SearchByRecon;
            SearchByReconTextBox.SendKeys("test");

            // Find recon name on the UserReconItem
            Thread.Sleep(2000);
            IWebElement NameReconUserItem = m_driver.FindElement(By.XPath("//div[@style='transform: translateY(0px);']"));

            // Check that NameURI = filled data
            var ActuaReconNameDP = NameReconUserItem.Text;

            //Check that recon name contains "test"
            Assert.LessOrEqual(ExpectedURTonDP, ActuaReconNameDP, "Recon name = %text%");

            IWebElement body = m_driver.FindElement(By.TagName("body"));

            IAction scrollDown = new Actions(m_driver)
                .MoveToElement(body, body.Size.Width - 10, 15) // position mouse over scrollbar
                .ClickAndHold()
                .MoveByOffset(0, 50) // scroll down
                .Release()
                .Build();

            scrollDown.Perform();


        }

        [TearDown]
        public void TearDown()
        {
            m_driver.Url = "https://lx-onb-recon-ui.web.app/";
            Thread.Sleep(4000);
        }

        [Test]
        public void ReconsDemo()
        {
            //Authorization demo
            

            // Find and click to "Recons" button 
            IWebElement ReconsButton = m_driver.FindElement(By.XPath("//a[@href='/recons']"));
            ReconsButton.Click();
            Thread.Sleep(2000);

            // Find 'Search by title' field and fill that field  
            IWebElement SearchByTitle = m_driver.FindElement(By.XPath("//input[@placeholder='Search by title']"));
            SearchByTitle.Click();
            IWebElement SearchByTitleTextBox = SearchByTitle;
            SearchByTitleTextBox.SendKeys("test");

            Thread.Sleep(2000);
            // Find Recon Name
            IWebElement ReconName = m_driver.FindElement(By.XPath("//div[@style='transform: translateY(0px);']"));
            var ActualReconName = ReconName.Text;

            // Check that recon name contains "test" 
            Assert.LessOrEqual(ExpectedActualReconName, ActualReconName, "Recon name = % text % ");

            // Click on the Filter By Categories button
            IWebElement FiterByCategories = m_driver.FindElement(By.XPath("//div[@role='button']"));
            FiterByCategories.Click();
 
            Thread.Sleep(2000);
            // Find "Search" field and fill this field
            IWebElement SearchInDropdownR = m_driver.FindElement(By.XPath("//input[@placeholder = 'Search']"));
            SearchInDropdownR.Click();
            IWebElement SearchInDropdownRTextBox = SearchInDropdownR;
            SearchInDropdownRTextBox.SendKeys("New Year");

            // Find and click on the checkbox "New Year"
            IWebElement checkBoxBycategoriesButton = m_driver.FindElement(By.XPath("//div[text()='New Year']"));
            checkBoxBycategoriesButton.Click();

            Thread.Sleep(2000);
        
            //Check that recons filtered by "New Year" category
            //var ActualReconRP = checkBoxBycategoriesButton.TagName;
            //Assert.AreEqual(ExpectedReconRP, ActualReconRP, "Filtered by 'New Year' category");

            // Find and click "Clear filters" button
            IWebElement clearFiltersButton = m_driver.FindElement(By.XPath("//button[text()='Clear filters']"));
            clearFiltersButton.Click();

            Thread.Sleep(1000);
            //Clear "Search" field
            SearchInDropdownR.Click();
            SearchInDropdownR.Clear();

            Thread.Sleep(1000);
            //Clear "Search by title" field
            SearchByTitle.Click();
            SearchByTitle.Clear();

            Thread.Sleep(2000);
            //     //Find and click "Give a recon" button on some recon
            IWebElement GiveReconButton = m_driver.FindElement(By.XPath("//button[text()='Give a recon']"));
            GiveReconButton.Click();
            Thread.Sleep(3000);


            // Find and click 'Search by name' field and fill that field
            IWebElement SearchByNameModalWindow = m_driver.FindElement(By.XPath("//input[@placeholder='Search by name']"));
            SearchByNameModalWindow.Click();
            IWebElement SearchByNameModalWindowTextBox = SearchByNameModalWindow;
            SearchByNameModalWindowTextBox.SendKeys("Ivan Lukyanau");
            Thread.Sleep(2000);

            //Find recon name
            IWebElement ReconNameMW = m_driver.FindElement(By.XPath("(//p[text()='test for recon 3'])[2]"));
            var ActualReconNameMW = ReconNameMW.Text;

            // Find element for IL
            IWebElement IvanLukanuyL = m_driver.FindElement(By.XPath("//p[text()='Ivan Lukyanau']"));

            //Find User in modal Window
            IWebElement UserinModalWindow = m_driver.FindElement(By.XPath("//button[contains(@class, 'move-button')]"));
            Actions actions = new Actions(m_driver);
            actions.MoveToElement(IvanLukanuyL).Perform();

            //Find User button and move to "Give to" field
            //IWebElement UserinModalWindow = m_driver.FindElement(By.XPath("(//button[@type='button'])[18]"));
            UserinModalWindow.Click();
            Thread.Sleep(2000);

            // Find and click 'Give a recon' button in the modal window
            IWebElement GiveAReconButton = m_driver.FindElement(By.XPath("//button[contains(@class, 'create-recon')]"));
            GiveAReconButton.Click();

            //Selected to window
           // m_driver.SwitchTo().Window(m_driver.WindowHandles[0]);
            Thread.Sleep(3000);

            //Find and click Logo
            IWebElement DasboardButton = m_driver.FindElement(By.XPath("(//a[@href='/dashboard'])[1]"));
            DasboardButton.Click();

            Thread.Sleep(2000);

            //Find name for first UserRecon Item on the Dasboard page
           IWebElement FirstReconDP = m_driver.FindElement(By.XPath("//div[text()='test for recon 3']"));
            var ExpectedReconNameMW = FirstReconDP.Text;

            // Check that name for first UserRecon Item on the Dasboard page = name gived recon
            Assert.AreEqual(ExpectedReconNameMW, ActualReconNameMW, "Gived recon is first on the Dashboard page");

 
        }

        [Test]
        public void ProfileDemo()
        {
            //Authorization demo
            

            // Find and click to Avatar 
            IWebElement Avatar = m_driver.FindElement(By.XPath("//*[@id='app']/div/div[1]/div/button[1]"));
            Avatar.Click();
            Thread.Sleep(2000);

            // Find 'Search by recon' field and fill that field
            IWebElement SearchByRecon = m_driver.FindElement(By.XPath("//input[@placeholder='Search by recon']"));
            SearchByRecon.Click();
            IWebElement SearchByReconTextBox = SearchByRecon;
            SearchByReconTextBox.SendKeys("test");

            Thread.Sleep(2000);
            // Find Recon Name
            IWebElement ReconName = m_driver.FindElement(By.XPath("//div[@style='transform: translateY(0px);']"));
            var ActualReconName = ReconName.Text;

            // Check that recon name contains "test" 
            Assert.LessOrEqual(ExpectedActualReconName, ActualReconName, "Recon name = % text % ");

            // Find and click 'Filter by user' button
            IWebElement FiterByUsers = m_driver.FindElement(By.XPath("//div[@role='button']"));
            FiterByUsers.Click();

            Thread.Sleep(2000);
            // Find "Search" field and fill this field
            IWebElement SearchInDropdownP = m_driver.FindElement(By.XPath("//input[@placeholder = 'Search']"));
            SearchInDropdownP.Click();
            IWebElement SearchInDropdownPTextBox = SearchInDropdownP;
            SearchInDropdownPTextBox.SendKeys("Stanislau Mandryk");
            var ExpectedRUserNamePP = SearchInDropdownP.Text;

            // Find and click on the checkbox "Stanislau Mandryk"
            IWebElement checkBoxByUsersButton = m_driver.FindElement(By.XPath("//div[text()='Stanislau Mandryk'][1]"));
            checkBoxByUsersButton.Click();

            Thread.Sleep(2000);

            // Find user name first Recon User Item
            IWebElement UsersNamePP = m_driver.FindElement(By.XPath("//div[text()='Stanislau Mandryk']"));
            var ActualRUserNamePP = UsersNamePP.Text;

            // Check that User Name = choused name
            Assert.LessOrEqual(ExpectedRUserNamePP, ActualRUserNamePP, "User Name = choused name");
           
           

        }

        [Test]
        public void LXPeopleDemo()
        {
            //Authorization demo
            

            //Find and click "LX People" button
            IWebElement LXPeopleButton = m_driver.FindElement(By.XPath("//a[@href='/lxpeople']"));
            LXPeopleButton.Click();
            Thread.Sleep(2000);

            // Find 'Search by name' field and fill that field
            IWebElement SearchByName = m_driver.FindElement(By.XPath("//input[@placeholder='Search by name']"));
            SearchByName.Click();
            IWebElement SearchByNameTextBox = SearchByName;
            SearchByNameTextBox.SendKeys("Ivan Lukyanau");

            Thread.Sleep(2000);
            // Find and click "Filter by department" Button
            IWebElement FilterByDepartmentLXP = m_driver.FindElement(By.XPath("//div[@role='button']"));
            FilterByDepartmentLXP.Click();
            Thread.Sleep(2000);

            // Find and fill "Search" field
            IWebElement SearchFieldD = m_driver.FindElement(By.XPath("//input[@placeholder = 'Search']"));
            SearchFieldD.Click();
            IWebElement SearchFieldDtextBox = SearchFieldD;
            SearchFieldDtextBox.SendKeys("Administration");

            Thread.Sleep(2000);
            // Click on the check box "Administrator"
            IWebElement CheckBoxDD = m_driver.FindElement(By.XPath("//div[@title='Administration']"));
            CheckBoxDD.Click();
            Thread.Sleep(2000);

            //Check that recons filtered by "Administrator" department
            //var ActualDepartment = CheckBoxDD.TagName;
            //Assert.AreEqual(ExpectedDepartment, ActualDepartment, "Filtered by 'Administrator' department");

            // Find and click "Clear filter" button
            IWebElement ClearFilterDD = m_driver.FindElement(By.XPath("//button[text()='Clear filters']"));
            ClearFilterDD.Click();
            Thread.Sleep(2000);

            // Clear "Search" field 
            SearchFieldD.Clear();
            Thread.Sleep(2000);

            // Clear "Search by name" field
            SearchByName.Clear();
            Thread.Sleep(2000);

            //Find user name
            IWebElement UserNameLX = m_driver.FindElement(By.XPath("//div[@title='Ivan Lukyanau']"));
            var ExpextedUserName = UserNameLX.Text;

            // Find some user
           // IWebElement UserLX = m_driver.FindElement(By.XPath("//div[='app']"));
            //UserLX.Click();
            Thread.Sleep(2000);

            // Find and click View on the some user
            FilterByDepartmentLXP.Click();
            Actions actions = new Actions(m_driver);
            actions.MoveToElement(UserNameLX).Perform();

            IWebElement ViewButton = m_driver.FindElement(By.XPath("(//button/div[text()='View'])[1]"));
            ViewButton.Click();
            Thread.Sleep(2000);

            // Find User Name on the LX People/User page
            IWebElement UserNameLXU = m_driver.FindElement(By.XPath("//div[@title='Ivan Lukyanau']"));
            var ActualUserName = UserNameLXU.Text;
            Thread.Sleep(2000);

            // Check that User name = Choosed user
            Assert.AreEqual(ExpextedUserName, ActualUserName, "User name = Choosed user");

            // Find LX People button on the bradcrumbs on the LX People/User page
            IWebElement Backbutton = m_driver.FindElement(By.XPath("//a[@href='/lxpeople']"));
            Backbutton.Click();

            // Find "Filter by button" 
            IWebElement FilterByDepartmentLX = m_driver.FindElement(By.XPath("//div[@role='button']"));
            var Actualbutton = FilterByDepartmentLX.Text;

            // Check that Filter by department is appears
            Assert.LessOrEqual(Expectedbutton, Actualbutton, "Check that Filter by department is appears");


        }

        [OneTimeTearDown]
        public void finishBrowser()
        {
            m_driver.Quit();
        }

    }

}
