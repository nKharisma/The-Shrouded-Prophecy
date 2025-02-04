using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorManager : MonoBehaviour
{
    CharacterManager character;
    
    protected virtual void Awake() {
        character = GetComponent<CharacterManager>();
    }
    
    public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement)
    {
        character.animator.SetFloat("Vertical", verticalMovement);
        character.animator.SetFloat("Horizontal", horizontalMovement);
    }
}
