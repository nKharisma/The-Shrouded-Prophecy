using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Auto_Scroll : MonoBehaviour
{
    [SerializeField] GameObject currentlySelected;
    [SerializeField] GameObject previouslySelected;
    [SerializeField] RectTransform currentSelectedTransform;
    
    [SerializeField] RectTransform contentPanel;
    [SerializeField] ScrollRect scrollRect;
    
    private void Update()
    {
        currentlySelected = EventSystem.current.currentSelectedGameObject;
        
        if(currentlySelected != null)
        {
            if(currentlySelected != previouslySelected)
            {
                previouslySelected = currentlySelected;
                currentSelectedTransform = currentlySelected.GetComponent<RectTransform>();
                SnapTo(currentSelectedTransform);
            }
        }
    }
    
    private void SnapTo(RectTransform target)
    {
        Canvas.ForceUpdateCanvases();
        
        Vector2 newPosition = (Vector2)scrollRect.transform.InverseTransformPoint(contentPanel.position) - (Vector2)scrollRect.transform.InverseTransformPoint(target.position);
        
        newPosition.x = 0;
        contentPanel.anchoredPosition = newPosition;
    }
    
}
