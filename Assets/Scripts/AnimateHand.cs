using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class AnimateHand : MonoBehaviour
{
    public InputActionProperty gripAction; 
    public Animator handAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float value = gripAction.action.ReadValue<float>() > 0.5f ? 0.5f : 0f;
        handAnimator.SetFloat("Grip", value);
    }
}
