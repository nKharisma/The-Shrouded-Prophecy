=== ARoadForgottenQuestStart ===
<i>Eryn, a young hunter, paces back and forth near a shelf of sharp weapons.</i>
<i>Their hand keeps brushing over the handle of a dagger at their hip, not out of aggression—just nerves. As you approach, they don't seem to notice you at first—muttering something under their breath.</i>
“It’s been too long… Something’s wrong, I know it—he’s never out this long.”
<i>You clear your throat gently.</i> 
<i>The hunter jolts slightly and turns. Their eyes narrow—not in hostility, just in caution. A beat passes before they speak.</i>
"Sorry, I didn't hear you walk up."
<iThey scan your face briefly, unfamiliarity written all over theirs.
<i>Before you can respond, Mira steps up beside you, offering a small, calm smile.</i>
<i>There’s a pause as they glance down the road again. Their hands clench.</i>
“It’s alright. They’re with me.”
“I see. Mira vouching for you means something.”
<i>The hunter lets out an exasperated sigh.</i>
“It’s Aspen. He left this morning to fish by the southern beach. We’ve been low on supplies… but he should’ve been back hours ago.”
<i>Mira furrows her brow.</i> 
“He’s never late.”
 “Exactly. And I—I can’t just leave to go after him. I’m not allowed to leave post without clearance, not alone."
 "I’m still in training. But Aspen… he’s the best we’ve got. If something happened to him…”
 * [I'll find him.]
 //trust up
 ~ TrustGained("5")
 ~ StartQuest("AForkInTheRoadSO")
 -> go_on
 * [Do you think he's still out there?]
 -> go_on
 = go_on
They hesitate, searching your expression again. Then, a slow nod.
“South trail. Follow the trail till you hit the shore. That area has been friendly to us lately. Until now it seems.”
Mira gives your shoulder a gentle pat.
"We've got this."
The hunter exhales deeply. 
“Thanks. Both of you.”
//transition to shore
-> DONE

=== meet_aspen ===
<i>You find Aspen slumped against an old worn-out boat, clutching his upper arm. His fingers are slick with blood, and sweat clings to his brow.</i>
<i>His eyes flick up as you approach, wild and sharp despite the exhaustion.</i>
“Stop—don’t come any closer."
“They’re still out there. If they see you—hell, they’ll be back any second.”
<i>You freeze.</i>
* [What are you talking about?]
-> continue_on
* [You look like hell]
-> continue_on
* [I was sent to bring you home.]
->continue_on

= continue_on
He blinks, clearly stunned.
"You're serious?"
<i>You meet his gaze, confused. There’s a beat of silence between you. His expression shifts from irritation to something more wary.</i>
“…You’re not from here, are you?”
<i>You don't answer fast enough.</i>
“Great. Just great. Look, this spot—this whole road—is a hunting ground for the Knights of the Fae. Bandits in armor, that’s all they are."
"They hit traders, lone travelers. I thought I’d have enough time to grab some fish and get back before they showed up again.”
<i>You glance around, narrowing your eyes like you’re trying to recall something. ou recognize the path. You’ve read about it in reports—Knights of the Fae have hit supply lines here more than once.</i>
"I was getting ready to fish. Thought I was alone, thought I had enough time. But they came and abushed me.”
“I got lucky. Got away. But they’re not far.”
You hear a crack of a twig. Aspen's head snaps around.
"Damn it. They're back."
Three figures emerge from the treeline—armor dark, blades drawn.
* [Fight them]
-> DONE
* [Refuse to fight them]
-> DONE

=== knights_battle ===
<i> silver insignia glints on their chest: the mark of the Knights of the Fae. Steel flashes.</i>
<i>One of them grins. Another twirls a wicked-looking blade. None of them speak.</i>
The Knights begin to fan out, boxing you in. Aspen steps beside you, injured but ready, bow raised.
You grip your weapon tighter. There’s no way out but through.
The Knights rush forward.
You stand your ground.
//skill check (can pass or fail) 
-> knights_battle_result

= knights_battle_result
<i>You can’t tell if Aspen’s silence means relief or disappointment.</i>

{result == 1:
<i>The Knights stagger back, wounded. One collapses, clutching a bloody gash. The others flee into the woods, pulling the wounded knight with them.</i>
Aspen lowers his weapon, panting.
“Hah... I can’t believe it. You’re stronger than you look. Don’t let it get to your head.”
<i>He smiles faintly as you help him up.</i>
Thanks for the save. Let’s get back before I pass out.”
~ TrustGained("10")
~ passedKnightBattle = true
-> DONE
- else:
    <i>You fall to one knee, gasping. A blade raises above your head—then an arrow flies past you, striking true.</i>
    <i>The Knight clutches the arrow in his chest
    before collapsing weakly. You turn to see Aspen
    breathing heavily, clutching his bow.</i>
    “Some fighter you are… if it were up to
you, we’d both be dead.”
~ TrustLost("10")
-> DONE
}


=== return_to_aspen === 
{passedKnightBattle == true:
<i>Aspen pauses, as if searching for the right words.</i>
"You really handled yourself out there. I hate to admit it, but if you hadn't shown up...well, I wouldn't be here."
<i>A tired smile breaks across his face as he extends a hand.</i>
"You're something else, stranger. I think you might actually belong here."
<i>You take his hand, and he grips it with surprising strength.</i>
"I don't know how long you'll be with us, but if you ever need anything—gear, food, help—I got you.
"You saved my life. You helped me, so I help you. That's how it works around here."
-> DONE
- else:
Aspen's voice is quiet.
"Look...what I said back there, I know it came off harsh. I snapped. That's on me."
He stares down his bandaged arm.
"But you need to understand—this place? It's not safe. We don't get do-overs out there."
"You hesitated, or you couldn't finish the fight. Either way...if I hadn't stepped in..."
<i>He trails off, jaw clenched.</i>
"I look out for my people. Always have, always will. And I don't know you—not really."
-> DONE
}

=== travel_to_town ===
Aspen exhales sharply, wiping blood from his brow. 
"Well, that could've gone worse."
<i>Mira hurries to his side, eyes wide with concern.</i>
“Are you both alright? I was trying to keep up, but—gods, I thought we were going to lose you.”
<i>Aspen clutches his arm with a wince, then gives Mira a shaky nod.</i>
“I’ll live. Wouldn’t be the first scar I’ve earned out here.”
<i>Mira's voice lowers as she looks at you, clearly rattled.</i>
“We should go. That commotion… others might have heard.”
“Yeah. This won’t be the last we see of them."
<i>Mira glances between you and Aspen, then steps a little closer to you.</i>
“You really didn’t have to come this far for someone you barely knew… Thank you.”
* [I wasn't going to let someone die out here.]
~ TrustGained("10")
->continue_on_after
* [Just doing what I said I would.]
~ TrustGained("5")
-> continue_on_after
= continue_on_after
<i>Regardless of what you say, Aspen gives you a hard look—measuring, maybe even respectful.</i>
“Let’s get back. The medic’ll patch me up—again—and maybe I’ll owe you a story or two.”
* [You're hurt. We need to patch you up first.]
-> mira_response
* [If you can walk, we should go.]
-> mira_response

= mira_response
<i>Aspen grits his teeth and clutches his side. His bandage is now soaked through with blood.</i>
Mira kneels besides him and hovers her hands right above his wound. Her hands glow slowly after and you can see some pain relieve from Aspen's face.
“This won’t fix everything, but it should slow the bleeding.”
“Huh. That’s new. Never figured you for the magic type.”
<i>You and Mira help Aspen to his feet, steadying him as he groans.</i>
 “I’ll manage. I’ve walked worse with less.”
 <i>He glances back at the battlefield briefly before turning away.</i>
“Let’s go home.”
//travel to the town
-> DONE

=== medic_mage ===
<i>You pull aside the flap of the worn canvas tent. The scent of herbs and smoke drifts out, warm and earthy.</i>
<i>Inside, the town mage is hunched over a small table, scribbling in the margins of a scroll with focused intensity.</i>
<i>At the sound of your footsteps, she looks up—and then freezes, eyes widening as she sees Aspen slumped against you.</i>
 "By the stars—Aspen!"
 <i>She rises so fast her stool falls backward with a clatter. She’s already moving, guiding Aspen to a cot laid with patched quilts.</i>
 "Lie down, love. Let me look at you."
<i>Her hands move with practiced care as she inspects the wound. She frowns, then reaches for a hanging bundle of dried herbs, muttering under her breath.</i>
<i>She plucks a red vial from her satchel, pops the top, and waves it briefly under Aspen’s nose. A shimmer glows faintly in the bottle’s swirl.</i>
"Drink. Slowly now."
Aspen groans but obeys. The Mage turns to you as he swallows.
"It was the Knights again, wasn’t it? …You brought him back in one piece. Thank you. He may act like a fool, but this village wouldn’t last a season without his bow."
<i>She places a firm hand on your shoulder, her gaze holding yours for a beat longer than expected.</i>
"Thank you. You didn’t have to bring him back—but you did."
{passedKnightBattle == true:
"The fact you came back alive... and brought him too... That says something. This town’s lucky to have you."
* [I couldn't let him fall. He's important here.]
~ TrustGained("5")
-> aspen_wakes
* [We both made it back. That's enough.]
-> aspen_wakes
* [He was reckless. Almost dragged us both under.]
~ TrustLost("5")
-> aspen_wakes
- else: 
    "Thank you. Even if it went wrong… you still brought him home."
* [I tried. I hope it was enough.]
~ TrustGained("5")
-> aspen_wakes
* [I did what I could.]
-> aspen_wakes
* [He walked into it. Not my fault]
~ TrustLost("10")
-> aspen_wakes
}

= aspen_wakes
<i>Aspen stirs, his voice low but steady.</i>
"Ugh, you're still here? Thought you'd have vanished."
{passedKnightBattle == true:
"You really saved my ass out there."
"If you need anything, come find me. That's a promise."
* [I'll take you up on that]
~ TrustGained("3")
-> DONE
- else:
"You know...I was mad back there. Maybe still am."
"But you didn't leave me to die. I'll give you that."
"Still think you should've listented. Might've saved us both some pain."
* [You're right. I didn't handle it well]
-> DONE
* [If you want someone perfect, look elsewhere.]
~ TrustLost("5")
-> DONE
}

=== town_celebration ===
<i>A couple of days pass. The town holds a gathering to celebrate the hunter's recovery. You're there. On the edge of it all.</i>
<i>Children run laughing through the dusk-lit square. Mira chats animately with the mage near the firepit, and Aspen leans on a crutch, offering small smiles to those who pass.</i>
<i>Plates of warm food, mismatching mugs, and handmade trinkets pass from hand to hand. The town buzzes with quiet joy—a rare and fragile kind.</i>
<i>You're handed a drink. Someone claps you on the back. You nod, you smile...but it never quite reaches your eyes.</i>
<i>They call this home. They call me friend...but do they really know who I am?</i>
<i>Mira glances over from across the fire and catches your gaze. Her smile softens, gentle.</i>
<i>You give her a smile back—but your chest feels hollow.</i>
<i>Later that night, the crowd begins to thin. Ash and laughter drift into the cool evening air.</i>
<i>Aspen sits under a tree, retelling the battle story with a bit more flair than truth. Mira throws in a teasing correction, and everyone laughs.</i>
<i>As you pass by the boundary trail near the north exit of the town, you reach into your pocket—you find something familiar tucked away—an insignia from your old life.</i>
<i>It's small. Smooth. Cold.</i>
<i>A metallic crest etched with sharp lines and old authority. The symbols of the city. The symnol of them.</i>
<i>You stare at it in the moonlight. You remember the captain giving it to you. Then it hums. Barely perceptible—but enough.</i>
<i>The world around you fades.</i>
<i>A faint puple glow pulses from the insignia, casting faint shadows across your hand. Your grip tightens as a voice crackles to life—metallic, distant, but unmistakably familiar.</i>
"If this reaches you, operative, then you’ve done your part. The town trusts you. It's time. Provide the entry point. Open the way. Your signal will trigger the march."
<i>The light fades. The insignia stills. But it feels heavier now. It weighs like a blade in your hand.</i>
-> DONE

=== companion_confrontation ===
<i>Memories strike like flint: your commanding officer’s cold gaze, the mission briefings, the vow you made to bring this town down from the inside.</i>
<i>You clutch it tighter, suddenly aware of the sound of footsteps.</i>
"You okay? You've been standing here a while." Mira speaks up first. 
Aspen glances between you and your clenched hand.
"You're looking at that thing like it's a ghost. You sure everything's alright?"
* [It's nothing. Just...old junk]
~ TrustLost("5")
<i>Mira watches you for a beat longer before nodding, unconvinced.</i>
"Okay...Just don't bottle things up, trust means a lot around here, remember?"
-> DONE
* [It's a piece of my past. One I'm not sure I've left behind.]
Aspen's usually battle-harden look wavers a bit. 
"We don't get to choose where we start, but we do get to choose what comes next."
-> DONE
* [You wouldn't understand.]
Aspen steps forward as Mira's eyes drop slightly.
"You're not the only one with a past. Don't talk like we haven't seen things too."
<i>The air hardens, and the space between you suddenly feels wider.</i>
//black scene
~ TrustLost("10")
-> DONE

=== meet_at_town_square ===
The next morning, Mira awakes you from your shack.
"They're asking for you in the center of town. Something's happening."
Aspen stands beside her. "There's movement in the woods. I saw tracks last night—fresh ones. Something's coming."
<i>You meet his gaze. It’s not accusatory. It’s searching. As if he already suspects, but still wants to believe you’re on their side.</i>
-> DONE

=== confrontation === 
<i>The town gathers at the square, the sky still painted in the soft glow of early morning. But there’s no warmth in the air—just tension.</i>
<i>It clings to every glance, every whispered word between townsfolk. Children are ushered indoors. Shop doors close one by one.</i>
<i>You arrive just as Eryn finishes speaking with the gate guard. His jaw is tight. His eyes scan the horizon.</i>
The watchtower guard reports the issue to those gathered around.
"They weren't hiding. They wanted us to see them. A city envoy. Five riders. Crest on their armor. They'll be here soon."
<i>Gasps ripple through the crowd. The elder calls for calm, but it barely holds. You feel the shift in the air—like the wind before a coming storm.</i>
<i>You feel it first in your chest—a slow pressure, rising like a tide. The insignia in your pocket feels heavier than iron. Burning. A relic from another life. Your real life.</i>
<i>Whispers begin to bubble in front of you.</i>
<i>Mira steps forward. Her eyes search yours—not angry, not afraid. Just…uncertain.</i>
"You feel it too, don't you? Something's not right. Say something."
<i>Even Aspen hesitates beside her, arms crossed, his voice low but firm.</i>
“If there’s anything you need to say—say it now.”
<i>The townspeople wait. The fire pit at the center of the square crackles quietly. In the distance, a hawk cries overhead. Everything is still.</i>
<b>You step forward, heart pounding. All eyes remain on you. Mira watches, silent. Aspen’s grip tightens around the handle of his blade. The choice is yours.<b>
* [I was sent here to gather information.]
"But, I was wrong. I shouldn't of dececived you all. I want to help you, not the city."
"If you all don't want me here afterwards, I understand. I will leave after, but I won't let this town suffer because of the tyranny of the city."
<i>The sound of heavy footsteps and metal clanking gets closer—too close.</i>
<i>Aspen pierces you with his staggering glare. Like when you first met him, but somehow even worse.</i>
"You better hope we can make it out of this."
~ betrayal = false
-> final_battle
* [This was always temporary. Just doing my job.]
~ betrayal = true
<i>The square falls silent.</i>
<i>Your words echo louder than any shout might have.</i>
<i>Murmurs spread like fire through the crowd. Some faces turn pale. Others twist into grim resolve.</i>
<i>A few townsfolk step forward—others step back.</i>
 A horn blasts in the distance. The envoy has arrived behind you, and they're not waiting to parley. The Elder's voice booms from the top of the watchtower hill.</i>
"Then you've made your decision. We won't go quietly though."
->final_battle

=== final_battle ===
<i>You'd never thought you'd be here.</i>

{betrayal == true && result == 1: 
<i>You charge into battle with the city’s envoy behind you, power surging through the enhancements you secretly brought along.</i>
<i>The companions falter under the barrage. The town watches, stunned, as its defenders fall to their knees.</i>
<i>The Elder drops their staff.</i>
“So... this was your truth.”
<i>Mira doesn’t speak. She only stares, expression unreadable, and turns away.</i>
<i>You don’t stay for the aftermath. You don’t look back as the banners of the city rise above the rooftops.</i>
<b>The Mission comes first.</b>
<b>This is the end of our game, we hope you enjoyed it!</b>
-> DONE
}
{betrayal == true && result == 0:
<i>You fight with all your might—but the town knows its terrain, and your body wears thin despite your advantage.</i>
<i>Just as a blow strikes your side, you twist, shielding the envoy. It’s enough. The companions rally and force the city back.</i>
<i>You hear Mira shout your name. Aspen barrels toward you, blade up—but the city’s vanguard is collapsing, and the envoy is exposed.</i>
<i>The city pulls back, broken and scattered. The town survives. But the one who bridged both worlds is left behind, unmoving.</i>
<i>Your insignia slips from your hand. Mira picks it up, but doesn’t speak.</i>
<b>This is the end of our game, we hope you enjoyed it!</b>
-> DONE
}
{betrayal == false && result == 0:
<i>Despite your efforts, the enemy pushes closer. You see Aspen stagger, Mira trying to protect the elder. It’s not enough. You stand in the center, panting. Then, a moment of clarity.</i>
<i>You shout for them to fall back—and you charge forward alone.</i>
<i>The magic from Mira’s earlier healing still lingers. It buys you time. Enough to draw the enemy fire. Enough to ensure the town survives.</i>
<i>Your fate is left uncertain, but the town holds strong in the end.</i>
<b>This is the end of our game, we hope you enjoyed it!</b>
-> DONE
}

{betrayal == false && result == 1:
   <i>Together with the companions, you weave through fire and steel, dodging blasts, leading enemies into traps.</i>
   <i>Powerups pulse within your veins—not from espionage, but from trust earned. The envoy falls, their plan foiled.</i>
   <i>The town roars as the gate closes behind the fleeing remnants of the city.</i>
   Aspen gives you a sly grin after the battle is over.
   "Looks like you made the right choice, huh?"
<b>This is the end of our game, we hope you enjoyed it!</b>
   -> DONE
}