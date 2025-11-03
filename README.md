# The Shrouded Prophecy — Project Overview

The Shrouded Prophecy is a narrative-driven Unity game blending story-first design with light action and RPG systems. The project pairs Ink-based narrative scripting with modular gameplay systems so narrative decisions, quests and player actions all affect game state and flow.

## Core systems and how they contribute

- Narrative / Dialogue (Ink)
  - Ink scripts define scenes, branching choices and external calls (e.g., playSkillCheckUI).
  - Keeps story content authorable and data-driven, letting designers iterate without code.
  - Dialogue outcomes drive quest progression, trust changes with companions and branch flow.

- Quest System
  - Tracks quest state, per-step logic and progression.
  - Exposes step-specific code hooks so gameplay (kill targets, finish dialogues, etc.) updates quest state.
  - Surfaces objectives through visual indicators and the quest UI so players always know goals.

- Skill Checks & Combat Encounters
  - Lightweight, reusable skill-check UI invoked from Ink (or code) to resolve contested moments.
  - Results are fed back to the narrative engine to determine success/failure branches.
  - Drives tangible outcomes (destroy enemies, grant trust, progress quests).

- Save / Load System
  - Persists global progress, quest states and metadata (time played, last quest).
  - Allows restore of play sessions and ensures narrative/quest continuity across loads.

- UI (Quest Log, Save Slots, HUD)
  - Communicates current objectives, companion status and progress details.
  - Quest log shows status and rewards; quest markers and visual indicators guide player navigation.
  - Save slot UI exposes metadata to help players pick saves quickly.

- Input & Player Controls
  - Centralized input layer (action maps) for consistent interactions across gameplay and UI.
  - WASD for player movement
  - E for NPC Interaction
  - Space to progress through dialogue/select choices
  - WASD or Arrow keys for skill checks
  - Prevents conflicts during dialogue or skill checks by enabling/disabling relevant actions.

- Scene / World Management & Events
  - Event management for dialogue start/complete and other cross-system notifications.
  - Decouples producers (dialogue, combat) from consumers (quests, UI) so systems respond without tight coupling.

- Presentation (Rendering, Audio, Effects)
  - Post-processing and audio polish reinforce pacing and feedback (skill-check results, enemy hits, dialogue cues).
  - Sprite-based indicators and sorting control ensure clear visual hierarchy.
