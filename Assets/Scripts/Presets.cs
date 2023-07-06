using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Presets : MonoBehaviour
{
    TMP_Dropdown dropdown;                  // references the dropdown menu
    string selectedPreset;                  // holds the name of the selected preset in the dropdown
    public TerrainPerlin terrainPerlinSC;   // references the terrain perlin script

    void Start()
    {
        dropdown = this.gameObject.GetComponent<TMP_Dropdown>();    // grabs the dropdown component
        //Debug.Log("Starting Dropdown Value : " + dropdown.value);
    }

    /// <summary>
    /// Depending on the preset selected on the preset list,
    /// alter the values of each of the modifiers.
    /// </summary>
    public void OnSelect()
    {
        selectedPreset = dropdown.options[dropdown.value].text;     // grabs the currently selected preset which will be from the options below

        if (selectedPreset == "Valleys and High Mountains")         // if selected, set slider values to the below
        {
            terrainPerlinSC.amplitudeSlider.value = 0.693f;
            terrainPerlinSC.frequencySlider.value = 0.008f;
            terrainPerlinSC.scaleSlider.value = 0.5f;
            terrainPerlinSC.heightSlider.value = 0.293f;
            terrainPerlinSC.waterLevelSlider.value = -4.040f;
            terrainPerlinSC.valleysSlider.value = 2.604f;
            terrainPerlinSC.mapSlider.value = -107.32f;
            terrainPerlinSC.coldSlider.value = 69f;
            terrainPerlinSC.worldZoomSlider.value = 10f;
            terrainPerlinSC.islandTypeSlider.value = 0f;
            terrainPerlinSC.cliffLimitSlider.value = 29.601f;
            terrainPerlinSC.cliffFlatnessSlider.value = 3.553f;
        }
        else if (selectedPreset == "Flat Valleys and Few Mountains")// if selected, set slider values to the below
        {
            terrainPerlinSC.amplitudeSlider.value = 0.078f;
            terrainPerlinSC.frequencySlider.value = 0.020f;
            terrainPerlinSC.scaleSlider.value = 0.478f;
            terrainPerlinSC.heightSlider.value = 0.582f;
            terrainPerlinSC.waterLevelSlider.value = -2.335f;
            terrainPerlinSC.valleysSlider.value = 1.679f;
            terrainPerlinSC.mapSlider.value = -97.940f;
            terrainPerlinSC.coldSlider.value = 69f;
            terrainPerlinSC.worldZoomSlider.value = 10f;
            terrainPerlinSC.islandTypeSlider.value = 0f;
            terrainPerlinSC.cliffLimitSlider.value = 200f;
            terrainPerlinSC.cliffFlatnessSlider.value = 3.553f;
        }
        else if (selectedPreset == "Bumpy Mountains 1")             // if selected, set slider values to the below
        {
            terrainPerlinSC.amplitudeSlider.value = 3f;
            terrainPerlinSC.frequencySlider.value = 0.008f;
            terrainPerlinSC.scaleSlider.value = 0.338f;
            terrainPerlinSC.heightSlider.value = 0.878f;
            terrainPerlinSC.waterLevelSlider.value = -4.013f;
            terrainPerlinSC.valleysSlider.value = -1.690f;
            terrainPerlinSC.mapSlider.value = -97.940f;
            terrainPerlinSC.coldSlider.value = 69f;
            terrainPerlinSC.worldZoomSlider.value = 10f;
            terrainPerlinSC.islandTypeSlider.value = 0f;
            terrainPerlinSC.cliffLimitSlider.value = 28.594f;
            terrainPerlinSC.cliffFlatnessSlider.value = 3.408f;

        }
        else if (selectedPreset == "Bumpy Mountains 2")             // if selected, set slider values to the below
        {
            terrainPerlinSC.amplitudeSlider.value = 2.248f;
            terrainPerlinSC.frequencySlider.value = 0.017f;
            terrainPerlinSC.scaleSlider.value = 0.5f;
            terrainPerlinSC.heightSlider.value = 0.205f;
            terrainPerlinSC.waterLevelSlider.value = -4.249f;
            terrainPerlinSC.valleysSlider.value = -0.616f;
            terrainPerlinSC.mapSlider.value = -113.647f;
            terrainPerlinSC.coldSlider.value = 69f;
            terrainPerlinSC.worldZoomSlider.value = 10f;
            terrainPerlinSC.islandTypeSlider.value = 0f;
            terrainPerlinSC.cliffLimitSlider.value = 33.185f;
            terrainPerlinSC.cliffFlatnessSlider.value = 4.713f;
        }
        else if (selectedPreset == "Plateaus")                      // if selected, set slider values to the below
        {
            terrainPerlinSC.amplitudeSlider.value = 1.031f;
            terrainPerlinSC.frequencySlider.value = 0.019f;
            terrainPerlinSC.scaleSlider.value = 0.5f;
            terrainPerlinSC.heightSlider.value = -0.234f;
            terrainPerlinSC.waterLevelSlider.value = -11.998f;
            terrainPerlinSC.valleysSlider.value = 3f;
            terrainPerlinSC.mapSlider.value = -12.795f;
            terrainPerlinSC.coldSlider.value = 70f;
            terrainPerlinSC.worldZoomSlider.value = 10f;
            terrainPerlinSC.islandTypeSlider.value = 0f;
            terrainPerlinSC.cliffLimitSlider.value = 147.966f;
            terrainPerlinSC.cliffFlatnessSlider.value = 11.237f;
        }       
        else if (selectedPreset == "Flatter Mountains")             // if selected, set slider values to the below
        {
            terrainPerlinSC.amplitudeSlider.value = 1.173f;
            terrainPerlinSC.frequencySlider.value = 0.019f;
            terrainPerlinSC.scaleSlider.value = 0.418f;
            terrainPerlinSC.heightSlider.value = -0.180f;
            terrainPerlinSC.waterLevelSlider.value = -8.500f;
            terrainPerlinSC.valleysSlider.value = -2.051f;
            terrainPerlinSC.mapSlider.value = 18.618f;
            terrainPerlinSC.coldSlider.value = 70f;
            terrainPerlinSC.worldZoomSlider.value = 10f;
            terrainPerlinSC.islandTypeSlider.value = 0f;
            terrainPerlinSC.cliffLimitSlider.value = 92.871f;
            terrainPerlinSC.cliffFlatnessSlider.value = 11.569f;
        }   
        else if (selectedPreset == "Small Simple Island")           // if selected, set slider values to the below
        {
            terrainPerlinSC.amplitudeSlider.value = 0.694f;
            terrainPerlinSC.frequencySlider.value = 0.008f;
            terrainPerlinSC.scaleSlider.value = 0.5f;
            terrainPerlinSC.heightSlider.value = 0.293f;
            terrainPerlinSC.waterLevelSlider.value = -4.04f;
            terrainPerlinSC.valleysSlider.value = 2.604f;
            terrainPerlinSC.mapSlider.value = -99.466f;
            terrainPerlinSC.coldSlider.value = 69f;
            terrainPerlinSC.worldZoomSlider.value = 10f;
            terrainPerlinSC.islandTypeSlider.value = 1f;
            terrainPerlinSC.islandIntensitySlider.value = 16;
            terrainPerlinSC.cliffLimitSlider.value = 29.601f;
            terrainPerlinSC.cliffFlatnessSlider.value = 2.553f;
        }
        else if (selectedPreset == "Cluster of Islands")            // if selected, set slider values to the below
        {
            terrainPerlinSC.amplitudeSlider.value = 3f;
            terrainPerlinSC.frequencySlider.value = 0.008f;
            terrainPerlinSC.scaleSlider.value = 0.338f;
            terrainPerlinSC.heightSlider.value = -0.878f;
            terrainPerlinSC.waterLevelSlider.value = -4.013f;
            terrainPerlinSC.valleysSlider.value = -1.69f;
            terrainPerlinSC.mapSlider.value = -62.861f;
            terrainPerlinSC.coldSlider.value = 69;
            terrainPerlinSC.worldZoomSlider.value = 10f;
            terrainPerlinSC.islandTypeSlider.value = 2f;
            terrainPerlinSC.islandIntensitySlider.value = 44f;
            terrainPerlinSC.cliffLimitSlider.value = 28.594f;
            terrainPerlinSC.cliffFlatnessSlider.value = 3.408f;
        }
        else if (selectedPreset == "Large Island")                  // if selected, set slider values to the below
        {
            terrainPerlinSC.amplitudeSlider.value = 0.427f;
            terrainPerlinSC.frequencySlider.value = 0.014f;
            terrainPerlinSC.scaleSlider.value = 0.5f;
            terrainPerlinSC.heightSlider.value = 0.293f;
            terrainPerlinSC.waterLevelSlider.value = -4.04f;
            terrainPerlinSC.valleysSlider.value = 2.362f;
            terrainPerlinSC.mapSlider.value = -154.440f;
            terrainPerlinSC.coldSlider.value = 69f;
            terrainPerlinSC.worldZoomSlider.value = 10f;
            terrainPerlinSC.islandTypeSlider.value = 2f;
            terrainPerlinSC.islandIntensitySlider.value = 41f;
            terrainPerlinSC.cliffLimitSlider.value = 29.601f;
            terrainPerlinSC.cliffFlatnessSlider.value = 2.553f;
        }
        else if (selectedPreset == "A Lake")                        // if selected, set slider values to the below
        {
            terrainPerlinSC.amplitudeSlider.value = 1.660f;
            terrainPerlinSC.frequencySlider.value = 0.007f;
            terrainPerlinSC.scaleSlider.value = 0.5f;
            terrainPerlinSC.heightSlider.value = 0.829f;
            terrainPerlinSC.waterLevelSlider.value = -11.998f;
            terrainPerlinSC.valleysSlider.value = -3f;
            terrainPerlinSC.mapSlider.value = -130.596f;
            terrainPerlinSC.coldSlider.value = 70f;
            terrainPerlinSC.worldZoomSlider.value = 10f;
            terrainPerlinSC.islandTypeSlider.value = 3f;
            terrainPerlinSC.islandIntensitySlider.value = 31f;
            terrainPerlinSC.cliffLimitSlider.value = 20.94221f;
            terrainPerlinSC.cliffFlatnessSlider.value = 2f;
        }
        else if (selectedPreset == "Diagonal Island")               // if selected, set slider values to the below
        {
            terrainPerlinSC.amplitudeSlider.value = 1.916f;
            terrainPerlinSC.frequencySlider.value = 0.003f;
            terrainPerlinSC.scaleSlider.value = 0.5f;
            terrainPerlinSC.heightSlider.value = 0.829f;
            terrainPerlinSC.waterLevelSlider.value = -11.998f;
            terrainPerlinSC.valleysSlider.value = -2.420f;
            terrainPerlinSC.mapSlider.value = 38.016f;
            terrainPerlinSC.coldSlider.value = 70f;
            terrainPerlinSC.worldZoomSlider.value = 10f;
            terrainPerlinSC.islandTypeSlider.value = 4f;
            terrainPerlinSC.islandIntensitySlider.value = -60f;
            terrainPerlinSC.cliffLimitSlider.value = 23.542f;
            terrainPerlinSC.cliffFlatnessSlider.value = 2.390f;
        }
        else if (selectedPreset == "Large Island 2")               // if selected, set slider values to the below
        {
            terrainPerlinSC.amplitudeSlider.value = 0.167f;
            terrainPerlinSC.frequencySlider.value = 0.018f;
            terrainPerlinSC.scaleSlider.value = 0.5f;
            terrainPerlinSC.heightSlider.value = 1f;
            terrainPerlinSC.waterLevelSlider.value = -4.013f;
            terrainPerlinSC.valleysSlider.value = 2.25f;
            terrainPerlinSC.mapSlider.value = -78.894f;
            terrainPerlinSC.coldSlider.value = 70f;
            terrainPerlinSC.worldZoomSlider.value = 10f;
            terrainPerlinSC.islandTypeSlider.value = 1f;
            terrainPerlinSC.islandIntensitySlider.value = 30f;
            terrainPerlinSC.cliffLimitSlider.value = 16.939f;
            terrainPerlinSC.cliffFlatnessSlider.value = 2.965f;
        }
    }
}
