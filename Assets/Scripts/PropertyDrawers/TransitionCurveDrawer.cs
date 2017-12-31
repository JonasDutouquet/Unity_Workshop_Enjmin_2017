using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer (typeof(TransitionCurve))]
public class TransitionCurveDrawer : PropertyDrawer
{
	public override void OnGUI(Rect pos, SerializedProperty prop, GUIContent label)
	{
		SerializedProperty isInstant = prop.FindPropertyRelative ("transition");
		SerializedProperty curve = prop.FindPropertyRelative ("transitionCurve");

		EditorGUI.BeginProperty (pos, label, prop);
		//don't make the child fields be indented
		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 1;

		GUIContent test = new GUIContent ("Transition", "Check to assign a transition curve.\n Leave unchecked if this state is instantaneous.");

		//draw label
		pos = EditorGUI.PrefixLabel (pos, GUIUtility.GetControlID (FocusType.Passive), test);
		EditorGUI.indentLevel = 0;
		//Calculate rects
		Rect isInstantRect = new Rect(pos.x, pos.y, 10, pos.height);
		Rect curveRect = new Rect (pos.x + pos.width/4, pos.y, 3*pos.width/4, pos.height);

		//Draw fields (GUIContent.none -> no labels)
		EditorGUI.PropertyField (isInstantRect, isInstant, GUIContent.none);
		EditorGUI.BeginDisabledGroup (!isInstant.boolValue);
		EditorGUI.PropertyField (curveRect, curve, GUIContent.none);
		EditorGUI.EndDisabledGroup ();

		//Set indent back to what it was
		EditorGUI.indentLevel = indent;

		EditorGUI.EndProperty ();
	}
}
