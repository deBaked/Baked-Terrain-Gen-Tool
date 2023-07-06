using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropdownAnim : MonoBehaviour
{
    public GameObject details;  // references the details menu for each modifier
    public Animator animator;   // references the animator component on the dropdown button

    /// <summary>
    /// This function is called onClick of a dropdown button 
    /// to start the animation and show the correct detals
    /// </summary>
    public void showDropDown()
    {
        if (details.activeInHierarchy)
        {
            animator.SetBool("Closing", true);
            animator.SetBool("Opening", false);
            details.SetActive(false);
        }
        else
        {
            animator.SetBool("Opening", true);
            animator.SetBool("Closing", false);
            details.SetActive(true);
        }
    }

    /// <summary>
    /// Called at the end of the animation to change animation parameters to false
    /// </summary>
    public void endAmin()
    {
        animator.SetBool("Opening", false);
        animator.SetBool("Closing", false);
    }
}
