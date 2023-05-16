﻿using System;

using UnityEngine;

namespace Scripts.Dungeon
{
	public class Room : MonoBehaviour
	{
		public BoundsInt bounds;
		private int roomDistance = 5;

		public Room(Vector3Int place, Vector3Int size)
		{
			bounds = new BoundsInt(place, size);
		}

		public static bool RoomsIntersecting(Room room1, Room room2)
		{
			return !(room1.bounds.position.x >= room2.bounds.position.x && room1.bounds.x <= room2.bounds.x ||
			       room1.bounds.y >= room2.bounds.y && room1.bounds.y <= room2.bounds.y ||
			       room1.bounds.z >= room2.bounds.z && room1.bounds.z <= room2.bounds.z);
		}
		
		public static bool RoomBounds(Room r1, Vector3Int size)
		{
			return (r1.bounds.xMin < 0 || r1.bounds.xMax >= size.x || 
			        r1.bounds.yMin < 0 || r1.bounds.yMax >= size.y ||
			        r1.bounds.zMin < 0 || r1.bounds.zMax >= size.z);
		}
	}
}