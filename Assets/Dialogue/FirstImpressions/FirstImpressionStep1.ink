-> FirstImpressionsQuestStart

EXTERNAL StartQuest(questID)
EXTERNAL AdvanceQuest(questID)
EXTERNAL FinishQuest(questID)

=== FirstImpressionsQuestStart ===
So, they finally think you're ready, huh?
If you're serious about this mission, you'll need to prove it first. The Captain is waiting for you.
* [Where is the Captain?] -> where_captain

= where_captain
You'll find him in the training grounds near the capital. Try not to embarrass yourself.
* [Okay]
    ~ StartQuest("FirstImpressionsSO")
-> DONE

=== CaptainDialogue ===
You've made it this far. That means they see potential in you. But, potential is not enough. We do not guess if you are worthy, you are tested. 
<i>The Captain folds his arms, glaring at the MC critically.</i>
Your mission requires more than just brute strength. You must have a sly tongue, a stone heart, and a quick mind.
So, before I test you myself, make sure you don't embarrass yourself.
<i>The Captain gestures towards the worn-out training dummy</i>
* [Yes, sir.]
-> DONE

