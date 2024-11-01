using NUnit.Framework;
using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using System;
using TechTalk.SpecFlow;
using TflJourneyPlanner.Models;
using TflJourneyPlanner.PageObjects;

namespace TflJourneyPlanner.StepDefinitions
{
    [Binding]
    public class JourneyPlannerStepDefinitions
    {


        private readonly IWebDriver _driver;
        private readonly JourneyPlannerPage _journeyPlannerPage;

        public JourneyPlannerStepDefinitions(IWebDriver driver, JourneyPlannerPage journeyPlannerPage)
        {
            _driver = driver;
            _journeyPlannerPage = journeyPlannerPage;
        }
        [Given(@"user open the TfL Journey Planner")]
        public void GivenUserOpenTheTfLJourneyPlanner()
        {
            _driver.Navigate().GoToUrl("https://tfl.gov.uk/plan-a-journey");
        }

        [When(@"user enter '([^']*)' and '([^']*)'")]
        public void WhenUserEnterAnd(string  origin, string destination)
        {
            _journeyPlannerPage.Cookies();
            _journeyPlannerPage.EnterOriginAndDestination(origin, destination);
           
        }

       

        [When(@"user submit the journey")]
        public void WhenUserSubmitTheJourney()
        {
            _journeyPlannerPage.SubmitForm();
        }

        [Then(@"user should see valid walking and cycling times for the journey")]
        public void ThenUserShouldSeeValidWalkingAndCyclingTimesForTheJourney()
        {
            var walkingAndCyclingResult = _journeyPlannerPage.GetWalkingandCyclingJourneyResults();
            Assert.That(walkingAndCyclingResult.WalkDuration, Is.EqualTo("6"));
            Assert.That(walkingAndCyclingResult.CyclingDuration, Is.EqualTo("1"));
        }

        [Given(@"user have planned a journey from '([^']*)' to '([^']*)'")]
        public void GivenUserHavePlannedAJourneyFromTo(string p0, string p1)
        {
            
        }

        [When(@"user edit the journey preferences to least walking")]
        public void WhenUserEditTheJourneyPreferencesToLeastWalking()
        {
            _journeyPlannerPage.EditJourneyPreferences();
        }

        [Then(@"the journey time should be updated")]
        public void ThenTheJourneyTimeShouldBeUpdated()
        {
            
            StringAssert.IsMatch(@"Total time:\s*\d+mins", _journeyPlannerPage.UpdatedJourneyTime(), "Journey time text does not match the expected pattern.");
           
        }

       

        [When(@"user click View Details")]
        public void WhenUserClickViewDetails()
        {
            _journeyPlannerPage.ViewDetails();
        }


        [Then(@"user should see access information for Covent Garden Underground Station")]
        public void ThenUserShouldSeeAccessInformationForCoventGardenUndergroundStation()
        {
            Assert.That(_journeyPlannerPage.AccessInfo(), Is.True);
            

        }

        [When(@"user try to submit the journey")]
        public void WhenUserTryToSubmitTheJourney()
        {
            throw new PendingStepException();
        }

        [Then(@"no journey should be planned")]
        public void ThenNoJourneyShouldBePlanned()
        {
            var errorMessage = _journeyPlannerPage.GetErrorMessage();
            Assert.That(errorMessage.InputFromError, Contains.Substring("The From field is required."));
            Assert.That(errorMessage.InputToError, Contains.Substring("The To field is required."));

        }

        [When(@"user submit the journey without entering any locations")]
        public void WhenUserSubmitTheJourneyWithoutEnteringAnyLocations()
        {
            _journeyPlannerPage.Cookies();
            _journeyPlannerPage.SubmitForm();
        }

        [Then(@"error message should be displayed")]
        public void ThenErrorMessageShouldBeDisplayed()
        {
            var errorMessage = _journeyPlannerPage.GetErrorMessageWhenInavlidInput();
            Assert.That(errorMessage, Contains.Substring("Sorry, we can't find a journey matching your criteria"));
        }

    }
}
