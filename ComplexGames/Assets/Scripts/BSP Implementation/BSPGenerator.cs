using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSPGenerator : MonoBehaviour
{
    private int maxLeafSize = 20;
    private List<BSPLeaf> _leaves = new List<BSPLeaf>();
    private BSPLeaf root;

    // Start is called before the first frame update
    void Start()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        root = new BSPLeaf(0, 0, 400, 400);
        _leaves.Add(root);

        bool didSplit = true;

        while(didSplit)
        {
            didSplit = false;

            for(int i = 0; i < _leaves.Count; i++)
            {
                if(_leaves[i].leftChild == null && _leaves[i].rightChild == null)
                {
                    // if its too big
                    if(_leaves[i].width > maxLeafSize || _leaves[i].height > maxLeafSize || Random.Range(0, 1) > .25f)
                    {
                        if(_leaves[i].split())
                        {
                            _leaves.Add(_leaves[i].leftChild);
                            _leaves.Add(_leaves[i].rightChild);
                            didSplit = true;
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
