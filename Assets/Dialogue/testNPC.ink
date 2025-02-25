EXTERNAL playSkillCheckUI()

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
You chose {creature}!

-> END
