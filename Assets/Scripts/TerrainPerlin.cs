using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TerrainPerlin : MonoBehaviour
{
    #region VARIABLES
    public GameObject mesh;
    [SerializeField] MeshFilter meshfilter; 
    [SerializeField] public Vector2Int size;       // controls size of mesh (number of vertices by x and y)
    public Vector3[] vertices;              // holds every vertice generated for the mesh

                                            // references the sliders used to allow the user to change values
    public Slider amplitudeSlider, frequencySlider, scaleSlider, heightSlider, waterLevelSlider, valleysSlider,
                  mapSlider, coldSlider, persistanceSlider, lacunaritySlider, worldZoomSlider, islandTypeSlider, islandIntensitySlider, cliffLimitSlider, cliffFlatnessSlider;

                                            // floats to hold the values for calculations
    public float amplitude, frequency, scale, height, alteredHeight, waterLevel, valleys,
                 heightLimit, map, cold, worldZoom, islandType, islandIntensity, cliffLimit, cliffFlatness;

    float tempAmp, tempFreq;                // a temporary amplitude and frequency of terrain that is used and alterated to calculate octaves instead of directly altering amp and freq

    #region UNUSED
    float finalAmp, finalFreq, finalPer, finalLac;

    //Texture2D texture;
    //Color perlinColour;
    //private float tempScale, scaleChange, newHeight;
    //public int octaveAmount;
    #endregion

    public float xOffset, yOffset;          // two values to hold the position of the psuedo random noise map we use
   
    public float lacunarity = 2;            // used to calculate the distribution of holes
    public float persistance = 0.5f;        // used to determine the strength of each layered octave

    //public float numOfOctaves;
    private float[] octaves = new float[6]; // array to hold 3 different noise maps, octaves
    private float mergedOctaves;            // holds the merge of the above array
                                            //int oct = 0;

    /*  variables used to calculate the positon of the vertice and apply island formula,
        distance being the vertices distance from the edge of the map
        nx and ny range from +1 to -1 and is used to calculate distance
        e is elevation of the vertice                                                   */
    public float distance, nx, ny, e;

    public GameObject water, waterOrigin;    // references a quad used which appears as water
    public Material biomes;     // references the material used to apply colour determined by height

    public bool exported;       // determines if the terrain has been exported or not
    #endregion

    private void Start()
    {
        exported = false;
        #region OLD
        //amplitudeSlider = amplitudeSlider.GetComponent<Slider>();   // grabs the slider component and assigns is to the corresponding var
        //frequencySlider = frequencySlider.GetComponent<Slider>();   // ^^
        //scaleSlider = scaleSlider.GetComponent<Slider>();           // ^^
        //heightSlider = heightSlider.GetComponent<Slider>();         // ^^
        //waterLevelSlider = waterLevelSlider.GetComponent<Slider>(); // ^^
        //valleysSlider = valleysSlider.GetComponent<Slider>();       // ^^
        //octaves = new float[Mathf.RoundToInt(numOfOctaves)];
        #endregion

        if (!exported)
        {
            try
            {
                mesh = GameObject.Find("terrain");
                #region OLD
                //valleysSlider.GetComponent<Slider>().value = 0.5f;
                //heightSlider.GetComponent<Slider>().value = 1f;
                //frequencySlider.GetComponent<Slider>().value = 0.06f;
                //finalAmp = amplitude;
                //finalFreq = frequency;
                #endregion
                Generate();                         // Generates terrain once at start and only updates when slider values are changed
            }
            catch (NullReferenceException)
            {
                exported = true;
            }
        }
        if (exported)
        {
            #region OLD
            //finalAmp = amplitude;
            //finalFreq = frequency;
            //amplitude = finalAmp;
            //frequency = finalFreq;
            #endregion
            Generate();
        }
    }

    public void Generate()
    {
        Debug.Log("generate called");
        if (!exported)
        {
            try
            {
                scale = scaleSlider.value;                                  // sets the slider component value to float
                frequency = frequencySlider.value;                          // ^^
                amplitude = amplitudeSlider.value;                          // ^^
                height = heightSlider.value;                                // ^^
                waterLevel = waterLevelSlider.value;                        // ^^
                valleys = valleysSlider.value;                              // ^^
                map = mapSlider.value;                                      // ^^  
                cold = coldSlider.value;                                    // ^^
                worldZoom = worldZoomSlider.value;                          // ^^
                islandType = islandTypeSlider.value;                        // ^^
                cliffLimit = cliffLimitSlider.value;                        // ^^
                cliffFlatness = cliffFlatnessSlider.value;                  // ^^

                islandIntensity = Convert.ToInt32(islandIntensitySlider.value); // converts island intensity to integer incase user inputs string
                
                mesh.transform.position = new Vector3(mesh.transform.position.x, map, mesh.transform.position.z);
                biomes.SetFloat("_SnowGrassThreshold", cold);

                #region BALANCING SCALE
                // this code helps balance out sliders when the user changes scale
                float oldScale = this.gameObject.transform.localScale.x;
                this.gameObject.transform.localScale = new Vector3(1 * worldZoom, 1 * worldZoom, 1 * worldZoom);
                float newScale = this.gameObject.transform.localScale.x;

                // if the user increases the scale 
                if (newScale > oldScale)    
                {
                    mapSlider.value -= .05f;    // lower map height slider
                    coldSlider.value += .01f;   // increase cold slider slightly
                }
                // if the user decreases the scale
                else if (newScale < oldScale)
                {
                    mapSlider.value += .05f;    // increase map height slider
                    coldSlider.value -= .01f;   // decrease the cold slider slightly
                }
                #endregion
            }
            catch (NullReferenceException)
            {
                exported = true;
            }

            // sets the y pos of the water quad to the slider value
            water.transform.position = new Vector3(water.transform.position.x, waterLevel, water.transform.position.z);
            
        }
        meshfilter = GetComponent<MeshFilter>();        // grabs the mesh filter component before generation
        meshfilter.mesh.vertices = CreateVertices();    // calls the creates vertices function for part 1 of mesh gen
        meshfilter.mesh.triangles = CreateTriangles();  // then calls create triangle function for part 2 of mesh gen where it takes shape
        meshfilter.mesh.RecalculateNormals();           // after modification of normals, recalculate to reflect the change
    }

    /// <summary>
    /// This function loops through each x, y vertice
    /// and generates its position and elevation
    /// </summary>
    /// <returns></returns>
    private Vector3[] CreateVertices()
    {
        // calculating the number of vertices
        vertices = new Vector3[(size.x + 1) * (size.y + 1)];

        // looping through each x,y position on the mesh
        for (int i = 0, y = 0; y <= size.y; y++)
        {
            for (int x = 0; x <= size.x; x++)
            {
                // if terrain has NOT been exported then do...
                if (!exported)
                {
                    // re-defining the tempVars for Octave calculation prevents infinity, as value needs to be reset
                    tempAmp = amplitudeSlider.value;
                    tempFreq = frequencySlider.value;
                    persistance = persistanceSlider.value;
                    lacunarity = lacunaritySlider.value;

                    //finalAmp = tempAmp;
                    //finalFreq = tempFreq;
                }
                // if terrain HAS been exported then do...
                if (exported)
                {
                    tempAmp = amplitude;
                    Debug.Log("final amp" + tempAmp);
                    tempFreq = frequency;
                    Debug.Log("final freq" + tempFreq);
                }

                // ISLANDS
                nx = 2f * x / size.x - 1f;  // creates a version of x and y which ranges from -1 to +1
                ny = 2f * y / size.y - 1f;  // these are used to calculate island shape

                #region NOT WORKING
                //// simplfied code
                //for (int oct = 0; oct < numOfOctaves; oct++)
                //{
                //    //generates a new octave
                //    octaves[oct] = amplitude * (Mathf.PerlinNoise(x / scale * frequency + xOffset, y / scale * frequency + yOffset));
                //    amplitude *= persistance;                               // modify amplitude by multiplying itself by persistance (float below 1 so amp becomes lower)
                //    frequency *= lacunarity;                                // modify freqency by multiplying itself by lacunarity (float above 1 so freq becomes larger)
                //    Debug.Log("Generated octave" + oct);
                //}
                //for (int mergeOct = 0; mergeOct < numOfOctaves; mergeOct++)
                //{
                //    mergedOctaves += octaves[mergeOct];
                //}
                #endregion
 
                #region OCTAVES
                octaves[0] = tempAmp * (Mathf.PerlinNoise(x / scale * tempFreq + xOffset, y / scale * tempFreq + yOffset)); // generates the first octave with unmodified amplitude and freq
                tempAmp *= persistance;                                                                                     // modify amplitude by multiplying itself by persistance (float below 1 so amp becomes lower)
                tempFreq *= lacunarity;                                                                                     // modify freqency by multiplying itself by lacunarity (float above 1 so freq becomes larger)

                // generates the second octave to go on top octave 1, but it has less affect to the overall view
                octaves[1] = tempAmp * (Mathf.PerlinNoise(x / scale * tempFreq + xOffset, y / scale * tempFreq + yOffset));
                tempAmp *= persistance;
                tempFreq *= lacunarity;

                // generates the third octave to go on top both octave 1 and 2, but it has even less strength and affect to the overall view
                octaves[2] = tempAmp * (Mathf.PerlinNoise(x / scale * tempFreq + xOffset, y / scale * tempFreq + yOffset));
                tempAmp *= persistance;
                tempFreq *= lacunarity;

                // fourth octave, and so on.
                octaves[3] = tempAmp * (Mathf.PerlinNoise(x / scale * tempFreq + xOffset, y / scale * tempFreq + yOffset));
                tempAmp *= persistance;
                tempFreq *= lacunarity;

                // fifth octave, and so on.
                octaves[4] = tempAmp * (Mathf.PerlinNoise(x / scale * tempFreq + xOffset, y / scale * tempFreq + yOffset));
                tempAmp *= persistance;
                tempFreq *= lacunarity;

                // sixth octave, and so on.
                octaves[5] = tempAmp * (Mathf.PerlinNoise(x / scale * tempFreq + xOffset, y / scale * tempFreq + yOffset));

                // merging all the octaves into one var for elevation calculation, adding multiple octaves together creates more realistic looking terrain
                mergedOctaves = octaves[0] + octaves[1] + octaves[2] + octaves[3] + octaves[4] + octaves[5];
                #endregion

                e = 0f;
                // calculates e, the elevation, with 6 octaves to the power of valleys intensity, multiplied by height modifier
                e = mergedOctaves /= (Mathf.Pow(mergedOctaves, valleys) * height);    

                if (e > cliffLimit)                             // if elevation is above the height limit for cliffs then...
                {
                    e = cliffLimit + (e / cliffFlatness) - 10;  // round the elevation down to keep the shape but lower its amplitude
                }

                e = CalculateShape(islandType, 1);              // calculates an island shape and alters elevation

                vertices[i] = new Vector3(x, e, y);             // set current vertice to corresponding perlin height using the merged octave and valley redistribution
                i++;                                            // move to next vertice
            }
        }
        return vertices;
    }

    /// <summary>
    /// Connects every 3 vertices together with a triangle
    /// </summary>
    /// <returns></returns>
    private int[] CreateTriangles()
    {
        int[] triangles = new int[size.x * size.y * 6];

        for (int z = 0, vert = 0, tris = 0; z < size.y; z++)
        {
            for (int x = 0; x < size.x; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + size.x + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + size.x + 1;
                triangles[tris + 5] = vert + size.x + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
        return triangles;
    }

    /// <summary>
    /// This function takes the type of island and a strength to apply it,
    /// it then recalculates elevation to visualise the island formula.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="intensityStrength"></param>
    /// <returns></returns>
    public float CalculateShape(float type, float intensityStrength)
    {
        switch (type)
        {
            // will apply no island shape
            case 0:
                distance = 0;
                break;
                
            // applies a squared circle shape
            case 1:
                distance = 1f - (1f - Mathf.Pow(nx, 2f)) * (1f - Mathf.Pow(ny, 2f));
                break;

            // applies a circle shape
            case 2:
                distance = Mathf.Min(1, (Mathf.Pow(nx, 2f) + Mathf.Pow(ny, 2f)) / Mathf.Sqrt(2f));
                break;

            // applies a lake shape
            case 3:
                distance = 1 - Mathf.Sqrt(Mathf.Pow(nx, 2f) + Mathf.Pow(ny, 2f));
                break;

            // applies a diagonal shape
            case 4:
                distance = 1 - Mathf.Sqrt((0.1f + Mathf.Pow(nx, 2f) * Mathf.PI / 2) * (0.1f + Mathf.Pow(ny, 2f) * Mathf.PI / 2f));
                break;

            // another lake
            case 5:
                distance = 1f - Mathf.Sqrt(Mathf.Pow(nx, 2f) + Mathf.Pow(ny, 2f) + Mathf.Pow(0.2f, 2f));
                break;
        }

        // calculates a new elevation for each vertice
        e = (e + ((1f - distance) * (islandIntensity / intensityStrength))) / 2f;
        return e;
    }

    #region UNUSED
    //public void RecalculateHeight()
    //{
    //    height = 
    //}

    //    float CalculatePerlin(int x, int y)
    //    {
    //        float xCoord = x + xOffset;
    //        float yCoord = y + yOffset;

    //        perlinNoiseY = Mathf.PerlinNoise(xCoord, yCoord);

    //        perlinColour = new Color(perlinNoiseY, perlinNoiseY, perlinNoiseY);

    //        return perlinNoiseY;
    //    }
    #endregion
}