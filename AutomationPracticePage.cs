using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;

public class AutomationPracticePage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    public AutomationPracticePage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
    }

    // HUMAN-LIKE TYPING
    private void SlowType(IWebElement element, string text, int delayMs = 150)
    {
        foreach (char c in text)
        {
            element.SendKeys(c.ToString());
            Thread.Sleep(delayMs); 
        }
    }

    // DYNAMIC INPUT LOCATOR USING LABEL
    private IWebElement GetInputByLabel(string labelText)
    {
        var label = wait.Until(ExpectedConditions.ElementIsVisible(
            By.XPath($"//label[contains(text(), '{labelText}')]")
        ));

        string forAttr = label.GetAttribute("for");

        return wait.Until(ExpectedConditions.ElementIsVisible(By.Id(forAttr)));
    }

    // ------------ FIELD ACTIONS ------------

    public void FillFirstName(string name) =>
        SlowType(GetInputByLabel("First Name"), name);

    public void FillEmail(string email) =>
        SlowType(GetInputByLabel("Email"), email);

    public void FillMobile(string phone) =>
        SlowType(GetInputByLabel("Mobile"), phone);
}
