﻿using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace TflJourneyPlanner.Hooks
{   

        [Binding]
        public class Hooks
        {
            private readonly IObjectContainer _objectContainer;
            private IWebDriver _driver;

            public Hooks(IObjectContainer objectContainer)
            {
                _objectContainer = objectContainer;
            }

            [BeforeScenario]
            public void InitializeWebDriver()
            {

                _driver = new ChromeDriver();
                _objectContainer.RegisterInstanceAs(_driver);
            }

            [AfterScenario]
            public void DisposeWebDriver()
            {

            _driver.Quit();
            }



        }
}