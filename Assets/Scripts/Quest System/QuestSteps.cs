using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestSteps : MonoBehaviour 
{
    //we make this abstract so that we can have different types of steps
    
    private bool isCompleted = false;
    
    protected void CompleteStep() {
        if(!isCompleted) {
            isCompleted = true;
            
            // TO-DO: advance the quest forward now that this step is completed  
            
            Destroy(this.gameObject);
        }
    }
}
