using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using TMPro;

public class PackageExport : MonoBehaviour
{
    public GameObject savedMesh;            // references the mesh the script will be saving
    public TerrainPerlin terrainPerlinSC;   // references the terrain perlin script 

    public TMP_InputField sizeX, sizeY;     // references the custom terrain size the player inputs

    /// <summary>
    /// This function is used to save the terrain mesh as a prefab when the user requests it
    /// </summary>
    public void savePrefab()
    {
        terrainPerlinSC.exported = true;                                    // sets the terrain to exported mode
        terrainPerlinSC.size[0] = (int)float.Parse(sizeX.text);             // sets the terrain size to the new inputted size by the player
        terrainPerlinSC.size[1] = (int)float.Parse(sizeY.text);             // ^^
        terrainPerlinSC.water.transform.localScale = new Vector3(float.Parse(sizeX.text), float.Parse(sizeY.text), 0f);
        terrainPerlinSC.waterOrigin.transform.position = terrainPerlinSC.mesh.transform.position;

        string localPath = "Assets/Prefabs/" + savedMesh.name + ".prefab";  // creates a string with the correct path and name
        if (!Directory.Exists("Assets/Prefabs"))                            // if the path does not exist... create a prefabs folder
        {
            AssetDatabase.CreateFolder("Assets", "Prefabs");
        }
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
        
        bool prefabSuccess;                                                         // bool used to store the success of the export
        PrefabUtility.SaveAsPrefabAsset(savedMesh, localPath, out prefabSuccess);   // save the terrain mesh as a prefab 
        if (prefabSuccess == true)
        {
            Debug.Log("Prefab saved successfully");                                 // if the prefab saved successfully, it lets the user know via the debug.log
            terrainPerlinSC.exported = false;                                       // sets the terrain back to un-exported mode to continue letting the player edit modifers
            
            // the following lines of code reset the terrain and water quad sizing
            terrainPerlinSC.size[0] = 200;
            terrainPerlinSC.size[1] = 160;
            terrainPerlinSC.water.transform.localScale = new Vector3(200, 160, 0f);
            terrainPerlinSC.waterOrigin.transform.position = terrainPerlinSC.mesh.transform.position;
        }
        else
        {
            Debug.Log("Prefab failed to save");                                     // if the prefab saved unsuccessfully, it lets the user know via the debug.log
            terrainPerlinSC.exported = false;                                       // sets the terrain back to un-exported mode to continue letting the player edit modifers

            // the following lines of code reset the terrain and water quad sizing
            terrainPerlinSC.size[0] = 200;
            terrainPerlinSC.size[1] = 160;
            terrainPerlinSC.water.transform.localScale = new Vector3(200, 160, 0f);
            terrainPerlinSC.waterOrigin.transform.position = terrainPerlinSC.mesh.transform.position;
        }

    }
}
