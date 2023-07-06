using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Incrementing : MonoBehaviour
{
    public Slider slider;                               // references the corresponding slider
    public TMP_InputField incrementVal, sliderInputVal; // references the input fields for corresponding slider

    /// <summary>
    /// Function to call when the enter value button is pressed
    /// </summary>
    public void OnGoBtn()
    {
        // try catch here to catch format exceptions entered by the user in the input field
        try
        {
            slider.value = float.Parse(sliderInputVal.text);    // set the slide value to the input from the user but convert to float
        }
        catch(FormatException e)                                // if input from user not convertable to float then set input field to zero and ask user to enter number
        {
            slider.value = 0f;
            sliderInputVal.text = "0";
            Debug.Log("Please enter a number - " + e.Message);
        }
    }

    /// <summary>
    /// Function to call when user decreases slider value
    /// </summary>
    public void DecreaseIncrement()
    {
        // try catch here to catch format exceptions entered by the user in the input field
        try
        {
            slider.value -= float.Parse(incrementVal.text);     // subtract input from user from slider value to decrease overall slider value
        }
        catch (FormatException e)                               // if input from user not convertable to float then set input field to zero and ask user to enter number
        {
            incrementVal.text = "0";
            Debug.Log("Please enter a number - " + e.Message);
        }
    }

    /// <summary>
    /// Function to call when user increase slider value
    /// </summary>
    public void IncreaseIncrement()
    {
        // try catch here to catch format exceptions entered by the user in the input field
        try
        {
            slider.value += float.Parse(incrementVal.text);     // subtract input from user from slider value to decrease overall slider value
        }
        catch (FormatException e)                               // if input from user not convertable to float then set input field to zero and ask user to enter number
        {
            incrementVal.text = "0";
            Debug.Log("Please enter a number - " + e.Message);
        }
    }


    /// <summary>
    /// Functionc called to update the slider value with the inputted float
    /// </summary>
    public void UpdateVal()
    {
        sliderInputVal.text = slider.value.ToString();
    }
}
