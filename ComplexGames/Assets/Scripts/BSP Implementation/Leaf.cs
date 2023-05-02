using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
   private int xpos;
   private int zpos;
   private int width;
   private int depthh;
   private int scale;

   public Leaf leftChild;
   public Leaf rightChild;
   public Leaf(int x, int z, int w, int d, int s)
   {
      xpos = x;
      zpos = z;
      width = w;
      depthh = d;
      scale = s;
   }

   public bool Split(int level)
   {
      if(Random.Range(0, 100) < 50)
      {
         int l1Depth = Random.Range((int) (depthh * .1f), (int) (depthh * .7f));
         leftChild = new Leaf(xpos, zpos, width, l1Depth, scale);
         rightChild = new Leaf(xpos, zpos + l1Depth, width, depthh - l1Depth, scale);
      }
      else
      {
         int l1Width = Random.Range((int) (width * .1f), (int) (width * .7f));
         leftChild = new Leaf(xpos, zpos, l1Width, depthh, scale);
         rightChild = new Leaf(xpos + l1Width, zpos , width, l1Width - depthh, scale);
      }

      leftChild.Draw(level);
      rightChild.Draw(level);
      
      return true;
      // Leaf l1 = new Leaf(0, 0, mapWidth, l1Depth, scale);
      // Leaf l2 = new Leaf(0, l1Depth, mapWidth, mapDepth - l1Depth, scale);
   }
   
   public void Draw(int level)
   {
      Color c = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
      
      for(int x = xpos; x < width + xpos; x++)
      {
         for(int z = zpos; z < depthh + zpos; z++)
         {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(x * scale, level * 3, z * scale);
            cube.transform.localScale = new Vector3(scale, scale, scale);
            cube.GetComponent<Renderer>().material.SetColor("_Color", c);
         }
      }
   }
}
