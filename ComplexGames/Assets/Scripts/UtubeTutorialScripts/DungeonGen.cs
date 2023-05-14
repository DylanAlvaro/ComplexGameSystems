using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtubeTutorialScripts;

public class DungeonGen : MonoBehaviour
{
    private List<RoomNode> allSpaceNodes = new List<RoomNode>();
    private int dungeonWidth;
    private int dungeonLength;

    public DungeonGen(int dungeonWidth, int dungeonLength)
    {
        this.dungeonWidth = dungeonWidth;
        this.dungeonLength = dungeonLength;
    }
    
    public List<Node> CalculateRooms(int maxIterations, int roomWidthMin, int roomLengthMin)
    {
        BinarySpaceParition bsp = new BinarySpaceParition(dungeonWidth, dungeonLength);
        allSpaceNodes = bsp.PrepareNodesCollection(maxIterations, roomWidthMin, roomLengthMin);
        return new List<Node>(allSpaceNodes);
    }
}
