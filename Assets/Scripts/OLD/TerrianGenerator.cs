using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerrianGenerator : MonoBehaviour
{
    Mesh mesh;
    //MeshFilter meshfilter;
    [SerializeField] Vector2Int size;
    [SerializeField] float Amplitude;
    public Slider amplitudeSlider;
    //public CustomPerlin customPerlinSC;
    public float colourSample, scale;

    public int texWidth, texHeight;

    public float xOffset, yOffset;
    //private float height = 1f;
    //private int octaves = 3;
    //private float lacunarity = 2f;
    //private float persistence = .5f;
    //private float waterLevel = .6f;

    void Start()
    {
        Generate();
        //GenerateTexture();
    }

    public void Generate()
    {
        Mesh mesh = new Mesh();
        //meshfilter = GetComponent<MeshFilter>();
        GetComponent<MeshFilter>().mesh = mesh;

        GenerateTexture();
        Amplitude = amplitudeSlider.value;

        mesh.vertices = CreateVertices();
        mesh.triangles = CreateTriangles();
        mesh.RecalculateNormals();
    }

    private Vector3[] CreateVertices()
    {
        Vector3[] vertices = new Vector3[(size.x + 1) * (size.y + 1)];

        for (int i = 0, z = 0; z <= size.y; z++)
        {
            for (int x = 0; x <= size.x; x++)
            {
                //add perlin noise
                // grab sample float to use for y value from custom perlin script
                //GenerateTexture();
                //float yAltered = colourSample * Amplitude;

                float yAltered = Mathf.PerlinNoise(x * .3f, z * .3f);

                //Color newColour = new Color(yAltered, yAltered, yAltered);

                //Texture2D texture = new Texture2D(texWidth, texHeight);

                //for (int xTex = 0; xTex < texWidth; xTex++)
                //{
                //    for (int yTex = 0; yTex < texHeight; yTex++)
                //    {
                //        texture.SetPixel(xTex, yTex, newColour);
                //    }
                //}
                //texture.Apply();

                //float yAltered = CalculateHeight(x, z);

                //Texture2D texture = new Texture2D(size.x, size.y);

                //for (int width = 0; width < size.x; width++)
                //{
                //    for (int height = 0; height < size.y; height++)
                //    {
                //        Color colour = CalculateColour(width, height);
                //        texture.SetPixel(width, height, colour);
                //    }
                //}
                //texture.Apply();

                //float yAltered = SampleNoise(x, z);

                vertices[i] = new Vector3(x, yAltered * Amplitude, z);
                i++;
            }
        }
        return vertices;
    }

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

    //public float CalculateHeight(float xPos, float zPos)
    //{
    //    float yPos = 0;

    //    float persistenceModifierWithHeight = height;
    //    float xPosWithOffsetScaledAndLacunarity = (xPos + xOffset * size.x) * Amplitude;
    //    float zPosWithOffsetScaledAndLacunarity = (zPos + zOffset * size.z) * Amplitude;
    //    for (int i = 0; i < octaves; i++, xPosWithOffsetScaledAndLacunarity *= lacunarity, zPosWithOffsetScaledAndLacunarity *= lacunarity, persistenceModifierWithHeight *= persistence)
    //    {
    //        // At each octave, calculate xz co-ords based on the perlin scale and the lacunarity.
    //        float compoundX = xPosWithOffsetScaledAndLacunarity;
    //        float compoundZ = zPosWithOffsetScaledAndLacunarity;
    //        // Calculate y position using these modified xz co-ords, as well as the height and persistence parameters.
    //        yPos += Mathf.PerlinNoise(compoundX, compoundZ) * persistenceModifierWithHeight;
    //    }
    //    return yPos;
    //}

    float SampleNoise(int x, int y)
    {
        float xCoord = (float)x / texWidth * scale + xOffset;
        float yCoord = (float)y / texHeight * scale + yOffset;
        float vertHeight = Mathf.PerlinNoise(xCoord, yCoord);
        Debug.Log(vertHeight);
        return vertHeight;
        
    }

    //public void UpdateMesh()
    //{
    //    mesh.Clear();

    //    mesh.vertices = CreateVertices();
    //    mesh.triangles = CreateTriangles();

    //    mesh.RecalculateNormals();
    //}

    

    //private void Update()
    //{
    //    Renderer renderer = GetComponent<Renderer>();
    //    renderer.material.mainTexture = GenerateTexture();
    //}

    //public void ApplyTexture()
    //{
    //    Renderer renderer = GetComponent<Renderer>();
    //    renderer.material.mainTexture = GenerateTexture();
    //}

    // not required
    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(texWidth, texHeight);

        for (int x = 0; x < texWidth; x++)
        {
            for (int y = 0; y < texHeight; y++)
            {
                Color colour =  CalculateColour(x, y);
                texture.SetPixel(x, y, colour);
            }
        }
        texture.Apply();
        return texture;
    }

    Color CalculateColour(int x, int y)
    {
        float xCoord = (float)x / texWidth * scale;
        float yCoord = (float)y / texHeight * scale;

        colourSample = Mathf.PerlinNoise(xCoord, yCoord);
        Debug.Log(colourSample);
        return new Color(colourSample, colourSample, colourSample);
    }

    
}
