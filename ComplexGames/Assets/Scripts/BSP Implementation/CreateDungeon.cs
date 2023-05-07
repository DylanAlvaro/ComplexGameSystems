using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDungeon : MonoBehaviour
{

    public int mapWidth = 50;
    public int mapDepth = 50;
    public int scale  = 2;

    private Leaf root;
    // Start is called before the first frame update
   private void Start()
    {
        root = new Leaf(0, 0, mapWidth, mapDepth, scale);
        BSP(root, 3);
        Debug.Log("calling bsp");
    }

   public void BSP(Leaf l, int sDepth)
    {
        if(l == null) return;
        if (sDepth <= 0)
        {
            l.Draw(0);
            
            return;
        }
        if(l.Split())
        {
            BSP(l.leftChild, sDepth  - 1);
            BSP(l.rightChild, sDepth - 1);
        }
        else
        {
            l.Draw(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
