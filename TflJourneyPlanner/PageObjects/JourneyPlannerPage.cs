using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using TflJourneyPlanner.Models;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Reflection.Metadata;
using System.IO;


namespace TflJourneyPlanner.PageObjects
{
    public class JourneyPlannerPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;


        public JourneyPlannerPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

      
        // Method to wait for the username field to be visible
        public void WaitForElementToBeVisible(IWebElement element, TimeSpan timeout)
        {
            _wait.Until(d =>
            {
                try
                {
                    return element.Displayed; // Check if the element is displayed
                }
                catch (StaleElementReferenceException)
                {
                    return false; // Keep waiting if the element is stale
                }
            });
        }


        public string GetTextonElementByJavaScript(string jsQuery)
        {
            var jsExecutor = (IJavaScriptExecutor)_driver;
            jsExecutor.ExecuteScript("return document.readyState").Equals("complete");
            return (string)jsExecutor.ExecuteScript(jsQuery);
           
           
        }
        public void ClickElementByJavaScript(string script)
        {
            var jsExecutor = (IJavaScriptExecutor)_driver;           
            jsExecutor.ExecuteScript(script);
        }

     
        private IWebElement originInput => _driver.FindElement(By.Id("InputFrom"));

        private IWebElement destinationInput => _driver.FindElement(By.Id("InputTo"));
        private IWebElement SubmitButton => _driver.FindElement(By.Id("plan-journey-button"));
        private IWebElement AcceptCookies => _driver.FindElement(By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll"));

        private IWebElement InputFromError => _driver.FindElement(By.Id("InputFrom-error"));
        private IWebElement InputToError => _driver.FindElement(By.Id("InputTo-error"));
        private IWebElement CyclingandWalkingResults => _driver.FindElement(By.CssSelector(".cycling-walking-only-information"));
        private IWebElement EditJourney => _driver.FindElement(By.CssSelector(".edit-journey"));
        private IWebElement timeElement => _driver.FindElement(By.XPath("//div[@class='journey-time no-map']"));

        private IWebElement NoJourneyPlanned => _driver.FindElement(By.CssSelector(".field-validation-errors"));
        private IWebElement EditPreferences => _driver.FindElement(By.XPath("//button[contains(text(), 'Edit preferences')]"));
      
        private IWebElement LeastWalking => _driver.FindElement(By.XPath("//input[@type='radio' and @value='leastwalking' and @id='JourneyPreference_2']"));
        private IWebElement UpdateJourney => _driver.FindElement(By.XPath("(//input[@type='submit' and @value='Update journey'])[2]"));
        private IWebElement viewDetailsButton => _driver.FindElements(By.XPath("//button[@data-show-text='View details']")).FirstOrDefault()!;
        private IWebElement UpStarirs => _driver.FindElements(By.XPath("//a[@class='up-stairs tooltip-container']"))[0];
        private IWebElement UpLift => _driver.FindElements(By.XPath("//a[@class='up-lift tooltip-container']"))[0];
        private IWebElement LevelWalkway => _driver.FindElements(By.XPath("//a[@class='level-walkway tooltip-container']"))[0];

        private IWebElement UpdatedJourneytime => _driver.FindElement(By.XPath("//div[@class='journey-time no-map']"));
        public void Cookies()
        {
            AcceptCookies.Click();
        }
        public void EnterOriginAndDestination(string origin, string destination)
        {

            WaitForElementToBeVisible(originInput, TimeSpan.FromSeconds(2));
            originInput.SendKeys(origin);
            System.Threading.Thread.Sleep(2000); // Allow autocomplete suggestions to load
            originInput.SendKeys(Keys.ArrowDown);
            originInput.SendKeys(Keys.Enter);
           
            destinationInput.SendKeys(destination);
            System.Threading.Thread.Sleep(2000); // Allow autocomplete suggestions to load
            destinationInput.SendKeys(Keys.ArrowDown);
            destinationInput.SendKeys(Keys.Enter);

        }

        public void SubmitForm()
        {
            WaitForElementToBeVisible(SubmitButton, TimeSpan.FromSeconds(2));
            SubmitButton.Click();
        }


        public WalkingAndCyclingResult GetWalkingandCyclingJourneyResults()
        {
        
            WalkingAndCyclingResult walkingAndCyclingResult = new WalkingAndCyclingResult()
            {
                WalkDuration = GetTextonElementByJavaScript($"return document.querySelectorAll('.col2.journey-info strong')[{1}].textContent;"),
                CyclingDuration = GetTextonElementByJavaScript($"return document.querySelectorAll('.col2.journey-info strong')[{0}].textContent;")
               

            };
           return walkingAndCyclingResult;
        }

       
        public ErrorMessage GetErrorMessage()
        {
            ErrorMessage errorMessage = new ErrorMessage()
            {
                InputFromError = InputFromError.Text,
                InputToError = InputToError.Text
            };
            return errorMessage;
        }


        public string GetErrorMessageWhenInavlidInput()
        {
           
         
            WaitForElementToBeVisible(NoJourneyPlanned, TimeSpan.FromSeconds(5));

            string errorMessage = NoJourneyPlanned.Text;

            return errorMessage;
        }

        public void EditJourneyPreferences()
         {


            WaitForElementToBeVisible(EditPreferences, TimeSpan.FromSeconds(2));
            ClickElementByJavaScript($"document.querySelector('.toggle-options.more-options').click();");
            ClickElementByJavaScript($"document.getElementById('JourneyPreference_2').checked = true;");
            UpdateJourney.Click();
            //string xpath = "//input[@type='submit' and '@value='Update journey' and not(@id='plan-journey-button')]";
            //ClickElementByJavaScript($"var element = document.evaluate(\"{xpath}\", document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;" +
            //            "if (element) { element.click(); }");

        }
        public string UpdatedJourneyTime()
        {
                
            return timeElement.Text;
        }

        public void ViewDetails()
        {
            WaitForElementToBeVisible(viewDetailsButton, TimeSpan.FromSeconds(2));
            viewDetailsButton.Click();
          
        }

        public bool AccessInfo()
        {
            bool accessInfo = false;
            WaitForElementToBeVisible(UpStarirs, TimeSpan.FromSeconds(2));
            if(UpStarirs.Displayed && UpLift.Displayed & LevelWalkway.Displayed)
            {
                accessInfo = true;
            }
            return accessInfo;
            
        }
               
    }
}
