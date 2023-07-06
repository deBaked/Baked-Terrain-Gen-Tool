using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InfoIconInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject textPrompt; // references the text prompt that will be displayed
    [SerializeField] Animator animator;     // refernces the animator component on the text prompt which allows it to fade in
    bool isHovering;                        // Used to hold and determine the state of the user's mouse over the prompt
    float hoverTime;                        // Used to determine if the user is holding their mouse over the prompt

    void Start()
    {
        textPrompt.SetActive(false);        // always ensures the text prompts are disabled when starting
    }

    private void Update()
    {
        if (isHovering)                         // if the mouse is hovering over the prompt then...
        {
            hoverTime += Time.deltaTime;            // increase the timer
            
        }
        if (hoverTime > 0.8f)                   // if the timer is above 0.8 seconds then...
        {
            isHovering = false;                     // set isHovering to false as we dont need to keeping timing
            textPrompt.SetActive(true);             // set the text prompt to true
            animator.SetBool("Disappear", false);   // Esnure the animation parameter for disappearing is turned off
            animator.SetBool("Appear", true);       // Ensure the animation parameter for appearing is on
        }
    }

    /// <summary>
    /// Used to determine if the user's mouse is over the prompt,
    /// if it is - isHovering is set to true
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("entered");
        isHovering = true;
    }

    /// <summary>
    /// Used to determine if the user's mouse is no longer over the prompt,
    /// if it is not - reset the timer and set the isHovering bool to false,
    /// also disable the animation parameter for appearing and enable for disappearing
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        hoverTime = 0f;
        isHovering = false;
        animator.SetBool("Appear", false);
        animator.SetBool("Disappear", true);
    }
}
