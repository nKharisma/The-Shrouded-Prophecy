EXTERNAL StartQuest(questID)
EXTERNAL AdvanceQuest(questID)
EXTERNAL CompleteQuest(questID)
EXTERNAL playSkillCheckUI()
EXTERNAL LoadSceneInGame(sceneIndex, x, y, z)

VAR result = 0
VAR passedTraining = false
VAR captainDuelPassed = false
VAR sirDuelPassed = false
VAR nice = false

INCLUDE FirstImpressions.ink
INCLUDE AWolfInSheepClothing.ink

