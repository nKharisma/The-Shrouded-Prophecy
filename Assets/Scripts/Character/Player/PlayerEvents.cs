using System;

public class PlayerEvents
{

    public event Action onQuestLogTogglePressed;
    public void QuestLogTogglePressed()
    {
        if (onQuestLogTogglePressed != null) 
        {
            onQuestLogTogglePressed();
        }
    }
    public event Action onDisablePlayerMovement;
    public void DisablePlayerMovement()
    {
        if(onDisablePlayerMovement != null)
        {
            onDisablePlayerMovement();
        }
    }

    public event Action onEnablePlayerMovement;
    public void EnablePlayerMovement()
    {
        if(onEnablePlayerMovement != null)
        {
            onEnablePlayerMovement();
        }
    }

    public event Action<int> onTrustGained;
    public void TrustGained(int trustAmount)
    {
        if(onTrustGained != null)
        {
            onTrustGained(trustAmount);
        }
    }
    
    public event Action<int> onPlayerTrustChange;
    public void PlayerTrustMeterChange(int trustAmount)
    {
        if(onPlayerTrustChange != null)
        {
            onPlayerTrustChange(trustAmount);
        }
    }
    
    public event Action<int> onPlayerTrustLevelChange;
    public void PlayerTrustLevelChange(int trustLevel)
    {
        if(onPlayerTrustLevelChange != null)
        {
            onPlayerTrustLevelChange(trustLevel);
        }
    }
}
