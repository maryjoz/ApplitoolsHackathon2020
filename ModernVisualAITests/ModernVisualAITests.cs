using System;
using System.Configuration;
using Applitools;
using Applitools.Selenium;
using Applitools.VisualGrid;
using ApplitoolsHackathon2020.Pages;
using NUnit.Framework;
using OpenQA.Selenium;

namespace ApplitoolsHackathon2020.ModernVisualAITests
{
    [TestFixture]
    [Category("ModernVisualAI")]
    public class ModernVisualAITests
    {
        #region Private Fields

        private static readonly string _url = ConfigurationManager.AppSettings["V1"];
        private readonly string myEyesServer = "https://eyes.applitools.com/";
        private readonly string appName = "Applitools Hackathon 2020";
        private readonly string batchName = "UFG Hackathon";
        private int concurrentSessions = 10;
        private EyesRunner _runner;
        private Eyes _eyes;
        private IWebDriver _driver;
        private ShoeSearch _shoeSearch;
        private Applitools.Selenium.Configuration _suiteConfig;

        #endregion Private Fields

        #region Public Methods

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            //Initialize the Runner
            _runner = new VisualGridRunner(concurrentSessions);

            //Create a configuration object
            _suiteConfig = new Applitools.Selenium.Configuration();

            /// Add Visual Grid browser configurations
            _suiteConfig
                .AddBrowser(1200, 700, BrowserType.CHROME)
                .AddBrowser(1200, 700, BrowserType.FIREFOX)
                .AddBrowser(1200, 700, BrowserType.EDGE_CHROMIUM)
                .AddBrowser(768, 700, BrowserType.CHROME)
                .AddBrowser(768, 700, BrowserType.FIREFOX)
                .AddBrowser(768, 700, BrowserType.EDGE_CHROMIUM)
                .AddDeviceEmulation(DeviceName.iPhone_X, Applitools.VisualGrid.ScreenOrientation.Portrait);

            // Set up default Eyes configuration values
            _suiteConfig
                .SetApiKey(Environment.GetEnvironmentVariable("APPLITOOLS_API_KEY", EnvironmentVariableTarget.User))
                .SetBatch(new BatchInfo(batchName))
                .SetServerUrl(myEyesServer)
                .SetAppName(appName);
        }

        [SetUp]
        public void SetUp()
        {
            // Create the Eyes instance for the test and associate it with the runner
            _eyes = new Eyes(_runner);

            // Set the configuration values we set up in OneTimeSetUp
            _eyes.SetConfiguration(_suiteConfig);

            // Create a WebDriver for the test
            _driver = new OpenQA.Selenium.Chrome.ChromeDriver();

            // intantiate page model
            _shoeSearch = new ShoeSearch(_driver);
        }

        [Test]
        [Category("ModernVisualAI")]
        [TestCase(TestName = "Task 1"), Order(1)]
        public void CrossDeviceElementTest()
        {
            // Update the Eyes configuration with test specific values
            Applitools.Selenium.Configuration testConfig = _eyes.GetConfiguration();
            testConfig.SetTestName(TestContext.CurrentContext.Test.Name);
            _eyes.SetConfiguration(testConfig);

            // Open eyes
            _eyes.Open(_driver);

            // open the application under test
            _shoeSearch.Open(_url);

            // take a screenshot
            _eyes.CheckWindow("Cross-Device Elements Test");
        }

        [Test]
        [Category("ModernVisualAI")]
        [TestCase(TestName = "Task 2"), Order(2)]
        public void FilterResultsTest()
        {
            // Update the Eyes configuration with test specific values
            Applitools.Selenium.Configuration testConfig = _eyes.GetConfiguration();
            testConfig.SetTestName(TestContext.CurrentContext.Test.Name);
            _eyes.SetConfiguration(testConfig);

            // Open eyes
            _eyes.Open(_driver);

            // open the application under test
            _shoeSearch.Open(_url);

            // filter for black shoes
            _shoeSearch.BlackFilter.Click();
            _shoeSearch.FilterButton.Click();

            //Take screenshot
            _eyes.CheckWindow("Filter Results");
        }

        [Test]
        [Category("ModernVisualAI")]
        [TestCase(TestName = "Task 3"), Order(3)]
        public void ProductDetailsTest()
        {
            // Update the Eyes configuration with test specific values
            Applitools.Selenium.Configuration testConfig = _eyes.GetConfiguration();
            testConfig.SetTestName(TestContext.CurrentContext.Test.Name);
            _eyes.SetConfiguration(testConfig);

            // Open eyes
            _eyes.Open(_driver);

            // open the application under test
            _shoeSearch.Open(_url);

            // filter for black shoes
            _shoeSearch.BlackFilter.Click();
            _shoeSearch.FilterButton.Click();

            // click on the first black shoe
            _shoeSearch.FirstBlackShoe.Click();

            // Take screenshot
            _eyes.CheckWindow("Product Details Test");
        }

        [TearDown]
        public void AfterEach()
        {
            // check if an exception was thrown
            Boolean testPassed = TestContext.CurrentContext.Result.Outcome.Status != NUnit.Framework.Interfaces.TestStatus.Failed;
            if (testPassed)
            {
                // Close the Eyes instance
                _eyes.CloseAsync();
            }
            else
            {
                // There was an exception so the test may be incomplete - abort the test
                _eyes.AbortIfNotClosed();
            }

            _driver.Quit();
        }

        [OneTimeTearDown]
        public void AfterTestSuite()
        {
            // Wait until the test results are available and retrieve them
            TestResultsSummary allTestResults = _runner.GetAllTestResults(false);
            foreach (var result in allTestResults)
            {
                HandleTestResults(result);
            }
        }

        #endregion Public Methods

        #region Private Methods

        private void HandleTestResults(TestResultContainer summary)
        {
            Exception ex = summary.Exception;
            if (ex != null)
            {
                TestContext.WriteLine("System error occured while checking target.");
            }
            TestResults result = summary.TestResults;
            if (result == null)
            {
                TestContext.WriteLine("No test results information available");
            }
            else
            {
                TestContext.WriteLine(
                    "AppName = {0}, testname = {1}, Browser = {2},OS = {3} viewport = {4}x{5}, matched = {6},mismatched = {7}, missing = {8}, aborted = {9}\n",
                    result.AppName,
                    result.Name,
                    result.HostApp,
                    result.HostOS,
                    result.HostDisplaySize.Width,
                    result.HostDisplaySize.Height,
                    result.Matches,
                    result.Mismatches,
                    result.Missing,
                    (result.IsAborted ? "aborted" : "no"));
            }
        }

        #endregion Private Methods
    }
}