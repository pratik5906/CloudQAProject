using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

[TestFixture]
public class AutomationTests
{
    private IWebDriver driver;
    private WebDriverWait wait;
    private AutomationPracticePage page;

    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl("https://app.cloudqa.io/home/AutomationPracticeForm");

        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

        // Detect & switch to iframe dynamically
        var frames = driver.FindElements(By.TagName("iframe"));
        for (int i = 0; i < frames.Count; i++)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(i);

            if (driver.FindElements(By.XPath("//label[contains(text(),'First Name')]")).Count > 0)
                break;
        }

        page = new AutomationPracticePage(driver);
    }

    [Test]
    public void Test_FirstName_Email_Mobile()
    {
        page.FillFirstName("Pratik");
        page.FillEmail("test@example.com");
        page.FillMobile("9876543210");

        Assert.Pass("3 dynamic field tests completed successfully");
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
}
