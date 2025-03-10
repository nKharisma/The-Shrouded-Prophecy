using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Ink.Runtime; 
public class DialogueEvents
{
    public event Action<string> onEnterDialogue;
    public void EnterDialogue(string knotName) 
    {
        if(onEnterDialogue != null)
        {
            onEnterDialogue(knotName);
        }
    }
    
    public event Action onExitDialogue;
    public void ExitDialogue()
    {
        if(onExitDialogue != null)
        {
            onExitDialogue();
        }
    }
    
    public event Action onDialogueStart;
    public void DialogueStart()
    {
        if(onDialogueStart != null)
        {
            onDialogueStart();
        }
    }
    
    public event Action onDialogueComplete;
    public void DialogueComplete()
    {
        if(onDialogueComplete != null)
        {
            onDialogueComplete();
        }
    }
    
    public event Action<string> onDisplayDialogue;
    public void DisplayDialogue(string text)
    {
        if(onDisplayDialogue != null)
        {
            onDisplayDialogue(text);
        }
    }
    
    public event Action<int> onUpdateChoice;
    public void UpdateChoice(int choiceIndex)
    {
        if(onUpdateChoice != null)
        {
            onUpdateChoice(choiceIndex);
        }
    }
    
    public event Action<string, Ink.Runtime.Object> onUpdateInkDialogueVariable;
    public void UpdateInkDialogueVariable(string variableName, Ink.Runtime.Object value)
    {
        if(onUpdateInkDialogueVariable != null)
        {
            onUpdateInkDialogueVariable(variableName, value);
        }
    }
}
