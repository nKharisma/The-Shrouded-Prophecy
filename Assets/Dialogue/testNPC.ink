EXTERNAL playSkillCheckUI()
VAR result = 0

-> main

=== main ===
Which totally not copyrighted creature do you choose?
    + [GrassDino<color=\#a3ff66> -\> Dexterity Skill Check</color>]
        ~ playSkillCheckUI()
        -> chosen("GrassDino")
        
    + [FireLizard<color=\#ff3333> -\> Strength Skill Check</color>]
        ~ playSkillCheckUI()
        -> chosen("FireLizard")
    + [I actually don't want one...]
        Oh, ok then. Your loss loser, good luck getting through the game. People like you sicken me.
        -> END
        
=== chosen(creature) ===
<i>The creature vendor gives you a glare.</i>
{result == 1:
    Hmph, fine. Here's your {creature}.
- else:
    You have failed, leave us.
}

-> END
