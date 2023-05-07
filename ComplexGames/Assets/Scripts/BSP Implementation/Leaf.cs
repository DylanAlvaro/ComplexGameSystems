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
   private int roomMin = 5;

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

   public bool Split()
   {
      if (width <= roomMin || depthh <= roomMin) return false;

      bool splitHorizontal = Random.Range(0, 100) > 50;
      if (width > depthh && width / depthh >= 1.2)
         splitHorizontal = false;
      else if (depthh > width && depthh / width >= 1.2)
         splitHorizontal = true;

      int max = (splitHorizontal ? depthh : width) - roomMin;
      if (max <= roomMin)
         return false;
      
      if(splitHorizontal)
      {
         int l1Depth = Random.Range(roomMin, max);
         leftChild = new Leaf(xpos, zpos, width, l1Depth, scale);
         rightChild = new Leaf(xpos, zpos + l1Depth, width, depthh - l1Depth, scale);
      }
      else
      {
         int l1Width = Random.Range(roomMin, max);
         leftChild = new Leaf(xpos, zpos, l1Width, depthh, scale);
         rightChild = new Leaf(xpos + l1Width, zpos , width, l1Width - depthh, scale);
      }
      
      return true;
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
