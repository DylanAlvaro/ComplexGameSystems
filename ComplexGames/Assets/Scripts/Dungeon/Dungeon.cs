using Scripts.Dungeon;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Unity.Mathematics;

using UnityEngine;
using Random = UnityEngine.Random;

public class Dungeon : MonoBehaviour
{
    enum RoomType
    {
        None,
        Room,
        Corridors
    }
    // Important variables for making sure the generation works
    [SerializeField] public Vector3Int size;
    [SerializeField] public Vector3Int maxRoomSize;
    [SerializeField] public int roomCount;
    [SerializeField] public GameObject floorPrefab;
    [SerializeField] public GameObject wallPrefab;
    public int seed;
   // public int Seed => seed.GetHashCode();

    // private variables
    private bool addDifferentRooms = true;
    private bool canAddWalls = true;
    private List<Rooms> _rooms;
    private int roomDistance = 10;

    private int spacingBetween = 50;
    
    // Start is called before the first frame update
    void Start()
    {
        _rooms = new List<Rooms>();
        
        //calling functions to start
        PlaceRooms();
       // PlaceWall();
        Triangulation();
        seed = Random.Range(0, 99999);
    }

    /// <summary>
    /// Placing Room functionality goes here:
    /// The first step is to go through a list of set rooms put into the inspector and make where they
    /// are placed random (x, y, z) this is also done for the size of the rooms plus how high they can go up
    /// if the bool returns true it calls the Place Room function which takes in a location and size
    /// </summary>
    private void PlaceRooms()
    {
        for(int i = 0; i < roomCount; i++)
        {
            Vector3Int place = new Vector3Int(Random.Range(0, size.x + spacingBetween), 
                                              Random.Range(0, size.y),
                                              Random.Range(0, size.z + spacingBetween));
            
            // making random sized rooms based on a x, y and z coord
            Vector3Int sizeOfRooms = new Vector3Int(Random.Range(1, maxRoomSize.x + 1),
                                                    Random.Range(1, maxRoomSize.y + 1),
                                                    Random.Range(1, maxRoomSize.z + 1));
            
            Room newAddedRoom = new Room(place, sizeOfRooms);
            Room spacing = new Room(place + new Vector3Int(-1, 0, -1), sizeOfRooms + new Vector3Int(5, 0, 5));
            
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
               // _rooms.Add(newAddedRoom);
                PlaceRoom(newAddedRoom.bounds.position, newAddedRoom.bounds.size);
            }
        }
    }

    private void Triangulation()
    {
        // this is where we will be adding Dulanays Triangulation 
    }

   //private void PlaceWall()
   //{
   //    if(canAddWalls)
   //    {
   //        PlaceWall();
   //    }
   //}
    
    /// <summary>
    /// Function takes in a vector3 location and size
    /// Instantiates a prefab and sets its transform
    /// </summary>
    /// <param name="location"></param>
    /// <param name="size"></param>
    /// <param name="material"></param>
   private void PlaceCube(Vector3Int location, Vector3Int size)
    {
        GameObject gameObject = Instantiate(floorPrefab, location, Quaternion.identity);
        gameObject.GetComponent<Transform>().localScale = size;
    }
    private void PlaceWalls(Vector3Int location, Vector3Int size)
    {
        GameObject gameObject = Instantiate(wallPrefab, location, quaternion.identity);
        gameObject.GetComponent<Transform>().localScale = this.size;
    }

    /// <summary>
    /// calls the place cube function above
    /// </summary>
    /// <param name="location"></param>
    /// <param name="size"></param>
    private void PlaceRoom(Vector3Int place, Vector3Int size)
    {
        PlaceCube(place, size);
    }
    private void PlaceWall(Vector3Int place, Vector3Int size)
    {
        PlaceWalls(place, size);
    }
}
