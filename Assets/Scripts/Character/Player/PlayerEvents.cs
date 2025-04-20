using System;

public class PlayerEvents
{
    public event Action<int> onTrustGained;
    public void TrustGained(int trustAmount)
    {
        if(onTrustGained != null)
        {
            onTrustGained(trustAmount);
        }
    }
    
    public event Action<int> onTrustLost;
    public void TrustLost(int trustAmount)
    {
        if(onTrustLost != null)
        {
            onTrustLost(trustAmount);
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
