using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OffsetTranslation : MonoBehaviour
{
    //float movementSpeed;
    [SerializeField] Slider speedSlider;    // references a slider used for the movement speed
    public GameObject mesh;                 // references the terrain's mesh
    TerrainPerlin terrainPerlinSC;          // references the terrian perlin script

    void Start()
    {
        // on start find the terrain perlin script component on the mesh
        terrainPerlinSC = mesh.gameObject.GetComponent<TerrainPerlin>();
    }

    void Update()
    {
        // depending on the input key (up,down,left,right) move the terrain in the corresponding direction to generate new terrain
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            terrainPerlinSC.xOffset += 1f * speedSlider.value;  // moves the perlin noise to the left
            terrainPerlinSC.Generate();                         // regenerates terran with new noise
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            terrainPerlinSC.xOffset -= 1f * speedSlider.value;  // moves the perlin noise to the right
            terrainPerlinSC.Generate();                         // regenerates terran with new noise
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            terrainPerlinSC.yOffset += 1f * speedSlider.value;  // moves the perlin noise down
            terrainPerlinSC.Generate();                         // regenerates terran with new noise
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            terrainPerlinSC.yOffset -= 1f * speedSlider.value;  // moves the perlin noise up
            terrainPerlinSC.Generate();                         // regenerates terran with new noise
        }
    }
}
