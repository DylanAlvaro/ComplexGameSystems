using UnityEditor;

using UnityEngine;

namespace Scripts.Dungeon
{
#region Editor
#if UNITY_EDITOR
[CustomEditor(typeof(Pathfinder.Pathfinder))]	
	public class DungeonEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			Pathfinder.Pathfinder pathfinder = (Pathfinder.Pathfinder) target;
			
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