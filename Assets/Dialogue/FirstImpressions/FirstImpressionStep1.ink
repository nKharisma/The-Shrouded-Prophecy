-> FirstImpressionsQuestStart

EXTERNAL StartQuest(questID)
EXTERNAL AdvanceQuest(questID)
EXTERNAL CompleteQuest(questID)
EXTERNAL playSkillCheckUI()
VAR result = 0
VAR passedTraining = false
VAR duelPassed = false

=== FirstImpressionsQuestStart ===
"So, they finally think you're ready, huh?"
"If you're serious about this mission, you'll need to prove it first. The Captain is waiting for you."
* [Where is the Captain?] -> where_captain

= where_captain
"You'll find him in the training grounds near the capital. Try not to embarrass yourself."
* [Okay]
    ~ StartQuest("FirstImpressionsSO")
-> DONE

=== CaptainDialogue ===
"You've made it this far. That means they see potential in you. But, potential is not enough. We do not guess if you are worthy, you are tested."
<i>The Captain folds his arms, glaring at the MC critically.</i>
"Your mission requires more than just brute strength. You must have a sly tongue, a stone heart, and a quick mind."
"So, before I test you myself, make sure you don't embarrass yourself."
<i>The Captain gestures towards the worn-out training dummy</i>
* [Yes, sir.]
-> DONE

=== TrainingDummyDialogue ===
What would you like to do? 

+ [Train] <b>The Captain nods approvingly</b>
    ~ playSkillCheckUI()
    -> check_result

+ [Walk Away] 
{ passedTraining == true:
    <b>The Captain scoffs.</b>
    "Already think you're above this? Let's see if your skill matches your arrogance."
    -> DONE
- else: 
    <i>The Captain shakes their head.</i>
    "You're not leaving until you've shown me you can pass this. Try again."
    -> TrainingDummyDialogue
}

= check_result
<i>The Captain observes your training from afar.</i>

 {result == 1:
    ~ passedTraining = true
    "Not bad. Up to you if you'll go again."
    -> TrainingDummyDialogue
- else:
    "That was sloppy. Again."
    -> TrainingDummyDialogue
}

=== DuelWithCaptain ===
<i>The Captain stands firm, his helmet conceling any emotion.</i>
"You've made it this far, but that means nothing. Show <b>me</b> you're ready."

Are you prepared?
+ [Yes, I'm ready.]
    <i>The Captain tilts his head slightly.</i>
    "Then let's begin."
    ~ playSkillCheckUI()
    -> duel_result
+ [Wait, I need a moment.]
    <b>The Captain exhales sharply.</b>
    "Nerves? Fine. Just don't keep me waiting."
    ->DONE

= duel_result
<i>The Captain studies you, gauging your performance.<i>

{result == 1: 
    ~ duelPassed = true
    <b>The Captain steps back, lowering his weapon back down to his side</b>
    "Not bad. You might actually survive out there."
    -> DONE
- else: 
    <b>The Captain's blade is at your throat before you can react, the weight of failure sinking in as the combat exercise ends.</b>
    "If that was a real fight, you'd be dead. Again!"
    ->DuelWithCaptain
}
    
=== duel_passed ===
    "Alright. I think you've proven yourself...for now. Time for your first real mission."
    ~ CompleteQuest("FirstImpressionsSO")
-> DONE

