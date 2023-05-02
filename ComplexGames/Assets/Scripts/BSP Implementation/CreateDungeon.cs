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
        // root.Draw();
    }

    private void BSP(Leaf l, int sDepth)
    {
        if(l == null) return;
        if(sDepth <= 0) return;
      //  l.Draw(sDepth);
        if(l.Split(sDepth))
        {
            BSP(l.leftChild, sDepth  - 1);
            BSP(l.rightChild, sDepth - 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
