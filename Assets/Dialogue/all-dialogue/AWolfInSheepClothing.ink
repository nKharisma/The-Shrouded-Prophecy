=== AWolfInSheepClothingStart ===
"You've proven yourself in training, but the real test begins now"
"Your mission is simple. Travel to the town, infiltrate, and gather intel. Blend in, observe, and report back."
<i>The Captain's gaze is sharp, assessing your readiness.</i>
"This is no simple task. You are not just a soldier, you are a shadow. Understand?"
* [Understand] -> accept_mission

= accept_mission
"Good. You leave immediately."
<i>The Captain hands you a small insignia, a token to prove your status if needed.</i>
"Stay focused. The fate of our mission rests on your shoulders."
~ StartQuest("AWolfInSheepClothingSO")
-> LeaveNowOrLater

=== LeaveNowOrLater ===
<i>The Captain watches you carefully.</i>
"You're still here? Are you having second thoughts, or do you just like my company?"
 * [I'm ready to go.]
    "Good. Now, get going and don't fail."
    ~ LoadSceneInGame("2", "-42.4", "15.3", "39.34")
    -> DONE
 * [Prepare first] -> DONE
 
 //figure out how to get LeaveNowOrLater to play in a loop until player decides to leave

=== talk_to_companion ===
<i>You approach the frightened figure just as a slime oozes closer in the distance.</i>
<i>Their eyes widen, and a soft glow pulses from their hands.</i>
"Wait—don’t get too close! They’re everywhere!"
    * [Are you okay?] 
    "N-No! I mean… I’m not hurt, but—I can’t fight these things!"
    "I can heal. That’s all I’ve ever been good at. I tried to push them away with my magic, but it just—won’t work like that..."
    ** [You have magic—use it!] 
        "It’s not like that! Healing doesn’t hurt. I’d have to twist it… and I’m scared of what might happen if I try."
    -> DONE
    ** [It’s okay. Stay behind me.]
        "You’d really do that...? Just—be careful, okay?"
    -> DONE
    * [What are you doing out here?]
    "I was just trying to gather herbs. This area’s usually quiet—I didn’t expect slimes!"
    "I thought… maybe if I found something strong enough, I could help people back in the village. But I didn't think it through..."
    ** [Stay calm. I’ll take care of it.]
        "You’re serious? Okay—okay. I’ll try not to panic."
        ->DONE

=== slime_battle ===
<i>The slimes begin to close in, bubbling aggressively as they slither toward you both.</i>
<i>The companion backs away, visibly shaking but staying close behind you.</i>
<i>You step forward, ready to fight.</i>
~ playSkillCheckUI()
-> slime_battle_result

= slime_battle_result
{result == 1:
    "You did it! Oh, thank you, thank you! I thought I was done for!"
    <i>The companion dusts themselves off, still shaky but clearly relieved.</i>
    "You’re a traveler, right? You’re heading to the town? I… I’d feel a lot safer if I traveled with you."
    "I tried to be brave. I really did. But… healing doesn’t stop monsters, does it?"
<i>She offers a small, apologetic smile, but there's a flicker of resolve beneath it.</i>
"Still... I want to help. I don’t know how yet, but if you’ll have me, I’ll learn. You seem experienced with these things."
    + [You don't need to fight. Just be there.]
    //trust gained
    ~ nice = true
    -> DONE
    + [If you're going to follow me, you'll have to toughen up.]
    //trust lost
    ~ nice = false
    -> DONE

- else:
    "No! They’re getting closer—please, you have to try again!"
    -> slime_battle
}

=== recruit_mira ===
{nice == true:
<i>Her eyes widen slightly, surprised by your reassurance. A soft warmth blooms across her face.</i>
    "Thank you. I'll try not slow you down."
    <i>She walks a little closer, shoulders less tense.</i>
    "Maybe you're not just strong. Maybe you're kind, too."
    <i>Mira has started to trust you more.</i>
 - else:
//if trust is lost
<i>Mira’s expression falters, and she looks down, the glow in her hands flickering erratically.</i>
"I...I understand. I'll do my best."
<i>She falls in step behind you, quiet for a while.</i>
<i>Mira's trust in you has lessened slightly.</i>
}
<i>Your new companion now walks beside you, still uncertain, but no longer afraid.</i>
"Whatever happens next, I'll be ready."
<i>The path ahead winds gently, the swamp slowly thinning.In the distance, far behind you, the city fades from view—while ahead, gentle hills hint at something more welcoming.</i>
"You know... the town I come from—it’s nothing like the city."
"People take care of each other there. No one’s left behind, even if they’re not strong or useful right away."
<i>She pauses, brushing a branch out of the way as you both walk.</i>
"The city's been choking off our supply lines. They don’t say it out loud, but we know. So we’ve had to...adapt. Take risks."
"This trip—it was supposed to be coordinated, careful. But I thought, maybe if I could do it alone... maybe I’d prove I could carry more than just healing herbs."
<i>Her steps slow slightly.</i>
"Turns out, bravery’s easier to talk about when you’re not surrounded by slimes."
* [You were brave to try. That means something.]
//trust gained 
"Thank you… not many people see it that way. They just see recklessness. But I just wanted to prove I could help."
<i>She looks down at her hands, then back at you with a steadier gaze.</i>
"I think… you might really get it. That means a lot."
->DONE
* [So you ignored the plan and almost got yourself killed?]
// trust down
"...I didn’t mean to make things worse. I thought I could handle it."
<i>She folds her arms, the glow in her fingers dimmer now.</i>
"I just… wanted to do something right for once."
<i>She walks a little ahead now, quieter than before.</i>

As you both get closer to the town, {nice: comfortable} {not nice: uncomfortable} silence.
<i>Through the fading mist, the first signs of the town come into view—wooden rooftops, flickering lanterns, the faint sound of music.</i>
//transition to town
->END