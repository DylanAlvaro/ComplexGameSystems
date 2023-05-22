using System;
using Scripts.Dungeon;
using Scripts.Dungeon.Algorithms;
using Scripts.Dungeon.Pathfinder;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;

using UnityEditor;

using UnityEngine;
using Random = UnityEngine.Random;

public class Dungeon : MonoBehaviour
{
    // Important variables for making sure the generation works
    [SerializeField] public Vector3Int size;
    [SerializeField] public Vector3Int maxRoomSize;
    [SerializeField] public int roomCount;
    [SerializeField] public GameObject floorPrefab;
    [SerializeField] public GameObject wallPrefab;
    public int seed;

    // private variables
    private bool addDifferentRooms = true;
    private bool canAddWalls = true;
    private List<Rooms> _rooms;
    private int roomDistance = 10;
    private int _spacingBetween = 50;

    private List<Point> _verts;

    //public variables
    public DelaunayTri DelaunayTri;
    public PrimsAlgorithm PrimsAlgorithm;
    
    // Start is called before the first frame update
    void Start()
    {
        _rooms = new List<Rooms>();
        _verts = new List<Point>();
        seed = Random.Range(0, 99999);
        
        //calling functions to start
        CreateRooms();
        Triangulation();
    }

    /// <summary>
    /// Placing Room functionality goes here:
    /// The first step is to go through a list of set rooms put into the inspector and make where they
    /// are placed random (x, y, z) this is also done for the size of the rooms plus how high they can go up
    /// if the bool returns true it calls the Place Room function which takes in a location and size
    /// </summary>
    private void CreateRooms()
    {
        for(int i = 0; i < roomCount; i++)
        {
            Vector3Int position = new Vector3Int(Random.Range(0, size.x + _spacingBetween),
                                                 Random.Range(0, size.y),
                                              Random.Range(0, size.z + _spacingBetween));
            
            //_verts.Add(new Point(position.x, position.y));
            // making random sized rooms based on a x, y and z coord
            Vector3Int sizeOfRooms = new Vector3Int(Random.Range(1, maxRoomSize.x + 1),
                                                    Random.Range(0, maxRoomSize.y + 1),
                                                    Random.Range(1, maxRoomSize.z + 1));
            
            Room newAddedRoom = new Room(position, sizeOfRooms);
            Room spacing = new Room(position + new Vector3Int(-1, 0, -1), sizeOfRooms + new Vector3Int(5, 0, 5));
            
           
            //This should hopefully make sure that the cubes (rooms) aren't overlapping
          
            foreach(Rooms room1 in _rooms) 
            {
               if(Room.RoomsIntersecting(newAddedRoom, spacing))
               {
                   addDifferentRooms = false;
                   break;
               }
               
               if(newAddedRoom.bounds.xMin < 0 || newAddedRoom.bounds.xMax >= size.x || 
                  newAddedRoom.bounds.yMin < 0 || newAddedRoom.bounds.yMax >= size.y ||
                  newAddedRoom.bounds.zMin < 0 || newAddedRoom.bounds.zMax >= size.z) {
                   addDifferentRooms = false; 
               } 
            } 
            
            if(addDifferentRooms)
            {
                PlaceRoom(newAddedRoom.bounds.position, newAddedRoom.bounds.size);
            }
        }
    }

    private void Triangulation()
    {
        //delaunay implementation
        
        
        // prims implementation
       // Vertex[] vertices = PrimsAlgorithm.FindMst(_rooms);

       // foreach(Vertex v in vertices)
       // {
       //     if(v.Parent >= roomCount)
       //     {
       //         
       //     }
       // }
    }


    /// <summary>
    /// This function will pathfind through the rooms and create links between them
    /// </summary>
    void AstarCorridors()
    {
        var startRoom = _rooms.Add();
        var endRoom = _rooms[i + 1];

        // Use your A* pathfinding script to find the shortest path
        Pathfinder pathfinder = new Pathfinder();
        pathfinder.FindPath(startRoom, endRoom);

       // foreach(Vertex vertex in )
       // {
       //     foreach(Rooms room in _rooms)
       //     {
       //         
       //     }
       // }
    }
    
    
    
    /// <summary>
    /// Function takes in a vector3 location and size
    /// Instantiates a prefab and sets its transform
    /// </summary>
    /// <param name="location"></param>
    /// <param name="size"></param>
    /// <param name="material"></param>
   
    private void PlaceRoomFloors(Vector3Int location, Vector3Int size)
    {
        GameObject gameObject = Instantiate(floorPrefab, location, Quaternion.identity);
        gameObject.GetComponent<Transform>().localScale = size;
    }
    /// <summary>
    /// calls the place cube function above
    /// </summary>
    /// <param name="location"></param>
    /// <param name="size"></param>
    private void PlaceRoom(Vector3Int place, Vector3Int size)
    {
        PlaceRoomFloors(place, size);
    }
    
#region Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(Pathfinder))]	
    public class DungeonEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Pathfinder pathfinder = (Pathfinder) target;
			
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
			
            EditorGUILayout.LabelField("Generate Delaunay Dungeon");
            EditorGUILayout.LabelField("Generate BSP Dungeon");

            EditorGUILayout.EndHorizontal();
        }
    }
#endif
#endregion

}