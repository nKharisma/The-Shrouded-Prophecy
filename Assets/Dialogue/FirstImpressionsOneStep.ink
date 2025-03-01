EXTERNAL StartQuest(questID)
EXTERNAL AdvanceQuest(questID)
EXTERNAL FinishQuest(questID)

//quest names
VAR FirstImpressionsID = "FirstImpressionsSO"

//quest states
VAR FirstImpressionsState = "Can_Start"

=== FirstImpressionsQuestStart ===
{   FirstImpressionsState :
    - "Requirements_Not_Met": -> requirementsNotMet
    - "Can_Start": -> canStart
    - "In_Progress": -> inProgress
    - "Can_Complete": -> canComplete
    - "Completed": -> completed
    - else: -> END
}

= requirementsNotMet
Come back when you have gained more trust.
-> END

= canStart
Before you leave for the town, we need to ensure you're truly ready. 
Go talk to the captain to get your final test. He's waiting for you by the tents inside the city.
* [Okay]
    ~ StartQuest("FirstImpressionsSO")
-> END

= inProgress
Did you talk to the captain yet? 
-> END

= canComplete
You've talked to the captain? Then you must go and practice your skills on the training dummy.
-> END

= completed
Thanks for talking to the captain.
-> END


Before you leave for the town, we need to ensure you're truly ready. 
Go talk to the captain to get your final test. He's waiting for you by the tents inside the city.
* [Okay]
    ~ StartQuest("FirstImpressionsSO")
- -> END