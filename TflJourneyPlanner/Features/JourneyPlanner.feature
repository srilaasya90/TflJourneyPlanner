Feature: JourneyPlanner
Scenario: Plan a valid journey from Leicester Square to Covent Garden
    Given user open the TfL Journey Planner
    When user enter '<From>' and '<To>'
    And user submit the journey
    Then user should see valid walking and cycling times for the journey

  Examples: 
        | From     | To |  
        |    Leicester Square Underground Station  | Covent Garden Underground Station | 
  

  Scenario: Edit preferences to least walking route and update the journey and view access information at Covent Garden
    Given user open the TfL Journey Planner
    When user enter '<From>' and '<To>'
    And user submit the journey   
    When user edit the journey preferences to least walking
    Then the journey time should be updated
    When user click View Details
    Then user should see access information for Covent Garden Underground Station

     Examples: 
        | From     | To |  
        |    Leicester Square Underground Station  | Covent Garden Underground Station | 



  Scenario: Invalid journey with incorrect locations
    Given user open the TfL Journey Planner
    When user enter '<From>' and '<To>'    
    Then error message should be displayed
     Examples: 
        | From     | To |  
        |   @£%$&  | @£%$& | 

  Scenario: Empty journey submission
    Given user open the TfL Journey Planner
    When user submit the journey without entering any locations
    Then no journey should be planned


    #Additional Scenarios - functional

    Scenario: View alternative route suggestions for a journey
  Given user open the TfL Journey Planner
 When user enter '<From>' and '<To>';';ooooooooooooooooooooo'/p./



  And user submit the journey
  Then user should see multiple route options available for the journey
  And each route should display different travel times and modes (e.g., bus, tube)

  Examples: 
		| From     | To |  
		|    Slough  | Covent Garden Underground Station |


        Scenario: Plan a journey with the fewest transfers
  Given user open the TfL Journey Planner
 When user enter '<From>' and '<To>'
  And user submit the journey
  And user choose the option for the fewest transfers in journey preferences
   Then user should see a journey result with the minimum number of transfers
  And the journey time should reflect the fewest-transfer route

  
  Examples: 
		| From     | To |  
		|    Leicester Square Underground Station  | Slough |

        Scenario: Access "Plan a journey" form with keyboard-only navigation
  Given user open the TfL Journey Planner
  When user navigate through the "Plan a journey" form using only the keyboard
  Then user should be able to access and enter the origin and destination fields
  And user should be able to select journey options using the keyboard
  And user should be able to submit the journey without using a mouse



   #Additional Scenarios - Non-functional
   Scenario: Verify the responsiveness of the Journey Planner widget on mobile devices
  Given user open the TfL Journey Planner
  When user resize the browser window to a mobile screen resolution
  Then the Journey Planner widget should be displayed correctly without horizontal scrolling
  And the input fields and buttons should be accessible on the mobile screen
  And user should be able to plan a journey successfully on the mobile view