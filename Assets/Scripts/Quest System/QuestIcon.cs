using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestIcon : MonoBehaviour
{
    [Header("Quest Icons")]
    [SerializeField] private GameObject requirementsNotMetIcon;
    [SerializeField] private GameObject canStartIcon;
    [SerializeField] private GameObject inProgressIcon;
    [SerializeField] private GameObject canCompleteIcon;

    public void SetState(QuestState newState, bool startPoint, bool completePoint)
    {
        requirementsNotMetIcon.SetActive(false);
        canStartIcon.SetActive(false);
        inProgressIcon.SetActive(false);
        canCompleteIcon.SetActive(false);

        switch(newState)
        {
            case QuestState.Requirements_Not_Met:
                requirementsNotMetIcon.SetActive(true);
                break;
            case QuestState.Can_Start:
                if(startPoint)
                {
                    canStartIcon.SetActive(true);
                }
                break;
            case QuestState.In_Progress:
                if(completePoint)
                {
                    inProgressIcon.SetActive(true);
                }
                break;
            case QuestState.Can_Complete:
                if(completePoint)
                {
                    canCompleteIcon.SetActive(true);
                }
                break;
            case QuestState.Completed:
                break;
            default:
                Debug.LogWarning("Quest State not recognized by switch statement for quest icon" + newState);
                break;
        }
    }
}
