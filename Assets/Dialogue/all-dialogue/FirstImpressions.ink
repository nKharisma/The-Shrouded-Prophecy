=== FirstImpressionsQuestStart ===
"So, they finally think you're ready, huh?"
"If you're serious about this mission, you'll need to prove it first. The Captain is waiting for you inside by the tents."
* [Where is the Captain?] -> where_captain

= where_captain
"You'll find him in the training grounds near the capital. Try not to embarrass yourself."
* [Okay, I’ll try not to.]  
    <i>The soldier gives you a knowing look.</i>  
    "That’s what they all say. Good luck, kid."
    ~ StartQuest("FirstImpressionsSO")
   // ~ LoadSceneInGame("2", "-42.4", "15.3", "39.34")
-> DONE

=== CaptainDialogue ===
"You've made it this far. That means they see potential in you. But, potential is not enough. We do not guess if you are worthy, you are tested."
<i>The Captain folds his arms, glaring at the MC critically.</i>
"Your mission requires more than just brute strength. You must have a sly tongue, a stone heart, and a quick mind."
"So, before I test you myself, make sure you don't embarrass yourself."
<i>The Captain gestures towards the worn-out training dummy.</i>
* [Yes, sir. I’ll prove myself.]  
    <i>The Captain hums, clearly amused by your enthusiasm.</i>  
    "Good. Show me what you’ve got."
    * *[By the way, how does this work?]  
        <i>The Captain raises an eyebrow, clearly impatient but willing to help.</i>  
        "You’ll face off against challenges that test your ability to dodge and survive. Simple enough, right? The key here is to outlast the danger or avoid the attacks that come at you. Stay focused and react quickly."  
        "When the timer is up or you’ve avoided the worst, you pass. If your health is deplated, you’ll need to try again."

        <i>He gestures to the dummy again, eyes narrowing.</i>  
        "Now stop asking questions and start training."

-> DONE

=== TrainingDummyDialogue ===
What would you like to do? 

+ [Train] <b>The Captain nods approvingly.</b>  
    ~ playSkillCheckUI()  
    -> check_result

+ [Walk Away]  
{ passedTraining == true:  
    <b>The Captain scoffs.</b>  
    "Already think you're above this? Let’s see if your skill matches your arrogance."  
    -> DONE  
- else:  
    <i>The Captain shakes his head, an eyebrow raised.</i>  
    "You're not leaving until you've shown me you can pass this. Try again."  
    -> TrainingDummyDialogue  
}

= check_result  
<i>The Captain observes your training from afar.</i>

{result == 1:  
    ~ passedTraining = true  
    "Not bad. Up to you if you want to go again."  
    -> TrainingDummyDialogue  
- else:  
    "That was sloppy. Again, or you're going to be running back to base with your tail between your legs."  
    -> TrainingDummyDialogue  
}

=== DuelWithSir ===
<i>Sir Veyne stands casually, his arms crossed as he sizes you up.</i>
"Everyone gets pass the training dummy, but not everyone gets past me."
<i>He pushes off his sword and rests it on his shoulder, eyes glinting with amusement.</i>
"I’ll make this simple. You’re not getting to the Captain without proving you’re not a waste of my time."
What do you say?

 + [I'm ready. Let's do this.]
"Confidence. I like that. Let’s see if you’ve got skill to back it up."
~ playSkillCheckUI()
-> sir_duel_result

+ [Do we really have to fight?]
"Oh, come on. Don’t be dull. This is a rite of passage, kid. If you’re scared, I’ll try not to hit you too hard."
    ~ playSkillCheckUI()
    -> sir_duel_result

+ [You don’t look that tough.]
    "Oho? A fighter with some bite. I love breaking that confidence."
    He rolls his shoulders, stepping into position.
    "Don’t disappoint me now."
    ~ playSkillCheckUI()
    -> sir_duel_result

= sir_duel_result
<i>Sir Veyne steps back, pondering your performance.</i>

{result == 1:
    ~  sirDuelPassed = true
"Hah! You’ve got some moves. Maybe you’re not a lost cause after all."
<i>He grins, rubbing his knuckles absentmindedly before cracking his neck one last time.</i>
"Alright, I’ll let you go make a fool of yourself in front of the Captain. Try not to die, yeah?"
-> DONE

- else:
"Oof. That was embarrassing. Try again, unless you’d rather quit now."
<i>He crosses his arms again, shaking his head with mock disappointment.</i>
"Come on, recruit, don’t make me feel bad for hitting you. Again."
-> DuelWithSir
}

=== DuelWithCaptain ===  
<i>The Captain stands firm, his helmet concealing any emotion.</i>  
"You've made it this far, but that means nothing. Show <b>me</b> you're ready."

Are you prepared?  
+ [Yes, I'm ready.]  
    <i>The Captain tilts his head slightly, clearly sizing you up.</i>  
    "Then let's begin."  
    ~ playSkillCheckUI()  
    -> duel_result  
+ [Wait, I need a moment.]  
    <b>The Captain exhales sharply, his patience thinning.</b>  
    "Nerves? Fine. Just don't keep me waiting."  
    -> DuelWithCaptain

= duel_result  
<i>The Captain studies you, gauging your performance.</i>

{result == 1:  
    ~ captainDuelPassed = true  
    <b>The Captain steps back, lowering his weapon back down to his side.</b>  
    "Not bad. You might actually survive out there. Don't get too cocky, though."  
    -> DONE  
- else:  
    <b>The Captain's blade is at your throat before you can react, the weight of failure sinking in.</b>  
    "If that was a real fight, you'd be dead. Again! Show me you can do better!"  
    -> DuelWithCaptain  
}

=== duel_passed ===  
    "Alright. I think you've proven yourself... for now. Time for your first real mission."  
    ~ CompleteQuest("FirstImpressionsSO")  
    "Don't get comfortable, this is nothing like the real thing."  
<i>This is the end of the tutorial, you are free to explore the world and interact with other NPCs, we know that it might not be a lot of content but we hope you enjoyed the demo and cannot wait to show you the final project!</i>
-> DONE

