using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSPLeaf : MonoBehaviour
{
    private int minLeafSize = 50;
    public BSPLeaf leftChild;
    public BSPLeaf rightChild;
    public bool hasRoom;
    public Vector2 roomSize;
    public Vector2 roomPos;

    public int x, y, width, height;
    private GameObject quad;

    private Renderer _renderer;
    
    // Start is called before the first frame update
    void Start()
    {
	  
    }
    
    public BSPLeaf(int _x, int _y, int _width, int _height)
    {
        x = _x;
        y = _y;
        width = _width;
        height = _height;
        quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.transform.position = new Vector3(x + (width * 0.5f), y + (height * 0.5f), 0f);
        quad.transform.localScale = new Vector3(width, height, 1f);

        Color randColor = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1f);
        quad.GetComponent<Renderer>().material.color = randColor;
    }

    public bool split()
    {
	    if(leftChild != null || rightChild != null)
		    return false;

	    bool splitH = Random.Range(0f, 1f) > 0.5f;
	    if(width > height && width / height >= .5f)
	    {
		    splitH = false;
	    }
	    else if(height > width && height / width >= .5f)
		    splitH = true;

	    int max = (splitH ? height : width) - minLeafSize;

	    if(max <= minLeafSize)
		    return false;

	    int split = Random.Range(minLeafSize, max);

	    if(splitH)
	    {
		    leftChild = new BSPLeaf(x, y, width, split);
		    rightChild = new BSPLeaf(x, y + split, width, height - split);
	    }
	    else
	    {
		    leftChild = new BSPLeaf(x, y, split, height);
		    rightChild = new BSPLeaf(x + split, y, width - split, height);
	    }

	    if(quad != null)
	    {
		    GameObject.Destroy(quad);
		    quad = null;
	    }

	    return true;
    }

    private void CreateRooms()
    {
	    
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
