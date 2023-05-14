using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCreate : MonoBehaviour
{
    // Dungeon Variables
    public int dungeonWidth;
    public int dungeonLength;
    public int roomWidthMin;
    public int roomLengthMin;
    public int maxIterations;
    public int corridorWidth;
    
    // Start is called before the first frame update
    void Start()
    {
        CreateDungeon();
    }

    private void CreateDungeon()
    {
        DungeonGen generator = new DungeonGen(dungeonWidth, dungeonLength);
        var listOfRooms = generator.CalculateRooms(maxIterations, roomWidthMin, roomLengthMin);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
