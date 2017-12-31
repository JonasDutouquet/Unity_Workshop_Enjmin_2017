using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer (typeof(Axes))]
public class AxesDrawer : PropertyDrawer
{
	public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
	{
		SerializedProperty x = prop.FindPropertyRelative ("xAxis");
		SerializedProperty y = prop.FindPropertyRelative ("yAxis");
		SerializedProperty z = prop.FindPropertyRelative ("zAxis");

		EditorGUI.BeginProperty (pos, label, prop);

		//don't make the child fields be indented
		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		//Calculate rects
		Rect xRect = new Rect (pos.x, pos.y, 30, pos.height);
		Rect yRect = new Rect (pos.x + 35, pos.y, 30, pos.height);
		Rect zRect = new Rect (pos.x + 70, pos.y, 30, pos.height);

		//Draw fields (GUIContent.none -> no labels)
		EditorGUIUtility.labelWidth = 10f;
		EditorGUI.PropertyField (xRect, x, new GUIContent("X"));
		EditorGUI.PropertyField (yRect, y, new GUIContent("Y"));
		EditorGUI.PropertyField (zRect, z, new GUIContent("Z"));

		//Set indent back to what it was
		EditorGUI.indentLevel = indent;

		EditorGUI.EndProperty ();
	}
}
