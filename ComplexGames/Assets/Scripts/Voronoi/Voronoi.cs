using Scripts.Voronoi;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class Voronoi : MonoBehaviour
{
    public Vector2Int imageDim;
    public int regionAmount;
    public bool drawByDistance = false;
    public int seeds;
    public Vector3 gameObject;
    private void Start()
    {
        //depending on if the bool in the inspector is ticked or not determines what function is run 
        GetComponent<SpriteRenderer>().sprite = Sprite.Create((drawByDistance ? GetDiagramByDistance() : GetDiagram()), 
                                                              new Rect(0, 0, imageDim.x, imageDim.y),
                                                              Vector2.one * .5f);
    }

    Texture2D GetDiagram()
    {
        Vector2Int[] centroids = new Vector2Int[regionAmount];
        Color[] regions = new Color[regionAmount];
       
        
        Random.InitState(seeds);
        for(int i = 0; i < regionAmount; i++)
        {
            centroids[i] = new Vector2Int(Random.Range(0, imageDim.x), Random.Range(0, imageDim.y));
            regions[i] = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f );
        }

        Color[] pixelColors = new Color[imageDim.x * imageDim.y];
        for(int y = 0; y < imageDim.x; y++)
        {
            for(int x = 0; x < imageDim.y; x++)
            {
                int index = x * imageDim.x + y;
                pixelColors[index] = regions[GetClosetCentroidIndex(new Vector2Int(x, y), centroids)];
            }
        }
        return GetImageFromColorArray(pixelColors);
    }

    Texture2D GetDiagramByDistance()
    {
        Vector2Int[] centroids = new Vector2Int[regionAmount];
        for(int i = 0; i < regionAmount; i++)
        {
            centroids[i] = new Vector2Int(Random.Range(0, imageDim.x), Random.Range(0, imageDim.y));
        }

        Color[] pixelColors = new Color[imageDim.x * imageDim.y];
        float[] distances = new float[imageDim.x * imageDim.y];
        for(int y = 0; y < imageDim.x; y++)
        {
            for(int x = 0; x < imageDim.y; x++)
            {
                int index = x * imageDim.x + y;
                distances[index] = Vector2.Distance(new Vector2Int(x, y), centroids[GetClosetCentroidIndex(new Vector2Int(x, y), centroids)]);
            }
        }

        float maxDist = GetMaxDistance(distances);
        for(int i = 0; i < distances.Length; i++)
        {
            float colorVal = distances[i] / maxDist;
            pixelColors[i] = new Color(colorVal, colorVal, colorVal, 1f);
        }
        return GetImageFromColorArray(pixelColors);
    }

    float GetMaxDistance(float[] distances)
    {
        float maxDist = float.MinValue;
        for(int i = 0; i < distances.Length; i++)
        {
            if(distances[i] > maxDist)
            {
                maxDist = distances[i];
            }
        }

        return maxDist;
    }

    int GetClosetCentroidIndex(Vector2Int pixelPos, Vector2Int[] centroids)
    {
        float smallestDst = float.MaxValue;
        int index = 0;
        for(int i = 0; i < centroids.Length; i++)
        {
            if(Vector2.Distance(pixelPos, centroids[i]) < smallestDst)
            {
                smallestDst = Vector2.Distance(pixelPos, centroids[i]);
                index = i;
            }
        }
        return index;
    }

    Texture2D GetImageFromColorArray(Color[] pixelColors)
    {
        Texture2D tex = new Texture2D(imageDim.x, imageDim.y);
        tex.filterMode = FilterMode.Point;
        tex.SetPixels(pixelColors);
        tex.Apply();
        return tex;
    }
}
