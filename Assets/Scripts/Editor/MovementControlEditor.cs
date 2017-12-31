using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MovementControl))]
public class MovementControlEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector ();
	}
}
