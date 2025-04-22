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
~ StartQuest("AWolfInSheepsClothingSO")
-> LeaveNowOrLater

=== LeaveNowOrLater ===
<i>The Captain watches you carefully.</i>
"You're still here? Are you having second thoughts, or do you just like my company?"
 * [I'm ready to go.]
    "Good. Now, get going and don't fail."
    -> DONE
 * [Prepare first] -> DONE
 
=== talk_to_companion ===
<i>You approach the frightened figure just as a slime oozes closer in the distance.</i>
<i>Their eyes widen, and a soft glow pulses from their hands.</i>
"Wait—don’t get too close! They’re everywhere!"

* [Are you okay?] 
"N-No! I mean… I’m not hurt, but—I can’t fight these things!"
"I can heal. That’s all I’ve ever been good at. I tried to push them away with my magic, but it just—won’t work like that..."
    * * [It’s okay. Stay behind me.]
        "You’d really do that...? Just—be careful, okay."
        
        * * * [What are you doing out here?]
            "I was just trying to gather herbs. This area’s usually quiet—I didn’t expect slimes!"
            "I thought… maybe if I found something strong enough, I could help people back in the village. But I didn't think it through..."

            * * * * [Stay calm. I’ll take care of it.]
                "You’re serious? Okay—okay. I’ll try not to panic."
    -> DONE


=== slime_battle ===
<i>The slimes begin to close in, bubbling aggressively as they slither toward you both.</i>
<i>The companion backs away, visibly shaking but staying close behind you.</i>
<i>You step forward, ready to fight.</i>
~ playSkillCheckUI()
-> slime_battle_result

= slime_battle_result
<i>The companion watches you nervously, holding their breath...</i>

{result == 1:
    "You did it! Oh, thank you, thank you! I thought I was done for!"
    <i>The companion dusts themselves off, still shaky but clearly relieved.</i>
    "You’re a traveler, right? You’re heading to the town? I… I’d feel a lot safer if I traveled with you."
    "I tried to be brave. I really did. But… healing doesn’t stop monsters, does it?"
<i>She offers a small, apologetic smile, but there's a flicker of resolve beneath it.</i>
"Still... I want to help. I don’t know how yet, but if you’ll have me, I’ll learn. You seem experienced with these things."
-> continue_choices
- else:  
    "No! They’re getting closer—please, you have to try again!"
    -> slime_battle
}

= continue_choices 
 * [You don't need to fight. Just be there.]
    ~ TrustGained("5")
    ~ nice = true
    -> DONE
    * [If you're going to follow me, you'll have to toughen up.]
    ~ TrustLost("5")
    ~ nice = false
    -> DONE

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
"Thank you… not many people see it that way. They just see recklessness. But I just wanted to prove I could help."
<i>She looks down at her hands, then back at you with a steadier gaze.</i>
"I think… you might really get it. That means a lot."
    ~ nice = true
    ~ TrustGained("10")
->DONE
* [Next time, tell someone. Bravery doesn't mean going alone.]
//nothing
<i>She nodes slowly, taking the words in.</i>
"You're right, I just...didn't want to be seen as weak."
~ TrustGained("5")
~ nice = true
->DONE
* [So you ignored the plan and almost got yourself killed?]
// trust down
"...I didn’t mean to make things worse. I thought I could handle it."
<i>She folds her arms, the glow in her fingers dimmer now.</i>
"I just… wanted to do something right for once."
<i>Her face tightens. She turns away slightly.</i>
~ TrustLost("5")
~ nice = false
->DONE

=== town_leave ===
As you both get closer to the town, {nice == true: comfortable} {nice == false: uncomfortable} silence.
<i>Through the fading mist, the first signs of the town come into view—wooden rooftops, flickering lanterns, the faint sound of music.</i>
//transition to town
->DONE

=== town_arrival ===
{nice == true:
"Alright… welcome to my home."
<i>She gestures ahead, walking slowly with a soft smile.</i>
Mira:
"I know it doesn’t look like much, but everything here was built by someone who cared."
"It’s ours. We built it from nothing, away from the city’s rules. People fight hard to keep it safe."
<i>She turns to you, her eyes searching.</i>
"Let’s head to the square. I'll show you around first."
- else:
"I’ll show you around…but just so you know—people here remember things. Words, too."
<i>She glances at you, not with anger, but with a quiet kind of disappointment.</i>
"Trust isn't given lightly here, not after everything this town’s been through."
<i>She pauses a moment, then nods and starts walking, her steps slow enough for you to catch up.</i>
}
->DONE

=== town_square ===
<i>You and Mira step into the town center. It’s quiet, but full of life—the wind blows gently, a merchant arranges jars of preserved herbs, and someone is fixing a broken sign with bits of scrap metal.</i>

"This is the Town Square. We don’t have monuments or fancy houses, but... it’s ours."
"Most of this was built from the ruins outside the city. Broken things they threw away—we gave them purpose again."
* [Looks like you’re doing more than surviving.]
    "We try. Hope’s hard to come by out here, but we find ways to hold onto it."
    ~ TrustGained("5")
    -> continue
* [Doesn’t the city get suspicious when all this goes up?]
    Mira: "They don’t know. Or maybe they don’t care enough to look this far out. We’re not supposed to exist."
    //neutral
    -> continue
* [This place is a ticking time bomb. The city <b>will</b> find it.]
    Mira: "...Maybe. But until they do, we’ll keep helping each other. That’s all we can do."
    ~ TrustLost("5")
    -> continue
= continue
<i>She walks toward the makeshift stands in the center, her fingers brushing its worn wood.</i>
"They say the old city had leaders from every race—until the fae decided unity wasn’t efficient enough."
"We’re what’s left of that dream. Not rebels. Just people who still believe we all deserve a place to belong."
<i>A bell rings in the distance—two short tones. A sign that midday is ending.</i>
"Come on. There’s still more to see."
->DONE

=== hunters_tent ===
<i>You and Mira pass a cluster of patched tents, wind-worn and half-camouflaged among the trees. One stands slightly apart—leaner, sturdier, with arrows bundled outside and the scent of dried meat hanging in the air.</i>
"This is the hunter’s tent. We don’t have the soil or space for real gardens, so most of our food comes from the woods."
"Aspen—our best hunter—leads the foraging runs. Sharp eyes, sharper instincts. They’ve kept the town fed more times than I can count."
* [So the city even controls your food supply?]
    Mira: "Yeah. Used to be we could trade with old routes, barter what little we had... but lately, the city’s been tightening things. Like they want us to starve quiet."
** [That sounds... unsustainable.]
"It is. That’s why people like Aspen take risks. Too many risks, sometimes."
<i>She glances at the tent’s flap, brow furrowed.</i>
"They’re not back yet, actually. They left before I did, said they found signs of something unusual out in the deeper woods. Should’ve returned by now..."
*** [You think something happened to them?]
"If it did, Aspen can handle it. But... I don’t like the silence."
<i>She sighs and turns away, motioning for you to follow.</i>
"Anyway—this is where most meals come from. When we’re lucky."
->DONE

=== healers_tent === 
<i>Mira leads you toward a quieter part of the camp. A soft herbal scent lingers in the air as you approach a canvas tent marked with stitched symbols—some worn, some freshly patched. Inside, you catch glimpses of bundled herbs, makeshift bandages, and a few worn cots lined up in careful rows.</i>
"This is the healer’s tent. We make do with what we can find—bark salves, swamp herbs, anything that still grows despite the city’s reach."
* [You do all the healing here?]
    Mira: "I’m one of a few. I help where I can, but it’s not easy. No steady supplies. No proper tools. Some days, it’s just hope and whatever nature gives us."
** [You’re resourceful. That takes more strength than most.]
    ~ TrustGained("5")
<i>She looks away, but a quiet pride flickers across her face.</i>
"Thanks. I try."
*** [So this is why you went into the swamp alone?]
"Yeah. We needed more of a root that helps with infections. The trip was supposed to be later, but... I wanted to prove I could help."
<i>She kneels briefly to straighten a cloth, her voice gentler now.</i>
"No one here gets left behind if we can help it. That’s what makes this place different from the city. Even if we’re struggling, we take care of our own."
<i>She rises again, brushing her hands together before glancing at you.</i>
"Anyway... that’s the healer’s tent. When we're not treating the wounded, the potions and tools satisfies the children's curiousity"
->DONE

=== elders_building ===
<i>Mira slows as you approach the largest structure in town. It’s made from salvaged wood, unearthed mud bricks, and old cloth banners that ripple gently in the wind. Just outside, a tall figure stands outside of the building, hands folded in his robe pockets—eyes scanning the horizon.</i>
"That’s Elder Iden. He doesn’t hide behind walls like the city leaders. Says if he’s going to lead, he needs to see the people he’s leading."
<i>There’s something off about him—and then you see it. The light catches his face just right.
Purple skin. Subtle, but unmistakable. The ears, too.
He’s one of you.
A changeling.
But… how is he free? No bindings. No trace of city control.
You were sent to investigate a threat, but what if this is what they meant?<i>
<i>As the two of you approach, Elder Iden turns his head. His gaze settles on Mira first, relief flickering across his weathered face—then shifts to you. He regards you with a quiet, unreadable look.</i>
"Mira. You came back. That’s good to see."
"Would’ve been worse if I hadn’t…but I didn’t come alone. They helped me get through."
<i>He nods slowly, eyes not leaving you.</i>
"So I see."
* [Just lending a hand.]
    "We’re used to handling things ourselves here. But…every hand has a cost, doesn’t it?"
-> quest_end
* [Don’t worry, I’m not here to cause trouble.]
    "No one ever says they’re here to cause trouble."
-> quest_end
* [Say nothing.]
    "Quiet type. That can be wise—or dangerous. Time’ll tell."
->quest_end

= quest_end
<i>There’s a pause—just long enough to feel the weight of his thoughts—before he turns back to Mira.</i>
"You didn’t see <b>Aspen</b> out there, did you?"
"No. But I was hoping you had."
"Nothing. Last word said he went northeast—ridge line. Hasn’t come back through any of the checkpoints."
<i>Mira frowns, then glances toward the edge of town.</i>
"I’ll stop by the hunter’s tent. See what I can find out."
<i>Iden nods once, but his eyes flick back to you.</i> 
"Be careful, Mira. This town can’t afford to lose people who care about it."
<i>There’s no threat in his voice—just gravity. A warning wrapped in concern.</i>
"Come on. If Aspen’s still out there, we need to find him—before something else does. Or worse…the city."
~ CompleteQuest("AWolfInSheepsClothingSO")
->DONE